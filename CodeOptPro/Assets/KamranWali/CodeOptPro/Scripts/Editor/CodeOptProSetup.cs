using KamranWali.CodeOptPro.Managers;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class CodeOptProSetup : EditorWindow
    {
        private MonoAdvManager[] _managers;
        private MonoAdv[] _objects;

        private int _counter;

        [MenuItem("KamranWali/CodeOptPro")]
        private static void Init()
        {
            CodeOptProSetup window = (CodeOptProSetup)EditorWindow.GetWindow(typeof(CodeOptProSetup)); // Setting the window
            window.Show(); // Opening the window
        }

        private void OnGUI() 
        {
            if (GUILayout.Button("Setup")) Setup();
        }

        /// <summary>
        /// This method sets up the awake start system.
        /// </summary>
        private void Setup()
        {
            _managers = FindObjectsByType<MonoAdvManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _objects = FindObjectsByType<MonoAdv>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            for (_counter = 0; _counter < _managers.Length; _counter++) // Loop for initializing the mono adv manager
            {
                _managers[_counter].Init(); // Initializing managers
                _managers[_counter].ResetData(); // Resetting data
            }

            for (_counter = 0; _counter < _objects.Length; _counter++) _objects[_counter].Init(); // Initializing objects
            for (_counter = 0; _counter < _managers.Length; _counter++) EditorUtility.SetDirty(_managers[_counter]); // Dirtying managers for save
        }
    }
}