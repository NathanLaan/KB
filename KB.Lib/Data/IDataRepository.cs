using System;
using KB.Lib.Entity;

namespace KB.Lib.Data
{
    public interface IDataRepository
    {
        Entry GetEntry(int id);
        void SaveEntry(Entry em);

        Account GetAccount(int id);
    }
}
