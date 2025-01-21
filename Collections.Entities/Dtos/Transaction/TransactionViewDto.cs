namespace Collections.Entities.Dtos.Transaction
{
    public class TransactionViewDto
    {
        public string Id { get; set; } = "";

        public string SenderUserId { get; set; } = "";

        public string ReceiverUserId { get; set; } = "";

        public string TransactionType { get; set; } = "";

        public string TypeId { get; set; } = "";
    }
}
