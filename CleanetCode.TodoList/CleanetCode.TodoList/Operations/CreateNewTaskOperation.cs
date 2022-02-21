using CleanetCode.TodoList.CLI.Models;

namespace CleanetCode.TodoList.CLI.Operations
{
    public class CreateNewTaskOperation : IOperation
    {
        public string Name { get; set; }

        public void Execute()
        {
            Console.Clear();
            Models.Task task = new Models.Task();

            task.Id = Guid.NewGuid();
            task.IsCompleted = false;
            task.CreatedDate = DateTime.Now;
            task.UpdatedDate = DateTime.Now;
            task.UserId = UserSession.sessions[0];

            Console.WriteLine("Добавление новой заметки...");
            Console.WriteLine();
            Console.Write("Название заметки: ");
            task.Name = Console.ReadLine();
            Console.Write("\nОписание заметки: ");
            task.Description = Console.ReadLine();

            string path = Path.GetFullPath("DataFiles");
            path += $"\\Tasks_{task.UserId}.txt";
            FileInfo fileinfo = new FileInfo(path);

            string taskInfo = "task.Id: " + task.Id + " | ";
            taskInfo += "task.IsCompleted: " + task.IsCompleted + " | ";
            taskInfo += "task.CreatedDate: " + task.CreatedDate + " | ";
            taskInfo += "task.UpdatedDate: " + task.UpdatedDate + " | ";
            taskInfo += "task.UserId: " + task.UserId + " | ";
            taskInfo += "task.Name: " + task.Name + " | ";
            taskInfo += "task.Description: " + task.Description + " | ";
            if (fileinfo.Exists)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(taskInfo);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine(taskInfo);
                }
            }

            Models.Task.tasks.Add(task);
            Console.Clear();
            Console.WriteLine("Заметка успешно создана!");
            Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
            Console.ReadLine();
        }
    }
}
