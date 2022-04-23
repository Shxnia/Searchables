using System;
using UnityEngine;

namespace WordSearch
{
	public class UIPop : MonoBehaviour
	{
		private void OnEnable()
		{
			if (base.transform.Find("InBox").Find("Btn_Sound") != null)
			{
				GameObject gameObject = base.transform.Find("InBox").Find("Btn_Sound").Find("On").gameObject;
				GameObject gameObject2 = base.transform.Find("InBox").Find("Btn_Sound").Find("Off").gameObject;
				if (SingletonComponent<WordSearchController>.Instance.IsSound == 0)
				{
					gameObject2.SetActive(true);
					gameObject.SetActive(false);
				}
				else
				{
					gameObject2.SetActive(false);
					gameObject.SetActive(true);
				}
			}
			if (base.transform.Find("InBox").Find("Btn_Push") != null)
			{
				GameObject gameObject3 = base.transform.Find("InBox").Find("Btn_Push").Find("On").gameObject;
				GameObject gameObject4 = base.transform.Find("InBox").Find("Btn_Push").Find("Off").gameObject;
				if (SingletonComponent<WordSearchController>.Instance.IsPushAlert == 0)
				{
					gameObject4.SetActive(true);
					gameObject3.SetActive(false);
				}
				else
				{
					gameObject4.SetActive(false);
					gameObject3.SetActive(true);
				}
			}
		}

		public void Onclick_Sound(GameObject obj)
		{
			GameObject gameObject = obj.transform.Find("On").gameObject;
			GameObject gameObject2 = obj.transform.Find("Off").gameObject;
			if (SingletonComponent<WordSearchController>.Instance.IsSound == 0)
			{
				gameObject2.SetActive(false);
				gameObject.SetActive(true);
				SingletonComponent<EffectSound>.Instance.SoundOnOff(1);
				SingletonComponent<EffectSound_ing>.Instance.SoundOnOff(1);
			}
			else
			{
				gameObject2.SetActive(true);
				gameObject.SetActive(false);
				SingletonComponent<EffectSound>.Instance.SoundOnOff(0);
				SingletonComponent<EffectSound_ing>.Instance.SoundOnOff(0);
			}
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
		}

		public void Onclick_Push(GameObject obj)
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
			GameObject gameObject = obj.transform.Find("On").gameObject;
			GameObject gameObject2 = obj.transform.Find("Off").gameObject;
			if (SingletonComponent<WordSearchController>.Instance.IsPushAlert == 0)
			{
				gameObject2.SetActive(false);
				gameObject.SetActive(true);
				SingletonComponent<WordSearchController>.Instance.IsPushAlert = 1;
				PlayerPrefs.SetInt("IsPushAlert", 1);
				SingletonComponent<Global>.Instance.ScheduleCustom();
			}
			else
			{
				gameObject2.SetActive(true);
				gameObject.SetActive(false);
				SingletonComponent<WordSearchController>.Instance.IsPushAlert = 0;
				PlayerPrefs.SetInt("IsPushAlert", 0);
				SingletonComponent<Global>.Instance.CancelAll();
			}
		}

		public void Onclick_Credit()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
		}

		public void Onclick_Help()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
			SingletonComponent<UIScreenController>.Instance.PopupOpen(11);
		}

		public void Onclick_Main()
		{
			SingletonComponent<EffectSound_ing>.Instance.SoundStop();
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(2, 0);
			SingletonComponent<UIScreenController>.Instance.PopupClose(2);
		}

		public void Onclick_Review()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
			SingletonComponent<UIScreenController>.Instance.PopupOpen(4);
		}

		public void Onclick_Review_Yes()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(0);
			SingletonComponent<Global>.Instance.go_market();
			SingletonComponent<UIScreenController>.Instance.PopupClose(4);
			PlayerPrefs.SetInt("Open_Review_Flag", 1);
		}

		public void Onclick_Review_No()
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(3);
			SingletonComponent<UIScreenController>.Instance.PopupClose(4);
		}

		public void Onclick_Close(int index)
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(3);
			SingletonComponent<UIScreenController>.Instance.PopupClose(index);
		}

		public void Onclick_Admob_Reward()
		{
			SingletonComponent<AdMob_Reward>.Instance.show_rewardAd();
			SingletonComponent<UIScreenController>.Instance.PopupClose(10);
		}

		public void Onclick_AppQuit()
		{
			Application.Quit();
		}
	}
}
