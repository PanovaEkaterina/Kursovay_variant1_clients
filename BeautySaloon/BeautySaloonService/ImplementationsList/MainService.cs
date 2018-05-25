using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    public class MainService : IMainService
    {
        private SaloonDbContext context;

        public MainService(SaloonDbContext context)
        {
            this.context = context;
        }

        public List<RequestViewModel> GetList()
        {
            List<RequestViewModel> result = context.Requests
                .Select(rec => new RequestViewModel
                {
                    Id = rec.Id,
                    KlientId = rec.KlientId,
                    ZakazId = rec.ZakazId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    Status = rec.Status.ToString(),
                    Sum = rec.Sum,
                    SumPay = rec.SumPay,
                    DateVisit = rec.DateVisit,
                    KlientFIO = rec.Klient.KlientFIO,
                    ZakazName = rec.Zakaz.ZakazName,
                })
                .ToList();
            return result;
        }

        public void CreateRequest(RequestBindingModel model)
        {
            context.Requests.Add(new Request
            {
                KlientId = model.KlientId,
                ZakazId = model.ZakazId,
                DateCreate = DateTime.Now,
                DateVisit = model.DataVisit,
                Sum = model.Sum,
                SumPay = model.SumPay,
                Status = PaymentState.Не_оплачен
            });
            context.SaveChanges();
        }

        public void PayRequest(RequestBindingModel model)
        {
            context.Requests.Add(new Request
            {
                SumPay = model.SumPay,
                Status = PaymentState.Оплачен
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request element = context.Requests.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        context.Requests.Remove(element);
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
