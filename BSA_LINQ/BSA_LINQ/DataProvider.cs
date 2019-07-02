using BSA_LINQ.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace BSA_LINQ
{
    //TODO: Make a signleton?
    public class DataProvider
    {
        IEnumerable<User> users;
        IEnumerable<Project> projects;
        IEnumerable<Team> teams;
        IEnumerable<Task> tasks;
        IEnumerable<TaskStateModel> stateModels;

        const string route = "https://bsa2019.azurewebsites.net/api/";
        HttpClient http;

        public DataProvider()
        {
            users = new List<User>();
            projects = new List<Project>();
            teams = new List<Team>();
            tasks = new List<Task>();
            stateModels = new List<TaskStateModel>();
            http = new HttpClient();
        }

        public void FetchData()
        {
            var projectsResult = http.GetStringAsync($"{route}/projects");
            var usersResult = http.GetStringAsync($"{route}/users");
            var teamsResult = http.GetStringAsync($"{route}/teams");
            var statesResult = http.GetStringAsync($"{route}/taskstates");
            var tasksResult = http.GetStringAsync($"{route}/tasks");

            users = JsonConvert.DeserializeObject<IEnumerable<User>>(usersResult.Result);
            projects = JsonConvert.DeserializeObject<IEnumerable<Project>>(projectsResult.Result);
            teams = JsonConvert.DeserializeObject<IEnumerable<Team>>(teamsResult.Result);
            stateModels = JsonConvert.DeserializeObject<IEnumerable<TaskStateModel>>(statesResult.Result);
            tasks = JsonConvert.DeserializeObject<IEnumerable<Task>>(tasksResult.Result);
        }
    }
}
