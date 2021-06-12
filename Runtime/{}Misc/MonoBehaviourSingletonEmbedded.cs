/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonoBehaviourSingletonEmbedded<T>
	where T : MonoBehaviour
{
	public static T Instance_ { get; protected set; }

	public T _Instance { get { return Instance_; } }

	protected virtual void WarningMessage(T creator)
	{
#if UNITY_EDITOR
		// This is just a hint
		// If the types are different it will throw an error anyway in future steps
		if (creator.GetType() != typeof(T))
			Debug.LogWarning("Instance type: " + creator.GetType().Name + " and encapsulation type: " + typeof(T).Name + " are different. This may lead to errors or unexpected behavior.");
#endif
	}

	protected virtual void InitializeInstance(T creator)
	{
		if (Instance_ == null)
		{
			Instance_ = creator;
			Instance_.transform.SetParent(null);

			//Transform rootTransform = Instance_.transform;

			//while (rootTransform.parent != null)
			//	rootTransform = rootTransform.parent;

			//Object.DontDestroyOnLoad(Instance_.gameObject);
		}
		else if (Instance_ != creator)
		{
			Object.Destroy(creator.gameObject);
		}
	}

	public MonoBehaviourSingletonEmbedded(T creator)
	{
		this.WarningMessage(creator);
		this.InitializeInstance(creator);
	}
}

///<summary>
/// Snippet
/// 
/// public static MonoBehaviourSingletonEmbedded<$classname$> S_Singleton_ { get; private set; }
/// public static $classname$ _Instance { get { return S_Singleton_._Instance; ; } }
/// S_Singleton_ = new MonoBehaviourSingletonEmbedded<$classname$>(this);
///</summary>