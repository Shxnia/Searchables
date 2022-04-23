using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public class Popup : MonoBehaviour
	{
		[SerializeField]
		protected int animationDuration = 300;

		[SerializeField]
		protected Transform uiContainer;

		[SerializeField]
		protected CanvasGroup canvasGroup;

		private bool _IsDisplayed_k__BackingField;

		public bool IsDisplayed
		{
			get;
			set;
		}

		private void Start()
		{
			this.SetDisplay(false, false, true);
		}

		public void Show(bool animate = true)
		{
			this.SetDisplay(true, animate, false);
		}

		public void Hide(bool animate = true)
		{
			this.SetDisplay(false, animate, false);
		}

		protected void SetDisplay(bool display, bool animate, bool force)
		{
			if (!force && this.IsDisplayed == display)
			{
				return;
			}
			this.IsDisplayed = display;
			this.canvasGroup.interactable = display;
			this.canvasGroup.blocksRaycasts = display;
			float num = (float)((!display) ? 1 : 0);
			float num2 = (float)((!display) ? 0 : 1);
			float num3 = (!display) ? 1f : 0f;
			float num4 = (!display) ? 0f : 1f;
			if (animate)
			{
				this.uiContainer.localScale = new Vector3(num, num, 1f);
				this.canvasGroup.alpha = num3;
				Tween.ScaleY(this.uiContainer, Tween.TweenStyle.EaseOut, num, num2, (float)this.animationDuration, Tween.LoopType.None);
				Tween.ScaleX(this.uiContainer, Tween.TweenStyle.EaseOut, num, num2, (float)this.animationDuration, Tween.LoopType.None);
				Tween.CanvasGroupAlpha(this.canvasGroup, Tween.TweenStyle.EaseOut, num3, num4, (float)this.animationDuration);
			}
			else
			{
				this.uiContainer.localScale = new Vector3(num2, num2, 1f);
				this.canvasGroup.alpha = num4;
			}
		}
	}
}
