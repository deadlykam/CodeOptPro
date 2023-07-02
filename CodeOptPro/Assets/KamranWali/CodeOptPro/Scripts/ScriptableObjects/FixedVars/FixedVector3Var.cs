using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedVector3Var",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedVector3Var",
                     order = 1)]
    public class FixedVector3Var : BaseFixedVar<Vector3> { }
}