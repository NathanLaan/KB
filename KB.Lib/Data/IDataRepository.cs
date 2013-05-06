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


        EntryVote Get(int entryID, int accountID);


        #region Entry

        Entry GetEntry(int id);

        Entry AddEntry(Entry em);


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
        Account AddAccount(Account account);

        Account GetAccount(int id);

        Account GetAccount(string accountName);

        List<Account> GetAccountList();

        #endregion


        int GetTotalEntryCount(int accountID);
        int GetTotalReplyCount(int accountID);
        int GetTotalVotesCount(int accountID);

    }
}
