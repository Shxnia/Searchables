using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModuleSprite
	{
		private sealed class _DOColor_c__AnonStorey0
		{
			internal SpriteRenderer target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOFade_c__AnonStorey1
		{
			internal SpriteRenderer target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOBlendableColor_c__AnonStorey2
		{
			internal Color to;

			internal SpriteRenderer target;

			internal Color __m__0()
			{
				return this.to;
			}

			internal void __m__1(Color x)
			{
				Color b = x - this.to;
				this.to = x;
				this.target.color += b;
			}
		}

		public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Sequence DOGradientColor(this SpriteRenderer target, Gradient gradient, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.color = gradientColorKey.color;
				}
				else
				{
					float duration2 = (i != num - 1) ? (duration * ((i != 0) ? (gradientColorKey.time - colorKeys[i - 1].time) : gradientColorKey.time)) : (duration - sequence.Duration(false));
					sequence.Append(target.DOColor(gradientColorKey.color, duration2).SetEase(Ease.Linear));
				}
			}
			return sequence;
		}

		public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate(Color x)
			{
				Color b = x - to;
				to = x;
				target.color += b;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget(target);
		}
	}
}
