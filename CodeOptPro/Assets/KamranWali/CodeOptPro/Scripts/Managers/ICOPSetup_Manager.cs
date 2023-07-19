using KamranWali.CodeOptPro.ScriptableObjects.Managers;

namespace KamranWali.CodeOptPro.Managers
{
    public interface ICOPSetup_Manager<T> : ICOPSetup<T>
    {
        /// <summary>
        /// This method gets the manager helper, THIS METHOD IS FOR EDITOR ONLY!
        /// </summary>
        /// <returns>The manager helper or the group, of type MonoAdvManagerHelper</returns>
        public MonoAdvManagerHelper GetManagerHelper();

        /// <summary>
        /// This method checks if the given group is same as the current group.
        /// </summary>
        /// <param name="managerHelper">The group to check, of type MonoAdvManager_CallHelper</param>
        /// <returns>True means similar, false otherwise, of type MonoAdvManager_CallHelper</returns>
        public bool IsMonoAdvManager_CallHelper(MonoAdvManager_CallHelper managerHelper);
    }
}