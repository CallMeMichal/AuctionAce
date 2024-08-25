namespace AuctionAce.Api.Models.DTO.Chat.Request
{
    public class ChatHistoryRequest
    {
        public string GroupName { get; set; }
        public string Message { get; set; }
        public string UserEmail { get; set; }
        public int AuctionItemId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
