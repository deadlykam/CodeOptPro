using UnityEngine;

namespace KamranWali.CodeOptPro.Pools
{
    public class PoolTransformGlobal : BasePoolGlobal<Transform>
    {
        protected override void SetupObjectPools()
        {
            for (indexObj = 0; indexObj < poolObjects.Length; indexObj++) // Loop for populating the pool array with transform
                poolObjects[indexObj] = transform.GetChild(indexObj);
        }
    }
}