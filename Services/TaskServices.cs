using TaskListCLI.Enums;
using TaskListCLI.Models;
using TaskListCLI.Repositories;

namespace TaskListCLI.Services;

public class TaskServices
{
    private readonly ManejoRepository _data = new();

    public void PostTask(TaskClass task)
    {
        var list = _data.GetJsonData();

        if (
            string.IsNullOrWhiteSpace(task.Title) 
            || string.IsNullOrWhiteSpace(task.Description))
        {
            throw new Exception("Datos ingresados incorrectos");
        }
                

        int idLastTask = list.Any() ? list.Max(g=> g.Id) : 0;
        var newTask = new TaskClass
        {
            Id = idLastTask + 1,
            Title = task.Title,
            Description = task.Description,
            StateTask = State.Pendding
        };

        list.Add(newTask);

        _data.PostJsonData(list);
    }

    public List<TaskClass> GetTask()
    {
        return _data.GetJsonData();
    }
    
    public void SelectTaskComplete(int idComplete)
    {
        var list = _data.GetJsonData();
        int idLastTask = list.Any() ? list.Max(g=> g.Id) : 0;

        if(idComplete > idLastTask )
        {
            throw new Exception("ID invalid.");
        }

        var newComplete = list
            .Where(i=> i.StateTask == State.Pendding)
            .FirstOrDefault(i => i.Id == idComplete);

        if(newComplete is null)
        {
            throw new Exception("Task not exist.");
        }

        newComplete.StateTask = State.Complete;

        _data.PostJsonData(list);

    }
    
    //eliminar una tarea que no se necesite
    //poder ver las tareas pendientes y las tareas ya completadas
}