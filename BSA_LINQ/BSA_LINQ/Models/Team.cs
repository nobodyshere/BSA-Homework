using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models
{
    class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Created_at { get; set; }

        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"Name: {Name},\n" +
                $"Created: {Created_at}";
        }
    }
}
