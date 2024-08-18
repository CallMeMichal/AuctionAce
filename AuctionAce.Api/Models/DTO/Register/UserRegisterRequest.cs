namespace AuctionAce.Api.Models.DTO.Register
{
    public class UserRegisterRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdRoles { get; set; }
    }
}