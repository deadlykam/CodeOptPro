using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CodeOptProSettings",
                     menuName = "CodeOptPro/ScriptableObjects/Managers/" +
                                "CodeOptProSettings",
                     order = 1)]
    public class CodeOptProSettings : ScriptableObject
    {
        [SerializeField] private bool _isAutoSetup;
        [SerializeField] private bool _isAutoSave;

        public bool IsAutoSetup() => _isAutoSetup;
        public bool IsAutoSave() => _isAutoSave;
        public void SetIsAutoSetup(bool value) => _isAutoSetup = value;
        public void SetIsAutoSave(bool value) => _isAutoSave = value;
    }
}