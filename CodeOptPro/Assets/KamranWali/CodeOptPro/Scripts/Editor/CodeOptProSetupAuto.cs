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
        private static int _counter;
        private static bool _isAutoSetup;
        private static bool _isAutoSave;

        static CodeOptProSetupAuto() => EditorApplication.playModeStateChanged += OnPlayModeStateChange;

        public static void SetIsAutoSetup(bool value) => _isAutoSetup = value;
        public static void SetIsAutoSave(bool value) => _isAutoSave = value;
        public static bool IsAutoSetup() => _isAutoSetup;
        public static bool IsAutoSave() => _isAutoSave;

        /// <summary>
        /// This method calls the setup on play state change.
        /// </summary>
        /// <param name="state">The state to check if exiting edit mode, of type PlayModeStateChange</param>
        private static void OnPlayModeStateChange(PlayModeStateChange state) 
        {
            if (state == PlayModeStateChange.ExitingEditMode) // Condition to execute during pre play mode
            {
                if (_isAutoSetup) Setup(); // Condition for auto setup
                if (_isAutoSave) AutoSaveScene(); // Condition for auto save
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
            //TODO: Initialize the manager helper list here and later on do the validations and the whole list
            ShowProgressBar("All objects found.", .0f);
            _managerCaller.ResetData();
            ShowProgressBar("Initializing Managers...", .01f);

            for (_counter = 0; _counter < _managers.Length; _counter++) // Loop for initializing the mono adv manager and adding managers to caller
            {
                _managerCaller.AddObject(_managers[_counter]); // Adding the manager to the calling manager
                _managers[_counter].Init(); // Initializing managers
                _managers[_counter].ResetData(); // Resetting data
                _managerCaller.AddManagerHelper(_managers[_counter].GetManagerHelper());
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

            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// This method saves the scene automatically.
        /// </summary>
        private static void AutoSaveScene() => EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        /// <summary>
        /// This method shows the progress bar.
        /// </summary>
        /// <param name="msg">The message to show</param>
        /// <param name="value">The value of the bar, range 0f - 1f, of type float</param>
        private static void ShowProgressBar(string msg, float value) => EditorUtility.DisplayProgressBar("Setting up CodeOptPro", msg, value);
    }
}