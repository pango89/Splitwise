using System;
using System.Collections.Generic;
using System.Linq;
using Splitwise.Interfaces;
using Splitwise.Models;

namespace Splitwise
{
    public class AppManager
    {
        public AppManager(IExpenseManager expenseManager, IUserManager userManager)
        {
            this.ExpenseManager = expenseManager;
            this.UserManager = userManager;
        }

        public IExpenseManager ExpenseManager { get; private set; }
        public IUserManager UserManager { get; private set; }

        public void AddUser(int id, string name)
        {
            this.UserManager.AddUser(new User(id, name));
        }

        public void ShowBalance(int userId)
        {
            this.ExpenseManager.ShowBalance(userId);
        }

        public void ShowBalances()
        {
            this.ExpenseManager.ShowBalances();
        }

        public void AddExpense(int paidByUserId, double amount, ExpenseType expenseType, List<int> userIds, List<double> exactAmounts, List<int> percents)
        {
            List<Split> splits = new List<Split>();

            switch (expenseType)
            {
                case ExpenseType.EQUAL:
                    userIds.ForEach(id =>
                    {
                        Split split = new Split(this.UserManager.GetUserById(id), amount / userIds.Count);
                        splits.Add(split);
                    });
                    break;
                case ExpenseType.EXACT:
                    if (exactAmounts.Sum() != amount)
                    {
                        Console.WriteLine("Incorrect Data");
                        return;
                    }

                    for (int i = 0; i < userIds.Count; i++)
                    {
                        Split split = new Split(this.UserManager.GetUserById(userIds[i]), exactAmounts[i]);
                        splits.Add(split);
                    }
                    break;
                case ExpenseType.PERCENT:
                    if (percents.Sum() != 100)
                    {
                        Console.WriteLine("Incorrect Data");
                        return;
                    }

                    for (int i = 0; i < userIds.Count; i++)
                    {
                        Split split = new Split(this.UserManager.GetUserById(userIds[i]), percents[i] * amount / 100);
                        splits.Add(split);
                    }
                    break;
                default:
                    break;
            }

            Expense expense = new Expense("", new DateTime(), this.UserManager.GetUserById(paidByUserId), splits);
            this.ExpenseManager.AddExpense(expense);
        }
    }
}
