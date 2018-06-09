using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using System;
using System.Windows;
using Microsoft.Reporting.WinForms;
using Unity;
using Unity.Attributes;
using System.Net.Mail;
using System.Net;
using BeautySaloonModels;
using BeautySaloonService;
using System.Linq;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormKlientRequests.xaml
    /// </summary>
    public partial class FormKlientRequests : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReportService service;

        private readonly IKlientService serviceK;

        private SaloonDbContext context;

        public int Id { set { id = value; } }

        private int id;
        
        public FormKlientRequests(IReportService service, IKlientService serviceK, SaloonDbContext context)
        {
            InitializeComponent();
            this.service = service;
            this.serviceK = serviceK;
            this.context = context;
        }

        public void SendMail()
        {
            Klient element = context.Klients.FirstOrDefault(kl => kl.Id == id);
            string mail = element.Mail;  

            MailAddress from = new MailAddress("LabWork15kafIs@gmail.com", "BeautySaloon");
            MailAddress to = new MailAddress(mail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Отчет об оплате";
            m.Body = "<h2></h2>";
            m.Attachments.Add(new Attachment("D://payment.pdf"));
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("LabWork15kafIs@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();
            MessageBox.Show("Письмо отправлено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                reportViewer.LocalReport.ReportEmbeddedResource = "ViewWPFKlient.ReportKlientRequest.rdlc";
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + dateTimePickerFrom.SelectedDate.ToString() +
                                            " по " + dateTimePickerTo.SelectedDate.ToString());
                reportViewer.LocalReport.SetParameters(parameter);


                var dataSource = service.GetKlientRequests(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate,
                    DateTo = dateTimePickerTo.SelectedDate
                },id);
                ReportDataSource source = new ReportDataSource("DataSetRequests", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonMail_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                try
                {
                    service.SaveKlientRequests(new ReportBindingModel
                    {
                        FileName = "D://payment.pdf",
                        DateFrom = dateTimePickerFrom.SelectedDate,
                        DateTo = dateTimePickerTo.SelectedDate
                    },id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            SendMail();
        }        
    }
}
