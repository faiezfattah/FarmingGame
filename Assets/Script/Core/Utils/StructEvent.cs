using System;
using R3;

public class StructEvent<T> where T : struct {
    public delegate void StructDelegate(ref T value);
    public StructDelegate Listener;
    public IDisposable Subscribe(StructDelegate structDelegate) {
        Listener += structDelegate;
        return Disposable.Create(() => Listener -= structDelegate);
    }
    public void Emit(ref T value) {
        Listener?.Invoke(ref value);
    }
}