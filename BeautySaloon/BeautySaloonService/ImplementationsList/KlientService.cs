using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    public class KlientService: IKlientService
    {
        private SaloonDbContext context;

        public KlientService(SaloonDbContext context)
        {
            this.context = context;
        }

        public KlientViewModel GetElement(int id)
        {
            Klient element = context.Klients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new KlientViewModel
                {
                    Id = element.Id,
                    KlientFIO = element.KlientFIO,
                    KlientPassword = element.KlientPassword,
                    Mail = element.Mail
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(KlientBindingModel model)
        {
            Klient element = context.Klients.FirstOrDefault(rec => rec.KlientFIO == model.KlientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Klients.Add(new Klient
            {
                KlientFIO = model.KlientFIO,
                KlientPassword = model.KlientPassword,
                Mail = model.Mail
            });
            context.SaveChanges();
        }

        public void UpdElement(KlientBindingModel model)
        {
            Klient element = context.Klients.FirstOrDefault(rec =>
                                    rec.KlientFIO == model.KlientFIO && rec.Id != model.Id);
            element = context.Klients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.KlientFIO = model.KlientFIO;
            element.KlientPassword = model.KlientPassword;
            element.Mail = model.Mail;
            context.SaveChanges();
        }        
    }
}
