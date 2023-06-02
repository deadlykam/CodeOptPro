using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class MonoAdvManager : MonoBehaviour, ICOPSetup_Manager<MonoAdv>, IInit
    {
        [Header("MonoAdvManager Global Properties")]
        [SerializeField] private MonoAdvManagerHelper _helper;

        [Header("AwakeStartManager Local Properties")]
        [SerializeField] private List<MonoAdv> _data;

        private int _counter;

        /// <summary>
        /// This method does setup before AwakeAdv.
        /// </summary>
        public void PreAwakeAdv() => _helper.SetManager(this);

        /// <summary>
        /// This method is called during awake, SHOULD BE CALLED BY MonoAdvManager_Call ONLY.
        /// </summary>
        public void AwakeAdv() { for (_counter = 0; _counter < _data.Count; _counter++) _data[_counter].AwakeAdv(); } // Calling all awakes

        /// <summary>
        /// This method is called during start, SHOULD BE CALLED BY MonoAdvManager_Call ONLY.
        /// </summary>
        public void StartAdv() { for (_counter = 0; _counter < _data.Count; _counter++) _data[_counter].StartAdv(); }

        #region Editor Methods
        /// <summary>
        /// This method initializes the object, THIS METHOD IS FOR EDITOR ONLY!
        /// </summary>
        public void Init() => _helper.SetManager(this);

        public bool HasManager() => _helper != null;

        /// <summary>
        /// This method adds an object to the list, NOT RECOMMENDED TO BE CALLED ON RUN TIME!
        /// </summary>
        /// <param name="obj">The object to add, of type BaseAwakeStart</param>
        public void AddObject(MonoAdv obj)
        {
            if (_data == null) ResetData(); // Checking if list is null then initializing it
            _data.Add(obj);
        }

        /// <summary>
        /// This method gets the manager helper, THIS METHOD IS FOR EDITOR ONLY!
        /// </summary>
        /// <returns></returns>
        public MonoAdvManagerHelper GetManagerHelper() => _helper;

        /// <summary>
        /// This method removes all data from the list, THIS METHOD IS FOR EDITOR ONLY!
        /// </summary>
        public void ResetData() => _data = new List<MonoAdv>();
        #endregion
    }
}