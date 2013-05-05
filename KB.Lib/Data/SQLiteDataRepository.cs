using System;
using System.Collections.Generic;
using System.Data.SQLite;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public class SQLiteDataRepository: IDataRepository
    {
        

        private string connectionString;

        private static readonly string SQL_ENTITY_INSERT = "INSERT INTO [Entity] (ParentID,AccountID,Title,Contents,Timestamp) VALUES(@ParentID,@AccountID,@Title,@Contents,@Timestamp); SELECT last_insert_rowid();";
        
        public SQLiteDataRepository(string connectionString)
        {
            this.connectionString = string.Format("Data Source={0}",connectionString);
        }

        public Entry GetEntry(int id)
        {
            throw new NotImplementedException();
        }

        public Entry AddEntry(Entry entry)
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTITY_INSERT, sqliteConnection);
                    //ParentID,AccountID,Title,Content,Timestamp
                    sqlCommand.Parameters.AddWithValue("@ParentID", entry.ParentID);
                    sqlCommand.Parameters.AddWithValue("@AccountID", entry.AccountID);
                    sqlCommand.Parameters.AddWithValue("@Title", entry.Title);
                    sqlCommand.Parameters.AddWithValue("@Content", entry.Contents);
                    sqlCommand.Parameters.AddWithValue("@Timestamp", entry.Timestamp);
                    object returnValue = sqlCommand.ExecuteScalar();

                    int id = int.Parse(returnValue.ToString());
                    entry.ID = id;
                }

                return entry;
            }
            catch (Exception exception)
            {
                return null;
            }
        }



        #region Account


        private static readonly string SQL_ACCOUNT_INSERT = "INSERT INTO [Account] (Name,Email,Password,PasswordSalt) VALUES(@Name,@Email,@Password,@PasswordSalt); SELECT last_insert_rowid();";
        private static readonly string SQL_ACCOUNT_SELECT_BY_ID = "SELECT ID,Name,Email,Password,PasswordSalt FROM [Account] WHERE ID=@ID;";
        private static readonly string SQL_ACCOUNT_SELECT_BY_NAME = "SELECT ID,Name,Email,Password,PasswordSalt FROM [Account] WHERE Name=@Name;";
        private static readonly string SQL_ACCOUNT_SELECT_ALL = "SELECT ID,Name,Email,Password,PasswordSalt FROM [Account] ORDER BY ID ASC;";


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

        public Account GetAccount(int id)
        {
            try
            {
                Account account = new Account();
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ACCOUNT_SELECT_BY_ID, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);

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
        public Account GetAccount(string accountName)
        {
            try
            {
                Account account = new Account();
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ACCOUNT_SELECT_BY_NAME, sqliteConnection);
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
