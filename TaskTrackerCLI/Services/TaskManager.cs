using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Services
{
    internal class TaskManager
    {
        public TaskManager()
        {

        }

        public void addTask()
        {
            TaskItem taskItem = new TaskItem();
            Console.WriteLine("Description of your task:");
            taskItem.Description = Console.ReadLine();
            Console.WriteLine(taskItem.Description);
            Console.WriteLine("Status of your task:");
            var values = Enum.GetValues(typeof(Status));
            int index = 1;
            foreach (var value in values)
            {
                ;
                Console.WriteLine($"{index}. {value}");
                index++;
            }
            //Console.WriteLine("Valorrrr:" + values.GetValue(0));

            //Console.WriteLine("Select one(write only the number):");
            //var opcion = Convert.ToInt32(Console.ReadLine());
            //opcion = opcion - 1;
            //Console.WriteLine("lugar del enum: "+ opcion);

            //if (opcion == 1)
            //{
            //    taskItem.Status = Status.InProgress;
            //}
            //if (opcion == 2)
            //{
            //    taskItem.Status = Status.Done;

            //}
            //else
            //{
            //    taskItem.Status = Status.ToDo;
            //}
            Console.Write("Select one (write only the number): ");
            if (int.TryParse(Console.ReadLine(), out int opcion) &&
                opcion >= 1 && opcion <= values.Length)
            {
                taskItem.Status = (Status)values.GetValue(opcion - 1);
            }
            else
            {
                Console.WriteLine("Opción inválida. Se asignará 'ToDo' por defecto.");
                taskItem.Status = Status.ToDo;
            }

            Console.WriteLine("Estado de la tarea: " + taskItem.Status);

            taskItem.CreatedAt = DateTime.Now;
            //Console.WriteLine("Tiempo"+ taskItem.CreatedAt);
            SaveTaskInJson(taskItem);

        }

        public void SaveTaskInJson(TaskItem task)
        {
            string filePath = "tasks.json";

            List<TaskItem> tasks;
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            else
            {
                tasks = new List<TaskItem>();
            }
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

            tasks.Add(task);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine("Tarea guardada con éxito.");

        }

      

    }
    
}
