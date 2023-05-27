using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdvUpdateGlobal : MonoAdvUpdate
    {
        [Header("MonoAdvUpdateGlobal Global Properties")]
        [SerializeField] protected UpdateManagerGlobalHelper updateManager;

        public override void Init()
        {
            base.Init();
            updateManager.AddObject(this);
        }
    }
}