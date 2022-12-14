namespace Api.Models
{
    public class SellerViewModel
    {
        public SellerViewModel(int id, string name, string cpf, string email, string phone)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}