namespace AuctionAce.Infrastructure.Data.Models
{
    public partial class ChatHistory
    {
        public int Id { get; set; }
        public int AuctionItemId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public virtual AuctionItem AuctionItem { get; set; }
        public virtual User User { get; set; }
    }
}
