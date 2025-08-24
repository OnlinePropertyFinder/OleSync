namespace OleSync.Domain.Boards.Utilities
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
