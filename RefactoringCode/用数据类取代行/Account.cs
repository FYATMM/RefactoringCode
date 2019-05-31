using System.Security.Principal;

namespace DataBaseCmd2Class
{
   public class Account
   {
       private decimal balance;
        public string Number { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public decimal Balance
        {
            get => Blocked ? 0 : balance;
            set => balance = value;
        }
        public bool Blocked { get; set; }
    }
}
