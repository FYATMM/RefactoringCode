using System.Security.Principal;

namespace DataBaseCmd2Class
{
   public class Account
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public bool Blocked { get; set; }

    }
}
