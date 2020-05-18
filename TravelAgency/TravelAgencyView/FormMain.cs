using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Interfaces;
using Unity;

namespace TravelAgencyView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly MainLogic logic;
        private readonly ITravelLogic travelLogic;
        public FormMain(MainLogic logic, ITravelLogic travelLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.travelLogic = travelLogic;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = travelLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    //dataGridView.Columns[0].Visible = false;
                    ////dataGridView.Columns[1].Visible = false;
                    // dataGridView.Columns[2].Visible = false;
                    //dataGridView.Columns[5].Visible = false;
                    //dataGridView.Columns[5].AutoSizeMode =
                    //  DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }    
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void турыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTours>();
            form.ShowDialog();
        }

        private void путешествияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTravels>();
            form.ShowDialog();

        }
    }
}