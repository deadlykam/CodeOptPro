using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedQuaternionVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedQuaternionVar",
                     order = 1)]
    public class FixedQuaternionVar : BaseFixedVar<Quaternion> { }
}