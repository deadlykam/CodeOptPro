using KamranWali.CodeOptPro.Managers;
using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class CodeOptProSetup : EditorWindow
    {
        [SerializeField] private CodeOptProSettings _settings;
        private MonoAdvManager_Call _managerCaller;
        private MonoAdvManager[] _managers;
        private UpdateManagerLocal[] _ums_Local;
        private UpdateManagerGlobal[] _ums_Global;
        private MonoAdv[] _objects;

        private int _counter;
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
        private readonly string _setupButtonToolTip = "For manually calling manager setup. Use this button if auto setup is" +
            " disabled.";
        private readonly string _autoSetupToolTip = "If enabled then will do auto setup when entering play mode or when" +
            " play button is pressed. If disabled then it is suggested to use the 'Setup' button for setting up the objects.";
        private readonly string _autoSaveToolTip = "If enabled then will auto save the scene when entering play mode or when" +
            " play button is pressed. If disabled then it is suggested to save the scene manually after exiting the play mode so that" +
            " all the objects added to the managers are saved for later use.";

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

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            EditorGUILayout.BeginVertical("Box");
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Editor Settings");
            _settings.SetIsAutoSetup(EditorGUILayout.Toggle(new GUIContent("Enable Auto Setup", _autoSetupToolTip), _settings.IsAutoSetup()));
            _settings.SetIsAutoSave(EditorGUILayout.Toggle(new GUIContent("Enable Auto Save", _autoSaveToolTip), _settings.IsAutoSave()));
            UpdateSettings(); // Saving settings
            EditorGUILayout.EndVertical();

            if (!_settings.IsAutoSetup()) // Condition to show manual setup
            {
                EditorGUILayout.BeginVertical("Box");
                GUI.skin.label.fontSize = 20;
                GUILayout.Label("Manual Setup");
                if (GUILayout.Button(new GUIContent("SETUP", _setupButtonToolTip))) Setup();
                EditorGUILayout.EndVertical();
            }

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
        /// This method updates the settings for the CodeOptProSetupAuto.
        /// </summary>
        private void UpdateSettings()
        {
            if (CodeOptProSetupAuto.IsAutoSetup() != _settings.IsAutoSetup()) // Condition to update auto setup
            {
                CodeOptProSetupAuto.SetIsAutoSetup(_settings.IsAutoSetup());
                DirtyingSettings();
            }

            if (CodeOptProSetupAuto.IsAutoSave() != _settings.IsAutoSave()) // Condition to update auto save
            {
                CodeOptProSetupAuto.SetIsAutoSave(_settings.IsAutoSave());
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