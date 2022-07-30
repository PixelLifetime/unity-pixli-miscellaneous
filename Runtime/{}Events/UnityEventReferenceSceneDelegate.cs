using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace PixLi
{
	public class UnityEventReferenceSceneDelegate : MonoBehaviour
	{
		[SerializeField] private UnityEventReference _unityEventReference;
		public UnityEventReference _UnityEventReference => this._unityEventReference;

		[SerializeField] private UnityEvent _unityEvent;
		public UnityEvent _UnityEvent => this._unityEvent;

		private void Awake()
		{
			this._unityEventReference._UnityEvent.AddListener(call: this._unityEvent.Invoke);
		}
	}
}