using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    public class ProcedureService : IProcedureService
    {
        private SaloonDbContext context;

        public ProcedureService(SaloonDbContext context)
        {
            this.context = context;
        }

        public List<ProcedureViewModel> GetList()
        {
            List<ProcedureViewModel> result = context.Procedures
                .Select(rec => new ProcedureViewModel
                {
                    Id = rec.Id,
                    ProcedureName = rec.ProcedureName,
                    Price = rec.Price
                })
                .ToList();
            return result;
        }

        public ProcedureViewModel GetElement(int id)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ProcedureViewModel
                {
                    Id = element.Id,
                    ProcedureName = element.ProcedureName,
                    Price = element.Price
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ProcedureBindingModel model)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec => rec.ProcedureName == model.ProcedureName);
            if (element != null)
            {
                throw new Exception("Уже есть услуга с таким названием");
            }
            context.Procedures.Add(new Procedure
            {
                ProcedureName = model.ProcedureName, 
                Price = model.Price
            });
            context.SaveChanges();
        }

        public void UpdElement(ProcedureBindingModel model)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec =>
                                        rec.ProcedureName == model.ProcedureName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть услуга с таким названием");
            }
            element = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ProcedureName = model.ProcedureName;
            element.Price = model.Price;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Procedure element = context.Procedures.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Procedures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
