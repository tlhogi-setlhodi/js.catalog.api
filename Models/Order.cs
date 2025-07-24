
namespace ThreadAndDaringStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }// foreign key
        public User? User{ get; set; }//Navigation Property 
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount{ get; set; }
    }
}