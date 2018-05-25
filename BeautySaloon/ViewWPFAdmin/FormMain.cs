using BeautySaloonService.Interfaces;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace ViewAdmin
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;

        public FormMain(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void добавитьToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var form = Container.Resolve<FormProcedures>();
            form.ShowDialog();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormKlients>();
            form.ShowDialog();
        }
    }
}
