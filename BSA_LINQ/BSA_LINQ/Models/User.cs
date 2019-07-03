using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models
{
    public class User
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime Registered_at { get; set; }
        public int? Team_Id { get; set; }


        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"First_Name: {First_Name},\n" +
                $"Last_Name: {Last_Name},\n" +
                $"Email: {Email},\n" +
                $"Birthd_at: {Birthday.ToShortDateString()},\n" +
                $"Registered_at: {Registered_at.ToShortDateString()},\n" +
                $"Team_Id: {Team_Id}";
        }
    }
}
