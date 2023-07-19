using KamranWali.CodeOptPro.ScriptableObjects.Managers;
using System.Collections.Generic;

namespace KamranWali.CodeOptPro.Managers
{
    public interface ICOPSetup_Call<T> : ICOPSetup<T>
    {
        /// <summary>
        /// This method gets the manager helper, THIS METHOD IS FOR EDITOR ONLY
        /// </summary>
        /// <returns>The manager helper or the group, of type MonoAdvManager_CallHelper</returns>
        public MonoAdvManager_CallHelper GetManagerHelper();

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