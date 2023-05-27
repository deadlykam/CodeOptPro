using KamranWali.CodeOptPro.Managers;
using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    [CreateAssetMenu(fileName = "UpdateManagerGlobalHelper",
                     menuName = "CodeOptPro/ScriptableObjects/Managers/" +
                                "UpdateManagerGlobalHelper",
                     order = 1)]
    public class UpdateManagerGlobalHelper : BaseManagerHelper<UpdateManagerGlobal>
    {
        /// <summary>
        /// Gets the time delta value for the manager.
        /// </summary>
        /// <returns>The time delta value, of type float</returns>
        public float GetTimeDelta() => manager != null ? manager.GetTimeDelta() : -1f;

        /// <summary>
        /// Gets the calculated Time.deltaTime value from the manager.
        /// </summary>
        /// <returns>The calculated Time.deltaTime value, of type float</returns>
        public float GetTime() => manager != null ? manager.GetTime() : -1f;

        /// <summary>
        /// This method adds an object to the list, ONLY CALL FROM EDITOR SCRIPT.
        /// </summary>
        /// <param name="obj">The object to add, of type MonoAdvUpdate</param>
        public void AddObject(MonoAdvUpdate obj) => manager?.AddObject(obj);

        /// <summary>
        /// This method removes all object from the list.
        /// </summary>
        public void ResetData() => manager?.ResetData();
    }
}