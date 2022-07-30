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
	public struct RangeFloat : IRange<float>
	{
		[SerializeField] private float _min;
		public float Min
		{
			get => this._min;
			set => this._min = value;
		}

		[SerializeField] private float _max;
		public float Max
		{
			get => this._max;
			set => this._max = value;
		}

		public float _ValueVeryCloseToZero => Mathf.Epsilon;

		public float GetRandom() => Random.Range(this.Min, this.Max);

		public float Lerp(float t) => Mathf.Lerp(this.Min, this.Max, t);

		public RangeFloat(float min, float max)
		{
			this._min = min;
			this._max = max;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			//this.Random = Random._GlobalRandom;
		}

#if UNITY_EDITOR
#endif
	}
}