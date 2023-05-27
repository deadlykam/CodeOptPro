namespace KamranWali.CodeOptPro.Managers
{
    /// <summary>
    /// This class helps the CodeOptProSetup class to setup the object
    /// </summary>
    public interface ICOPSetup<T>
    {
        /// <summary>
        /// This method adds an object to the list, NOT RECOMMENDED TO BE CALLED ON RUN TIME!
        /// </summary>
        /// <param name="obj">The object to add, of type T</param>
        public void AddObject(T obj);

        /// <summary>
        /// This method removes all data from the list.
        /// </summary>
        public void ResetData();
    }
}