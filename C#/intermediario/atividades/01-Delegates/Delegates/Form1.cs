using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delegates
{
    public partial class frmCalculadora : Form
    {
        public frmCalculadora()
        {
            InitializeComponent();
        }

        private delegate double ExecutarOperacao(double num1, double num2);
        private ExecutarOperacao operacao;

        private double Calcular()
        {
            double num1 = Convert.ToDouble(txbNum1.Text);
            double num2 = Convert.ToDouble(txbNum2.Text);
            return operacao(num1, num2);
        }

        private double Somar(double num1, double num2)
        {
            return num1 + num2;
        }

        private double Subtrair(double num1, double num2)
        {
            return num1 - num2;
        }

        private double Multiplicar(double num1, double num2)
        {
            return num1 * num2;
        }

        private double Dividir(double num1, double num2)
        {
            return num1 / num2;
        }

        private void btnAdicao_Click(object sender, EventArgs e)
        {
            operacao = new ExecutarOperacao(Somar);
            txbResultado.Text = Calcular().ToString();
        }

        private void btnSubtracao_Click(object sender, EventArgs e)
        {
            operacao = new ExecutarOperacao(Subtrair);
            txbResultado.Text = Calcular().ToString();
        }

        private void btnMultiplicacao_Click(object sender, EventArgs e)
        {
            operacao = new ExecutarOperacao(Multiplicar);
            txbResultado.Text = Calcular().ToString();
        }

        private void btnDivisao_Click(object sender, EventArgs e)
        {
            operacao = new ExecutarOperacao(Dividir);
            txbResultado.Text = Calcular().ToString();
        }
    }
}
