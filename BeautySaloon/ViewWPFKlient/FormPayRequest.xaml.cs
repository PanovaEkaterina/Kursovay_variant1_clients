using BeautySaloonModels;
using BeautySaloonService;
using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using System;
using System.Linq;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormPayRequest.xaml
    /// </summary>
    public partial class FormPayRequest : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private SaloonDbContext context;

        public int Id { set { id = value; } }

        private readonly IMainService serviceM;

        private int id;

        public FormPayRequest(IMainService serviceM, SaloonDbContext context)
        {
            InitializeComponent();
            Loaded += FormPayRequest_Load;
            this.serviceM = serviceM;
            this.context = context;
        }

        private void FormPayRequest_Load(object sender, EventArgs e)
        {
            try
            {
                if (id==0)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }

                Request element = context.Requests.FirstOrDefault(kl => kl.Id == id);
                decimal summ = element.Sum;
                decimal payment = element.SumPay;
                decimal zadol = summ - payment;

                textBoxSumm.Text = summ.ToString();
                textBoxSumpay.Text = payment.ToString();
                textBoxZadol.Text = zadol.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Request element = context.Requests.FirstOrDefault(kl => kl.Id == id);
            decimal sumpay = element.SumPay;

            if (textBoxSum.Text == null)
            {
                MessageBox.Show("Введите сумму оплаты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if(sumpay + Convert.ToDecimal(textBoxSum.Text) > element.Sum)
                {
                    MessageBox.Show("Сумма оплаты больше суммы заказа", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (sumpay + Convert.ToDecimal(textBoxSum.Text) < element.Sum)
                {
                    serviceM.PayPartRequest(new RequestBindingModel
                    {
                        Id = id,
                        SumPay = sumpay + Convert.ToDecimal(textBoxSum.Text)
                    });
                    MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                if (sumpay + Convert.ToDecimal(textBoxSum.Text) == element.Sum)
                {
                    serviceM.PayRequest(new RequestBindingModel
                    {
                        Id = id,
                        SumPay = sumpay + Convert.ToDecimal(textBoxSum.Text)
                    });
                    MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }        
    }
}
