using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdv : MonoBehaviour, IInit
    {
        [Header("MonoAdv Global Properties")]
        [SerializeField] private MonoAdvManagerHelper _manager;

        #region Editor Scripts
        /// <summary>
        /// This method initializes the object.
        /// </summary>
        public virtual void Init() => _manager.AddObject(this);

        public virtual bool HasManager() => _manager != null;
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