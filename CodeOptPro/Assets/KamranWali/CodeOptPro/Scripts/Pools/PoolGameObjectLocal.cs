using UnityEngine;

namespace KamranWali.CodeOptPro.Pools
{
    public class PoolGameObjectLocal : BasePoolLocal<GameObject>
    {
        protected override void SetupObjectPools()
        {
            for (indexObj = 0; indexObj < poolObjects.Length; indexObj++) // Loop for populating the pool array with game objects
                poolObjects[indexObj] = transform.GetChild(indexObj).gameObject;
        }
    }
}