using TaskListCLI.Models;
using TaskListCLI.Services;

TaskServices _services = new();
bool sistema = true;
int option;
Console.WriteLine("Welcome to Task List!");

while (sistema)
{
    Console.WriteLine("Select to options:");
    Console.WriteLine("\t1. Create new task");
    Console.WriteLine("\t2. View tasks");
    Console.WriteLine("\t3. Compelete task");

    Console.Write("Your select: ");
    while
    (
        !int.TryParse(Console.ReadLine(), out option) 
        || option > 5 
        || option < 0
    )
    {
        Console.WriteLine("[!] Respuesta incorrecta. Your Select: ");  
        Console.Write("Your select: ");
    }

    switch (option)
    {
        case 1:
            Console.WriteLine("Create new Task!");
            Console.Write("\tTitle: ");
            string? title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(title)) {
                Console.WriteLine("\t[!] Title invalid.");
                Console.Write("\tTitle: ");
                title = Console.ReadLine();
            };

            Console.Write("\tDescription: ");
            string? description = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(description)) {
                Console.WriteLine("\t[!] Description invalid.");
                Console.Write("\tDescription: ");
                description = Console.ReadLine();
            };

            var newTask = new TaskClass
            {
              Title = title,
              Description = description
            };
            try
            {
                _services.PostTask(newTask);
                Console.WriteLine("Task create!\n");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        break;

        case 2:
            var list = _services.GetTask();
            Console.WriteLine("\n---------------------------  List Task  ---------------------------\n");
            foreach(var task in list)
            {
                Console.WriteLine($"{task.Id, 3} | {task.Title,-15} | {task.Description, -30} | {task.StateTask, 10}");
            }
            Console.WriteLine("\n------------------------------------------------------------------\n");
        break;

        case 3:
            var listPendding = _services.GetTask();
            Console.WriteLine("\n---------------------------  List Task  ---------------------------\n");
            foreach(var task in listPendding)
            {
                Console.WriteLine($"{task.Id, 3} | {task.Title,-15} | {task.Description, -30} | {task.StateTask, 10}");
            }
            Console.WriteLine("\n------------------------------------------------------------------");
            Console.Write("\nSelect id task complete: ");
            int idComplete;
            while(!int.TryParse(Console.ReadLine(), out idComplete) || idComplete < 0)
            {
                Console.WriteLine("[!] ID invalid!.");
                Console.Write("Select id task complete: ");
            }
            try
            {
                _services.SelectTaskComplete(idComplete);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        break;
    }

}