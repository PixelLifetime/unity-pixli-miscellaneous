using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using HDebug = PixLi.Debugging.Debug;

namespace PixLi.Debugging
{
	public class Debugger : MonoBehaviour
	{
		public void Debug(string message) => HDebug.Log(message);

#if UNITY_EDITOR
		//protected virtual void OnDrawGizmos()
		//{
		//}
#endif
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(Debugger))]
	[CanEditMultipleObjects]
	public class DebuggerEditor : MultiSupportEditor
	{
#pragma warning disable 0219, 414
		private Debugger _sDebugger;
#pragma warning restore 0219, 414

		public override void OnEnable()
		{
			base.OnEnable();

			this._sDebugger = this.target as Debugger;
		}

		public override void MainDrawGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}