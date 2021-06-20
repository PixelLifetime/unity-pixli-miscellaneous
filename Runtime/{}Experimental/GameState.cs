using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Reload-proof Singleton
[CreateAssetMenu(fileName = GameState.DEFAULT_GAME_STATE_NAME, menuName = "Game State", order = 202)]
public class GameState : ScriptableObjectSingleton<GameState>
{
	private const string DEFAULT_GAME_STATE_NAME = "[Game State] Default";

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void CreateGameStateManager()
	{
		new GameObject("[Game State Manager]", typeof(GameStateManager));
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameState))]
[CanEditMultipleObjects]
public class GameStateEditor : Editor
{
#pragma warning disable 0219, 414
	private GameState _sGameState;
#pragma warning restore 0219, 414

	private void OnEnable()
	{
		this._sGameState = this.target as GameState;
	}

	public override void OnInspectorGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif