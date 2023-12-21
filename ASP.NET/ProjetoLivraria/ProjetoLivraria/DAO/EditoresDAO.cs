using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class EditoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Editores> BuscaEditores(decimal? idEditor = null)
        {
            BindingList<Editores> listaEditores = new BindingList<Editores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Caso não seja passado id do editor, buscar todos
                    if (idEditor == null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES;", ioConexao);
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES WHERE EDI_ID_EDITOR=@id; ", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idEditor));
                    }
                    // Leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Editores loEditor = new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            listaEditores.Add(loEditor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar editor(es).");
                }
            }
            return listaEditores;
        }

        public int InsereEditor(Editores novoEditor)
        {
            if (novoEditor == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "INSERT INTO EDI_EDITORES VALUES(@id, @nome, @email, @url); ",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", novoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nome", novoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@email", novoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@url", novoEditor.edi_ds_url));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar novo editor.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaEditor(Editores editorAtualizado)
        {
            if (editorAtualizado == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosAlterados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "UPDATE EDI_EDITORES " +
                        "SET EDI_NM_EDITOR = @nome, EDI_DS_EMAIL = @email, EDI_DS_URL = @url " +
                        "WHERE EDI_ID_EDITOR = @id;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@nome", editorAtualizado.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@email", editorAtualizado.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@url", editorAtualizado.edi_ds_url));
                    ioQuery.Parameters.Add(new SqlParameter("@id", editorAtualizado.edi_id_editor));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar autor.");
                }
            }
            return qtdRegistrosAlterados;
        }

        public int RemoveEditor(Editores editorRemovido)
        {
            if (editorRemovido == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR=@id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", editorRemovido.edi_id_editor));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover editor.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}