using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "FloatVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "FloatVar",
                     order = 1)]
    public class FloatVar : BaseVar<float> { }
}