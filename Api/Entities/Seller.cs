using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class Seller
    {
        public Seller(string name, string cpf, string email, string phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
        }

        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public string Cpf { get; private set; }
        [Required]
        public string Email { get; private set; }
        [Required]
        public string Phone { get; private set; }
    }
}