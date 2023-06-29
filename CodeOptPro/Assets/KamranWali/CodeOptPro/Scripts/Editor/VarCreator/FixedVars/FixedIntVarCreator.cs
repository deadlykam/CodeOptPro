using KamranWali.CodeOptPro.ScriptableObjects.FixedVars;
using UnityEditor;

namespace KamranWali.CodeOptPro.Editor.VarCreator.FixedVars
{
    public class FixedIntVarCreator : BaseVarCreator<FixedIntVar>
    {
        private int _value;

        [MenuItem("KamranWali/CodeOptPro/FixedVariables/IntFixedVariable")]
        private static void Init()
        {
            FixedIntVarCreator window = (FixedIntVarCreator)EditorWindow.GetWindow(typeof(FixedIntVarCreator));
            window.Show();
        }

        protected override void SetProperties(ref SerializedObject sObj) => sObj.FindProperty("value").intValue = _value;
        protected override void SetupInputs() => _value = EditorGUILayout.IntField("Value", _value);
    }
}