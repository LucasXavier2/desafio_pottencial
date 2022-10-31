namespace Api.Models
{
    public class ProductOrderViewModel
    {
        public ProductOrderViewModel(int id, ProductViewModel product, SellerViewModel seller, double unitPrice, int quantity, DateTime date)
        {
            Id = id;
            Product = product;
            Seller = seller;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Date = date;
        }

        public int Id { get; private set; }
        public ProductViewModel Product { get; private set;}
        public SellerViewModel Seller { get; private set; }
        public double UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public DateTime Date { get; private set; }
    }
}