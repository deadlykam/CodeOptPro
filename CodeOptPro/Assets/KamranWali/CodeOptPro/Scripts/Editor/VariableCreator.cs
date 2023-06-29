using KamranWali.CodeOptPro.ScriptableObjects;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor
{
    public class VariableCreator : EditorWindow
    {
        private string _name;
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
        #endregion
        #region Script Paths
        private readonly string _pathActionScripts = "Assets/KamranWali/CodeOptPro/Scripts/ScriptableObjects/Actions";
        private readonly string _pathFixedVarScripts = "Assets/KamranWali/CodeOptPro/Scripts/ScriptableObjects/FixedVars";
        private readonly string _pathVarScripts = "Assets/KamranWali/CodeOptPro/Scripts/ScriptableObjects/Vars";
        #endregion
        private int _index;
        private bool _init = false;
        private ScriptableObject _createVar;

        [MenuItem("KamranWali/CodeOptPro/VariableCreator")]
        private static void Init()
        {
            VariableCreator window = (VariableCreator)EditorWindow.GetWindow(typeof(VariableCreator));
            window.Show();
        }

        private void OnGUI()
        {
            _name = EditorGUILayout.TextField("Name", _name);
            if (!_init) SetCategoryTypes(); // Setting up category types
            _selCate = EditorGUILayout.Popup(new GUIContent("Category", _categoryToolTip), _selCate, _categories);

            if (_init) // Condition to show the correct category type
            {
                if (_selCate == 0)
                {
                    _selActions = EditorGUILayout.Popup(new GUIContent("Actions", _actionToolTip), _selActions, _actions);
                    if (GUILayout.Button($"Create {_actions[_selActions]}")) CreateActionType();
                }
                else if (_selCate == 1) _selFixedVar = EditorGUILayout.Popup(new GUIContent("Fixed Vars", _fixedVarToolTip), _selFixedVar, _fixedVars);
                else if (_selCate == 2) _selVar = EditorGUILayout.Popup(new GUIContent("Vars", _varToolTip), _selVar, _vars);

                //TODO: Give name input as well but if blank then use value as name but only for vars that have inputs
                //TODO: Button for creating the SO and give input methods inside the else if conditions
                //if (GUILayout.Button("Create Variable")) CreateVariable();
            }
        }

        /// <summary>
        /// This method creates Action types.
        /// </summary>
        private void CreateActionType() 
        {
            _createVar = ScriptableObject.CreateInstance(System.Type.GetType($"KamranWali.CodeOptPro.ScriptableObjects.Actions.{_actions[_selActions]}, Assembly-CSharp"));
            AssetDatabase.CreateAsset(_createVar, $"Assets/KamranWali/CodeOptPro/Scripts/Editor/{_name}.asset");
            AssetDatabase.SaveAssets();
        }

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
    }
}