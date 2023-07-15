using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    [CreateAssetMenu(fileName = "CameraVar",
                     menuName = "CodeOptPro/ScriptableObjects/Vars/" +
                                "CameraVar",
                     order = 1)]
    public class CameraVar : BaseVar<Camera> { }
}