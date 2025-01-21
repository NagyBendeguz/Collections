using Collections.Data;
using Collections.Entities.Dtos.Transaction;
using Collections.Entities.Models;
using Collections.Logic.Helpers;

namespace Collections.Logic.Logic
{
    public class TransactionLogic
    {
        Repository<Transaction> repo;
        DtoProvider dtoProvider;

        public TransactionLogic(Repository<Transaction> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public IEnumerable<TransactionViewDto> GetPendingTransactions()
        {
            return repo.GetAll().Select(t => dtoProvider.Mapper.Map<TransactionViewDto>(t));
        }

        public TransactionViewDto GetTransactionById(string id)
        {
            var model = repo.GetById(id);
            return dtoProvider.Mapper.Map<TransactionViewDto>(model);
        }

        public void CreateTransaction(TransactionCreateDto dto, string senderUserId)
        {
            var model = dtoProvider.Mapper.Map<Transaction>(dto);
            model.SenderUserId = senderUserId;
            repo.Create(model);
        }

        public void DeleteTransaction(string id)
        {
            repo.DeleteById(id);
        }
    }
}
