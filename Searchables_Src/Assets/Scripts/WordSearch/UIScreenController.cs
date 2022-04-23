using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class UIScreenController : SingletonComponent<UIScreenController>
	{
		private sealed class _MoveScreen_c__AnonStorey0
		{
			internal int hide;

			internal UIScreenController _this;

			internal void __m__0()
			{
				this._this.Complete_Back(this.hide);
			}
		}

		private sealed class _ShowUIScreen_c__AnonStorey1
		{
			internal Action onTweenFinished;

			internal UIScreenController _this;

			internal void __m__0()
			{
				this._this.isAnimating = false;
				if (this.onTweenFinished != null)
				{
					this.onTweenFinished();
				}
			}
		}

		private sealed class _TransitionUIScreen_c__AnonStorey2
		{
			internal Action onTweenFinished;

			internal void __m__0(GameObject tweenedObject)
			{
				this.onTweenFinished();
			}
		}

		private sealed class _PopupClose_c__AnonStorey3
		{
			internal int index;

			internal UIScreenController _this;

			internal void __m__0()
			{
				this._this.PopupCloseComplete(this.index);
			}
		}

		[SerializeField]
		private float animationSpeed;

		[SerializeField]
		private List<UIScreen> uiScreens;

		[SerializeField]
		private List<GameObject> UiPopups;

		[SerializeField]
		private List<GameObject> UiPopups_Result;

		[SerializeField]
		private GameObject UiPopups_LevelUp;

		public const string MainScreenId = "main";

		public const string OptionScreenId = "option";

		public const string GameScreenId = "game";

		private UIScreen currentUIScreen;

		private bool isAnimating;

		public List<bool> pop_flag = new List<bool>();

		public Action<string, string, bool> OnSwitchingScreens;

		public float AnimationSpeed
		{
			get
			{
				return this.animationSpeed;
			}
		}

		public string CurrentScreenId
		{
			get
			{
				return (!(this.currentUIScreen == null)) ? this.currentUIScreen.id : string.Empty;
			}
		}

		private void Start()
		{
			for (int i = 0; i < this.UiPopups.Count; i++)
			{
				this.pop_flag.Add(false);
			}
			for (int j = 0; j < this.uiScreens.Count; j++)
			{
				this.uiScreens[j].Initialize();
				this.uiScreens[j].gameObject.SetActive(false);
			}
			this.uiScreens[0].gameObject.SetActive(true);
		}

		public void MoveScreen(int hide, int show)
		{
			if (show == 0)
			{
				SingletonComponent<EffectSound>.Instance.SystemSound(5);
			}
			this.uiScreens[hide].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
			this.uiScreens[hide].GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(delegate
			{
				this.Complete_Back(hide);
			});
			this.uiScreens[show].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			this.uiScreens[show].transform.GetComponent<CanvasGroup>().alpha = 0f;
			this.uiScreens[show].transform.gameObject.SetActive(true);
			this.uiScreens[show].transform.DOScale(Vector3.one, 0.5f).SetDelay(0.1f);
			this.uiScreens[show].transform.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetDelay(0.1f);
			this.currentUIScreen = this.uiScreens[show];
		}

		private void Complete_Back(int index)
		{
			this.uiScreens[index].gameObject.SetActive(false);
		}

		public void Show(string id, bool back = false, bool animate = true, bool overlay = false, Action onTweenFinished = null, string data = "")
		{
			if (this.isAnimating)
			{
				return;
			}
			UIScreen screenInfo = this.GetScreenInfo(id);
			if (screenInfo != null)
			{
				this.ShowUIScreen(screenInfo, animate, back, onTweenFinished, data);
				if (!overlay)
				{
					this.HideUIScreen(this.currentUIScreen, animate, back, null);
				}
				if (this.OnSwitchingScreens != null)
				{
					this.OnSwitchingScreens((!(this.currentUIScreen == null)) ? this.currentUIScreen.id : null, id, overlay);
				}
				this.currentUIScreen = screenInfo;
			}
		}

		public void HideOverlay(string id, Action onTweenFinished = null)
		{
			this.HideUIScreen(this.GetScreenInfo(id), true, false, onTweenFinished);
		}

		private void ShowUIScreen(UIScreen uiScreen, bool animate, bool back, Action onTweenFinished, string data)
		{
			if (uiScreen == null)
			{
				return;
			}
			uiScreen.OnShowing(data);
			float num = (!back) ? 1f : -1f;
			float fromX = uiScreen.RectT.rect.width * num;
			float toX = 0f;
			float worldFromX = Utilities.WorldWidth * num;
			float worldToX = 0f;
			this.isAnimating = animate;
			this.TransitionUIScreen(uiScreen, fromX, toX, worldFromX, worldToX, animate, delegate
			{
				this.isAnimating = false;
				if (onTweenFinished != null)
				{
					onTweenFinished();
				}
			});
		}

		private void HideUIScreen(UIScreen uiScreen, bool animate, bool back, Action onTweenFinished)
		{
			if (uiScreen == null)
			{
				return;
			}
			float num = (!back) ? -1f : 1f;
			float fromX = 0f;
			float toX = uiScreen.RectT.rect.width * num;
			float worldFromX = 0f;
			float worldToX = Utilities.WorldWidth * num;
			this.TransitionUIScreen(uiScreen, fromX, toX, worldFromX, worldToX, animate, onTweenFinished);
		}

		private void TransitionUIScreen(UIScreen uiScreen, float fromX, float toX, float worldFromX, float worldToX, bool animate, Action onTweenFinished)
		{
			uiScreen.RectT.anchoredPosition = new Vector2(fromX, uiScreen.RectT.anchoredPosition.y);
			if (animate)
			{
				Tween tween = Tween.PositionX(uiScreen.RectT, Tween.TweenStyle.EaseOut, fromX, toX, this.animationSpeed, false, Tween.LoopType.None);
				tween.SetUseRectTransform(true);
				if (onTweenFinished != null)
				{
					tween.SetFinishCallback(delegate(GameObject tweenedObject)
					{
						onTweenFinished();
					});
				}
			}
			else
			{
				uiScreen.RectT.anchoredPosition = new Vector2(toX, uiScreen.RectT.anchoredPosition.y);
			}
			for (int i = 0; i < uiScreen.worldObjects.Count; i++)
			{
				uiScreen.worldObjects[i].transform.position = new Vector3(worldFromX, uiScreen.worldObjects[i].transform.position.y, uiScreen.worldObjects[i].transform.position.z);
				if (animate)
				{
					Tween.PositionX(uiScreen.worldObjects[i].transform, Tween.TweenStyle.EaseOut, worldFromX, worldToX, this.animationSpeed, false, Tween.LoopType.None);
				}
				else
				{
					uiScreen.worldObjects[i].transform.position = new Vector3(worldToX, uiScreen.worldObjects[i].transform.position.y, uiScreen.worldObjects[i].transform.position.z);
				}
			}
		}

		private UIScreen GetScreenInfo(string id)
		{
			for (int i = 0; i < this.uiScreens.Count; i++)
			{
				if (id == this.uiScreens[i].id)
				{
					return this.uiScreens[i];
				}
			}
			UnityEngine.Debug.LogError("[UIScreenController] No UIScreen exists with the id " + id);
			return null;
		}

		public void PopupOpen(int index)
		{
			if (this.pop_flag[index])
			{
				return;
			}
			this.pop_flag[index] = true;
			if (index == 1)
			{
				if (SingletonComponent<WordSearchController>.Instance.IsSetting)
				{
					return;
				}
				SingletonComponent<WordSearchController>.Instance.IsSetting = true;
			}
			else if (index == 2)
			{
				if (SingletonComponent<WordSearchController>.Instance.IsPause)
				{
					return;
				}
				SingletonComponent<EffectSound_ing>.Instance.SoundPause();
				SingletonComponent<WordSearchController>.Instance.IsPause = true;
			}
			else if (index == 10)
			{
				if (SingletonComponent<WordSearchController>.Instance.IsPause)
				{
					return;
				}
				SingletonComponent<EffectSound_ing>.Instance.SoundPause();
				SingletonComponent<WordSearchController>.Instance.IsPause = true;
			}
			this.UiPopups[index].GetComponent<CanvasGroup>().alpha = 0f;
			this.UiPopups[index].SetActive(true);
			this.UiPopups[index].GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		}

		public void PopupClose(int index)
		{
			if (!this.pop_flag[index])
			{
				return;
			}
			if (index == 1)
			{
				if (!SingletonComponent<WordSearchController>.Instance.IsSetting)
				{
					return;
				}
			}
			else if (index == 2)
			{
				if (!SingletonComponent<WordSearchController>.Instance.IsPause)
				{
					return;
				}
			}
			else if (index == 10 && !SingletonComponent<WordSearchController>.Instance.IsPause)
			{
				return;
			}
			if (!this.UiPopups[index].gameObject.activeSelf)
			{
				return;
			}
			this.UiPopups[index].GetComponent<CanvasGroup>().DOFade(0f, 0.2f).OnComplete(delegate
			{
				this.PopupCloseComplete(index);
			});
		}

		private void PopupCloseComplete(int index)
		{
			this.pop_flag[index] = false;
			this.UiPopups[index].SetActive(false);
			if (index == 1)
			{
				SingletonComponent<WordSearchController>.Instance.IsSetting = false;
			}
			else if (index == 2)
			{
				SingletonComponent<WordSearchController>.Instance.IsPause = false;
				SingletonComponent<EffectSound_ing>.Instance.SoundUnPause();
			}
			else if (index == 10)
			{
				SingletonComponent<WordSearchController>.Instance.IsPause = false;
				SingletonComponent<EffectSound_ing>.Instance.SoundUnPause();
			}
		}

		public void PopupOpenChallenge(int index, int level, bool new_record)
		{
			GameObject gameObject = this.UiPopups[index].transform.Find("InBox").Find("ChallengeFace").gameObject;
			GameObject gameObject2 = this.UiPopups[index].transform.Find("InBox").Find("ChallengeFace_Sad").gameObject;
			this.UiPopups[index].transform.Find("InBox").Find("Title_Level").GetComponent<Text>().text = "Lv." + level;
			gameObject.SetActive(false);
			gameObject2.SetActive(false);
			if (new_record)
			{
				gameObject.SetActive(true);
				this.UiPopups[index].transform.Find("InBox").Find("Title_Gameover").GetComponent<Text>().text = "NEW RECORD!";
			}
			else
			{
				gameObject2.SetActive(true);
				this.UiPopups[index].transform.Find("InBox").Find("Title_Gameover").GetComponent<Text>().text = "GAME OVER!";
			}
			this.PopupOpen(index);
			SingletonComponent<WordSearchController>.Instance.GamePlayOpenReview();
		}

		public void BackKey_PopClose()
		{
			bool flag = true;
			if (this.pop_flag[4])
			{
				SingletonComponent<EffectSound>.Instance.SystemSound(3);
				this.PopupClose(4);
				return;
			}
			if (this.pop_flag[11])
			{
				SingletonComponent<EffectSound>.Instance.SystemSound(3);
				this.PopupClose(11);
				return;
			}
			for (int i = 0; i < this.pop_flag.Count; i++)
			{
				if (i < 5 || i >= 10)
				{
					if (this.pop_flag[i])
					{
						flag = false;
						SingletonComponent<EffectSound>.Instance.SystemSound(3);
						this.PopupClose(i);
						break;
					}
				}
			}
			if (SingletonComponent<UIScreenController>.Instance.CurrentScreenId == "game" && !SingletonComponent<WordSearchController>.Instance.IsPause)
			{
				SingletonComponent<EffectSound>.Instance.SystemSound(2);
				this.PopupOpen(2);
				return;
			}
			if (flag)
			{
				SingletonComponent<EffectSound>.Instance.SystemSound(2);
				this.PopupOpen(3);
			}
		}

		public GameObject returnOBJ(int index)
		{
			return this.uiScreens[index].gameObject;
		}

		public GameObject returnPopOBJ(int index)
		{
			return this.UiPopups[index].gameObject;
		}
	}
}
