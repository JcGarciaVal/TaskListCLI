using System.Data.Common;
using TaskListCLI.Enums;
using TaskListCLI.Models;
using TaskListCLI.Repositories;

namespace TaskListCLI.Services;

public class TaskServices
{
    private readonly TaskRepository _data = new();

    public void PostTask(TaskItem task)
    {
        var list = _data.SendTask();

        if (
            string.IsNullOrWhiteSpace(task.Title) 
            || string.IsNullOrWhiteSpace(task.Description))
        {
            throw new Exception("Datos ingresados incorrectos");
        }
                
        int idLastTask = list.Any() ? list.Max(g=> g.Id) : 0;

        var newTask = new TaskItem
        {
            Id = idLastTask + 1,
            Title = task.Title,
            Description = task.Description,
            StateTask = State.Pending
        };

        list.Add(newTask);

        _data.SaveTask(list);
    }

    public List<TaskItem> GetTask()
    {
        return _data.SendTask();
    }
    
    public void SelectTaskComplete(int idComplete)
    {
        var list = _data.SendTask();

        var task = list.FirstOrDefault(t => t.Id == idComplete);

        if (task is null)
        {
            throw new InvalidOperationException("Task not found.");
        }

        task.StateTask = State.Completed;

        _data.SaveTask(list);

    }

    public void GetAllGroupState()
    {
        var list = _data.SendTask();
        
        var groupTask = list
            .GroupBy(t => t.StateTask)
            .Select(s => new
            {
               StateColl = s.Key,
               TaskL = s.Select(t => new
               {
                   CollTitle = t.Title,
                   CollDescription = t.Description
               })
            });

            foreach(var group in groupTask)
        {
            Console.WriteLine("\nState: " + group.StateColl);
            foreach(var t in group.TaskL)
            {
                Console.WriteLine("\tTitle: " + t.CollTitle);
                Console.WriteLine("\tDescription: " + t.CollDescription + "\n");
            }
        }
    }

    public void DeleteTask(int idDelete)
    {
        var list = _data.SendTask();
        var task = list.FirstOrDefault(t => t.Id == idDelete);

        if (task is null)
        {
            throw new InvalidOperationException("Task not found.");
        }

        list.Remove(task);

        _data.SaveTask(list);
    }
}