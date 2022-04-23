using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class WordList : MonoBehaviour
	{
		private sealed class _SetWordFound_c__AnonStorey0
		{
			internal string word;

			internal bool __m__0(string a)
			{
				return a == this.word;
			}
		}

		[SerializeField]
		private RectTransform wordListContainer;

		[SerializeField]
		private WordListItem wordListItemPrefab;

		[SerializeField]
		private CanvasGroup wordListCanvasGroup;

		[SerializeField]
		private RectTransform sequentialContainer;

		[SerializeField]
		private WordListItem sequentialItemPrefab;

		[SerializeField]
		private RectTransform QuizContainer;

		[SerializeField]
		private WordListItem QuizItemPrefab;

		[SerializeField]
		private RectTransform TutorialContainer;

		[SerializeField]
		private WordListItem TutorialItemPrefab;

		[SerializeField]
		private GameObject UI_Timer;

		[SerializeField]
		private Sprite UI_Timer_BG_chall;

		[SerializeField]
		private Sprite UI_Timer_BG_quiz;

		[SerializeField]
		private GameObject UI_Classic;

		[SerializeField]
		private GameObject UI_Challenge;

		[SerializeField]
		private GameObject UI_Quiz;

		[SerializeField]
		private GameObject UI_Tutorial;

		[SerializeField]
		private GameObject UI_BG;

		[SerializeField]
		private GameObject UI_Hand;

		[SerializeField]
		private List<Sprite> UI_Chk_Sprite;

		[SerializeField]
		private GameObject UI_Pop_Hint;

		[SerializeField]
		private Text UI_Hint_Counter;

		[SerializeField]
		private Font font_other;

		[SerializeField]
		private Font font_jpn;

		private ObjectPool wordListItemPool;

		private ObjectPool sequentialItemPool;

		private ObjectPool sequentialItemPool_Quiz;

		private ObjectPool sequentialItemPool_Tutorial;

		private Dictionary<string, WordListItem> wordListItems;

		private List<string> sequentialWords;

		private List<string> quizQuesions;

		private string currentSequentialWord;

		private bool hint_flag;

		private bool time_almost_flag = true;

		private static Tween.OnTweenFinished __f__am_cache0;

		private void Update()
		{
			if (SingletonComponent<WordSearchController>.Instance.IsTimedMode)
			{
				this.UI_Timer.transform.Find("Counter").GetComponent<Text>().text = SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds.ToString();
				float x = (float)SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds / SingletonComponent<WordSearchController>.Instance.ActiveDifficulty.timedModeStartTime * 306f;
				this.UI_Timer.transform.Find("TimeBar").GetComponent<RectTransform>().DOSizeDelta(new Vector2(x, 18f), 1f, false).SetEase(Ease.Linear);
				if (SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds == 4 && this.time_almost_flag)
				{
					this.time_almost_flag = false;
					SingletonComponent<EffectSound_ing>.Instance.GameSound(1);
				}
			}
		}

		public void Initialize()
		{
			this.wordListItemPool = new ObjectPool(this.wordListItemPrefab.gameObject, 10, this.wordListContainer);
			this.sequentialItemPool = new ObjectPool(this.sequentialItemPrefab.gameObject, 10, this.sequentialContainer);
			this.sequentialItemPool_Quiz = new ObjectPool(this.QuizItemPrefab.gameObject, 10, this.QuizContainer);
			this.sequentialItemPool_Tutorial = new ObjectPool(this.TutorialItemPrefab.gameObject, 10, this.TutorialContainer);
			this.wordListItems = new Dictionary<string, WordListItem>();
			this.sequentialWords = new List<string>();
			this.quizQuesions = new List<string>();
		}

		public void Setup(Board board)
		{
			this.Clear();
			this.UIModeChanger();
			this.sequentialWords = new List<string>(board.usedWords);
			this.quizQuesions = new List<string>(board.usedQuestions);
			if (SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count == 0 && !SingletonComponent<WordSearchController>.Instance.Quiz_Hint_AD_Flag)
			{
				this.UI_Hint_Counter.text = "AD";
			}
			else
			{
				this.UI_Hint_Counter.text = SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count.ToString();
			}
			this.hint_flag = false;
			foreach (KeyValuePair<string, WordListItem> current in this.wordListItems)
			{
				current.Value.wordText.color = new Color(1f, 1f, 1f, 1f);
			}
			this.time_almost_flag = true;
			if (SingletonComponent<WordSearchController>.Instance.IsSequentialMode)
			{
				this.NextSequentialWord(false);
			}
			else
			{
				for (int i = 0; i < this.sequentialWords.Count; i++)
				{
					this.CreateWordListItem(this.sequentialWords[i], this.wordListItemPool);
				}
			}
			Tween.CanvasGroupAlpha(this.wordListCanvasGroup, Tween.TweenStyle.EaseOut, 0f, 1f, 400f);
		}

		public void SetWordFound(string word, bool fromSave)
		{
			if (this.wordListItems.ContainsKey(word))
			{
				int count = SingletonComponent<WordSearchController>.Instance.FoundWords.Count;
				int count2 = SingletonComponent<WordSearchController>.Instance.ActiveBoard.usedWords.Count;
				this.UIWordCountChanger(count, count2);
				if (SingletonComponent<WordSearchController>.Instance.IsSequentialMode)
				{
					this.time_almost_flag = true;
					SingletonComponent<EffectSound>.Instance.SoundStop();
					this.NextSequentialWord(!fromSave);
				}
				else
				{
					if (SingletonComponent<WordSearchController>.Instance.ActiveDifficulty.difficultyName == "easy")
					{
						this.wordListItems[word].foundIndicator.GetComponent<Image>().sprite = this.UI_Chk_Sprite[0];
					}
					else if (SingletonComponent<WordSearchController>.Instance.ActiveDifficulty.difficultyName == "normal")
					{
						this.wordListItems[word].foundIndicator.GetComponent<Image>().sprite = this.UI_Chk_Sprite[1];
					}
					else if (SingletonComponent<WordSearchController>.Instance.ActiveDifficulty.difficultyName == "hard")
					{
						this.wordListItems[word].foundIndicator.GetComponent<Image>().sprite = this.UI_Chk_Sprite[2];
					}
					this.wordListItems[word].wordText.color = new Color(1f, 1f, 1f, 0.5f);
					this.wordListItems[word].foundIndicator.SetActive(true);
				}
			}
			else if (SingletonComponent<WordSearchController>.Instance.IsSequentialMode)
			{
				int num = this.sequentialWords.FindIndex((string a) => a == word);
				this.quizQuesions.RemoveAt(num);
				this.sequentialWords.Remove(word);
				UnityEngine.Debug.Log(num);
			}
			else
			{
				UnityEngine.Debug.LogError("[WordList] Word does not exist in the word list: " + word);
			}
		}

		public void Clear()
		{
			this.wordListItemPool.ReturnAllObjectsToPool();
			this.sequentialItemPool.ReturnAllObjectsToPool();
			this.sequentialItemPool_Quiz.ReturnAllObjectsToPool();
			this.sequentialItemPool_Tutorial.ReturnAllObjectsToPool();
			this.wordListItems.Clear();
			this.sequentialWords.Clear();
			this.quizQuesions.Clear();
			this.currentSequentialWord = string.Empty;
			this.wordListContainer.sizeDelta = new Vector2(this.wordListContainer.sizeDelta.x, 0f);
			this.wordListCanvasGroup.alpha = 0f;
		}

		private void NextSequentialWord(bool doAnimation)
		{
			string text = this.currentSequentialWord.Replace("\r", string.Empty).Trim();
			text = text.Replace(" ", string.Empty);
			WordListItem wordListItem = (!string.IsNullOrEmpty(text)) ? this.wordListItems[text] : null;
			WordListItem wordListItem2;
			if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
			{
				this.hint_flag = false;
				wordListItem2 = ((this.sequentialWords.Count != 0) ? this.CreateWordListItem(this.sequentialWords[0], this.sequentialItemPool_Quiz) : null);
				Text component = this.UI_Quiz.transform.Find("QuizExplaing").GetComponent<Text>();
				if (this.quizQuesions.Count > 0)
				{
					component.text = this.quizQuesions[0];
				}
			}
			else if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Tutorial")
			{
				this.UIHand(this.sequentialWords.Count);
				wordListItem2 = ((this.sequentialWords.Count != 0) ? this.CreateWordListItem(this.sequentialWords[0], this.sequentialItemPool_Tutorial) : null);
			}
			else
			{
				wordListItem2 = ((this.sequentialWords.Count != 0) ? this.CreateWordListItem(this.sequentialWords[0], this.sequentialItemPool) : null);
			}
			this.currentSequentialWord = ((this.sequentialWords.Count != 0) ? this.sequentialWords[0] : string.Empty);
			if (this.sequentialWords.Count > 0)
			{
				this.sequentialWords.RemoveAt(0);
				if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
				{
					this.quizQuesions.RemoveAt(0);
				}
			}
			if (doAnimation)
			{
				float width = (this.sequentialContainer.transform as RectTransform).rect.width;
				if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
				{
					width = (this.QuizContainer.transform as RectTransform).rect.width;
				}
				if (wordListItem != null)
				{
					Tween tween = Tween.PositionX(wordListItem.transform, Tween.TweenStyle.EaseOut, 0f, -width, 500f, false, Tween.LoopType.None);
					tween.SetUseRectTransform(true);
					tween.SetFinishCallback(delegate(GameObject tweenedObject)
					{
						tweenedObject.SetActive(false);
					});
				}
				if (wordListItem2 != null)
				{
					Tween.PositionX(wordListItem2.transform, Tween.TweenStyle.EaseOut, width, 0f, 1000f, false, Tween.LoopType.None).SetUseRectTransform(true);
				}
			}
			else
			{
				if (wordListItem != null)
				{
					wordListItem.gameObject.SetActive(false);
				}
				if (wordListItem2 != null)
				{
					(wordListItem2.transform as RectTransform).anchoredPosition = Vector2.zero;
				}
			}
		}

		private WordListItem CreateWordListItem(string word, ObjectPool itemPool)
		{
			WordListItem wordListItem = null;
			if (!this.wordListItems.ContainsKey(word))
			{
				wordListItem = itemPool.GetObject().GetComponent<WordListItem>();
				if (SingletonComponent<WordSearchController>.Instance.Language == 2)
				{
					wordListItem.wordText.font = this.font_jpn;
					wordListItem.wordText.fontStyle = FontStyle.Normal;
				}
				else
				{
					wordListItem.wordText.font = this.font_other;
					wordListItem.wordText.fontStyle = FontStyle.Bold;
				}
				if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Quiz")
				{
					string text = string.Empty;
					for (int i = 0; i < word.Length; i++)
					{
						text += "■";
					}
					wordListItem.wordText.text = text;
				}
				else
				{
					wordListItem.wordText.text = word.ToUpper();
					wordListItem.wordText.color = new Color(1f, 1f, 1f, 1f);
				}
				wordListItem.gameObject.SetActive(true);
				if (wordListItem.foundIndicator != null)
				{
					wordListItem.foundIndicator.SetActive(false);
				}
				string key = word.Replace(" ", string.Empty);
				this.wordListItems.Add(key, wordListItem);
			}
			else
			{
				UnityEngine.Debug.LogWarning("[WordList] Board contains duplicate words. Word: " + word);
			}
			return wordListItem;
		}

		public void UIModeChanger()
		{
			Image component = this.UI_BG.GetComponent<Image>();
			this.UI_Classic.SetActive(false);
			this.UI_Challenge.SetActive(false);
			this.UI_Quiz.SetActive(false);
			this.UI_Tutorial.SetActive(false);
			this.UI_Hand.SetActive(false);
			base.transform.parent.Find("Btn_Pause").gameObject.SetActive(true);
			if (SingletonComponent<WordSearchController>.Instance.SelectedMode != null)
			{
				if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Challenge")
				{
					this.UI_Timer.GetComponent<Image>().sprite = this.UI_Timer_BG_chall;
					this.UI_Challenge.SetActive(true);
					this.UI_Challenge.transform.Find("CurrentLevel").GetComponent<Text>().text = "Lv." + (SingletonComponent<WordSearchController>.Instance.Challenge_Level + 1);
					this.UI_Challenge.transform.Find("BestLevel").GetComponent<Text>().text = "My Best Lv." + (PlayerPrefs.GetInt("ChallengeModeLevel", 0) + 1);
					component.color = new Color32(49, 49, 49, 255);
					Text component2 = this.UI_Challenge.transform.Find("Category").GetComponent<Text>();
					if (SingletonComponent<WordSearchController>.Instance.Language == 1)
					{
						component2.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_KOR;
					}
					else if (SingletonComponent<WordSearchController>.Instance.Language == 2)
					{
						component2.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_JPN;
					}
					else
					{
						component2.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_ENG;
					}
				}
				else if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Quiz")
				{
					this.UI_Timer.GetComponent<Image>().sprite = this.UI_Timer_BG_quiz;
					this.UI_Quiz.SetActive(true);
					this.UI_Quiz.transform.Find("CurrentLevel").GetComponent<Text>().text = "Lv." + (SingletonComponent<WordSearchController>.Instance.Quiz_Level + 1);
					component.color = new Color32(136, 112, 255, 255);
					Text component3 = this.UI_Quiz.transform.Find("Category").GetComponent<Text>();
					if (SingletonComponent<WordSearchController>.Instance.Language == 1)
					{
						component3.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_KOR;
					}
					else if (SingletonComponent<WordSearchController>.Instance.Language == 2)
					{
						component3.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_JPN;
					}
					else
					{
						component3.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_ENG;
					}
				}
				else if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Tutorial")
				{
					base.transform.parent.Find("Btn_Pause").gameObject.SetActive(false);
					this.UI_Tutorial.SetActive(true);
					Text component4 = this.UI_Tutorial.transform.Find("Mode").Find("Text").GetComponent<Text>();
					component4.text = SingletonComponent<LanguageController>.Instance.ModeNameTutorial();
					component4.color = new Color32(255, 210, 0, 255);
					component.color = new Color32(255, 210, 0, 255);
				}
				else
				{
					this.UI_Classic.SetActive(true);
					Text component5 = this.UI_Classic.transform.Find("Mode").Find("Text").GetComponent<Text>();
					string difficultyName = SingletonComponent<WordSearchController>.Instance.SelectedDifficulty.difficultyName;
					Text component6 = this.UI_Classic.transform.Find("Category").GetComponent<Text>();
					if (SingletonComponent<WordSearchController>.Instance.Language == 1)
					{
						component6.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_KOR;
					}
					else if (SingletonComponent<WordSearchController>.Instance.Language == 2)
					{
						component6.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_JPN;
					}
					else
					{
						component6.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_ENG;
					}
					if (difficultyName == "easy")
					{
						component5.text = SingletonComponent<LanguageController>.Instance.ModeNameEasy();
						component5.color = new Color32(255, 162, 160, 255);
						component.color = new Color32(255, 162, 160, 255);
					}
					else if (difficultyName == "normal")
					{
						component5.text = SingletonComponent<LanguageController>.Instance.ModeNameNormal();
						component5.color = new Color32(114, 188, 128, 255);
						component.color = new Color32(114, 188, 128, 255);
					}
					else if (difficultyName == "hard")
					{
						component5.text = SingletonComponent<LanguageController>.Instance.ModeNameHard();
						component5.color = new Color32(107, 124, 140, 255);
						component.color = new Color32(107, 124, 140, 255);
					}
				}
			}
			if (SingletonComponent<WordSearchController>.Instance.IsTimedMode)
			{
				this.UI_Timer.SetActive(true);
			}
			else
			{
				this.UI_Timer.SetActive(false);
			}
		}

		public void BG_Reset()
		{
			this.UI_Classic.SetActive(false);
			this.UI_Challenge.SetActive(false);
			this.UI_Quiz.SetActive(false);
			this.UI_Tutorial.SetActive(false);
			this.UI_Hand.SetActive(false);
			this.UI_BG.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
		}

		public void UIWordCountChanger(int available_total, int all_total)
		{
			if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Challenge")
			{
				this.UI_Challenge.transform.Find("Word_Counter").GetComponent<Text>().text = available_total + " / " + all_total;
			}
			else if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Quiz")
			{
				this.UI_Quiz.transform.Find("Word_Counter").GetComponent<Text>().text = available_total + " / " + all_total;
			}
			else if (SingletonComponent<WordSearchController>.Instance.SelectedMode.modeName == "Tutorial")
			{
				this.UI_Tutorial.transform.Find("Word_Counter").GetComponent<Text>().text = available_total + " / " + all_total;
			}
			else
			{
				this.UI_Classic.transform.Find("Word_Counter").GetComponent<Text>().text = available_total + " / " + all_total;
			}
		}

		public void UIHand(int index)
		{
			this.UI_Hand.SetActive(true);
			if (SingletonComponent<WordSearchController>.Instance.Language == 2)
			{
				if (index == 3)
				{
					this.UI_Hand.GetComponent<Animator>().Play("hand_ke1");
				}
				else if (index == 2)
				{
					this.UI_Hand.GetComponent<Animator>().Play("hand_j2");
				}
				else if (index == 1)
				{
					this.UI_Hand.GetComponent<Animator>().Play("hand_j3");
				}
			}
			else if (index == 3)
			{
				this.UI_Hand.GetComponent<Animator>().Play("hand_ke1");
			}
			else if (index == 2)
			{
				this.UI_Hand.GetComponent<Animator>().Play("hand_ke2");
			}
			else if (index == 1)
			{
				this.UI_Hand.GetComponent<Animator>().Play("hand_ke3");
			}
		}

		public void BtnHint()
		{
			if (SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count > 0)
			{
				if (this.hint_flag)
				{
					UnityEngine.Debug.Log("이미본 힌트");
					SingletonComponent<Global>.Instance.MyShowToastMethod("You got hint");
					return;
				}
				this.hint_flag = true;
				WordListItem wordListItem = (!string.IsNullOrEmpty(this.currentSequentialWord)) ? this.wordListItems[this.currentSequentialWord] : null;
				wordListItem.wordText.text = this.currentSequentialWord;
				if (SingletonComponent<WordSearchController>.Instance.Language == 2)
				{
					this.UI_Pop_Hint.transform.Find("Inbox").Find("Text").GetComponent<Text>().font = this.font_jpn;
				}
				else
				{
					this.UI_Pop_Hint.transform.Find("Inbox").Find("Text").GetComponent<Text>().font = this.font_other;
				}
				this.UI_Pop_Hint.transform.Find("Inbox").Find("Text").GetComponent<Text>().text = this.currentSequentialWord;
				this.UI_Pop_Hint.transform.localPosition = new Vector3(-190f, 411f, 0f);
				this.UI_Pop_Hint.transform.localScale = Vector3.zero;
				this.UI_Pop_Hint.transform.DOLocalMove(new Vector3(0f, 250f, 0f), 0.5f, false).SetEase(Ease.OutBack);
				this.UI_Pop_Hint.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
				this.UI_Pop_Hint.transform.DOLocalMove(new Vector3(-160f, 309f, 0f), 0.5f, false).SetEase(Ease.InBack).SetDelay(1.5f);
				this.UI_Pop_Hint.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetDelay(1.5f);
				SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count--;
				this.UI_Hint_Counter.text = SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count.ToString();
				if (SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count == 0 && !SingletonComponent<WordSearchController>.Instance.Quiz_Hint_AD_Flag)
				{
					this.UI_Hint_Counter.text = "AD";
				}
			}
			else
			{
				if (SingletonComponent<WordSearchController>.Instance.Quiz_Hint_AD_Flag)
				{
					if (SingletonComponent<WordSearchController>.Instance.Language == 1)
					{
						SingletonComponent<Global>.Instance.MyShowToastMethod("힌트가 모두 소진 되었습니다.");
					}
					else if (SingletonComponent<WordSearchController>.Instance.Language == 2)
					{
						SingletonComponent<Global>.Instance.MyShowToastMethod("これ以上ヒントが使えません。");
					}
					else
					{
						SingletonComponent<Global>.Instance.MyShowToastMethod("There is no hints");
					}
					return;
				}
				SingletonComponent<UIScreenController>.Instance.PopupOpen(10);
			}
		}

		public void MoreHint()
		{
			SingletonComponent<WordSearchController>.Instance.Quiz_Hint_AD_Flag = true;
			SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count = 3;
			this.UI_Hint_Counter.text = SingletonComponent<WordSearchController>.Instance.Quiz_Hint_Count.ToString();
		}
	}
}
