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
    public class AutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Autores> BuscaAutores(decimal? idAutor = null)
        {
            BindingList<Autores> listaAutores = new BindingList<Autores>();
            using(ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    // Caso não seja passado id do autor, buscar todos
                    if (idAutor == null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioConexao);
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID_AUTOR=@id", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idAutor));
                    }

                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loAutor = new Autores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            listaAutores.Add(loAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar autor(es).");
                }
            }
            return listaAutores;
        }

        public int InsereAutor(Autores novoAutor)
        {
            if (novoAutor == null)
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
                        "INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) " +
                        "VALUES(@id, @nome, @sobrenome, @email)",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", novoAutor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nome", novoAutor.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenome", novoAutor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@email", novoAutor.aut_ds_email));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar novo autor.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaAutor(Autores autorAtualizado)
        {
            if (autorAtualizado == null)
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
                        "UPDATE AUT_AUTORES " +
                        "SET AUT_NM_NOME=@nome, AUT_NM_SOBRENOME=@sobrenome, AUT_DS_EMAIL=@email " +
                        "WHERE AUT_ID_AUTOR=@id",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@nome", autorAtualizado.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenome", autorAtualizado.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@email", autorAtualizado.aut_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@id", autorAtualizado.aut_id_autor));
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

        public int RemoveAutor(Autores autorRemovido)
        {
            if (autorRemovido == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR=@id", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", autorRemovido.aut_id_autor));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover autor.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}