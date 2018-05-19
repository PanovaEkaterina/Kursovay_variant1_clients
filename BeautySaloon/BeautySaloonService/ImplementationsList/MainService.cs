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
                    ProcedureId = rec.ProcedureId,
                    MasterId = rec.MasterId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    KlientFIO = rec.Klient.KlientFIO,
                    ProcedureName = rec.Procedure.ProcedureName,
                    MasterFIO = rec.Master.MasterFIO
                })
                .ToList();
            return result;
        }

        public void CreateRequest(RequestBindingModel model)
        {
            context.Requests.Add(new Request
            {
                KlientId = model.KlientId,
                ProcedureId = model.ProcedureId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = PaymentState.Не_оплачено
            });
            context.SaveChanges();
        }

        public void PayRequest(RequestBindingModel model)
        {
            context.Requests.Add(new Request
            {
                Sum = model.Sum,
                Status = PaymentState.Принят
            });
            context.SaveChanges();
        }
    }
}
