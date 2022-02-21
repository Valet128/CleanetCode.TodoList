
namespace CleanetCode.TodoList.CLI.Models
{

    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UserId { get; set; }
        public int Number { get; set; }

        public static List<Task> tasks = new List<Task>();
        
    }

}