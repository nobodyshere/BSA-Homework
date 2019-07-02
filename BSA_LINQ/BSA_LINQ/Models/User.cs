using System;
using System.Collections.Generic;
using System.Text;

namespace BSA_LINQ.Models
{
    class User
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Birthd_at { get; set; }
        public string Registered_at { get; set; }
        public int? Team_Id { get; set; }


        public override string ToString()
        {
            return $"ID: {Id},\n" +
                $"First_Name: {First_Name},\n" +
                $"Last_Name: {Last_Name},\n" +
                $"Email: {Email},\n" +
                $"Birthd_at: {Birthd_at},\n" +
                $"Registered_at: {Registered_at},\n" +
                $"Team_Id: {Team_Id}";
        }
    }
}
