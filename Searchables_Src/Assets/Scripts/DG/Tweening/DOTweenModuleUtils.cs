using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenModuleUtils
	{
		public static class Physics
		{
			public static void SetOrientationOnPath(PathOptions options, Tween t, Quaternion newRot, Transform trans)
			{
				if (options.isRigidbody)
				{
					((Rigidbody)t.target).rotation = newRot;
				}
				else
				{
					trans.rotation = newRot;
				}
			}

			public static bool HasRigidbody2D(Component target)
			{
				return target.GetComponent<Rigidbody2D>() != null;
			}

			public static bool HasRigidbody(Component target)
			{
				return target.GetComponent<Rigidbody>() != null;
			}

			public static TweenerCore<Vector3, Path, PathOptions> CreateDOTweenPathTween(MonoBehaviour target, bool tweenRigidbody, bool isLocal, Path path, float duration, PathMode pathMode)
			{
				Rigidbody rigidbody = (!tweenRigidbody) ? null : target.GetComponent<Rigidbody>();
				TweenerCore<Vector3, Path, PathOptions> result;
				if (tweenRigidbody && rigidbody != null)
				{
					result = ((!isLocal) ? rigidbody.DOPath(path, duration, pathMode) : rigidbody.DOLocalPath(path, duration, pathMode));
				}
				else
				{
					result = ((!isLocal) ? target.transform.DOPath(path, duration, pathMode) : target.transform.DOLocalPath(path, duration, pathMode));
				}
				return result;
			}
		}

		private static bool _initialized;

		private static Action<PathOptions, Tween, Quaternion, Transform> __f__mg_cache0;

		public static void Init()
		{
			if (DOTweenModuleUtils._initialized)
			{
				return;
			}
			DOTweenModuleUtils._initialized = true;
			if (DOTweenModuleUtils.__f__mg_cache0 == null)
			{
				DOTweenModuleUtils.__f__mg_cache0 = new Action<PathOptions, Tween, Quaternion, Transform>(DOTweenModuleUtils.Physics.SetOrientationOnPath);
			}
			DOTweenExternalCommand.SetOrientationOnPath += DOTweenModuleUtils.__f__mg_cache0;
		}
	}
}
