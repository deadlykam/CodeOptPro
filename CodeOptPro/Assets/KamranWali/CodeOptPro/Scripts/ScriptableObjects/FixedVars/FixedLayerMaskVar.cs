using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    [CreateAssetMenu(fileName = "FixedLayerMaskVar",
                     menuName = "CodeOptPro/ScriptableObjects/FixedVars/" +
                                "FixedLayerMaskVar",
                     order = 1)]
    public class FixedLayerMaskVar : BaseFixedVar<LayerMask> { }
}