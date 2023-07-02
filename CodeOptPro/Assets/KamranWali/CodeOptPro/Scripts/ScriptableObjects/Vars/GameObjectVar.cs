using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "GameObjectVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "GameObjectVar",
                     order = 1)]
    public class GameObjectVar : BaseVar<GameObject> { }
}