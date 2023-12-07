using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaGeek.Classes
{
    internal class Cliente
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string celular { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }       
        public string complemento { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\TDS\\2° Periodo\\PA - Prof.Emerson\\LojaGeek\\LojaGeek.mdf\";Integrated Security=True");

        public List<Cliente> listaCliente()
        {
            List<Cliente> li = new List<Cliente>();
            string sql = "SELECT * FROM Cliente";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente c = new Cliente();
                c.Id = (int)dr["Id"];
                c.nome = dr["nome"].ToString();
                c.cpf = dr["cpf"].ToString();
                c.celular = dr["celular"].ToString();
                c.cep = dr["cep"].ToString();
                c.endereco = dr["endereco"].ToString();
                c.bairro = dr["bairro"].ToString();
                c.cidade = dr["cidade"].ToString();                               
                c.complemento = dr["complemento"].ToString();
                li.Add(c);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string nome, string cpf, string endereco, string bairro, string cidade, string celular, string cep, string complemento)
        {
            string sql = "INSERT INTO Cliente(nome,cpf,endereco,bairro,cidade,celular,cep,complemento) VALUES ('" + nome + "','" + cpf + "','" + endereco + "','" + bairro + "','" + cidade + "','" + celular + "','" + cep + "','" + complemento + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Atualizar(int Id, string nome, string cpf, string endereco, string bairro, string cidade, string celular, string cep, string complemento)
        {
            string sql = "UPDATE Cliente SET nome='" + nome + "',cpf='" + cpf + "',endereco='" + endereco + "',bairro='" + bairro + "',cidade='" + cidade + "',celular='" + celular + "',cep='" + cep + "',complemento='" + complemento + "' WHERE Id='" + Id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Excluir(int Id)
        {
            string sql = "DELETE FROM Cliente WHERE Id='" + Id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Localizar(int Id)
        {
            string sql = "SELECT * FROM Cliente WHERE Id='" + Id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                cpf = dr["cpf"].ToString();
                endereco = dr["endereco"].ToString();
                bairro = dr["bairro"].ToString();
                cidade = dr["cidade"].ToString();
                celular = dr["celular"].ToString();
                cep = dr["cep"].ToString();                
                complemento = dr["complemento"].ToString();
            }
            dr.Close();
            con.Close();
        }

        public bool RegistroRepetido(string nome, string cpf)
        {
            string sql = "SELECT * FROM Cliente WHERE nome='" + nome + "' AND cpf='" + cpf + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }
    }
}
