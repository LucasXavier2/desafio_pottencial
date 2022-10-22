namespace Api.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
    }
}