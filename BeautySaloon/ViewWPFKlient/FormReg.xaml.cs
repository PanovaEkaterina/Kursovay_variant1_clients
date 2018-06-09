using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormReg.xaml
    /// </summary>
    public partial class FormReg : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IKlientService service;

        private int? id;

        public FormReg(IKlientService service)
        {
            InitializeComponent();
            this.service = service;
        }

        

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPass.Text))
            {
                MessageBox.Show("Придумайте пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMail.Text))
            {
                MessageBox.Show("Введите свою почту", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (textBoxPass.Text!= textBoxPassRepeat.Text)
            {
                MessageBox.Show("Пароли должны совпадать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                string fio = textBoxFIO.Text;
                string mail = textBoxMail.Text;
                string pass = textBoxPass.Text;
                if (!string.IsNullOrEmpty(mail))
                {
                    if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                    {
                        MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }             
                if (id.HasValue)
                {
                    service.UpdElement(new KlientBindingModel
                    {
                        Id = id.Value,
                        KlientFIO = textBoxFIO.Text,
                        KlientPassword = pass,
                        Mail = mail
                });
                }
                else
                {
                    service.AddElement(new KlientBindingModel
                    {
                        KlientFIO = textBoxFIO.Text,
                        KlientPassword = pass,
                        Mail = mail
                    });
                }
                MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);               
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
