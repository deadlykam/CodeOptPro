using KamranWali.CodeOptPro.ScriptableObjects.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class MonoAdvManager_Call : MonoBehaviour, ICOPSetup_Call<MonoAdvManager>, IInit
    {
        [Header("MonoAdvManager_Call Global Properties")]
        [SerializeField] private MonoAdvManager_CallHelper _helper;

        [Header("MonoAdvManager_Call Local Properties")]
        [SerializeField] private List<MonoAdvManager> _managers_PreAwakeAdv_Setup;
        [SerializeField] private List<MonoAdvManagerHelper> _managers;

        private int _counter;

        private void Awake()
        {
            //TODO: Set the MonoAdvManager_CallHelper reference here
            for (_counter = 0; _counter < _managers_PreAwakeAdv_Setup.Count; _counter++) _managers_PreAwakeAdv_Setup[_counter].PreAwakeAdv(); // Calling pre awake adv setup to allow global variable setup
            for (_counter = 0; _counter < _managers.Count; _counter++) _managers[_counter].AwakeAdv(); // Calling all manager awake
        }
        private void Start() { for (_counter = 0; _counter < _managers.Count; _counter++) _managers[_counter].StartAdv(); } // Calling all manager start

        #region Editor Script
        /// <summary>
        /// This method initializes the script and is ONLY called from CodeOptProSetupAuto script.
        /// </summary>
        public void Init() => _helper.SetManager(this);
        public void AddObject(MonoAdvManager obj) => _managers_PreAwakeAdv_Setup.Add(obj);
        public void SetManagers(List<MonoAdvManagerHelper> managers) => _managers = managers;
        public List<MonoAdvManagerHelper> GetManagers() => _managers;
        public bool HasManager() => _helper != null;
        public MonoAdvManager_CallHelper GetManagerHelper() => _helper;

        public void ResetData()
        {
            _managers_PreAwakeAdv_Setup = new List<MonoAdvManager>();
            _managers = new List<MonoAdvManagerHelper>();
            for (_counter = 0; _counter < _managers.Count; _counter++) _managers[_counter].SetManager(null); // Flushing out any null references
        }
        #endregion
    }
}