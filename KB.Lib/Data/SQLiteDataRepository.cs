using System;
using System.Text;
using System.Data.SQLite;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public class SQLiteDataRepository: IDataRepository
    {
        

        private string connectionString;

        public SQLiteDataRepository(string connectionString)
        {
            this.connectionString = string.Format("Data Source={0}",connectionString);
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

        private static readonly string SQL_ACCOUNT_INSERT = "INSERT INTO [Account] (Name,Email,Password,PasswordSalt) VALUES(@Name,@Email,@Password,@PasswordSalt); SELECT last_insert_rowid();";

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAccount(string username, string password)
        {
            return true;
        }

        public Account AddAccount(Account account)
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ACCOUNT_INSERT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@Name", account.Name);
                    sqlCommand.Parameters.AddWithValue("@Email", account.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", account.Password);
                    sqlCommand.Parameters.AddWithValue("@PasswordSalt", account.PasswordSalt);
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

        #endregion

    }
}
