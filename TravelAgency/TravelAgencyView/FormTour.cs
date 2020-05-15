using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.Interfaces;
using Unity;

namespace TravelAgencyView
{
    public partial class FormTour : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ITourLogic logic;
        private int? id;
        public FormTour(ITourLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormTour_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new TourBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.TourName;
                        textBoxCountry.Text = view.Country;
                        textBoxDuration.Text = view.Duration.ToString();
                        textBoxCost.Text = view.Cost.ToString();
                        textBoxTypeOfAllocation.Text = view.TypeOfAllocation;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCountry.Text))
            {
                MessageBox.Show("Заполните страну", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxDuration.Text))
            {
                MessageBox.Show("Заполните длительность", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCost.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxTypeOfAllocation.Text))
            {
                MessageBox.Show("Заполните тип размещения", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new TourBindingModel
                {
                    Id = id,
                    TourName = textBoxName.Text,
                    Country = textBoxCountry.Text,
                    Duration = Convert.ToInt32(textBoxDuration.Text),
                    Cost = Convert.ToDecimal(textBoxCost.Text),
                    TypeOfAllocation = textBoxTypeOfAllocation.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
