using System.IO;
using System.Text.RegularExpressions;
using CleanetCode.TodoList.CLI.Models;
namespace CleanetCode.TodoList.CLI.Operations
{
    public class CreateNewUserOperation : IOperation
    {
        public string Name { get; set; }

        public void Execute()
        {
            Guid id = Guid.NewGuid();
            Console.WriteLine("Введите ваш email чтобы зарегистрироваться: ");
            string email = Console.ReadLine();
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))";
            while (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine("Введите адрес правильно - (test@test.com)");
                email = Console.ReadLine();
            }
                User user = new User(id, email);



                string path = Path.GetFullPath("DataFiles");
                path += @"\Users.txt";
                FileInfo fileinfo = new FileInfo(path);

                string userInfo = "user.Id: " + user.Id + " | ";
                userInfo += "user.Email: " + user.Email + " | ";
                if (fileinfo.Exists)
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(userInfo);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        sw.WriteLine(userInfo);
                    }
                }



                User.users.Add(user);

                UserSession.sessions.Add(user.Id);

                Console.Clear();
                Console.WriteLine($"Поздравляем, новый пользователь создан! \nВаш Id: {user.Id} \nВаш Email: {user.Email}");
                Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                Console.ReadLine();
            
           
                
            
        }
    }
}
