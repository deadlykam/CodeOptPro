using CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace CodeOptPro.Managers
{
    public class AwakeStartManager : MonoBehaviour
    {
        [Header("AwakeStartManager Global Properties")]
        [SerializeField] private AwakeStartManagerHelper _helper;

        [Header("AwakeStartManager Local Properties")]
        [SerializeField] private List<BaseAwakeStart> _objs;

        private int _counter;

        private void Awake() 
        {
            _helper.SetManager(this);
            for (_counter = 0; _counter < _objs.Count; _counter++) _objs[_counter].AwakeAdv(); // Calling all awakes
        }
        
        private void Start() { for (_counter = 0; _counter < _objs.Count; _counter++) _objs[_counter].StartAdv(); } // Calling all starts
                                                                                                                    //TODO: Call linked manager either in update or call the individually that is awake ones from awake and start ones from start

        #region Editor Methods
        /// <summary>
        /// This method adds an object to the list, NOT RECOMMENDED TO BE CALLED ON RUN TIME!
        /// </summary>
        /// <param name="obj">The object to add, of type BaseAwakeStart</param>
        public void AddObject(BaseAwakeStart obj)
        {
            if (_objs == null) ResetData(); // Checking if list is null then initializing it
            _objs.Add(obj);
        }

        /// <summary>
        /// This method removes all data from the list.
        /// </summary>
        public void ResetData() => _objs = new List<BaseAwakeStart>();
        #endregion
    }
}