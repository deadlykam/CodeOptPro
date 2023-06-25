using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "ActionTransform",
                     menuName = "CodeOptPro/ScriptableObjects/Actions/" +
                                "ActionTransform",
                     order = 1)]
    public class ActionTransform : BaseAction<Transform> { }
}