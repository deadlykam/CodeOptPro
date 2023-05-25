using KamranWali.CodeOptPro.Managers;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class AwakeStartSetup : EditorWindow
    {
        private AwakeStartManager[] _managers;
        private BaseAwakeStart[] _objects;

        private int _counter;

        [MenuItem("KamranWali/CodeOptPro")]
        private static void Init()
        {
            AwakeStartSetup window = (AwakeStartSetup)EditorWindow.GetWindow(typeof(AwakeStartSetup)); // Setting the window
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
            _managers = FindObjectsByType<AwakeStartManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _objects = FindObjectsByType<BaseAwakeStart>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            for (_counter = 0; _counter < _managers.Length; _counter++) // Loop for initializing the awake start manager
            {
                _managers[_counter].Init(); // Initializing managers
                _managers[_counter].ResetData(); // Resetting data
            }

            for (_counter = 0; _counter < _objects.Length; _counter++) _objects[_counter].Init(); // Initializing objects
        }
    }
}