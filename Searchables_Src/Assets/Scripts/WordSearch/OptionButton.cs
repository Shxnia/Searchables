using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class OptionButton : Button
	{
		private sealed class _WaitThenStartShowAnimation_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float delay;

			internal OptionButton _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _WaitThenStartShowAnimation_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(this.delay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.StartShowAnimation();
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		public Text uiText;

		public Color textNormalColor = Color.white;

		public Color textHighlightedColor = Color.white;

		public Color textPressedColor = Color.white;

		public Color textDisabledColor = Color.white;

		public Color bkgSelectedColor = Color.white;

		public Color bkgNormalColor = Color.white;

		public Color textSelectedColor = Color.white;

		public CanvasGroup canvasGroup;

		private int index;

		private Action<int> onOptionClicked;

		private bool isSelected;

		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
			set
			{
				this.isSelected = value;
				ColorBlock colors = base.colors;
				colors.normalColor = ((!this.isSelected) ? this.bkgNormalColor : this.bkgSelectedColor);
				base.colors = colors;
			}
		}

		public void Update()
		{
			if (this.uiText == null)
			{
				return;
			}
			if (!base.interactable)
			{
				this.uiText.color = this.textDisabledColor;
			}
			else if (base.currentSelectionState == Selectable.SelectionState.Normal)
			{
				this.uiText.color = ((!this.isSelected) ? this.textNormalColor : this.textSelectedColor);
			}
			else if (base.currentSelectionState == Selectable.SelectionState.Highlighted)
			{
				this.uiText.color = this.textHighlightedColor;
			}
			else if (base.currentSelectionState == Selectable.SelectionState.Pressed)
			{
				this.uiText.color = this.textPressedColor;
			}
			else if (base.currentSelectionState == Selectable.SelectionState.Disabled)
			{
				this.uiText.color = this.textDisabledColor;
			}
		}

		public void Setup(string difficultyName, int index, Action<int> onToggleChanged)
		{
			this.index = index;
			this.onOptionClicked = onToggleChanged;
			this.uiText.text = difficultyName;
		}

		public void OnClicked()
		{
			if (this.onOptionClicked != null)
			{
				this.onOptionClicked(this.index);
			}
		}

		public void StartShowAnimation(float delay)
		{
			if (this.canvasGroup != null)
			{
				this.canvasGroup.alpha = 0f;
				if (delay == 0f)
				{
					this.StartShowAnimation();
				}
				else
				{
					base.StartCoroutine(this.WaitThenStartShowAnimation(delay));
				}
			}
		}

		private IEnumerator WaitThenStartShowAnimation(float delay)
		{
			OptionButton._WaitThenStartShowAnimation_c__Iterator0 _WaitThenStartShowAnimation_c__Iterator = new OptionButton._WaitThenStartShowAnimation_c__Iterator0();
			_WaitThenStartShowAnimation_c__Iterator.delay = delay;
			_WaitThenStartShowAnimation_c__Iterator._this = this;
			return _WaitThenStartShowAnimation_c__Iterator;
		}

		private void StartShowAnimation()
		{
			Tween.CanvasGroupAlpha(this.canvasGroup, Tween.TweenStyle.EaseOut, 0f, 1f, 400f);
		}
	}
}
