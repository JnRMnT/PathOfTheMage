using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;

namespace JMGames.Scripts.Utilities
{
#if UNITY_EDITOR
    public class EditorUtilities
    {
        public static Type GetSerializedPropertyType(SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = parentType.GetField(property.propertyPath);
            return fieldInfo.FieldType;
        }
    }
#endif
}
