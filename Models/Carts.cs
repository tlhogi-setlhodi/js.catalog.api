namespace ThreadAndDaringStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }// foreign key
        public User? User{ get; set; }
    }
}