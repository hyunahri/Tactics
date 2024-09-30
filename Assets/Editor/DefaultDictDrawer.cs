using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class DefaultDictEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty dictProperty = serializedObject.FindProperty("Bonuses");

            if (dictProperty != null && dictProperty.isArray)
            {
                for (int i = 0; i < dictProperty.arraySize; i++)
                {
                    var entry = dictProperty.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(entry);
                }

                if (GUILayout.Button("Add Entry"))
                {
                    dictProperty.arraySize++;
                }
            }
            else
            {
                //helpbox that shows error
                EditorGUILayout.HelpBox("DefaultDict not found or not array.", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}