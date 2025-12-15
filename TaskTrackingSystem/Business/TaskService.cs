// Service layer ref https://github.com/alexandra-valkova/TaskManagerConsole
// reff for the bubble sort...seems just like a common way to go about it  wlensinas/Bubble-Sort-With-C-Sharp


using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.DataAccess;
using TaskTrackingSystem.Models;
using ModelTaskStatus = TaskTrackingSystem.Models.TaskStatus;

namespace TaskTrackingSystem.Business
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }
// Cruds
        // INSERT (create task )
        public TaskItem AddTask(string title, string description,
                                DateTime dueDate, int priority, ModelTaskStatus status)
        {
            int id = _repository.GetNextId();
            var task = new TaskItem(id, title, description, dueDate, priority, status);
            _repository.Add(task);
            return task;
        }

        // READ ALL
        public List<TaskItem> GetAllTasks()
        {
            return _repository.GetAll();
        }

        // READ BY ID (direct repository lookup)
        public TaskItem? GetTaskById(int id)
        {
            return _repository.GetById(id);
        }

        // UPDATE
        public void UpdateTask(TaskItem task)
        {
            _repository.Update(task);
        }

        // DELETEE
        public void DeleteTask(int id)
        {
            _repository.Delete(id);
        }

        //Bubble sorting
        // Returns tasks sorted by DueDate using Bubble Sort
        public List<TaskItem> GetTasksSortedByDueDate()
        {
            var tasks = _repository.GetAll().ToArray();
            BubbleSort(tasks, (a, b) => a.DueDate.CompareTo(b.DueDate));
            return tasks.ToList();
        }

        // Bubble Sort over TaskItem[] using a Comparison 
        private void BubbleSort(TaskItem[] array, Comparison<TaskItem> compare)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (compare(array[j], array[j + 1]) > 0)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        //(id search)

        public TaskItem? BinarySearchById(int id)
        {
            // Get tasks and sort them by Id first (also using Buble Sort)
            var tasks = _repository.GetAll().ToArray();
            BubbleSort(tasks, (a, b) => a.Id.CompareTo(b.Id));

            int left = 0;
            int right = tasks.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (tasks[mid].Id == id)
                    return tasks[mid];

                if (tasks[mid].Id < id)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return null;
        }
    }
}

 
