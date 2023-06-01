using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class MonoAdvManager_Call : MonoBehaviour, ICOPSetup<MonoAdvManager>
    {
        [Header("MonoAdvManager_Call Local Properties")]
        [SerializeField] private List<MonoAdvManager> _managers_PreAwakeAdv_Setup;
        [SerializeField] private MonoAdvManagerHelper[] _managers;

        private int _counter;

        private void Awake()
        {
            for (_counter = 0; _counter < _managers_PreAwakeAdv_Setup.Count; _counter++) _managers_PreAwakeAdv_Setup[_counter].PreAwakeAdv(); // Calling pre awake adv setup to allow global variable setup
            for (_counter = 0; _counter < _managers.Length; _counter++) _managers[_counter].AwakeAdv(); // Calling all manager awake
        }
        private void Start() { for (_counter = 0; _counter < _managers.Length; _counter++) _managers[_counter].StartAdv(); } // Calling all manager start

        #region Editor Script
        public void SetManagers(MonoAdvManagerHelper[] managers) => _managers = managers;
        public void AddObject(MonoAdvManager obj) => _managers_PreAwakeAdv_Setup.Add(obj);
        
        public void ResetData()
        {
            _managers_PreAwakeAdv_Setup = new List<MonoAdvManager>();
            for (_counter = 0; _counter < _managers.Length; _counter++) _managers[_counter].SetManager(null); // Flushing out any null references
        }
        #endregion
    }
}