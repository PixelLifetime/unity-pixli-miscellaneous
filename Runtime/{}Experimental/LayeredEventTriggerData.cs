using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	[System.Serializable]
	public class LayeredEventTriggerData<TEvent>
		where TEvent : UnityEventBase
	{
		[SerializeField] private TEvent _event;
		public TEvent _Event { get { return this._event; } }

		[SerializeField] private LayerMask _layerMask;
		public LayerMask _LayerMask { get { return this._layerMask; } }

		public LayeredEventTriggerData(TEvent @event, LayerMask layerMask)
		{
			this._event = @event;
			this._layerMask = layerMask;
		}
	}

	//#if UNITY_EDITOR
	//	[CustomEditor(typeof(LayeredEventTriggerData))]
	//	[CanEditMultipleObjects]
	//	public class LayeredEventTriggerDataEditor : Editor
	//	{
	//#pragma warning disable 0219, 414
	//		private LayeredEventTriggerData _sLayeredEventTriggerData;
	//#pragma warning restore 0219, 414

	//		private void OnEnable()
	//		{
	//			this._sLayeredEventTriggerData = this.target as LayeredEventTriggerData;
	//		}

	//		public override void OnInspectorGUI()
	//		{
	//			this.DrawDefaultInspector();
	//		}
	//	}
	//#endif
}