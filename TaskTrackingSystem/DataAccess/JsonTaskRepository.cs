// JSON persistence pattern reff
// - m-ah07/to-do-list-csharp (JSON file-based to-do list):
//   https://github.com/m-ah07/to-do-list-csharp
// - KAAli-DBS/Advanced-Programming PhoneBookJSON example:
//   https://github.com/KAAli-DBS/Advanced-Programming
//
// need to adjust it to work with TaskItem and ITaskRepository.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskTrackingSystem.Models;

namespace TaskTrackingSystem.DataAccess
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private readonly List<TaskItem> _tasks;

        public JsonTaskRepository(string filePath)
        {
            _filePath = filePath;
            _tasks = LoadFromFile();
        }

        private List<TaskItem> LoadFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<TaskItem>();

            string json = File.ReadAllText(_filePath);
            var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
            return tasks ?? new List<TaskItem>();
        }

        private void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_tasks, options);
            File.WriteAllText(_filePath, json);
        }

        public List<TaskItem> GetAll()
        {
            return new List<TaskItem>(_tasks);
        }

        public TaskItem? GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Add(TaskItem task)
        {
            _tasks.Add(task);
            SaveToFile();
        }

        public void Update(TaskItem task)
        {
            var existing = GetById(task.Id);
            if (existing == null) return;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.DueDate = task.DueDate;
            existing.Priority = task.Priority;
            existing.Status = task.Status;

            SaveToFile();
        }

        public void Delete(int id)
        {
            var task = GetById(id);
            if (task == null) return;

            _tasks.Remove(task);
            SaveToFile();
        }

        public int GetNextId()
        {
            return _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        }
    }
}


