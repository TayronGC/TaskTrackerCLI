using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

class Program
{
    static void Main(string[] args)
    {
        /*
        while (true)
        {
            string? op = Console.ReadLine();
            string[] ops = op.Split('"');
            Console.WriteLine(ops);
            foreach (var item in ops)
            {
                Console.WriteLine(item);
            }
        }
        */

        
        TaskManager taskManager = new TaskManager();
        while (true)
        {
            string? op = Console.ReadLine();
            var matches = Regex.Matches(op, @"[\""].+?[\""]|\S+");
            //string[] ops = op.Split(" ");
            
            var inputs = matches
    .Select(m => m.Value.Trim('"')) // Quita las comillas de los argumentos entrecomillados
    .ToArray();

            /*
            Console.WriteLine(inputs);
            foreach (var item in inputs)
            {
                Console.WriteLine(item);
            }
        }
            */
            try
            {
                switch (inputs[0])
                {
                    case "add":
                        //taskManager.addTask();
                        taskManager.AddTask2(inputs[1]);
                        //Console.WriteLine("Task added successfully (ID: 1)");
                        break;
                    case "list":
                        if(inputs.Length == 2)
                        {
                            if (inputs[1] == "done")
                            {
                                taskManager.ListByStatus(Status.Done);
                            }
                            else if (inputs[1] == "in-progress")
                            {
                                taskManager.ListByStatus(Status.InProgress);
                            }
                            else if (inputs[1] == "todo")
                            {
                                taskManager.ListByStatus(Status.ToDo);
                            }
                        }
                        else if(inputs.Length == 1)
                        {
                            taskManager.readJson();
                        }
                        
                        break;

                    case "update":
                        {
                            int id = Convert.ToUInt16(inputs[1]);
                            taskManager.UpdateTask(id, inputs[2]);
                            break;
                        }
                    case "delete":
                        {
                            int id = Convert.ToUInt16(inputs[1]);
                            taskManager.DeleteTask(id);
                            break;
                        }
                        
                    case "mark-in-progress":
                        {
                            int id = Convert.ToUInt16(inputs[1]);
                            Status status = Status.InProgress;
                            taskManager.Marking(id, status);
                            break;
                        }

                    case "mark-done":
                        {
                            int id = Convert.ToUInt16(inputs[1]);
                            Status status = Status.Done;
                            taskManager.Marking(id, status);
                            break;
                        }
                    //case "list done"://Mal
                    //    taskManager.ListByStatus(Status.Done);
                    //    break;
                    case "help":
                        Console.WriteLine("add \"task\"");
                        Console.WriteLine("update Id \"task\"");
                        Console.WriteLine("delete Id");
                        Console.WriteLine("mark-in-progress Id");
                        Console.WriteLine("mark-done Id");
                        Console.WriteLine("list");
                        Console.WriteLine("list done\nlist todo\nlist in-progress");
                        break;
                    default:
                        Console.WriteLine("Unknown command, type \"help\" to see the commands");
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please type something, also type \"help\" to see the commands");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.ToString());
            }
            
            //taskManager.addTask();
            //taskManager.readJson();
        }

        
        /*
        TaskItem task = new TaskItem
        {
            Id = 1,
            Description = "Clean bedroom",
            Status = Status.InProgress
        };
        List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Id = 1,
                Description = "Clean bedroom",
                Status = Status.InProgress
            },
            new TaskItem
            {
                Id = 2,
                Description = "Lavar los platos",
                Status = Status.ToDo
            },
            new TaskItem
            {
                Id = 3,
                Description = "Responder correos",
                Status = Status.ToDo
            }
        };

        string fileName2 = "Prueba2.json";
        string jsonString2 = JsonSerializer.Serialize(tasks);
        //Console.WriteLine(jsonString2);
        File.WriteAllText(fileName2, jsonString2);
        Console.WriteLine(File.ReadAllText(fileName2));

        List<TaskItem> taskItemsss = JsonSerializer.Deserialize<List<TaskItem>>(jsonString2)!;
        Console.WriteLine(taskItemsss);
        foreach (TaskItem item in taskItemsss)
        {
            Console.WriteLine(item.Id);
            Console.WriteLine(item.Description);
            Console.WriteLine(item.Status);
            Console.WriteLine(item.CreatedAt);
            Console.WriteLine(item.UpdatedAt);
        }
        
        //Nombre del archivo
        string fileName = "Pruba.json";

        //Serialización
        string jsonString = JsonSerializer.Serialize(task);
        Console.WriteLine(jsonString);
        Console.WriteLine("");
        //Console.WriteLine(task);

        //Crear el archivo json
        File.WriteAllText(fileName, jsonString);

        Console.WriteLine(File.ReadAllText(fileName));

        var options = new JsonSerializerOptions { WriteIndented = true };

        string json = JsonSerializer.Serialize(task, options);
        File.WriteAllText("tasks.json", json);
         Console.WriteLine(json);

        TaskItem? taskss = JsonSerializer.Deserialize<TaskItem>(json);
        Console.WriteLine($"Id: {taskss?.Id}");
        Console.WriteLine($"Descripcion: {taskss?.Description}");
        Console.WriteLine($"Estado: {taskss?.Status}");
        Console.WriteLine($"Creado: {taskss?.CreatedAt}");

        //string json2 = File.ReadAllText(fileName);
        //List<TaskItem> tasks = JsonSerializer.Deserialize<List<TaskItem>>(json2);
        //Console.WriteLine(tasks);




        TaskManager taskManager = new TaskManager();
        taskManager.addTask();
        */
    }
}