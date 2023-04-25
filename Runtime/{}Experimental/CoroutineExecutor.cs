using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public class CoroutineExecutor
	{
		public MonoBehaviour Invoker { get; set; }
		public Func<IEnumerator> Process { get; set; }

		public Coroutine Coroutine_ { get; private set; }

		public void StopProcess()
		{
			if (this.Coroutine_ != null)
			{
				this.Invoker.StopCoroutine(this.Coroutine_);
			}
		}

		public Coroutine StartProcess(IEnumerator routine)
		{
			this.StopProcess();

			this.Coroutine_ = this.Invoker.StartCoroutine(routine);

			return this.Coroutine_;
		}

		public Coroutine StartProcess() => this.StartProcess(this.Process.Invoke());

		public CoroutineExecutor(MonoBehaviour invoker)
		{
			this.Invoker = invoker;
		}

		//public CoroutineExecutor() : this(GameStateManager._Instance)
		//{
		//}

		public CoroutineExecutor()
		{
		}

		public CoroutineExecutor(MonoBehaviour invoker, Func<IEnumerator> process) : this(invoker)
		{
			this.Process = process;
		}

#if UNITY_EDITOR
#endif
	}
}