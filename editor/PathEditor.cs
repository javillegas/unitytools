using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof(Path))]
public class PathEditor : Editor 
{
	Path path;
	List<Vector3> waypoints;

	Transform handleTransform;
	Quaternion handleRotation;
	Quaternion arrowRotation;

	int length;
	int selectedIndex = -1;

	void OnSceneGUI ()
	{
		path = target as Path;
		waypoints = path.route;
		length = path.route.Count;
		handleTransform = path.transform;

		if (Tools.pivotRotation == PivotRotation.Local)
			handleRotation = handleTransform.rotation;
		else
			handleRotation = Quaternion.identity;
		
		Handles.color = path.color;			

		path.route[0] = ShowWaypoint (0);
		for (int i = 0; i < length-1; i++)
		{
			Handles.DrawLine (waypoints[i], waypoints[i+1]);
			arrowRotation = Quaternion.FromToRotation(new Vector3 (0, 0, 1), 
			                                          waypoints[i+1] - waypoints[i]);
			Handles.ConeCap(0, 0.5f * (waypoints[i+1] + waypoints[i]), 
			                arrowRotation, 0.15f * HandleUtility.GetHandleSize (waypoints [i]));

			path.route[i+1] = ShowWaypoint(i+1);
		}

		if (path.closed) 
		{
			Handles.DrawLine (waypoints[0], waypoints[length - 1]);
			arrowRotation = Quaternion.FromToRotation(new Vector3 (0, 0, 1), 
			                                          waypoints[0] - waypoints[length - 1]);
			Handles.ConeCap(0, 0.5f * (waypoints[length - 1] + waypoints[0]), 
			                arrowRotation, 0.15f * HandleUtility.GetHandleSize (waypoints [length - 1]));
		}
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update();

		path = target as Path;
		SerializedProperty prop = serializedObject.FindProperty("route");
		SerializedProperty closed = serializedObject.FindProperty("closed");
		SerializedProperty follow = serializedObject.FindProperty("lookForward");
		SerializedProperty color = serializedObject.FindProperty("color");

		float inspectorWidth = EditorGUIUtility.currentViewWidth;

		int listSize = prop.arraySize;

		EditorGUILayout.PropertyField (closed);
		EditorGUILayout.PropertyField (follow);
		EditorGUILayout.PropertyField (color);
		listSize = EditorGUILayout.IntField ("Object count", listSize);	

		EditorGUILayout.PropertyField(prop);
		
		if(listSize != prop.arraySize)
		{
			while(listSize > prop.arraySize)
				prop.InsertArrayElementAtIndex (prop.arraySize);
			
			while(listSize < prop.arraySize)
				prop.DeleteArrayElementAtIndex (prop.arraySize-1);
		}
		
		for (int i = 0; i < prop.arraySize; i++) 
		{
			SerializedProperty waypoint = prop.GetArrayElementAtIndex(i);

			if (prop.isExpanded)
			{
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.LabelField(i.ToString(), GUILayout.Width (0.05f * inspectorWidth));
				waypoint.vector3Value = EditorGUILayout.Vector3Field ("", waypoint.vector3Value);

				if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width (0.08f * inspectorWidth)))
					path.InsertWaypoint (new Vector3 (), i);

				if(GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width (0.08f * inspectorWidth)))
					path.DeleteWaypoint (i);
				
				EditorGUILayout.EndHorizontal();
			}
		}		

		if (prop.isExpanded)
		{
			if(GUILayout.Button("Add Waypoint"))
				path.AddWaypoint(new Vector3 ());
		}
		
		serializedObject.ApplyModifiedProperties();
	}

	private Vector3 ShowWaypoint (int index)
	{
		Vector3 point = waypoints[index];
		float size = HandleUtility.GetHandleSize (waypoints [index]);

		if (index != 0)
		{
			if (Handles.Button (waypoints[index], handleRotation, 0.05f * size, 0.05f * size, Handles.DotCap))
				selectedIndex = index;
		}
		else
		{
			if (Handles.Button (waypoints[index], handleRotation, 0.2f * size, 0.2f * size, Handles.SphereCap))
				selectedIndex = index;
		}

		if (selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck ();
			point = Handles.DoPositionHandle(point, handleRotation);

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(path, "Move Point");
				EditorUtility.SetDirty(path);
				path.Route[index] = waypoints[index];
			}
		}
		return point;
	}
}
