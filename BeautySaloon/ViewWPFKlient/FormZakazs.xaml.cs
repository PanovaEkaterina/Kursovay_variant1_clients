using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormZakazs.xaml
    /// </summary>
    public partial class FormZakazs : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IZakazService service;

        public int Id { set { id = value; } }

        private int id;

        public FormZakazs(IZakazService service)
        {
            InitializeComponent();
            Loaded += FormZakazs_Load;
            this.service = service;
        }

        private void FormZakazs_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<ZakazViewModel> list = service.GetList(id);
                if (list != null)
                {
                    dataGridViewZakazs.ItemsSource = list;
                    dataGridViewZakazs.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewZakazs.Columns[4].Visibility = Visibility.Hidden;
                    dataGridViewZakazs.Columns[2].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewZakazs.SelectedItem != null)
            {
                var form = Container.Resolve<FormZakaz>();
                form.Id = ((ZakazViewModel)dataGridViewZakazs.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormZakaz>();
            form.ID = id;
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewZakazs.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int id = ((ZakazViewModel)dataGridViewZakazs.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
    }
}
