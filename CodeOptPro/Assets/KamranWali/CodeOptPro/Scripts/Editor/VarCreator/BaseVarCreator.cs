using KamranWali.CodeOptPro.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor.VarCreator
{
    public abstract class BaseVarCreator<T> : EditorWindow where T : BaseScriptableObject
    {
        private T _createVar;
        private SerializedObject _sObj;
        private string _varName;
        private string _path = ""; // Give default path

        private readonly string _varNameToolTip = "The name for the variable";
        private readonly string _pathToolTip = "Path to save the object";

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("Box");
            /*GUI.skin.label.fontSize = 20;
            GUILayout.Label("Create Properties");*/

            //TODO: Create abstract method for getting inputs
            SetupInputs(); // Setting up inputs
            _varName = EditorGUILayout.TextField(new GUIContent("Name", _varNameToolTip), _varName);
            _path = EditorGUILayout.TextField(new GUIContent("Path", _pathToolTip), _path);
            //TODO: Give a check to see if path has changed then also update the SO saving the path
            EditorGUILayout.EndVertical();

            if (GUILayout.Button($"Create Variable")) CreateVariable();
        }

        /// <summary>
        /// This method sets up the inputs for the user.
        /// </summary>
        protected abstract void SetupInputs();

        /// <summary>
        /// This method sets the properties for the variable, Please see other variable creators to know
        /// how to set this if you don't know how SerializedObject works.
        /// </summary>
        /// <param name="sObj">The object to set the properties too, of type SerializedObject</param>
        protected abstract void SetProperties(ref SerializedObject sObj);

        /// <summary>
        /// This method creates the variable.
        /// </summary>
        private void CreateVariable()
        {
            _createVar = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(_createVar, $"{_path}/{_varName}.asset");
            AssetDatabase.SaveAssets();

            _sObj = null;
            _sObj = new SerializedObject(_createVar);
            SetProperties(ref _sObj);
            _sObj.ApplyModifiedProperties();
            EditorUtility.SetDirty(_createVar);
            _sObj = null;
            _createVar = null;
        }
    }
}