using KamranWali.CodeOptPro.Pools;
using KamranWali.CodeOptPro.ScriptableObjects.Actions;
using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.RequestObjects
{
    public class BaseRequest<T> : BaseAction<IRequestObject<T>> { }
}