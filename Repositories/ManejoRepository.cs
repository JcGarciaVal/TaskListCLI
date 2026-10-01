using System.Text.Json;
using TaskListCLI.Models;

namespace TaskListCLI.Repositories;

public class ManejoRepository
{
    private readonly string _ruta =
        Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

    public List<TaskClass> GetJsonData()
    {
        if(!File.Exists(_ruta))
            return new List<TaskClass>();
        
        var json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<TaskClass>>(json) 
            ?? new List<TaskClass>();
    }

    public void PostJsonData(List<TaskClass> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_ruta, json);
    }
}