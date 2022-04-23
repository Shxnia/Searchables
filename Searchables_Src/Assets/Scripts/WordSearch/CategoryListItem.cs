using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class CategoryListItem : MonoBehaviour
	{
		private sealed class _WaitThenStartShowAnimation_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float animDelay;

			internal CategoryListItem _this;

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
					this._current = new WaitForSeconds(this.animDelay);
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

		[SerializeField]
		private RectTransform itemUI;

		[SerializeField]
		private CanvasGroup itemCanvasGroup;

		[SerializeField]
		private Text categoryNameText;

		[SerializeField]
		private Image categoryIconImage;

		private const float animDuration = 400f;

		private const float animStartY = -150f;

		private const float animStartScale = 0.8f;

		private int index;

		private Action<int> onButtonClicked;

		public void Setup(string categoryName, Sprite categoryIcon, int index, Action<int> onButtonClicked)
		{
			this.index = index;
			this.onButtonClicked = onButtonClicked;
			this.categoryNameText.text = categoryName;
			this.categoryIconImage.sprite = categoryIcon;
		}

		public void OnClicked()
		{
			if (this.onButtonClicked != null)
			{
				this.onButtonClicked(this.index);
			}
		}

		public void StartShowAnimation(float animDelay)
		{
			this.itemUI.anchoredPosition = new Vector2(0f, -150f);
			this.itemUI.localScale = new Vector3(0.8f, 0.8f, 1f);
			this.itemCanvasGroup.alpha = 0f;
			if (animDelay == 0f)
			{
				this.StartShowAnimation();
			}
			else
			{
				base.StartCoroutine(this.WaitThenStartShowAnimation(animDelay));
			}
		}

		private IEnumerator WaitThenStartShowAnimation(float animDelay)
		{
			CategoryListItem._WaitThenStartShowAnimation_c__Iterator0 _WaitThenStartShowAnimation_c__Iterator = new CategoryListItem._WaitThenStartShowAnimation_c__Iterator0();
			_WaitThenStartShowAnimation_c__Iterator.animDelay = animDelay;
			_WaitThenStartShowAnimation_c__Iterator._this = this;
			return _WaitThenStartShowAnimation_c__Iterator;
		}

		private void StartShowAnimation()
		{
			Tween.PositionY(this.itemUI, Tween.TweenStyle.EaseOut, -150f, 0f, 400f, false, Tween.LoopType.None).SetUseRectTransform(true);
			Tween.ScaleX(this.itemUI, Tween.TweenStyle.EaseOut, 0.8f, 1f, 400f, Tween.LoopType.None);
			Tween.ScaleY(this.itemUI, Tween.TweenStyle.EaseOut, 0.8f, 1f, 400f, Tween.LoopType.None);
			Tween.CanvasGroupAlpha(this.itemCanvasGroup, Tween.TweenStyle.EaseOut, 0f, 1f, 400f);
		}
	}
}
