namespace KamranWali.CodeOptPro.Managers
{
    public interface IAwakeStart
    {
        /// <summary>
        /// This method initializes the object.
        /// </summary>
        public void Init();

        /// <summary>
        /// This method is called during awake.
        /// </summary>
        public void AwakeAdv();

        /// <summary>
        /// This method is called during start.
        /// </summary>
        public void StartAdv();
    }
}