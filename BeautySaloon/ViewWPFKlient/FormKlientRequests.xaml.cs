using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using System;
using System.Windows;
using Microsoft.Reporting.WinForms;
using Unity;
using Unity.Attributes;
using Microsoft.Win32;

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

        public FormKlientRequests(IReportService service)
        {
            InitializeComponent();
            this.service = service;
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
                });
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

        }
    }
}
