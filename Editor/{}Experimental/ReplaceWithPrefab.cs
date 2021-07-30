using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class ReplaceWithPrefab : EditorWindow
{
	[SerializeField] private GameObject prefab;

	[MenuItem("Tools/Replace With Prefab")]
	private static void CreateReplaceWithPrefab()
	{
		EditorWindow.GetWindow<ReplaceWithPrefab>();
	}

	private void OnGUI()
	{
		this.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", this.prefab, typeof(GameObject), false);

		if (GUILayout.Button("Replace"))
		{
			var selection = Selection.gameObjects;

			for (var i = selection.Length - 1; i >= 0; --i)
			{
				var selected = selection[i];

				PrefabAssetType prefabAssetType = PrefabUtility.GetPrefabAssetType(this.prefab);
				PrefabInstanceStatus prefabInstanceStatus = PrefabUtility.GetPrefabInstanceStatus(this.prefab);

				GameObject newObject;

				if (prefabAssetType != PrefabAssetType.NotAPrefab && prefabAssetType != PrefabAssetType.MissingAsset)
				{
					newObject = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab);
				}
				else
				{
					newObject = Instantiate(this.prefab);
					newObject.name = this.prefab.name;
				}

				if (newObject == null)
				{
					Debug.LogError("Error instantiating prefab");
					break;
				}

				Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefabs");
				newObject.transform.parent = selected.transform.parent;
				newObject.transform.localPosition = selected.transform.localPosition;
				newObject.transform.localRotation = selected.transform.localRotation;
				newObject.transform.localScale = selected.transform.localScale;
				newObject.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
				Undo.DestroyObjectImmediate(selected);
			}
		}

		GUI.enabled = false;
		EditorGUILayout.LabelField("Selection count: " + Selection.objects.Length);
	}
}
#endif