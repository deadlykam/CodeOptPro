using KamranWali.CodeOptPro.CustomClasses;
using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class FolderSelection : BaseCodeOptPro
    {
        [SerializeField] private FolderSelectSettings _settings;
        private Object _obj;
        private string _name_Add;
        private string _path_Add;

        private readonly string _addFolderToolTip = "Add new folder selectors by giving a name and the path of the folder. " +
                                                    "To find the path of the folder simply go the the folder in Project View " +
                                                    " and then right click in the empty place of the folder and select " +
                                                    "'Copy Path'. Paste that folder path in the path field below.";

        private readonly string _nameAddToolTip = "Give the name for the folder path. It could be any name and does not have to " +
                                                  "be same as the folder name.";

        private readonly string _pathAddToolTip = "Give the path location of the folder to be selected. To find the path of the " +
                                                  "folder simply go to the folder in Project View and then right click in the " +
                                                  "empty place of the folder and select 'Copy Path'. Paste that folder path here.";

        private readonly string _optionsToolTip = "Select the mode type. Edit Mode will allow you to edit the stored folder data " +
                                                  "and remove them. Select Mode will allow you to select the stored folders.";

        private List<FolderSelectData> _data;
        private string[] _names;
        private GUIStyle _styleTextArea;
        private int _index, _index2;
        private string[] _options = new string[] { "Edit Mode", "Select Mode" };
        private int _selOption = 1, _selGrid = -1, _gridColumns = 3;
        private bool _isUpdateNames = false;
        private FolderSelectData _swap;

        [MenuItem("KamranWali/CodeOptPro/Folder Selection Palette")]
        private static void Init()
        {
            FolderSelection window = GetWindow<FolderSelection>();
            window.titleContent = new GUIContent("Folder Selection Palette");
            window.Show();
        }

        protected override void InitInput()
        {
            if (_data == null) SetupData(); // Condition to set up data
            if (_styleTextArea == null) _styleTextArea = new GUIStyle(EditorStyles.textArea); // Initializing text area style

            EditorGUILayout.BeginVertical("Box");
            GUI.skin.label.fontSize = 20;
            GUILayout.Label(new GUIContent("Add Folder Properties", _addFolderToolTip));
            _name_Add = EditorGUILayout.TextField(new GUIContent("Name", _nameAddToolTip), _name_Add);
            EditorGUILayout.BeginHorizontal();
            _styleTextArea.wordWrap = true;
            EditorGUILayout.LabelField(new GUIContent("Path", _pathAddToolTip));
            _path_Add = EditorGUILayout.TextArea(_path_Add, _styleTextArea);
            EditorGUILayout.EndHorizontal();

            if (ValidateAdd()) // Validating the add input
            {
                if (GUILayout.Button("ADD FOLDER")) // Adding new folder
                {
                    _settings.AddFolder(_name_Add, _path_Add);
                    _data.Add(_settings.GetFolder(_settings.GetSize() - 1)); // Adding the newly added data
                    DirtyingSettings();
                    SetLog($"Added new folder name: {_name_Add}, path: {_path_Add}");
                }
            }
            else // Showing disabled add folder button
            {
                EditorGUI.BeginDisabledGroup(true);
                if (GUILayout.Button("ADD FOLDER")) { }
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.EndVertical();

            if (_settings.GetSize() != 0) // Condition to show the mode type
            {
                _selOption = EditorGUILayout.Popup(new GUIContent("Mode Type", _optionsToolTip), _selOption, _options);

                if (_selOption == 0)
                {
                    if (GUILayout.Button("SAVE"))
                    {
                        SetLog("Settings Saved Successfully!");
                        DirtyingSettings(); // Saving by dirtying the object
                    }
                }
            }
            else // Showing disabled options
            {
                EditorGUI.BeginDisabledGroup(true);
                _selOption = EditorGUILayout.Popup(new GUIContent("Mode Type", _optionsToolTip), _selOption, _options);
                EditorGUI.EndDisabledGroup();
            }
        }

        protected override void InitInput_Scroll()
        {
            if (_settings.GetSize() != 0) // Checking if there is at least 1 data available
            {
                if (_isUpdateNames) _isUpdateNames = false; // Names NOT updated
                

                if (_selOption == 0) // Edit Mode
                {
                    for (_index2 = 0; _index2 < _data.Count; _index2++)
                    {
                        EditorGUILayout.BeginVertical("Box");
                        _data[_index2].name = EditorGUILayout.TextField(new GUIContent("Name", _nameAddToolTip), _data[_index2].name);
                        EditorGUILayout.BeginHorizontal();
                        _styleTextArea.wordWrap = true;
                        EditorGUILayout.LabelField(new GUIContent("Path", _pathAddToolTip));
                        _data[_index2].path = EditorGUILayout.TextArea(_data[_index2].path, _styleTextArea);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        
                        if(_index2 != 0) { if (GUILayout.Button("MOVE UP")) MoveUp(_index2); } // Moving up
                        else // Disabling move up
                        {
                            EditorGUI.BeginDisabledGroup(true);
                            if (GUILayout.Button("MOVE UP")) { }
                            EditorGUI.EndDisabledGroup();
                        }

                        if (_index2 != (_data.Count - 1)) { if (GUILayout.Button("MOVE DOWN")) MoveDown(_index2); } // Moving down
                        else // Disabling move down
                        {
                            EditorGUI.BeginDisabledGroup(true);
                            if (GUILayout.Button("MOVE DOWN")) { }
                            EditorGUI.EndDisabledGroup();
                        }

                        if (GUILayout.Button("DELETE")) RemoveData(_index2); // Remove data
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                    }
                }
                else if (_selOption == 1) // Select Mode
                {
                    if (!_isUpdateNames) // Condition for settig up the names
                    {
                        SetupNames(); // Setting up the names
                        _isUpdateNames = true; // Names updated
                    }

                    _selGrid = GUILayout.SelectionGrid(_selGrid, _names, _gridColumns);
                    if (_selGrid != -1) SelectFolder(_data[_selGrid].path); // Condition to select a folder
                    _selGrid = -1;
                }
            }
        }

        /// <summary>
        /// This method selects the choosen folder and highlights it.
        /// </summary>
        /// <param name="path">The path of the folder to select, of type string</param>
        private void SelectFolder(string path)
        {
            _obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            Selection.activeObject = _obj;
            EditorGUIUtility.PingObject(_obj);
            _obj = null;
        }

        /// <summary>
        /// This method checks if the add inputs.
        /// </summary>
        /// <returns>True means the inputs are ok, false otherwise, of type bool</returns>
        private bool ValidateAdd() => !string.IsNullOrWhiteSpace(_name_Add) && !string.IsNullOrWhiteSpace(_path_Add);

        /// <summary>
        /// This method sets up the data.
        /// </summary>
        private void SetupData() 
        { 
            if (_data == null) _data = new List<FolderSelectData>();
            for (_index = 0; _index < _settings.GetSize(); _index++) _data.Add(_settings.GetFolder(_index));
        }
    
        /// <summary>
        /// This method sets up the name data.
        /// </summary>
        private void SetupNames()
        {
            _names = new string[_data.Count]; // Initializing array
            for (_index = 0; _index < _names.Length; _index++) _names[_index] = _data[_index].name; // Copying all the names
        }

        /// <summary>
        /// This method swaps two elements.
        /// </summary>
        /// <param name="a">The index of the element 1 to be swapped, of type int</param>
        /// <param name="b">The index of the element 2 to be swapped, of type int</param>
        private void SwapData(int a, int b)
        {
            _swap = _data[a];
            _data[a] = _data[b];
            _data[b] = _swap;
            _swap = null;
        }

        /// <summary>
        /// This method moves the data up.
        /// </summary>
        /// <param name="index">The indexth data to move up, of type int</param>
        private void MoveUp(int index)
        {
            SetLog($"Moved {_data[index].name} Up and Settings Saved Successfully!");
            _settings.SwapFolderUp(index);
            SwapData(index - 1, index);
            DirtyingSettings();
        }

        /// <summary>
        /// This method moves the data down.
        /// </summary>
        /// <param name="index">The indexth data to move down, of type int</param>
        private void MoveDown(int index)
        {
            SetLog($"Moved {_data[index].name} Down and Settings Saved Successfully!");
            _settings.SwapFolderDown(index);
            SwapData(index + 1, index);
            DirtyingSettings();
        }

        /// <summary>
        /// This method removes a data.
        /// </summary>
        /// <param name="index">The indexth data to remove, of type int</param>
        private void RemoveData(int index)
        {
            SetLog($"Successfully removed {_data[index].name} and Saved Settings!");
            _settings.RemoveFolder(index);
            _data.RemoveAt(index);
            DirtyingSettings();
        }

        /// <summary>
        /// This method dirtys the settings.
        /// </summary>
        private void DirtyingSettings()
        {
            EditorUtility.SetDirty(_settings);
            Undo.RecordObject(_settings, "Folder Settings Updated");
        }
    }
}