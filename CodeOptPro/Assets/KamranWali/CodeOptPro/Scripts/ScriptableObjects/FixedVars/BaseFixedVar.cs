using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.FixedVars
{
    /// <summary>
    /// Extend this generic class to create a new variable type. The data type will be based on the generic type provided.
    /// </summary>
    /// <typeparam name="T">The type of variable to created, of type <typeparamref name="T"/></typeparam>
    public abstract class BaseFixedVar<T> : BaseScriptableObject
    {
        [SerializeField] protected T value;

        /// <summary>
        /// This method gets the value of the fixed variable.
        /// </summary>
        /// <returns>The fixed variable value, of type <typeparamref name="T"/></returns>
        public virtual T Get() => value;

        protected override string GetToStringValues() => value.ToString();
    }
}