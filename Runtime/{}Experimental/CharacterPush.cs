using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPush : MonoBehaviour
{
	[SerializeField] private LayerMask _layerMask;
	public LayerMask _PayerMask => this._layerMask;

	[SerializeField] private QueryTriggerInteraction _queryTriggerInteraction = QueryTriggerInteraction.Ignore;
	public QueryTriggerInteraction _QueryTriggerInteraction => this._queryTriggerInteraction;

	[SerializeField] private Transform _triggerPoint;
	public Transform _TriggerPoint => this._triggerPoint;

	[SerializeField] private float _triggerRadius = 0.2f;
	public float _TriggerRadius => this._triggerRadius;

	[SerializeField] private float _strength = 5.0f;
	public float _Strength => this._strength;

	[SerializeField] private Animator _animator;
	public Animator _Animator => this._animator;

	public bool Attempt(Vector3 direction, out Vector3 movement)
	{
		Collider[] colliders = Physics.OverlapSphere(
			position: this._triggerPoint.position,
			radius: this._triggerRadius,
			layerMask: this._layerMask,
			queryTriggerInteraction: this._queryTriggerInteraction
		);

		if (colliders.Length > 0)
		{
			TransformWaypointsPathMovement twpm = colliders[0].GetComponentInParent<TransformWaypointsPathMovement>();

			if (twpm.IsDirectionValid(direction: direction))
			{
				twpm.Move(
					strength: this._strength,
					direction: direction,
					out movement
				);

				this._animator.SetBool("Push", true);

				return true;
			}
		}

		movement = Vector3.zero;

		this._animator.SetBool("Push", false);

		return false;
	}

#if UNITY_EDITOR
	[SerializeField] private Color _eoGizmosColor = Color.magenta;

	private void OnDrawGizmos()
	{
		if (this._triggerPoint != null)
		{
			Gizmos.color = this._eoGizmosColor;

			Gizmos.DrawWireSphere(this._triggerPoint.position, this._triggerRadius);
		}
	}
#endif
}