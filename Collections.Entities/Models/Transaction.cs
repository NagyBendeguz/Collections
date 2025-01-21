using Collections.Entities.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Models
{
    public class Transaction : IIdEntity
    {
        public Transaction(string senderUserId, string receiverUserId, string transactionType, string typeId)
        {
            Id = Guid.NewGuid().ToString();
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
            TransactionType = transactionType;
            TypeId = typeId;
        }

        public Transaction() { }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(50)]
        public string SenderUserId { get; set; }

        [StringLength(50)]
        public string ReceiverUserId { get; set; }

        [StringLength(20)]
        public string TransactionType { get; set; }

        [StringLength(50)]
        public string TypeId { get; set; }
    }
}
