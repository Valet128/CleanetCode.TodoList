namespace CleanetCode.TodoList.CLI.Operations
{
    public class UpdateTaskOperation : IOperation
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
                Console.WriteLine("Какую задачу необходимо отредактировать? Для отмены операции введите \"0\"");

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
                        Console.WriteLine("Пожалуйста, введите число соответсвующее редактируемой заметки!");
                        isNumber = false;
                    }
                    else
                    {
                    

                        Console.WriteLine("Редактирование заметки...");
                        Console.WriteLine();

                        //Хотелось бы конечно отправлять данные которые уже есть в задаче на консоль,
                        //и там уже убирать что не нужно и оставлять что нужно...
                        //но быстрого решения не нашел, поэтому при редактировании нужно будет заново все вводить - название и описание.
                        //как вариант копировать в буфер и потом через "up" находить нужный текст

                        Console.Write("Название заметки: ");
                        Models.Task.tasks[keyInt - 1].Name = Console.ReadLine();
                        Console.Write("\nОписание заметки: ");
                        Models.Task.tasks[keyInt - 1].Description = Console.ReadLine();

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
                        Console.WriteLine($"Задача {keyInt} успешно отредактирована!");
                        Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                        Console.ReadLine();
                    }

                }
            }
            
        }
    

    }
}
