using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XML
{
    public partial class frmAgenda : Form
    {
        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "Agenda.xml";

        public frmAgenda()
        {
            InitializeComponent();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = CarregarTitulo();
            CriarContato();
            CarregarContatos();
        }

        private string CarregarTitulo()
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@EnderecoArquivo);
            XmlNode titulo = documentoXml.SelectSingleNode("/agenda/titulo");
            return titulo.InnerText;
        }

        private void CarregarContatos()
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@EnderecoArquivo);
            XmlNodeList contatos = documentoXml.SelectNodes("/agenda/contatos/contato");
            foreach(XmlNode contato in contatos)
            {
                string infoContato = "";
                string id = contato.Attributes["id"].Value;
                string nome = contato.Attributes["nome"].Value;
                string idade = contato.Attributes["idade"].Value;
                infoContato = id + " | " + nome + " | " + idade;
                lbxContatos.Items.Add(infoContato);
            }
        }

        private void CriarContato()
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@EnderecoArquivo);

            XmlAttribute atributoId = documentoXml.CreateAttribute("id");
            atributoId.Value = "4";
            XmlAttribute atributoNome = documentoXml.CreateAttribute("nome");
            atributoNome.Value = "Contato 4";
            XmlAttribute atributoIdade = documentoXml.CreateAttribute("idade");
            atributoIdade.Value = "28";

            XmlNode novoContato = documentoXml.CreateElement("contato");
            novoContato.Attributes.Append(atributoId);
            novoContato.Attributes.Append(atributoNome);
            novoContato.Attributes.Append(atributoIdade);

            XmlNode contatos = documentoXml.SelectSingleNode("/agenda/contatos");
            contatos.AppendChild(novoContato);
            documentoXml.Save(@EnderecoArquivo);
        }
    }
}
