using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.RequestObjects
{
    [CreateAssetMenu(fileName = "RequestGameObject",
                     menuName = "CodeOptPro/ScriptableObjects/RequestObjects/" +
                                "RequestGameObject",
                     order = 1)]
    public class RequestGameObject : BaseRequest<GameObject> { }
}