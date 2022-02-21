
namespace CleanetCode.TodoList.CLI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public static List<User> users = new List<User>();
        public User(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}