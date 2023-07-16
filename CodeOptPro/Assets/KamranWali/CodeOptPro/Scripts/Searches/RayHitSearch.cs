using KamranWali.CodeOptPro.Maths;
using UnityEngine;

namespace KamranWali.CodeOptPro.Searches
{
    public class RayHitSearch : RayHit
    {
        private RaycastHit _hit;
        private int _index;
        private float _hitDist;

        /// <summary>
        /// This constructor initializes the RayHitSearch.
        /// </summary>
        /// <param name="hitSize">The size of the hit array, of type int</param>
        /// <param name="rayRange">The range of the ray, of type float</param>
        /// <param name="hitMask">The hitting layers, of type LayerMask</param>
        public RayHitSearch(int hitSize, float rayRange, LayerMask hitMask) : base(hitSize, rayRange, hitMask) 
        {
            _index = -1;
            _hitDist = -1f;
        }

        public override void CalculateHits()
        {
            base.CalculateHits();
            if (IsHit()) _hit = raycastHit[0]; // Setting the first hit object
        }

        /// <summary>
        /// This method finds the closest hit object.
        /// </summary>
        /// <param name="origin">The origin point from which to measure distance, of type Vector3</param>
        /// <returns>The closest hit object, of type RaycastHit</returns>
        public RaycastHit GetClosestHit(Vector3 origin)
        {
            _hit = raycastHit[0]; // Setting the first hit object
            _hitDist = Vec3.Distance(_hit.point, origin);

            for (_index = 1; _index < numHit; _index++) // Loop for finding the closest hit distance
            {
                if (Vec3.Distance(raycastHit[_index].point, origin) < _hitDist) // Closest hit object found
                {
                    _hit = raycastHit[_index]; // Setting closest dist
                    _hitDist = Vec3.Distance(_hit.point, origin);
                }
            }

            return _hit;
        }

        /// <summary>
        /// This method gets the farthest hit object.
        /// </summary>
        /// <param name="origin">The origin point from which to measure distance, of type Vector3</param>
        /// <returns>The farthest hit object, of type RaycastHit</returns>
        public RaycastHit GetFarthestHit(Vector3 origin)
        {
            _hit = raycastHit[0]; // Setting the first hit object
            _hitDist = Vec3.Distance(_hit.point, origin);

            for (_index = 1; _index < numHit; _index++) // Loop for finding the closest hit distance
            {
                if (Vec3.Distance(raycastHit[_index].point, origin) > _hitDist) // Closest hit object found
                {
                    _hit = raycastHit[_index]; // Setting closest dist
                    _hitDist = Vec3.Distance(_hit.point, origin);
                }
            }

            return _hit;
        }
    }
}