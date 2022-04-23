using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public class BoardCreatorWebGl : MonoBehaviour
	{
		private const char BlankChar = ' ';

		private const int Tries = 100;

		public BoardConfig boardConfig;

		public Action<Board> onBoardCompleted;

		private Board board;

		private System.Random random;

		private Stopwatch timer;

		private Board workingBoard;

		private int wordIndex;

		private bool forceStop;

		private static Comparison<string> __f__am_cache0;

		private void Start()
		{
			this.boardConfig.words.Sort((string x, string y) => y.Length - x.Length);
			this.random = new System.Random();
			this.timer = new Stopwatch();
			this.timer.Start();
		}

		private void Update()
		{
			if (this.forceStop || (this.workingBoard == null && this.timer.ElapsedMilliseconds >= this.boardConfig.algoTimeoutInMilliseconds))
			{
				this.timer.Stop();
				for (int i = 0; i < this.boardConfig.rowSize; i++)
				{
					for (int j = 0; j < this.boardConfig.columnSize; j++)
					{
						if (this.board.boardCharacters[i][j] == ' ')
						{
							string randomCharacters = this.boardConfig.randomCharacters;
							bool flag = false;
							while (randomCharacters.Length > 0)
							{
								int num = this.random.Next(0, randomCharacters.Length);
								char value = randomCharacters[num];
								randomCharacters.Remove(num);
								this.board.boardCharacters[i][j] = value;
								if (!BoardCreatorWebGl.CheckForFilterWord(this.boardConfig, this.board, new Position(i, j)))
								{
									flag = true;
									break;
								}
								this.board.boardCharacters[i][j] = ' ';
							}
							if (!flag)
							{
								UnityEngine.Debug.LogError("[WordSearch] A random character could not be placed because all letters in the randomCharacters list create a word in filterWords.");
							}
						}
					}
				}
				this.onBoardCompleted(this.board);
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			int k = 100;
			while (k > 0)
			{
				k--;
				if (this.workingBoard == null)
				{
					this.workingBoard = new Board();
					this.workingBoard.rowSize = this.boardConfig.rowSize;
					this.workingBoard.columnSize = this.boardConfig.columnSize;
					this.workingBoard.usedWords = new List<string>();
					this.workingBoard.boardCharacters = new List<List<char>>();
					this.workingBoard.wordPlacements = new List<WordPlacement>();
					for (int l = 0; l < this.boardConfig.rowSize; l++)
					{
						this.workingBoard.boardCharacters.Add(new List<char>());
						for (int m = 0; m < this.boardConfig.columnSize; m++)
						{
							this.workingBoard.boardCharacters[l].Add(' ');
						}
					}
					this.wordIndex = 0;
				}
				string text = this.boardConfig.words[this.wordIndex];
				if (string.IsNullOrEmpty(text))
				{
					UnityEngine.Debug.LogWarning("[WordSearch] One of the words what an empty string.");
					this.NextWord();
				}
				else
				{
					this.PlaceWordOnBoard(text);
				}
			}
		}

		public static void CreateBoard(BoardConfig boardConfig, Action<Board> onBoardCompleted)
		{
			GameObject gameObject = new GameObject("BoardCreatorWebGl");
			BoardCreatorWebGl boardCreatorWebGl = gameObject.AddComponent<BoardCreatorWebGl>();
			boardCreatorWebGl.boardConfig = boardConfig;
			boardCreatorWebGl.onBoardCompleted = onBoardCompleted;
		}

		private void PlaceWordOnBoard(string word)
		{
			List<Position> possibleBoardIndices = BoardCreatorWebGl.GetPossibleBoardIndices(this.workingBoard.boardCharacters, word[0]);
			while (possibleBoardIndices.Count > 0)
			{
				Position position = possibleBoardIndices[this.random.Next(0, possibleBoardIndices.Count)];
				possibleBoardIndices.Remove(position);
				List<int[]> list = new List<int[]>();
				list.Add(new int[]
				{
					-1,
					-1
				});
				List<int[]> arg_63_0 = list;
				int[] expr_5F = new int[2];
				expr_5F[0] = -1;
				arg_63_0.Add(expr_5F);
				list.Add(new int[]
				{
					-1,
					1
				});
				list.Add(new int[]
				{
					0,
					1
				});
				list.Add(new int[]
				{
					1,
					1
				});
				List<int[]> arg_AB_0 = list;
				int[] expr_A7 = new int[2];
				expr_A7[0] = 1;
				arg_AB_0.Add(expr_A7);
				list.Add(new int[]
				{
					1,
					-1
				});
				list.Add(new int[]
				{
					0,
					-1
				});
				List<int[]> list2 = list;
				while (list2.Count > 0)
				{
					int index = this.random.Next(0, list2.Count);
					int[] array = list2[index];
					list2.RemoveAt(index);
					int num = position.row + array[0] * (word.Length - 1);
					int num2 = position.col + array[1] * (word.Length - 1);
					if (num >= 0 && num < this.workingBoard.rowSize && num2 >= 0 && num2 < this.workingBoard.columnSize)
					{
						bool flag = true;
						for (int i = 0; i < word.Length; i++)
						{
							int index2 = position.row + array[0] * i;
							int index3 = position.col + array[1] * i;
							char c = this.workingBoard.boardCharacters[index2][index3];
							if (c != ' ' && c != word[i])
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							List<Position> list3 = new List<Position>();
							for (int j = 0; j < word.Length; j++)
							{
								int num3 = position.row + array[0] * j;
								int num4 = position.col + array[1] * j;
								char c2 = this.workingBoard.boardCharacters[num3][num4];
								if (c2 == ' ')
								{
									this.workingBoard.boardCharacters[num3][num4] = word[j];
									list3.Add(new Position(num3, num4));
								}
							}
							bool flag2 = false;
							for (int k = 0; k < word.Length; k++)
							{
								int row = position.row + array[0] * k;
								int col = position.col + array[1] * k;
								if (BoardCreatorWebGl.CheckForFilterWord(this.boardConfig, this.workingBoard, new Position(row, col)))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								WordPlacement wordPlacement = new WordPlacement();
								wordPlacement.word = word;
								wordPlacement.startingPosition = position;
								wordPlacement.verticalDirection = array[0];
								wordPlacement.horizontalDirection = array[1];
								this.workingBoard.wordPlacements.Add(wordPlacement);
								this.workingBoard.usedWords.Add(word);
								this.NextWord();
								return;
							}
							for (int l = 0; l < list3.Count; l++)
							{
								this.workingBoard.boardCharacters[list3[l].row][list3[l].col] = ' ';
							}
						}
					}
				}
			}
			this.WorkingBoardFinished();
		}

		private static List<Position> GetPossibleBoardIndices(List<List<char>> boardCharacters, char startingChar)
		{
			List<Position> list = new List<Position>();
			for (int i = 0; i < boardCharacters.Count; i++)
			{
				for (int j = 0; j < boardCharacters[i].Count; j++)
				{
					if (boardCharacters[i][j] == ' ' || boardCharacters[i][j] == startingChar)
					{
						list.Add(new Position(i, j));
					}
				}
			}
			return list;
		}

		private static bool CheckForFilterWord(BoardConfig boardConfig, Board board, Position position)
		{
			char c = board.boardCharacters[position.row][position.col];
			if (c == ' ')
			{
				return false;
			}
			List<int[]> list = new List<int[]>();
			list.Add(new int[]
			{
				-1,
				-1
			});
			List<int[]> arg_4C_0 = list;
			int[] expr_48 = new int[2];
			expr_48[0] = -1;
			arg_4C_0.Add(expr_48);
			list.Add(new int[]
			{
				-1,
				1
			});
			list.Add(new int[]
			{
				0,
				1
			});
			list.Add(new int[]
			{
				1,
				1
			});
			List<int[]> arg_94_0 = list;
			int[] expr_90 = new int[2];
			expr_90[0] = 1;
			arg_94_0.Add(expr_90);
			list.Add(new int[]
			{
				1,
				-1
			});
			list.Add(new int[]
			{
				0,
				-1
			});
			List<int[]> list2 = list;
			for (int i = 0; i < boardConfig.filterWords.Count; i++)
			{
				string text = boardConfig.filterWords[i];
				int num = -1;
				while (true)
				{
					num = text.IndexOf(c, num + 1);
					if (num == -1)
					{
						break;
					}
					foreach (int[] current in list2)
					{
						int num2 = position.row - current[0] * num;
						int num3 = position.col - current[1] * num;
						bool flag = true;
						for (int j = 0; j < text.Length; j++)
						{
							int num4 = num2 + current[0] * j;
							int num5 = num3 + current[1] * j;
							if (num4 < 0 || num4 >= board.rowSize || num5 < 0 || num5 >= board.columnSize || board.boardCharacters[num4][num5] != text[j])
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		private void NextWord()
		{
			this.wordIndex++;
			if (this.wordIndex >= this.boardConfig.words.Count)
			{
				this.WorkingBoardFinished();
			}
		}

		private void WorkingBoardFinished()
		{
			if (this.board == null || this.board.usedWords.Count < this.workingBoard.usedWords.Count)
			{
				this.board = this.workingBoard;
				if (this.board.usedWords.Count == this.boardConfig.words.Count)
				{
					this.forceStop = true;
				}
			}
			this.workingBoard = null;
		}
	}
}
