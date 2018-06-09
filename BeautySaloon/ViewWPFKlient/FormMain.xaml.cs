using BeautySaloonService;
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
    /// Логика взаимодействия для FormMain.xaml
    /// </summary>
    public partial class FormMain : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private SaloonDbContext context;

        private readonly IMainService service;

        public int Id { set { id = value; } }

        private int id;

        public FormMain(IMainService service, SaloonDbContext context)
        {
            InitializeComponent();
            this.service = service;
            this.context = context;
        }     
               
        private void LoadData()
        {
            try
            {
                List<RequestViewModel> list = service.GetList(id);
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Visibility = Visibility.Hidden;
                    dataGridView.Columns[3].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormZakazs>();
            form.Id = id;
            form.ShowDialog();
        }

        private void прайсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSendMail>();
            form.Id = id;
            form.ShowDialog();
        }

        private void buttonCreateRequest_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateRequest>();
            form.Id = id;
            form.ShowDialog();
            LoadData();
        }



        private void buttonPayRequest_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                var form = Container.Resolve<FormPayRequest>();
                form.Id = ((RequestViewModel)dataGridView.SelectedItem).Id;
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int id = ((RequestViewModel)dataGridView.SelectedItem).Id;
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

        private void заказыКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormKlientRequests>();
            form.Id = id;
            form.ShowDialog();
        }
    }
}
