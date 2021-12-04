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

        internal User CadastroDeUser(User usu)
        {
            var usuRecuperado = RecuperarUsuario(usu);

            if(usuRecuperado == null)
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

                    usu = RecuperarUsuario(usu);
                    
                    return usu; 
                }
                catch (Exception ex)
                {
                    Notificar(ex.Message);
                    return usu;
                }
            }
            else {
                Notificar("Usuário já cadastrado.");
                usu.user_id = 0;
                return usu;
            }          
        }

        internal User RecuperarUsuario(User usu)
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
