using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IRange<TValue> : ISerializationCallbackReceiver
{
	TValue Min { get; set; }
	TValue Max { get; set; }

	TValue _ValueVeryCloseToZero { get; }
	
	/// <summary>
	/// Get random value between min and max.
	/// </summary>
	/// <returns>Random value between Min and Max.</returns>
	TValue GetRandom();
	TValue Lerp(float t);
}

[System.Serializable]
public abstract class Range<TValue> : IRange<TValue>
{
	[SerializeField] private TValue _min;
	public TValue Min
	{
		get => this._min;
		set => this._min = value;
	}

	[SerializeField] private TValue _max;
	public TValue Max
	{
		get => this._max;
		set => this._max = value;
	}

	public abstract TValue _ValueVeryCloseToZero { get; }

	/// <summary>
	/// Get random value between min and max.
	/// </summary>
	/// <returns>Random value between min and max.</returns>
	public abstract TValue GetRandom();

	public abstract TValue Lerp(float t);

	protected Range(TValue min, TValue max)
	{
		this._min = min;
		this._max = max;
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
	}

#if UNITY_EDITOR
#endif
}