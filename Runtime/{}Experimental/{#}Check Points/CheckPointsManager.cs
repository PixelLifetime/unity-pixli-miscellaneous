using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsManager : MonoBehaviourSingleton<CheckPointsManager>
{
	public Transform ActiveCheckPoint { get; set; }

	[SerializeField] private Transform _playerTransform;
	public Transform _PlayerTransform => this._playerTransform;

	public void Respawn()
	{
		this._playerTransform.GetComponent<CharacterController>().enabled = false;

		this._playerTransform.position = this.ActiveCheckPoint.position;
		this._playerTransform.rotation = this.ActiveCheckPoint.rotation;

		this.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: 1.0f,
				action: () =>
				{
					this._playerTransform.GetComponent<CharacterController>().enabled = true;
				}
			)
		);

		Debug.Log(this._playerTransform.position);
		Debug.Log("Respawn");
	}
}