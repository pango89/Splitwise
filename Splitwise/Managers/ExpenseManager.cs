using System;
using System.Collections.Generic;
using Splitwise.Interfaces;
using Splitwise.Models;

namespace Splitwise.Managers
{
    public class ExpenseManager : IExpenseManager
    {
        public ExpenseManager(IUserManager userManager)
        {
            this.Expenses = new List<Expense>();
            this.UserBalanceSheet = new Dictionary<int, Dictionary<int, double>>();
            this.UserManager = userManager;
        }

        public IUserManager UserManager { get; private set; }
        public List<Expense> Expenses { get; private set; }
        public Dictionary<int, Dictionary<int, double>> UserBalanceSheet { get; private set; }

        public void AddExpense(Expense expense)
        {
            this.Expenses.Add(expense);
            expense.Splits.ForEach(split =>
            {
                if (!this.UserBalanceSheet.ContainsKey(expense.PaidByUser.Id))
                    this.UserBalanceSheet.Add(expense.PaidByUser.Id, new Dictionary<int, double>());

                if (!this.UserBalanceSheet.ContainsKey(split.PaidToUser.Id))
                    this.UserBalanceSheet.Add(split.PaidToUser.Id, new Dictionary<int, double>());

                if (!this.UserBalanceSheet[expense.PaidByUser.Id].ContainsKey(split.PaidToUser.Id))
                    this.UserBalanceSheet[expense.PaidByUser.Id].Add(split.PaidToUser.Id, 0.0);

                this.UserBalanceSheet[expense.PaidByUser.Id][split.PaidToUser.Id] += split.Amount;

                if (!this.UserBalanceSheet[split.PaidToUser.Id].ContainsKey(expense.PaidByUser.Id))
                    this.UserBalanceSheet[split.PaidToUser.Id].Add(expense.PaidByUser.Id, 0.0);

                this.UserBalanceSheet[split.PaidToUser.Id][expense.PaidByUser.Id] -= split.Amount;
            });
        }

        public void ShowBalance(int userId)
        {
            bool isEmpty = true;

            foreach (KeyValuePair<int, double> userBalance in this.UserBalanceSheet[userId])
            {
                if (userBalance.Value != 0)
                {
                    isEmpty = false;

                    string userName1 = this.UserManager.GetUserById(userBalance.Key).Name;
                    string userName2 = this.UserManager.GetUserById(userId).Name;

                    if (userBalance.Value > 0)
                        Console.WriteLine("{0} owes {1} INR {2}", userName1, userName2, userBalance.Value);
                    else
                        Console.WriteLine("{0} is owed by {1} INR {2}", userName2, userName1, Math.Abs(userBalance.Value));
                }
            }

            if (isEmpty)
                Console.WriteLine("No Balances");
        }

        public void ShowBalances()
        {
            bool isEmpty = true;

            foreach (KeyValuePair<int, Dictionary<int, double>> balanceSheet in this.UserBalanceSheet)
            {
                foreach (KeyValuePair<int, double> userBalance in balanceSheet.Value)
                {
                    if (userBalance.Value != 0)
                    {
                        isEmpty = false;

                        string userName1 = this.UserManager.GetUserById(userBalance.Key).Name;
                        string userName2 = this.UserManager.GetUserById(balanceSheet.Key).Name;

                        if (userBalance.Value > 0)
                            Console.WriteLine("{0} owes {1} INR {2}", userName1, userName2, userBalance.Value);
                        //else
                        //    Console.WriteLine("{0} owes {1} INR {2}", balanceSheet.Key, userBalance.Key, Math.Abs(userBalance.Value));
                    }
                }
            }

            if (isEmpty)
                Console.WriteLine("No Balances");
        }
    }
}
