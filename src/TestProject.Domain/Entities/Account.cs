using TestProject.Domain.Enums;

namespace TestProject.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public CreditScore CreditScore { get; set; }

        public User User { get; set; }

    }
}
