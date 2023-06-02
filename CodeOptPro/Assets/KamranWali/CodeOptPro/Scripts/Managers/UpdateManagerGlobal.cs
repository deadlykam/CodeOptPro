using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class UpdateManagerGlobal : UpdateManagerLocal
    {
        [Header("UpdateManagerGlobal Global Properties")]
        [SerializeField] private UpdateManagerGlobalHelper _helper;

        public override void AwakeAdv()
        {
            base.AwakeAdv();
            _helper.SetManager(this);
        }
        public override void StartAdv() { }

        #region Editor Script
        /// <summary>
        /// This method sets up the object which is needed by the CodeOptProSetup class, ONLY CALL FROM CodeOptProSetup CLASS.
        /// </summary>
        public void Setup() => _helper.SetManager(this);

        public override bool HasManager() => base.HasManager() && (_helper != null);
        #endregion
    }
}