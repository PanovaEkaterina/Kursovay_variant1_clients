using BeautySaloonModels;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySaloonService.ImplementationsList
{
    public class KlientService : IKlientService
    {
        private SaloonDbContext context;

        public KlientService(SaloonDbContext context)
        {
            this.context = context;
        }

        public void AddElement(KlientBindingModel model)
        {
            Klient element = context.Klients.FirstOrDefault(rec => rec.KlientFIO == model.KlientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким именем");
            }
            context.Klients.Add(new Klient
            {
                Id = model.Id,
                KlientFIO = model.KlientFIO,
                Mail = model.Mail,
                KlientPassword = model.KlientPassword,
                Requests = null,
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Klient element = context.Klients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Klients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
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
                    Mail = element.Mail,
                    KlientPassword = element.KlientPassword
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<KlientViewModel> GetList()
        {
            List<KlientViewModel> result = context.Klients.Select(rec => new KlientViewModel
            {
                Id = rec.Id,
                KlientFIO = rec.KlientFIO,
                Mail = rec.Mail,
                KlientPassword = rec.KlientPassword
        })
                .ToList();
            return result;
        }

        public void UpdElement(KlientBindingModel model)
        {
            Klient element = context.Klients.FirstOrDefault(rec =>
                                    rec.KlientFIO == model.KlientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Klients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.KlientFIO = model.KlientFIO;
            element.Id = model.Id;
            element.Mail = model.Mail;
            element.KlientPassword = model.KlientPassword;
            context.SaveChanges();
        }

        public string GenerateLogin(string fio)
        {
            char split = ' ';
            string firstName = fio.Substring(0, fio.IndexOf(split));

            fio = fio.Substring(fio.IndexOf(split) + 1);

            string name = fio.Substring(0, fio.IndexOf(split));

            string namePath = string.Empty;

            int position = 1;

            while (true)
            {
                if (name.Length > 0)
                {
                    namePath += name.First();
                    name = name.Substring(1);
                }
                else
                {
                    position++;
                }
                string login = firstName + "." + namePath + ((position > 1) ? position + "" : "");

                Klient Klient = context.Klients.FirstOrDefault(rec => rec.KlientFIO.Equals(login));

                if (Klient == null)
                {
                    return login;
                }
            }
        }                
    }
}
