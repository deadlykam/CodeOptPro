namespace KamranWali.CodeOptPro.Managers
{
    /// <summary>
    /// This class helps to initialize the object.
    /// </summary>
    public interface IInit
    {
        /// <summary>
        /// This method initializes the object.
        /// </summary>
        public void Init();

        /// <summary>
        /// This method checks if the object has a manager helper reference.
        /// </summary>
        /// <returns>True means has reference, false otherwise, of type bool</returns>
        public bool HasManager();
    }
}