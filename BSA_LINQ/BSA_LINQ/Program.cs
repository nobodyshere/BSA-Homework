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
        IEnumerable<Task> tasks = new List<Task>();
        IEnumerable<TaskStateModel> stateModels = new List<TaskStateModel>();

        string route = "https://bsa2019.azurewebsites.net/api/";
        HttpClient http = new HttpClient();

        List<NewProject> nprojects = new List<NewProject>();

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.TestJoin();

            Console.ReadLine();
        }

        public void TestJoin()
        {
            var projectsResult = http.GetStringAsync(route + "/projects");
            var usersResult = http.GetStringAsync(route + "/users");
            var teamsResult = http.GetStringAsync(route + "/teams");
            var statesResult = http.GetStringAsync(route + "/taskstates");
            var tasksResult = http.GetStringAsync(route + "/tasks");

            users = JsonConvert.DeserializeObject<IEnumerable<User>>(usersResult.Result);
            projects = JsonConvert.DeserializeObject<IEnumerable<Project>>(projectsResult.Result);
            teams = JsonConvert.DeserializeObject<IEnumerable<Team>>(teamsResult.Result);
            stateModels = JsonConvert.DeserializeObject<IEnumerable<TaskStateModel>>(statesResult.Result);
            tasks = JsonConvert.DeserializeObject<IEnumerable<Task>>(tasksResult.Result);

            var projectsQuerry = projects.Join(teams, p => p.Team_id, t => t.Id, (p, t) => new
            {
                Team = t,
                Project = p
            })
                .Join(tasks, p => p.Project.Id, x => x.Project_id, (t, x) => new
            {
                Team = t.Team,
                Project = t.Project,
                Tasks = x
            }).ToList();

            Console.WriteLine($"Real project count: {projects.Count()}");
            Console.WriteLine($"Project count after sorting: {projectsQuerry.Count}");

            //foreach (var item in projectsQuerry)
            //{
            //    Console.WriteLine($"{item.Team.Name}");
            //}

            //var querry = users.Join(teams, u => u.Team_Id, t => t.Id, (u, t) => new
            //{
            //    Name = $"{u.First_Name} {u.Last_Name}",
            //    Email = u.Email,
            //    Birthday = u.Birthday,
            //    Team = t.Name
            //});

            //foreach (var user in querry.Take(10))
            //{
            //    Console.WriteLine($"Name: {user.Name}\n" +
            //        $"Birthday: {user.Birthday}\n" +
            //        $"Team: {user.Team}\n" +
            //        $"Email: {user.Email}");
            //    Console.WriteLine(new string('-', 20));
            //}


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
            this.tasks = JsonConvert.DeserializeObject<IEnumerable<Task>>(tasks.Result);

            foreach (var item in projects)
            {
                nprojects.Add(new NewProject()
                {
                    Tasks = this.tasks.Where(x => x.Project_id == item.Id),
                    Performer = users.FirstOrDefault(x => x.Id == this.tasks.FirstOrDefault(y => y.Performer_id == x.Id).Performer_id),
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
