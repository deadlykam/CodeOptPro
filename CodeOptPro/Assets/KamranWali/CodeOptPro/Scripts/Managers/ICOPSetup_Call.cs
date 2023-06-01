using KamranWali.CodeOptPro.ScriptableObjects;

namespace KamranWali.CodeOptPro.Managers
{
    public interface ICOPSetup_Call<T> : ICOPSetup<T>
    {
        /// <summary>
        /// This method adds the manager helpers.
        /// </summary>
        /// <param name="obj">The manager helper to add, of type MonoAdvManagerHelper</param>
        public void AddManagerHelper(MonoAdvManagerHelper obj);
    }
}