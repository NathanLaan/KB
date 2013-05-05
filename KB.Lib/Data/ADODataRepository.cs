using System;
using System.Collections.Generic;
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

        public Entry AddEntry(Entity.Entry em)
        {
            throw new NotImplementedException();
        }



        #region Account

        private static readonly string SQL_ACCOUNT_INSERT = "INSERT INTO Account (Name,Email,Password,PasswordSalt) VALUES(@name,@email,@password,@passwordSalt); SELECT @@identity;";

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }
        public Account GetAccount(string accountName)
        {
            throw new NotImplementedException();
        }

        public Account AddAccount(Account account)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(ADODataRepository.SQL_ACCOUNT_INSERT, sqlConnection);
                    object returnValue = sqlCommand.ExecuteScalar();

                    int id = int.Parse(returnValue.ToString());
                    account.ID = id;
                }

                return account;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<Account> GetAccountList()
        {
            return null;
        }

        #endregion

    }
}
