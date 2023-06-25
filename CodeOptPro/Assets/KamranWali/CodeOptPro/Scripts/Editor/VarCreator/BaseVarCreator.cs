using UnityEditor;
using UnityEngine;

namespace KamranWali.CodeOptPro.Editor.VarCreator
{
    public abstract class BaseVarCreator<T, U> : EditorWindow
    {
        private T _createVar;
        private U _value;
        private SerializedObject _sObj;
        private string _path = ""; // Give default path

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("Box");
            /*GUI.skin.label.fontSize = 20;
            GUILayout.Label("Create Properties");*/
            
            //TODO: Create abstract method for getting inputs
            EditorGUILayout.EndVertical();

            //if (GUILayout.Button($"Create Variable")) CreateVariable();
        }
    }
}