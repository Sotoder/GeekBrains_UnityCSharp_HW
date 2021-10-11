using System;
public class RefOnClassFild<T>
{
    private Func<T> getter;
    private Action<T> setter;
    public RefOnClassFild(Func<T> getter, Action<T> setter)
    {
        this.getter = getter;
        this.setter = setter;
    }
    public T Value
    {
        get => getter();
        set => setter(value);
    }
}
