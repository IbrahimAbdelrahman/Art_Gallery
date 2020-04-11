namespace DutchTreat.Data.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public Order Order { get; set; }
    }
}