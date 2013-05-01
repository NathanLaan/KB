using System;
using System.Data.SqlClient;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public class ADODataRepository: IDataRepository
    {

        private string connectionString;

        public ADODataRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Entry GetEntry(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveEntry(Entity.Entry em)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAccount(string username, string password)
        {
            SqlConnection sc = new SqlConnection(this.connectionString);
            SqlCommand c = new SqlCommand("SELECT A.ID,A.Email,A.PasswordSalt FROM Account A WHERE A.Name=@Username", sc);
            c.Parameters.AddWithValue("@Username", username);

            object results = c.ExecuteScalar();
            return true;
        }

        public Account AddAccount(Account account)
        {


            return account;
        }
    }
}
