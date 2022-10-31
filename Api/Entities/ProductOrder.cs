using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class ProductOrder
    {
        public ProductOrder(double unitPrice, int quantity)
        {
            UnitPrice = unitPrice;
            Quantity = quantity;
            Date = DateTime.Now;
        }
        public ProductOrder(Product product, Seller seller, double unitPrice, int quantity)
        {
            Product = product;
            Seller = seller;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Date = DateTime.Now;
        }

        [Key]
        public int Id { get; private set; }
        [Required]
        public Product Product { get; private set; }
        [Required]
        public Seller Seller { get; private set; }
        [Required]
        public double UnitPrice { get; private set; }
        [Required]
        public int Quantity { get; private set; }
        [Required]
        public DateTime Date { get; private set; }
    }
}