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

        private readonly IZakazService serviceZ;

        private readonly IMainService serviceM;

        public int Id { set { id = value; } }

        private int id;

        public FormCreateRequest(IZakazService serviceZ, IMainService serviceM)
        {
            InitializeComponent();
            Loaded += FormCreateRequest_Load;
            comboBoxZakaz.SelectionChanged += comboBoxZakaz_SelectedIndexChanged;
            comboBoxZakaz.SelectionChanged += new SelectionChangedEventHandler(comboBoxZakaz_SelectedIndexChanged);
            this.serviceZ = serviceZ;
            this.serviceM = serviceM;
        }

        private void FormCreateRequest_Load(object sender, EventArgs e)
        {
            try
            {
                List<ZakazViewModel> listZ = serviceZ.GetList(id);
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
                    KlientId = id,
                    ZakazId = ((ZakazViewModel)comboBoxZakaz.SelectedItem).Id,
                    Sum = Convert.ToDecimal(textBoxSum.Text),
                    DataVisit = datePickerDay.SelectedDate.ToString(),

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
