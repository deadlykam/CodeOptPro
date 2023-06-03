using UnityEngine;
using UnityEditor;
using KamranWali.CodeOptPro.Managers;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using KamranWali.CodeOptPro.ScriptableObjects;

namespace KamranWali.CodeOptPro.Editor
{
    [InitializeOnLoad]
    public static class CodeOptProSetupAuto
    {
        private static MonoAdvManager_Call _managerCaller;
        private static MonoAdvManager[] _managers;
        private static UpdateManagerLocal[] _ums_Local;
        private static UpdateManagerGlobal[] _ums_Global;
        private static MonoAdv[] _objects;
        private static List<MonoAdvManagerHelper> _managerHelpers;
        private static List<MonoAdvManagerHelper> _checkHelpers;
        private static CodeOptProSettings _settings;
        private static int _counter;
        private static readonly string _settingsPath = "Assets/KamranWali/CodeOptPro/SO_Data/DefaultCodeOptProSettings.asset";

        static CodeOptProSetupAuto() => EditorApplication.playModeStateChanged += OnPlayModeStateChange;

        /// <summary>
        /// This method calls the setup on play state change.
        /// </summary>
        /// <param name="state">The state to check if exiting edit mode, of type PlayModeStateChange</param>
        private static void OnPlayModeStateChange(PlayModeStateChange state) 
        {
            if (state == PlayModeStateChange.ExitingEditMode) // Condition to execute during pre play mode
            {
                LoadSettings();
                if (_settings.IsAutoSetup()) Setup(); // Condition for auto setup
                if (_settings.IsAutoSave()) AutoSaveScene(); // Condition for auto save
            }
        }

