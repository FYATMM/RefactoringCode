using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentAWheel
{
    class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Branch(int id, string  name)
        {
            Id = id;
            Name = name;
        }
    }
}
