using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedDoubleVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedDoubleVar",
                     order = 1)]
    public class FixedDoubleVar : BaseFixedVar<double> { }
}