using UnityEngine;

namespace KamranWali.CodeOptPro.Maths
{
    public abstract class Vec3
    {
        /// <summary>
        /// This method calculates the squared distance between
        /// two points.
        /// </summary>
        /// <param name="source">The source point, of type Vector3</param>
        /// <param name="target">The target point, of type Vector3</param>
        /// <returns>The squared distance, of type float</returns>
        public static float Distance(Vector3 source, Vector3 target)
        {
            target.Set(source.x - target.x,
                       source.y - target.y,
                       source.z - target.z);
            return target.sqrMagnitude;
        }

        /// <summary>
        /// This method substracts two Vector3 values.
        /// </summary>
        /// <param name="value1">The vector to subtract from, of type Vector3</param>
        /// <param name="value2">The vector value to substract, of type Vector3</param>
        /// <returns>The subtracted vector value, of type Vector3</returns>
        public static Vector3 Subtract(Vector3 value1, Vector3 value2)
        {
            value1.Set(value1.x - value2.x,
                       value1.y - value2.y,
                       value1.z - value2.z);
            return value1;
        }

        /// <summary>
        /// This method adds two Vector3 values.
        /// </summary>
        /// <param name="value1">The vector to add, of type Vector3</param>
        /// <param name="value2">The vector to add, of type Vector3</param>
        /// <returns>The added vector value, of type Vector3</returns>
        public static Vector3 Add(Vector3 value1, Vector3 value2)
        {
            value1.Set(value1.x + value2.x,
                       value1.y + value2.y,
                       value1.z + value2.z);
            return value1;
        }

        /// <summary>
        /// This method divides a vector with a given value.
        /// </summary>
        /// <param name="vector">The vector to divide, of type Vector3</param>
        /// <param name="value">The value to divide the vector by, of type float</param>
        /// <returns>The divided vector, of type Vector3</returns>
        public static Vector3 Divide(Vector3 vector, float value)
        {
            vector.Set(vector.x / value,
                       vector.y / value,
                       vector.z / value);
            return vector;
        }

        /// <summary>
        /// This method multiplys a vector with a given value.
        /// </summary>
        /// <param name="vector">The vector to multiply, of type Vector3</param>
        /// <param name="value">The value to multiply the vector by, of type float</param>
        /// <returns>The multiplied vector, of type Vector3</returns>
        public static Vector3 Multiply(Vector3 vector, float value)
        {
            vector.Set(vector.x * value,
                       vector.y * value,
                       vector.z * value);
            return vector;
        }
    }
}