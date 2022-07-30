using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PixLi
{
	[System.Serializable]
	public struct RangeInt : IRange<int>
	{
		[SerializeField] private int _min;
		public int Min
		{
			get => this._min;
			set => this._min = value;
		}

		[SerializeField] private int _max;
		public int Max
		{
			get => this._max;
			set => this._max = value;
		}

		public int _ValueVeryCloseToZero => 0;

		public int GetRandom() => Random.Range(this.Min, this.Max);

		public int Lerp(float t) => (int)(this.Min + t * (this.Max - this.Min));

		public RangeInt(int min, int max)
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