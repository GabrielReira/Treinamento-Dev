using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaDeContatos
{
    public partial class frmAgendaDeContatos : Form
    {
        private OperacaoEnum acao;
        public frmAgendaDeContatos()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            AlterarBotoesCancelarSalvar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            CarregarListaContatos();
            AlterarEstadosCampos(false);
        }

        private void AlterarBotoesCancelarSalvar(bool estado)
        {
            btnCancelar.Enabled = estado;
            btnSalvar.Enabled = estado;
        }

        private void AlterarBotoesIncluirAlterarExcluir(bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void btnIncluir_Click_1(object sender, EventArgs e)
        {
            AlterarBotoesCancelarSalvar(true);
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarEstadosCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesCancelarSalvar(true);
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarEstadosCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza?", "AVISO!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);

                List<Contato> listaContatos = new List<Contato>();
                foreach (Contato contato in lbxContatos.Items)
                {
                    listaContatos.Add(contato);
                }
                ManipuladorArquivo.EscreverArquivo(listaContatos);
                CarregarListaContatos();
                LimparCampos();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesCancelarSalvar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            AlterarEstadosCampos(false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Contato contato = new Contato
            {
                Nome = txbNome.Text,
                Email = txbEmail.Text,
                NumeroTelefone = txbTelefone.Text
            };
            List<Contato> listaContatos = new List<Contato>();
            foreach (Contato c in lbxContatos.Items)
            {
                listaContatos.Add(c);
            }

            // Incluir novo contato
            if (acao == OperacaoEnum.INCLUIR)
            {
                listaContatos.Add(contato);
            }
            // Alterar contato existente
            else
            {
                int indice = lbxContatos.SelectedIndex;
                listaContatos.RemoveAt(indice);
                listaContatos.Insert(indice, contato);
            }

            ManipuladorArquivo.EscreverArquivo(listaContatos);
            CarregarListaContatos();
            AlterarBotoesCancelarSalvar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            LimparCampos();
            AlterarEstadosCampos(false);
        }

        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorArquivo.LerArquivo().ToArray());
        }

        private void LimparCampos()
        {
            txbNome.Text = "";
            txbEmail.Text = "";
            txbTelefone.Text = "";
        }

        private void AlterarEstadosCampos(bool estado)
        {
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
            txbTelefone.Enabled = estado;
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbTelefone.Text = contato.NumeroTelefone;
        }
    }
}
