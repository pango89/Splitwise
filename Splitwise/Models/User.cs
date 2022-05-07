namespace Splitwise.Models
{
    public class User
    {
        public User(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            //this.Email = email;
            //this.Phone = phone;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        //public string Email { get; private set; }
        //public string Phone { get; private set; }
    }
}
