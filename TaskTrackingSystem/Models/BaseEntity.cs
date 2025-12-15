/
// rpository examples 
// https://github.com/alexandra-valkova/TaskManagerConsole
// 

namespace TaskTrackingSystem.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        protected BaseEntity(int id)
        {
            Id = id;
        }
    }
}


