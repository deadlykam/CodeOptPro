using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedBoolVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedBoolVar",
                     order = 1)]
    public class FixedBoolVar : BaseFixedVar<bool> { }
}