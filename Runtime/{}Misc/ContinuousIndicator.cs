using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContinuousIndicator : Indicator<float, UnityEvent<float>>
{
	protected override float Clamp(float value, float min, float max) => Mathf.Clamp(value: value, min: min, max: max);
}
