using Api.Enums;

namespace Api.Models
{
    public class OrderViewModel
    {
        public OrderViewModel(int id, List<ProductOrderViewModel> orderedProducts, OrderStatusEnum status, DateTime date)
        {
            Id = id;
            OrderedProducts = orderedProducts;
            Status = status;
            Date = date;
        }

        public int Id { get; private set; }
        public List<ProductOrderViewModel> OrderedProducts { get; private set; }
        public SellerViewModel Seller { get; private set; }
        public OrderStatusEnum Status { get; private set; }
        public DateTime Date { get; private set; }
    }
}