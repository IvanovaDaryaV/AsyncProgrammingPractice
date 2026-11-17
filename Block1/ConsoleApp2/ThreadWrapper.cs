/// <summary>
/// Обобщенный класс-обертка для работы с классом Thread.
/// </summary>
public class ThreadWrapper<TResult>
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

    private TResult? _result;
    private readonly Func<object, TResult> _func;
    private Thread? _thread;

    public ThreadWrapper(Func<object, TResult> func){
        _func = func ?? throw new ArgumentNullException(nameof(func));
        _result = default;
    }
    public TResult Result
    {
        get
        {
            while (!this.IsCompleted)
            {
                Thread.Sleep(150);
            }

            if (this.Exception != null)
            {
                throw this.Exception;
            }

            return _result!;
        }
    }

    /// <summary>
    /// Запускает делегат Func<object, TResult> в отдельном потоке Thread. 
    /// </summary>
    public void Start(object arg)
    {
        _thread = new Thread(new ParameterizedThreadStart(x =>
        {
            try
            {
                _result = _func(x);
                this.IsSuccess = true;
            }
            catch(Exception ex)
            {
                this.IsFaulted = true;
                this.Exception = ex;
            }
            finally
            {
                this.IsCompleted = true;
            }
        }));

        _thread.Start(arg);
    }
}
