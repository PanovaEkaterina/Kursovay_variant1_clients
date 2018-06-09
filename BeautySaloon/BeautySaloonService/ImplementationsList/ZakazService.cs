using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    public class ZakazService : IZakazService
    {
        private SaloonDbContext context;

        public ZakazService(SaloonDbContext context)
        {
            this.context = context;
        }

        public List<ZakazViewModel> GetList(int id)
        {
            List<ZakazViewModel> result = context.Zakazs.Where(rec => rec.KlientID == id)
                .Select(rec => new ZakazViewModel
                {
                    Id = rec.Id,
                    ZakazName = rec.ZakazName,
                    KlientID= rec.KlientID,
                    Price = rec.Price,
                    ZakazProcedures = context.ZakazProcedures
                            .Where(recPC => recPC.ZakazId == rec.Id)
                            .Select(recPC => new ZakazProcedureViewModel
                            {
                                Id = recPC.Id,
                                ZakazId = recPC.ZakazId,
                                ProcedureId = recPC.ProcedureId,
                                ProcedureName = recPC.Procedure.ProcedureName,
                                Price = recPC.Price
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public ZakazViewModel GetElement(int id)
        {
            Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ZakazViewModel
                {
                    Id = element.Id,
                    ZakazName = element.ZakazName,
                    KlientID=element.KlientID,
                    Price = element.Price,
                    ZakazProcedures = context.ZakazProcedures
                            .Where(recPC => recPC.ZakazId == element.Id)
                            .Select(recPC => new ZakazProcedureViewModel
                            {
                                Id = recPC.Id,
                                ZakazId = recPC.ZakazId,
                                ProcedureId = recPC.ProcedureId,
                                ProcedureName = recPC.Procedure.ProcedureName,
                                Price = recPC.Price
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ZakazBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    
                    Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.ZakazName == model.ZakazName);
                    element = new Zakaz
                    {
                        ZakazName = model.ZakazName,
                        KlientID = model.KlientID,
                        Price = model.Price
                    };
                    context.Zakazs.Add(element);
                    context.SaveChanges();
                    
                    var groupProcedures = model.ZakazProcedures
                                                .GroupBy(rec => rec.ProcedureId)
                                                .Select(rec => new
                                                {
                                                    ProcedureId = rec.Key,
                                                    Price = rec.Sum(r => r.Price)
                                                });
                    
                    foreach (var groupProcedure in groupProcedures)
                    {
                        context.ZakazProcedures.Add(new ZakazProcedure
                        {
                            ZakazId = element.Id,
                            ProcedureId = groupProcedure.ProcedureId,
                            Price = groupProcedure.Price
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(ZakazBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Zakaz element = context.Zakazs.FirstOrDefault(rec =>
                                        rec.ZakazName == model.ZakazName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть заказ с таким названием");
                    }
                    element = context.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.ZakazName = model.ZakazName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    
                    var compIds = model.ZakazProcedures.Select(rec => rec.ProcedureId).Distinct();
                    var updateProcedures = context.ZakazProcedures
                                                    .Where(rec => rec.ZakazId == model.Id &&
                                                        compIds.Contains(rec.ProcedureId));
                    foreach (var updateProcedure in updateProcedures)
                    {
                        updateProcedure.Price = model.ZakazProcedures
                                                        .FirstOrDefault(rec => rec.Id == updateProcedure.Id).Price;
                    }
                    context.SaveChanges();
                    context.ZakazProcedures.RemoveRange(
                                        context.ZakazProcedures.Where(rec => rec.ZakazId == model.Id &&
                                                                            !compIds.Contains(rec.ProcedureId)));
                    context.SaveChanges();
                    
                    var groupProcedures = model.ZakazProcedures
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.ProcedureId)
                                                .Select(rec => new
                                                {
                                                    ProcedureId = rec.Key,
                                                    Price = rec.Sum(r => r.Price)
                                                });
                    foreach (var groupProcedure in groupProcedures)
                    {
                        ZakazProcedure elementPC = context.ZakazProcedures
                                                .FirstOrDefault(rec => rec.ZakazId == model.Id &&
                                                                rec.ProcedureId == groupProcedure.ProcedureId);
                        if (elementPC != null)
                        {
                            elementPC.Price += groupProcedure.Price;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ZakazProcedures.Add(new ZakazProcedure
                            {
                                ZakazId = model.Id,
                                ProcedureId = groupProcedure.ProcedureId,
                                Price = groupProcedure.Price
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        
                        context.ZakazProcedures.RemoveRange(
                                            context.ZakazProcedures.Where(rec => rec.ZakazId == id));
                        context.Zakazs.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
