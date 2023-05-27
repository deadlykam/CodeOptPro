using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class UpdateManagerLocal : MonoAdv, ICOPSetup<MonoAdvUpdate>
    {
        [Header("UpdateManagerLocal Local Properties")]
        [SerializeField] private List<MonoAdvUpdate> _objects;
        [SerializeField, Min(1)] private int _numUpdate = 1;

        private int _pointer;
        private int _indexUpdate;
        private float _timeDelta;

        public override void AwakeAdv() => _timeDelta = _objects.Count / _numUpdate; // Calculating the delta time for the update manager
        public override void StartAdv() { }

        protected virtual void Update()
        {
            if (_objects.Count != 0) // Checking if Update is allowed
            {
                if (_numUpdate == 1) UpdateObject(); // Update 1 obj per frame
                else for (_indexUpdate = 0; _indexUpdate < _numUpdate; _indexUpdate++) UpdateObject(); // Update n obj per frame
            }
        }

        /// <summary>
        /// Gets the time delta value for the manager.
        /// </summary>
        /// <returns>The time delta value, of type float</returns>
        public float GetTimeDelta() => _timeDelta;

        /// <summary>
        /// Gets the calculated Time.deltaTime value from the manager.
        /// </summary>
        /// <returns>The calculated Time.deltaTime value, of type float</returns>
        public float GetTime() => _timeDelta * Time.deltaTime;

        #region Editor Methods
        /// <summary>
        /// This method adds an object to the list, ONLY CALL FROM EDITOR SCRIPT.
        /// </summary>
        /// <param name="obj">The object to add, of type MonoAdvUpdate</param>
        public void AddObject(MonoAdvUpdate obj)
        {
            if (_objects == null) ResetData();
            _objects.Add(obj);
        }

        /// <summary>
        /// This method removes all object from the list.
        /// </summary>
        public void ResetData() => _objects = new List<MonoAdvUpdate>();
        #endregion

        /// <summary>
        /// This method updates the active object.
        /// </summary>
        private void UpdateObject() { if (_objects[_pointer = _pointer + 1 >= _objects.Count ? 0 : _pointer + 1].IsActive()) _objects[_pointer].UpdateObject(); }
    }
}