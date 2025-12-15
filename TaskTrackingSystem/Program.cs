// Main console UI for this whole thing
//ref again can be used pretty easily here for 
// Menu structure i:
// - Abhi-AIX/task_manager_c_sharp
// - alexeykrymov/ToDo-List-in-C-
// JSON storage idea to use  :from m-ah07/to-do-list-csharp and
// KAAli-DBS/Advanced-Programming PhoneBookJSON examples.

using System;
using TaskTrackingSystem.Business;
using TaskTrackingSystem.DataAccess;
using TaskTrackingSystem.Models;
using ModelTaskStatus = TaskTrackingSystem.Models.TaskStatus;

namespace TaskTrackingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "tasks.json";
            var repository = new JsonTaskRepository(filePath);
            var service = new TaskService(repository);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Task Tracking System ===");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View All Tasks (sorted by due date)");
                Console.WriteLine("3. Search Tas by Id (Binary Search)");
                Console.WriteLine("4. Update Task");
                Console.WriteLine("5. Delete Task");
                Console.WriteLine("6. Exit");
                Console.Write("Choose : ");

                string? input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        AddTask(service);
                        break;
                    case "2":
                        ViewTasks(service);
                        break;
                    case "3":
                        SearchTask(service);
                        break;
                    case "4":
                        UpdateTask(service);
                        break;
                    case "5":
                        DeleteTask(service);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option, Press Entr to continue....");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void AddTask(TaskService service)
        {
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Description: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Due date (yyyy-MM-dd):");
            string? dueInput = Console.ReadLine();
            DateTime dueDate;
            if (!DateTime.TryParse(dueInput, out dueDate))
            {
                Console.WriteLine("Invalid date, enter future one");
                dueDate = DateTime.Today;
            }

            Console.Write("Priority (1 = High, 2 = Medium, 3 = Low): ");
            string? prInput = Console.ReadLine();
            int priority;
            if (!int.TryParse(prInput, out priority) || priority < 1 || priority > 3)
            {
                Console.WriteLine("Invalid priority, using 2 (Medium).");
                priority = 2;
            }

            Console.Write("Status (0 = Todo, 1 = InProgress, 2 = Done): ");
            string? stInput = Console.ReadLine();
            int stInt;
            if (!int.TryParse(stInput, out stInt) || stInt < 0 || stInt > 2)
            {
                Console.WriteLine("Invalid status, using 0 (Todo).");
                stInt = 0;
            }
            ModelTaskStatus status = (ModelTaskStatus)stInt;

            var task = service.AddTask(title, description, dueDate, priority, status);
            Console.WriteLine($"Task added with Id {task.Id}.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void ViewTasks(TaskService service)
        {
            var tasks = service.GetTasksSortedByDueDate();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                Console.WriteLine("Tasks sorted by due date:");
                foreach (var t in tasks)
                {
                    Console.WriteLine(t);
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void SearchTask(TaskService service)
        {
            Console.Write("Enter Id to search: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid Id.");
            }
            else
            {
                var task = service.BinarySearchById(id);
                if (task == null)
                    Console.WriteLine("Task not found.");
                else
                    Console.WriteLine(task);
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void UpdateTask(TaskService service)
        {
            Console.Write("Enter Id to update: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid Id.");
                Console.ReadLine();
                return;
            }

            var existing = service.BinarySearchById(id);
            if (existing == null)
            {
                Console.WriteLine("Task not found.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Leave blankto keep the existing value.");

            Console.Write($"Title ({existing.Title}): ");
            string? title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
                existing.Title = title;

            Console.Write($"Description ({existing.Description}): ");
            string? description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
                existing.Description = description;

            Console.Write($"Due date ({existing.DueDate:yyyy-MM-dd}): ");
            string? dueInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dueInput) && DateTime.TryParse(dueInput, out DateTime newDue))
                existing.DueDate = newDue;

            Console.Write($"Prioity ({existing.Priority}): ");
            string? prInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(prInput) && int.TryParse(prInput, out int newPr)
                && newPr >= 1 && newPr <= 3)
                existing.Priority = newPr;

            Console.Write($"Status ({(int)existing.Status}): ");
            string? stInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stInput) && int.TryParse(stInput, out int newSt)
                && newSt >= 0 && newSt <= 2)
                existing.Status = (ModelTaskStatus)newSt;

            service.UpdateTask(existing);

            Console.WriteLine("Task Updated.");
            Console.WriteLine("Press Enter  to continue...");
            Console.ReadLine();
        }

        private static void DeleteTask(TaskService service)
        {
            Console.Write("Enter Id to delete: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Id Invalid");
            }
            else
            {
                service.DeleteTask(id);
                Console.WriteLine("If the Id xisted, the task was deleted.");
            }
            Console.WriteLine("Press Enter continue...");
            Console.ReadLine();
        }
    }
}



