using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models.DTOmodels
{
    class UserTasksDTO
    {
        public User User { get; set; }
        public IEnumerable<Task> Tasks { get; set; }

        public override string ToString()
        {
            return $"{User.First_Name} {User.Last_Name}: Tasks: {Tasks.GetType()}";
        }
    }
}
