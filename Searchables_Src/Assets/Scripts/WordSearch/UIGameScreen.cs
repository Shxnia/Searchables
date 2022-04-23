using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public class UIGameScreen : UIScreen
	{
		private sealed class _LevelUp_Challenge_Action_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal UIGameScreen _this;

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

			public _LevelUp_Challenge_Action_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					SingletonComponent<WordSearchController>.Instance.IsPause = true;
					SingletonComponent<EffectSound>.Instance.GameSound(6);
					this._this.popup_levelup.SetActive(false);
					this._this.popup_levelup.GetComponent<CanvasGroup>().alpha = 0f;
					this._this.popup_levelup.SetActive(true);
					this._this.popup_levelup.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
					this._current = new WaitForSeconds(1.5f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.popup_levelup.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
					SingletonComponent<WordSearchController>.Instance.Challenge_Next_Game();
					this._current = new WaitForSeconds(0.5f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this.popup_levelup.SetActive(false);
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

		private sealed class _LevelUp_Quiz_Action_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal UIGameScreen _this;

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

			public _LevelUp_Quiz_Action_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					SingletonComponent<WordSearchController>.Instance.IsPause = true;
					SingletonComponent<EffectSound>.Instance.GameSound(6);
					this._this.popup_levelup.SetActive(false);
					this._this.popup_levelup.GetComponent<CanvasGroup>().alpha = 0f;
					this._this.popup_levelup.SetActive(true);
					this._this.popup_levelup.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
					this._current = new WaitForSeconds(1.5f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.popup_levelup.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
					SingletonComponent<WordSearchController>.Instance.Quiz_Next_Game();
					this._current = new WaitForSeconds(0.5f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this.popup_levelup.SetActive(false);
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

		private sealed class _Ready_Action_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Transform _text_ready___0;

			internal Transform _text_go___0;

			internal UIGameScreen _this;

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

			public _Ready_Action_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					SingletonComponent<WordSearchController>.Instance.IsPause = true;
					this._text_ready___0 = this._this.popup_Ready.transform.Find("Text_Ready");
					this._text_go___0 = this._this.popup_Ready.transform.Find("Text_Go");
					this._text_ready___0.DOScale(new Vector3(1f, 1f, 1f), 1.5f).SetEase(Ease.Linear);
					this._current = new WaitForSeconds(1.5f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._text_ready___0.gameObject.SetActive(false);
					SingletonComponent<EffectSound>.Instance.GameSound(2);
					this._text_go___0.DOScale(new Vector3(1f, 1f, 1f), 0.4f).SetEase(Ease.OutBounce);
					this._current = new WaitForSeconds(0.4f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this.popup_Ready.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
					this._current = new WaitForSeconds(0.2f);
					if (!this._disposing)
					{
						this._PC = 3;
					}
					return true;
				case 3u:
					this._this.popup_Ready.SetActive(false);
					SingletonComponent<WordSearchController>.Instance.IsPause = false;
					SingletonComponent<EffectSound_ing>.Instance.GameSound(0);
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
		private GameObject loadingObject;

		[SerializeField]
		private GameObject popup_levelup;

		[SerializeField]
		private GameObject popup_Ready;

		[SerializeField]
		private GameObject popup_Slot;

		[SerializeField]
		private GameObject Wordlist_OBJ;

		public override void Initialize()
		{
			base.Initialize();
			WordSearchController expr_0B = SingletonComponent<WordSearchController>.Instance;
			expr_0B.OnBoardStateChanged = (Action)Delegate.Combine(expr_0B.OnBoardStateChanged, new Action(this.UpdateUI));
			if (PlayerPrefs.GetInt("First_Start_Tutorial", 0) == 0)
			{
				SingletonComponent<WordSearchController>.Instance.StartTutorial();
			}
		}

		public override void OnShowing(string data)
		{
			base.OnShowing(data);
		}

		public void OnNewGameButtonClicked(int pop_num)
		{
			if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName != "Tutorial")
			{
				SingletonComponent<AdMob>.Instance.show_InterstitialAd();
			}
			SingletonComponent<UIScreenController>.Instance.PopupClose(pop_num);
			if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Challenge")
			{
				SingletonComponent<WordSearchController>.Instance.Challenge_New_Game();
			}
			else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
			{
				SingletonComponent<WordSearchController>.Instance.Quiz_New_Game();
			}
			else
			{
				SingletonComponent<WordSearchController>.Instance.GamePlayCounter();
				this.popup_Slot.GetComponent<PopSlot>().PopOpen();
			}
		}

		public void OnMainMenuButtonClicked(int pop_num)
		{
			if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName != "Tutorial")
			{
				SingletonComponent<AdMob>.Instance.show_InterstitialAd();
			}
			SingletonComponent<UIScreenController>.Instance.PopupClose(pop_num);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(2, 0);
		}

		public void OnClicked_Skip()
		{
			base.transform.Find("Hand").gameObject.SetActive(false);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(2, 0);
		}

		public void OnClick_Pause()
		{
			SingletonComponent<UIScreenController>.Instance.PopupOpen(2);
		}

		private void UpdateUI()
		{
			if (SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.Generating)
			{
				this.loadingObject.GetComponent<CanvasGroup>().alpha = 0f;
				this.loadingObject.SetActive(true);
				this.loadingObject.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
				this.Wordlist_OBJ.GetComponent<WordList>().BG_Reset();
				if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Challenge")
				{
					SingletonComponent<UIScreenController>.Instance.returnOBJ(2).GetComponent<UIGameScreen>().ResetReadyPop();
				}
				else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
				{
					SingletonComponent<UIScreenController>.Instance.returnOBJ(2).GetComponent<UIGameScreen>().ResetReadyPop();
				}
			}
			else
			{
				this.loadingObject.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(new TweenCallback(this.CompleteCloseLoading));
			}
			bool flag = SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.Completed;
			bool flag2 = SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.TimesUp;
			if (flag || flag2)
			{
				if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Challenge")
				{
					SingletonComponent<EffectSound_ing>.Instance.SoundStop();
					if (flag)
					{
						base.StartCoroutine("LevelUp_Challenge_Action");
					}
					else if (flag2)
					{
						SingletonComponent<EffectSound>.Instance.GameSound(3);
						bool new_record = false;
						if (PlayerPrefs.GetInt("ChallengeModeLevel", 0) < SingletonComponent<WordSearchController>.Instance.Challenge_Level)
						{
							new_record = true;
							SingletonComponent<EffectSound>.Instance.GameSound(4);
						}
						else
						{
							SingletonComponent<EffectSound>.Instance.GameSound(5);
						}
						SingletonComponent<UIScreenController>.Instance.PopupOpenChallenge(6, SingletonComponent<WordSearchController>.Instance.Challenge_Level + 1, new_record);
						PlayerPrefs.SetInt("ChallengeModeLevel", SingletonComponent<WordSearchController>.Instance.Challenge_Level);
					}
				}
				else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
				{
					SingletonComponent<EffectSound_ing>.Instance.SoundStop();
					if (flag)
					{
						base.StartCoroutine("LevelUp_Quiz_Action");
					}
					else if (flag2)
					{
						SingletonComponent<EffectSound>.Instance.GameSound(3);
						bool new_record2 = false;
						if (PlayerPrefs.GetInt("QuizModeLevel", 0) < SingletonComponent<WordSearchController>.Instance.Quiz_Level)
						{
							new_record2 = true;
							SingletonComponent<EffectSound>.Instance.GameSound(4);
						}
						else
						{
							SingletonComponent<EffectSound>.Instance.GameSound(5);
						}
						SingletonComponent<UIScreenController>.Instance.PopupOpenChallenge(7, SingletonComponent<WordSearchController>.Instance.Quiz_Level + 1, new_record2);
						PlayerPrefs.SetInt("QuizModeLevel", SingletonComponent<WordSearchController>.Instance.Quiz_Level);
					}
				}
				else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Tutorial")
				{
					if (flag || flag2)
					{
						SingletonComponent<EffectSound>.Instance.GameSound(5);
						SingletonComponent<UIScreenController>.Instance.PopupOpen(8);
						base.transform.Find("Hand").gameObject.SetActive(false);
					}
				}
				else
				{
					SingletonComponent<EffectSound>.Instance.GameSound(4);
					SingletonComponent<UIScreenController>.Instance.PopupOpen(5);
					SingletonComponent<WordSearchController>.Instance.GamePlayOpenReview();
				}
			}
			else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Challenge")
			{
				SingletonComponent<UIScreenController>.Instance.PopupClose(6);
			}
			else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
			{
				SingletonComponent<UIScreenController>.Instance.PopupClose(7);
			}
			else
			{
				SingletonComponent<UIScreenController>.Instance.PopupClose(5);
			}
			if (SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.BoardActive && SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Challenge")
			{
				this.ReadyPlay();
			}
			if (SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.BoardActive && SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
			{
				this.ReadyPlay();
			}
		}

		private void CompleteCloseLoading()
		{
			this.loadingObject.SetActive(false);
		}

		public void ReadyPlay()
		{
			base.StartCoroutine("Ready_Action");
		}

		public void ResetReadyPop()
		{
			Transform transform = this.popup_Ready.transform.Find("Text_Ready");
			Transform transform2 = this.popup_Ready.transform.Find("Text_Go");
			transform.gameObject.SetActive(true);
			transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			transform2.localScale = new Vector3(0f, 0f, 0f);
			this.popup_Ready.GetComponent<CanvasGroup>().alpha = 1f;
			this.popup_Ready.SetActive(true);
		}

		private IEnumerator LevelUp_Challenge_Action()
		{
			UIGameScreen._LevelUp_Challenge_Action_c__Iterator0 _LevelUp_Challenge_Action_c__Iterator = new UIGameScreen._LevelUp_Challenge_Action_c__Iterator0();
			_LevelUp_Challenge_Action_c__Iterator._this = this;
			return _LevelUp_Challenge_Action_c__Iterator;
		}

		private IEnumerator LevelUp_Quiz_Action()
		{
			UIGameScreen._LevelUp_Quiz_Action_c__Iterator1 _LevelUp_Quiz_Action_c__Iterator = new UIGameScreen._LevelUp_Quiz_Action_c__Iterator1();
			_LevelUp_Quiz_Action_c__Iterator._this = this;
			return _LevelUp_Quiz_Action_c__Iterator;
		}

		private IEnumerator Ready_Action()
		{
			UIGameScreen._Ready_Action_c__Iterator2 _Ready_Action_c__Iterator = new UIGameScreen._Ready_Action_c__Iterator2();
			_Ready_Action_c__Iterator._this = this;
			return _Ready_Action_c__Iterator;
		}
	}
}
