// Repository interface ref....not sure about this but i like it being seperated from the logic
// alexandra-valkova/TaskManagerConsole:
// https://github.com/alexandra-valkova/TaskManagerConsole

using System.Collections.Generic;
using TaskTrackingSystem.Models;

namespace TaskTrackingSystem.DataAccess
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(int id);
        int GetNextId();
    }
}

