using UnityEngine;

namespace KamranWali.CodeOptPro.Pools
{
    public class PoolTransformLocal : BasePoolLocal<Transform>
    {
        protected override void SetupObjectPools()
        {
            for (indexObj = 0; indexObj < poolObjects.Length; indexObj++) // Loop for populating the pool array with transform
                poolObjects[indexObj] = transform.GetChild(indexObj);
        }
    }
}