using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Custom Cursor Data Adapter]", menuName = "[Misc]/[Custom Cursor Data Adapter]")]
public class CustomCursorDataAdapter : ScriptableObject
{
	[SerializeField] private CustomCursor.Data _data;
	public CustomCursor.Data _Data => this._data;
}