using System;
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
	public abstract class CooldownExecutor<T> //TODO: split into multiple files
		where T : ICooldown
	{
		protected T cooldown;
		public T _Cooldown { get { return this.cooldown; } }

		protected Action action;

		public abstract void Finish();
		public abstract bool Execute();

		public CooldownExecutor(T cooldown, Action action)
		{
			this.cooldown = cooldown;
			this.action = action;
		}
	}

	public sealed class CooldownExecutor : CooldownExecutor<ICooldown>
	{
		public CooldownExecutor(ICooldown cooldown, Action action) : base(cooldown, action)
		{
		}

		public override void Finish()
		{
			this.cooldown.Reset();
			this.action.Invoke();
		}

		public override bool Execute()
		{
			if (this.cooldown._Finished)
			{
				this.Finish();

				return true;
			}

			return false;
		}
	}

	public sealed class ProgressiveCooldownExecutor : CooldownExecutor<IProgressiveCooldown>
	{
		public ProgressiveCooldownExecutor(IProgressiveCooldown cooldown, Action action) : base(cooldown, action)
		{
		}

		public override void Finish()
		{
			this.cooldown.Reset();
			this.action.Invoke();
		}

		public override bool Execute()
		{
			this.cooldown.Progress();

			if (this.cooldown._Finished)
			{
				this.Finish();

				return true;
			}

			return false;
		}
	}

	public interface ICooldown
	{
		bool _Finished { get; }
		void Reset();
	}

	public interface IProgressiveCooldown : ICooldown
	{
		void Progress();
	}

	[System.Serializable]
	public class Cooldown : CustomYieldInstruction, ICooldown
	{
		//[HelpBox("The time that is required to elapse(pass) after Reset()ting cooldown in order for it to be _Finished.", HelpBoxAttribute.HelpBoxType.Info)]
		//[Min(0f)]
		[SerializeField] private float _tagetElapsedTime;
		public float TargetElapsedTime
		{
			get => this._tagetElapsedTime;
			set => this._tagetElapsedTime = value;
		}

		public float ResetTime_ { get; private set; }

		public bool _Finished => Time.time - this.ResetTime_ >= this._tagetElapsedTime;

		public override void Reset()
		{
			this.ResetTime_ = Time.time;
		}

		public virtual void Reset(float newTargetElapsedTime)
		{
			this._tagetElapsedTime = newTargetElapsedTime;
			this.Reset();
		}

		public virtual void Finish()
		{
			this.ResetTime_ = Time.time - this._tagetElapsedTime;
		}

		public virtual float GetElapsedTime()
		{
			return Time.time - this.ResetTime_;
		}

		public virtual float GetTimeLeft()
		{
			return this._tagetElapsedTime - this.GetElapsedTime();
		}

		public Cooldown(float targetElapsedTime, float initialTime = 0)
		{
			this._tagetElapsedTime = targetElapsedTime;
			this.ResetTime_ = Time.time - initialTime;
		}

		public override bool keepWaiting => !this._Finished;
	}

	[System.Serializable]
	public class ProgressiveCooldown : IProgressiveCooldown
	{
		[SerializeField] private float _tagetElapsedTime;
		public float _TagetElapsedTime => this._tagetElapsedTime;

		public float ElapsedTime_ { get; private set; }

		public bool _Finished => this.ElapsedTime_ >= this._tagetElapsedTime;

		public virtual void Progress()
		{
			this.ElapsedTime_ += Time.deltaTime;
		}

		public virtual void Reset()
		{
			this.ElapsedTime_ -= this._tagetElapsedTime;
		}

		public virtual void Finish()
		{
			this.ElapsedTime_ = this._tagetElapsedTime;
		}

		public ProgressiveCooldown(float targetElapsedTime, float initialTime = 0)
		{
			this._tagetElapsedTime = targetElapsedTime;
			this.ElapsedTime_ = initialTime;
		}
	}
}