using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformWaypointsPathMovement : MonoBehaviour
{
	[SerializeField] private bool _updatePosition = true;
	public bool _UpdatePosition => this._updatePosition;

	//[SerializeField] private AnimationCurve _positionCurve;
	//public AnimationCurve _PositionCurve => this._positionCurve;

	[SerializeField] private bool _updateRotation;
	public bool _UpdateRotation => this._updateRotation;

	[SerializeField] private bool _updateLocalScale;
	public bool _UpdateLocalScale => this._updateLocalScale;

	[SerializeField] private Transform[] _waypoints;
	public Transform[] _Waypoints => this._waypoints;

	[SerializeField] private Transform _movable;
	public Transform _Movable => this._movable;

	private int _freeCurrentWaypointIndex;

	public static int Oscillate(int input, int min, int max)
	{
		int range = max - min;
		return min + Math.Abs(((input + range) % (range * 2)) - range);
	}

	[SerializeField] private bool _loop;
	public bool _Loop => this._loop;

	private int _currentWaypointIndex => this._loop ? this._freeCurrentWaypointIndex % this._waypoints.Length : Oscillate(this._freeCurrentWaypointIndex, 0, this._waypoints.Length - 1);
	private int _nextWaypointIndex => this._loop ? (this._freeCurrentWaypointIndex + 1) % this._waypoints.Length : Oscillate(this._freeCurrentWaypointIndex + 1, 0, this._waypoints.Length - 1);

	public bool IsDirectionValid(Vector3 direction)
	{
		Transform currentWaypoint = this._waypoints[this._currentWaypointIndex];
		Transform targetWaypoint = this._waypoints[this._nextWaypointIndex];

		Vector3 directionToTargetWaypoint = targetWaypoint.position - currentWaypoint.position;

		return MathUtil.GetProjectionValue(direction, directionToTargetWaypoint) > 0.7f;
	}

	[SerializeField] private Rigidbody _rigidbody;
	public Rigidbody _Rigidbody => this._rigidbody;

	public void Move(float strength, Vector3 direction, out Vector3 movement)
	{
		movement = Vector3.zero;

		if (this._currentWaypointIndex + 1 < this._waypoints.Length)
		{
			Transform currentWaypoint = this._waypoints[this._currentWaypointIndex];
			Transform targetWaypoint = this._waypoints[this._nextWaypointIndex];

			Vector3 directionToTargetWaypoint = targetWaypoint.position - currentWaypoint.position;

			movement = directionToTargetWaypoint.normalized * strength * Time.fixedDeltaTime;

			//this._movable.position = this._movable.position + movement;

			this._rigidbody.MovePosition(this._rigidbody.position + movement);

			//Debug.Log(MathUtil.GetProjectionValue(directionToTargetWaypoint, targetWaypoint.position - this._movable.position));
			if (MathUtil.GetProjectionValue(directionToTargetWaypoint, targetWaypoint.position - this._movable.position) < 0)
			{
				//this._movable.position = targetWaypoint.position;
				this._rigidbody.MovePosition(targetWaypoint.position);

				++this._freeCurrentWaypointIndex;
			}
		}
	}

	//public void MoveToNextWaypoint()
	//{
	//}

	private Coroutine _coroutine;

	private IEnumerator TraverseAllWaypointsProcess(float targetTime)
	{
		float time = 0.0f;

		float timePerWaypoint = targetTime / (this._loop ? this._waypoints.Length : (this._waypoints.Length - 1));

		//Debug.LogError($"targetTime: {targetTime.ToString("0.00")} • this._waypoints.Length: {this._waypoints.Length} • timePerWaypoint: {timePerWaypoint.ToString("0.00")}");

		// Hacky.
		int addedIndex = 1;

		while (time < targetTime)
		{
			//TODO: You could do distance ratio here between waypoints instead of array length which makes them all equal.

			float tOfWaypoint = (time % timePerWaypoint) / timePerWaypoint;

			if (time / timePerWaypoint >= addedIndex)
			{
				++this._freeCurrentWaypointIndex;

				++addedIndex;
			}

			Transform currentWaypoint = this._waypoints[this._currentWaypointIndex];
			Transform targetWaypoint = this._waypoints[this._nextWaypointIndex];

			if (this._updatePosition)
				this._movable.position = Vector3.Lerp(currentWaypoint.position, targetWaypoint.position, tOfWaypoint);

			if (this._updateRotation)
				this._movable.rotation = Quaternion.Lerp(currentWaypoint.rotation, targetWaypoint.rotation, tOfWaypoint);

			if (this._updateLocalScale)
				this._movable.localScale =  Vector3.Lerp(currentWaypoint.localScale, targetWaypoint.localScale, tOfWaypoint);

			//Debug.Log($"time: {time.ToString("0.00")} • tOfWaypoint: {tOfWaypoint.ToString("0.00")} • targetWaypoint: {targetWaypoint} • this._currentWaypointIndex: {this._currentWaypointIndex} • this._nextWaypointIndex: {this._nextWaypointIndex}");

			yield return null;

			time += Time.deltaTime;
		}

		++this._freeCurrentWaypointIndex;

		this._movable.position = this._waypoints[this._currentWaypointIndex].position;

		//Debug.Log($"End Of Cycle • this._currentWaypointIndex: {this._currentWaypointIndex} • this._nextWaypointIndex: {this._nextWaypointIndex}");

		this._coroutine = null;
	}

	public void TraverseAllWaypoints(bool repeat, float time)
	{
		if (this._coroutine == null)
		{
			if (repeat)
			{
				this.StartCoroutine(
					routine: CoroutineProcessorsCollection.InvokeAfter(
						yieldInstruction: this.StartCoroutine(this.TraverseAllWaypointsProcess(targetTime: time)),
						action: () =>
						{
							this.TraverseAllWaypoints(repeat: repeat, time: time);
						}
					)
				);
			}
			else
			{
				this.StartCoroutine(
					routine: this.TraverseAllWaypointsProcess(targetTime: time)
				);
			}
		}
	}

	[SerializeField] private bool _repeat;
	public bool _Repeat => this._repeat;

	[SerializeField] private float _time;
	public float _Time => this._time;

	public void TraverseAllWaypoints() => this.TraverseAllWaypoints(repeat: this._repeat, time: this._time);
}