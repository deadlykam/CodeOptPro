namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdvUpdate : MonoAdv
    {
        /// <summary>
        /// This method updates the object.
        /// </summary>
        public abstract void UpdateObject();

        /// <summary>
        /// This method sets the active state of the object.
        /// </summary>
        /// <param name="isActivate">Flag to activate/deactivate object, of type bool</param>
        public abstract void SetActive(bool isActivate);

        /// <summary>
        /// This method checks if the object is active.
        /// </summary>
        /// <returns>True means active, false means otherwise, of type bool</returns>
        public abstract bool IsActive();
    }
}