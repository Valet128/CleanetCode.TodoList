namespace CleanetCode.TodoList.CLI.Operations
{
    public class DeleteTaskOperation : IOperation
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
                Console.WriteLine("Какую задачу необходимо удалить? Для отмены операции введите \"0\"");

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
                        Console.WriteLine("Пожалуйста, введите число соответсвующее удаляемой заметки!");
                        isNumber = false;
                    }
                    else
                    {
                        string path = Path.GetFullPath("DataFiles");
                        path += $"\\Tasks_{UserSession.sessions[0]}.txt";
                        FileInfo fileinfoTasks = new FileInfo(path);
                        if (fileinfoTasks.Exists)
                        {
                            IEnumerable<string> fileNew = File.ReadAllLines(path).Where(s => !s.Contains(Models.Task.tasks[keyInt - 1].Id.ToString()));
                            File.WriteAllLines(path, fileNew);

                        }
                        Models.Task.tasks.Remove(Models.Task.tasks[keyInt - 1]);
                        Console.Clear();
                        Console.WriteLine($"Задача {keyInt} успешно удалена!");
                        Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                        Console.ReadLine();

                        
                    }

                }
            }
        }
    }
}
