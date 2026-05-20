using AppLogin.Models;
using AppLogin.Models.Constant;
using AppLogin.Repository.Contract;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System.Data;
using X.PagedList;

namespace AppLogin.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;

        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public Cliente Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from cliente where Email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Nome = Convert.ToString(dr["Nome"]);
                    cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);

                    cliente.Sexo = Convert.ToString(dr["Sexo"]);
                    cliente.CPF = Convert.ToString(dr["CPF"]);
                    cliente.Telefone = Convert.ToString(dr["Telefone"]);
                    cliente.Situacao = Convert.ToString(dr["Situacao"]);

                    cliente.Email = Convert.ToString(dr["Email"]);
                    cliente.Senha = Convert.ToString(dr["Senha"]);
                }

                return cliente;
            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> cliList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM CLIENTE", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add(
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Telefone = Convert.ToString(dr["Telefone"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"]),
                            Situacao = Convert.ToString(dr["Situacao"])
                        });
                }

                return cliList;
            }
        }

        public void Cadastrar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativado;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "insert into Cliente(Nome, Nascimento, Sexo, CPF, Telefone, Email, Senha, Situacao) " +
                    "values (@Nome, @Nascimento, @Sexo, @CPF, @Telefone, @Email, @Senha, @Situacao)",
                    conexao
                ); // @: PARAMETRO

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@Nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Atualizar(Cliente cliente)
        {
            string Situacao = SituacaoConstant.Ativado;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "update Cliente set Nome=@Nome, Nascimento=@Nascimento, Sexo=@Sexo, CPF=@CPF, " +
                    "Telefone=@Telefone, Email=@Email, Senha=@Senha, Situacao=@Situacao WHERE Id=@Id",
                    conexao);

                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cliente.Id;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cliente.CPF;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cliente.Senha;
                cmd.Parameters.Add("@situacao", MySqlDbType.VarChar).Value = Situacao;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "Delete from Cliente WHERE Id=@Id",
                    conexao);

                cmd.Parameters.AddWithValue("@Id", Id);

                int i = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM Cliente WHERE Id = @Id",
                    conexao
                );

                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataReader dr;

                Cliente cliente = new Cliente();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Nome = Convert.ToString(dr["Nome"]);
                    cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);
                    cliente.Sexo = Convert.ToString(dr["Sexo"]);
                    cliente.CPF = Convert.ToString(dr["CPF"]);
                    cliente.Telefone = Convert.ToString(dr["Telefone"]);
                    cliente.Email = Convert.ToString(dr["Email"]);
                    cliente.Senha = Convert.ToString(dr["Senha"]);
                    cliente.Situacao = Convert.ToString(dr["Situacao"]);
                }

                return cliente;
            }
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            throw new NotImplementedException();
        }
    }
}
