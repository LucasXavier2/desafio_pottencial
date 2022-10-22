namespace Api.Models
{
    public class ProductInputModel
    {
        public ProductInputModel(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public double Price { get; private set; }
    }
}