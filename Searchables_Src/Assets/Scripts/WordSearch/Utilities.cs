using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace WordSearch
{
	public static class Utilities
	{
		public delegate TResult MapFunc<out TResult, TArg>(TArg arg);

		public delegate bool FilterFunc<TArg>(TArg arg);

		public static double SystemTimeInMilliseconds
		{
			get
			{
				return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
			}
		}

		public static float WorldWidth
		{
			get
			{
				return 2f * Camera.main.orthographicSize * Camera.main.aspect;
			}
		}

		public static float WorldHeight
		{
			get
			{
				return 2f * Camera.main.orthographicSize;
			}
		}

		public static float XScale
		{
			get
			{
				return (float)Screen.width / 1080f;
			}
		}

		public static float YScale
		{
			get
			{
				return (float)Screen.height / 1920f;
			}
		}

		public static List<TOut> Map<TIn, TOut>(List<TIn> list, Utilities.MapFunc<TOut, TIn> func)
		{
			List<TOut> list2 = new List<TOut>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(func(list[i]));
			}
			return list2;
		}

		public static void Filter<T>(List<T> list, Utilities.FilterFunc<T> func)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (func(list[i]))
				{
					list.RemoveAt(i);
				}
			}
		}

		public static void SwapValue<T>(ref T value1, ref T value2)
		{
			T t = value1;
			value1 = value2;
			value2 = t;
		}

		public static float EaseOut(float t)
		{
			return 1f - (1f - t) * (1f - t) * (1f - t);
		}

		public static float EaseIn(float t)
		{
			return t * t * t;
		}

		public static Vector2 MousePosition()
		{
			if (UnityEngine.Input.touchCount > 0)
			{
				return Input.touches[0].position;
			}
			return Vector2.zero;
		}

		public static bool MouseDown()
		{
			return Input.GetMouseButtonDown(0) || (UnityEngine.Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began);
		}

		public static bool MouseUp()
		{
			return Input.GetMouseButtonUp(0) || (UnityEngine.Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended);
		}

		public static bool MouseNone()
		{
			return !Input.GetMouseButton(0) && UnityEngine.Input.touchCount == 0;
		}

		public static char CharToLower(char c)
		{
			return (char)((c < 'A' || c > 'Z') ? c : (c + ' '));
		}

		public static int GCD(int a, int b)
		{
			int num = Mathf.Min(a, b);
			for (int i = num; i > 1; i--)
			{
				if (a % i == 0 && b % i == 0)
				{
					return i;
				}
			}
			return 1;
		}

		public static Canvas GetCanvas(Transform transform)
		{
			if (transform == null)
			{
				return null;
			}
			Canvas component = transform.GetComponent<Canvas>();
			if (component != null)
			{
				return component;
			}
			return Utilities.GetCanvas(transform.parent);
		}

		public static void CallExternalAndroid(string methodname, params object[] args)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			@static.Call(methodname, args);
		}

		public static T CallExternalAndroid<T>(string methodname, params object[] args)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			return @static.Call<T>(methodname, args);
		}

		public static List<T> SyncList<T>(int count, T prefab, Transform parent, List<T> list) where T : MonoBehaviour
		{
			while (list.Count < count)
			{
				T item = UnityEngine.Object.Instantiate<T>(prefab);
				item.transform.SetParent(parent, false);
				list.Add(item);
			}
			while (list.Count > count)
			{
				T t = list[list.Count - 1];
				UnityEngine.Object.Destroy(t.gameObject);
				list.RemoveAt(list.Count - 1);
			}
			return list;
		}

		public static string ConvertToJsonString(object data, bool addQuoteEscapes = false)
		{
			string text = string.Empty;
			if (data is IDictionary)
			{
				Dictionary<string, object> dictionary = data as Dictionary<string, object>;
				text += "{";
				List<string> list = new List<string>(dictionary.Keys);
				for (int i = 0; i < list.Count; i++)
				{
					if (i != 0)
					{
						text += ",";
					}
					if (addQuoteEscapes)
					{
						text += string.Format("\\\"{0}\\\":{1}", list[i], Utilities.ConvertToJsonString(dictionary[list[i]], addQuoteEscapes));
					}
					else
					{
						text += string.Format("\"{0}\":{1}", list[i], Utilities.ConvertToJsonString(dictionary[list[i]], addQuoteEscapes));
					}
				}
				text += "}";
			}
			else if (data is IList)
			{
				IList list2 = data as IList;
				text += "[";
				for (int j = 0; j < list2.Count; j++)
				{
					if (j != 0)
					{
						text += ",";
					}
					text += Utilities.ConvertToJsonString(list2[j], addQuoteEscapes);
				}
				text += "]";
			}
			else if (data is string)
			{
				if (addQuoteEscapes)
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"\\\"",
						data,
						"\\\""
					});
				}
				else
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						"\"",
						data,
						"\""
					});
				}
			}
			else if (data is bool)
			{
				text += ((!(bool)data) ? "false" : "true");
			}
			else
			{
				text += data.ToString();
			}
			return text;
		}

		public static void SetLayer(GameObject gameObject, int layer, bool applyToChildren = false)
		{
			gameObject.layer = layer;
			if (applyToChildren)
			{
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					Utilities.SetLayer(gameObject.transform.GetChild(i).gameObject, layer, true);
				}
			}
		}

		public static List<string[]> ParseCSVFile(string fileContents, char delimiter)
		{
			List<string[]> list = new List<string[]>();
			string[] array = fileContents.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i].Split(new char[]
				{
					delimiter
				}));
			}
			return list;
		}

		public static void DestroyAllChildren(Transform parent)
		{
			for (int i = parent.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(parent.GetChild(i).gameObject);
			}
		}

		public static string FindFile(string fileName, string directory)
		{
			List<string> list = new List<string>(Directory.GetFiles(directory));
			string[] directories = Directory.GetDirectories(directory);
			for (int i = 0; i < list.Count; i++)
			{
				if (fileName == Path.GetFileNameWithoutExtension(list[i]))
				{
					return list[i];
				}
			}
			for (int j = 0; j < directories.Length; j++)
			{
				string text = Utilities.FindFile(fileName, directories[j]);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return null;
		}

		public static string CalculateMD5Hash(string input)
		{
			MD5 mD = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(input);
			byte[] array = mD.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		public static bool CompareLists<T>(List<T> list1, List<T> list2)
		{
			if (list1.Count != list2.Count)
			{
				return false;
			}
			for (int i = list1.Count - 1; i >= 0; i--)
			{
				bool flag = false;
				for (int j = 0; j < list2.Count; j++)
				{
					T t = list1[i];
					if (t.Equals(list2[j]))
					{
						flag = true;
						list1.RemoveAt(i);
						list2.RemoveAt(j);
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		public static void PrintList<T>(List<T> list)
		{
			string text = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				if (i != 0)
				{
					text += ", ";
				}
				string arg_35_0 = text;
				T t = list[i];
				text = arg_35_0 + t.ToString();
			}
			UnityEngine.Debug.Log(text);
		}
	}
}
