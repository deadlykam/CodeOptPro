using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;

namespace KamranWali.CodeOptPro.Managers
{
    public interface ICOPSetup_Call<T> : ICOPSetup<T>
    {
        /// <summary>
        /// This method sets the manager lists.
        /// </summary>
        /// <param name="managers">The manager list to set, of type List<MonoAdvManagerHelper></param>
        public void SetManagers(List<MonoAdvManagerHelper> managers);

        /// <summary>
        /// This method gets the list of manager helpers.
        /// </summary>
        /// <returns>The list of manager helpers, of type MonoAdvManagerHelper</returns>
        public List<MonoAdvManagerHelper> GetManagers();
    }
}