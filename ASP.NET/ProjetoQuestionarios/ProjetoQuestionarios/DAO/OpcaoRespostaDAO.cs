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
    public class OpcaoRespostaDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<OpcaoResposta> BuscaOpcaoRespostas(decimal? idOpcaoResposta = null)
        {
            BindingList<OpcaoResposta> listaOpcaoRespostas = new BindingList<OpcaoResposta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Caso não seja passado id da opção resposta, buscar todas
                    if (idOpcaoResposta == null)
                        ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_gmoreira;", ioConexao);
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_gmoreira WHERE opr_id_opcao_resposta=@id;", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idOpcaoResposta));
                    }
                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            OpcaoResposta loOpcaoResposta = new OpcaoResposta(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetString(2), loReader.GetString(3)[0], loReader.GetInt32(4));
                            listaOpcaoRespostas.Add(loOpcaoResposta);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar opção(ões) de resposta.");
                }
            }
            return listaOpcaoRespostas;
        }

        public BindingList<OpcaoResposta> BuscaOpcoesRespostaPorPergunta(Pergunta pergunta)
        {
            BindingList<OpcaoResposta> listaOpcaoRespostas = new BindingList<OpcaoResposta>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todas as opções de resposta da pergunta
                    ioQuery = new SqlCommand("SELECT * FROM OPR_OPCAO_RESPOSTA_gmoreira WHERE opr_id_pergunta = @id; ", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", pergunta.per_id_pergunta));

                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            OpcaoResposta loOpcaoResposta = new OpcaoResposta(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetString(2), loReader.GetString(3)[0], loReader.GetInt32(4));
                            listaOpcaoRespostas.Add(loOpcaoResposta);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar opção(ões) de resposta.");
                }
            }
            return listaOpcaoRespostas;
        }

        public int InsereOpcaoResposta(OpcaoResposta novaOpcaoResposta)
        {
            if (novaOpcaoResposta == null)
                throw new NullReferenceException();

            int qtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "INSERT INTO OPR_OPCAO_RESPOSTA_gmoreira(opr_id_opcao_resposta, opr_id_pergunta, opr_ds_opcao_resposta, opr_ch_resposta_correta, opr_nu_ordem) " +
                        "VALUES(@idOpcaoResposta, @idPergunta, @descricao, @resposta, @ordem);",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idOpcaoResposta", novaOpcaoResposta.opr_id_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", novaOpcaoResposta.opr_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@descricao", novaOpcaoResposta.opr_ds_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@resposta", novaOpcaoResposta.opr_ch_resposta_correta));
                    ioQuery.Parameters.Add(new SqlParameter("@ordem", novaOpcaoResposta.opr_nu_ordem));

                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar opção de resposta.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaOpcaoResposta(OpcaoResposta opcaoRespostaAtualizada)
        {
            if (opcaoRespostaAtualizada == null)
                throw new NullReferenceException();

            int qtdRegistrosAtualizados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                        "UPDATE OPR_OPCAO_RESPOSTA_gmoreira " +
                        "SET opr_id_pergunta = @idPergunta, opr_ds_opcao_resposta = @descricao, opr_ch_resposta_correta = @resposta, opr_nu_ordem = @ordem " +
                        "WHERE opr_id_opcao_resposta = @idOpcaoResposta;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idOpcaoResposta", opcaoRespostaAtualizada.opr_id_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@idPergunta", opcaoRespostaAtualizada.opr_id_pergunta));
                    ioQuery.Parameters.Add(new SqlParameter("@descricao", opcaoRespostaAtualizada.opr_ds_opcao_resposta));
                    ioQuery.Parameters.Add(new SqlParameter("@resposta", opcaoRespostaAtualizada.opr_ch_resposta_correta));
                    ioQuery.Parameters.Add(new SqlParameter("@ordem", opcaoRespostaAtualizada.opr_nu_ordem));

                    qtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar opção de resposta.");
                }
            }
            return qtdRegistrosAtualizados;
        }

        public int RemoveOpcaoResposta (OpcaoResposta opcaoRespostaRemovida)
        {
            if (opcaoRespostaRemovida == null)
                throw new NullReferenceException();

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM OPR_OPCAO_RESPOSTA_gmoreira WHERE opr_id_opcao_resposta=@id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", opcaoRespostaRemovida.opr_id_opcao_resposta));

                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover opção de resposta.");
                }
            }
            return qtdRegistrosRemovidos;
        }
    }
}