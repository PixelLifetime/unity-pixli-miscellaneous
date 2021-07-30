using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointOperator : MonoBehaviour
{
	public void SetActiveCheckPoint(Transform transform)
	{
		CheckPointsManager._Instance.ActiveCheckPoint = transform;

		//Debug.Log("ACtive point set.");
	}

	public void Respawn() => CheckPointsManager._Instance.Respawn();
}