using System;
using System.Collections.Generic;
using Splitwise.Interfaces;
using Splitwise.Managers;

namespace Splitwise
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserManager um = new UserManager();
            IExpenseManager em = new ExpenseManager(um);

            AppManager am = new AppManager(em, um);

            am.AddUser(1, "Pankaj");
            am.AddUser(2, "Neha");
            am.AddUser(3, "Gudia");
            am.AddUser(4, "Jyoti");

            am.AddExpense(1, 100, ExpenseType.EQUAL, new List<int> { 2, 4 }, new List<double>(), new List<int>());
            am.AddExpense(3, 100, ExpenseType.PERCENT, new List<int> { 1, 2, 4 }, new List<double>(), new List<int> { 30, 30, 40 });
            am.AddExpense(2, 500, ExpenseType.EXACT, new List<int> { 1, 2, 3, 4 }, new List<double> { 150, 150, 100, 100 }, new List<int>());
            am.ShowBalance(1);
            //am.ShowBalances();
        }
    }
}
