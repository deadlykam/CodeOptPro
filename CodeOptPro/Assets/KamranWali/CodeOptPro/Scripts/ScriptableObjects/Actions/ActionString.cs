using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "ActionString",
                     menuName = "CodeOptPro/ScriptableObjects/Actions/" +
                                "ActionString",
                     order = 1)]
    public class ActionString : BaseAction<string> { }
}