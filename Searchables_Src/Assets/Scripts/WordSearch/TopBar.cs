using System;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class TopBar : SingletonComponent<TopBar>
	{
		[SerializeField]
		private RectTransform backButton;

		[SerializeField]
		private RectTransform mainScreenContent;

		[SerializeField]
		private RectTransform gameScreenContent;

		[SerializeField]
		private Image categoryIconImage;

		[SerializeField]
		private Text categoryNameText;

		[SerializeField]
		private Text wordsFoundText;

		[SerializeField]
		private Text timeLeftText;

		private void Start()
		{
			UIScreenController expr_05 = SingletonComponent<UIScreenController>.Instance;
			expr_05.OnSwitchingScreens = (Action<string, string, bool>)Delegate.Combine(expr_05.OnSwitchingScreens, new Action<string, string, bool>(this.OnSwitchingScreens));
			WordSearchController expr_2B = SingletonComponent<WordSearchController>.Instance;
			expr_2B.OnBoardStateChanged = (Action)Delegate.Combine(expr_2B.OnBoardStateChanged, new Action(this.UpdateGameUI));
			WordSearchController expr_51 = SingletonComponent<WordSearchController>.Instance;
			expr_51.OnWordFound = (Action)Delegate.Combine(expr_51.OnWordFound, new Action(this.UpdateGameUI));
			this.UpdateGameUI();
		}

		private void Update()
		{
			if (SingletonComponent<WordSearchController>.Instance.IsTimedMode)
			{
				int num = Mathf.FloorToInt((float)SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds / 60f);
				int num2 = SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds % 60;
				this.timeLeftText.text = string.Format("{0}:{1}{2}", num, (num2 >= 10) ? string.Empty : "0", num2);
			}
		}

		public void OnBackButtonClicked()
		{
			SingletonComponent<UIScreenController>.Instance.Show("main", true, true, false, null, string.Empty);
		}

		private void OnSwitchingScreens(string fromScreenId, string toScreenId, bool overlay)
		{
			this.mainScreenContent.gameObject.SetActive(true);
			this.gameScreenContent.gameObject.SetActive(true);
			float animationSpeed = SingletonComponent<UIScreenController>.Instance.AnimationSpeed;
			float num = 48f;
			float num2 = -150f;
			float num3 = 300f;
			float num4 = 1200f;
			if (fromScreenId == "main" && toScreenId == "game")
			{
				Tween.PositionX(this.backButton, Tween.TweenStyle.EaseOut, num2, num, animationSpeed, false, Tween.LoopType.None).SetUseRectTransform(true);
				Tween.PositionY(this.mainScreenContent, Tween.TweenStyle.EaseOut, 0f, num3, animationSpeed, false, Tween.LoopType.None).SetUseRectTransform(true);
				Tween.PositionX(this.gameScreenContent, Tween.TweenStyle.EaseOut, num4, 0f, animationSpeed, false, Tween.LoopType.None).SetUseRectTransform(true);
			}
			else if (fromScreenId == "game" && toScreenId == "main")
			{
				Tween.PositionX(this.backButton, Tween.TweenStyle.EaseOut, num, num2, animationSpeed, false, Tween.LoopType.None).SetUseRectTransform(true);
				Tween.PositionY(this.mainScreenContent, Tween.TweenStyle.EaseOut, num3, 0f, animationSpeed, false, Tween.LoopType.None).SetUseRectTransform(true);
				Tween.PositionX(this.gameScreenContent, Tween.TweenStyle.EaseOut, 0f, num4, animationSpeed, false, Tween.LoopType.None).SetUseRectTransform(true);
			}
			else
			{
				this.backButton.anchoredPosition = new Vector2(num2, 0f);
				this.mainScreenContent.anchoredPosition = Vector2.zero;
				this.gameScreenContent.anchoredPosition = new Vector2(num4, 0f);
			}
		}

		private void UpdateGameUI()
		{
			this.wordsFoundText.text = ((SingletonComponent<WordSearchController>.Instance.ActiveBoardState != WordSearchController.BoardState.Generating) ? string.Empty : "Loading Board");
			if (SingletonComponent<WordSearchController>.Instance.ActiveCategory != null)
			{
				this.categoryIconImage.sprite = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryIcon;
				this.categoryNameText.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_ENG;
			}
			if (SingletonComponent<WordSearchController>.Instance.ActiveBoard != null)
			{
				this.wordsFoundText.text = string.Format("Found: {0}/{1}", SingletonComponent<WordSearchController>.Instance.FoundWords.Count, SingletonComponent<WordSearchController>.Instance.ActiveBoard.usedWords.Count);
			}
			this.timeLeftText.gameObject.SetActive(SingletonComponent<WordSearchController>.Instance.IsTimedMode);
		}
	}
}
