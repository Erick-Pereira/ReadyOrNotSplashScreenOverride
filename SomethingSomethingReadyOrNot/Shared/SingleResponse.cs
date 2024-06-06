namespace SomethingSomethingReadyOrNot.Shared
{
    public class SingleResponse<T> : Response
    {
        public T Item { get; set; }
    }
}