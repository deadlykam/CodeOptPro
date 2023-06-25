using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    public abstract class BaseScriptableObject : ScriptableObject
    {
        /// <summary>
        /// This method gets the ToString value which will be shown in the ToString() method.
        /// </summary>
        /// <returns>The ToString() value to get, of type string</returns>
        protected abstract string GetToStringValues();

        public override string ToString() => $"{base.ToString()}: {GetToStringValues()}";
    }
}