        /// <summary>
        /// This method sets up the awake start system.
        /// </summary>
        public static void Setup()
        {
            _managerCaller = EditorWindow.FindAnyObjectByType<MonoAdvManager_Call>(FindObjectsInactive.Include);
            _managers = EditorWindow.FindObjectsByType<MonoAdvManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _ums_Local = EditorWindow.FindObjectsByType<UpdateManagerLocal>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _ums_Global = EditorWindow.FindObjectsByType<UpdateManagerGlobal>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _objects = EditorWindow.FindObjectsByType<MonoAdv>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _managerHelpers = _managerCaller.GetManagers();
            _checkHelpers = new List<MonoAdvManagerHelper>();
            ShowProgressBar("All objects found.", .0f);
            _managerCaller.ResetData();
            ShowProgressBar("Initializing Managers...", .01f);

            if (_settings.IsAutoFixNullMissRef()) // Condition to auto fix null and missing reference
            {
                for (_counter = 0; _counter < _managerHelpers.Count; _counter++) // Loop for removing null or missing references
                {
                    if (_managerHelpers[_counter] == null) // Condition to check if the reference is missing or null
                    {
                        _managerHelpers.RemoveAt(_counter); // Removing the missing or null reference
                        _counter--; // Helping counter to point correctly
                    }
                }
            }

            for (_counter = 0; _counter < _managers.Length; _counter++) // Loop for initializing the mono adv manager and adding managers to caller
            {
                _managerCaller.AddObject(_managers[_counter]); // Adding the manager to the calling manager

                if (!_managers[_counter].HasManager()) // Condition to stop the system process
                {
                    ShowMRDialog("Missing Reference In MonoAdvManager", _managers[_counter].gameObject);
                    return; // Stop process here until error is fixed
                }

                _managers[_counter].Init(); // Initializing managers
                _managers[_counter].ResetData(); // Resetting data
                if (!_managerHelpers.Contains(_managers[_counter].GetManagerHelper())) _managerHelpers.Add(_managers[_counter].GetManagerHelper()); // Adding any newly added helper
                if (!_checkHelpers.Contains(_managers[_counter].GetManagerHelper())) _checkHelpers.Add(_managers[_counter].GetManagerHelper()); // Adding manager helpers which will later be needed for validation
                ShowProgressBar("Setting MonoAdvManager and Caller...", ((_counter / _managers.Length) * .14f) + .01f);

            }

            if (_settings.IsAutoFixNullMissRef()) // Condition to remove any helpers that does NOT exist in scene
            {
                for (_counter = 0; _counter < _managerHelpers.Count; _counter++) // Loop for removing any helpers that are NOT present in the scene
                {
                    if (!_checkHelpers.Contains(_managerHelpers[_counter])) // Condition to check if helper does NOT exist
                    {
                        _managerHelpers.RemoveAt(_counter); // Removing the helper
                        _counter--; // Helping counter to point correctly after removal
                    }
                }
            }

            _managerCaller.SetManagers(_managerHelpers); // Setting the newly populated manager helpers.
            _managerHelpers = null; // Helping with GC
            _checkHelpers = null; // Helping with GC

            for (_counter = 0; _counter < _ums_Local.Length; _counter++) // Loop for resetting local Update Managers
            {
                if (!_ums_Local[_counter].HasManager()) // Condition to stop the system process
                {
                    ShowMRDialog("Missing Reference In UpdateManagerLocal", _ums_Local[_counter].gameObject);
                    return; // Stop process here until error is fixed
                }

                _ums_Local[_counter].ResetData();
                ShowProgressBar("Setting UpdateManagerLocals...", ((_counter / _ums_Local.Length) * .14f) + .15f);
            }

            for (_counter = 0; _counter < _ums_Global.Length; _counter++) // Loop for setting up global Update Managers
            {
                if (!_ums_Global[_counter].HasManager()) // Condition to stop the system process
                {
                    ShowMRDialog("Missing Reference In UpdateManagerGlobal", _ums_Global[_counter].gameObject);
                    return; // Stop process here until error is fixed
                }

                _ums_Global[_counter].Setup(); // Setting up the update managers
                _ums_Global[_counter].ResetData(); // Resetting data
                ShowProgressBar("Setting UpdateManagerGlobals...", ((_counter / _ums_Global.Length) * .14f) + .29f);
            }

            for (_counter = 0; _counter < _objects.Length; _counter++) // Loop for setting up MonoAdv objects
            {
                if (!_objects[_counter].HasManager()) // Condition to stop the system process
                {
                    ShowMRDialog("Missing Reference In MonoAdv/MonoAdvUpdateLocal/MonoAdvUpdateGlobal", _objects[_counter].gameObject);
                    return; // Stop process here until error is fixed
                }

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

            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// This method loads the settings.
        /// </summary>
        public static void LoadSettings() => _settings = AssetDatabase.LoadAssetAtPath(_settingsPath, typeof(CodeOptProSettings)) as CodeOptProSettings; // Loading settings

        /// <summary>
        /// This method saves the scene automatically.
        /// </summary>
        private static void AutoSaveScene() => EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        /// <summary>
        /// This method shows the progress bar.
        /// </summary>
        /// <param name="msg">The message to show</param>
        /// <param name="value">The value of the bar, range 0f - 1f, of type float</param>
        private static void ShowProgressBar(string msg, float value) => EditorUtility.DisplayCancelableProgressBar("Setting up CodeOptPro", msg, value);

        /// <summary>
        /// This method shows the missing reference dialog warning and focuses on the error game object.
        /// </summary>
        /// <param name="title">The title of the dialog popup, of type string</param>
        /// <param name="obj">The gameobject to focus on when popup comes, of type GameObject</param>
        private static void ShowMRDialog(string title, GameObject obj)
        {
            EditorUtility.DisplayDialog(
                        title,
                        $"Missing reference found on GameObject -> {obj.name}. Please fix it. Process is STOPPED!",
                        "Ok",
                        "");
            Selection.activeGameObject = obj; // Selecting the error GameObject
            EditorUtility.ClearProgressBar(); // Closing the progress bar
        }
    }
}