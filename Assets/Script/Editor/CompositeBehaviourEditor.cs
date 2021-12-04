using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
		CompositeBehavior cb = (CompositeBehavior)target;

		GUILayout.Space(10f);
		// Buttons to add and remove behaviours in our containers
		EditorGUILayout.BeginVertical();
		if (GUILayout.Button("Add"))
		{
			AddBehavior(cb);
			EditorUtility.SetDirty(cb);
		}
		if (GUILayout.Button("Remove"))
		{
			RemoveBehavior(cb);
			EditorUtility.SetDirty(cb);
		}
		EditorGUILayout.EndVertical();

		// Check for behaviours
		if (cb.behaviours == null || cb.behaviours.Length == 0)
		{
			EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
		}
		else
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(20f);
			//EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
			EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f));
			EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
			EditorGUILayout.EndHorizontal();

			EditorGUI.BeginChangeCheck();
			for (int i = 0; i < cb.behaviours.Length; ++i)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(15f));
				cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false, GUILayout.MinWidth(60f));
				cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
				EditorGUILayout.EndHorizontal();
			}
			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(cb);
			}
		}
		void AddBehavior(CompositeBehavior cb)
		{
			int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
			FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
			float[] newWeights = new float[oldCount + 1];
			for (int i = 0; i < oldCount - 1; i++)
			{
				newBehaviours[i] = cb.behaviours[i];
				newWeights[i] = cb.weights[i];
			}
			cb.behaviours = newBehaviours;
			cb.weights = newWeights;
		}
		void RemoveBehavior(CompositeBehavior cb)
		{
			int oldCount = cb.behaviours.Length;
			if (oldCount == 1)
			{
				cb.behaviours = null;
				cb.weights = null;
				return;
			}
			FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
			float[] newWeights = new float[oldCount - 1];
			for (int i = 0; i < oldCount; i++)
			{
				newBehaviours[i] = cb.behaviours[i];
				newWeights[i] = cb.weights[i];
			}
			cb.behaviours = newBehaviours;
			cb.weights = newWeights;
		}
	}

}
