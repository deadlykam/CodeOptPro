using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;

namespace KamranWali.CodeOptPro.Managers
{
    public interface ICOPSetup_Call<T> : ICOPSetup<T>
    {
        /// <summary>
        /// This method adds the manager helpers.
        /// </summary>
        /// <param name="obj">The manager helper to add, of type MonoAdvManagerHelper</param>
        public void AddManagerHelper(MonoAdvManagerHelper obj);

        /// <summary>
        /// This method gets the list of manager helpers.
        /// </summary>
        /// <returns>The list of manager helpers, of type MonoAdvManagerHelper</returns>
        public List<MonoAdvManagerHelper> GetManagers();
    }
}