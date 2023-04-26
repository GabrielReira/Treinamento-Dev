using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enumeradores
{
    public partial class frmEnums : Form
    {
        public frmEnums()
        {
            InitializeComponent();
        }

        private void frmEnums_Shown(object sender, EventArgs e)
        {
            cmbDias.DataSource = Enum.GetNames(typeof(DiasEnum));
        }

        private void btnExibirDia_Click(object sender, EventArgs e)
        {
            MessageBox.Show("O dia escolhido foi: " + Enum.GetName(typeof(DiasEnum), cmbDias.SelectedIndex));
        }
    }
}
