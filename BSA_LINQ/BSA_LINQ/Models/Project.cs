using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models
{
    class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created_at { get; set; }
        public string Deadline { get; set; }
        public int Author_id { get; set; }
        public int Team_id { get; set; }

        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"Name: {Name},\n" +
                $"Description: {Description},\n" +
                $"Created: {Created_at},\n" +
                $"Deadline: {Deadline},\n" +
                $"Author_id: {Author_id},\n" +
                $"Team_id ID: {Team_id}";
        }
    }

    class NewProject
    {
        public IEnumerable<Task> Tasks { get; set; }
        public User Performer { get; set; }
        public User Author { get; set; }
        public Team Team { get; set; }
    }
}
