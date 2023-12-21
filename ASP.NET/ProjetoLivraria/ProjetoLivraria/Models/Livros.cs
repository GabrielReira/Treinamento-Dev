using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livros
    {
        public decimal liv_id_livro { get; set; }
        public decimal? liv_id_tipo_livro { get; set; }
        public decimal? liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal? liv_vl_preco { get; set; }
        public decimal? liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int? liv_nu_edicao { get; set; }

        public Livros(decimal idLivro, decimal idTipoLivro, decimal idEditor, string tituloLivro, decimal precoLivro, decimal royaltyLivro, string resumoLivro, int edicaoLivro)
        {
            this.liv_id_livro = idLivro;
            this.liv_id_tipo_livro = idTipoLivro;
            this.liv_id_editor = idEditor;
            this.liv_nm_titulo = tituloLivro;
            this.liv_vl_preco = precoLivro;
            this.liv_pc_royalty = royaltyLivro;
            this.liv_ds_resumo = resumoLivro;
            this.liv_nu_edicao = edicaoLivro;
        }

        // Construtor que recebe os valores das FKs de autor, categoria e editor
        public decimal? aut_id_autor { get; set; }
        public string aut_nm_nome { get; set; }
        public decimal? til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }
        public decimal? edi_id_editor { get; set; }
        public string edi_nm_editor { get; set; }

        public Livros(
            decimal idLivro, string tituloLivro,
            decimal? idAutor, string nomeAutor,
            decimal? idCategoria, string nomeCategoria,
            decimal? idEditor, string nomeEditor,
            decimal? precoLivro, decimal? royaltyLivro, string resumoLivro, int? nuEdicaoLivro
        )
        {
            this.liv_id_livro = idLivro;
            this.liv_nm_titulo = tituloLivro;
            this.aut_id_autor = idAutor;
            this.aut_nm_nome = nomeAutor;
            this.til_id_tipo_livro = idCategoria;
            this.til_ds_descricao = nomeCategoria;
            this.edi_id_editor = idEditor;
            this.edi_nm_editor = nomeEditor;
            this.liv_vl_preco = precoLivro;
            this.liv_pc_royalty = royaltyLivro;
            this.liv_ds_resumo = resumoLivro;
            this.liv_nu_edicao = nuEdicaoLivro;
        }
    }
}