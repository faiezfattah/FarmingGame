namespace Script.Core.Interface {
public interface IUseable {} // tagging interface
public interface IUseable<TContext> : IUseable {
    public void Use(TContext context);
}
}