using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedFloatVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedFloatVar",
                     order = 1)]
    public class FixedFloatVar : BaseFixedVar<float> { }
}