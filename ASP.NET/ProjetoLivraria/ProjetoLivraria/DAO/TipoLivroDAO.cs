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
    public class TipoLivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<TipoLivro> BuscaCategorias(decimal? idCategoria = null)
        {
            BindingList<TipoLivro> listaCategorias = new BindingList<TipoLivro>();
            using(ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Caso não seja passado id da categoria (tipolivro), buscar todas
                    if (idCategoria == null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO;", ioConexao);
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO=@id;", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idCategoria));
                    }

                    // Lendo os dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            TipoLivro loCategoria = new TipoLivro(loReader.GetDecimal(0), loReader.GetString(1));
                            listaCategorias.Add(loCategoria);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar categoria(s).");
                }
            }
            return listaCategorias;
        }

        public int InsereCategoria(TipoLivro novaCategoria)
        {
            if(novaCategoria == null)
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
                        "INSERT INTO TIL_TIPO_LIVRO(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO)" +
                        "VALUES(@id, @descricao);",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", novaCategoria.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricao", novaCategoria.til_ds_descricao));
                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar nova categoria.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaCategoria(TipoLivro categoriaAtualizada)
        {
            if (categoriaAtualizada == null)
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
                        "UPDATE TIL_TIPO_LIVRO " +
                        "SET TIL_DS_DESCRICAO = @descricao " +
                        "WHERE TIL_ID_TIPO_LIVRO = @id;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@descricao", categoriaAtualizada.til_ds_descricao));
                    ioQuery.Parameters.Add(new SqlParameter("@id", categoriaAtualizada.til_id_tipo_livro));
                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar categoria.");
                }
            }
            return qtdRegistrosAlterados;
        }

        public int RemoveCategoria(TipoLivro categoriaRemovida)
        {
            if (categoriaRemovida == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO=@id", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", categoriaRemovida.til_id_tipo_livro));
                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover categoria.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}
