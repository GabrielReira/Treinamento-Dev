using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWhile
{
    public partial class frmRepeticao : Form
    {
        public frmRepeticao()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(txbNumero.Text);
            /*
            for(int i = 0; i <= 7; i++)
            {
                lsbResultado.Items.Add(string.Format("{1} x {0} = {2}", numero, i, numero*i));
            }
            */
            int i = 0;
            while(i <= 7)
            {
                lsbResultado.Items.Add(string.Format("{1} x {0} = {2}", numero, i, numero * i));
                i++;
            }
        }
    }
}
