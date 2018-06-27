using System.Data;
using System.Collections.Generic;
using LoginFun.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;

namespace LoginFun.Factories
{
    public class UserFactory
    {
        static string server = "localhost";
        static string db = "mydb"; //Change to your schema name
        static string port = "3306"; //Potentially 8889
        static string user = "root";
        static string pass = "root";
        internal static IDbConnection Connection {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }
        public BaseUser GetUserById(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                //  [;'DROP DATABASE;]
                object paramObj = new{ MyLittleId = id };
                BaseUser user = dbConnection.Query<BaseUser>("SELECT ALL FROM Users WHERE UserId = @MyLittleId", paramObj).SingleOrDefault();
                return user;
            }
        }
        public bool UsernameExists(string username)
        {
            using(IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<BaseUser>($"SELECT ALL FROM Users WHERE Username = '{username}'").Count() > 0;
            }
        }

        public void CreateUser(RegisterUser user)
        {
            string SQL = "INSERT into users (Name, Username, Password) VALUES (@Name, @Username, @Password);";
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(SQL, user);
            }
        }
    }
}