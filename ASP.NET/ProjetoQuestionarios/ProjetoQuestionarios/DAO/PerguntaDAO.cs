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
    public class PerguntaDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Pergunta> BuscaPerguntas(decimal? idPergunta = null)
        {
            BindingList<Pergunta> listaPerguntas = new BindingList<Pergunta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Caso não seja passado id da pergunta, buscar todas
                    if (idPergunta == null)
                        ioQuery = new SqlCommand("SELECT * FROM PER_PERGUNTA_gmoreira;", ioConexao);
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM PER_PERGUNTA_gmoreira WHERE per_id_pergunta=@id", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idPergunta));
                    }
                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Pergunta loPergunta = new Pergunta(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetString(2), loReader.GetString(3)[0], loReader.GetString(4)[0], loReader.GetInt32(5));
                            listaPerguntas.Add(loPergunta);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar pergunta(s).");
                }
            }
            return listaPerguntas;
        }        

        public BindingList<Pergunta> BuscaPerguntasPorQuestionario(Questionario questionario)
        {
            BindingList<Pergunta> listaPerguntas = new BindingList<Pergunta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todas as perguntas daquele questionário
                    ioQuery = new SqlCommand("SELECT * FROM PER_PERGUNTA_gmoreira WHERE per_id_questionario = @id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", questionario.qst_id_questionario));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Pergunta loPergunta = new Pergunta(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetString(2), loReader.GetString(3)[0], loReader.GetString(4)[0], loReader.GetInt32(5));
                            listaPerguntas.Add(loPergunta);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar pergunta(s).");
                }
            }
            return listaPerguntas;
        }

        public BindingList<Pergunta> BuscaPerguntaPorOrdem(Pergunta pergunta)
        {
            BindingList<Pergunta> listaPerguntas = new BindingList<Pergunta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todas as perguntas daquele questionário
                    ioQuery = new SqlCommand(
                        "SELECT * FROM PER_PERGUNTA_gmoreira " +
                        "INNER JOIN QST_QUESTIONARIO_gmoreira ON per_id_questionario = qst_id_questionario " +
                        "WHERE per_nu_ordem = @ordem AND per_id_questionario=@idQuestionario;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@ordem", pergunta.per_nu_ordem));
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", pergunta.per_id_questionario));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Pergunta loPergunta = new Pergunta(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetString(2), loReader.GetString(3)[0], loReader.GetString(4)[0], loReader.GetInt32(5));
                            listaPerguntas.Add(loPergunta);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar pergunta(s).");
                }
            }
            return listaPerguntas;
        }

        public int InserePergunta(Pergunta novaPergunta)
        {
            if (novaPergunta == null)
                throw new NullReferenceException();

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "INSERT INTO PER_PERGUNTA_gmoreira(per_id_pergunta, per_id_questionario, per_ds_pergunta, per_tp_pergunta, per_ch_resposta_obrigatoria, per_nu_ordem) " +
                        "VALUES(@idPergunta, @idQuestionario, @pergunta, @tipo, @resposta, @ordem); ",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", novaPergunta.per_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", novaPergunta.per_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@pergunta", novaPergunta.per_ds_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@tipo", novaPergunta.per_tp_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@resposta", novaPergunta.per_ch_resposta_obrigatoria));
                    ioQuery.Parameters.Add(new SqlParameter("@ordem", novaPergunta.per_nu_ordem));

                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar pergunta.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaPergunta(Pergunta perguntaAtualizada)
        {
            if (perguntaAtualizada == null)
                throw new NullReferenceException();

            int qtdRegistrosAtualizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "UPDATE PER_PERGUNTA_gmoreira " +
                        "SET per_id_questionario = @idQuestionario, per_ds_pergunta = @pergunta, per_tp_pergunta = @tipo, per_ch_resposta_obrigatoria = @resposta, per_nu_ordem = @ordem " +
                        "WHERE per_id_pergunta = @idPergunta; ",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", perguntaAtualizada.per_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@idQuestionario", perguntaAtualizada.per_id_questionario));
                    ioQuery.Parameters.Add(new SqlParameter("@pergunta", perguntaAtualizada.per_ds_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@tipo", perguntaAtualizada.per_tp_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@resposta", perguntaAtualizada.per_ch_resposta_obrigatoria));
                    ioQuery.Parameters.Add(new SqlParameter("@ordem", perguntaAtualizada.per_nu_ordem));

                    qtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar pergunta.");
                }
            }
            return qtdRegistrosAtualizados;
        }

        public int RemovePergunta(Pergunta perguntaRemovida)
        {
            if (perguntaRemovida == null)
                throw new NullReferenceException();

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM PER_PERGUNTA_gmoreira WHERE per_id_pergunta=@id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", perguntaRemovida.per_id_pergunta));

                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover pergunta.");
                }
            }
            return qtdRegistrosRemovidos;
        }    
    }
}