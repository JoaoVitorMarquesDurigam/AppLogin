using AppLogin.Models;
using AppLogin.Models.Constant;
using AppLogin.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;
using X.PagedList;

namespace AppLogin.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly string _conexaoMySQL;
        public ColaboradorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public Colaborador Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM Colaborador WHERE Email = @Email AND Senha = @Senha",
                    conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataReader dr;

                Colaborador colaborador = new Colaborador();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    colaborador.Id = (Int32)dr["id"];
                    colaborador.Nome = (string)dr["Nome"];
                    colaborador.Email = (string)dr["Email"];
                    colaborador.Senha = (string)dr["Senha"];
                    colaborador.Tipo = (string)dr["Tipo"];
                }
                conexao.Close();

                return colaborador;
            }
        }

        public void Cadastrar(Colaborador colaborador)
        {
            string comum = ColaboradorTipoConstant.Comum;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Colaborador(Nome, CPF, Telefone, Email, Senha, Tipo) " +
                    "VALUES (@Nome, @CPF, @Telefone, @Email, @Senha, @Tipo)",
                    conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = colaborador.Nome;
                cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = colaborador.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = colaborador.Telefone;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = colaborador.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = colaborador.Senha;
                cmd.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = colaborador.Tipo;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Atualizar(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Colaborador ObterColaborador(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> ObterTodosColaboradores()
        {
            throw new NotImplementedException();
        }

        public IPagedList<Colaborador> ObterTodosColaboradores(int? pagina)
        {
            throw new NotImplementedException();
        }
    }
}
