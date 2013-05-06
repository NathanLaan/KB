﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public class SQLiteDataRepository: IDataRepository
    {

        #region Properties
        private string connectionString;
        #endregion

        #region Constructor
        public SQLiteDataRepository(string connectionString)
        {
            this.connectionString = string.Format("Data Source={0}",connectionString);
        }
        #endregion


        private static readonly string SQL_EntryVote_SELECT
            = "SELECT ID,EntryID,AccountID,Vote FROM [EntryVote]"
            + "WHERE EntryID=@EntryID AND AccountID=@AccountID;";
        private static readonly string SQL_EntryVote_INSERT
            = "INSERT INTO [EntryVote] EntryID,AccountID,Vote VALUES(@EntryID,@AccountID,@Vote); SELECT last_insert_rowid();";
        

        public EntryVote Add(EntryVote entryVote)
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ACCOUNT_INSERT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@EntryID", entryVote.EntryID);
                    sqlCommand.Parameters.AddWithValue("@AccountID", entryVote.AccountID);
                    sqlCommand.Parameters.AddWithValue("@Vote", entryVote.Vote);
                    object returnValue = sqlCommand.ExecuteScalar();

                    int id = int.Parse(returnValue.ToString());
                    entryVote.ID = id;
                }
                return entryVote;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        public EntryVote Get(int entryID, int accountID)
        {
            try
            {
                EntryVote entryVote = new EntryVote();
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_EntryVote_SELECT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@EntryID", entryID);
                    sqlCommand.Parameters.AddWithValue("@AccountID", accountID);

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entryVote.ID = reader.GetInt32(0);
                            entryVote.EntryID = reader.GetInt32(1);
                            entryVote.AccountID = reader.GetInt32(2);
                            entryVote.Vote = reader.GetInt32(3);
                        }
                    }
                }
                return entryVote;
            }
            catch (Exception exception)
            {
                return null;
            }
        }



        #region Entry


        private static readonly string SQL_ENTRY_INSERT = "INSERT INTO [Entry] (ParentID,AccountID,Title,Contents,Timestamp) VALUES(@ParentID,@AccountID,@Title,@Contents,@Timestamp); SELECT last_insert_rowid();";
        private static readonly string SQL_ENTRY_SELECT_BY_ID = "SELECT ID,ParentID,AccountID,Title,Contents,Timestamp FROM [Entry] WHERE ID=@ID;";
        private static readonly string SQL_ENTRY_SELECT_BY_ID_WITH_Account =
            "SELECT Entry.ID,Entry.ParentID,Entry.AccountID,Entry.Title,Entry.Contents,Entry.Timestamp," +
            "Account.Name,Account.Email,Account.Score FROM [Entry] LEFT OUTER JOIN [Account] ON Entry.AccountID=Account.ID WHERE Entry.ID=@ID;";
        private static readonly string SQL_ENTRY_SELECT_BY_ParentID_WITH_Account
            = "SELECT Entry.ID,Entry.ParentID,Entry.AccountID,Entry.Title,Entry.Contents,Entry.Timestamp,"
            + "Account.Name,Account.Email,Account.Score FROM [Entry] LEFT OUTER JOIN [Account] ON Entry.AccountID=Account.ID "
            + "WHERE Entry.ParentID=@ID ORDER BY Entry.Timestamp ASC;";
        private static readonly string SQL_ENTRY_SELECT_NO_PARENT
            = "SELECT Entry.ID,Entry.ParentID,Entry.AccountID,Entry.Title,Entry.Contents,Entry.Timestamp,"
            + "Account.Name,Account.Email,Account.Score FROM [Entry] LEFT OUTER JOIN [Account] ON Entry.AccountID=Account.ID "
            + "WHERE Entry.ParentID IS NULL ORDER BY Entry.Timestamp DESC LIMIT @Limit OFFSET @Offset;";


        /// <summary>
        /// All top-level entries (Entry with no parent).
        /// </summary>
        /// <returns></returns>
        public List<Entry> GetTopLevelEntryList(int page, int pageSize)
        {
            List<Entry> entryList = new List<Entry>();
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    //
                    // "SELECT ID FROM Entry ORDER BY Timestamp ASC LIMIT 2 OFFSET 1"
                    //
                    // LIMIT = pageSize
                    // OFFSET = page * pageSize
                    //
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTRY_SELECT_NO_PARENT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@Limit", pageSize);
                    sqlCommand.Parameters.AddWithValue("@Offset", (page-1) * pageSize);
                    

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Entry entry = new Entry();
                            entry.ID = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                            {
                                entry.ID = reader.GetInt32(1);
                            }
                            else
                            {
                                entry.ParentID = null;
                            }
                            entry.AccountID = reader.GetInt32(2);
                            entry.Title = reader.GetString(3);
                            entry.Contents = reader.GetString(4);
                            entry.Timestamp = reader.GetDateTime(5);

                            entry.Author = new Account();
                            entry.Author.ID = entry.AccountID;
                            entry.Author.Name = reader.GetString(6);
                            entry.Author.Email = reader.GetString(7);
                            //entry.Author.Score = reader.GetString(6);

                            entryList.Add(entry);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return null;
            }
            return entryList;
        }

        public List<Entry> GetEntryListForParent(int parentID)
        {
            List<Entry> entryList = new List<Entry>();
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTRY_SELECT_BY_ParentID_WITH_Account, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", parentID);

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Entry entry = new Entry();
                            entry.ID = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                            {
                                entry.ID = reader.GetInt32(1);
                            }
                            else
                            {
                                entry.ParentID = null;
                            }
                            entry.AccountID = reader.GetInt32(2);
                            entry.Title = reader.GetString(3);
                            entry.Contents = reader.GetString(4);
                            entry.Timestamp = reader.GetDateTime(5);

                            entry.Author = new Account();
                            entry.Author.ID = entry.AccountID;
                            entry.Author.Name = reader.GetString(6);
                            entry.Author.Email = reader.GetString(7);
                            //entry.Author.Score = reader.GetString(6);

                            entryList.Add(entry);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return null;
            }
            return entryList;
        }


        public Entry GetEntry(int id)
        {
            try
            {
                Entry entry = new Entry();
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTRY_SELECT_BY_ID_WITH_Account, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
                        //
                        // Read the first record only
                        //
                        if (reader.Read())
                        {
                            entry.ID = reader.GetInt32(0);
                            if (!reader.IsDBNull(1))
                            {
                                entry.ID = reader.GetInt32(1);
                            }
                            else
                            {
                                entry.ParentID = null;
                            }
                            entry.AccountID = reader.GetInt32(2);
                            entry.Title = reader.GetString(3);
                            entry.Contents = reader.GetString(4);
                            entry.Timestamp = reader.GetDateTime(5);

                            entry.Author = new Account();
                            entry.Author.ID = entry.AccountID;
                            entry.Author.Name = reader.GetString(6);
                            entry.Author.Email = reader.GetString(7);
                            //entry.Author.Score = reader.GetString(6);
                        }
                    }
                }
                return entry;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public Entry AddEntry(Entry entry)
        {
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTRY_INSERT, sqliteConnection);
                    //ParentID,AccountID,Title,Content,Timestamp
                    sqlCommand.Parameters.AddWithValue("@ParentID", entry.ParentID);
                    sqlCommand.Parameters.AddWithValue("@AccountID", entry.AccountID);
                    sqlCommand.Parameters.AddWithValue("@Title", entry.Title);
                    sqlCommand.Parameters.AddWithValue("@Contents", entry.Contents);
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

        #endregion


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

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
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

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
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

                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
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



        private static readonly string SQL_ENTRY_COUNT_PARENT_FOR_ACCOUNT
            = "SELECT COUNT(Entry.ID) FROM [Entry] WHERE Entry.ParentID IS NOT NULL AND AccountID=@AccountID;";
        //
        // Replies:
        //
        private static readonly string SQL_ENTRY_COUNT_NO_PARENT_FOR_ACCOUNT
            = "SELECT COUNT(Entry.ID) FROM [Entry] WHERE Entry.ParentID IS NULL AND AccountID=@AccountID;";

        private static readonly string SQL_EntryVote_COUNT
            = "SELECT COUNT(ID) FROM [EntryVote] WHERE AccountID=@AccountID;";

        public int GetTotalEntryCount(int accountID)
        {
            int count = 0;
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTRY_COUNT_NO_PARENT_FOR_ACCOUNT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@AccountID", accountID);
                    object returnValue = sqlCommand.ExecuteScalar();
                    count = int.Parse(returnValue.ToString());
                }
            }
            catch (Exception exception)
            {
            }
            return count;
        }
        public int GetTotalReplyCount(int accountID)
        {
            int count = 0;
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_ENTRY_COUNT_PARENT_FOR_ACCOUNT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@AccountID", accountID);
                    object returnValue = sqlCommand.ExecuteScalar();
                    count = int.Parse(returnValue.ToString());
                }
            }
            catch (Exception exception)
            {
            }
            return count;
        }
        public int GetTotalVotesCount(int accountID)
        {
            int count = 0;
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(this.connectionString))
                {
                    sqliteConnection.Open();
                    SQLiteCommand sqlCommand = new SQLiteCommand(SQLiteDataRepository.SQL_EntryVote_COUNT, sqliteConnection);
                    sqlCommand.Parameters.AddWithValue("@AccountID", accountID);
                    object returnValue = sqlCommand.ExecuteScalar();
                    count = int.Parse(returnValue.ToString());
                }
            }
            catch (Exception exception)
            {
            }
            return count;
        }


    }
}
