using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PixLi
{
	public interface ICalculator { }

	public interface ICalculator<T> : ICalculator
	{
		T Add(T a, T b);
		T Subtract(T a, T b);
		T Multiply(T a, T b);
		T Divide(T a, T b);
		T Clamp(T value, T min, T max);

		//float ConvertToFloat(T value);
	}

	public static class Calculators
	{
		private static readonly Dictionary<Type, ICalculator> _calculators = new Dictionary<Type, ICalculator>() {
			{ typeof(int), new IntCalculator() },
			{ typeof(float), new FloatCalculator() }
		};

		public static ICalculator<T> GetInstance<T>() => (ICalculator<T>)_calculators[typeof(T)];
	}

	public class IntCalculator : ICalculator<int>
	{
		public int Add(int a, int b) => a + b;
		public int Subtract(int a, int b) => a - b;
		public int Multiply(int a, int b) => a * b;
		public int Divide(int a, int b) => a / b;

		public int Clamp(int value, int min, int max) => Mathf.Clamp(value: value, min: min, max: max);

		public float ConvertToFloat(int value) => value;
	}

	public class FloatCalculator : ICalculator<float>
	{
		public float Add(float a, float b) => a + b;
		public float Subtract(float a, float b) => a - b;
		public float Multiply(float a, float b) => a * b;
		public float Divide(float a, float b) => a / b;

		public float Clamp(float value, float min, float max) => Mathf.Clamp(value: value, min: min, max: max);

		public float ConvertToFloat(float value) => value;
	}
}