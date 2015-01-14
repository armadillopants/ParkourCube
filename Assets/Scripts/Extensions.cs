using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;


public static class Extensions
{

	#region Lists

	/// <summary>
	/// Adds the element to the list if it is not already there.
	/// </summary>
	/// <param name="element">The element to add.</param>
	public static void AddUnique<T>(this List<T> list, T element)
	{
		if(!list.Contains(element))
		{
			list.Add(element);
		}
	}

	#endregion

	#region Strings

	/// <summary>
	/// Returns true if the compare string is equal to this string, regardless of case.
	/// </summary>
	/// <param name="compare">The string to compare against.</param>
	public static bool EqualsIgnoreCase(this string source, string compare)
	{
		return source.Equals(compare, System.StringComparison.InvariantCultureIgnoreCase);
	}

	/// <summary>
	/// Creates an object of type T from the given string, if possible.
	/// </summary>
	/// <typeparam name="T">The type to convert to.</typeparam>
	public static T Parse<T>(this string value)
	{
		// Get default value for type so if string
		// is empty then we can return default value.
		T result = default(T);
		if(!string.IsNullOrEmpty(value))
		{
			// we are not going to handle exception here
			// if you need SafeParse then you should create
			// another method specially for that.
			TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
			result = (T)tc.ConvertFrom(value);
		}

		return result;
	}

	#endregion

	#region GameObjects

	#region Roots
	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this GameObject obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this GameObject obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Transform obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Transform obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Collider obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Collider obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this MonoBehaviour obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this MonoBehaviour obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Animation obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Animation obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this AudioSource obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this AudioSource obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this GUIText obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this GUIText obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this GUITexture obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this GUITexture obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this HingeJoint obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this HingeJoint obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Light obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Light obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this NetworkView obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this NetworkView obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this ParticleSystem obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this ParticleSystem obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this ParticleEmitter obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this ParticleEmitter obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Renderer obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Renderer obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Rigidbody obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Rigidbody obj) { return obj.transform.root.gameObject; }

	/// <summary>
	/// Get the root Transform.
	/// </summary>
	public static Transform RootTransform(this Rigidbody2D obj) { return obj.transform.root; }

	/// <summary>
	/// Get the root GameObject.
	/// </summary>
	public static GameObject RootGameObject(this Rigidbody2D obj) { return obj.transform.root.gameObject; }
	#endregion

	#region Relativity
	public static bool IsAbove(this Transform trans, Transform other)
	{
		return trans.position.y > other.position.y;
	}

	public static bool IsBelow(this Transform trans, Transform other)
	{
		return trans.position.y < other.position.y;
	}

	public static bool IsLeftOf(this Transform trans, Transform other)
	{
		return trans.position.x > other.position.x;
	}

	public static bool IsRightOf(this Transform trans, Transform other)
	{
		return trans.position.x < other.position.x;
	}

	public static bool IsOverlapping(this Transform trans, Transform other)
	{
		return trans.collider.bounds.Intersects(other.collider.bounds);
	}
	#endregion

	#endregion
}