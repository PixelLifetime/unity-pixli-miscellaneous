using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace PixLi
{
	//TODO: I would probably like to change this name to something different or even think about if this is even useful to have.
	[System.Serializable]
	public class BoundedValue<TValue, TRange>
		where TValue : IComparable, IComparable<TValue>, IConvertible, IEquatable<TValue>, IFormattable
		where TRange : IRange<TValue>
	{
		public static readonly ICalculator<TValue> s_calculator = Calculators.GetInstance<TValue>();

		[SerializeField] private TRange _valueRange;
		public TRange _ValueRange => this._valueRange;

		private TValue _value;
		public TValue Value_
		{
			get => this._value;
			set
			{
				this._value = value;

				this._onValueChanged.Invoke();
				this._onValueChangedRatio.Invoke(this._value.ToSingle(null) / this._valueRange.Max.ToSingle(null));
			}
		}

		[SerializeField] private TValue _defaultValue;
		public TValue _DefaultValue => this._defaultValue;

		public TValue _DefaultValueClamped => s_calculator.Clamp(this._defaultValue, this._valueRange.Min, this._valueRange.Max);

		[SerializeField] private UnityEvent _onValueChanged = new UnityEvent();
		public UnityEvent _OnValueChanged => this._onValueChanged;

		[SerializeField] private UnityEvent<float> _onValueChangedRatio = new UnityEvent<float>();
		public UnityEvent<float> _OnValueChangedRatio => this._onValueChangedRatio;

		public void Add(TValue value)
		{
			//? Why not add it right away to the Value_? Because it will call the events twice.
			// You could also do double assignment with use of `this._value` but idk which is better. For redability this is definitely better for this whole class. So until there are performance problems I think it's fine.
			if (s_calculator.Add(this._value, value).CompareTo(this._valueRange.Max) > 0)
				this.Value_ = this._valueRange.Max;
			else
				this.Value_ = s_calculator.Add(this._value, value);
		}

		public void Reduce(TValue value)
		{
			if (s_calculator.Subtract(this._value, value).CompareTo(this._valueRange.Min) < 0)
				this.Value_ = this._valueRange.Min;
			else
				this.Value_ = s_calculator.Subtract(this._value, value);
		}

		public void Set(TValue value) => this.Value_ = s_calculator.Clamp(value, this._valueRange.Min, this._valueRange.Max);

		public void Maximize() => this.Value_ = this._valueRange.Max;
		public void Minimize() => this.Value_ = this._valueRange.Min;
		public void Randomize() => this.Value_ = this._valueRange.GetRandom();

		public void Reset() => this.Set(this._defaultValue);

		public BoundedValue(TRange valueRange, TValue defaultValue)
		{
			this._valueRange = valueRange;
			this._defaultValue = defaultValue;
		}
	}
}