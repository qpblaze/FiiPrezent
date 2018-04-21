using System;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;

namespace FiiPrezent.Core.Interfaces
{
    public interface IAccountsService
    {
        Task<ResultStatus> CreateAccount(Account account);

        Task<Account> GetAccountById(Guid id);
        Task<Account> GetAccountByNameIdentifier(string nameIdentifier);

        Task<bool> Exists(string nameIdentifier);
    }
}