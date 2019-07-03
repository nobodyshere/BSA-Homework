using BSA_LINQ.Models;
using BSA_LINQ.Models.DTOmodels;
using System.Collections.Generic;
using System.Linq;

namespace BSA_LINQ
{
    class LinqService
    {
        DataProvider data = DataProvider.GetInstance();

        //1. Получить кол-во тасков у проекта конкретного пользователя (по id) (словарь, где ключом будет проект, а значением кол-во тасков).
        public Dictionary<Project, int> GetNumberOfUserTasks(int userId)
        {
            Dictionary<Project, int> result = data.GetTasks()
                                .GroupBy(x => x.Project_id)
                                .Join(data.GetProjects(), x => x.Key, y => y.Id, (x, y) => new
                                {
                                    Project = y,
                                    TaskCount = x.Count()
                                })
                                .Where(x => x.Project.Author_id == userId)
                                .ToDictionary(x => x.Project, y => y.TaskCount);

            return result;
        }

        //2. Получить список тасков, назначенных на конкретного пользователя (по id), где name таска < 45 символов (коллекция из тасков).
        public IEnumerable<Task> GetListOfUserTasks(int userId)
        {
            var result = data.GetTasks()
                .Where(x => x.Performer_id == userId)
                .Where(y => y.Name.Length < 45);

            return result;
        }

        //3. Получить список (id, name) из коллекции тасков, которые выполнены (finished) в текущем (2019) году для конкретного пользователя (по id).
        public Dictionary<int, string> GetListOfFinishedTasks(int userId)
        {
            Dictionary<int, string> result = data.GetTasks()
                .Where(x => x.Performer_id == userId
                    && x.State == 2
                    && x.Finished_at.Year == 2019)
                .ToDictionary(k => k.Id, v => v.Name);

            return result;
        }

        //4. Получить список (id, имя команды и список пользователей) из коллекции команд, участники которых старше 12 лет,
        //отсортированных по дате регистрации пользователя по убыванию, а также сгруппированных по командам.
        //P.S - в этом запросе допускается проверить только год рождения пользователя, без привязки к месяцу/дню/времени рождения.
        public IEnumerable<TaskWithOlderUsersDTO> GetListOfOlderUsers()
        {
            var result = data.GetUsers().Where(x => x.Birthday.Year > 2007)
                .OrderByDescending(x => x.Registered_at)
                .GroupBy(x => x.Team_Id)
                .Join(data.GetTeams(), x => x.Key, y => y.Id, (x, y) => new TaskWithOlderUsersDTO
                {
                    Id = y.Id,
                    Name = y.Name,
                    Users = x
                });

            return result;
        }


        //5. Получить список пользователей по алфавиту first_name (по возрастанию) с отсортированными tasks по длине name (по убыванию).
        public IEnumerable<UserTasksDTO> GetSortedUsers()
        {
            IEnumerable<UserTasksDTO> result = data.GetUsers().OrderBy(x => x.First_Name).Select((x) => new UserTasksDTO
            {
                User = x,
                Tasks = data.GetTasks()
                            .Where(y => y.Performer_id == x.Id)
                            .OrderByDescending(z => z.Name.Length)
            });

            return result;
        }

        //6.Получить следующую структуру (передать Id пользователя в параметры):
        //User
        //Последний проект пользователя(по дате создания)
        //Общее кол-во тасков под последним проектом
        //Общее кол-во незавершенных или отмененных тасков для пользователя
        //Самый долгий таск пользователя по дате(раньше всего создан - позже всего закончен)
        //P.S. - в данном случае, статус таска не имеет значения, фильтруем только по дате.
        public object GetStructure(int userId)
        {
            //Нашли
            //Все таски для юзера
            //Все проекты, в которых учавствует юзер юзера
            //Юзера

            //Осталось найти
            //Общее кол-во тасков под последним проектом
            //Общее кол-во незавершенных или отмененных тасков для пользователя
            //Самый долгий таск пользователя по дате(раньше всего создан - позже всего закончен)
            var result = data.GetTasks().Where(x => x.Performer_id == userId)
                                        .Join(data.GetProjects().GroupBy(o => o.Team_id), x => x.Project_id, y => y.Id, (x, y) => new
                                        {
                                            User = data.GetUsers().FirstOrDefault(x => x.Id == userId),
                                            TasksCountFromLastProject = y.
                                        });


            return null;
        }



        //7. Получить следующую структуру (передать Id проекта в параметры):
        //Проект
        //Самый длинный таск проекта(по описанию)
        //Самый короткий таск проекта(по имени)
        //Общее кол-во пользователей в команде проекта, где или описание проекта > 25 символов или кол-во тасков< 3
    }
}
