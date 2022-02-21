namespace CleanetCode.TodoList.CLI.Operations
{
    public class ReadAllTaskOperation : IOperation
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
                Console.Clear();
                Console.WriteLine("Ваши задачи: ");
                Console.WriteLine();
                foreach (Models.Task task in Models.Task.tasks)
                {
                    Console.WriteLine($"ID: {task.Id} " +

                        $"\nДата создания: {task.CreatedDate} " +
                        $"\nДата обновления: {task.UpdatedDate} " +
                        $"\nID пользователя: {task.UserId} " +
                        $"\nНазвание задачи: {task.Name} " +
                        $"\nОписание задачи: {task.Description}");
                    if (task.IsCompleted)
                    {
                        Console.WriteLine($"Статус: Задача выполнена!\n");
                    }
                    else
                    {
                        Console.WriteLine($"Статус: Задача в процессе выполнения!\n");
                    }
                }
                Console.WriteLine("Все задачи выведены!\n");
                Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                Console.ReadLine();
            }
            

        }
    

    }
}
