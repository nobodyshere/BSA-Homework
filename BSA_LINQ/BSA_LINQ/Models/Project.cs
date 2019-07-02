using System.Collections.Generic;

namespace BSA_LINQ.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime Created_at { get; set; }
        public System.DateTime Deadline { get; set; }
        public int Author_id { get; set; }
        public int Team_id { get; set; }

        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"Name: {Name},\n" +
                $"Description: {Description},\n" +
                $"Created: {Created_at.ToShortDateString()},\n" +
                $"Deadline: {Deadline.ToShortDateString()},\n" +
                $"Author_id: {Author_id},\n" +
                $"Team_id ID: {Team_id}";
        }
    }
}
