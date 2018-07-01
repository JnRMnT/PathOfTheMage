using UnityEditor;
using UnityEngine;

namespace JMGames.Scripts.EditorScripts
{
#if UNITY_EDITOR
    public class EnumFlagsAttribute : PropertyAttribute
    {
        public EnumFlagsAttribute() { }
    }

    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, UnityEditor.SerializedProperty _property, GUIContent _label)
        {
            // Change check is needed to prevent values being overwritten during multiple-selection
            UnityEditor.EditorGUI.BeginChangeCheck();
            int newValue = UnityEditor.EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                _property.intValue = newValue;
            }
        }
    }
#endif
}