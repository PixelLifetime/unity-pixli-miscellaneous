using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingTrigger : MonoBehaviour
{
	[SerializeField] private LayerMask _layerMask;
	public LayerMask _LayerMask => this._layerMask;

	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("OnCollisionEnter");

		if (this._layerMask.Contains(collider))
			collider.GetComponent<ParentingChild>().Parent(this.transform);
	}

	private void OnTriggerExit(Collider collider)
	{
		Debug.Log("OnCollisionExit");

		if (this._layerMask.Contains(collider))
			collider.GetComponent<ParentingChild>().UnParent();
	}
}