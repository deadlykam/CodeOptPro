using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.RequestObjects
{
    [CreateAssetMenu(fileName = "RequestTransform",
                     menuName = "CodeOptPro/ScriptableObjects/Actions/" +
                                "RequestTransform",
                     order = 1)]
    public class RequestTransform : BaseRequest<Transform> { }
}