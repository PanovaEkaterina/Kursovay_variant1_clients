using BeautySaloonModels;
using BeautySaloonService;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormAuthorization.xaml
    /// </summary>
    public partial class FormAuthorization : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private SaloonDbContext context;

        public FormAuthorization(SaloonDbContext context)
        {
            InitializeComponent();
            this.context = context;
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
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-8AL8991\SQLEXPRESS;Initial Catalog=BeautySaloon;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Klients where KlientFIO = '" + textBoxFIO.Text + "' and KlientPassword = '" + textBoxPass.Text + "'", conn);
            SqlDataReader dt;
            dt = cmd.ExecuteReader();
            int count = 0;

            while (dt.Read())
            {
                count += 1;

            }
            if (count == 1)
            {
                Klient element = context.Klients.FirstOrDefault(kl => kl.KlientFIO == textBoxFIO.Text & kl.KlientPassword== textBoxPass.Text);
                int id = element.Id;
                
                var form = Container.Resolve<FormMain>();
                form.Id = id;
                form.ShowDialog();

            }
            else
            {
                MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }                       
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<FormReg>();
            form.ShowDialog();
        }
    }
}
