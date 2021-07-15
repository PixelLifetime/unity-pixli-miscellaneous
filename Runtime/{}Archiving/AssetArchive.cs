using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixLi.RandomDistribution;
using Random = PixLi.RandomDistribution.Random;

namespace PixLi
{
	[System.Serializable]
	public abstract class AssetArchive<TAsset> : ScriptableObject, IArchive<TAsset>
	{
		private int _selectedIndex = 0;

		[SerializeField] private RandomDistributionProvider<TAsset> _randomDistributionProvider = new RandomDistributionProvider<TAsset>();
		public RandomDistributionProvider<TAsset> _RandomDistributionProvider => this._randomDistributionProvider;

		//TODO: I kinda don't like this. You should be able to access assets directly. (bad idea 1)[syncronizing 2 arrays] (good idea)[find some method to separate assets and table logic without syncing arrays as well].
		public List<TableData<TAsset>> Assets => this._randomDistributionProvider._Data;

		public TAsset _SelectedAsset => this._randomDistributionProvider._Data[this._selectedIndex].Object;

		public TAsset Previous(bool loop = true)
		{
			this._selectedIndex = loop
								  ? ((--this._selectedIndex + this._randomDistributionProvider._Data.Count) % this._randomDistributionProvider._Data.Count)
								  : Mathf.Clamp(--this._selectedIndex, 0, this._randomDistributionProvider._Data.Count - 1);

			return this._SelectedAsset;
		}

		public TAsset Next(bool loop = true)
		{
			this._selectedIndex = loop
								  ? (++this._selectedIndex % this._randomDistributionProvider._Data.Count)
								  : Mathf.Clamp(++this._selectedIndex, 0, this._randomDistributionProvider._Data.Count - 1);

			return this._SelectedAsset;
		}

		//[SerializeField] private bool _doNotRepeatRandomAsset;
		//public bool _DoNotRepeatRandomAsset => this._doNotRepeatRandomAsset;

		private TableData<TAsset> _lastProvidedEntry;

		public TAsset Random()
		{
			//if (this._doNotRepeatRandomAsset)
			//{
			//	TableData<TAsset> previouslyProvidedEntry = this._lastProvidedEntry;

			//	this._lastProvidedEntry = this._randomDistributionProvider.Provide();
			//	this._lastProvidedEntry.Selectable = false;

			//	if (previouslyProvidedEntry != null)
			//		previouslyProvidedEntry.Selectable = true;
			//}
			//else
				this._lastProvidedEntry = this._randomDistributionProvider.Provide();

			//Debug.Log(this._lastProvidedEntry.Object);

			return this._lastProvidedEntry.Object;
		}

		public virtual void Initialize(Random random)
		{
			this._randomDistributionProvider.Initialize(random: random);
		}

		public void Initialize() => this.Initialize(new Random());

		public virtual void Reset()
		{
			this._selectedIndex = 0;
		}

		protected virtual void OnEnable()
		{
			this.Reset();

			this.Initialize();
		}
	}
}