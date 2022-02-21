namespace CleanetCode.TodoList.CLI.Operations
{
    public class CompleteTaskOperation : IOperation
    {
        public string Name { get; set; }

        public void Execute()
        {
            if (Models.Task.tasks.Count() == 0)
            {
                Console.Clear();
                Console.WriteLine("Задачи отсутствуют!\n");
                Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                Console.ReadLine();
            }
            else
            {
                int countNumber = 0;
                Console.Clear();
                Console.WriteLine("Какую задачу необходимо отметить выполненной? Для отмены операции введите \"0\"");

                foreach (Models.Task task in Models.Task.tasks)
                {
                    task.Number = ++countNumber;
                    Console.WriteLine($"{task.Name} - номер задачи: {task.Number}");

                }
                bool isNumber = false;


                while (!isNumber)
                {
                    string key = Console.ReadLine();

                    if (key == "0")
                    {
                        Console.Clear();
                        break;
                    }

                    int keyInt;
                    isNumber = int.TryParse(key, out keyInt);
                    if (!isNumber || keyInt < 1 || keyInt > countNumber)
                    {
                        Console.WriteLine("Пожалуйста, введите число соответсвующее заметке!");
                        isNumber = false;
                    }
                    else
                    {

                        Models.Task.tasks[keyInt - 1].UpdatedDate = DateTime.Now;
                        Models.Task.tasks[keyInt - 1].IsCompleted = true;

                        string taskInfo = "task.Id: " + Models.Task.tasks[keyInt - 1].Id + " | ";
                        taskInfo += "task.IsCompleted: " + Models.Task.tasks[keyInt - 1].IsCompleted + " | ";
                        taskInfo += "task.CreatedDate: " + Models.Task.tasks[keyInt - 1].CreatedDate + " | ";
                        taskInfo += "task.UpdatedDate: " + Models.Task.tasks[keyInt - 1].UpdatedDate + " | ";
                        taskInfo += "task.UserId: " + Models.Task.tasks[keyInt - 1].UserId + " | ";
                        taskInfo += "task.Name: " + Models.Task.tasks[keyInt - 1].Name + " | ";
                        taskInfo += "task.Description: " + Models.Task.tasks[keyInt - 1].Description + " | ";

                        string path = Path.GetFullPath("DataFiles");
                        path += $"\\Tasks_{UserSession.sessions[0]}.txt";
                        FileInfo fileinfoTasks = new FileInfo(path);
                        if (fileinfoTasks.Exists)
                        {
                            IEnumerable<string> fileNew = File.ReadAllLines(path).Where(s => !s.Contains(Models.Task.tasks[keyInt - 1].Id.ToString()));
                            File.WriteAllLines(path, fileNew);


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


                        Console.Clear();
                        Console.WriteLine($"Задача {keyInt} выполнена!");
                        Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                        Console.ReadLine();
                    }

                }
            }
        }
    
    
    }
}
