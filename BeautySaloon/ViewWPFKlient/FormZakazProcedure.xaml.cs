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
    /// Логика взаимодействия для FormZakazProcedure.xaml
    /// </summary>
    public partial class FormZakazProcedure : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public ZakazProcedureViewModel Model { set { model = value; } get { return model; } }

        private readonly IProcedureService service;

        private ZakazProcedureViewModel model;

        public FormZakazProcedure(IProcedureService service)
        {
            InitializeComponent();
            Loaded += FormZakazProcedure_Load;
            comboBoxProcedure.SelectionChanged += comboBoxProcedure_SelectedIndexChanged;
            comboBoxProcedure.SelectionChanged += new SelectionChangedEventHandler(comboBoxProcedure_SelectedIndexChanged);
            this.service = service;
        }

        private void FormZakazProcedure_Load(object sender, EventArgs e)
        {
            List<ProcedureViewModel> list = service.GetList();
            try
            {
                if (list != null)
                {
                    comboBoxProcedure.DisplayMemberPath = "ProcedureName";
                    comboBoxProcedure.SelectedValuePath = "Id";
                    comboBoxProcedure.ItemsSource = list;
                    comboBoxProcedure.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model != null)
            {
                comboBoxProcedure.IsEnabled = false;
                foreach (ProcedureViewModel item in list)
                {
                    if (item.ProcedureName == model.ProcedureName)
                    {
                        comboBoxProcedure.SelectedItem = item;
                    }
                }
                textBoxPrice.Text = model.Price.ToString();
            }
        }

        private void CalcSum()
        {
            if (comboBoxProcedure.SelectedItem != null)
            {
                try
                {
                    int id = ((ProcedureViewModel)comboBoxProcedure.SelectedItem).Id;
                    ProcedureViewModel product = service.GetElement(id);
                    textBoxPrice.Text = product.Price.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxProcedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxProcedure.SelectedItem == null)
            {
                MessageBox.Show("Выберите услугу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new ZakazProcedureViewModel
                    {
                        ProcedureId = Convert.ToInt32(comboBoxProcedure.SelectedValue),
                        ProcedureName = comboBoxProcedure.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text)
                    };
                }
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
