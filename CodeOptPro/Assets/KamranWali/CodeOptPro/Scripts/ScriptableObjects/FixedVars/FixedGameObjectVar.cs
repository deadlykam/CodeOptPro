using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedGameObjectVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedGameObjectVar",
                     order = 1)]
    public class FixedGameObjectVar : BaseFixedVar<GameObject> { }
}