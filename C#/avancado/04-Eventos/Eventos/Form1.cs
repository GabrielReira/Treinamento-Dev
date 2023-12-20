using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eventos
{
    public partial class frmGerenciador : Form
    {
        private GerenciadorLatidos _gerenciadorLatidos;

        public frmGerenciador()
        {
            InitializeComponent();
            _gerenciadorLatidos = new GerenciadorLatidos();
            _gerenciadorLatidos.ExcessoDecibeisEvent += QuandoExcederDecibeis;
        }

        private void frmGerenciador_Load(object sender, EventArgs e)
        {
            pgbIntensidadeLatido.Value = 0;
        }

        private void btnLatir_Click(object sender, EventArgs e)
        {
            pgbIntensidadeLatido.Value = _gerenciadorLatidos.Latir();
        }

        private void QuandoExcederDecibeis(object sender, EventArgs e)
        {
            ExcessoDecibeisEventArgs eventArgs = (ExcessoDecibeisEventArgs)e;
            MessageBox.Show(string.Format("O latido passou do limite. Excesso de {0} decibéis.", eventArgs.IntensidadeLatido-80), "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
