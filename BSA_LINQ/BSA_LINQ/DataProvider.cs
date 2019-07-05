using BSA_LINQ.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BSA_LINQ
{
    public class DataProvider
    {
        private static readonly Lazy<DataProvider> Instance = new Lazy<DataProvider>(() => new DataProvider());

        IEnumerable<User> users;
        IEnumerable<Project> projects;
        IEnumerable<Team> teams;
        IEnumerable<Task> tasks;

        const string route = "https://bsa2019.azurewebsites.net/api/";
        HttpClient http;

        private DataProvider()
        {
            users = null;
            projects = null;
            teams = null;
            tasks = null;
            http = new HttpClient();
        }

        public void GetDataFromServer()
        {
            users = GetUsers();
            projects = GetProjects();
            teams = GetTeams();
            tasks = GetTasks();
        }

        public static DataProvider GetInstance() => Instance.Value;

        public IEnumerable<User> GetUsers()
        {
            return users ?? (users =
                       JsonConvert.DeserializeObject<IEnumerable<User>>(http.GetStringAsync($"{route}/users").Result));
        }

        public IEnumerable<Project> GetProjects()
        {
            return projects ?? (projects =
                       JsonConvert.DeserializeObject<IEnumerable<Project>>(http.GetStringAsync($"{route}/projects").Result));
        }

        public IEnumerable<Team> GetTeams()
        {
            return teams ?? (teams =
                       JsonConvert.DeserializeObject<IEnumerable<Team>>(http.GetStringAsync($"{route}/teams").Result));
        }

        public IEnumerable<Models.Task> GetTasks()
        {
            return tasks ?? (tasks =
                       JsonConvert.DeserializeObject<IEnumerable<Models.Task>>(http.GetStringAsync($"{route}/tasks").Result));
        }
    }
}
