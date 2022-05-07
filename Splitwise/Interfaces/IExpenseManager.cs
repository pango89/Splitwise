
using Splitwise.Models;

namespace Splitwise.Interfaces
{
    public interface IExpenseManager
    {
        void AddExpense(Expense expense);
        void ShowBalance(int userId);
        void ShowBalances();
    }
}
