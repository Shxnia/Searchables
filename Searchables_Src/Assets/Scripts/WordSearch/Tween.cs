using System;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class Tween : MonoBehaviour
	{
		public enum TweenType
		{
			PositionX,
			PositionY,
			PositionZ,
			ScaleX,
			ScaleY,
			ScaleZ,
			RotateX,
			RotateY,
			RotateZ,
			Rotation,
			RotationPoint,
			ColourGraphic,
			ColourMaterial,
			PivotX,
			PivotY,
			RectTransformWidth,
			RectTransformHeight,
			CanvaGroup
		}

		public enum TweenStyle
		{
			Linear,
			EaseIn,
			EaseOut
		}

		public enum LoopType
		{
			None,
			Reset,
			Reverse
		}

		public delegate void OnTweenFinished(GameObject tweenedObject);

		private Tween.TweenType tweenType;

		private Tween.TweenStyle tweenStyle;

		private float duration;

		private double startTime;

		private double endTime;

		private Tween.LoopType loopType;

		private bool useRectTransform;

		private Tween.OnTweenFinished finishCallback;

		private bool isDestroyed;

		private AnimationCurve animationCurve;

		private float fromValue;

		private float toValue;

		private bool useLocal;

		private Vector3 point;

		private Transform pointT;

		private Vector3 axis;

		private float angleSoFar;

		private Vector3 fromPoint;

		private Vector3 toPoint;

		private Color fromColour;

		private Color toColour;

		public Vector3 Point
		{
			get
			{
				return this.point;
			}
			set
			{
				this.point = value;
			}
		}

		private void Start()
		{
			this.SetTimes();
		}

		private void Update()
		{
			if (Utilities.SystemTimeInMilliseconds >= this.endTime)
			{
				Tween.LoopType loopType = this.loopType;
				if (loopType != Tween.LoopType.None)
				{
					if (loopType != Tween.LoopType.Reset)
					{
						if (loopType == Tween.LoopType.Reverse)
						{
							this.SetTimes();
							this.SetToValue();
							this.Reverse();
						}
					}
					else
					{
						this.SetTimes();
						this.Reset();
					}
				}
				else
				{
					this.SetToValue();
					this.DestroyTween();
				}
				if (this.finishCallback != null)
				{
					this.finishCallback(base.gameObject);
				}
			}
			else
			{
				switch (this.tweenType)
				{
				case Tween.TweenType.PositionX:
				case Tween.TweenType.PositionY:
				case Tween.TweenType.PositionZ:
					this.UpdatePosition(Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT()));
					break;
				case Tween.TweenType.ScaleX:
				case Tween.TweenType.ScaleY:
				case Tween.TweenType.ScaleZ:
					this.UpdateScale(Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT()));
					break;
				case Tween.TweenType.RotateX:
				case Tween.TweenType.RotateY:
				case Tween.TweenType.RotateZ:
					this.UpdateRotation(Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT()));
					break;
				case Tween.TweenType.Rotation:
					this.UpdateRotation();
					break;
				case Tween.TweenType.RotationPoint:
					this.UpdateRotationPoint(Vector3.Lerp(this.fromPoint, this.toPoint, this.GetLerpT()));
					break;
				case Tween.TweenType.ColourGraphic:
				case Tween.TweenType.ColourMaterial:
					this.UpdateColour(Color.Lerp(this.fromColour, this.toColour, this.GetLerpT()));
					break;
				case Tween.TweenType.PivotX:
				case Tween.TweenType.PivotY:
					this.UpdatePivot(Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT()));
					break;
				case Tween.TweenType.RectTransformWidth:
				case Tween.TweenType.RectTransformHeight:
					this.UpdateRectSize(Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT()));
					break;
				case Tween.TweenType.CanvaGroup:
					this.UpdateCanvasGroup(Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT()));
					break;
				}
			}
		}

		public static Tween GetTween(GameObject obj, Tween.TweenType tweenType)
		{
			if (obj == null)
			{
				return null;
			}
			Tween[] components = obj.GetComponents<Tween>();
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].tweenType == tweenType)
				{
					return components[i].isDestroyed ? null : components[i];
				}
			}
			return null;
		}

		public static void RemoveTween(GameObject obj, Tween.TweenType tweenType)
		{
			Tween tween = Tween.GetTween(obj, tweenType);
			if (tween != null)
			{
				tween.DestroyTween();
			}
		}

		public static void RemoveAllTweens(GameObject obj)
		{
			Tween[] components = obj.GetComponents<Tween>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].DestroyTween();
			}
		}

		public static Tween PositionX(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, bool transformLocal = false, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.PositionX, tweenStyle, fromValue, toValue, duration, transformLocal, loopType);
		}

		public static Tween PositionY(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, bool transformLocal = false, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.PositionY, tweenStyle, fromValue, toValue, duration, transformLocal, loopType);
		}

		public static Tween PositionZ(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, bool transformLocal = false, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.PositionZ, tweenStyle, fromValue, toValue, duration, transformLocal, loopType);
		}

		public static Tween ScaleX(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.ScaleX, tweenStyle, fromValue, toValue, duration, true, loopType);
		}

		public static Tween ScaleY(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.ScaleY, tweenStyle, fromValue, toValue, duration, true, loopType);
		}

		public static Tween ScaleZ(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.ScaleZ, tweenStyle, fromValue, toValue, duration, true, loopType);
		}

		public static Tween RotateX(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.RotateX, tweenStyle, fromValue, toValue, duration, true, loopType);
		}

		public static Tween RotateY(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.RotateY, tweenStyle, fromValue, toValue, duration, false, loopType);
		}

		public static Tween RotateZ(Transform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.RotateZ, tweenStyle, fromValue, toValue, duration, false, loopType);
		}

		public static Tween RotateAround(Transform transform, Tween.TweenStyle tweenStyle, Vector3 point, Vector3 axis, float angle, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateRotationTween(transform.gameObject, Tween.TweenType.Rotation, tweenStyle, point, axis, angle, duration, loopType);
		}

		public static Tween RotateAround(Transform transform, Tween.TweenStyle tweenStyle, Transform point, Vector3 axis, float angle, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateRotationTween(transform.gameObject, Tween.TweenType.Rotation, tweenStyle, point, axis, angle, duration, loopType);
		}

		public static Tween Colour(Graphic uiGraphic, Tween.TweenStyle tweenStyle, Color fromValue, Color toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateColourTween(uiGraphic.gameObject, Tween.TweenType.ColourGraphic, tweenStyle, fromValue, toValue, duration, loopType);
		}

		public static Tween Colour(Renderer renderer, Tween.TweenStyle tweenStyle, Color fromValue, Color toValue, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			return Tween.CreateColourTween(renderer.gameObject, Tween.TweenType.ColourMaterial, tweenStyle, fromValue, toValue, duration, loopType);
		}

		public static Tween PivotX(RectTransform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.PivotX, tweenStyle, fromValue, toValue, duration, false, Tween.LoopType.None);
		}

		public static Tween PivotY(RectTransform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.PivotY, tweenStyle, fromValue, toValue, duration, false, Tween.LoopType.None);
		}

		public static Tween RectTransformWidth(RectTransform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.RectTransformWidth, tweenStyle, fromValue, toValue, duration, false, Tween.LoopType.None);
		}

		public static Tween RectTransformHeight(RectTransform transform, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration)
		{
			return Tween.CreateTween(transform.gameObject, Tween.TweenType.RectTransformHeight, tweenStyle, fromValue, toValue, duration, false, Tween.LoopType.None);
		}

		public static Tween CanvasGroupAlpha(CanvasGroup canvasGroup, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration)
		{
			return Tween.CreateTween(canvasGroup.gameObject, Tween.TweenType.CanvaGroup, tweenStyle, fromValue, toValue, duration, false, Tween.LoopType.None);
		}

		public void SetFinishCallback(Tween.OnTweenFinished finishCallback)
		{
			this.finishCallback = finishCallback;
		}

		public void SetUseRectTransform(bool useRectTransform)
		{
			this.useRectTransform = useRectTransform;
		}

		public void SetAnimationCurve(AnimationCurve animationCurve)
		{
			this.animationCurve = animationCurve;
		}

		public Tween TweenRotationPoint(Tween.TweenStyle tweenStyle, Vector3 fromPoint, Vector3 toPoint, float duration, Tween.LoopType loopType = Tween.LoopType.None)
		{
			if (this.tweenType != Tween.TweenType.Rotation)
			{
				UnityEngine.Debug.LogWarning("Cannot set a TweenType.RotationPoint on a Tween that is not a TweenType.Rotation.");
				return null;
			}
			Tween tween = Tween.GetTween(base.gameObject, Tween.TweenType.RotationPoint);
			if (tween == null)
			{
				tween = base.gameObject.AddComponent<Tween>();
			}
			tween.tweenType = Tween.TweenType.RotationPoint;
			tween.tweenStyle = tweenStyle;
			tween.fromPoint = fromPoint;
			tween.toPoint = toPoint;
			tween.duration = duration;
			tween.loopType = loopType;
			return tween;
		}

		public void DestroyTween()
		{
			UnityEngine.Object.Destroy(this);
			this.isDestroyed = true;
		}

		private static Tween CreateTween(GameObject obj, Tween.TweenType tweenType, Tween.TweenStyle tweenStyle, float fromValue, float toValue, float duration, bool transformLocal, Tween.LoopType loopType)
		{
			Tween tween = Tween.GetTween(obj, tweenType);
			if (tween == null)
			{
				tween = obj.AddComponent<Tween>();
			}
			tween.tweenType = tweenType;
			tween.tweenStyle = tweenStyle;
			tween.fromValue = fromValue;
			tween.toValue = toValue;
			tween.duration = duration;
			tween.useLocal = transformLocal;
			tween.loopType = loopType;
			return tween;
		}

		private static Tween CreateRotationTween(GameObject obj, Tween.TweenType tweenType, Tween.TweenStyle tweenStyle, Vector3 point, Vector3 axis, float angle, float duration, Tween.LoopType loopType)
		{
			Tween tween = Tween.GetTween(obj, tweenType);
			if (tween == null)
			{
				tween = obj.AddComponent<Tween>();
			}
			tween.angleSoFar = 0f;
			tween.tweenType = tweenType;
			tween.tweenStyle = tweenStyle;
			tween.point = point;
			tween.pointT = null;
			tween.axis = axis;
			tween.fromValue = 0f;
			tween.toValue = angle;
			tween.duration = duration;
			tween.loopType = loopType;
			return tween;
		}

		private static Tween CreateRotationTween(GameObject obj, Tween.TweenType tweenType, Tween.TweenStyle tweenStyle, Transform point, Vector3 axis, float angle, float duration, Tween.LoopType loopType)
		{
			Tween tween = Tween.GetTween(obj, tweenType);
			if (tween == null)
			{
				tween = obj.AddComponent<Tween>();
			}
			tween.angleSoFar = 0f;
			tween.tweenType = tweenType;
			tween.tweenStyle = tweenStyle;
			tween.pointT = point;
			tween.axis = axis;
			tween.fromValue = 0f;
			tween.toValue = angle;
			tween.duration = duration;
			tween.loopType = loopType;
			return tween;
		}

		private static Tween CreateColourTween(GameObject obj, Tween.TweenType tweenType, Tween.TweenStyle tweenStyle, Color fromValue, Color toValue, float duration, Tween.LoopType loopType)
		{
			Tween tween = Tween.GetTween(obj, tweenType);
			if (tween == null)
			{
				tween = obj.AddComponent<Tween>();
			}
			tween.tweenType = tweenType;
			tween.tweenStyle = tweenStyle;
			tween.fromColour = fromValue;
			tween.toColour = toValue;
			tween.duration = duration;
			tween.loopType = loopType;
			return tween;
		}

		private void SetTimes()
		{
			this.startTime = Utilities.SystemTimeInMilliseconds;
			this.endTime = this.startTime + (double)this.duration;
		}

		private void Reset()
		{
			switch (this.tweenType)
			{
			case Tween.TweenType.PositionX:
			case Tween.TweenType.PositionY:
			case Tween.TweenType.PositionZ:
				this.UpdatePosition(this.fromValue);
				break;
			case Tween.TweenType.ScaleX:
			case Tween.TweenType.ScaleY:
			case Tween.TweenType.ScaleZ:
				this.UpdateScale(this.fromValue);
				break;
			case Tween.TweenType.RotateX:
			case Tween.TweenType.RotateY:
			case Tween.TweenType.RotateZ:
				this.UpdateScale(this.fromValue);
				break;
			case Tween.TweenType.Rotation:
				base.transform.RotateAround((!(this.pointT == null)) ? this.pointT.position : this.point, this.axis, -this.toValue);
				this.angleSoFar = 0f;
				break;
			case Tween.TweenType.RotationPoint:
				this.UpdateRotationPoint(this.fromPoint);
				break;
			case Tween.TweenType.ColourGraphic:
			case Tween.TweenType.ColourMaterial:
				this.UpdateColour(this.fromColour);
				break;
			case Tween.TweenType.PivotX:
			case Tween.TweenType.PivotY:
				this.UpdatePivot(this.fromValue);
				break;
			case Tween.TweenType.RectTransformWidth:
			case Tween.TweenType.RectTransformHeight:
				this.UpdateRectSize(this.fromValue);
				break;
			case Tween.TweenType.CanvaGroup:
				this.UpdateCanvasGroup(this.fromValue);
				break;
			}
		}

		private void SetToValue()
		{
			switch (this.tweenType)
			{
			case Tween.TweenType.PositionX:
			case Tween.TweenType.PositionY:
			case Tween.TweenType.PositionZ:
				this.UpdatePosition(this.toValue);
				break;
			case Tween.TweenType.ScaleX:
			case Tween.TweenType.ScaleY:
			case Tween.TweenType.ScaleZ:
				this.UpdateScale(this.toValue);
				break;
			case Tween.TweenType.RotateX:
			case Tween.TweenType.RotateY:
			case Tween.TweenType.RotateZ:
				this.UpdateRotation(this.toValue);
				break;
			case Tween.TweenType.Rotation:
				base.transform.RotateAround((!(this.pointT == null)) ? this.pointT.position : this.point, this.axis, this.toValue - this.angleSoFar);
				this.angleSoFar = 0f;
				break;
			case Tween.TweenType.RotationPoint:
				this.UpdateRotationPoint(this.toPoint);
				break;
			case Tween.TweenType.ColourGraphic:
			case Tween.TweenType.ColourMaterial:
				this.UpdateColour(this.toColour);
				break;
			case Tween.TweenType.PivotX:
			case Tween.TweenType.PivotY:
				this.UpdatePivot(this.toValue);
				break;
			case Tween.TweenType.RectTransformWidth:
			case Tween.TweenType.RectTransformHeight:
				this.UpdateRectSize(this.toValue);
				break;
			case Tween.TweenType.CanvaGroup:
				this.UpdateCanvasGroup(this.toValue);
				break;
			}
		}

		private void Reverse()
		{
			switch (this.tweenType)
			{
			case Tween.TweenType.PositionX:
			case Tween.TweenType.PositionY:
			case Tween.TweenType.PositionZ:
			case Tween.TweenType.ScaleX:
			case Tween.TweenType.ScaleY:
			case Tween.TweenType.ScaleZ:
			case Tween.TweenType.RotateX:
			case Tween.TweenType.RotateY:
			case Tween.TweenType.RotateZ:
			case Tween.TweenType.Rotation:
			{
				float num = this.fromValue;
				this.fromValue = this.toValue;
				this.toValue = num;
				break;
			}
			case Tween.TweenType.RotationPoint:
			{
				Vector3 vector = this.fromPoint;
				this.fromPoint = this.toPoint;
				this.toPoint = vector;
				break;
			}
			case Tween.TweenType.ColourGraphic:
			case Tween.TweenType.ColourMaterial:
			{
				Color color = this.fromColour;
				this.fromColour = this.toColour;
				this.toColour = color;
				break;
			}
			}
		}

		private void UpdatePosition(float pos)
		{
			Tween.TweenType tweenType = this.tweenType;
			if (tweenType != Tween.TweenType.PositionX)
			{
				if (tweenType != Tween.TweenType.PositionY)
				{
					if (tweenType == Tween.TweenType.PositionZ)
					{
						if (this.useLocal)
						{
							base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, pos);
						}
						else
						{
							base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, pos);
						}
					}
				}
				else if (this.useLocal)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, pos, base.transform.localPosition.z);
				}
				else if (this.useRectTransform)
				{
					(base.transform as RectTransform).anchoredPosition = new Vector2((base.transform as RectTransform).anchoredPosition.x, pos);
				}
				else
				{
					base.transform.position = new Vector3(base.transform.position.x, pos, base.transform.position.z);
				}
			}
			else if (this.useLocal)
			{
				base.transform.localPosition = new Vector3(pos, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			else if (this.useRectTransform)
			{
				(base.transform as RectTransform).anchoredPosition = new Vector2(pos, (base.transform as RectTransform).anchoredPosition.y);
			}
			else
			{
				base.transform.position = new Vector3(pos, base.transform.position.y, base.transform.position.z);
			}
		}

		private void UpdateScale(float scale)
		{
			Tween.TweenType tweenType = this.tweenType;
			if (tweenType != Tween.TweenType.ScaleX)
			{
				if (tweenType != Tween.TweenType.ScaleY)
				{
					if (tweenType == Tween.TweenType.ScaleZ)
					{
						base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, scale);
					}
				}
				else
				{
					base.transform.localScale = new Vector3(base.transform.localScale.x, scale, base.transform.localScale.z);
				}
			}
			else
			{
				base.transform.localScale = new Vector3(scale, base.transform.localScale.y, base.transform.localScale.z);
			}
		}

		private void UpdateRotation(float angle)
		{
			Tween.TweenType tweenType = this.tweenType;
			if (tweenType != Tween.TweenType.RotateX)
			{
				if (tweenType != Tween.TweenType.RotateY)
				{
					if (tweenType == Tween.TweenType.RotateZ)
					{
						base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, angle);
					}
				}
				else
				{
					base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, angle, base.transform.eulerAngles.z);
				}
			}
			else
			{
				base.transform.eulerAngles = new Vector3(angle, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
			}
		}

		private void UpdateRotation()
		{
			float num = Mathf.Lerp(this.fromValue, this.toValue, this.GetLerpT());
			float angle = this.angleSoFar - num;
			base.transform.RotateAround((!(this.pointT == null)) ? this.pointT.position : this.point, this.axis, angle);
			this.angleSoFar = num;
		}

		private void UpdateRotationPoint(Vector3 point)
		{
			Tween tween = Tween.GetTween(base.gameObject, Tween.TweenType.Rotation);
			if (tween == null)
			{
				this.DestroyTween();
			}
			else
			{
				tween.Point = point;
			}
		}

		private void UpdateColour(Color colour)
		{
			Tween.TweenType tweenType = this.tweenType;
			if (tweenType != Tween.TweenType.ColourGraphic)
			{
				if (tweenType == Tween.TweenType.ColourMaterial)
				{
					base.gameObject.GetComponent<Renderer>().material.color = colour;
				}
			}
			else
			{
				base.gameObject.GetComponent<Graphic>().color = colour;
			}
		}

		private void UpdatePivot(float value)
		{
			Tween.TweenType tweenType = this.tweenType;
			if (tweenType != Tween.TweenType.PivotX)
			{
				if (tweenType == Tween.TweenType.PivotY)
				{
					(base.transform as RectTransform).pivot = new Vector2((base.transform as RectTransform).pivot.x, value);
				}
			}
			else
			{
				(base.transform as RectTransform).pivot = new Vector2(value, (base.transform as RectTransform).pivot.y);
			}
		}

		private void UpdateRectSize(float value)
		{
			Tween.TweenType tweenType = this.tweenType;
			if (tweenType != Tween.TweenType.RectTransformWidth)
			{
				if (tweenType == Tween.TweenType.RectTransformHeight)
				{
					(base.transform as RectTransform).sizeDelta = new Vector2((base.transform as RectTransform).sizeDelta.x, value);
				}
			}
			else
			{
				(base.transform as RectTransform).sizeDelta = new Vector2(value, (base.transform as RectTransform).sizeDelta.y);
			}
		}

		public void UpdateCanvasGroup(float value)
		{
			base.gameObject.GetComponent<CanvasGroup>().alpha = value;
		}

		private float GetLerpT()
		{
			float num = (float)(Utilities.SystemTimeInMilliseconds - this.startTime) / this.duration;
			if (this.animationCurve == null)
			{
				Tween.TweenStyle tweenStyle = this.tweenStyle;
				if (tweenStyle != Tween.TweenStyle.EaseIn)
				{
					if (tweenStyle == Tween.TweenStyle.EaseOut)
					{
						num = 1f - (1f - num) * (1f - num) * (1f - num);
					}
				}
				else
				{
					num = num * num * num;
				}
			}
			else
			{
				num = this.animationCurve.Evaluate(num);
			}
			return num;
		}
	}
}
