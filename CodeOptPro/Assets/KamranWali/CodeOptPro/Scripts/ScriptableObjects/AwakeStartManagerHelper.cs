using KamranWali.CodeOptPro.Managers;
using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AwakeStartManagerHelper",
                     menuName = "CodeOptPro/ScriptableObjects/Managers/" +
                                "AwakeStartManagerHelper",
                     order = 1)]
    public class AwakeStartManagerHelper : ScriptableObject
    {
        private AwakeStartManager _manager;

        /// <summary>
        /// This method sets the manager.
        /// </summary>
        /// <param name="manager">The manager to set, of type AwakeStartManager</param>
        public void SetManager(AwakeStartManager manager) => _manager = manager;

        /// <summary>
        /// This method is called during awake, SHOULD BE CALLED BY AwakeStartManager_Call ONLY.
        /// </summary>
        public void AwakeAdv() => _manager?.AwakeAdv();

        /// <summary>
        /// This method is called during start, SHOULD BE CALLED BY AwakeStartManager_Call ONLY.
        /// </summary>
        public void StartAdv() => _manager?.StartAdv();

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