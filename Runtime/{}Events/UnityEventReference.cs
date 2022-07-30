using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace PixLi
{
	[CreateAssetMenu(fileName = "[Unity Event Reference]", menuName = "[Events]/[Unity Event Reference]")]
	public class UnityEventReference : ScriptableObject
	{
		[SerializeField] private UnityEvent _unityEvent;
		public UnityEvent _UnityEvent => this._unityEvent;
	}
}