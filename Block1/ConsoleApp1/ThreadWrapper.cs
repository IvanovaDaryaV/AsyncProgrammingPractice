using System.Runtime.CompilerServices;

/// <summary>
/// Класс-обертка для работы с классом Thread.
/// </summary>
public class ThreadWrapper
{
    /// <summary>
    /// Показывает, завершилость ли выполнение метода.
    /// </summary>
    public bool IsCompleted { get; private set; } = false;
    /// <summary>
    /// Показывает, завершилось ли выполнение успешно.
    /// </summary>
    public bool IsSuccess { get; private set; } = false;
    /// <summary>
    /// Показывает, завершилось ли выполнение с ошибкой.
    /// </summary>
    public bool IsFaulted { get; private set; } = false;
    /// <summary>
    /// Хранит исключение, возникшее во вторичном потоке.
    /// </summary>
    public Exception? Exception { get; private set; } = null;
    private Thread? _thread;
    private readonly Action<object> _action;

    public ThreadWrapper(Action<object> action)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }

    /// <summary>
    /// Запускает делегат Action<object> в отдельном потоке Thread. 
    /// </summary>
    public void Start(object arg)
    {
        _thread = new Thread(new ParameterizedThreadStart(x => {
                try
                {
                    _action(x);
                    this.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    this.Exception = ex;
                    this.IsFaulted = true;
                }
                finally
                {
                    this.IsCompleted = true;
                }
            }
        ));

        _thread.Start(arg);
    }

    /// <summary>
    /// Блокирует вызвавший его поток до завершения работы делегата.
    /// </summary>
    public void Wait()
    {
        while (this.IsCompleted == false)
        {
            Thread.Sleep(150);
        }

        if (Exception != null)
        {
            throw Exception;
        }
    }
}