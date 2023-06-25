using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects.Actions
{
    [CreateAssetMenu(fileName = "Action",
                     menuName = "CodeOptPro/ScriptableObjects/Actions/" +
                                "Action",
                     order = 1)]
    public class Action : BaseScriptableObject
    {
        private System.Action _action;

        /// <summary>
        /// This method sets the Action delegate.
        /// </summary>
        /// <param name="action">The Action delegate to set, of type Action</param>
        public void Set(System.Action action) => _action = action;

        /// <summary>
        /// This method calls the Action delegate.
        /// </summary>
        public void Call() => _action();

        protected override string GetToStringValues() => _action.ToString();
    }
}