

using System.Text.RegularExpressions;

namespace CleanetCode.TodoList.CLI.Operations
{
    public class LoginUserOperation : IOperation
    {
        public string Name { get; set; }

        public void Execute()
        {
            Console.Clear();
            bool isLogin = false;
            while (!isLogin)
            {
                Console.WriteLine("Введите ваш Email: (Для отмены ввода нажмите \"0\")");

                string email = Console.ReadLine();
                if (email == "0") 
                {
                    Console.Clear();
                    Menu.isRun = false;
                    break;
                }
                string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))";
                while (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Введите адрес правильно - (test@test.com)");
                    email = Console.ReadLine();
                }

                string path = Path.GetFullPath("DataFiles");
                path += @"\Users.txt";
                FileInfo fileinfo = new FileInfo(path);

                
                if (fileinfo.Exists)
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string? result;
                        while ((result = sr.ReadLine()) != null)
                        {

                            Regex regex = new Regex(@"user\.Email:\s\S+\s");
                            MatchCollection matches = regex.Matches(result);
                            if (matches.Count > 0)
                            {
                                foreach (Match match in matches)
                                {
                                    string value = match.Value;
                                    value = value.Substring(11).Trim();

                                    if (email == value)
                                    {
                                        Regex regexId = new Regex(@"user\.Id:\s\S+\s");
                                        MatchCollection matchesId = regexId.Matches(result);
                                        if (matchesId.Count > 0)
                                        {

                                            foreach (Match matchId in matchesId)
                                            {
                                                string valueId = matchId.Value;
                                                valueId = valueId.Substring(8).Trim();
                                                UserSession.sessions.Add(new Guid(valueId));
                                                Console.Clear();
                                                Console.WriteLine("Вы авторизованы!");
                                                isLogin = true;
                                                Console.WriteLine("Нажмите \"Enter\" чтобы продолжить...");
                                                Console.ReadLine();
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Пользователя с таким Email не существует! Попробуйте еще раз...");
                    }
                }

                if (isLogin)
                {
                    path = Path.GetFullPath("DataFiles");
                    path += $"\\Tasks_{UserSession.sessions[0]}.txt";
                    FileInfo fileinfoTasks = new FileInfo(path);
                    if (fileinfoTasks.Exists)
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            string? result;
                            while ((result = sr.ReadLine()) != null)
                            {

                                Regex regexId = new Regex(@"task\.Id:\s\S+\s");
                                Regex regexIsCompleted = new Regex(@"task\.IsCompleted:\s\S+\s");
                                Regex regexCreatedDate = new Regex(@"task\.CreatedDate:\s\S+\s\S+\s");
                                Regex regexUpdatedDate = new Regex(@"task\.UpdatedDate:\s\S+\s\S+\s");
                                Regex regexUserId = new Regex(@"task\.UserId:\s\S+\s");
                                Regex regexName = new Regex(@"task\.Name:[\s\S]+\| t");
                                Regex regexDescription = new Regex(@"task\.Description:[\s\S]+\|");

                                Models.Task task = new Models.Task();

                                MatchCollection matches = regexId.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(8).Trim();
                                        task.Id = new Guid(value);
                                    }
                                }
                                matches = regexIsCompleted.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(17).Trim();
                                        task.IsCompleted = Convert.ToBoolean(value);
                                    }
                                }
                                matches = regexCreatedDate.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(17).Trim();
                                        task.CreatedDate = Convert.ToDateTime(value);
                                    }
                                }
                                matches = regexUpdatedDate.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(17).Trim();
                                        task.UpdatedDate = Convert.ToDateTime(value);
                                    }
                                }
                                matches = regexUserId.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(12).Trim();
                                        task.UserId = new Guid(value);
                                    }
                                }
                                matches = regexName.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(10);
                                        value = value.Substring(0, value.Length -3).Trim();
                                        task.Name = value;
                                    }
                                }
                                matches = regexDescription.Matches(result);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        string value = match.Value;
                                        value = value.Substring(17);
                                        value = value.Substring(0, value.Length-1).Trim();
                                        task.Description = value;
                                    }
                                }
                                Models.Task.tasks.Add(task);
                            }
                        }
                    }
                }
            }
        }
    }
}
