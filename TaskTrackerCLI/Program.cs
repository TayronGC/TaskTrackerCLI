using TaskTrackerCLI.Services;

class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();
        taskManager.addTask();
    }
}