namespace Script.Core {
public interface IEntity<in T>{
    public void Initialize(T context);
}
}