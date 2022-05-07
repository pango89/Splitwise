namespace Splitwise.Models
{
    public class Split
    {
        public Split(User paidToUser, double amount)
        {
            this.PaidToUser = paidToUser;
            this.Amount = amount;
        }

        public User PaidToUser { get; private set; }
        public double Amount { get; private set; }
    }
}
