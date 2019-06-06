using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BaseGameLogic.Singleton
{
    public class TypePropertyDrower<T> : PropertyDrawer where T : SingletonTypeManager<T>
    {
        private SerializedProperty _name = null;
        private int index = -1;
        private string[] names = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _name = property.FindPropertyRelative("_name");
            if (SingletonTypeManager<T>.Instance != null)
            {
                names = new string[SingletonTypeManager<T>.Instance.TypesNames.Count];
                SingletonTypeManager<T>.Instance.TypesNames.CopyTo(names, 0);
            }
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (SingletonTypeManager<T>.Instance != null && SingletonTypeManager<T>.Instance.TypesNames.Count > 0)
            {
                index = SingletonTypeManager<T>.Instance.TypesNames.IndexOf(_name.stringValue);
                index = index < 0 ? 0 : index;
                index = EditorGUI.Popup(position, property.displayName, index, names);
                _name.stringValue = names[index];
                property.serializedObject.ApplyModifiedProperties();
            }
            else
                EditorGUI.LabelField(position, "No weapon mode types defined.");
        }
    }
}