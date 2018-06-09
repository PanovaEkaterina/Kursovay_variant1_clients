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
    }
}
