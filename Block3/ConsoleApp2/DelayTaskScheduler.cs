public class DelayTaskScheduler : TaskScheduler
{
    private readonly LinkedList<Task> tasks = new LinkedList<Task>();
    protected override IEnumerable<Task> GetScheduledTasks()
    {
        lock (tasks)
        {
            return tasks.ToList();
        }
    }

    /// <summary>
    /// Добавляет задачу в список задач и инициирует ее выполнение.
    /// </summary>
    /// <remarks>
    /// Метод выполняется при вызове метода Start() класса Task.
    /// </remarks>
    protected override void QueueTask(Task task)
    {
        lock (tasks)
        {
            tasks.AddLast(task);

            NotifyThreadPoolOfPendingWork();
        }
    }

    /// <summary>
    /// Параллельно запрашивает поток из пула для выполнения последней поставленной в стек задачи.
    /// </summary>
    private void NotifyThreadPoolOfPendingWork()
    {
        Timer? timer = null;
        timer = new Timer(_ =>
        {
            timer?.Dispose();

            Task? taskToRun = null;

            lock (tasks)
            {
                if (tasks.Count != 0)
                {
                    taskToRun = this.tasks.First?.Value;
                    tasks.RemoveFirst();
                }
            }

            if (taskToRun != null)
            {
                TryExecuteTask(taskToRun);
            }
        }, null, 2000, Timeout.Infinite);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return false;
    }
}