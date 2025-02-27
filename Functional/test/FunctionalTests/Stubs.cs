namespace FunctionalTests;

public sealed class StubAction
{
    public bool Received { get; private set; }

    public void Invoke()
    {
        Received = true;
    }
}

public sealed class StubAction<T>
{
    public bool Received { get; private set; }

    public T Parameter { get; private set; } = default!;

    public void Invoke(T parameter)
    {
        Received = true;
        Parameter = parameter;
    }
}

public sealed class StubFunc<T>(T result)
{
    public bool Received { get; private set; }

    public T Invoke()
    {
        Received = true;
        return result;
    }
}

public sealed class StubFunc<T1, T2>(T2 result)
{
    public bool Received { get; private set; }

    public T1 Parameter { get; private set; } = default!;

    public T2 Invoke(T1 parameter)
    {
        Received = true;
        Parameter = parameter;
        return result;
    }
}