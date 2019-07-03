using BSA_LINQ.Models;
using BSA_LINQ.Models.DTOmodels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace BSA_LINQ
{
    public class DataProvider
    {
        private static readonly Lazy<DataProvider> instance = new Lazy<DataProvider>(() => new DataProvider());

        IEnumerable<User> users;
        IEnumerable<Project> projects;
        IEnumerable<Team> teams;
        IEnumerable<Models.Task> tasks;
        IEnumerable<TaskStateModel> stateModels;

        public IEnumerable<ProjectDTO> projectDTOs;

        const string route = "https://bsa2019.azurewebsites.net/api/";
        HttpClient http;

        private DataProvider()
        {
            users = null;
            projects = null;
            teams = null;
            tasks = null;
            stateModels = null;
            http = new HttpClient();
        }

        public void LoadData()
        {
            users = GetUsers();
            projects = GetProjects();
            teams = GetTeams();
            tasks = GetTasks();
            stateModels = GetStateModels();

            //projectDTOs = from task in tasks
            //              group task by task.Project_id into t
            //              join project in projects on t.Key equals project.Id
            //              join author in users on project.Author_id equals author.Id
            //              join team in teams on project.Team_id equals team.Id
            //              select new ProjectDTO
            //              {
            //                  Id = project.Id,
            //                  Tasks = t,
            //                  Team = team,
            //                  Author = author
            //              };

            //projectDTOs = from project in projects
            //              join task in tasks on project.Id equals task.Project_id
            //              group task by task.Project_id into t
            //              join author in users on t.Author_id equals author.Id
            //              join team in teams on project.Team_id equals team.Id
            //              select new ProjectDTO
            //              {
            //                  Id = project.Id,
            //                  Tasks = t,
            //                  Team = team,
            //                  Author = author
            //              };

            //Console.WriteLine("ProjectDTOs count: " + projectDTOs.Count());
            //Console.WriteLine("Real projects count: " + projects.Count());
        }

        public static DataProvider GetInstance() => instance.Value;

        public IEnumerable<User> GetUsers()
        {
            if (users == null)
            {
                users = JsonConvert.DeserializeObject<IEnumerable<User>>(http.GetStringAsync($"{route}/users").Result);
            }
            return users;
        }

        public IEnumerable<Project> GetProjects()
        {
            if (projects == null)
            {
                projects = JsonConvert.DeserializeObject<IEnumerable<Project>>(http.GetStringAsync($"{route}/projects").Result);
            }
            return projects;
        }

        public IEnumerable<Team> GetTeams()
        {
            if (teams == null)
            {
                teams = JsonConvert.DeserializeObject<IEnumerable<Team>>(http.GetStringAsync($"{route}/teams").Result);
            }
            return teams;
        }

        public IEnumerable<Models.Task> GetTasks()
        {
            if (tasks == null)
            {
                tasks = JsonConvert.DeserializeObject<IEnumerable<Models.Task>>(http.GetStringAsync($"{route}/tasks").Result);
            }
            return tasks;
        }

        public IEnumerable<TaskStateModel> GetStateModels()
        {
            if (stateModels == null)
            {
                stateModels = JsonConvert.DeserializeObject<IEnumerable<TaskStateModel>>(http.GetStringAsync($"{route}/taskstates").Result);
            }
            return stateModels;
        }
    }
}
