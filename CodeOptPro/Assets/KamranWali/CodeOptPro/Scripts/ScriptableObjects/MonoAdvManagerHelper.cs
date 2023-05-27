using KamranWali.CodeOptPro.Managers;
using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MonoAdvManagerHelper",
                     menuName = "CodeOptPro/ScriptableObjects/Managers/" +
                                "MonoAdvManagerHelper",
                     order = 1)]
    public class MonoAdvManagerHelper : BaseManagerHelper<MonoAdvManager>
    {
        /// <summary>
        /// This method is called during awake, SHOULD BE CALLED BY AwakeStartManager_Call ONLY.
        /// </summary>
        public void AwakeAdv() => manager?.AwakeAdv();

        /// <summary>
        /// This method is called during start, SHOULD BE CALLED BY AwakeStartManager_Call ONLY.
        /// </summary>
        public void StartAdv() => manager?.StartAdv();

        /// <summary>
        /// This method adds an object to the list, NOT RECOMMENDED TO BE CALLED ON RUN TIME!
        /// </summary>
        /// <param name="obj">The object to add, of type BaseAwakeStart</param>
        public void AddObject(MonoAdv obj) => manager?.AddObject(obj);

        /// <summary>
        /// This method removes all data from the list.
        /// </summary>
        public void ResetData() => manager?.ResetData();
    }
}