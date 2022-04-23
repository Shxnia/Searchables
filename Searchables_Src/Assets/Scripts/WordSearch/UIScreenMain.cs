using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class UIScreenMain : UIScreen
	{
		private sealed class _WaitThenAnimateContinueItem_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float delay;

			internal UIScreenMain _this;

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

			public _WaitThenAnimateContinueItem_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(this.delay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					Tween.CanvasGroupAlpha(this._this.continueItemCanvasGroup, Tween.TweenStyle.EaseOut, 0f, 1f, 400f);
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
		private Transform difficultyButtonContainer;

		[SerializeField]
		private Transform modeButtonContainer;

		[SerializeField]
		private OptionButton optionButtonPrefab;

		[SerializeField]
		private RectTransform categoryListContainer;

		[SerializeField]
		private CategoryListItem categoryListItemPrefab;

		[SerializeField]
		private GameObject continueHeader;

		[SerializeField]
		private GameObject continueContent;

		[SerializeField]
		private CanvasGroup continueItemCanvasGroup;

		[SerializeField]
		private Image continueIconImage;

		[SerializeField]
		private Text continueCategoryText;

		[SerializeField]
		private Text continueWordsFoundText;

		[SerializeField]
		private Text continueTimeLeftText;

		private List<OptionButton> difficultyButtons;

		private List<OptionButton> modeButtons;

		private List<CategoryListItem> categoryListItems;

		public override void Initialize()
		{
			base.Initialize();
			this.difficultyButtons = new List<OptionButton>();
			this.modeButtons = new List<OptionButton>();
			this.categoryListItems = new List<CategoryListItem>();
			for (int i = 0; i < SingletonComponent<WordSearchController>.Instance.ModeInfos.Count; i++)
			{
				WordSearchController.ModeInfo modeInfo = SingletonComponent<WordSearchController>.Instance.ModeInfos[i];
				OptionButton optionButton = UnityEngine.Object.Instantiate<OptionButton>(this.optionButtonPrefab);
				optionButton.transform.SetParent(this.modeButtonContainer, false);
				optionButton.Setup(modeInfo.modeName, i, new Action<int>(this.OnModeClicked));
				this.modeButtons.Add(optionButton);
			}
			for (int j = 0; j < SingletonComponent<WordSearchController>.Instance.DifficultyInfos.Count; j++)
			{
				WordSearchController.DifficultyInfo difficultyInfo = SingletonComponent<WordSearchController>.Instance.DifficultyInfos[j];
				OptionButton optionButton2 = UnityEngine.Object.Instantiate<OptionButton>(this.optionButtonPrefab);
				optionButton2.transform.SetParent(this.difficultyButtonContainer, false);
				optionButton2.Setup(difficultyInfo.difficultyName, j, new Action<int>(this.OnDifficultyClicked));
				this.difficultyButtons.Add(optionButton2);
			}
			for (int k = 0; k < SingletonComponent<WordSearchController>.Instance.CategoryInfos.Count; k++)
			{
				WordSearchController.CategoryInfo categoryInfo = SingletonComponent<WordSearchController>.Instance.CategoryInfos[k];
				CategoryListItem categoryListItem = UnityEngine.Object.Instantiate<CategoryListItem>(this.categoryListItemPrefab);
				categoryListItem.transform.SetParent(this.categoryListContainer, false);
				categoryListItem.Setup(categoryInfo.categoryName_ENG, categoryInfo.categoryIcon, k, new Action<int>(this.OnCategoryListItemClicked));
				this.categoryListItems.Add(categoryListItem);
			}
			this.SetButtonsSelected(this.modeButtons, SingletonComponent<WordSearchController>.Instance.ModeInfos.IndexOf(SingletonComponent<WordSearchController>.Instance.SelectedMode));
			this.SetButtonsSelected(this.difficultyButtons, SingletonComponent<WordSearchController>.Instance.DifficultyInfos.IndexOf(SingletonComponent<WordSearchController>.Instance.SelectedDifficulty));
		}

		public override void OnShowing(string data)
		{
			base.OnShowing(data);
			this.categoryListContainer.anchoredPosition = new Vector2(this.categoryListContainer.anchoredPosition.x, 0f);
			for (int i = 0; i < this.categoryListItems.Count; i++)
			{
				this.categoryListItems[i].StartShowAnimation(0.2f + (float)i * 0.05f);
			}
			for (int j = 0; j < this.difficultyButtons.Count; j++)
			{
				this.difficultyButtons[j].StartShowAnimation(0.2f + (float)j * 0.05f);
			}
			for (int k = 0; k < this.modeButtons.Count; k++)
			{
				this.modeButtons[k].StartShowAnimation(0.2f + (float)k * 0.05f);
			}
			bool flag = SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.BoardActive;
			this.continueHeader.SetActive(flag);
			this.continueContent.SetActive(flag);
			if (flag)
			{
				this.continueIconImage.sprite = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryIcon;
				this.continueCategoryText.text = SingletonComponent<WordSearchController>.Instance.ActiveCategory.categoryName_ENG;
				this.continueWordsFoundText.text = string.Format("Words Found: {0}/{1}", SingletonComponent<WordSearchController>.Instance.FoundWords.Count, SingletonComponent<WordSearchController>.Instance.ActiveBoard.usedWords.Count);
				this.continueTimeLeftText.gameObject.SetActive(SingletonComponent<WordSearchController>.Instance.IsTimedMode);
				if (SingletonComponent<WordSearchController>.Instance.IsTimedMode)
				{
					int num = Mathf.FloorToInt((float)SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds / 60f);
					int num2 = SingletonComponent<WordSearchController>.Instance.TimeLeftInSeconds % 60;
					this.continueTimeLeftText.text = string.Format("Time Left: {0}:{1}{2}", num, (num2 >= 10) ? string.Empty : "0", num2);
				}
				this.continueItemCanvasGroup.alpha = 0f;
				base.StartCoroutine(this.WaitThenAnimateContinueItem(0.2f));
			}
		}

		public void OnContinueButtonClicked()
		{
			SingletonComponent<UIScreenController>.Instance.Show("game", false, true, false, null, string.Empty);
		}

		private void OnCategoryListItemClicked(int index)
		{
			SingletonComponent<UIScreenController>.Instance.Show("game", false, true, false, null, string.Empty);
		}

		private void OnModeClicked(int index)
		{
			this.SetButtonsSelected(this.modeButtons, index);
			SingletonComponent<WordSearchController>.Instance.SetSelectedMode(index);
		}

		private void OnDifficultyClicked(int index)
		{
			this.SetButtonsSelected(this.difficultyButtons, index);
			SingletonComponent<WordSearchController>.Instance.SetSelectedDifficulty(index);
		}

		private void SetButtonsSelected(List<OptionButton> optionButtons, int selectedButtonIndex)
		{
			for (int i = 0; i < optionButtons.Count; i++)
			{
				optionButtons[i].IsSelected = (i == selectedButtonIndex);
			}
		}

		private IEnumerator WaitThenAnimateContinueItem(float delay)
		{
			UIScreenMain._WaitThenAnimateContinueItem_c__Iterator0 _WaitThenAnimateContinueItem_c__Iterator = new UIScreenMain._WaitThenAnimateContinueItem_c__Iterator0();
			_WaitThenAnimateContinueItem_c__Iterator.delay = delay;
			_WaitThenAnimateContinueItem_c__Iterator._this = this;
			return _WaitThenAnimateContinueItem_c__Iterator;
		}
	}
}
