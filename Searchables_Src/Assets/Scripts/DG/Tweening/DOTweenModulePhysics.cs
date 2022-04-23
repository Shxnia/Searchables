using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModulePhysics
	{
		private sealed class _DOMove_c__AnonStorey0
		{
			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOMoveX_c__AnonStorey1
		{
			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOMoveY_c__AnonStorey2
		{
			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOMoveZ_c__AnonStorey3
		{
			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DORotate_c__AnonStorey4
		{
			internal Rigidbody target;

			internal Quaternion __m__0()
			{
				return this.target.rotation;
			}
		}

		private sealed class _DOLookAt_c__AnonStorey5
		{
			internal Rigidbody target;

			internal Quaternion __m__0()
			{
				return this.target.rotation;
			}
		}

		private sealed class _DOJump_c__AnonStorey6
		{
			internal Rigidbody target;

			internal float startPosY;

			internal bool offsetYSet;

			internal Sequence s;

			internal Vector3 endValue;

			internal float offsetY;

			internal Tween yTween;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}

			internal void __m__1()
			{
				this.startPosY = this.target.position.y;
			}

			internal Vector3 __m__2()
			{
				return this.target.position;
			}

			internal Vector3 __m__3()
			{
				return this.target.position;
			}

			internal void __m__4()
			{
				if (!this.offsetYSet)
				{
					this.offsetYSet = true;
					this.offsetY = ((!this.s.isRelative) ? (this.endValue.y - this.startPosY) : this.endValue.y);
				}
				Vector3 position = this.target.position;
				position.y += DOVirtual.EasedValue(0f, this.offsetY, this.yTween.ElapsedPercentage(true), Ease.OutQuad);
				this.target.MovePosition(position);
			}
		}

		private sealed class _DOPath_c__AnonStorey7
		{
			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOLocalPath_c__AnonStorey8
		{
			internal Transform trans;

			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.trans.localPosition;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.MovePosition((!(this.trans.parent == null)) ? this.trans.parent.TransformPoint(x) : x);
			}
		}

		private sealed class _DOPath_c__AnonStorey9
		{
			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.target.position;
			}
		}

		private sealed class _DOLocalPath_c__AnonStoreyA
		{
			internal Transform trans;

			internal Rigidbody target;

			internal Vector3 __m__0()
			{
				return this.trans.localPosition;
			}

			internal void __m__1(Vector3 x)
			{
				this.target.MovePosition((!(this.trans.parent == null)) ? this.trans.parent.TransformPoint(x) : x);
			}
		}

		public static Tweener DOMove(this Rigidbody target, Vector3 endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static Tweener DOMoveX(this Rigidbody target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget(target);
		}

		public static Tweener DOMoveY(this Rigidbody target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
		}

		public static Tweener DOMoveZ(this Rigidbody target, float endValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget(target);
		}

		public static Tweener DORotate(this Rigidbody target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.rotation, new DOSetter<Quaternion>(target.MoveRotation), endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		public static Tweener DOLookAt(this Rigidbody target, Vector3 towards, float duration, AxisConstraint axisConstraint = AxisConstraint.None, Vector3? up = null)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = DOTween.To(() => target.rotation, new DOSetter<Quaternion>(target.MoveRotation), towards, duration).SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetLookAt);
			tweenerCore.plugOptions.axisConstraint = axisConstraint;
			tweenerCore.plugOptions.up = (up.HasValue ? up.Value : Vector3.up);
			return tweenerCore;
		}

		public static Sequence DOJump(this Rigidbody target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			Tween yTween = DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative<Tweener>().SetLoops(numJumps * 2, LoopType.Yoyo).OnStart(delegate
			{
				startPosY = target.position.y;
			});
			s.Append(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)).Join(yTween).SetTarget(target).SetEase(DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = ((!s.isRelative) ? (endValue.y - startPosY) : endValue.y);
				}
				Vector3 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(true), Ease.OutQuad);
				target.MovePosition(position);
			});
			return s;
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Rigidbody target, Vector3[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
			tweenerCore.plugOptions.isRigidbody = true;
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Rigidbody target, Vector3[] path, float duration, PathType pathType = PathType.Linear, PathMode pathMode = PathMode.Full3D, int resolution = 10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			Transform trans = target.transform;
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => trans.localPosition, delegate(Vector3 x)
			{
				target.MovePosition((!(trans.parent == null)) ? trans.parent.TransformPoint(x) : x);
			}, new Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(UpdateType.Fixed);
			tweenerCore.plugOptions.isRigidbody = true;
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		internal static TweenerCore<Vector3, Path, PathOptions> DOPath(this Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), path, duration).SetTarget(target);
			tweenerCore.plugOptions.isRigidbody = true;
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		internal static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Rigidbody target, Path path, float duration, PathMode pathMode = PathMode.Full3D)
		{
			Transform trans = target.transform;
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => trans.localPosition, delegate(Vector3 x)
			{
				target.MovePosition((!(trans.parent == null)) ? trans.parent.TransformPoint(x) : x);
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.isRigidbody = true;
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}
	}
}
