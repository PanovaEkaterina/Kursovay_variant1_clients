using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    class MainService : IMainService
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
                    RequestProcedureId = rec.RequestProcedureId,
                    PaymentId = rec.PaymentId,                               
                    Count = rec.Count,
                    Summ = rec.Summ,
                    KlientFIO = rec.Klient.KlientFIO,
                    ProcedureName = rec.ProcedureName,
                })
                .ToList();
            return result;
        }

        public void CreateRequest(RequestBindingModel model)
        {
            context.Requests.Add(new Request
            {
                KlientId = model.KlientId,
                RequestProcedureId = model.RequestProcedureId,
                Date = DateTime.Now,
                Count = model.Count,
                Summ = model.Summ,
            });
            context.SaveChanges();
        }

        public void PartiallyPaid(int id)
        {
            Request element = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = PaymentState.Оплачено_частично;
            context.SaveChanges();
        }

        public void PayRequest(int id)
        {
            Request element = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = PaymentState.Оплачено;
            context.SaveChanges();
        }
    }
}
