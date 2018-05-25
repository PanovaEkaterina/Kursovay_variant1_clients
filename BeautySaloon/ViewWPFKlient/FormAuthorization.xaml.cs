using System;
using System.Data.SqlClient;
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
        string AdminLogin = "admin";
        string AdminPassword = "admin";
        public FormAuthorization()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxFIO.Text == AdminLogin && textBoxPass.Text==AdminPassword)
            {
                Close();
                var form = Container.Resolve<ViewAdmin.FormMain>();
                form.ShowDialog();
            }

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
           
                Close();
                var form = Container.Resolve<FormMain>();
                
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
    }
}
