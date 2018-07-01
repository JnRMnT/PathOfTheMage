using JMGames.JMDialogs.Infrastructure.Base;
using JMGames.Scripts.DialogSystem;
using JMGames.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace JMGames.Scripts.EditorScripts
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DialogItemDefinition))]
    [CustomPropertyDrawer(typeof(DialogDefinition))]
    public class JMDialogsPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect remainingPosition = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive),
                                  new GUIContent(label.text));

            Type propertyType = EditorUtilities.GetSerializedPropertyType(property);
            FieldInfo typeFieldInfo = propertyType.GetField("InternalType");
            Type baseType = null;
            if (propertyType.Name == "DialogDefinition")
            {
                baseType = Type.GetType("JMGames.JMDialogs.Infrastructure.Base.Dialog, JMGames.JMDialogs");
            }
            else
            {
                baseType = Type.GetType("JMGames.JMDialogs.Infrastructure.Base.DialogItem, JMGames.JMDialogs");
            }

            List<Type> availableTypes = new List<Type>
            {
                null
            };

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Namespace != "JMGames.JMDialogs.Infrastructure.Base" && baseType.IsAssignableFrom(type))
                    {
                        availableTypes.Add(type);
                    }
                }
            }

            EditorGUI.BeginChangeCheck();
            float indent = remainingPosition.width;

            int selectedIndex = 0;
            if (property.objectReferenceValue != null)
            {
                Type selectedType = typeFieldInfo.GetValue(property.objectReferenceValue) as Type;
                if (selectedType != null)
                {
                    selectedIndex = availableTypes.FindIndex(e => e != null && e.FullName == selectedType.FullName);
                }
            }


            selectedIndex = EditorGUI.Popup(remainingPosition, selectedIndex, availableTypes.Select(e => e == null ? "Random" : e.FullName).ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                if (selectedIndex != -1)
                {
                    UnityEngine.Object objectInstance = ScriptableObject.CreateInstance(propertyType) as UnityEngine.Object;
                    typeFieldInfo.SetValue(objectInstance, availableTypes[selectedIndex]);
                    property.objectReferenceValue = objectInstance;
                }
                else
                {
                    property.objectReferenceValue = null;
                }
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}
