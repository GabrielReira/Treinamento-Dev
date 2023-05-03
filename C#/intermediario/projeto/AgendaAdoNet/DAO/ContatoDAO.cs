using AgendaAdoNet.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAdoNet.DAO
{
    public class ContatoDAO
    {
        public DataTable GetContatos()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM CONTATOS";

            // Utilizando DataTable
            DbDataReader reader = DAOUtils.GetDataReader(comando);
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;

            // Utilizando DataSet
            /* DbDataAdapter adapter = new SqlDataAdapter((SqlCommand)comando);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "CONTATOS");
            return ds; */
        }

        public void Excluir(int id)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "DELETE FROM CONTATOS WHERE ID = @id";
            // Evitar SQL Injection
            comando.Parameters.Add(DAOUtils.GetParametro("@id", id));
            comando.ExecuteNonQuery();
        }

        public void Adicionar(Contato contato)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "INSERT INTO CONTATOS (NOME, EMAIL, TELEFONE) VALUES (@nome, @email, @telefone)";
            // Evitar SQL Injection
            comando.Parameters.Add(DAOUtils.GetParametro("@nome", contato.Nome));
            comando.Parameters.Add(DAOUtils.GetParametro("@email", contato.Email));
            comando.Parameters.Add(DAOUtils.GetParametro("@telefone", contato.Telefone));

            comando.ExecuteNonQuery();
        }

        public void Alterar(Contato contato)
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE CONTATOS SET NOME=@nome, EMAIL=@email, TELEFONE=@telefone WHERE ID=@id";
            // Evitar SQL Injection
            comando.Parameters.Add(DAOUtils.GetParametro("@nome", contato.Nome));
            comando.Parameters.Add(DAOUtils.GetParametro("@email", contato.Email));
            comando.Parameters.Add(DAOUtils.GetParametro("@telefone", contato.Telefone));
            comando.Parameters.Add(DAOUtils.GetParametro("@id", contato.Id));

            comando.ExecuteNonQuery();
        }

        public int QtdContatos()
        {
            DbConnection conexao = DAOUtils.GetConexao();
            DbCommand comando = DAOUtils.GetComando(conexao);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT COUNT(*) FROM CONTATOS";
            return (int)comando.ExecuteScalar();
        }
    }
}
