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
    /// Логика взаимодействия для FormZakaz.xaml
    /// </summary>
    public partial class FormZakaz : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IZakazService service;

        private int? id;

        private List<ZakazProcedureViewModel> zakazProcedures;

        public FormZakaz(IZakazService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += FormZakaz_Load;
        }

        private void FormZakaz_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ZakazViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.ZakazName;
                        textBoxPrice.Text = view.Price.ToString();
                        zakazProcedures = view.ZakazProcedures;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                zakazProcedures = new List<ZakazProcedureViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (zakazProcedures != null)
                {
                    dataGridViewZakaz.ItemsSource = null;
                    dataGridViewZakaz.ItemsSource = zakazProcedures;
                    dataGridViewZakaz.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewZakaz.Columns[1].Visibility = Visibility.Hidden;
                    dataGridViewZakaz.Columns[2].Visibility = Visibility.Hidden;
                    dataGridViewZakaz.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }       

        private void Summ()
        {
            decimal sum = 0;
            for (int i = 0; i < zakazProcedures.Count; ++i)
            {
                sum = sum + zakazProcedures[i].Price;
            }
            textBoxPrice.Text = sum.ToString();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormZakazProcedure>();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.ZakazId = id.Value;
                    zakazProcedures.Add(form.Model);
                }
                LoadData();
                Summ();

            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewZakaz.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        zakazProcedures.RemoveAt(dataGridViewZakaz.SelectedIndex);
                        Summ();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (zakazProcedures == null || zakazProcedures.Count == 0)
            {
                MessageBox.Show("Заполните услуги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<ZakazProcedureBindingModel> productComponentBM = new List<ZakazProcedureBindingModel>();
                for (int i = 0; i < zakazProcedures.Count; ++i)
                {
                    productComponentBM.Add(new ZakazProcedureBindingModel
                    {
                        Id = zakazProcedures[i].Id,
                        ZakazId = zakazProcedures[i].ZakazId,
                        ProcedureId = zakazProcedures[i].ProcedureId,
                        Price = zakazProcedures[i].Price,
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new ZakazBindingModel
                    {
                        Id = id.Value,
                        ZakazName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        ZakazProcedures = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new ZakazBindingModel
                    {
                        ZakazName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        ZakazProcedures = productComponentBM
                    });
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
