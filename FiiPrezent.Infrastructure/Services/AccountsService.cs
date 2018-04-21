using System;
using System.Linq;
using System.Threading.Tasks;
using FiiPrezent.Core;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;

namespace FiiPrezent.Infrastructure.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultStatus> CreateAccount(Account account)
        {
            if(await GetAccountByNameIdentifier(account.NameIdentifier) != null)
                return new ResultStatus(ResultStatusType.AlreadyExists);

            account.Id = new Guid();

            await _unitOfWork.Accounts.AddAsync(account);
            await _unitOfWork.CompletedAsync();

            return new ResultStatus();
        }

        public async Task<Account> GetAccountById(Guid id)
        {
            return await _unitOfWork.Accounts.GetByIdAsync(id);
        }

        public async Task<Account> GetAccountByNameIdentifier(string nameIdentifier)
        {
            return (await _unitOfWork.Accounts.GetAsync(x => x.NameIdentifier == nameIdentifier)).SingleOrDefault();
        }

        public async Task<bool> Exists(string nameIdentifier)
        {
            return (await GetAccountByNameIdentifier(nameIdentifier)) != null;
        }
    }
}