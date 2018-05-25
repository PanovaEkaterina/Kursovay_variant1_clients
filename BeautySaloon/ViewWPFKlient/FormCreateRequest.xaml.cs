using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;
using Unity.Attributes;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormCreateRequest.xaml
    /// </summary>
    public partial class FormCreateRequest : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IKlientService serviceK;

        private readonly IZakazService serviceZ;

        private readonly IMainService serviceM;

        public FormCreateRequest(IKlientService serviceK, IZakazService serviceZ, IMainService serviceM)
        {
            InitializeComponent();
            Loaded += FormCreateRequest_Load;
            comboBoxZakaz.SelectionChanged += comboBoxZakaz_SelectedIndexChanged;
            comboBoxZakaz.SelectionChanged += new SelectionChangedEventHandler(comboBoxZakaz_SelectedIndexChanged);
            this.serviceK = serviceK;
            this.serviceZ = serviceZ;
            this.serviceM = serviceM;
        }

        private void FormCreateRequest_Load(object sender, EventArgs e)
        {
            try
            {
                List<KlientViewModel> listK = serviceK.GetList();
                if (listK != null)
                {
                    comboBoxKlient.DisplayMemberPath = "KlientFIO";
                    comboBoxKlient.SelectedValuePath = "Id";
                    comboBoxKlient.ItemsSource = listK;
                    comboBoxKlient.SelectedItem = null;
                }
                List<ZakazViewModel> listZ = serviceZ.GetList();
                if (listZ != null)
                {
                    comboBoxZakaz.DisplayMemberPath = "ZakazName";
                    comboBoxZakaz.SelectedValuePath = "Id";
                    comboBoxZakaz.ItemsSource = listZ;
                    comboBoxZakaz.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxZakaz.SelectedItem != null)
            {
                try
                {
                    int id = ((ZakazViewModel)comboBoxZakaz.SelectedItem).Id;
                    ZakazViewModel product = serviceZ.GetElement(id);
                    textBoxSum.Text = product.Price.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxSum_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxZakaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxKlient.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxZakaz.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerDay.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату посещения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerDay.SelectedDate <= DateTime.Now.Date)
            {
                MessageBox.Show("Дата посещения должна быть больше текущего дня", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            try
            {
                serviceM.CreateRequest(new RequestBindingModel
                {
                    KlientId = ((KlientViewModel)comboBoxKlient.SelectedItem).Id,
                    ZakazId = ((ZakazViewModel)comboBoxZakaz.SelectedItem).Id,
                    Sum = Convert.ToDecimal(textBoxSum.Text),
                    DataVisit = datePickerDay.SelectedDate.ToString(),
                    SumPay = 0,

                });
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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
