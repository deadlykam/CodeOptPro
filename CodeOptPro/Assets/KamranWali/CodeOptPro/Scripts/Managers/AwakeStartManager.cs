using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class AwakeStartManager : MonoBehaviour
    {
        [Header("AwakeStartManager Global Properties")]
        [SerializeField] private AwakeStartManagerHelper _helper;

        [Header("AwakeStartManager Local Properties")]
        [SerializeField] private List<BaseAwakeStart> _data;

        private int _counter;

        private void Awake() 
        {
            _helper.SetManager(this);
            for (_counter = 0; _counter < _data.Count; _counter++) _data[_counter].AwakeAdv(); // Calling all awakes
        }
        
        private void Start() 
        { 
            for (_counter = 0; _counter < _data.Count; _counter++) _data[_counter].StartAdv(); 
        } // Calling all starts
          //TODO: Call linked manager either in update or call the individually that is awake ones from awake and start ones from start

        #region Editor Methods
        /// <summary>
        /// This method initializes the object.
        /// </summary>
        public void Init() => _helper.SetManager(this);

        /// <summary>
        /// This method adds an object to the list, NOT RECOMMENDED TO BE CALLED ON RUN TIME!
        /// </summary>
        /// <param name="obj">The object to add, of type BaseAwakeStart</param>
        public void AddObject(BaseAwakeStart obj)
        {
            if (_data == null) ResetData(); // Checking if list is null then initializing it
            _data.Add(obj);
        }

        /// <summary>
        /// This method removes all data from the list.
        /// </summary>
        public void ResetData() => _data = new List<BaseAwakeStart>();
        #endregion
    }
}