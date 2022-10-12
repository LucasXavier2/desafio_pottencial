namespace Api.Models
{
    public class SellerInputModel
    {
        public SellerInputModel(string name, string cpf, string email, string phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
        }

        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}