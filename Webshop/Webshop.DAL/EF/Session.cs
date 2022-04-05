namespace Webshop.DAL.EF
{
    public class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SessionId { get; set; }
        public bool IsActive { get; set; }

        public User User { get; set; }
    }
}
