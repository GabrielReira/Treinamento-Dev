using AgendaAdoNet.Classes;
using AgendaAdoNet.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaAdoNet
{
    public partial class frmAdicionarAlterarContato : Form
    {
        private Contato contato;

        public frmAdicionarAlterarContato(Contato contato = null)
        {
            this.contato = contato;
            InitializeComponent();
        }

        public frmAdicionarAlterarContato()
        {
            InitializeComponent();
        }

        private void frmAdicionarAlterarContato_Load(object sender, EventArgs e)
        {
            // Para alterar um contato
            if (this.contato != null)
            {
                txbNome.Text = this.contato.Nome;
                txbEmail.Text = this.contato.Email;
                txbTelefone.Text = this.contato.Telefone.ToString();
            }
            // Para incluir um contato
            else
            {
                txbNome.Text = string.Empty;
                txbEmail.Text = string.Empty;
                txbTelefone.Text = string.Empty;
            }
            txbNome.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ContatoDAO contatoDao = new ContatoDAO();
            if (this.contato != null)
            {
                this.contato.Nome = txbNome.Text;
                this.contato.Email = txbEmail.Text;
                this.contato.Telefone = Convert.ToInt32(txbTelefone.Text);
                contatoDao.Alterar(this.contato);
            }
            else
            {
                Contato novoContato = new Contato
                {
                    Nome = txbNome.Text,
                    Email = txbEmail.Text,
                    Telefone = Convert.ToInt32(txbTelefone.Text)
                };
                contatoDao.Adicionar(novoContato);
            }
            this.Close();
        }
    }
}
