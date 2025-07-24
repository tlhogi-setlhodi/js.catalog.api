namespace ThreadAndDaringStore.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int CartId { get; set; }//foreign key
        public Cart? Cart { get; set; }// Navigation property
        public int ProductId { get; set; }
        public Product? Product{ get; set; }
        public int Quantity { get; set; }
    }
}