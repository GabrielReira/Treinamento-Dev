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
    public class LivrosDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<Livros> BuscaLivros(decimal? idLivro = null)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Caso não seja passado id do livro, buscar todos
                    if (idLivro == null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS;", ioConexao);
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO=@id;", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idLivro));
                    }
                    // Leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.GetDecimal(1),
                                loReader.GetDecimal(2),
                                loReader.GetString(3),
                                loReader.GetDecimal(4),
                                loReader.GetDecimal(5),
                                loReader.GetString(6),
                                loReader.GetInt32(7)
                            );
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livros(s).");
                }
            }
            return listaLivros;
        }

        // Outro método para buscar Livros, mas ao invés de guardar apenas os IDs das FK, também guarda seu valor
        public BindingList<Livros> BuscaLivrosCustom(decimal? idLivro = null)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if ((idLivro == null))
                    {
                        ioQuery = new SqlCommand("SELECT LIV_ID_LIVRO, LIV_NM_TITULO, " +
                                                 "AUT_ID_AUTOR, AUT_NM_NOME, " +
                                                 "TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO, " +
                                                 "EDI_ID_EDITOR, EDI_NM_EDITOR, " +
                                                 "LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO " +
                                                 "FROM LIV_LIVROS " +
                                                    "INNER JOIN TIL_TIPO_LIVRO ON LIV_ID_TIPO_LIVRO = TIL_ID_TIPO_LIVRO " +
                                                    "INNER JOIN EDI_EDITORES ON LIV_ID_EDITOR = EDI_ID_EDITOR " +
                                                    "INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO " +
                                                    "INNER JOIN AUT_AUTORES ON LIA_ID_AUTOR = AUT_ID_AUTOR ",
                        ioConexao);
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT LIV_ID_LIVRO, LIV_NM_TITULO, " +
                                                 "AUT_ID_AUTOR, AUT_NM_NOME, " +
                                                 "TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO, " +
                                                 "EDI_ID_EDITOR, EDI_NM_EDITOR, " +
                                                 "LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO " +
                                                 "FROM LIV_LIVROS " +
                                                    "INNER JOIN TIL_TIPO_LIVRO ON LIV_ID_TIPO_LIVRO = TIL_ID_TIPO_LIVRO " +
                                                    "INNER JOIN EDI_EDITORES ON LIV_ID_EDITOR = EDI_ID_EDITOR " +
                                                    "INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO " +
                                                    "INNER JOIN AUT_AUTORES ON LIA_ID_AUTOR = AUT_ID_AUTOR " +
                                                 "WHERE LIV_ID_LIVRO = @id;",
                        ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@id", idLivro));
                    }
                    // Leitura de dados - Nessa consulta, os valores podem vir nulos ou não
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.IsDBNull(1) ? String.Empty : loReader.GetString(1),
                                loReader.IsDBNull(2) ? (decimal?)null : loReader.GetDecimal(2),
                                loReader.IsDBNull(3) ? String.Empty : loReader.GetString(3),
                                loReader.IsDBNull(4) ? (decimal?)null : loReader.GetDecimal(4),
                                loReader.IsDBNull(5) ? String.Empty : loReader.GetString(5),
                                loReader.IsDBNull(6) ? (decimal?)null : loReader.GetDecimal(6),
                                loReader.IsDBNull(7) ? String.Empty : loReader.GetString(7),
                                loReader.IsDBNull(8) ? (decimal?)null : loReader.GetDecimal(8),
                                loReader.IsDBNull(9) ? (decimal?)null : loReader.GetDecimal(9),
                                loReader.IsDBNull(10) ? String.Empty : loReader.GetString(10),
                                loReader.IsDBNull(11) ? (int?)null : loReader.GetInt32(11)
                            );
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }

        public int InsereLivro(Livros novoLivro)
        {
            if (novoLivro == null)
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
                        "INSERT INTO LIV_LIVROS VALUES" +
                        "(@id_livro, @id_tipo_livro, @id_editor, @titulo, @preco, @royalty, @resumo, @nu_edicao);",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id_livro", novoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@id_tipo_livro", novoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@id_editor", novoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@titulo", novoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@preco", novoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", novoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", novoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@nu_edicao", novoLivro.liv_nu_edicao));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar novo livro.");
                }
            }
            return qtdRegistrosInseridos;
        }

        public int AtualizaLivro(Livros livroAtualizado)
        {
            if (livroAtualizado == null)
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
                        "UPDATE LIV_LIVROS "+
                        "SET LIV_ID_TIPO_LIVRO=@id_tipo_livro, LIV_ID_EDITOR=@id_editor, LIV_NM_TITULO=@titulo, LIV_VL_PRECO=@preco, LIV_DS_RESUMO=@resumo, LIV_NU_EDICAO=@nu_edicao " +
                        "WHERE LIV_ID_LIVRO = @id_livro;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id_tipo_livro", livroAtualizado.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@id_editor", livroAtualizado.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@titulo", livroAtualizado.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@preco", livroAtualizado.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", livroAtualizado.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", livroAtualizado.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@nu_edicao", livroAtualizado.liv_nu_edicao));
                    ioQuery.Parameters.Add(new SqlParameter("@id_livro", livroAtualizado.liv_id_livro));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosAlterados = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar livro.");
                }
            }
            return qtdRegistrosAlterados;
        }

        public int RemoveLivro(Livros livroRemovido)
        {
            if (livroRemovido == null)
            {
                throw new NullReferenceException();
            }

            int qtdRegistrosRemovidos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO=@id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", livroRemovido.liv_id_livro));
                    // Executa o comando Transact-SQL que retorna a qtd de linhas afetadas
                    qtdRegistrosRemovidos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar remover livro.");
                }
            }
            return qtdRegistrosRemovidos;
        }
        
        public BindingList<Livros> BuscarLivrosPorAutor(Autores autor)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todos os livros do autor
                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS " +
                                             "INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO " +
                                             "INNER JOIN AUT_AUTORES ON LIA_ID_AUTOR = AUT_ID_AUTOR " +
                                             "WHERE AUT_ID_AUTOR = @id", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", autor.aut_id_autor));

                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }

        public BindingList<Livros> BuscaLivrosPorAutorCustom(Autores autor)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todos os livros do autor especificado
                    ioQuery = new SqlCommand("SELECT LIV_ID_LIVRO, LIV_NM_TITULO, " +
                                                 "AUT_ID_AUTOR, AUT_NM_NOME, " +
                                                 "TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO, " +
                                                 "EDI_ID_EDITOR, EDI_NM_EDITOR, " +
                                                 "LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO " +
                                                 "FROM LIV_LIVROS " +
                                                    "INNER JOIN TIL_TIPO_LIVRO ON LIV_ID_TIPO_LIVRO = TIL_ID_TIPO_LIVRO " +
                                                    "INNER JOIN EDI_EDITORES ON LIV_ID_EDITOR = EDI_ID_EDITOR " +
                                                    "INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO " +
                                                    "INNER JOIN AUT_AUTORES ON LIA_ID_AUTOR = AUT_ID_AUTOR " +
                                                "WHERE AUT_ID_AUTOR=@id;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", autor.aut_id_autor));

                    // Bloco de leitura de dados
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.IsDBNull(1) ? String.Empty : loReader.GetString(1),
                                loReader.IsDBNull(2) ? (decimal?)null : loReader.GetDecimal(2),
                                loReader.IsDBNull(3) ? String.Empty : loReader.GetString(3),
                                loReader.IsDBNull(4) ? (decimal?)null : loReader.GetDecimal(4),
                                loReader.IsDBNull(5) ? String.Empty : loReader.GetString(5),
                                loReader.IsDBNull(6) ? (decimal?)null : loReader.GetDecimal(6),
                                loReader.IsDBNull(7) ? String.Empty : loReader.GetString(7),
                                loReader.IsDBNull(8) ? (decimal?)null : loReader.GetDecimal(8),
                                loReader.IsDBNull(9) ? (decimal?)null : loReader.GetDecimal(9),
                                loReader.IsDBNull(10) ? String.Empty : loReader.GetString(10),
                                loReader.IsDBNull(11) ? (int?)null : loReader.GetInt32(11)
                            );
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }

        public BindingList<Livros> BuscarLivrosPorCategoria(TipoLivro categoria)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todos os livros da categoria
                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS " +
                                                "INNER JOIN TIL_TIPO_LIVRO ON LIV_ID_TIPO_LIVRO = TIL_ID_TIPO_LIVRO " +
                                             "WHERE LIV_ID_TIPO_LIVRO = @id;", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", categoria.til_id_tipo_livro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }

        public BindingList<Livros> BuscarLivrosPorCategoriaCustom(TipoLivro categoria)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todos os livros da categoria
                    ioQuery = new SqlCommand("SELECT LIV_ID_LIVRO, LIV_NM_TITULO, " +
                                                 "AUT_ID_AUTOR, AUT_NM_NOME, " +
                                                 "TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO, " +
                                                 "EDI_ID_EDITOR, EDI_NM_EDITOR, " +
                                                 "LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO " +
                                                 "FROM LIV_LIVROS " +
                                                    "INNER JOIN TIL_TIPO_LIVRO ON LIV_ID_TIPO_LIVRO = TIL_ID_TIPO_LIVRO " +
                                                    "INNER JOIN EDI_EDITORES ON LIV_ID_EDITOR = EDI_ID_EDITOR " +
                                                    "INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO " +
                                                    "INNER JOIN AUT_AUTORES ON LIA_ID_AUTOR = AUT_ID_AUTOR " +
                                                "WHERE LIV_ID_TIPO_LIVRO = @id;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", categoria.til_id_tipo_livro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.IsDBNull(1) ? String.Empty : loReader.GetString(1),
                                loReader.IsDBNull(2) ? (decimal?)null : loReader.GetDecimal(2),
                                loReader.IsDBNull(3) ? String.Empty : loReader.GetString(3),
                                loReader.IsDBNull(4) ? (decimal?)null : loReader.GetDecimal(4),
                                loReader.IsDBNull(5) ? String.Empty : loReader.GetString(5),
                                loReader.IsDBNull(6) ? (decimal?)null : loReader.GetDecimal(6),
                                loReader.IsDBNull(7) ? String.Empty : loReader.GetString(7),
                                loReader.IsDBNull(8) ? (decimal?)null : loReader.GetDecimal(8),
                                loReader.IsDBNull(9) ? (decimal?)null : loReader.GetDecimal(9),
                                loReader.IsDBNull(10) ? String.Empty : loReader.GetString(10),
                                loReader.IsDBNull(11) ? (int?)null : loReader.GetInt32(11)
                            );
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }

        public BindingList<Livros> BuscarLivrosPorEditor(Editores editor)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todos os livros do editor
                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS " +
                                             "INNER JOIN EDI_EDITORES ON LIV_ID_EDITOR = EDI_ID_EDITOR " +
                                             "WHERE LIV_ID_EDITOR = @id; ", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", editor.edi_id_editor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }

        public BindingList<Livros> BuscarLivrosPorEditorCustom(Editores editor)
        {
            BindingList<Livros> listaLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // Buscar todos os livros do editor especificado
                    ioQuery = new SqlCommand("SELECT LIV_ID_LIVRO, LIV_NM_TITULO, " +
                                                 "AUT_ID_AUTOR, AUT_NM_NOME, " +
                                                 "TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO, " +
                                                 "EDI_ID_EDITOR, EDI_NM_EDITOR, " +
                                                 "LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO " +
                                                 "FROM LIV_LIVROS " +
                                                    "INNER JOIN TIL_TIPO_LIVRO ON LIV_ID_TIPO_LIVRO = TIL_ID_TIPO_LIVRO " +
                                                    "INNER JOIN EDI_EDITORES ON LIV_ID_EDITOR = EDI_ID_EDITOR " +
                                                    "INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO " +
                                                    "INNER JOIN AUT_AUTORES ON LIA_ID_AUTOR = AUT_ID_AUTOR " +
                                                "WHERE LIV_ID_EDITOR = @id;",
                    ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@id", editor.edi_id_editor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loLivro = new Livros(
                                loReader.GetDecimal(0),
                                loReader.IsDBNull(1) ? String.Empty : loReader.GetString(1),
                                loReader.IsDBNull(2) ? (decimal?)null : loReader.GetDecimal(2),
                                loReader.IsDBNull(3) ? String.Empty : loReader.GetString(3),
                                loReader.IsDBNull(4) ? (decimal?)null : loReader.GetDecimal(4),
                                loReader.IsDBNull(5) ? String.Empty : loReader.GetString(5),
                                loReader.IsDBNull(6) ? (decimal?)null : loReader.GetDecimal(6),
                                loReader.IsDBNull(7) ? String.Empty : loReader.GetString(7),
                                loReader.IsDBNull(8) ? (decimal?)null : loReader.GetDecimal(8),
                                loReader.IsDBNull(9) ? (decimal?)null : loReader.GetDecimal(9),
                                loReader.IsDBNull(10) ? String.Empty : loReader.GetString(10),
                                loReader.IsDBNull(11) ? (int?)null : loReader.GetInt32(11)
                            );
                            listaLivros.Add(loLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar livro(s).");
                }
            }
            return listaLivros;
        }
    }
}
