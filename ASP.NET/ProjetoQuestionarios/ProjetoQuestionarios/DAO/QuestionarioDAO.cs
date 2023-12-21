using ProjetoQuestionarios.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoQuestionarios.DAO
{
    public class QuestionarioDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Questionario> BuscaQuestionarios(decimal? idQuestionario = null)
        {
            BindingList<Questionario> listaQuestionarios = new BindingList<Questionario>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Caso não seja passado id do questionário, buscar todos
                    if (idQuestionario == null)
                        ioQuery = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_gmoreira;", ioConexao);
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM QST_QUESTIONARIO_gmoreira WHERE qst_id_questionario=@id;", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idQuestionario));
                    }
                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Questionario loQuestionario = new Questionario(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2)[0], loReader.GetString(3));
                            listaQuestionarios.Add(loQuestionario);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar questionário(s).");
                }
            }
            return listaQuestionarios;
        }

        public int InsereQuestionario(Questionario novoQuestionario)
        {
            if (novoQuestionario == null)
                throw new NullReferenceException();

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "INSERT INTO QST_QUESTIONARIO_gmoreira(qst_id_questionario, qst_nm_questionario, qst_tp_questionario, qst_ds_link_instrucoes) " +
                        "VALUES(@id, @nome, @tipo, @link); ",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", novoQuestionario.qst_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@nome", novoQuestionario.qst_nm_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@tipo", novoQuestionario.qst_tp_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@link", novoQuestionario.qst_ds_link_instrucoes));

                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar questionário.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaQuestionario(Questionario questionarioAtualizado)
        {
            if (questionarioAtualizado == null)
                throw new NullReferenceException();

            int qtdRegistrosAtualizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "UPDATE QST_QUESTIONARIO_gmoreira " +
                        "SET qst_nm_questionario = @nome, qst_tp_questionario = @tipo, qst_ds_link_instrucoes = @link " +
                        "WHERE qst_id_questionario = @id;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", questionarioAtualizado.qst_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@nome", questionarioAtualizado.qst_nm_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@tipo", questionarioAtualizado.qst_tp_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@link", questionarioAtualizado.qst_ds_link_instrucoes));

                    qtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar questionário.");
                }
            }
            return qtdRegistrosAtualizados;
        }

        public int RemoveQuestionario(Questionario questionarioRemovido)
        {
            if (questionarioRemovido == null)
                throw new NullReferenceException();

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM QST_QUESTIONARIO_gmoreira WHERE qst_id_questionario=@id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", questionarioRemovido.qst_id_questionario));

                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover questionário.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}