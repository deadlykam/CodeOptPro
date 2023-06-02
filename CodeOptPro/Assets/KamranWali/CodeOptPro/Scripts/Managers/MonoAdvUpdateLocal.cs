using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdvUpdateLocal : MonoAdvUpdate
    {
        [Header("MonoAdvUpdateLocal Local Properties")]
        [SerializeField] protected UpdateManagerLocal updateManager;

        #region Editor Scripts
        public override void Init()
        {
            base.Init();
            updateManager.AddObject(this);
        }

        public override bool HasManager() => base.HasManager() && (updateManager != null);
        #endregion
    }
}