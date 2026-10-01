using TaskListCLI.Enums;
namespace TaskListCLI.Models;

public class TaskItem
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public State StateTask {get; set;}

}