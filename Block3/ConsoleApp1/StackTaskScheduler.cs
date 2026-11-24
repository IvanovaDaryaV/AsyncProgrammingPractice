public class StackTaskScheduler : TaskScheduler
{
    private readonly Stack<Task> tasks = new Stack<Task>(); 
    protected override IEnumerable<Task> GetScheduledTasks()
    {
        lock (tasks)
        {
            return tasks.ToArray();
        }
    }

    /// <summary>
    /// Добавляет задачу в стек задач и инициирует ее выполнение.
    /// </summary>
    /// <remarks>
    /// Метод выполняется при вызове метода Start() класса Task.
    /// </remarks>
    protected override void QueueTask(Task task)
    {
        lock (tasks)
        {
            tasks.Push(task);

            NotifyThreadPoolOfPendingWork();
        }
    }

    /// <summary>
    /// Параллельно запрашивает поток из пула для выполнения последней поставленной в стек задачи.
    /// </summary>
    private void NotifyThreadPoolOfPendingWork()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Task? taskToRun = null;

            lock(tasks){
                if (tasks.Count > 0)
                {
                        tasks.TryPop(out taskToRun);
                }
            }

            if (taskToRun != null)
            {
                TryExecuteTask(taskToRun);
            }
        }, null);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        if (taskWasPreviouslyQueued)
            return false;

        return TryExecuteTask(task);
    }
}
