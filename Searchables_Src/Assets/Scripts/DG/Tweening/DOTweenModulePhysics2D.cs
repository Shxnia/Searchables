using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModulePhysics2D
	{
		private sealed class _DOMove_c__AnonStorey0
		{
			internal Rigidbody2D target;

			internal Vector2 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOMoveX_c__AnonStorey1
		{
			internal Rigidbody2D target;

			internal Vector2 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOMoveY_c__AnonStorey2
		{
			internal Rigidbody2D target;

			internal Vector2 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DORotate_c__AnonStorey3
		{
			internal Rigidbody2D target;

			internal float __m__0()
			{
				return this.target.rotation;
			}
		}

		private sealed class _DOJump_c__AnonStorey4
		{
			internal Rigidbody2D target;

			internal float startPosY;

			internal bool offsetYSet;

			internal Sequence s;

			internal Vector2 endValue;

			internal float offsetY;

			internal Tween yTween;

			internal Vector2 __m__0()
			{
				return this.target.position;
			}

			internal void __m__1(Vector2 x)
			{
				this.target.position = x;
			}

			internal void __m__2()
			{
				this.startPosY = this.target.position.y;
			}

			internal Vector2 __m__3()
			{
				return this.target.position;
			}

			internal void __m__4(Vector2 x)
			{
				this.target.position = x;
			}

			internal void __m__5()
			{
				if (!this.offsetYSet)
				{
					this.offsetYSet = true;
					this.offsetY = ((!this.s.isRelative) ? (this.endValue.y - this.startPosY) : this.endValue.y);
				}
				Vector3 v = this.target.position;
				v.y += DOVirtual.EasedValue(0f, this.offsetY, this.yTween.ElapsedPercentage(true), Ease.OutQuad);
				this.target.MovePosition(v);
			}
		}

		public static Tweener DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DORotate(this Rigidbody2D target, float endValue, float duration)
		{
			return DOTween.To(() => target.rotation, new DOSetter<float>(target.MoveRotation), endValue, duration).SetTarget(target);
		}

		public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative<Tweener>().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(delegate
			{
				startPosY = target.position.y;
			});
			s.Append(DOTween.To(() => target.position, delegate(Vector2 x)
			{
				target.position = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = ((!s.isRelative) ? (endValue.y - startPosY) : endValue.y);
				}
				Vector3 v = target.position;
				v.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.MovePosition(v);
			});
			return s;
		}
	}
}
