using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "BoolVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "BoolVar",
                     order = 1)]
    public class BoolVar : BaseVar<bool> { }
}