using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public interface IDataRepository
    {


        /// <summary>
        /// Return the value passed in, with the ID filled in. Do I really need this, or just following the pattern???
        /// </summary>
        /// <param name="entryVote"></param>
        /// <returns></returns>
        EntryVote Add(EntryVote entryVote);


        EntryVote GetEntryVote(int entryID, int accountID);


        #region Entry

        Entry Add(Entry entry);

        Entry GetEntry(int id);


        /// <summary>
        /// All top-level entries (Entry with no parent).
        /// </summary>
        /// <returns></returns>
        List<Entry> GetTopLevelEntryList(int page, int pageSize);

        List<Entry> GetEntryListForParent(int parentID);

        #endregion


        #region Account

        /// <summary>
        /// Creates the account, and populates the ID field.
        /// </summary>
        /// <param name="account">The account with populated ID field.</param>
        /// <returns></returns>
        Account Add(Account account);

        Account GetAccount(int id);

        Account GetAccount(string accountName);

        Account GetAccountForEmail(string email);

        List<Account> GetAccountList();

        List<Account> GetAccountListPaged(int page, int pageSize);

        #endregion


        //
        // Dashboard
        //

        List<Entry> GetLatestEntryList();
        List<Entry> GetPopularEntryList();
        List<Account> GetActiveAccountList();

        int GetTotalUsersCount();
        int GetTotalEntryCount();
        int GetTotalReplyCount();
        int GetTotalVotesCount();
        int GetTotalEntryCount(int accountID);
        int GetTotalReplyCount(int accountID);
        int GetTotalVotesCount(int accountID);

    }
}
