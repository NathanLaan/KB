using System;
using System.Collections.Generic;
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


        public List<Account> GetAccountList()
        {
            List<Account> accountList = new List<Account>();

            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ACCOUNT_SELECT_ALL, sqliteConnection);

                    SQLiteDataReader reader = sqlCommand.ExecuteReader();
                    //
                    // Read the first record only
                    //
                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.ID = reader.GetInt32(0);
                        account.Name = reader.GetString(1);
                        account.Email = reader.GetString(2);
                        account.Password = reader.GetString(3);
                        account.PasswordSalt = reader.GetString(4);
                        accountList.Add(account);
                    }
                }
            }
            catch (Exception exception)
            {
            }

            return accountList;
        }

        private static readonly string SQL_ACCOUNT_INSERT = "INSERT INTO [Account] (Name,Email,Password,PasswordSalt) VALUES(@Name,@Email,@Password,@PasswordSalt); SELECT last_insert_rowid();";
        private static readonly string SQL_ACCOUNT_SELECT = "SELECT ID,Name,Email,Password,PasswordSalt FROM [Account] WHERE Name=@Name;";
        private static readonly string SQL_ACCOUNT_SELECT_ALL = "SELECT ID,Name,Email,Password,PasswordSalt FROM [Account] ORDER BY ID ASC;";

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }
        public Account GetAccount(string accountName)
        {
            try
            {
                Account account = new Account();
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ACCOUNT_SELECT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@Name", accountName);

                    SQLiteDataReader reader = sqlCommand.ExecuteReader();
                    //
                    // Read the first record only
                    //
                    if (reader.Read())
                    {
                        account.ID = reader.GetInt32(0);
                        account.Name = reader.GetString(1);
                        account.Email = reader.GetString(2);
                        account.Password = reader.GetString(3);
                        account.PasswordSalt = reader.GetString(4);
                    }
                }
                return account;
            }
            catch (Exception exception)
            {
                return null;
            }
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
            catch (Exception exception)
            {
                return null;
            }
        }

        #endregion

    }
}
