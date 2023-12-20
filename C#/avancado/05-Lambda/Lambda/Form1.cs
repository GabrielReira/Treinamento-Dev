using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lambda
{
    public partial class frmLambda : Form
    {
        public frmLambda()
        {
            InitializeComponent();
        }

        private void btnConcatenar_Click(object sender, EventArgs e)
        {
            Func<string, string, string> concatenador = (s1, s2) => { return s1 + " " + s2; };
            Action<string> escritor = (s) => { txbResultado.Text = s; };

            escritor(concatenador(txbTexto1.Text, txbTexto2.Text));
        }
    }
}
