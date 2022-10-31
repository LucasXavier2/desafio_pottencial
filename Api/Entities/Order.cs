using System.ComponentModel.DataAnnotations;
using Api.Enums;

namespace Api.Entities
{
    public class Order
    {
        public Order()
        {
            Status = OrderStatusEnum.PendingPayment;
            Date = DateTime.Now;
        }

        public Order(List<ProductOrder> orderedProducts)
        {
            OrderedProducts = orderedProducts;
            Status = OrderStatusEnum.PendingPayment;
            Date = DateTime.Now;
        }

        public Order(List<ProductOrder> orderedProducts, Seller seller)
        {
            OrderedProducts = orderedProducts;
            Seller = seller;
            Status = OrderStatusEnum.PendingPayment;
            Date = DateTime.Now;
        }

        [Key]
        public int Id { get; private set; }
        [Required]
        public List<ProductOrder> OrderedProducts { get; private set; }
        [Required]
        public Seller Seller { get; private set; }
        [Required]
        public OrderStatusEnum Status { get; private set; }
        [Required]
        public DateTime Date { get; private set; }

        public bool UpdateStatus(OrderStatusEnum newStatus)
        {
            if (Status == newStatus) return false;

            if (Status == OrderStatusEnum.PendingPayment)
            {
                if (newStatus == OrderStatusEnum.ApprovedPayment || newStatus == OrderStatusEnum.Canceled)
                {
                    Status = newStatus;
                }
            }
            else if (Status == OrderStatusEnum.ApprovedPayment)
            {
                if (newStatus == OrderStatusEnum.Shipped || newStatus == OrderStatusEnum.Canceled)
                {
                    Status = newStatus;
                }
            }
            else if (Status == OrderStatusEnum.Shipped && newStatus == OrderStatusEnum.Completed)
            {
                Status = newStatus;
            }
            
            if (Status == newStatus) return true;
            return false;
        }
    }
}