using CodeOptPro.Managers;
using UnityEngine;

namespace CodeOptPro.ScriptableObjects
{
    public class AwakeStartManagerHelper
    {
        private AwakeStartManager _manager;

        public void SetManager(AwakeStartManager manager) => _manager = manager;

        /// <summary>
        /// This method adds an object to the list, NOT RECOMMENDED TO BE CALLED ON RUN TIME!
        /// </summary>
        /// <param name="obj">The object to add, of type BaseAwakeStart</param>
        public void AddObject(BaseAwakeStart obj) => _manager?.AddObject(obj);

        /// <summary>
        /// This method removes all data from the list.
        /// </summary>
        public void ResetData() => _manager?.ResetData();
    }
}