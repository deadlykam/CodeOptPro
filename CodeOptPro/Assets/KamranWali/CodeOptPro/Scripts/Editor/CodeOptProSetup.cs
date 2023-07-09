using KamranWali.CodeOptPro.Managers;
using KamranWali.CodeOptPro.ScriptableObjects;
using KamranWali.CodeOptPro.ScriptableObjects.Managers;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class CodeOptProSetup : BaseCodeOptPro
    {
        [SerializeField] private CodeOptProSettings _settings;
        [SerializeField] private MonoAdvManagerHelper _defaultManager;

        private GameObject _managers_creator;
        private readonly string _managers_name = "Managers";
        private bool _preIsAutoSetup;
        private bool _preIsAutoSave;
        private bool _preIsAutoFixNullMissRef;

        #region Tool Tips
        private readonly string _setupButtonToolTip = "For manually calling manager setup. Use this button if auto setup is" +
            " disabled.";
        private readonly string _autoSetupToolTip = "If enabled then will do auto setup when entering play mode or when" +
            " play button is pressed. If disabled then it is suggested to use the 'Setup' button for setting up the objects.";
        private readonly string _autoSaveToolTip = "If enabled then will auto save the scene when entering play mode or when" +
            " play button is pressed. If disabled then it is suggested to save the scene manually after exiting the play mode so that" +
            " all the objects added to the managers are saved for later use.";
        private readonly string _autoFixNullMissRefToolTip = "If enabled then any missing references stored in the list will be" +
            " removed automatically. It is recommended to keep this enabled but if disabled then user must remove the null/missing" +
            " references manually";
        private readonly string _setupSceneToolTip = "This will setup the scene for using CodeOptPro. If the scene is already setup" +
            " then no changes will be made.";
        #endregion

        [MenuItem("KamranWali/CodeOptPro/CodeOptPro")]
        private static void Init()
        {
            CodeOptProSetup window = GetWindow<CodeOptProSetup>();
            window.titleContent = new GUIContent("CodeOptPro");
            window.Show(); // Opening the window
        }

        protected override void InitInput() 
        {
            if (GUILayout.Button(new GUIContent("SCENE SETUP", _setupSceneToolTip))) SceneSetup();
            EditorGUILayout.BeginVertical("Box");
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Editor Settings");
            _settings.SetIsAutoSetup(EditorGUILayout.ToggleLeft(new GUIContent("Enable Auto Setup", _autoSetupToolTip), _settings.IsAutoSetup()));
            _settings.SetIsAutoSave(EditorGUILayout.ToggleLeft(new GUIContent("Enable Auto Save", _autoSaveToolTip), _settings.IsAutoSave()));
            _settings.SetIsAutoFixNullMissRef(EditorGUILayout.ToggleLeft(new GUIContent("Enable Auto Fix Null/Missing Refs", _autoFixNullMissRefToolTip), _settings.IsAutoFixNullMissRef()));
            UpdateSettings(); // Saving settings
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("Box");
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Manual Setup");
            if (GUILayout.Button(new GUIContent("SETUP", _setupButtonToolTip)))
            {
                SetLog("Initializing...");
                CodeOptProSetupAuto.LoadSettings();
                CodeOptProSetupAuto.Setup();
                WriteToLog("Done!");
            }
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// This method setups the scene for using CodeOptPro.
        /// </summary>
        private void SceneSetup()
        {
            SetLog("Setting up scene for CodeOptPro...");
            _managers_creator = GameObject.Find(_managers_name);

            if (_managers_creator == null)
            {
                _managers_creator = new GameObject(_managers_name);
                _managers_creator.transform.position = Vector3.zero;
                _managers_creator.AddComponent<MonoAdvManager_Call>().SetManagers(new List<MonoAdvManagerHelper> { _defaultManager });
                _managers_creator.AddComponent<MonoAdvManager>();
                WriteToLog("Setup Done!");
            }
            else WriteToLog("Scene already setup for CodeOptPro.");
            _managers_creator = null;
        }

        /// <summary>
        /// This method updates the settings and makes it dirty for saving.
        /// </summary>
        private void UpdateSettings()
        {
            if (_preIsAutoSetup != _settings.IsAutoSetup()) // Condition to update auto setup
            {
                _preIsAutoSetup = _settings.IsAutoSetup();
                DirtyingSettings();
            }

            if (_preIsAutoSave != _settings.IsAutoSave()) // Condition to update auto save
            {
                _preIsAutoSave = _settings.IsAutoSave();
                DirtyingSettings();
            }

            if (_preIsAutoFixNullMissRef != _settings.IsAutoFixNullMissRef()) // Condition to update auto fix null/miss ref
            {
                _preIsAutoFixNullMissRef = _settings.IsAutoFixNullMissRef();
                DirtyingSettings();
            }
        }

        /// <summary>
        /// This method dirtys the setting so that it can be saved.
        /// </summary>
        private void DirtyingSettings()
        {
            EditorUtility.SetDirty(_settings);
            Undo.RecordObject(_settings, "Settings Updated");
        }

        /// <summary>
        /// This method shows the progress bar.
        /// </summary>
        /// <param name="msg">The message to show</param>
        /// <param name="value">The value of the bar, range 0f - 1f, of type float</param>
        private void ShowProgressBar(string msg, float value) => EditorUtility.DisplayProgressBar("Setting up CodeOptPro", msg, value);
    }
}