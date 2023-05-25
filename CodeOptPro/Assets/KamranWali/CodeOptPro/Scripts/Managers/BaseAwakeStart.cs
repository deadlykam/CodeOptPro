using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public abstract class BaseAwakeStart : MonoBehaviour, IAwakeStart
    {
        [Header("BaseAwakeStart Global Properties")]
        [SerializeField] private AwakeStartManagerHelper _manager;

        #region Editor Scripts
        /// <summary>
        /// This method initializes the object.
        /// </summary>
        public virtual void Init() => _manager.AddObject(this);
        #endregion

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