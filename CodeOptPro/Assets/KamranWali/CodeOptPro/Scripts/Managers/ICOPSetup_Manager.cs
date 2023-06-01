using KamranWali.CodeOptPro.ScriptableObjects;

namespace KamranWali.CodeOptPro.Managers
{
    public interface ICOPSetup_Manager<T> : ICOPSetup<T>
    {
        /// <summary>
        /// This method gets the manager helper, THIS METHOD IS FOR EDITOR ONLY!
        /// </summary>
        /// <returns></returns>
        public MonoAdvManagerHelper GetManagerHelper();
    }
}