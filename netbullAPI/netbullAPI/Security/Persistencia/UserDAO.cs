using Dapper;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Security.Models;

namespace netbullAPI.Security.Persistencia
{
    public class UserDAO : DaoBase
    {
        protected IConfiguration _configuration;
        public UserDAO(INotificador notificador, IConfiguration configuration) : base(notificador, configuration)
        {
            _configuration = configuration;
        }

        internal async Task<User> CadastroDeUser(User usu)
        {
            try
            {
                var usuRecuperado = await RecuperarUsuario(usu);

                if (usuRecuperado == null)
                {

                    string sqlUser = $@" INSERT INTO users ( user_nome, user_email,user_accesskey)
                                            VALUES( '{usu.user_nome}', '{usu.user_email}', '{usu.user_accessKey}')";

                    var connection = getConnection();

                    using (connection)
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            connection.Execute(sqlUser, usu, transaction);

                            transaction.Commit();
                        }
                    }

                    usu = await RecuperarUsuario(usu);

                    return usu;
                }

                else
                {
                    Notificar("Usuário já cadastrado.");
                    usu.user_id = 0;
                    return usu;
                }
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
                return usu;
            }
        }

        internal async Task<User> VerificarUsuarioSenha(User usu)
        {
            try
            {
                var usuConsulta = await RecuperarUsuario(usu);
                if (usuConsulta != null)
                {
                    if (usuConsulta.user_accessKey == usu.user_accessKey)
                    {
                        return usuConsulta;
                    }
                    else
                    {
                        usu.user_id = 0;
                        Notificar("Senha incorreta");
                        return usu;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Notificar("Não foi possível verificar senha do usuário.");
                return usu;
            }
        }

        internal async Task<bool> alterarSenha(User usu)
        {
            var retorno = false;
            try
            {
                var usuario = (await getAllUsers()).Where(u => u.user_nome == usu.user_nome).FirstOrDefault();

                if (usuario != null)
                {
                    if (usuario.user_email.Equals(usu.user_email))
                    {
                        string sqlUser = $@" UPDATE users SET user_accesskey = '{usu.user_accessKey}' WHERE user_id = '{usuario.user_id}'";

                        var connection = getConnection();

                        using (connection)
                        {
                            connection.Open();

                            using (var transaction = connection.BeginTransaction())
                            {
                                connection.Execute(sqlUser, transaction);
                                transaction.Commit();
                            }
                        }
                        retorno = true;
                    }
                    else
                    {
                        Notificar("Endereço de e-mail não pertence a esse usuário.");
                        retorno = false;
                    }
                }
                else
                {
                    Notificar("Usuário informado não foi encontrado.");
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                Notificar("Nao foi possível alterar a senha do usuário.");
            }
            return retorno;
        }

        internal async Task<bool> DeleteUser(int id)
        {
            var retorno = false;
            try
            {
                var listaUsu = await getAllUsers();

                if (listaUsu.Exists(l => l.user_id == id))
                {
                    string sqlUser = $@" DELETE FROM users WHERE user_id = '{id}'";

                    var connection = getConnection();

                    using (connection)
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            connection.Execute(sqlUser, transaction);
                            transaction.Commit();
                        }
                    }

                    retorno = true;
                }
                else
                {
                    Notificar("Usuário informado não foi encontrado.");
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                Notificar("Não foi possível deletar usuario.");
            }
            return retorno;
        }

        internal async Task<List<User>> getAllUsers()
        {
            List<User> users = null;
            try
            {
                string sqlUser = $@" SELECT user_id, user_nome, user_email FROM users ";
                var connection = getConnection();

                using (connection)
                {

                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        users = connection.Query<User>(sqlUser, transaction).ToList();
                        transaction.Commit();
                    }
                }

                if (users == null)
                {
                    Notificar("Usuários não encontrados.");
                }

                return users;
            }
            catch (Exception ex)
            {
                Notificar("Não foi possível encontrar usuarios.");
                return users;
            }
        }

        internal async Task<User> RecuperarUsuario(User usu)
        {
            try
            {
                string sqlUser = $@" SELECT * FROM users WHERE user_nome = '{usu.user_nome}'";
                User user;

                var connection = getConnection();

                using (connection)
                {

                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        user = connection.Query<User>(sqlUser, usu, transaction).FirstOrDefault();
                        transaction.Commit();
                    }
                }

                if (user == null)
                {
                    Notificar("Usuário não encontrado.");
                }

                return user;
            }
            catch (Exception ex)
            {
                Notificar("Não foi possível recuperar usuario.");
                return usu;
            }
        }
    }
}
