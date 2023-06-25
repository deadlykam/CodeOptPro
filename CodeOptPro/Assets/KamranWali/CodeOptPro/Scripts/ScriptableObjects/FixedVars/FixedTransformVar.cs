using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedTransformVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedTransformVar",
                     order = 1)]
    public class FixedTransformVar : BaseFixedVar<Transform> { }
}