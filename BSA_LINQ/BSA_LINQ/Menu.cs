using System;

namespace BSA_LINQ
{
    class Menu
    {
        private bool run = true;
        DataProvider data = DataProvider.GetInstance();
        DataSortingService linqService;

        public Menu()
        {
            linqService = new DataSortingService();
        }

        public void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please, press any key to continue.");
            Console.ResetColor();

            while (run)
            {
                ShowMenu();
                GetInput();
            }
        }

        private void ShowMenu()
        {
            Console.ReadKey();
            Console.Clear();
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please choose an option below");
            Console.ForegroundColor = currentColor;

            Console.WriteLine("1. Download data from API (will speed up next requests)\n\n" +
                "2. Get the number of tasks for a specific user project (by id) \n\n" +
                "3. Get a list of tasks assigned to a specific user (by id), where name less than 45 characters \n\n" +
                "4. Get a list (id, name) from the collection of tasks that are completed (finished) in the current (2019) year for a specific user (by id) \n\n" +
                "5. Get a list (id, team name and list of users) from a collection of teams that are over 12 years old, sorted by user registration date in descending order, and also grouped by teams. \n\n" +
                "6. Get a list of users alphabetically first_name (ascending) with sorted tasks by name (descending) \n\n" +
                "7  Get the following structure: User, User’s last project (by creation date), Total number of tasks under the last project, Total number of incomplete or canceled tasks for a user, User’s longest task by date (first created, later completed)\n" +
                "8  Get the following structure: Project, The longest task of the project(by description), Total number of users in the project team, where either the project description is less than 25 characters or the number of tasks is less than 3\n\n" +
                "0 (Enter). Exit \n");
        }

        private void GetInput()
        {
            int.TryParse(Console.ReadLine(), out int responce);

            Console.Clear();
            switch (responce)
            {
                case 0:
                    run = false;
                    break;
                case 1:
                    DownloadData();
                    break;
                case 2:
                    GetNumberOfTasks();
                    break;
                case 3:
                    GetListOfTasks();
                    break;
                case 4:
                    GetListOfFinishedTasks();
                    break;
                case 5:
                    GetListOfOlderTeams();
                    break;
                case 6:
                    GetListOfSortedUsers();
                    break;
                case 7:
                    GetUserTasksInfo();
                    break;
                case 8:
                    GetTasksInfoForProject();
                    break;
                default:
                    DisplayError($"There are no option like {responce}.");
                    ShowMenu();
                    break;
            }
        }

        private void GetUserTasksInfo()
        {
            Console.WriteLine("Please, enter user id");
            string respoce = Console.ReadLine();

            int userID = Convert.ToInt32(respoce);
            var data = linqService.GetUserTasksData(userID);

            Console.WriteLine($"User ID: {data.user.Id}, Full Name: {data.user.First_Name} {data.user.Last_Name}\n" +
                $"Last Project ID: {data.lastProject.Id}, Name: {data.lastProject.Name}\n" +
                $"Tasks in last project: {data.lastProjectTasksCount}\n" +
                $"Unfinished tasks in last project: {data.unfinishedTasksCount}\n" +
                $"Longest task by date id: {data.longestTaskByDate.Id}, Created at: {data.longestTaskByDate.Created_at.ToShortDateString()}, Finished at: {data.longestTaskByDate.Finished_at.ToShortDateString()}");

        }

        private void GetTasksInfoForProject()
        {
            Console.WriteLine("Please, enter project id");
            string responce = Console.ReadLine();

            int projectID = Convert.ToInt32(responce);

            var (project, longestTask, shortestTask, usersCount) = linqService.GetTasksInfoForProject(projectID);

            if (project != null)
            {
                Console.WriteLine($"Project ID: {project.Id}, Project Name: {project.Name}");
                if (project != null)
                {
                    Console.WriteLine($"Longest task of project (by description): {longestTask.Id} \n" +
                        $"With description: {longestTask.Description}");

                    Console.WriteLine($"The shortest task of project (by name): {shortestTask.Id} \n" +
                        $"With name: {shortestTask.Name}");
                }
                else
                {
                    Console.WriteLine("There are no tasks in Project");
                }

                Console.WriteLine("Total number of users in the project team, where either the project description is less than 25 characters or the number of tasks is less than 3:");
                Console.WriteLine(usersCount);
            }
            else
            {
                DisplayError("Project not found");
            }
        }

        private void GetListOfSortedUsers()
        {
            var list = linqService.GetSortedUsers();

            foreach (var (user, tasks) in list)
            {
                Console.WriteLine($"User ID: {user.Id}, Full Name: {user.First_Name} {user.Last_Name}, Tasks list:");
                foreach (var x in tasks)
                {
                    Console.WriteLine($"{x.Id}, {x.Name}");
                }

                Console.WriteLine();
            }
        }

        private void GetListOfOlderTeams()
        {
            var teams = linqService.GetListOfOlderUsers();

            foreach (var (id, teamName, users) in teams)
            {
                Console.WriteLine($"Team ID: {id}, Team Name: {teamName}, List of users:");
                foreach (var x in users)
                {
                    Console.WriteLine($"ID: {x.Id}, Full name: {x.First_Name} {x.Last_Name}, Birth: {x.Birthday}");
                }

                Console.WriteLine();
            }
        }

        private void GetListOfFinishedTasks()
        {
            Console.WriteLine("Please, enter user id");
            string respoce = Console.ReadLine();

            int userID = Convert.ToInt32(respoce);
            var tasks = linqService.GetListOfFinishedTasks(userID);

            if (tasks.Count != 0)
            {
                Console.WriteLine($"In this year user with id {userID} has finish next tasks:");
                foreach (var item in tasks)
                {
                    Console.WriteLine($"Task ID: {item.Key}, Task Name: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine("This user dont have finished tasks in 2019");
            }

        }

        private void GetListOfTasks()
        {
            Console.WriteLine("Please, enter user id");
            string respoce = Console.ReadLine();

            int userID = Convert.ToInt32(respoce);
            var tasks = linqService.GetListOfUserTasks(userID);

            Console.WriteLine($"User with id {userID} have next assigned tasks:"); ;
            foreach (var item in tasks)
            {
                Console.WriteLine($"Task ID: {item.Id}, Name: {item.Name}");
            }
        }

        private void GetNumberOfTasks()
        {
            Console.WriteLine("Please, enter user id");
            string respoce = Console.ReadLine();

            int userID = Convert.ToInt32(respoce);
            var dictionary = linqService.GetNumberOfUserTasks(userID);

            if (dictionary.Count > 0)
            {
                Console.WriteLine($"User with id {userID} have next projects with tasks:");
                foreach (var item in dictionary)
                {
                    Console.WriteLine($"Project ID: {item.Key.Id} with {item.Value} tasks");
                }
            }
            else
            {
                Console.WriteLine($"User with id {userID} dont have any projects with tasks");
            }
        }

        private void DownloadData()
        {
            data.LoadData();
            Console.WriteLine("Data was updated.");
        }

        private void DisplayError(string message)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"ERROR: ");
            Console.ForegroundColor = current;
            Console.WriteLine(message);
        }
    }
}
