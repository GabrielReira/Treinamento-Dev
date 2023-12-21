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
    public class LivroAutorDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<LivroAutor> BuscaLivroAutor(decimal? idLivro = null)
        {
            BindingList<LivroAutor> listaLivroAutor = new BindingList<LivroAutor>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();

                    if (idLivro == null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR;", ioConexao);
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR WHERE LIA_ID_LIVRO = @id;", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idLivro));
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            LivroAutor loLivroAutor = new LivroAutor(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2));
                            listaLivroAutor.Add(loLivroAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro-autor.");
                }
            }
            return listaLivroAutor;
        }

        public int InsereLivroAutor(LivroAutor novoLivroAutor)
        {
            if (novoLivroAutor == null)
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
                        "INSERT INTO LIA_LIVRO_AUTOR " +
                        "VALUES(@id_autor, @id_livro, @pc_royalty);",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id_autor", novoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@id_livro", novoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@pc_royalty", novoLivroAutor.lia_pc_royalty));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar novo livro-autor.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaLivroAutor(LivroAutor livroAutorAtualizado)
        {
            if (livroAutorAtualizado == null)
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
                        "UPDATE LIA_LIVRO_AUTOR " +
                        "SET LIA_ID_AUTOR = @id_autor, LIA_ID_LIVRO = @id_livro, LIA_PC_ROYALTY = @pc_royalty " +
                        "WHERE LIA_ID_LIVRO = @id_livro;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id_autor", livroAutorAtualizado.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@pc_royalty", livroAutorAtualizado.lia_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@id_livro", livroAutorAtualizado.lia_id_livro));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar livro-autor.");
                }
            }
            return qtdRegistrosAlterados;
        }

        public int RemoveLivroAutor(LivroAutor livroAutorRemovido)
        {
            if (livroAutorRemovido == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIA_LIVRO_AUTOR WHERE LIA_ID_LIVRO=@id_livro;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id_livro", livroAutorRemovido.lia_id_livro));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover livro-autor.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}