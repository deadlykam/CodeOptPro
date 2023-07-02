using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "ActionGameObject",
                     menuName = "CodeOptPro/ScriptableObjects/Actions/" +
                                "ActionGameObject",
                     order = 1)]
    public class ActionGameObject : BaseAction<GameObject> { }
}