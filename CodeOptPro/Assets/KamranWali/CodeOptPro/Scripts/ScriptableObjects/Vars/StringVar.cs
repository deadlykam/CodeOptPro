using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "StringVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "StringVar",
                     order = 1)]
    public class StringVar : BaseVar<string> { }
}