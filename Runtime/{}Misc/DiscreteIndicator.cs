using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscreteIndicator : Indicator<int, UnityEvent<int>>
{
	protected override int Clamp(int value, int min, int max) => Mathf.Clamp(value: value, min: min, max: max);
}
