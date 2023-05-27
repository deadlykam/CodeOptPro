using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    public abstract class BaseManagerHelper<T> : ScriptableObject
    {
        protected T manager;

        /// <summary>
        /// This method sets the manager.
        /// </summary>
        /// <param name="manager">The manager to set, of type T</param>
        public void SetManager(T manager) => this.manager = manager;
    }
}