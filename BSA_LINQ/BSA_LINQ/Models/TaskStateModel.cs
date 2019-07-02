using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models
{
    class TaskStateModel
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, \n" +
                $"Value: {Value}";
        }
    }
}
