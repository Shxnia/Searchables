using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class UIOption : UIScreen
	{
		private sealed class _Initialize_c__AnonStorey0
		{
			internal int index;

			internal UIOption _this;

			internal void __m__0()
			{
				this._this.OnClick_Select_Level(this.index);
			}
		}

		private sealed class _Initialize_c__AnonStorey1
		{
			internal int index;

			internal UIOption _this;

			internal void __m__0()
			{
				this._this.OnClick_Select_Category(this.index);
			}
		}

		[SerializeField]
		private Transform UIMain;

		[SerializeField]
		private Transform UIPlay;

		private int Current_Difficulty;

		private int Current_Category;

		private Color Current_Category_Color;

		[Header("Animate Option")]
		public float BG_Move_Speed;

		[Header("Level Button")]
		public List<GameObject> Btn_Level;

		[Header("Character Button")]
		public GameObject Btn_Character;

		[Header("Category Button")]
		public List<GameObject> Btn_Category;

		public Color32 Category_On;

		public Color32 Category_Off;

		[Header("Start Button")]
		public List<GameObject> Btn_Start;

		[Header("Cate Button")]
		public GameObject Btn_Cate;

		public GameObject Btn_Box_Parent;

		[Header("ECT Button")]
		public GameObject Btn_Setting;

		public GameObject Btn_Achivement;

		[Header("Font")]
		public Font font_other;

		public Font font_jpn;

		public override void Initialize()
		{
			base.Initialize();
			for (int i = 0; i < this.Btn_Level.Count; i++)
			{
				int index = i;
				this.Btn_Level[i].GetComponent<Button>().onClick.AddListener(delegate
				{
					this.OnClick_Select_Level(index);
				});
			}
			for (int j = 0; j < SingletonComponent<WordSearchController>.Instance.CategoryInfos.Count; j++)
			{
				int index = j;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Btn_Cate);
				gameObject.transform.parent = this.Btn_Box_Parent.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.Find("Icon_off").GetComponent<Image>().sprite = SingletonComponent<WordSearchController>.Instance.CategoryInfos[j].categoryIcon;
				gameObject.transform.Find("Icon_off").GetComponent<Image>().color = new Color32(177, 177, 177, 255);
				gameObject.transform.Find("Icon_on").GetComponent<Image>().sprite = SingletonComponent<WordSearchController>.Instance.CategoryInfos[j].categoryIcon;
				if (SingletonComponent<LanguageController>.Instance.DeviceLanguage == 1)
				{
					gameObject.transform.Find("Text").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.CategoryInfos[j].categoryName_KOR;
				}
				else if (SingletonComponent<LanguageController>.Instance.DeviceLanguage == 2)
				{
					gameObject.transform.Find("Text").GetComponent<Text>().font = this.font_jpn;
					gameObject.transform.Find("Text").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.CategoryInfos[j].categoryName_JPN;
				}
				else
				{
					gameObject.transform.Find("Text").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.CategoryInfos[j].categoryName_ENG;
				}
				gameObject.GetComponent<Button>().onClick.AddListener(delegate
				{
					this.OnClick_Select_Category(index);
				});
				this.Btn_Category.Add(gameObject);
			}
			this.Btn_Box_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(440f, (float)(SingletonComponent<WordSearchController>.Instance.CategoryInfos.Count * 82));
			this.Btn_Level_OnOff(0);
			this.Btn_Category_OnOff(0);
		}

		public void Btn_Languege_Changer(int index)
		{
			for (int i = 0; i < this.Btn_Category.Count; i++)
			{
				if (index == 1)
				{
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().font = this.font_other;
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.CategoryInfos[i].categoryName_KOR;
				}
				else if (index == 2)
				{
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().font = this.font_jpn;
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.CategoryInfos[i].categoryName_JPN;
				}
				else
				{
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().font = this.font_other;
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.CategoryInfos[i].categoryName_ENG;
				}
			}
		}

		public void Btn_Level_OnOff(int index)
		{
			this.Current_Difficulty = index;
			SingletonComponent<WordSearchController>.Instance.SetSelectedDifficulty(index);
			for (int i = 0; i < this.Btn_Level.Count; i++)
			{
				this.Btn_Level[i].transform.Find("Check").transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutExpo);
				this.Btn_Level[i].transform.Find("DottedGroup").gameObject.SetActive(false);
				if (i == index)
				{
					this.Btn_Level[i].transform.Find("Check").transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
					this.Btn_Level[i].transform.Find("DottedGroup").gameObject.SetActive(true);
				}
			}
			if (index == 0)
			{
				this.Current_Category_Color = new Color(1f, 0.6392157f, 0.6352941f);
			}
			else if (index == 1)
			{
				this.Current_Category_Color = new Color(0.4431373f, 0.7333333f, 0.509804f);
			}
			else if (index == 2)
			{
				this.Current_Category_Color = new Color(0.4196079f, 0.4862745f, 0.5490196f);
			}
			this.Btn_Category[this.Current_Category].GetComponent<Image>().DOColor(this.Current_Category_Color, 0.2f);
			this.Btn_Category[this.Current_Category].transform.Find("CheckBox").Find("Check1").gameObject.SetActive(false);
			this.Btn_Category[this.Current_Category].transform.Find("CheckBox").Find("Check2").gameObject.SetActive(false);
			this.Btn_Category[this.Current_Category].transform.Find("CheckBox").Find("Check3").gameObject.SetActive(false);
			this.Btn_Category[this.Current_Category].transform.Find("CheckBox").Find("Check" + (this.Current_Difficulty + 1).ToString()).gameObject.SetActive(true);
		}

		public void OnClick_Select_Level(int index)
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(4);
			this.Btn_Level_OnOff(index);
		}

		public void Btn_Category_OnOff(int index)
		{
			this.Current_Category = index;
			SingletonComponent<WordSearchController>.Instance.SelectedCategorys(index);
			for (int i = 0; i < this.Btn_Category.Count; i++)
			{
				if (i == index)
				{
					this.Btn_Category[i].GetComponent<Image>().color = this.Current_Category_Color;
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check1").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check2").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check3").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("Icon_on").gameObject.SetActive(true);
					this.Btn_Category[i].transform.Find("Icon_off").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().color = this.Category_On;
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check" + (this.Current_Difficulty + 1).ToString()).gameObject.SetActive(true);
					this.Btn_Category[i].transform.Find("CheckBox").DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
				}
				else
				{
					this.Btn_Category[i].GetComponent<Image>().color = new Color(0.9019608f, 0.9019608f, 0.9019608f);
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check1").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check2").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("CheckBox").Find("Check3").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("Icon_on").gameObject.SetActive(false);
					this.Btn_Category[i].transform.Find("Icon_off").gameObject.SetActive(true);
					this.Btn_Category[i].transform.Find("Text").GetComponent<Text>().color = this.Category_Off;
					this.Btn_Category[i].transform.Find("CheckBox").DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutExpo);
				}
			}
		}

		public void OnClick_Select_Category(int index)
		{
			SingletonComponent<EffectSound>.Instance.SystemSound(1);
			this.Btn_Category_OnOff(index);
			this.Onclick_StartGame(index);
		}

		public void Onclick_StartGame(int index)
		{
			SingletonComponent<UIScreenController>.Instance.MoveScreen(3, 2);
			SingletonComponent<WordSearchController>.Instance.StartGame();
		}

		public void Onclick_BackMain()
		{
			SingletonComponent<UIScreenController>.Instance.MoveScreen(1, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(2, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(3, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(4, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(5, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(6, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(7, 0);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(8, 0);
		}

		public void Onclick_Setting_Open()
		{
			SingletonComponent<UIScreenController>.Instance.PopupOpen(1);
		}
	}
}
