using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KB.Lib.Data
{
    public interface IDataRepository
    {
        Entry Get(int id);
        void Save(Entry em);
    }
}
