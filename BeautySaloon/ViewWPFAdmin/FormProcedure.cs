using BeautySaloonService.BindingModel;
using BeautySaloonService.Interfaces;
using BeautySaloonService.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace ViewAdmin
{
    public partial class FormProcedure : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IProcedureService service;

        private int? id;

        private List<ZakazProcedureViewModel> zakazProcedures;

        public FormProcedure(IProcedureService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormProcedure_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ProcedureViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.ProcedureName;
                        textBoxPrice.Text = view.Price.ToString();
                        //zakazProcedures = view.ZakazProcedures;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                zakazProcedures = new List<ZakazProcedureViewModel>();
            }
        }
                
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {                
                if (id.HasValue)
                {
                    service.UpdElement(new ProcedureBindingModel
                    {
                        Id = id.Value,
                        ProcedureName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                    });
                }
                else
                {
                    service.AddElement(new ProcedureBindingModel
                    {
                        ProcedureName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
