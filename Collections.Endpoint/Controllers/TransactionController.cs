using Collections.Entities.Dtos.Transaction;
using Collections.Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Collections.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        TransactionLogic transactionLogic;
        CollectionLogic collectionLogic;
        ObjectLogic objectLogic;
        UserManager<IdentityUser> userManager;

        public TransactionController(TransactionLogic transactionLogic, CollectionLogic collectionLogic, ObjectLogic objectLogic, UserManager<IdentityUser> userManager)
        {
            this.transactionLogic = transactionLogic;
            this.collectionLogic = collectionLogic;
            this.objectLogic = objectLogic;
            this.userManager = userManager;
        }

        [HttpGet("pending")]
        public IEnumerable<TransactionViewDto> GetPendingTransactions()
        {
            return transactionLogic.GetPendingTransactions();
        }

        [HttpPost("start")]
        public async Task StartTransaction(TransactionCreateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            if (dto.TransactionType == "object")
            {
                var currentObject = objectLogic.GetObjectById(dto.TypeId);

                if (currentObject.UserId == user.Id)
                {
                    transactionLogic.CreateTransaction(dto, user.Id);
                }
                else
                {
                    throw new ArgumentException("You can only send your own objects!");
                }
            }
            else if (dto.TransactionType == "collection")
            {
                var currentCollection = collectionLogic.GetCollectionById(dto.TypeId);

                if (currentCollection.UserId == user.Id)
                {
                    transactionLogic.CreateTransaction(dto, user.Id);
                }
                else
                {
                    throw new ArgumentException("You can only send your own collections!");
                }
            }
            else
            {
                throw new ArgumentException("Invalid transaction type! It can only be: 'object' and 'collection'.");
            }
        }

        [HttpDelete("accept/{id}")]
        public async Task AcceptTransaction(string id, string collectionId)
        {
            var user = await userManager.GetUserAsync(User);

            var transaction = transactionLogic.GetTransactionById(id);

            if (transaction.ReceiverUserId == user.Id)
            {
                if (transaction.TransactionType.ToLower() == "object")
                {
                    var checkCollection = collectionLogic.GetCollectionById(collectionId);

                    if (checkCollection != null && checkCollection.UserId == user.Id)
                    {
                        objectLogic.ChangeObjectOwnerAndCollection(transaction, collectionId);

                        transactionLogic.DeleteTransaction(id);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid collection id! You need to add one of your collection id in order to accept an object!");
                    }
                }
                else if (transaction.TransactionType.ToLower() == "collection")
                {
                    collectionLogic.ChangeCollectionAndItsObjectsOwner(transaction);

                    transactionLogic.DeleteTransaction(id);
                }
            }
            else
            {
                throw new ArgumentException("You are not allowed to accept some else's transaction!");
            }
        }

        [HttpDelete("refuse/{id}")]
        public async Task RefuseTransaction(string id)
        {
            var user = await userManager.GetUserAsync(User);

            var transaction = transactionLogic.GetTransactionById(id);

            if (transaction.ReceiverUserId == user.Id)
            {
                transactionLogic.DeleteTransaction(id);
            }
            else
            {
                throw new ArgumentException("You are not allowed to refuse some else's transaction!");
            }
        }
    }
}
