using KamranWali.CodeOptPro.Managers;
using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class CodeOptProSetup : EditorWindow
    {
        [SerializeField] private CodeOptProSettings _settings;
        [SerializeField] private MonoAdvManagerHelper _defaultManager;

        private GameObject _managers_creator;
        private readonly string _managers_name = "Managers";
        private string _log;
        private bool _isSetLogo;
        private Vector2 _scrollPos;
        private Texture _texLogo;
        private Texture _texLogoName;
        private readonly string _logoPath = "KamranWali/CodeOptPro/Images/CodeOptProLogo_Only_500x651";
        private readonly string _logoNamePath = "KamranWali/CodeOptPro/Images/CodeOptProLogo_Name_500x89";
        private  GUIStyle _versionStyle;
        private readonly int _fontSize = 18;
        private readonly string _version = "Version - v1.0.0";
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

        [MenuItem("KamranWali/CodeOptPro")]
        private static void Init()
        {
            CodeOptProSetup window = (CodeOptProSetup)EditorWindow.GetWindow(typeof(CodeOptProSetup)); // Setting the window
            window.Show(); // Opening the window
        }

        private void OnGUI() 
        {
            if (!_isSetLogo)
            {
                _texLogo = Resources.Load<Texture>(_logoPath);
                _texLogoName = Resources.Load<Texture>(_logoNamePath);
                _versionStyle = new GUIStyle();
                _versionStyle.fontSize = _fontSize;
                _versionStyle.normal.textColor = Color.white;
                _isSetLogo = true;
            }

            if (GUILayout.Button(new GUIContent("SCENE SETUP", _setupSceneToolTip))) SceneSetup();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
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
                _log = "Initializing...";
                CodeOptProSetupAuto.LoadSettings();
                CodeOptProSetupAuto.Setup();
                WriteToLog("Done!");
            }
            EditorGUILayout.EndVertical();
            
            EditorGUI.BeginDisabledGroup(true);
            _log = EditorGUILayout.TextArea(_log);
            EditorGUI.EndDisabledGroup();

            if (_isSetLogo) // Condition to show the logo
            {
                GUILayout.Space(30f);
                GUILayout.Box(_texLogo, new GUILayoutOption[] { GUILayout.Width(100f), GUILayout.Height(130.2f), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false) });
                GUILayout.Box(_texLogoName, new GUILayoutOption[] { GUILayout.Width(200f), GUILayout.Height(35.6f), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false) });
                GUILayout.Space(10f);
                GUILayout.BeginHorizontal();
                GUILayout.Space(5f);
                EditorGUILayout.LabelField(_version, _versionStyle);
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// This method setups the scene for using CodeOptPro.
        /// </summary>
        private void SceneSetup()
        {
            _log = "Setting up scene for CodeOptPro...";
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

        /// <summary>
        /// This method writes to log.
        /// </summary>
        /// <param name="msg">The message to write, of type string</param>
        protected void WriteToLog(string msg) => _log += $"\n{msg}";
    }
}