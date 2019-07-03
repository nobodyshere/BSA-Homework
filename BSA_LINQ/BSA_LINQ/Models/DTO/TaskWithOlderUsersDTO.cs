using System.Collections.Generic;

namespace BSA_LINQ.Models.DTOmodels
{
    class TaskWithOlderUsersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
