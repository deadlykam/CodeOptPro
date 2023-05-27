using KamranWali.CodeOptPro.Managers;
using UnityEngine;

namespace KamranWali.CodeOptPro
{
    public class TestScript2 : MonoAdvUpdateLocal
    {
        [Header("TestScript2 Local Properties")]
        [SerializeField] private Transform _objToMove;
        [SerializeField] private float speed;

        public override void AwakeAdv()
        {
            Debug.Log($"Awake -> {gameObject.name}");
        }

        public override bool IsActive() => gameObject.activeSelf;

        public override void SetActive(bool isActivate) => gameObject.SetActive(isActivate);

        public override void StartAdv()
        {
            Debug.Log($"Start -> {gameObject.name}");
        }

        public override void UpdateObject()
        {
            _objToMove.Translate(Vector3.forward * speed * updateManager.GetTime());
        }
    }
}