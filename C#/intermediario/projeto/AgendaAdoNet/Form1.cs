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
    public partial class frmAgenda : Form
    {
        public frmAgenda()
        {
            InitializeComponent();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            CarregarDataGridView();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int id = (int)dvgAgenda.CurrentRow.Cells[0].Value;
            ContatoDAO contatoDao = new ContatoDAO();
            contatoDao.Excluir(id);
            CarregarDataGridView();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            frmAdicionarAlterarContato form = new frmAdicionarAlterarContato();
            form.ShowDialog();
            CarregarDataGridView();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Contato contatoAlterado = new Contato
            {
                Id = (int)dvgAgenda.CurrentRow.Cells[0].Value,
                Nome = dvgAgenda.CurrentRow.Cells[1].Value.ToString(),
                Email = dvgAgenda.CurrentRow.Cells[2].Value.ToString(),
                Telefone = (int)dvgAgenda.CurrentRow.Cells[3].Value
            };
            frmAdicionarAlterarContato form = new frmAdicionarAlterarContato(contatoAlterado);
            form.ShowDialog();
            CarregarDataGridView();
        }

        private void CarregarDataGridView()
        {
            ContatoDAO contatoDao = new ContatoDAO();

            // Utlizando DataTable
            DataTable dataTable = contatoDao.GetContatos();
            dvgAgenda.DataSource = dataTable;
            dvgAgenda.Refresh();

            // Utlizando DataSet
            /* DataSet ds = contatoDao.GetContatos();
            dvgAgenda.DataSource = ds.Tables["CONTATOS"];
            dvgAgenda.Refresh(); */

            CarregarStatusStrip();
        }

        private void CarregarStatusStrip()
        {
            ContatoDAO contatoDao = new ContatoDAO();
            int qtdContatos = contatoDao.QtdContatos();
            stsContatos.Items[0].Text = qtdContatos.ToString() + " contato(s).";
        }
    }
}
