using BSA_LINQ.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace BSA_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string route = "https://bsa2019.azurewebsites.net/api/";
            HttpClient http = new HttpClient();
            //List<User> tasks = new List<User>();
            //try
            //{
            //    var responce = http.GetStringAsync(route + "/users");
            //    tasks = JsonConvert.DeserializeObject<List<User>>(responce.Result).Where(x => x.Id < 10).ToList();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //foreach (var item in tasks)
            //{
            //    Console.WriteLine(item);
            //    Console.WriteLine();
            //}

            List<NewProject> projects = new List<NewProject>();
            try
            {
                var proj = http.GetStringAsync(route + "/projects");
                var users = http.GetStringAsync(route + "/users");
                var teams = http.GetStringAsync(route + "/team");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        //public Dictionary<Project, int> GetNumberOfTasks(int userId)
        //{

        //}
    }
}
