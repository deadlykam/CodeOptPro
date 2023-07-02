using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "QuaternionVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "QuaternionVar",
                     order = 1)]
    public class QuaternionVar : BaseVar<Quaternion> { }
}