
namespace TestProject.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // We could use a ValueObject for email
        public string Email { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpenses { get; set; }
    }
}
