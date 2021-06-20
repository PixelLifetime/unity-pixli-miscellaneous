using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameStateManager : MonoBehaviourReverseSingleton<GameStateManager>
{
	protected override void Awake()
	{
		base.Awake();

		if (GameState._Instance == null) // Null check is important, it's also a way to initialize and find GameState on Awake to prevent spike during gameplay.
		{
#if UNITY_EDITOR
			Debug.LogError("Game State instance could neither be found nor created.");
#endif
		}
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameStateManager))]
[CanEditMultipleObjects]
public class GameStateManagerEditor : Editor
{
#pragma warning disable 0219, 414
    private GameStateManager _sGameStateManager;
#pragma warning restore 0219, 414

    private void OnEnable()
    {
        this._sGameStateManager = this.target as GameStateManager;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
#endif