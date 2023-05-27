using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEngine;

namespace KamranWali.CodeOptPro.Managers
{
    public class MonoAdvManager_Call : MonoBehaviour
    {
        [Header("AwakeStartManager_Call Local Properties")]
        [SerializeField] private AwakeStartManagerHelper[] _managers;

        private int _counter;

        private void Start() 
        {
            for (_counter = 0; _counter < _managers.Length; _counter++) _managers[_counter].AwakeAdv(); // Calling all manager awake
            for (_counter = 0; _counter < _managers.Length; _counter++) _managers[_counter].StartAdv(); // Calling all manager start
        }
    }
}