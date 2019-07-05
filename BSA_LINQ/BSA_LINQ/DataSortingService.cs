using BSA_LINQ.Models;
using System.Collections.Generic;
using System.Linq;

namespace BSA_LINQ
{
    class DataSortingService
    {
        DataProvider data = DataProvider.GetInstance();

        //1. Получить кол-во тасков у проекта конкретного пользователя (по id) (словарь, где ключом будет проект, а значением кол-во тасков).
        public Dictionary<Project, int> GetNumberOfUserTasks(int userId)
        {
            return data.GetTasks()
                .GroupBy(x => x.Project_id)
                .Join(data.GetProjects(), x => x.Key, y => y.Id, (x, y) => new
                {
                    Project = y,
                    TaskCount = x.Count()
                })
                .Where(x => x.Project.Author_id == userId)
                .ToDictionary(x => x.Project, y => y.TaskCount);
        }

        //2. Получить список тасков, назначенных на конкретного пользователя (по id), где name таска < 45 символов (коллекция из тасков).
        public IEnumerable<Task> GetListOfUserTasks(int userId)
        {
            return data.GetTasks()
                .Where(x => x.Performer_id == userId)
                .Where(y => y.Name.Length < 45);
        }

        //3. Получить список (id, name) из коллекции тасков, которые выполнены (finished) в текущем (2019) году для конкретного пользователя (по id).
        public Dictionary<int, string> GetListOfFinishedTasks(int userId)
        {
            return data.GetTasks()
                .Where(x => x.Performer_id == userId
                    && x.State == TaskState.Finished
                    && x.Finished_at.Year == 2019)
                .ToDictionary(k => k.Id, v => v.Name);
        }

        //4. Получить список (id, имя команды и список пользователей) из коллекции команд, участники которых старше 12 лет,
        //отсортированных по дате регистрации пользователя по убыванию, а также сгруппированных по командам.
        //P.S - в этом запросе допускается проверить только год рождения пользователя, без привязки к месяцу/дню/времени рождения.
        public IEnumerable<(int id, string teamName, List<User> users)> GetListOfOlderUsers()
        {
            return data.GetUsers().Where(x => x.Birthday.Year > 2007)
                .OrderByDescending(x => x.Registered_at)
                .GroupBy(x => x.Team_Id)
                .Join(data.GetTeams(), x => x.Key, y => y.Id, (x, y) =>
                (
                    id: y.Id,
                    teamName: y.Name,
                    users: x.ToList()
                ));
        }

        //5. Получить список пользователей по алфавиту first_name (по возрастанию) с отсортированными tasks по длине name (по убыванию).
        public IEnumerable<(User user, List<Task> tasks)> GetSortedUsers()
        {
            return data.GetUsers().OrderBy(x => x.First_Name).Select((x) => (
                user: x,
                tasks: data.GetTasks()
                            .Where(y => y.Performer_id == x.Id)
                            .OrderByDescending(z => z.Name.Length).ToList()
            ));
        }

        //6.Получить следующую структуру (передать Id пользователя в параметры):
        //User
        //Последний проект пользователя(по дате создания)
        //Общее кол-во тасков под последним проектом
        //Общее кол-во незавершенных или отмененных тасков для пользователя
        //Самый долгий таск пользователя по дате(раньше всего создан - позже всего закончен)
        //P.S. - в данном случае, статус таска не имеет значения, фильтруем только по дате.
        public (User user, Project lastProject, int lastProjectTasksCount, int unfinishedTasksCount, Task longestTaskByDate) GetUserTasksData(int userId)
        {
            var currentUser = data.GetUsers().FirstOrDefault(x => x.Id == userId);
            var lastProject = data.GetProjects()
                                  .Where(x => x.Author_id == userId)
                                  .OrderByDescending(x => x.Created_at)
                                  .FirstOrDefault();
            var tasksCount = data.GetTasks().GroupBy(x => x.Project_id)
                                  .FirstOrDefault(x => x.Key == lastProject.Id).Count();
            var notFinishedTask = data.GetTasks().Count(x => x.Project_id == lastProject.Id && x.State != TaskState.Finished);
            var longestTaskbyDate = data.GetTasks().Where(x => x.Performer_id == userId).OrderBy(x => x.Created_at)
                                         .ThenByDescending(x => x.Finished_at)
                                         .FirstOrDefault();

            return (currentUser, lastProject, tasksCount, notFinishedTask, longestTaskbyDate);
        }

        //7. Получить следующую структуру (передать Id проекта в параметры):
        //Проект
        //Самый длинный таск проекта(по описанию)
        //Самый короткий таск проекта(по имени)
        //Общее кол-во пользователей в команде проекта, где или описание проекта > 25 символов или кол-во тасков < 3
        public (Project project, Task longestTask, Task shortestTask, int usersCount) GetTasksInfoForProject(int projectId)
        {
            return (from project in data.GetProjects()
                    where project.Id == projectId
                    join task in data.GetTasks() on project.Id equals task.Project_id
                    into tasklist
                    let longestTask = (from task in data.GetTasks()
                                       where task.Project_id == projectId
                                       orderby task.Description descending
                                       select task).FirstOrDefault()
                    let shortestTask = (from task in data.GetTasks()
                                        where task.Project_id == projectId
                                        orderby task.Name
                                        select task).FirstOrDefault()
                    let users = from user in data.GetUsers()
                                where user.Team_Id == project.Team_id && (project.Description.Length > 25 || tasklist.Count() < 3)
                                select user
                    select (
                       project,
                       longestTask,
                       shortestTask,
                       users.Count()
                    )).FirstOrDefault();
        }
    }
}
