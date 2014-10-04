using UnityEngine;
using System.Collections;

public class ExtendedMonoBehaviour : MonoBehaviour {

	public Transform thisTransform;
	public GameObject thisGameObject;
	public Rigidbody thisBody;
	public Rigidbody2D thisBody2D;

	public bool isInit;
	public bool controllable;

	public int id;

	[System.NonSerialized]
	public Vector3 tmpV;

	[System.NonSerialized]
	public Transform tmpTr;

	public virtual void SetID (int newID)
	{
		id = newID;
	}
}
