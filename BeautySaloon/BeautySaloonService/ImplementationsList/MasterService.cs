using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    public class MasterService : IMasterService
    {
        private SaloonDbContext context;

        public MasterService(SaloonDbContext context)
        {
            this.context = context;
        }

        public void AddElement(MasterBindingModel model)
        {
            Master element = context.Masters.FirstOrDefault(rec => rec.MasterFIO == model.MasterFIO);
            if (element != null)
            {
                throw new Exception("Уже есть админ с таким ФИО");
            }
            context.Masters.Add(new Master
            {
                MasterFIO = model.MasterFIO,
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Master element = context.Masters.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Masters.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public MasterViewModel GetElement(int id)
        {
            Master element = context.Masters.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MasterViewModel
                {
                    Id = element.Id,
                    MasterFIO = element.MasterFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<MasterViewModel> GetList()
        {
            List<MasterViewModel> result = context.Masters.Select(rec => new MasterViewModel
            {
                Id = rec.Id,
                MasterFIO = rec.MasterFIO,
            })
                .ToList();
            return result;
        }

        public void UpdElement(MasterBindingModel model)
        {
            Master element = context.Masters.FirstOrDefault(rec =>
                                    rec.MasterFIO == model.MasterFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть админ с таким ФИО");
            }
            element = context.Masters.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.MasterFIO = model.MasterFIO;
            context.SaveChanges();
        }
    }
}
