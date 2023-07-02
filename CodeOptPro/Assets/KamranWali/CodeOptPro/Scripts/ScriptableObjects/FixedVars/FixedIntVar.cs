using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedIntVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedIntVar",
                     order = 1)]
    public class FixedIntVar : BaseFixedVar<int> { }
}