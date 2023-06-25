using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "TransformVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "TransformVar",
                     order = 1)]
    public class TransformVar : BaseVar<Transform> { }
}