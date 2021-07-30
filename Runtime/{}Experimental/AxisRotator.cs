using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRotator : MonoBehaviour
{
	[SerializeField] private Vector3 _rotation;
	public Vector3 _Rotation => this._rotation;

	[SerializeField] private Space _space = Space.World;
	public Space _Space => this._space;

	[SerializeField] private float _time = 3.0f;
	public float _Time => this._time;

	private IEnumerator RotateProcess(float targetTime)
	{
		float time = 0.0f;

		Quaternion startRotation = this.transform.rotation;
		Quaternion targetRotation = Quaternion.Euler(this._rotation);

		while (time < targetTime)
		{
			this.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / targetTime);

			yield return null;

			time += Time.deltaTime;
		}

		this.transform.rotation = targetRotation;
	}

	public void Rotate()
	{
		this.StopAllCoroutines();
		this.StartCoroutine(this.RotateProcess(targetTime: this._time));
	}
}