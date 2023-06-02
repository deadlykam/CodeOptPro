using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdvUpdateGlobal : MonoAdvUpdate
    {
        [Header("MonoAdvUpdateGlobal Global Properties")]
        [SerializeField] protected UpdateManagerGlobalHelper updateManager;

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