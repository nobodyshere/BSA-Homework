using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models
{
    class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created_at { get; set; }
        public string Finished_at { get; set; }
        public int State { get; set; }
        public int Project_id { get; set; }
        public int Performer_id { get; set; }

        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"Name: {Name},\n" +
                $"Description: {Description},\n" +
                $"Created: {Created_at},\n" +
                $"Finished: {Finished_at},\n" +
                $"State: {State},\n" +
                $"Project ID: {Project_id},\n" +
                $"PerformerId: {Performer_id}";
        }
    }
}
