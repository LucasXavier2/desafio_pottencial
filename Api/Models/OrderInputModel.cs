namespace Api.Models
{
    public class OrderInputModel
    {
        public OrderInputModel(List<ProductOrderInputModel> orderedProducts, int sellerId)
        {
            OrderedProducts = orderedProducts;
            SellerId = sellerId;
        }

        public List<ProductOrderInputModel> OrderedProducts { get; private set; }
        public int SellerId { get; private set; }
    }
}