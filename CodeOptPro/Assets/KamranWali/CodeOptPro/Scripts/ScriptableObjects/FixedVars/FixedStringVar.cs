using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedStringVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedStringVar",
                     order = 1)]
    public class FixedStringVar : BaseFixedVar<string> { }
}