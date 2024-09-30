using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{

    [CustomPropertyDrawer(typeof(KeyValuePair<,>), true)]
    public class KeyValuePairDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keyProperty = property.FindPropertyRelative("Key");
            var valueProperty = property.FindPropertyRelative("Value");

            var halfWidth = position.width / 2;
            var keyRect = new Rect(position.x, position.y, halfWidth - 5, position.height);
            var valueRect = new Rect(position.x + halfWidth + 5, position.y, halfWidth - 5, position.height);

            EditorGUI.PropertyField(keyRect, keyProperty, GUIContent.none);
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none);
        }
    }
}