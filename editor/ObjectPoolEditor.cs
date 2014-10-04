using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (ObjectPool))]
public class ObjectPoolEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		ObjectPool poolScript = target as ObjectPool;
		SerializedProperty prop = serializedObject.FindProperty("objectPool");

		float inspectorWidth = EditorGUIUtility.currentViewWidth;

		int listSize = prop.arraySize;
		listSize = EditorGUILayout.IntField ("Object count", listSize);	

		EditorGUILayout.PropertyField(prop);

		if(listSize != prop.arraySize){
			while(listSize > prop.arraySize)
				prop.InsertArrayElementAtIndex(prop.arraySize);
			
			while(listSize < prop.arraySize)
				prop.DeleteArrayElementAtIndex(prop.arraySize - 1);
		}

		if (prop.isExpanded)
		{
			if (listSize != 0)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField ("ID", GUILayout.Width (0.1f * inspectorWidth));
				EditorGUILayout.LabelField ("Object", GUILayout.Width (0.4f * inspectorWidth));
				EditorGUILayout.LabelField ("Size", GUILayout.Width (0.2f * inspectorWidth));
				EditorGUILayout.LabelField ("Grow", GUILayout.Width (0.12f * inspectorWidth));
				EditorGUILayout.EndHorizontal ();
			}
			
			for (int i = 0; i < prop.arraySize; i++) 
			{
				SerializedProperty listRef = prop.GetArrayElementAtIndex(i);
				SerializedProperty objType = listRef.FindPropertyRelative("type");
				SerializedProperty size = listRef.FindPropertyRelative("size");
				SerializedProperty isExt = listRef.FindPropertyRelative("grow");

				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.LabelField(i.ToString(), GUILayout.Width (0.1f * inspectorWidth));

				objType.objectReferenceValue = EditorGUILayout.ObjectField("", objType.objectReferenceValue, typeof(GameObject), true, GUILayout.Width (0.4f * inspectorWidth));
				size.intValue = EditorGUILayout.IntField("", size.intValue, GUILayout.Width (0.2f * inspectorWidth));
				isExt.boolValue = EditorGUILayout.Toggle("", isExt.boolValue, GUILayout.Width (0.12f * inspectorWidth));

				if(GUILayout.Button("-"))
					prop.DeleteArrayElementAtIndex(i);

				EditorGUILayout.EndHorizontal();
			}

			
			if(GUILayout.Button("+"))
				poolScript.objectPool.Add(new ObjectPool.PoolRef());
		}
		serializedObject.ApplyModifiedProperties();
	}
}
