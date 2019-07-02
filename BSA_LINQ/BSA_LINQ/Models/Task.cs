namespace BSA_LINQ.Models
{
    class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime Created_at { get; set; }
        public System.DateTime Finished_at { get; set; }
        public int State { get; set; }
        public int Project_id { get; set; }
        public int Performer_id { get; set; }

        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"Name: {Name},\n" +
                $"Description: {Description},\n" +
                $"Created: {Created_at.ToShortDateString()},\n" +
                $"Finished: {Finished_at.ToShortDateString()},\n" +
                $"State: {State},\n" +
                $"Project ID: {Project_id},\n" +
                $"PerformerId: {Performer_id}";
        }
    }
}
