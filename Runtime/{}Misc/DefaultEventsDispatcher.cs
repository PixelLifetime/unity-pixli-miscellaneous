using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefaultEventsDispatcher : MonoBehaviour
{
	[SerializeField] private UnityEvent _onAwake;
	public UnityEvent _OnAwake => this._onAwake;

	private void Awake() => this._onAwake.Invoke();

	[SerializeField] private UnityEvent _onStart;
	public UnityEvent _OnStart => this._onStart;

	[SerializeField] private float _delay;
	public float _Delay => this._delay;

	[SerializeField] private UnityEvent _onStartDelayed;
	public UnityEvent _OnStartDelayed => this._onStartDelayed;

	private void Start()
	{
		this._onStart.Invoke();

		this.StartCoroutine(
			routine: CoroutineProcessorsCollection.InvokeAfter(
				seconds: this._delay,
				action: this._onStartDelayed.Invoke
			)
		);
	}

	[SerializeField] private UnityEvent _onUpdate;
	public UnityEvent _OnUpdate => this._onUpdate;

	private void Update() => this._onUpdate.Invoke();
}