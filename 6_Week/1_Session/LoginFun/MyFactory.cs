using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LoginFun.Models;
using MySql.Data.MySqlClient;

namespace LoginFun
{
    public class UserFactory
    {
        // Connect to a DB
        static string server = "localhost";
        static string db = "mydb"; //Change to your schema name 
        static string port = "3306"; //Potentially 8889
        static string user = "root";
        static string pass = "root";

        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }

        public List<BaseUser> AllUsers()
        {
            using(IDbConnection dbConnection = Connection)
            {
                IEnumerable<BaseUser> result = dbConnection.Query<BaseUser>("SELECT * FROM Users");
                return result.Take(50).ToList();
            }
        }
        public bool UsernameExists(string un)
        {
            string SQL = $"SELECT * FROM users WHERE username = @MySillyUserName";

            object paramObj = new {MySillyUserName = un};

            using(IDbConnection dbConnection = Connection)
            {
                IEnumerable<BaseUser> result = dbConnection.Query<BaseUser>(SQL, paramObj);
                int numUsersWithUsername = result.Count();

                return numUsersWithUsername > 0;
            }
        }
        public string HashedPWFromUsername(string un)
        {
           
            string SQL = $"SELECT * FROM users WHERE username = @MySillyUserName";

            object paramObj = new {MySillyUserName = un};

            using(IDbConnection dbConnection = Connection)
            {
                IEnumerable<BaseUser> result = dbConnection.Query<BaseUser>(SQL, paramObj);
                
                if(result.SingleOrDefault() == null)
                {
                    return null;
                }
                return result.SingleOrDefault().Password;
            }
        }
        public void CreateUser(BaseUser user)
        {
            string SQL = $@"INSERT into users (Name, Username, Password) VALUES (@Name, @Username, @Password);";
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(SQL, user);
            }

        }
    }
}