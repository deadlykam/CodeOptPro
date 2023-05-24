using System.Collections.Generic;
using UnityEngine;

namespace CodeOptPro.Managers
{
    public class AwakeStartManager : MonoBehaviour
    {
        [Header("AwakeStartManager Local Properties")]
        [SerializeField] private List<BaseAwakeStart> _objs;

        private int _counter;

        private void Awake() { for (_counter = 0; _counter < _objs.Count; _counter++) _objs[_counter].AwakeAdv(); } // Calling all awakes
        
    }
}