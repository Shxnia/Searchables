using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
	public static class DOTweenModuleUI
	{
		public static class Utils
		{
			public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
			{
				Vector2 b = new Vector2(from.rect.width * 0.5f + from.rect.xMin, from.rect.height * 0.5f + from.rect.yMin);
				Vector2 vector = RectTransformUtility.WorldToScreenPoint(null, from.position);
				vector += b;
				Vector2 b2;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(to, vector, null, out b2);
				Vector2 b3 = new Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
				return to.anchoredPosition + b2 - b3;
			}
		}

		private sealed class _DOFade_c__AnonStorey0
		{
			internal CanvasGroup target;

			internal float __m__0()
			{
				return this.target.alpha;
			}

			internal void __m__1(float x)
			{
				this.target.alpha = x;
			}
		}

		private sealed class _DOColor_c__AnonStorey1
		{
			internal Graphic target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOFade_c__AnonStorey2
		{
			internal Graphic target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOColor_c__AnonStorey3
		{
			internal Image target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOFade_c__AnonStorey4
		{
			internal Image target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOFillAmount_c__AnonStorey5
		{
			internal Image target;

			internal float __m__0()
			{
				return this.target.fillAmount;
			}

			internal void __m__1(float x)
			{
				this.target.fillAmount = x;
			}
		}

		private sealed class _DOFlexibleSize_c__AnonStorey6
		{
			internal LayoutElement target;

			internal Vector2 __m__0()
			{
				return new Vector2(this.target.flexibleWidth, this.target.flexibleHeight);
			}

			internal void __m__1(Vector2 x)
			{
				this.target.flexibleWidth = x.x;
				this.target.flexibleHeight = x.y;
			}
		}

		private sealed class _DOMinSize_c__AnonStorey7
		{
			internal LayoutElement target;

			internal Vector2 __m__0()
			{
				return new Vector2(this.target.minWidth, this.target.minHeight);
			}

			internal void __m__1(Vector2 x)
			{
				this.target.minWidth = x.x;
				this.target.minHeight = x.y;
			}
		}

		private sealed class _DOPreferredSize_c__AnonStorey8
		{
			internal LayoutElement target;

			internal Vector2 __m__0()
			{
				return new Vector2(this.target.preferredWidth, this.target.preferredHeight);
			}

			internal void __m__1(Vector2 x)
			{
				this.target.preferredWidth = x.x;
				this.target.preferredHeight = x.y;
			}
		}

		private sealed class _DOColor_c__AnonStorey9
		{
			internal Outline target;

			internal Color __m__0()
			{
				return this.target.effectColor;
			}

			internal void __m__1(Color x)
			{
				this.target.effectColor = x;
			}
		}

		private sealed class _DOFade_c__AnonStoreyA
		{
			internal Outline target;

			internal Color __m__0()
			{
				return this.target.effectColor;
			}

			internal void __m__1(Color x)
			{
				this.target.effectColor = x;
			}
		}

		private sealed class _DOScale_c__AnonStoreyB
		{
			internal Outline target;

			internal Vector2 __m__0()
			{
				return this.target.effectDistance;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.effectDistance = x;
			}
		}

		private sealed class _DOAnchorPos_c__AnonStoreyC
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.anchoredPosition = x;
			}
		}

		private sealed class _DOAnchorPosX_c__AnonStoreyD
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.anchoredPosition = x;
			}
		}

		private sealed class _DOAnchorPosY_c__AnonStoreyE
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.anchoredPosition = x;
			}
		}

		private sealed class _DOAnchorPos3D_c__AnonStoreyF
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition3D;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition3D = x;
			}
		}

		private sealed class _DOAnchorPos3DX_c__AnonStorey10
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition3D;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition3D = x;
			}
		}

		private sealed class _DOAnchorPos3DY_c__AnonStorey11
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition3D;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition3D = x;
			}
		}

		private sealed class _DOAnchorPos3DZ_c__AnonStorey12
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition3D;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition3D = x;
			}
		}

		private sealed class _DOAnchorMax_c__AnonStorey13
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.anchorMax;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.anchorMax = x;
			}
		}

		private sealed class _DOAnchorMin_c__AnonStorey14
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.anchorMin;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.anchorMin = x;
			}
		}

		private sealed class _DOPivot_c__AnonStorey15
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.pivot;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.pivot = x;
			}
		}

		private sealed class _DOPivotX_c__AnonStorey16
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.pivot;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.pivot = x;
			}
		}

		private sealed class _DOPivotY_c__AnonStorey17
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.pivot;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.pivot = x;
			}
		}

		private sealed class _DOSizeDelta_c__AnonStorey18
		{
			internal RectTransform target;

			internal Vector2 __m__0()
			{
				return this.target.sizeDelta;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.sizeDelta = x;
			}
		}

		private sealed class _DOPunchAnchorPos_c__AnonStorey19
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition = x;
			}
		}

		private sealed class _DOShakeAnchorPos_c__AnonStorey1A
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition = x;
			}
		}

		private sealed class _DOShakeAnchorPos_c__AnonStorey1B
		{
			internal RectTransform target;

			internal Vector3 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.anchoredPosition = x;
			}
		}

		private sealed class _DOJumpAnchorPos_c__AnonStorey1C
		{
			internal RectTransform target;

			internal float startPosY;

			internal bool offsetYSet;

			internal Sequence s;

			internal Vector2 endValue;

			internal float offsetY;

			internal Vector2 __m__0()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.anchoredPosition = x;
			}

			internal void __m__2()
			{
				this.startPosY = this.target.anchoredPosition.y;
			}

			internal Vector2 __m__3()
			{
				return this.target.anchoredPosition;
			}

			internal void __m__4(Vector2 x)
			{
				this.target.anchoredPosition = x;
			}

			internal void __m__5()
			{
				if (!this.offsetYSet)
				{
					this.offsetYSet = true;
					this.offsetY = ((!this.s.isRelative) ? (this.endValue.y - this.startPosY) : this.endValue.y);
				}
				Vector2 anchoredPosition = this.target.anchoredPosition;
				anchoredPosition.y += DOVirtual.EasedValue(0f, this.offsetY, this.s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				this.target.anchoredPosition = anchoredPosition;
			}
		}

		private sealed class _DONormalizedPos_c__AnonStorey1D
		{
			internal ScrollRect target;

			internal Vector2 __m__0()
			{
				return new Vector2(this.target.horizontalNormalizedPosition, this.target.verticalNormalizedPosition);
			}

			internal void __m__1(Vector2 x)
			{
				this.target.horizontalNormalizedPosition = x.x;
				this.target.verticalNormalizedPosition = x.y;
			}
		}

		private sealed class _DOHorizontalNormalizedPos_c__AnonStorey1E
		{
			internal ScrollRect target;

			internal float __m__0()
			{
				return this.target.horizontalNormalizedPosition;
			}

			internal void __m__1(float x)
			{
				this.target.horizontalNormalizedPosition = x;
			}
		}

		private sealed class _DOVerticalNormalizedPos_c__AnonStorey1F
		{
			internal ScrollRect target;

			internal float __m__0()
			{
				return this.target.verticalNormalizedPosition;
			}

			internal void __m__1(float x)
			{
				this.target.verticalNormalizedPosition = x;
			}
		}

		private sealed class _DOValue_c__AnonStorey20
		{
			internal Slider target;

			internal float __m__0()
			{
				return this.target.value;
			}

			internal void __m__1(float x)
			{
				this.target.value = x;
			}
		}

		private sealed class _DOColor_c__AnonStorey21
		{
			internal Text target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOFade_c__AnonStorey22
		{
			internal Text target;

			internal Color __m__0()
			{
				return this.target.color;
			}

			internal void __m__1(Color x)
			{
				this.target.color = x;
			}
		}

		private sealed class _DOText_c__AnonStorey23
		{
			internal Text target;

			internal string __m__0()
			{
				return this.target.text;
			}

			internal void __m__1(string x)
			{
				this.target.text = x;
			}
		}

		private sealed class _DOBlendableColor_c__AnonStorey24
		{
			internal Color to;

			internal Graphic target;

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

		private sealed class _DOBlendableColor_c__AnonStorey25
		{
			internal Color to;

			internal Image target;

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

		private sealed class _DOBlendableColor_c__AnonStorey26
		{
			internal Color to;

			internal Text target;

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

		public static Tweener DOFade(this CanvasGroup target, float endValue, float duration)
		{
			return DOTween.To(() => target.alpha, delegate(float x)
			{
				target.alpha = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOColor(this Graphic target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Graphic target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOColor(this Image target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Image target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFillAmount(this Image target, float endValue, float duration)
		{
			if (endValue > 1f)
			{
				endValue = 1f;
			}
			else if (endValue < 0f)
			{
				endValue = 0f;
			}
			return DOTween.To(() => target.fillAmount, delegate(float x)
			{
				target.fillAmount = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Sequence DOGradientColor(this Image target, Gradient gradient, float duration)
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

		public static Tweener DOFlexibleSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.flexibleWidth, target.flexibleHeight), delegate(Vector2 x)
			{
				target.flexibleWidth = x.x;
				target.flexibleHeight = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMinSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.minWidth, target.minHeight), delegate(Vector2 x)
			{
				target.minWidth = x.x;
				target.minHeight = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOPreferredSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.preferredWidth, target.preferredHeight), delegate(Vector2 x)
			{
				target.preferredWidth = x.x;
				target.preferredHeight = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOColor(this Outline target, Color endValue, float duration)
		{
			return DOTween.To(() => target.effectColor, delegate(Color x)
			{
				target.effectColor = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Outline target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.effectColor, delegate(Color x)
			{
				target.effectColor = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOScale(this Outline target, Vector2 endValue, float duration)
		{
			return DOTween.To(() => target.effectDistance, delegate(Vector2 x)
			{
				target.effectDistance = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOAnchorPos(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPosX(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPosY(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPos3D(this RectTransform target, Vector3 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPos3DX(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPos3DY(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DOAnchorPos3DZ(this RectTransform target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchoredPosition3D, delegate(Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
		}

		public static Tweener DOAnchorMax(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchorMax, delegate(Vector2 x)
			{
				target.anchorMax = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOAnchorMin(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.anchorMin, delegate(Vector2 x)
			{
				target.anchorMin = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOPivot(this RectTransform target, Vector2 endValue, float duration)
		{
			return DOTween.To(() => target.pivot, delegate(Vector2 x)
			{
				target.pivot = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOPivotX(this RectTransform target, float endValue, float duration)
		{
			return DOTween.To(() => target.pivot, delegate(Vector2 x)
			{
				target.pivot = x;
			}, new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, false).SetTarget(target);
		}

		public static Tweener DOPivotY(this RectTransform target, float endValue, float duration)
		{
			return DOTween.To(() => target.pivot, delegate(Vector2 x)
			{
				target.pivot = x;
			}, new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, false).SetTarget(target);
		}

		public static Tweener DOSizeDelta(this RectTransform target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.sizeDelta, delegate(Vector2 x)
			{
				target.sizeDelta = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOPunchAnchorPos(this RectTransform target, Vector2 punch, float duration, int vibrato = 10, float elasticity = 1f, bool snapping = false)
		{
			return DOTween.Punch(() => target.anchoredPosition, delegate(Vector3 x)
			{
				target.anchoredPosition = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target).SetOptions(snapping);
		}

		public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, float strength = 100f, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true)
		{
			return DOTween.Shake(() => target.anchoredPosition, delegate(Vector3 x)
			{
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, true, fadeOut).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, Vector2 strength, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true)
		{
			return DOTween.Shake(() => target.anchoredPosition, delegate(Vector3 x)
			{
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static Sequence DOJumpAnchorPos(this RectTransform target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween t = DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative<Tweener>().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(delegate
			{
				startPosY = target.anchoredPosition.y;
			});
			s.Append(DOTween.To(() => target.anchoredPosition, delegate(Vector2 x)
			{
				target.anchoredPosition = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(t).SetTarget(target).SetEase(DOTween.defaultEaseType);
			s.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = ((!s.isRelative) ? (endValue.y - startPosY) : endValue.y);
				}
				Vector2 anchoredPosition = target.anchoredPosition;
				anchoredPosition.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.anchoredPosition = anchoredPosition;
			});
			return s;
		}

		public static Tweener DONormalizedPos(this ScrollRect target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => new Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition), delegate(Vector2 x)
			{
				target.horizontalNormalizedPosition = x.x;
				target.verticalNormalizedPosition = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOHorizontalNormalizedPos(this ScrollRect target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.horizontalNormalizedPosition, delegate(float x)
			{
				target.horizontalNormalizedPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOVerticalNormalizedPos(this ScrollRect target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.verticalNormalizedPosition, delegate(float x)
			{
				target.verticalNormalizedPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOValue(this Slider target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.value, delegate(float x)
			{
				target.value = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOColor(this Text target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOFade(this Text target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate(Color x)
			{
				target.color = x;
			}, endValue, duration).SetTarget(target);
		}

		public static Tweener DOText(this Text target, string endValue, float duration, bool richTextEnabled = true, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
		{
			return DOTween.To(() => target.text, delegate(string x)
			{
				target.text = x;
			}, endValue, duration).SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
		}

		public static Tweener DOBlendableColor(this Graphic target, Color endValue, float duration)
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

		public static Tweener DOBlendableColor(this Image target, Color endValue, float duration)
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

		public static Tweener DOBlendableColor(this Text target, Color endValue, float duration)
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
