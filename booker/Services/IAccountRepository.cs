using booker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public interface IAccountRepository
    {
        IEnumerable<IAccount> GetAccounts();
        IAccount GetAccount(int id);
        int DeleteAccount(int id);
        int SaveAccount(IAccount item);
    }
}
