using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using Abilities;



/// <summary>
/// Custom editor for the Ability scriptable object.
/// Allows editing strategies without needing to create a new scriptable object for each one.
/// </summary>
[CustomEditor(typeof(Ability))]
public class AbilityEditor : UnityEditor.Editor
{
    
    //DATA
    //Order must match in these lists
    private List<Type> managedTypes = new List<Type>() { typeof(AbilityTargetingStrategy), typeof(AbilityRequirementStrategy), typeof(AbilityEffect) };
    private List<string> propertyNames = new List<string>() { "TargetingStrategies", "FixedRequirements", "Effects" };

    private Dictionary<Type, List<Type>> subtypes = new Dictionary<Type, List<Type>>();
    private Dictionary<Type, List<string>> subtypeNames = new Dictionary<Type, List<string>>();
    private Dictionary<Type, int> subtypeIndices = new Dictionary<Type, int>();

    private void OnEnable()
    {
        // Populate the subtypes dictionary for each managed type
        foreach (var t in managedTypes)
        {
            subtypes[t] = new List<Type>();
            subtypes[t].AddRange(AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(t) && !type.IsAbstract)
                .ToList());

            subtypeNames[t] = subtypes[t].Select(type => type.Name).ToList();
            subtypeIndices[t] = 0;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        // Draw the default inspector (for fields other than the lists of strategies and effects)
        DrawDefaultInspector();

        // Handle each managed type (currently just AbilityEffect)
        Dictionary<Type, SerializedProperty> properties = new Dictionary<Type, SerializedProperty>();
        for (int i = 0; i < managedTypes.Count; i++)
        {
            string name = propertyNames[i];
            Type type = managedTypes[i];
            SerializedProperty property = serializedObject.FindProperty(name);

            if (property == null)
            {
                Debug.LogError($"Property '{name}' not found in serialized object.");
                continue;
            }

            properties.Add(type, property);
        }

        foreach (KeyValuePair<Type, SerializedProperty> pair in properties)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(pair.Key.Name + " List", EditorStyles.boldLabel);

            if (!pair.Value.propertyType.HasFlag(SerializedPropertyType.ManagedReference))
            {
                EditorGUILayout.HelpBox($"Property '{pair.Key.Name}' does not support managed references.", MessageType.Warning);
                continue;
            }

            // Check if we have subtypes for this managed type
            if (subtypes[pair.Key].Count == 0)
            {
                EditorGUILayout.HelpBox("No subtypes found", MessageType.Error);
                continue;
            }

            // Iterate over the existing elements in the list
            for (int i = 0; i < pair.Value.arraySize; i++)
            {
                SerializedProperty elementProperty = pair.Value.GetArrayElementAtIndex(i);

                // Ensure the property is valid for managedReferenceValue
                if (elementProperty == null || !elementProperty.propertyType.HasFlag(SerializedPropertyType.ManagedReference))
                {
                    Debug.LogError($"Invalid property found in {pair.Key.Name}");
                    continue;
                }

                Type elementType = elementProperty.managedReferenceValue?.GetType();
                string elementLabel = elementType != null ? elementType.Name : "Unknown Property";

                // Draw the foldout for each effect/strategy to allow editing of unique fields
                elementProperty.isExpanded = EditorGUILayout.Foldout(elementProperty.isExpanded, elementLabel);
                if (elementProperty.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(elementProperty, true);
                    EditorGUI.indentLevel--;
                }

                // Option to remove the effect
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace(); // Pushes the button to the right
                if (GUILayout.Button($"Remove {elementLabel}", GUILayout.Width(200), GUILayout.Height(18))) // Shrink button
                {
                    pair.Value.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndHorizontal();
            }

            // Button to add new effects/strategies
            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"Add New {propertyNames[managedTypes.IndexOf(pair.Key)]}", EditorStyles.boldLabel);
            subtypeIndices[pair.Key] = EditorGUILayout.Popup($"{pair.Key.Name} Type", subtypeIndices[pair.Key], subtypeNames[pair.Key].ToArray());

            if (!GUILayout.Button($"Add {propertyNames[managedTypes.IndexOf(pair.Key)]}")) continue;
            
            Type selectedType = subtypes[pair.Key][subtypeIndices[pair.Key]];
            var newElement = Activator.CreateInstance(selectedType);

            pair.Value.InsertArrayElementAtIndex(pair.Value.arraySize);
            pair.Value.GetArrayElementAtIndex(pair.Value.arraySize - 1).managedReferenceValue = newElement;
        }

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
