using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WordSearch
{
	public class CharacterGrid : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEventSystemHandler
	{
		private enum HighlightType
		{
			Random,
			Range,
			Custom
		}

		[SerializeField]
		private RuntimeAnimatorController rac;

		[SerializeField]
		private float maxCellSize;

		[SerializeField]
		private Vector2 spacing;

		[SerializeField]
		private RectOffset padding;

		[SerializeField]
		private Font letterFont;

		[SerializeField]
		private Font letterFont_JPN;

		[SerializeField]
		private int letterFontSize;

		[SerializeField]
		private Color letterColor;

		[SerializeField]
		private Color letterSelectColor;

		[SerializeField]
		private Vector2 letterOffsetInCell;

		[SerializeField]
		private Sprite highlightSprite;

		[SerializeField]
		private float highlightExtraSize;

		[SerializeField]
		private float highlightAlpha;

		[SerializeField]
		private CharacterGrid.HighlightType highlightType;

		[Range(0f, 255f), SerializeField]
		private int redMin;

		[Range(0f, 255f), SerializeField]
		private int redMax = 255;

		[Range(0f, 255f), SerializeField]
		private int greenMin;

		[Range(0f, 255f), SerializeField]
		private int greenMax = 255;

		[Range(0f, 255f), SerializeField]
		private int blueMin;

		[Range(0f, 255f), SerializeField]
		private int blueMax = 255;

		[SerializeField]
		private List<Color> customColors;

		private Board currentBoard;

		private RectTransform gridContainer;

		private RectTransform gridOverlayContainer;

		private ObjectPool characterPool;

		private List<List<CharacterGridItem>> characterItems;

		private List<Image> highlights;

		private float currentScale;

		private float currentCellSize;

		private Image selectingHighlight;

		private bool isSelecting;

		private CharacterGridItem startCharacter;

		private CharacterGridItem lastEndCharacter;

		private int customColors_number;

		private static Tween.OnTweenFinished __f__am_cache0;

		private Vector2 ScaledSpacing
		{
			get
			{
				return this.spacing * this.currentScale;
			}
		}

		private float ScaledHighlighExtraSize
		{
			get
			{
				return this.highlightExtraSize * this.currentScale;
			}
		}

		private Vector2 ScaledLetterOffsetInCell
		{
			get
			{
				return this.letterOffsetInCell * this.currentScale;
			}
		}

		private float CellFullWidth
		{
			get
			{
				return this.currentCellSize + this.ScaledSpacing.x;
			}
		}

		private float CellFullHeight
		{
			get
			{
				return this.currentCellSize + this.ScaledSpacing.y;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.BoardActive)
			{
				CharacterGridItem characterItemAtPosition = this.GetCharacterItemAtPosition(eventData.position);
				if (characterItemAtPosition != null)
				{
					this.isSelecting = true;
					this.startCharacter = characterItemAtPosition;
					this.lastEndCharacter = characterItemAtPosition;
					this.AssignHighlighColor(this.selectingHighlight);
					this.selectingHighlight.gameObject.SetActive(true);
					this.UpdateSelectingHighlight(eventData.position);
				}
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.BoardActive)
			{
				this.UpdateSelectingHighlight(eventData.position);
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.startCharacter != null && this.lastEndCharacter != null && SingletonComponent<WordSearchController>.Instance.ActiveBoardState == WordSearchController.BoardState.BoardActive)
			{
				Position start = new Position(this.startCharacter.Row, this.startCharacter.Col);
				Position end = new Position(this.lastEndCharacter.Row, this.lastEndCharacter.Col);
				string text = SingletonComponent<WordSearchController>.Instance.OnWordSelected(start, end);
				if (!string.IsNullOrEmpty(text))
				{
					this.wordFound(this.startCharacter, this.lastEndCharacter);
					this.HighlightWord(start, end, true);
					Vector2 anchoredPosition = (this.startCharacter.transform as RectTransform).anchoredPosition;
					Vector2 anchoredPosition2 = (this.lastEndCharacter.transform as RectTransform).anchoredPosition;
					Vector2 position = anchoredPosition2 + (anchoredPosition - anchoredPosition2) / 2f;
					Text text2 = this.CreateFloatingText(text, this.selectingHighlight.color, position);
					Color toValue = new Color(text2.color.r, text2.color.g, text2.color.b, 0f);
					Tween.PositionY(text2.transform, Tween.TweenStyle.Linear, position.y, position.y + 75f, 1000f, false, Tween.LoopType.None).SetUseRectTransform(true);
					Tween.Colour(text2, Tween.TweenStyle.Linear, text2.color, toValue, 1000f, Tween.LoopType.None).SetFinishCallback(delegate(GameObject tweenedObject)
					{
						UnityEngine.Object.Destroy(tweenedObject);
					});
				}
			}
			for (int i = 0; i < this.characterItems.Count; i++)
			{
				for (int j = 0; j < this.characterItems[i].Count; j++)
				{
					if (!this.characterItems[i][j].IsFound)
					{
						this.characterItems[i][j].characterText.color = this.letterColor;
					}
					else
					{
						this.characterItems[i][j].characterText.color = this.letterSelectColor;
					}
				}
			}
			this.isSelecting = false;
			this.startCharacter = null;
			this.selectingHighlight.gameObject.SetActive(false);
		}

		public void Initialize()
		{
			GameObject gameObject = new GameObject("grid_overlay_container", new Type[]
			{
				typeof(RectTransform)
			});
			this.gridOverlayContainer = gameObject.GetComponent<RectTransform>();
			this.gridOverlayContainer.SetParent(base.transform, false);
			this.gridOverlayContainer.anchoredPosition = Vector2.zero;
			this.gridOverlayContainer.anchorMin = Vector2.zero;
			this.gridOverlayContainer.anchorMax = Vector2.one;
			this.gridOverlayContainer.offsetMin = Vector2.zero;
			this.gridOverlayContainer.offsetMax = Vector2.zero;
			GameObject gameObject2 = new GameObject("grid_container", new Type[]
			{
				typeof(RectTransform),
				typeof(GridLayoutGroup),
				typeof(CanvasGroup)
			});
			this.gridContainer = gameObject2.GetComponent<RectTransform>();
			this.gridContainer.SetParent(base.transform, false);
			this.gridContainer.anchoredPosition = Vector2.zero;
			this.gridContainer.anchorMin = Vector2.zero;
			this.gridContainer.anchorMax = Vector2.one;
			this.gridContainer.offsetMin = Vector2.zero;
			this.gridContainer.offsetMax = Vector2.zero;
			CharacterGridItem characterGridItem = this.CreateCharacterGridItem();
			characterGridItem.name = "template_character_grid_item";
			characterGridItem.gameObject.SetActive(false);
			characterGridItem.transform.SetParent(base.transform, false);
			GameObject gameObject3 = new GameObject("character_pool");
			gameObject3.transform.SetParent(base.transform);
			gameObject3.SetActive(false);
			this.characterPool = new ObjectPool(characterGridItem.gameObject, 25, gameObject3.transform);
			this.characterItems = new List<List<CharacterGridItem>>();
			this.highlights = new List<Image>();
			this.selectingHighlight = this.CreateNewHighlight();
			this.selectingHighlight.gameObject.SetActive(false);
			this.customColors_number = UnityEngine.Random.Range(0, this.customColors.Count);
		}

		public void Setup(Board board)
		{
			this.Clear();
			this.currentCellSize = this.SetupGridContainer(board.rowSize, board.columnSize);
			this.currentScale = this.currentCellSize / this.maxCellSize;
			for (int i = 0; i < board.boardCharacters.Count; i++)
			{
				this.characterItems.Add(new List<CharacterGridItem>());
				for (int j = 0; j < board.boardCharacters[i].Count; j++)
				{
					CharacterGridItem component = this.characterPool.GetObject().GetComponent<CharacterGridItem>();
					component.Row = i;
					component.Col = j;
					component.gameObject.SetActive(true);
					component.transform.SetParent(this.gridContainer, false);
					component.characterText.text = board.boardCharacters[i][j].ToString().ToUpper();
					component.characterText.transform.localScale = new Vector3(this.currentScale, this.currentScale, 1f);
					(component.characterText.transform as RectTransform).anchoredPosition = this.ScaledLetterOffsetInCell;
					component.characterText.color = this.letterColor;
					component.IsFound = false;
					if (SingletonComponent<WordSearchController>.Instance.Language == 2)
					{
						component.characterText.font = this.letterFont_JPN;
					}
					else
					{
						component.characterText.font = this.letterFont;
					}
					this.characterItems[i].Add(component);
				}
			}
			this.currentBoard = board;
			Tween.CanvasGroupAlpha(this.gridContainer.GetComponent<CanvasGroup>(), Tween.TweenStyle.EaseOut, 0f, 1f, 400f);
		}

		public void HighlightWord(Position start, Position end, bool useSelectedColour)
		{
			Image image = this.CreateNewHighlight();
			this.highlights.Add(image);
			this.PositionHighlight(image, this.characterItems[start.row][start.col], this.characterItems[end.row][end.col]);
			if (useSelectedColour && this.selectingHighlight != null)
			{
				image.color = this.selectingHighlight.color;
			}
		}

		public void Clear()
		{
			this.characterPool.ReturnAllObjectsToPool();
			this.characterItems.Clear();
			for (int i = 0; i < this.highlights.Count; i++)
			{
				UnityEngine.Object.Destroy(this.highlights[i].gameObject);
			}
			this.highlights.Clear();
			this.gridContainer.GetComponent<CanvasGroup>().alpha = 0f;
			this.customColors_number = UnityEngine.Random.Range(0, this.customColors.Count);
		}

		private void UpdateSelectingHighlight(Vector2 screenPosition)
		{
			if (this.isSelecting)
			{
				CharacterGridItem characterGridItem = this.GetCharacterItemAtPosition(screenPosition);
				if (characterGridItem != null)
				{
					int row = this.startCharacter.Row;
					int col = this.startCharacter.Col;
					int row2 = characterGridItem.Row;
					int col2 = characterGridItem.Col;
					int num = row2 - row;
					int num2 = col2 - col;
					if (num != num2 && num != 0 && num2 != 0)
					{
						if (Mathf.Abs(num2) > Mathf.Abs(num))
						{
							if (Mathf.Abs(num2) - Mathf.Abs(num) > Mathf.Abs(num))
							{
								num = 0;
							}
							else
							{
								num2 = this.AssignKeepSign(num2, num);
							}
						}
						else if (Mathf.Abs(num) - Mathf.Abs(num2) > Mathf.Abs(num2))
						{
							num2 = 0;
						}
						else
						{
							num2 = this.AssignKeepSign(num2, num);
						}
						if (col + num2 < 0)
						{
							num2 -= col + num2;
							num = this.AssignKeepSign(num, Mathf.Abs(num2));
						}
						else if (col + num2 >= this.currentBoard.columnSize)
						{
							num2 -= col + num2 - this.currentBoard.columnSize + 1;
							num = this.AssignKeepSign(num, Mathf.Abs(num2));
						}
						characterGridItem = this.characterItems[row + num][col + num2];
					}
				}
				else
				{
					characterGridItem = this.lastEndCharacter;
				}
				this.PositionHighlight(this.selectingHighlight, this.startCharacter, characterGridItem);
				this.lastEndCharacter = characterGridItem;
				this.lastEndCharacter.GetComponent<Animator>().Play("word_ani");
			}
		}

		private void PositionHighlight(Image highlight, CharacterGridItem start, CharacterGridItem end)
		{
			RectTransform rectTransform = highlight.transform as RectTransform;
			Vector2 anchoredPosition = (start.transform as RectTransform).anchoredPosition;
			Vector2 anchoredPosition2 = (end.transform as RectTransform).anchoredPosition;
			float num = Vector2.Distance(anchoredPosition, anchoredPosition2);
			float num2 = this.currentCellSize + num + this.ScaledHighlighExtraSize;
			float num3 = this.currentCellSize + this.ScaledHighlighExtraSize;
			float num4 = num3 / highlight.sprite.rect.height;
			rectTransform.anchoredPosition = anchoredPosition + new Vector2(0f, -2f) + (anchoredPosition2 - anchoredPosition) / 2f;
			rectTransform.localScale = new Vector3(num4, num4);
			rectTransform.sizeDelta = new Vector2(num2 / num4 + 5f, highlight.sprite.rect.height - 10f);
			float num5 = Vector2.Angle(new Vector2(1f, 0f), anchoredPosition2 - anchoredPosition);
			if (anchoredPosition.y > anchoredPosition2.y)
			{
				num5 = -num5;
			}
			rectTransform.eulerAngles = new Vector3(0f, 0f, num5);
			this.wordChangeColor(start, end);
		}

		private CharacterGridItem GetCharacterItemAtPosition(Vector2 screenPoint)
		{
			for (int i = 0; i < this.characterItems.Count; i++)
			{
				for (int j = 0; j < this.characterItems[i].Count; j++)
				{
					Vector2 vector;
					RectTransformUtility.ScreenPointToLocalPointInRectangle(this.characterItems[i][j].transform as RectTransform, screenPoint, null, out vector);
					vector.x += this.CellFullWidth / 2f;
					vector.y += this.CellFullHeight / 2f;
					if (vector.x >= 0f && vector.y >= 0f && vector.x < this.CellFullWidth && vector.y < this.CellFullHeight)
					{
						return this.characterItems[i][j];
					}
				}
			}
			return null;
		}

		private float SetupGridContainer(int rows, int columns)
		{
			GridLayoutGroup component = this.gridContainer.GetComponent<GridLayoutGroup>();
			RectOffset rectOffset = new RectOffset(20, 20, 20, 20);
			float num = (this.gridContainer.rect.width - this.ScaledSpacing.x * (float)(columns - 1) - (float)rectOffset.horizontal) / (float)columns;
			float num2 = (this.gridContainer.rect.height - this.ScaledSpacing.y * (float)(rows - 1) - (float)rectOffset.vertical) / (float)rows;
			float num3 = Mathf.Min(new float[]
			{
				num,
				num2,
				this.maxCellSize
			});
			component.cellSize = new Vector2(num3, num3);
			component.spacing = this.ScaledSpacing;
			component.padding = rectOffset;
			component.childAlignment = TextAnchor.MiddleCenter;
			component.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			component.constraintCount = columns;
			return num3;
		}

		private CharacterGridItem CreateCharacterGridItem()
		{
			GameObject gameObject = new GameObject("character_grid_item", new Type[]
			{
				typeof(RectTransform)
			});
			GameObject gameObject2 = new GameObject("character_text", new Type[]
			{
				typeof(RectTransform)
			});
			Image image = gameObject.AddComponent<Image>();
			image.color = new Color(0f, 0f, 0f, 0f);
			gameObject2.transform.SetParent(gameObject.transform);
			(gameObject2.transform as RectTransform).anchoredPosition = this.letterOffsetInCell;
			Text text = gameObject2.AddComponent<Text>();
			text.font = this.letterFont;
			text.fontSize = this.letterFontSize;
			text.color = this.letterColor;
			if (SingletonComponent<WordSearchController>.Instance.Language == 2)
			{
				text.font = this.letterFont_JPN;
			}
			ContentSizeFitter contentSizeFitter = gameObject2.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			CharacterGridItem characterGridItem = gameObject.AddComponent<CharacterGridItem>();
			characterGridItem.characterText = text;
			characterGridItem.gameObject.AddComponent<Animator>();
			characterGridItem.gameObject.GetComponent<Animator>().runtimeAnimatorController = this.rac;
			return characterGridItem;
		}

		private Image CreateNewHighlight()
		{
			GameObject gameObject = new GameObject("highlight");
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			Image image = gameObject.AddComponent<Image>();
			rectTransform.anchorMin = new Vector2(0f, 1f);
			rectTransform.anchorMax = new Vector2(0f, 1f);
			rectTransform.SetParent(this.gridOverlayContainer, false);
			image.type = Image.Type.Sliced;
			image.fillCenter = true;
			image.sprite = this.highlightSprite;
			this.AssignHighlighColor(image);
			if (this.selectingHighlight != null)
			{
				this.selectingHighlight.transform.SetAsLastSibling();
			}
			return image;
		}

		private void AssignHighlighColor(Image highlight)
		{
			Color color = Color.white;
			CharacterGrid.HighlightType highlightType = this.highlightType;
			if (highlightType != CharacterGrid.HighlightType.Random)
			{
				if (highlightType != CharacterGrid.HighlightType.Range)
				{
					if (highlightType == CharacterGrid.HighlightType.Custom)
					{
						if (this.customColors.Count > 0)
						{
							if (this.customColors_number < this.customColors.Count - 1)
							{
								this.customColors_number++;
							}
							else
							{
								this.customColors_number = 0;
							}
							color = this.customColors[this.customColors_number];
						}
						else
						{
							UnityEngine.Debug.LogError("[CharacterGrid] Custom Colors is empty.");
						}
					}
				}
				else
				{
					int num = UnityEngine.Random.Range(this.redMin, this.redMax + 1);
					int num2 = UnityEngine.Random.Range(this.greenMin, this.greenMax + 1);
					int num3 = UnityEngine.Random.Range(this.blueMin, this.blueMax + 1);
					color = new Color((float)num / 255f, (float)num2 / 255f, (float)num3 / 255f);
				}
			}
			else
			{
				color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
			}
			color.a = this.highlightAlpha;
			highlight.color = color;
		}

		private Text CreateFloatingText(string text, Color color, Vector2 position)
		{
			GameObject gameObject = new GameObject("found_word_floating_text");
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			Text text2 = gameObject.AddComponent<Text>();
			text2.text = text;
			text2.font = this.letterFont;
			text2.fontSize = this.letterFontSize;
			text2.color = color;
			if (SingletonComponent<WordSearchController>.Instance.Language == 2)
			{
				text2.font = this.letterFont_JPN;
			}
			rectTransform.anchoredPosition = position;
			rectTransform.localScale = new Vector3(this.currentScale, this.currentScale, 1f);
			rectTransform.anchorMin = new Vector2(0f, 1f);
			rectTransform.anchorMax = new Vector2(0f, 1f);
			rectTransform.SetParent(this.gridOverlayContainer, false);
			ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			return text2;
		}

		private int AssignKeepSign(int a, int b)
		{
			int num = (a >= 0) ? 1 : (-1);
			a = Mathf.Abs(b);
			a *= num;
			return a;
		}

		private void wordChangeColor(CharacterGridItem start, CharacterGridItem end)
		{
			int num = (start.Row != end.Row) ? ((start.Row >= end.Row) ? (-1) : 1) : 0;
			int num2 = (start.Col != end.Col) ? ((start.Col >= end.Col) ? (-1) : 1) : 0;
			int num3 = Mathf.Max(Mathf.Abs(start.Row - end.Row), Mathf.Abs(start.Col - end.Col));
			for (int i = 0; i < this.characterItems.Count; i++)
			{
				for (int j = 0; j < this.characterItems[i].Count; j++)
				{
					if (!this.characterItems[i][j].IsFound)
					{
						this.characterItems[i][j].characterText.color = this.letterColor;
					}
					else
					{
						this.characterItems[i][j].characterText.color = this.letterSelectColor;
					}
				}
			}
			for (int k = 0; k <= num3; k++)
			{
				CharacterGridItem characterGridItem = this.characterItems[start.Row + k * num][start.Col + k * num2];
				characterGridItem.characterText.color = this.letterSelectColor;
			}
		}

		private void wordFound(CharacterGridItem start, CharacterGridItem end)
		{
			int num = (start.Row != end.Row) ? ((start.Row >= end.Row) ? (-1) : 1) : 0;
			int num2 = (start.Col != end.Col) ? ((start.Col >= end.Col) ? (-1) : 1) : 0;
			int num3 = Mathf.Max(Mathf.Abs(start.Row - end.Row), Mathf.Abs(start.Col - end.Col));
			for (int i = 0; i <= num3; i++)
			{
				this.characterItems[start.Row + i * num][start.Col + i * num2].IsFound = true;
			}
		}
	}
}
