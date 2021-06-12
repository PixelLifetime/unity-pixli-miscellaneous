using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Follow : MonoBehaviour
{
	[SerializeField] private Transform _target;
	public Transform Target
	{
		get => this._target;
		set => this._target = value;
	}

	public void SetTarget(Transform transform) => this._target = transform;
	public void SetTarget(Unit unit) => this._target = unit.transform;

	public IEnumerator ReplaceMeWithNormalMethodThatYouHadInOtherPackages(Action action)
	{
		yield return null;

		action.Invoke();
	}

	public void ResetTarget() => this._target = null;

	public void SetTargetMomentarily(Unit unit)
	{
		this.SetTarget(unit);

		this.StartCoroutine(
			routine: this.ReplaceMeWithNormalMethodThatYouHadInOtherPackages(
				action: this.ResetTarget
			)
		);
	}

	public Vector3 Offset;

	private void Update()
	{
		if (this._target != null)
		{
			this.transform.position = this._target.position + this.Offset;
		}
	}
}