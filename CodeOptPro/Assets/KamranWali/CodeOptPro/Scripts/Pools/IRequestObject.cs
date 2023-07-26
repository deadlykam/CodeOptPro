namespace KamranWali.CodeOptPro.Pools
{
    /// <summary>
    /// This interface allows an object to recieve pool objects from the BasePool objects.
    /// </summary>
    /// <typeparam name="T">The type of pool object to receive, of type <typeparamref name="T"/></typeparam>
    public interface IRequestObject<T>
    {
        /// <summary>
        /// This method receives a pool object.
        /// </summary>
        /// <param name="poolObject">The pool object to recieve, of type <typeparamref name="T"/></param>
        public void ReceivePoolObject(T poolObject);
    }
}