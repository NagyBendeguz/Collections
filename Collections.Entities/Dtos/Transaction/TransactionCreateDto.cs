using System.ComponentModel.DataAnnotations;

namespace Collections.Entities.Dtos.Transaction
{
    public class TransactionCreateDto
    {
        [MaxLength(50)]
        public required string ReceiverUserId { get; set; } = "";

        [MaxLength(20)]
        public required string TransactionType { get; set; } = "";

        [MaxLength(50)]
        public required string TypeId { get; set; } = "";
    }
}
