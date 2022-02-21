using CleanetCode.TodoList.CLI.Operations;

namespace CleanetCode.TodoList.CLI
{
    public static class Menu
    {
        static bool isRunMain = true;
        public static bool isRun = true;
        public static void Run()
        {
            Console.WriteLine("Добро пожаловать в программу \"Список задач!\"\n ");

            
            while (isRunMain)
            {
                if (UserSession.sessions.Count != 0)
                {
                    Console.WriteLine("Выберите команду: " +
                           "\n1 - Перейти в меню задач" +
                           "\n2 - Выйти из профиля" +
                           "\n3 - Закончить работу с программой" +
                           "");
                }
                else
                {
                    Console.WriteLine("Выберите команду: " +
                           "\n1 - Создать нового пользователя" +
                           "\n2 - Авторизоваться" +
                           "\n3 - Закончить работу с программой" +
                           "");
                }

               

                isRun = true;

                ConsoleKey key = Console.ReadKey().Key;
                Console.Clear();


                switch (key)
                {
                    case ConsoleKey.D1:
                        if (UserSession.sessions.Count != 0)
                        {
                            Console.Clear();
                            ListMenu();
                        }
                        else
                        {
                            CreateNewUserOperation cnuo = new CreateNewUserOperation();
                            cnuo.Execute();
                            ListMenu();
                        }
                    break;

                    case ConsoleKey.D2:
                        if (UserSession.sessions.Count != 0)
                        {
                            UserSession.sessions.Remove(UserSession.sessions[0]);
                            Models.Task.tasks.RemoveRange(0, Models.Task.tasks.Count());
                            Console.Clear();
                        }
                        else 
                        {
                            LoginUserOperation luo = new LoginUserOperation();
                            luo.Execute();
                            ListMenu();
                        }
                    break;
                    
                    case ConsoleKey.D3:
                        isRunMain = false;
                       
                        break;

                    default:
                    break;
                }
            }
        }

        private static void ListMenu()
        {
            while (isRun)
            {
                Console.Clear();
                Console.WriteLine("Выберите команду: " +
                        "\n1 - Добавить заметку" +
                        "\n2 - Удалить заметку" +
                        "\n3 - Редактировать заметку" +
                        "\n4 - Показать все заметки" +
                        "\n5 - Отметить заметку как выполнена" +
                        "\n6 - Выйти в главное меню" +
                        "");
                ConsoleKey key2 = Console.ReadKey().Key;
                switch (key2)
                {
                    case ConsoleKey.D1:
                        CreateNewTaskOperation cnto = new CreateNewTaskOperation();
                        cnto.Execute();

                        break;
                    case ConsoleKey.D2:
                        DeleteTaskOperation dto = new DeleteTaskOperation();
                        dto.Execute();

                        break;
                    case ConsoleKey.D3:
                        UpdateTaskOperation uto = new UpdateTaskOperation();
                        uto.Execute();

                        break;
                    case ConsoleKey.D4:
                        ReadAllTaskOperation rato = new ReadAllTaskOperation();
                        rato.Execute();

                        break;
                    case ConsoleKey.D5:
                        CompleteTaskOperation cto = new CompleteTaskOperation();
                        cto.Execute();

                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        isRun = false;

                        break;

                    default:
                        break;
                }
            }
        }
    }
}

