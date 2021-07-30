using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingChild : MonoBehaviour
{
	[SerializeField] private Transform _rootParent;
	public Transform _RootParent => this._rootParent;

	private Transform _cachedParentOfRootParent;

	public void Parent(Transform transform)
	{
		this._cachedParentOfRootParent = this._rootParent.parent;

		this._rootParent.SetParent(transform);

		Debug.Log($"New Parent: {transform}");
	}

	public void UnParent()
	{
		this._rootParent.SetParent(this._cachedParentOfRootParent);

		Debug.Log($"Reset To Parent: {this._cachedParentOfRootParent}");
	}

	private void Awake()
	{
		this._cachedParentOfRootParent = this._rootParent.parent;
	}
}