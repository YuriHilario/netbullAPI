using Dapper;
using netbullAPI.Security.Models;
using System.Data.SqlClient;
using System.Linq;

namespace netbullAPI.Security.Persistencia
{
    public class UserDAO
    {
        private IConfiguration _configuration;

        public UserDAO()
        {
        }
        public UserDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal User CadastroDeUser(User usu)
        {
            try
            {
                string sqlUser = $@" INSERT INTO Users ( user_nome, user_email,user_accesskey)
                                            VALUES({usu.user_id}, {usu.user_nome}, {usu.user_email}, {usu.user_accessKey})";
                //string sqlRoles = $@"INSERT INTO Roles (role_descricao,role_idUser)
                //                     VALUES(@role_descricao, @role_idUser)";

                using (SqlConnection conexao = new SqlConnection())
                {
                    conexao.Open();

                    using (var transaction = conexao.BeginTransaction(_configuration.GetConnectionString("NetBullConnection")))
                    {
                        var retornoAluno = conexao.Execute(sqlUser, usu, transaction);

                        //foreach (var role in usu.ListaRoles)
                        //{
                        //    role.role_idUser = usu.user_id;
                        //}

                        //var retornoRoles = conexao.Execute(sqlRoles, usu.ListaRoles, transaction);

                        transaction.Commit();
                    }
                }
                return usu;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal User RecuperarUsuario(User usu)
        {
            try
            {
                string sqlUser = $@" SELECT * Users WHERE user_id = user_id and user_nome = @user_nome";
                //string sqlRoles = $@"INSERT INTO Roles (role_descricao,role_idUser)
                //                     VALUES(@role_descricao, @role_idUser)";

                User user;

                using (SqlConnection conexao = new SqlConnection())
                {

                    conexao.Open();

                    using (var transaction = conexao.BeginTransaction(_configuration.GetConnectionString("NetBullConnection")))
                    {
                        user = conexao.Query<User>(sqlUser, param: new { user_nome = usu.user_nome }, transaction).FirstOrDefault();
                        transaction.Commit();
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
