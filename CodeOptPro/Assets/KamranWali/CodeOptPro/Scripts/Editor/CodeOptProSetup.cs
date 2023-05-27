using KamranWali.CodeOptPro.Managers;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class CodeOptProSetup : EditorWindow
    {
        private MonoAdvManager_Call _managerCaller;
        private MonoAdvManager[] _managers;
        private UpdateManagerLocal[] _ums_Local;
        private UpdateManagerGlobal[] _ums_Global;
        private MonoAdv[] _objects;

        private int _counter;
        private string _log;

        [MenuItem("KamranWali/CodeOptPro")]
        private static void Init()
        {
            CodeOptProSetup window = (CodeOptProSetup)EditorWindow.GetWindow(typeof(CodeOptProSetup)); // Setting the window
            window.Show(); // Opening the window
        }

        private void OnGUI() 
        {
            if (GUILayout.Button("Setup")) Setup();
            EditorGUI.BeginDisabledGroup(true);
            _log = EditorGUILayout.TextArea(_log);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// This method sets up the awake start system.
        /// </summary>
        private void Setup()
        {
            _log = "Initializing...";
            WriteToLog("Searching all objects...");
            _managerCaller = FindAnyObjectByType<MonoAdvManager_Call>(FindObjectsInactive.Include);
            _managers = FindObjectsByType<MonoAdvManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _ums_Local = FindObjectsByType<UpdateManagerLocal>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _ums_Global = FindObjectsByType<UpdateManagerGlobal>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _objects = FindObjectsByType<MonoAdv>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            WriteToLog("All objects found.");
            ShowProgressBar("All objects found.", .0f);
            _managerCaller.ResetData();
            ShowProgressBar("Initializing Managers...", .01f);
            WriteToLog("Initializing Managers...");

            for (_counter = 0; _counter < _managers.Length; _counter++) // Loop for initializing the mono adv manager and adding managers to caller
            {
                _managerCaller.AddObject(_managers[_counter]); // Adding the manager to the calling manager
                _managers[_counter].Init(); // Initializing managers
                _managers[_counter].ResetData(); // Resetting data
                ShowProgressBar("Setting MonoAdvManager and Caller...", ((_counter / _managers.Length) * .14f) + .01f);

            }

            for (_counter = 0; _counter < _ums_Local.Length; _counter++) // Loop for resetting local Update Managers
            {
                _ums_Local[_counter].ResetData();
                ShowProgressBar("Setting UpdateManagerLocals...", ((_counter / _ums_Local.Length) * .14f) + .15f);
            }

            for (_counter = 0; _counter < _ums_Global.Length; _counter++) // Loop for setting up global Update Managers
            {
                _ums_Global[_counter].Setup(); // Setting up the update managers
                _ums_Global[_counter].ResetData(); // Resetting data
                ShowProgressBar("Setting UpdateManagerGlobals...", ((_counter / _ums_Global.Length) * .14f) + .29f);
            }

            WriteToLog("Setting objects...");
            for (_counter = 0; _counter < _objects.Length; _counter++)
            {
                _objects[_counter].Init(); // Initializing objects
                ShowProgressBar("Adding all objects...", ((_counter / _objects.Length) * .14f) + .43f);
            }

            EditorUtility.SetDirty(_managerCaller); // Dirtying manager caller for save
            ShowProgressBar("Dirtying MonoAdvManager_Call", .58f);

            for (_counter = 0; _counter < _managers.Length; _counter++)
            {
                EditorUtility.SetDirty(_managers[_counter]); // Dirtying managers for save
                ShowProgressBar("Dirtying All MonoAdvManagers...", ((_counter / _objects.Length) * .14f) + .58f);
            }

            for (_counter = 0; _counter < _ums_Local.Length; _counter++)
            {
                EditorUtility.SetDirty(_ums_Local[_counter]); // Dirtying local update managers for save
                ShowProgressBar("Dirtying All UpdateManagerLocals...", ((_counter / _objects.Length) * .14f) + .72f);
            }

            for (_counter = 0; _counter < _ums_Global.Length; _counter++)
            {
                EditorUtility.SetDirty(_ums_Global[_counter]); // Dirtying global update manager for save
                ShowProgressBar("Dirtying All UpdateManagerGlobals...", ((_counter / _objects.Length) * .14f) + .86f);
            }

            WriteToLog("Setup Completed Successfully!");
            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// This method shows the progress bar.
        /// </summary>
        /// <param name="msg">The message to show</param>
        /// <param name="value">The value of the bar, range 0f - 1f, of type float</param>
        private void ShowProgressBar(string msg, float value) => EditorUtility.DisplayProgressBar("Setting up CodeOptPro", msg, value);

        /// <summary>
        /// This method writes to log.
        /// </summary>
        /// <param name="msg">The message to write, of type string</param>
        protected void WriteToLog(string msg) => _log += $"\n{msg}";
    }
}