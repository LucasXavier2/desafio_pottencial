namespace Api.Models
{
    public class ProductOrderInputModel
    {
        public ProductOrderInputModel(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
    }
}