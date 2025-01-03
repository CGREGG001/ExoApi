using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ExoApi;

public class UserServices
{
    public SqlConnection Connection;

    public UserServices(SqlConnection connection)
    {
        Connection = connection;
    }

    public List<User> GetAllUsers()
    {
        List<User> users = [];
        using (SqlCommand sqlCommand = Connection.CreateCommand())
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = @"SELECT * FROM [User]";

            Connection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
            }

            Connection.Close();
        }

        return users.ToList();
    }


    public User GetUserById(int id)
    {
        using (SqlCommand sqlCommand = Connection.CreateCommand())
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = $"SELECT * FROM [User] WHERE Id = {id}";

            Connection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    User user = new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                    return user;
                }
            }
        }

        Connection.Close();
        return null;
    }
    public void DeleteUser(int id)
    {
        using (SqlCommand sqlCommand = Connection.CreateCommand())
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = $"DELETE FROM [User] WHERE Id = {id}";

            Connection.Open();
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }
    }

    public void UpdateUser(int id, User user)
    {
        using (SqlCommand sqlCommand = Connection.CreateCommand())
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = $"UPDATE [User] SET Username = '{user.Username}', Email = '{user.Email}', Password = '{user.Password}' WHERE Id = {user.Id}";

            Connection.Open();
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }
    }

    public User? Login(string email)
    {
        using (SqlCommand sqlCommand = Connection.CreateCommand())
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = $"SELECT * FROM [User] WHERE Email = '{email}'";

            Connection.Open();

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    User user = new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                    return user;
                }
            }
        }

        Connection.Close();
        return null;
    }

    public void Register(User user)
    {
        using (SqlCommand sqlCommand = Connection.CreateCommand())
        {
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = $"INSERT INTO [User] (Username, Email, Password) VALUES ('{user.Username}', '{user.Email}', '{user.Password}')";

            Connection.Open();
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
