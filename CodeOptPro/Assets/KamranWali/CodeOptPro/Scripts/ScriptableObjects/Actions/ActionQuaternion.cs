using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "ActionQuaternion",
                     menuName = "CodeOptPro/ScriptableObjects/Actions/" +
                                "ActionQuaternion",
                     order = 1)]
    public class ActionQuaternion : BaseAction<Quaternion> { }
}