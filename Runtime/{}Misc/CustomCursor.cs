using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviourSingleton<CustomCursor>
{
	[SerializeField] private Data _defaultData = new Data(null, new Vector2(32.0f, 32.0f), Vector2.zero, 0.0f, -1);
	public Data _DefaultCursorData => this._defaultData;

	internal sealed class DataComparer : IComparer<Data>
	{
		public int Compare(Data x, Data y) => x.Order.CompareTo(value: y.Order);
	}
	private SortedSet<Data> _dataSet = new SortedSet<Data>(comparer: new DataComparer());

	public void Add(Data data)
	{
		if (!this._dataSet.Contains(item: data))
			this._dataSet.Add(item: data);
	}

	public void Remove(Data data) => this._dataSet.Remove(item: data);

	private void OnGUI()
	{
		Data data = this._dataSet.Max;

		Matrix4x4 matrix = GUI.matrix;

		float x = Event.current.mousePosition.x + data.Hotspot.x;
		float y = Event.current.mousePosition.y + data.Hotspot.y;

		Vector2 pivot = new Vector2(x: Event.current.mousePosition.x, y: Event.current.mousePosition.y);

		GUIUtility.RotateAroundPivot(angle: data.Angle, pivotPoint: pivot);

		GUI.DrawTexture(new Rect(x, y, data.Size.x, data.Size.y), data.Texture2D);

		GUI.matrix = matrix;
	}

	protected override void Awake()
	{
		base.Awake();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		this._dataSet.Add(item: this._defaultData);
	}

	[Serializable]
	public class Data
	{
		public Texture2D Texture2D;
		public Vector2 Size = new Vector2(32.0f, 32.0f);
		public Vector2 Hotspot;
		public float Angle;

		[SerializeField] private int _order;
		public int Order => this._order;

		public Data(Texture2D texture2D, Vector2 size, Vector2 hotspot, float angle, int order)
		{
			this.Texture2D = texture2D;
			this.Size = size;
			this.Hotspot = hotspot;
			this.Angle = angle;
			this._order = order;
		}

		public override bool Equals(object obj)
		{
			return obj is Data other &&
				   EqualityComparer<Texture2D>.Default.Equals(this.Texture2D, other.Texture2D) &&
				   this.Size.Equals(other.Size) &&
				   this.Hotspot.Equals(other.Hotspot) &&
				   this.Angle == other.Angle;
		}

		public override int GetHashCode()
		{
			int hashCode = -1971360830;
			hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.Texture2D);
			hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
			hashCode = hashCode * -1521134295 + this.Hotspot.GetHashCode();
			hashCode = hashCode * -1521134295 + this.Angle.GetHashCode();
			hashCode = hashCode * -1521134295 + this._order.GetHashCode();
			return hashCode;
		}

		public void Deconstruct(out Texture2D texture2D, out Vector2 size, out Vector2 hotspot, out float angle, out int order)
		{
			texture2D = this.Texture2D;
			size = this.Size;
			hotspot = this.Hotspot;
			angle = this.Angle;
			order = this._order;
		}

		public static implicit operator (Texture2D Texture2D, Vector2 Size, Vector2 Hotspot, float Angle, int Order) (Data value)
		{
			return (value.Texture2D, value.Size, value.Hotspot, value.Angle, value._order);
		}

		public static implicit operator Data((Texture2D Texture2D, Vector2 Size, Vector2 Hotspot, float Angle, int Order) value)
		{
			return new Data(value.Texture2D, value.Size, value.Hotspot, value.Angle, value.Order);
		}
	}
}