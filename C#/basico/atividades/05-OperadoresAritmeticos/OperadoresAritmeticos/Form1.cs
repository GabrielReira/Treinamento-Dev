using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperadoresAritmeticos
{
    public partial class frmCalculadora : Form
    {
        int num1;
        int num2;

        public frmCalculadora()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSoma_Click(object sender, EventArgs e)
        {
            num1 = Convert.ToInt32(txbNumero1.Text);
            num2 = Convert.ToInt32(txbNumero2.Text);
            txbResultado.Text = Somar(num1, num2).ToString();
        }

        private void btnSubtracao_Click(object sender, EventArgs e)
        {
            num1 = Convert.ToInt32(txbNumero1.Text);
            num2 = Convert.ToInt32(txbNumero2.Text);
            txbResultado.Text = Subtrair(num1, num2).ToString();
        }

        private void btnMultiplica_Click(object sender, EventArgs e)
        {
            num1 = Convert.ToInt32(txbNumero1.Text);
            num2 = Convert.ToInt32(txbNumero2.Text);
            txbResultado.Text = Multiplicar(num1, num2).ToString();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            num1 = Convert.ToInt32(txbNumero1.Text);
            num2 = Convert.ToInt32(txbNumero2.Text);
            txbResultado.Text = Dividir(num1, num2).ToString();
        }

        int Somar(int a, int b)
        {
            return a + b;
        }
        int Subtrair(int a, int b)
        {
            return a - b;
        }
        int Multiplicar(int a, int b)
        {
            return a * b;
        }
        int Dividir(int a, int b)
        {
            return a / b;
        }
    }
}
