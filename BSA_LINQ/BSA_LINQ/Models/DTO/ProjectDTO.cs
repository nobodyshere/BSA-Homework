using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models.DTOmodels
{
    public class ProjectDTO
    {
        public int? Id { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        public User Author { get; set; }
        public Team Team { get; set; }
    }

}
