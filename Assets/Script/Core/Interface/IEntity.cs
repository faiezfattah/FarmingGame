namespace Script.Core.Interface {
public interface IEntity<in T> {
    public void Initialize(T context);
}
}