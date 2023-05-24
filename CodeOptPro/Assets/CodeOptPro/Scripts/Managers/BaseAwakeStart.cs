using UnityEngine;

namespace CodeOptPro.Managers
{
    public abstract class BaseAwakeStart : MonoBehaviour, IAwakeStart
    {
        /// <summary>
        /// This method is called during awake.
        /// </summary>
        public abstract void AwakeAdv();

        /// <summary>
        /// This method is called during start.
        /// </summary>
        public abstract void StartAdv();
    }
}