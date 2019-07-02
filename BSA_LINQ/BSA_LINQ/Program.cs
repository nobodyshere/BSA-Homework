using BSA_LINQ.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace BSA_LINQ
{
    //TODO: change names
    class Program
    {
        IEnumerable<User> users = new List<User>();
        IEnumerable<Project> projects = new List<Project>();
        IEnumerable<Team> teams = new List<Team>();
        IEnumerable<Task> tsks = new List<Task>();
        IEnumerable<TaskStateModel> stateModels = new List<TaskStateModel>();
        string route = "https://bsa2019.azurewebsites.net/api/";
        HttpClient http = new HttpClient();

        List<NewProject> nprojects = new List<NewProject>();

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.FetchData();

            Console.ReadLine();
        }

        public void FetchData()
        {
            var proj = http.GetStringAsync(route + "/projects");
            var usrs = http.GetStringAsync(route + "/users");
            var tms = http.GetStringAsync(route + "/teams");
            var models = http.GetStringAsync(route + "/taskstates");
            var tasks = http.GetStringAsync(route + "/tasks");

            users = JsonConvert.DeserializeObject<IEnumerable<User>>(usrs.Result);
            projects = JsonConvert.DeserializeObject<IEnumerable<Project>>(proj.Result);
            teams = JsonConvert.DeserializeObject<IEnumerable<Team>>(tms.Result);
            stateModels = JsonConvert.DeserializeObject<IEnumerable<TaskStateModel>>(models.Result);
            tsks = JsonConvert.DeserializeObject<IEnumerable<Task>>(tasks.Result);

            foreach (var item in projects)
            {
                nprojects.Add(new NewProject()
                {
                    Tasks = tsks.Where(x => x.Project_id == item.Id),
                    Performer = users.FirstOrDefault(x => x.Id == tsks.FirstOrDefault(y => y.Performer_id == x.Id).Performer_id),
                    Team = teams.FirstOrDefault(x => x.Id == item.Id),
                    Author = users.FirstOrDefault(x => x.Id == item.Author_id)
                });
            }
        }

        //public Dictionary<Project, int> GetNumberOfTasks(int userId)
        //{

        //}
    }
}
