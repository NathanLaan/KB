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



        #region Account

        private static readonly string SQL_ACCOUNT_INSERT = "INSERT INTO Account (Name,Email,Password,PasswordSalt) VALUES(@name,@email,@password,@passwordSalt); SELECT @@identity;";

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
            try
            {
                SqlConnection sqlConnection = new SqlConnection(this.connectionString);
                SqlCommand sqlCommand = new SqlCommand(ADODataRepository.SQL_ACCOUNT_INSERT, sqlConnection);
                object returnValue = sqlCommand.ExecuteScalar();

                int id = int.Parse(returnValue.ToString());

                return account;
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
