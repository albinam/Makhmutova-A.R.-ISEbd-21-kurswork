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
using TravelAgencyBusinessLogic.ViewModel;
using Unity;

namespace TravelAgencyView
{
    public partial class FormTravel : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private List<TravelTourViewModel> TravelTours;
        private readonly ITravelLogic logic;

        public FormTravel(ITravelLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormTravel_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TravelViewModel TravelView = logic.Read(new TravelBindingModel { Id = id.Value })?[0];
                    if (TravelView != null)
                    {
                        textBoxName.Text = TravelView.TravelName;
                        textBoxPrice.Text = TravelView.FinalCost.ToString();
                        TravelTours = TravelView.TravelTours;
                        LoadData();
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                TravelTours = new List<TravelTourViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (TravelTours != null)
                {
                    dataGridView.DataSource = TravelTours;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTravelTour>();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.TravelId = id.Value;
                    }
                    TravelTours.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        TravelTours.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormTravelTour>();

                form.Model = TravelTours[dataGridView.SelectedRows[0].Cells[0].RowIndex];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    TravelTours[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
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
            if (TravelTours == null || TravelTours.Count == 0)
            {
                MessageBox.Show("Заполните туры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<TravelTourBindingModel> TravelToursBinding = new List<TravelTourBindingModel>();
                for (int i = 0; i < TravelTours.Count; ++i)
                {
                    TravelToursBinding.Add(new TravelTourBindingModel
                    {
                        Id = TravelTours[i].Id,
                        TravelId = TravelTours[i].TravelId,
                        TourId = TravelTours[i].TourId,
                        Count = TravelTours[i].Count,
                    });
                }
                if (id.HasValue)
                {
                    logic.CreateOrUpdate(new TravelBindingModel
                    {
                        Id = id.Value,
                        TravelName = textBoxName.Text,
                        FinalCost = Convert.ToDecimal(textBoxPrice.Text),
                        TravelTours = TravelToursBinding
                    });
                }
                else
                {
                    logic.CreateOrUpdate(new TravelBindingModel
                    {
                        TravelName = textBoxName.Text,
                        FinalCost = Convert.ToDecimal(textBoxPrice.Text),
                        TravelTours = TravelToursBinding
                    });
                }
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
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}

