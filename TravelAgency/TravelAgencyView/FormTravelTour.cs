using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModel;
using Unity;

namespace TravelAgencyView
{
    public partial class FormTravelTour : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public TravelTourViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private TravelTourViewModel model;
        public FormTravelTour(ITourLogic logic)
        {
            InitializeComponent();
            List<TourViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxTour.DisplayMember = "TourName";
                comboBoxTour.ValueMember = "Id";
                comboBoxTour.DataSource = list;
                comboBoxTour.SelectedItem = null;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {           
            if (comboBoxTour.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (model == null)
            {
                model = new TravelTourViewModel
                {
                    TourId = Convert.ToInt32(comboBoxTour.SelectedValue),
                };
            }
            MessageBox.Show("Сохранение прошло успешно", "Сообщение",
           MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
