using System;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace ViewWPFKlient
{
    /// <summary>
    /// Логика взаимодействия для FormaBasic.xaml
    /// </summary>
    public partial class FormBasic : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormBasic()
        {
            InitializeComponent();
        }
        private void buttonReg_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReg>();
            form.ShowDialog();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {            
            var form = Container.Resolve<FormAuthorization>();
            form.ShowDialog();            
        }
    }
}
