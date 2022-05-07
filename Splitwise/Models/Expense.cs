using System;
using System.Collections.Generic;

namespace Splitwise.Models
{
    public class Expense
    {
        public Expense(string title, DateTime date, User paidByUser, List<Split> splits)
        {
            //this.Id = id;
            this.Title = title;
            this.Date = date;
            this.PaidByUser = paidByUser;
            this.Splits = splits;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public User PaidByUser { get; private set; }
        public List<Split> Splits { get; private set; }
    }
}
