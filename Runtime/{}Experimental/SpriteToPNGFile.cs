using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteToPNGFile : MonoBehaviour
{
#if UNITY_EDITOR
	[SerializeField] private Sprite _sprite;
	public Sprite _Sprite => this._sprite;

	[SerializeField] private Texture2D _texturePreview;
	public Texture2D _TexturePreview => this._texturePreview;

	public void SaveTexture(Texture2D texture2D)
	{
		this._texturePreview = texture2D;

		byte[] bytes = texture2D.EncodeToPNG();

		string directoryPath = Application.dataPath + "/SpriteToPNGFileOutput";

		if (!Directory.Exists(directoryPath))
		{
			Directory.CreateDirectory(directoryPath);
		}

		File.WriteAllBytes(directoryPath + "/Tex_" + GUID.Generate() + ".png", bytes);

		Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + directoryPath);

#if UNITY_EDITOR
		AssetDatabase.Refresh();
#endif
	}

	public void SaveSpriteAsTexture(Sprite sprite)
	{
		Texture2D texture = new Texture2D(
			(int)sprite.textureRect.width,
			(int)sprite.textureRect.height
		);

		Color[] pixels = sprite.texture.GetPixels(
			(int)sprite.textureRect.x,
			(int)sprite.textureRect.y,
			(int)sprite.textureRect.width,
			(int)sprite.textureRect.height
		);

		texture.SetPixels(pixels);
		texture.Apply();

		this.SaveTexture(texture2D: texture);
	}

	[CustomEditor(inspectedType: typeof(SpriteToPNGFile))]
	public class SpriteToPNGFileEditor : Editor
	{
		private SpriteToPNGFile _target;

		private void OnEnable()
		{
			this._target = (SpriteToPNGFile)this.target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Generate PNG"))
			{
				this._target.SaveSpriteAsTexture(sprite: this._target._sprite);
			}
		}
	}
#endif
}