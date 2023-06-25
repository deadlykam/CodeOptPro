namespace KamranWali.CodeOptPro.ScriptableObjects.Vars
{
    /// <summary>
    /// Extend this generic class to create a new variable type. The data type will be based on the generic type provided.
    /// </summary>
    /// <typeparam name="T">The type of variable to created, of type <typeparamref name="T"/></typeparam>
    public abstract class BaseVar<T> : BaseScriptableObject
    {
        protected T value;

        /// <summary>
        /// This method returns the value of the variable.
        /// </summary>
        /// <returns>The value of the variable, of type <typeparamref name="T"/></returns>
        public virtual T Get() => value;

        /// <summary>
        /// This method sets the value of the variable.
        /// </summary>
        /// <param name="value">The value to set, of type <typeparamref name="T"/></param>
        public virtual void Set(T value) => this.value = value;

        protected override string GetToStringValues() => value.ToString();
    }
}