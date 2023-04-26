using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDeContatos
{
    public class ManipuladorArquivo
    {
        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt";

        public static List<Contato> LerArquivo()
        {
            if (File.Exists(@EnderecoArquivo))
            {
                List<Contato> listaContatos = new List<Contato>();

                using (StreamReader sr = File.OpenText(@EnderecoArquivo))
                {
                    while (sr.Peek() >= 0)  // enquanto houver linha
                    {
                        string linha = sr.ReadLine();
                        string[] contato = linha.Split(';');
                        // Se o contato tiver nome, email e telefone
                        if (contato.Count() == 3)
                        {
                            Contato c = new Contato();
                            c.Nome = contato[0];
                            c.Email = contato[1];
                            c.NumeroTelefone = contato[2];
                            listaContatos.Add(c);
                        }
                    }
                }
                return listaContatos;
            }
            else
            {
                throw new Exception("Endereço do arquivo não encontrado.");
            }
        }

        public static void EscreverArquivo(List<Contato> listaContatos)
        {
            using (StreamWriter sw = new StreamWriter(@EnderecoArquivo, false))
            {
                foreach (Contato contato in listaContatos)
                {
                    string c = string.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(c);
                }
                sw.Flush();
            }
        }
    }
}
