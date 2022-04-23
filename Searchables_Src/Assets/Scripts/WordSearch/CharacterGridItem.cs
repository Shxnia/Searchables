using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class CharacterGridItem : MonoBehaviour
	{
		public bool IsFound;

		public Text characterText;

		private int _Row_k__BackingField;

		private int _Col_k__BackingField;

		public int Row
		{
			get;
			set;
		}

		public int Col
		{
			get;
			set;
		}
	}
}
