using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Switch
{
    public partial class frmSwitch : Form
    {
        public frmSwitch()
        {
            InitializeComponent();
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(txbNumero.Text);
            switch (numero)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    MessageBox.Show("Número de baixo valor.");
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    MessageBox.Show("Número de valor médio.");
                    break;
                default:
                    MessageBox.Show("Número negativo ou de alto valor.");
                    break;
            }
        }
    }
}
