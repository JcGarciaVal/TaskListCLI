using System.Text.Json;
using TaskListCLI.Models;

namespace TaskListCLI.Repositories;

public class TaskRepository
{
    private readonly string _ruta =
        Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

    public List<TaskItem> SendTask()
    {
        if(!File.Exists(_ruta))
            return new List<TaskItem>();
        
        var json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<TaskItem>>(json) 
            ?? new List<TaskItem>();
    }

    public void SaveTask(List<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_ruta, json);
    }
}