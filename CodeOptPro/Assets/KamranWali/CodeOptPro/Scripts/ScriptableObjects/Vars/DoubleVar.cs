using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "DoubleVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "DoubleVar",
                     order = 1)]
    public class DoubleVar : BaseVar<double> { }
}