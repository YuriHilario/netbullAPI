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
            var usuRecuperado = await RecuperarUsuario(usu);

            if (usuRecuperado == null)
            {
                try
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
                catch (Exception ex)
                {
                    Notificar(ex.Message);
                    return usu;
                }
            }
            else
            {
                Notificar("Usuário já cadastrado.");
                usu.user_id = 0;
                return usu;
            }
        }

        internal async Task<bool> DeleteUser(int id)
        {
            var retorno = false;
            try
            {
                var listaUsu = await getAllUsers();

                if( listaUsu.Exists(l=> l.user_id == id))
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
                Notificar(ex.Message);
                retorno = false;
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

                return users;
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
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

                return user;
            }
            catch (Exception ex)
            {
                Notificar(ex.Message);
                return usu;
            }

        }
    }
}
