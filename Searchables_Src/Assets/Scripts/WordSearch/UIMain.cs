using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class UIMain : UIScreen
	{
		[SerializeField]
		private Transform UIOption;

		[SerializeField]
		private Transform UIPlay;

		[Header("Animate Option")]
		public float BG_Move_Speed;

		[Header("Start Button")]
		public List<GameObject> Btn_Start;

		[Header("Language Button")]
		public GameObject Btn_Language;

		public GameObject Popup_Language;

		[Header("ECT Button")]
		public GameObject Btn_Setting;

		[Header("Title Text")]
		public Text Text_Title;

		public void OnClick_StartButton(int index)
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(1);
			if (index == 0)
			{
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 1);
			}
			else if (index == 1)
			{
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 1);
			}
			else if (index == 2)
			{
				SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 2);
			}
			else if (index == 3)
			{
				// generate window
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(1, 3);
			}
			else if (index == 4)
			{
				// Word bank windwo
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 4);
			}
			else if (index == 5)
			{
				// class info
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(1, 5);
			}
			else if (index == 6)
			{
				// Log in 
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 6);
			}
			else if (index == 7)
			{
				// Log in 
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 7);
			}
			else if (index == 8)
			{
				// Log in 
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 8);
			}
			else if (index == -1)
			{
				// Log in 
				//SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
				SingletonComponent<UIScreenController>.Instance.MoveScreen(8, 1);
			}
		}

		public void OnClick_StartChallenge()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
			SingletonComponent<WordSearchController>.Instance.SetSelectedMode(3);
		}

		public void OnClick_Language_Select(int index)
		{
			SingletonComponent<WordSearchController>.Instance.SettingGameLanguage(index);
			this.ChangeFlag(index);
			this.PopupLanguage_CLose();
		}

		public void OnClick_Language()
		{
			this.PopupLanguage_Open();
		}

		private void PopupLanguage_Open()
		{
			if (SingletonComponent<WordSearchController>.Instance.IsPopLanguage)
			{
				this.PopupLanguage_CLose();
				return;
			}
			SingletonComponent<EffectSound>.Instance.SystemSound(2);
			SingletonComponent<WordSearchController>.Instance.IsPopLanguage = true;
			this.Popup_Language.GetComponent<CanvasGroup>().alpha = 0f;
			this.Popup_Language.SetActive(true);
			this.Popup_Language.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		}

		private void PopupLanguage_CLose()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(3);
			this.Popup_Language.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(new TweenCallback(this.Complete_PopupLanguage_CLose));
		}

		private void Complete_PopupLanguage_CLose()
		{
			SingletonComponent<WordSearchController>.Instance.IsPopLanguage = false;
			this.Popup_Language.SetActive(false);
		}

		public void Onclick_Setting_Open()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(2);
			SingletonComponent<UIScreenController>.Instance.PopupOpen(1);
		}

		public void ChangeFlag(int index)
		{
			List<GameObject> list = new List<GameObject>();
			list.Add(this.Btn_Language.transform.Find("ENG").gameObject);
			list.Add(this.Btn_Language.transform.Find("KOR").gameObject);
			list.Add(this.Btn_Language.transform.Find("JPN").gameObject);
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetActive(false);
			}
			list[index].SetActive(true);
		}
	}
}
