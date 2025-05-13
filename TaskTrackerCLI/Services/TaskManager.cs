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
        string fileName = "taskss.json";
        List<TaskItem> tasks = new List<TaskItem>();
        public TaskManager()
        {
            File.WriteAllText(fileName, "[]");
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
            SaveTaskInJson2(taskItem);

        }

        private void SaveTaskInJson(TaskItem task)
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

        private void SaveTaskInJson2(TaskItem task)
        {
            if (task == null)
            {
                Console.WriteLine("Tarea vacia");
            }
            else
            {
                tasks.Add(task);
                string jsonString = JsonSerializer.Serialize(tasks);
                File.WriteAllText(fileName, jsonString);
            }
            //Console.WriteLine(File.ReadAllText(fileName));
            //foreach (var item in tasks)
            //{
            //    Console.WriteLine(item.Id);
            //    Console.WriteLine(item.Description);
            //    Console.WriteLine(item.Status);
            //}
        }

        public void readJson()
        {
            //string jsonString = File.ReadAllText(fileName);
            ////List<TaskItem> taskItemsss = JsonSerializer.Deserialize<List<TaskItem>>(jsonString)!;
            //tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString)!;
            tasks = LoadTasks();
            //var taskss= LoadTasks();//Otra forma
            if (tasks.Count > 0)
            {
                foreach (TaskItem item in tasks)
                {
                    Console.WriteLine(item.Id);
                    Console.WriteLine(item.Description);
                    Console.WriteLine(item.Status);
                    Console.WriteLine(item.CreatedAt);
                    Console.WriteLine(item.UpdatedAt);
                    Console.WriteLine("-----");
                }
            }
            else
            {
                Console.WriteLine("There are no tasks");
            }
        }

        public void AddTask2(string description)
        {
            TaskItem taskItem = new TaskItem();
            taskItem.Description = description;
            //tasks.FindLastIndex(task => task.Id == taskItem.Id);
            //taskItem.Id = tasks.FindLastIndex(task => task.Description == description);
            if (tasks.Count > 0) // tambien usar tasks.Any()
            {
                taskItem.Id = tasks.Max(x => x.Id) +1;
            }
            else
            {
                taskItem.Id = 1;
            }

            /*
            if (tasks.Count > 0)
            {
                //tasks.FindLastIndex(task => task.Id == taskItem.Id + 1);
                //for (int i = 0; i < tasks.Count; i++)
                //{
                //    if (tasks[i].Id == tasks.Count)
                //    {

                //    }
                //}
                taskItem.Id = tasks.Count + 1;

                
            }
            else
            {
                taskItem.Id = 1;
            }
            //Console.WriteLine("ID: " + taskItem.Id);
            */
           tasks.Add(taskItem);
           upJson(tasks);
            //SaveTaskInJson2(taskItem);
            Console.WriteLine($"Task added successfully (ID: {taskItem.Id})");
        }

        public void UpdateTask(int id, string description)
        {
            if (id <= 0)
            {
                Console.WriteLine("Negative Id");
                return;
            }

            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Description = description;
                task.UpdatedAt = DateTime.Now;

                //string jsonString = JsonSerializer.Serialize(tasks);
                //File.WriteAllText(fileName, jsonString);
                upJson(tasks);

                Console.WriteLine($"Task updated successfully (ID: {task.Id})");
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }




        public void DeleteTask(int id)
        {
            if (id > 0)
            {
                var taskToDelete = tasks.FirstOrDefault(t => t.Id == id);
                if (taskToDelete != null)
                {
                    tasks.Remove(taskToDelete);
                    Console.WriteLine("Task deleted");

                    upJson(tasks);
                    //string jsonString = JsonSerializer.Serialize(tasks);
                    //File.WriteAllText(fileName, jsonString);
                }
                else
                {
                    Console.WriteLine("Task not found");
                }
            }
            else
            {
                Console.WriteLine("Negative Id");
            }
        }


        private void upJson(List<TaskItem> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            File.WriteAllText(fileName, jsonString);
        }

        private List<TaskItem> LoadTasks()
        {
            if (!File.Exists(fileName))
            {
                return new List<TaskItem>();
            }

            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? new List<TaskItem>();
        }


        public void Marking(int id, string status)
        {
            if (id > 0)
            {
                var taskToMarking = tasks.FirstOrDefault(t => t.Id == id);
                if (taskToMarking != null)
                {
                    if (status == "mark-in-progress")
                    {
                        taskToMarking.Status = Status.InProgress;
                        upJson(tasks);

                    }
                    else if (status == "mark-done")
                    {
                        taskToMarking.Status = Status.Done;
                        upJson(tasks);
                    }
                }
            }
        }

        public void Marking2(int id)
        {
            if (id > 0)
            {
                var taskToMarking = tasks.FirstOrDefault(t => t.Id == id);
                if (taskToMarking != null)
                {
                    //if (status == "mark-in-progress")
                    //{
                    //    taskToMarking.Status = Status.InProgress;
                    //    upJson(tasks);

                    //}
                    //else if (status == "mark-done")
                    //{
                    //    taskToMarking.Status = Status.Done;
                    //    upJson(tasks);
                    //}
                }
            }
        }

        //private void LoadTask()
        //{
        //    File.WriteAllText(fileName, "[]");
        //}


        /*
        public void UpdateTask(int id, string description)
        {
            //var task = tasks.FirstOrDefault(t => t.Id == id);
            //task.Description = description;
            //task.UpdatedAt = DateTime.Now;
            //SaveTaskInJson2(task);

            //for (int i = 0; i < tasks.Count; i++)
            //{
            //    if (tasks[i].Id == id)
            //    {
            //        tasks[i].Description = description;
            //        tasks[i].UpdatedAt = DateTime.Now;
            //    }
            //}
            if (id > 0)
            {
                foreach (var item in tasks)
                {
                    if (item.Id == id)
                    {
                        item.Description = description;
                        item.UpdatedAt = DateTime.Now;
                        //Console.WriteLine("id" + item.Id);
                        //Console.WriteLine("id" + item.Description);
                        Console.WriteLine($"Task updated successfully (ID: {item.Id})");
                    }
                    else
                    {
                        Console.WriteLine("Task Not found");
                    }
                }


                string jsonString = JsonSerializer.Serialize(tasks);
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine($"Task updated successfully (ID: {id})");
            }
            else
            {
                Console.WriteLine("Negative Id");
            }
        }

        */


    }

}


//var task = tasks.FirstOrDefault(t => t.Id == 1);