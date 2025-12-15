// double check for code ref me
// - Abhi-AIX/task_manager_c_sharp - m-ah07/to-do-list-csharp

using System;

namespace TaskTrackingSystem.Models
{
    public enum TaskStatus
    {
        Todo,
        InProgress,
        Done
    }

    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; } // 1 = High, 3 = Loww etc
        public TaskStatus Status { get; set; }

        public TaskItem(int id, string title, string description,
                        DateTime dueDate, int priority, TaskStatus status)
            : base(id)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Status = status;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title} (Due: {DueDate:yyyy-MM-dd}, Priority: {Priority}, Status: {Status})";
        }
    }
}

