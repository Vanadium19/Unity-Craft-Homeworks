namespace Game.Core.Components
{
    public interface IEntity
    {
        public T Get<T>();
        public bool TryGet<T>(out T component) where T : class;
    }
}