using KamranWali.CodeOptPro.ScriptableObjects;
using KamranWali.CodeOptPro.ScriptableObjects.FixedVars;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class VariableCreator : BaseCodeOptPro
    {
        [SerializeField] private VariablePath _actionPaths;
        [SerializeField] private VariablePath _fixedVarPaths;
        [SerializeField] private VariablePath _varPaths;
        [SerializeField] private FixedStringVar _version;

        private string _name = "VarName";
        private string _path;
        private int _selCate = 0;
        private int _selActions = 0;
        private int _selFixedVar;
        private int _selVar;

        private readonly string[] _categories = new string[] { "Actions", "Fixed Variables", "Variables" };
        private string[] _actions;
        private string[] _fixedVars;
        private string[] _vars;
        private List<string> _tempNames;
        #region ToolTips
        private readonly string _categoryToolTip = "Select the variable category";
        private readonly string _actionToolTip = "Select the Action type";
        private readonly string _fixedVarToolTip = "Select the Fixed Var type";
        private readonly string _varToolTip = "Select the Var type";
        private readonly string _pathToolTip = "Give a path location for the variable type to be saved in. Right click a " +
                                               "folder and select 'Copy Path'. Then paste the path in the field here to update " +
                                               "the path location.";
        #endregion
        #region Script Paths
        private readonly string _pathActionScripts = "Assets/KamranWali/CodeOptPro/Scripts/ScriptableObjects/Actions";
        private readonly string _pathFixedVarScripts = "Assets/KamranWali/CodeOptPro/Scripts/ScriptableObjects/FixedVars";
        private readonly string _pathVarScripts = "Assets/KamranWali/CodeOptPro/Scripts/ScriptableObjects/Vars";
        #endregion
        #region Input Fields
        private bool _valueBool1;
        private double _valueDouble1;
        private float _valueFloat1;
        private int _valueInt1;
        private string _valueString1;
        private Vector2 _valueVector2_1;
        private Vector3 _valueVector3_1;
        #endregion
        private int _index, _selCatePre = -1, _selPre = -1;
        private bool _init = false;
        private ScriptableObject _createVar;
        private SerializedObject _sObj;
        private GUIStyle _style = new GUIStyle(EditorStyles.textArea);

        [MenuItem("KamranWali/CodeOptPro/Variable Creator")]
        private static void Init()
        {
            VariableCreator window = (VariableCreator)EditorWindow.GetWindow(typeof(VariableCreator), true, "Variable Creator");
            window.Show();
        }

        protected override void InitInput()
        {
            _name = EditorGUILayout.TextField("Name", _name);

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField(new GUIContent("Path", _pathToolTip));
            EditorGUILayout.BeginHorizontal();
            SetPath(); // Setting the correct path
            _style.wordWrap = true;
            _path = EditorGUILayout.TextArea(_path, _style);
            if (GUILayout.Button("Update Path")) UpdatePath(); // Updating the path
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            if (!_init) SetCategoryTypes(); // Setting up category types
            _selCate = EditorGUILayout.Popup(new GUIContent("Category", _categoryToolTip), _selCate, _categories);

            if (_init) // Condition to show the selected category
            {
                if (_selCate == 0) // Condition to show Action type variables
                {
                    _selActions = EditorGUILayout.Popup(new GUIContent("Actions", _actionToolTip), _selActions, _actions);
                    if (!string.IsNullOrWhiteSpace(_name)) if (GUILayout.Button($"Create {_actions[_selActions]}")) CreateVariable(true);
                }
                else if (_selCate == 1) // Condition to show Fixed Var type variables
                {
                    _selFixedVar = EditorGUILayout.Popup(new GUIContent("Fixed Vars", _fixedVarToolTip), _selFixedVar, _fixedVars);
                    SetupFixedVarInput(); // Setting up the input
                    if (!string.IsNullOrWhiteSpace(_name)) if (GUILayout.Button($"Create {_fixedVars[_selFixedVar]}")) CreateFixedVarType();
                }
                else if (_selCate == 2) // Condition to show Var type variables
                {
                    _selVar = EditorGUILayout.Popup(new GUIContent("Vars", _varToolTip), _selVar, _vars);
                    if (!string.IsNullOrWhiteSpace(_name)) if (GUILayout.Button($"Create {_vars[_selVar]}")) CreateVariable(true);
                }
            }
        }

        /// <summary>
        /// This method sets the correct path.
        /// </summary>
        private void SetPath()
        {
            if (_selCate == 0) // Getting the action path
            {
                if (_selCatePre != _selCate || _selPre != _selActions) // Condition to check if new path to set
                {
                    _path = _actionPaths.GetPath(_selActions);
                    _selPre = _selActions;
                }
            }
            else if (_selCate == 1) // Getting the fixed var path
            {
                if (_selCatePre != _selCate || _selPre != _selFixedVar) // Condition to check if new path to set
                {
                    _path = _fixedVarPaths.GetPath(_selFixedVar);
                    _selPre = _selFixedVar;
                }
            }
            else if (_selCate == 2) // Getting the var path
            {
                if (_selCatePre != _selCate || _selPre != _selVar) // Condition to check if new path to set
                {
                    _path = _varPaths.GetPath(_selVar);
                    _selPre = _selVar;
                }
            }

            if (_selCatePre != _selCate) _selCatePre = _selCate; // updating the pre category
        }

        /// <summary>
        /// This method creates Fixed Var Type.
        /// </summary>
        private void CreateFixedVarType()
        {
            CreateVariable(false); // Creating the variable
            WriteToLog($"Setting {_name}'s Values...");
            _sObj = null;
            _sObj = new SerializedObject(_createVar);
            if (GetFixedVarType().Equals(typeof(FixedBoolVar))) _sObj.FindProperty("value").boolValue = _valueBool1;
            else if (GetFixedVarType().Equals(typeof(FixedDoubleVar))) _sObj.FindProperty("value").doubleValue = _valueDouble1;
            else if (GetFixedVarType().Equals(typeof(FixedFloatVar))) _sObj.FindProperty("value").floatValue = _valueFloat1;
            else if (GetFixedVarType().Equals(typeof(FixedIntVar))) _sObj.FindProperty("value").intValue = _valueInt1;
            else if (GetFixedVarType().Equals(typeof(FixedStringVar))) _sObj.FindProperty("value").stringValue = _valueString1;
            else if (GetFixedVarType().Equals(typeof(FixedVector2Var))) _sObj.FindProperty("value").vector2Value = _valueVector2_1;
            else if (GetFixedVarType().Equals(typeof(FixedVector3Var))) _sObj.FindProperty("value").vector3Value = _valueVector3_1; ;
            _sObj.ApplyModifiedProperties();
            EditorUtility.SetDirty(_createVar);
            _sObj = null;
            _createVar = null; // Helping GC
            WriteToLog($"{_name}'s Values Set Successful!");
        }

        /// <summary>
        /// This method sets up the input needed for the correct fixed var type.
        /// </summary>
        private void SetupFixedVarInput()
        {
            if (GetFixedVarType().Equals(typeof(FixedBoolVar))) _valueBool1 = EditorGUILayout.Toggle("Bool", _valueBool1);
            else if (GetFixedVarType().Equals(typeof(FixedDoubleVar))) _valueDouble1 = EditorGUILayout.DoubleField("Double", _valueDouble1);
            else if (GetFixedVarType().Equals(typeof(FixedFloatVar))) _valueFloat1 = EditorGUILayout.FloatField("Float", _valueFloat1);
            else if (GetFixedVarType().Equals(typeof(FixedIntVar))) _valueInt1 = EditorGUILayout.IntField("Int", _valueInt1);
            else if (GetFixedVarType().Equals(typeof(FixedStringVar))) _valueString1 = EditorGUILayout.TextField("String", _valueString1);
            else if (GetFixedVarType().Equals(typeof(FixedVector2Var))) _valueVector2_1 = EditorGUILayout.Vector2Field("Vector2", _valueVector2_1);
            else if (GetFixedVarType().Equals(typeof(FixedVector3Var))) _valueVector3_1 = EditorGUILayout.Vector3Field("Vector3", _valueVector3_1);
        }

        /// <summary>
        /// This method creates the variable.
        /// </summary>
        /// <param name="isMakeNull">Flag for making _createVar null, true means to make it null, false otherwise, of type bool</param>
        private void CreateVariable(bool isMakeNull)
        {
            SetLog($"Creating {_name} Variable...");
            _createVar = null;
            if (_selCate == 0) _createVar = ScriptableObject.CreateInstance(System.Type.GetType($"KamranWali.CodeOptPro.ScriptableObjects.Actions.{_actions[_selActions]}, Assembly-CSharp"));
            else if (_selCate == 1) _createVar = ScriptableObject.CreateInstance(System.Type.GetType($"KamranWali.CodeOptPro.ScriptableObjects.FixedVars.{_fixedVars[_selFixedVar]}, Assembly-CSharp"));
            else if (_selCate == 2) _createVar = ScriptableObject.CreateInstance(System.Type.GetType($"KamranWali.CodeOptPro.ScriptableObjects.Vars.{_vars[_selVar]}, Assembly-CSharp"));
            AssetDatabase.CreateAsset(_createVar, $"{_path}/{_name}.asset");
            AssetDatabase.SaveAssets();
            if (isMakeNull) _createVar = null; // Condition for making _createVar to help GC
            WriteToLog($"{_name} Creation Successful!");
        }

        /// <summary>
        /// This method gets the fixed var type.
        /// </summary>
        /// <returns>The fixed var type to get, of type System.Type</returns>
        private System.Type GetFixedVarType() => System.Type.GetType($"KamranWali.CodeOptPro.ScriptableObjects.FixedVars.{_fixedVars[_selFixedVar]}, Assembly-CSharp");

        /// <summary>
        /// This method sets up the categories.
        /// </summary>
        private void SetCategoryTypes()
        {
            SetScriptNames(ref _actions, _pathActionScripts); // Setting up the action script names
            SetScriptNames(ref _fixedVars, _pathFixedVarScripts); // Setting up the fixed var script names
            SetScriptNames(ref _vars, _pathVarScripts); // Setting up the var script names
            if (_actions != null && _fixedVars != null && _vars != null) _init = true; // Condition to check if category types are initialized
        }

        /// <summary>
        /// This method sets up the correct script name from the given path.
        /// </summary>
        /// <param name="names">The names of all the script, of type string[]</param>
        /// <param name="path">The path from which to get all the script names, of type path</param>
        private void SetScriptNames(ref string[] names, string path)
        {
            names = Directory.GetFiles(path, "*.cs"); // Getting all the script names from the path
            _tempNames = new List<string>();

            for(_index = 0; _index < names.Length; _index++) // Loop for adding all the scripts
            {
                names[_index] = names[_index].Remove(0, path.Length + 1); // Removing path string from name
                names[_index] = names[_index].Remove(names[_index].Length - 3); // Removing .cs string from name
                if (!names[_index].Contains("Base")) _tempNames.Add(names[_index]); // Validating if NOT base class
            }

            if (names.Length != _tempNames.Count) names = _tempNames.ToArray(); // Condition for setting the correct script names
            _tempNames = null;
        }

        /// <summary>
        /// This method updates the path.
        /// </summary>
        private void UpdatePath()
        {
            SetLog("Updating Path...");
            if (_selCate == 0) // Updating Action Paths
            {
                _actionPaths.SetPath(_selActions, _path);
                DirtyingSO(_actionPaths, "Action Path Update");
                WriteToLog($"{_actions[_selActions]} Path Update Successful!");
            }
            else if (_selCate == 1) // Updating Fixed Var Paths
            {
                _fixedVarPaths.SetPath(_selFixedVar, _path);
                DirtyingSO(_fixedVarPaths, "Fixed Var Path Update");
                WriteToLog($"{_fixedVars[_selFixedVar]} Path Update Successful!");
            }
            else if (_selCate == 2) // Updating Var Paths
            {
                _varPaths.SetPath(_selVar, _path);
                DirtyingSO(_varPaths, "Var Path Update");
                WriteToLog($"{_vars[_selVar]} Path Update Successful!");
            }
        }

        /// <summary>
        /// This method dirty's the scriptable object.
        /// </summary>
        /// <param name="so">The scriptable object to dirty, of type VariablePath</param>
        /// <param name="msg">The message for the undo, of type string</param>
        private void DirtyingSO(ScriptableObject so, string msg)
        {
            EditorUtility.SetDirty(so);
            Undo.RecordObject(so, msg);
        }
    }
}