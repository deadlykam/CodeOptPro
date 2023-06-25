using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "IntVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "IntVar",
                     order = 1)]
    public class IntVar : BaseVar<int> { }
}