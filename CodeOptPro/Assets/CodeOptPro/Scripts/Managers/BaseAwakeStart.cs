using CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace CodeOptPro.Managers
{
    public abstract class BaseAwakeStart : MonoBehaviour, IAwakeStart
    {
        [Header("BaseAwakeStart Global Properties")]
        [SerializeField] private AwakeStartManagerHelper _manager;

        /// <summary>
        /// This method is called during awake.
        /// </summary>
        public abstract void AwakeAdv();

        /// <summary>
        /// This method is called during start.
        /// </summary>
        public abstract void StartAdv();

        //TODO: An editor method that will add itself to the manager
    }
}