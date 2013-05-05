using System;
using System.Collections.Generic;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public interface IDataRepository
    {

        #region Entry

        Entry GetEntry(int id);

        Entry AddEntry(Entry em);

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

    }
}
