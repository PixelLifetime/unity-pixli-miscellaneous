using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

//TODO: Move to separate file.
[Serializable]
public struct RaycastData
{
	[SerializeField] private float _maxDistance;
	public float _MaxDistance => this._maxDistance;

	[SerializeField] private LayerMask _layerMask;
	public LayerMask _LayerMask => this._layerMask;

	[SerializeField] private QueryTriggerInteraction _queryTriggerInteraction;
	public QueryTriggerInteraction _QueryTriggerInteraction => this._queryTriggerInteraction;
}

public class RaycastEmitter : MonoBehaviour
{
	[Serializable]
	private class EditorOnly
	{
		[SerializeField] private bool _debug = true;
		public bool _Debug => this._debug;

		[SerializeField] private Color _debugColor = Color.white;
		public Color _DebugColor => this._debugColor;
	}

	[SerializeField] private RaycastData _raycastData;
	public RaycastData _RaycastData => this._raycastData;

#if UNITY_EDITOR
	[SerializeField] private EditorOnly _editorOnly;
#endif

	public bool Raycast(Ray ray, out RaycastHit raycastHit)
	{
#if UNITY_EDITOR
		if (this._editorOnly._Debug)
			Debug.DrawRay(ray.origin, ray.direction * this._raycastData._MaxDistance, this._editorOnly._DebugColor);
#endif

		return RaycastUtility.Raycast(
			ray: ray,
			raycastHit: out raycastHit,
			raycastData: this._raycastData
		);
	}

	public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit raycastHit)
	{
#if UNITY_EDITOR
		if (this._editorOnly._Debug)
			Debug.DrawRay(origin, direction * this._raycastData._MaxDistance, this._editorOnly._DebugColor);
#endif

		return RaycastUtility.Raycast(
			origin: origin,
			direction: direction,
			raycastHit: out raycastHit,
			raycastData: this._raycastData
		);
	}

#if ENABLE_INPUT_SYSTEM
	public bool Raycast(Camera camera, out RaycastHit raycastHit) => this.Raycast(ray: camera.ScreenPointToRay(pos: Mouse.current.position.ReadValue()), out raycastHit);
#endif
}

//TODO: Move to a separate file.
public static class RaycastUtility
{
	public static bool Raycast(Ray ray, out RaycastHit raycastHit, RaycastData raycastData) => Physics.Raycast(
		ray: ray,
		hitInfo: out raycastHit,
		maxDistance: raycastData._MaxDistance,
		layerMask: raycastData._LayerMask,
		queryTriggerInteraction: raycastData._QueryTriggerInteraction
	);

	public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit raycastHit, RaycastData raycastData) => Physics.Raycast(
		origin: origin,
		direction: direction,
		hitInfo: out raycastHit,
		maxDistance: raycastData._MaxDistance,
		layerMask: raycastData._LayerMask,
		queryTriggerInteraction: raycastData._QueryTriggerInteraction
	);

#if UNITY_EDITOR
	public static void VisualizeRaycast(Ray ray, RaycastData raycastData, Color? color = null)
	{
		Gizmos.color = color ?? Color.green;
		Gizmos.DrawLine(
			from:	ray.origin,
			to:		ray.direction * raycastData._MaxDistance
		);

		//Debug.DrawRay(
		//	start: ray.origin, 
		//	dir: ray.direction * raycastData._MaxDistance, 
		//	color: color ?? Color.green
		//);
	}
#endif
}

//TODO: Move to a separate file.
public static class PhysicsUtility
{
	/// <summary>
	/// Returns true if there are any colliders overlapping the sphere defined by parameters of a raycast.
	/// Useful for checking if the newly instantiated object that uses raycasting wasn't instantiated inside an object that it can hit.
	/// </summary>
	/// <param name="position">Center of the sphere.</param>
	/// <param name="raycastData">Parameters used in raycast.</param>
	/// <returns>True if there are any colliders overlapping the sphere.</returns>
	public static bool CheckSphere(Vector3 position, RaycastData raycastData) => Physics.CheckSphere(
		position:					position,
		radius:						raycastData._MaxDistance,
		layerMask:					raycastData._LayerMask,
		queryTriggerInteraction:	raycastData._QueryTriggerInteraction
	);

	/// <summary>
	/// Computes and stores colliders touching or inside the sphere defined by parameters of a raycast.
	/// Useful for getting colliders when a newly instantiated object may have been instantiated inside an object(s) that it can hit.
	/// </summary>
	/// <param name="position">Center of the sphere.</param>
	/// <param name="raycastData">Parameters used in raycast.</param>
	/// <returns>An array with all colliders touching or inside the sphere.</returns>
	public static Collider[] OverlapSphere(Vector3 position, RaycastData raycastData) => Physics.OverlapSphere(
		position:					position,
		radius:						raycastData._MaxDistance,
		layerMask:					raycastData._LayerMask,
		queryTriggerInteraction:	raycastData._QueryTriggerInteraction
	);
}