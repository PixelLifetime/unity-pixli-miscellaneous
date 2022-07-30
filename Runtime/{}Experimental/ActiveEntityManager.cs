using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: Change naming of all of the things in here honestly.
public class ActiveEntityManager<TEntity> : MonoBehaviour
{
	[SerializeField] private ProviderMonoBehaviour<TEntity> _provider;
	public ProviderMonoBehaviour<TEntity> _Provider => this._provider;

	[SerializeField] private UnityEvent<TEntity> _onActiveEntityChanged;
	public UnityEvent<TEntity> _OnActiveEntityChanged => this._onActiveEntityChanged;

	private TEntity _activeEntity;
	public TEntity ActiveEntity
	{
		get => this._activeEntity;
		set
		{
			this._activeEntity = value;
			
			this._onActiveEntityChanged.Invoke(this._activeEntity);
		}
	}

	[ContextMenu("F: Next")]
	public void Next()
	{
		this.ActiveEntity = this._provider.Provide();
	}
}