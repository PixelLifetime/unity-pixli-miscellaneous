using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseDissolveAnimationController : MonoBehaviour
{
	//[SerializeField] private MeshRenderer[] _dissolvableObjectsMeshRenderers;
	//public MeshRenderer[] _DissolvableObjectsMeshRenderers => this._dissolvableObjectsMeshRenderers;

	//private Material[] _cachedMaterials;

	[SerializeField] private Material _material;
	public Material _Material => this._material;

	//[SerializeField] private MeshRenderer _meshRenderer;
	//public MeshRenderer _MeshRenderer => this._meshRenderer;

	[SerializeField] private AnimationCurve _dissolveIn;
	public AnimationCurve _DissolveIn => this._dissolveIn;

	[SerializeField] private AnimationCurve _dissolveOut;
	public AnimationCurve _DissolveOut => this._dissolveOut;

	[SerializeField] private float _time = 1.5f;
	public float _Time => this._time;

	//private MaterialPropertyBlock _materialPropertyBlock;

	private int _dissolveAmountNameId;

	private IEnumerator DissolveProcess(AnimationCurve animationCurve, float targetTime)
	{
		float time = 0.0f;

		while (time < targetTime)
		{
			//this._materialPropertyBlock.SetFloat()
			this._material.SetFloat(nameID: this._dissolveAmountNameId, value: animationCurve.Evaluate(time / targetTime));

			yield return null;

			time += Time.deltaTime;
		}
	}

	public void Dissolve(bool visible)
	{
		this.StartCoroutine(
			routine: this.DissolveProcess(
				animationCurve: visible ? this._dissolveOut : this._dissolveIn,
				targetTime: this._time
			)
		);
	}

	private void Awake()
	{
		this._dissolveAmountNameId = Shader.PropertyToID("_Dissolve");

		// ---

		//this._materialPropertyBlock = new MaterialPropertyBlock();

		// ---

		//this._cachedMaterials = new Material[this._dissolvableObjectsMeshRenderers.Length];

		//for (int a = 0; a < this._dissolvableObjectsMeshRenderers.Length; a++)
		//{
		//	this._cachedMaterials[a] = this._dissolvableObjectsMeshRenderers[a].material;
		//}
	}

#if UNITY_EDITOR
	private void Reset()
	{
		//this._dissolvableObjectsMeshRenderers = this.GetComponentsInChildren<MeshRenderer>();
	}
#endif
}