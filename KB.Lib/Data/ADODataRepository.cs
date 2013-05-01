using System;
using System.Data.SqlClient;

namespace KB.Lib.Data
{
    public class ADODataRepository: IDataRepository
    {

        private string connectionString;

        public ADODataRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Entity.Entry GetEntry(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveEntry(Entity.Entry em)
        {
            throw new NotImplementedException();
        }

        public Entity.Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(string username, string password)
        {
            SqlConnection sc = new SqlConnection(this.connectionString);
            SqlCommand c = new SqlCommand("SELECT A.ID,A.Email,A.PasswordSalt FROM Account A WHERE A.Name=@Username", sc);
            c.Parameters.AddWithValue("@Username", username);

            object results = c.ExecuteScalar();
            return true;
        }
    }
}
