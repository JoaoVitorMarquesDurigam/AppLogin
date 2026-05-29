using AppLogin.Models;
using Newtonsoft.Json;

namespace AppLogin.Libraries.Login
{
    public class LoginColaborador
    {
        private string key = "Login.Colaborador";
        private Sessao.Sessao _sessao;
        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }
        public void Login(Colaborador colaborador)
        {
            String colaboradorJSONString = JsonConvert.SerializeObject(colaborador);

            _sessao.Cadastrar(key, colaboradorJSONString);
        }
        public Colaborador GetColaborador()
        {
            if (_sessao.Existe(key))
            {
                string colaboradorJSONString = _sessao.Consultar(key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJSONString);
            }
            else
            {
                return null;
            }
        }
        
    }
}
