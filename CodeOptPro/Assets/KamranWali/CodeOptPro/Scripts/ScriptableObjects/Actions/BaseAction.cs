using System;

namespace KamranWali.CodeOptPro.ScriptableObjects.Actions
{
    /// <summary>
    /// Extend this generic class to create a new Action type. The Action type will be based on the generic type provided.
    /// </summary>
    /// <typeparam name="T">The type of Action to created, of type Action<<typeparamref name="T"/>></typeparam>
    public abstract class BaseAction<T> : BaseScriptableObject
    {
        protected Action<T> action;

        /// <summary>
        /// This method sets the Action delegate.
        /// </summary>
        /// <param name="action">The Action delegate to set, of type Action<<typeparamref name="T"/>></param>
        public virtual void Set(Action<T> action) => this.action = action;

        /// <summary>
        /// This method calls the Action delegate.
        /// </summary>
        /// <param name="value">Calling the Action delegate by sending the value, of type <typeparamref name="T"/></param>
        public virtual void Call(T value) => action(value);

        protected override string GetToStringValues() => action.ToString();
    }
}