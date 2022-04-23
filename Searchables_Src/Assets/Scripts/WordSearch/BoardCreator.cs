using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public static class BoardCreator
	{
		private sealed class _CreateBoard_c__AnonStorey0
		{
			internal BoardConfig boardConfig;

			internal Action<Board> OnBoardCompleted;

			private static Comparison<string> __f__am_cache0;

			internal object __m__0(object inData)
			{
				if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName != "Quiz")
				{
					this.boardConfig.words.Sort((string x, string y) => y.Length - x.Length);
				}
				System.Random random = new System.Random();
				Stopwatch stopwatch = new Stopwatch();
				Board board = null;
				stopwatch.Start();
				while (stopwatch.ElapsedMilliseconds < this.boardConfig.algoTimeoutInMilliseconds)
				{
					Board board2 = new Board();
					board2.rowSize = this.boardConfig.rowSize;
					board2.columnSize = this.boardConfig.columnSize;
					board2.usedWords = new List<string>();
					board2.usedQuestions = new List<string>();
					board2.boardCharacters = new List<List<char>>();
					board2.wordPlacements = new List<WordPlacement>();
					for (int i = 0; i < this.boardConfig.rowSize; i++)
					{
						board2.boardCharacters.Add(new List<char>());
						for (int j = 0; j < this.boardConfig.columnSize; j++)
						{
							board2.boardCharacters[i].Add(' ');
						}
					}
					BoardCreator.PlaceWordsOnBoard(this.boardConfig, random, board2, 0);
					if (board == null || board.usedWords.Count < board2.usedWords.Count)
					{
						board = board2;
						if (board.usedWords.Count == this.boardConfig.words.Count)
						{
							break;
						}
					}
				}
				stopwatch.Stop();
				for (int k = 0; k < this.boardConfig.rowSize; k++)
				{
					for (int l = 0; l < this.boardConfig.columnSize; l++)
					{
						if (board.boardCharacters[k][l] == ' ')
						{
							string randomCharacters = this.boardConfig.randomCharacters;
							bool flag = false;
							while (randomCharacters.Length > 0)
							{
								int num = random.Next(0, randomCharacters.Length);
								char value = randomCharacters[num];
								randomCharacters.Remove(num);
								board.boardCharacters[k][l] = value;
								if (!BoardCreator.CheckForFilterWord(this.boardConfig, board, new Position(k, l)))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								UnityEngine.Debug.LogError("[WordSearch] A random character could not be placed because all letters in the randomCharacters list create a word in filterWords.");
								return null;
							}
						}
					}
				}
				return board;
			}

			internal void __m__1(object obj)
			{
				this.OnBoardCompleted(obj as Board);
			}

			private static int __m__2(string x, string y)
			{
				return y.Length - x.Length;
			}
		}

		private sealed class _TutorialCreateBoard_c__AnonStorey1
		{
			internal BoardConfig boardConfig;

			internal Action<Board> OnBoardCompleted;

			internal object __m__0(object inData)
			{
				System.Random random = new System.Random();
				Stopwatch stopwatch = new Stopwatch();
				Board board = null;
				stopwatch.Start();
				while (stopwatch.ElapsedMilliseconds < this.boardConfig.algoTimeoutInMilliseconds)
				{
					Board board2 = new Board();
					board2.rowSize = this.boardConfig.rowSize;
					board2.columnSize = this.boardConfig.columnSize;
					board2.usedWords = new List<string>();
					board2.usedQuestions = new List<string>();
					board2.boardCharacters = new List<List<char>>();
					board2.wordPlacements = new List<WordPlacement>();
					for (int i = 0; i < this.boardConfig.rowSize; i++)
					{
						board2.boardCharacters.Add(new List<char>());
						for (int j = 0; j < this.boardConfig.columnSize; j++)
						{
							board2.boardCharacters[i].Add(' ');
						}
					}
					BoardCreator.TutorialPlaceWordsOnBoard(this.boardConfig, board2, 0);
					if (board == null || board.usedWords.Count < board2.usedWords.Count)
					{
						board = board2;
						if (board.usedWords.Count == this.boardConfig.words.Count)
						{
							break;
						}
					}
				}
				stopwatch.Stop();
				for (int k = 0; k < this.boardConfig.rowSize; k++)
				{
					for (int l = 0; l < this.boardConfig.columnSize; l++)
					{
						if (board.boardCharacters[k][l] == ' ')
						{
							string randomCharacters = this.boardConfig.randomCharacters;
							bool flag = false;
							while (randomCharacters.Length > 0)
							{
								int num = random.Next(0, randomCharacters.Length);
								char value = randomCharacters[num];
								randomCharacters.Remove(num);
								board.boardCharacters[k][l] = value;
								if (!BoardCreator.CheckForFilterWord(this.boardConfig, board, new Position(k, l)))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								UnityEngine.Debug.LogError("[WordSearch] A random character could not be placed because all letters in the randomCharacters list create a word in filterWords.");
								return null;
							}
						}
					}
				}
				return board;
			}

			internal void __m__1(object obj)
			{
				this.OnBoardCompleted(obj as Board);
			}
		}

		private sealed class _PlaceWordOnBoard_c__AnonStorey2
		{
			internal string word;

			internal bool __m__0(string a)
			{
				return a == this.word;
			}
		}

		private sealed class _TutorialPlaceWordOnBoard_c__AnonStorey3
		{
			internal string word;

			internal bool __m__0(string a)
			{
				return a == this.word;
			}
		}

		private const char BlankChar = ' ';

		public static void CreateBoard(BoardConfig boardConfig, Action<Board> OnBoardCompleted)
		{
			AsyncTask.Start(boardConfig, delegate(object inData)
			{
				if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName != "Quiz")
				{
					boardConfig.words.Sort((string x, string y) => y.Length - x.Length);
				}
				System.Random random = new System.Random();
				Stopwatch stopwatch = new Stopwatch();
				Board board = null;
				stopwatch.Start();
				while (stopwatch.ElapsedMilliseconds < boardConfig.algoTimeoutInMilliseconds)
				{
					Board board2 = new Board();
					board2.rowSize = boardConfig.rowSize;
					board2.columnSize = boardConfig.columnSize;
					board2.usedWords = new List<string>();
					board2.usedQuestions = new List<string>();
					board2.boardCharacters = new List<List<char>>();
					board2.wordPlacements = new List<WordPlacement>();
					for (int i = 0; i < boardConfig.rowSize; i++)
					{
						board2.boardCharacters.Add(new List<char>());
						for (int j = 0; j < boardConfig.columnSize; j++)
						{
							board2.boardCharacters[i].Add(' ');
						}
					}
					BoardCreator.PlaceWordsOnBoard(boardConfig, random, board2, 0);
					if (board == null || board.usedWords.Count < board2.usedWords.Count)
					{
						board = board2;
						if (board.usedWords.Count == boardConfig.words.Count)
						{
							break;
						}
					}
				}
				stopwatch.Stop();
				for (int k = 0; k < boardConfig.rowSize; k++)
				{
					for (int l = 0; l < boardConfig.columnSize; l++)
					{
						if (board.boardCharacters[k][l] == ' ')
						{
							string randomCharacters = boardConfig.randomCharacters;
							bool flag = false;
							while (randomCharacters.Length > 0)
							{
								int num = random.Next(0, randomCharacters.Length);
								char value = randomCharacters[num];
								randomCharacters.Remove(num);
								board.boardCharacters[k][l] = value;
								if (!BoardCreator.CheckForFilterWord(boardConfig, board, new Position(k, l)))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								UnityEngine.Debug.LogError("[WordSearch] A random character could not be placed because all letters in the randomCharacters list create a word in filterWords.");
								return null;
							}
						}
					}
				}
				return board;
			}, delegate(object obj)
			{
				OnBoardCompleted(obj as Board);
			});
		}

		public static void TutorialCreateBoard(BoardConfig boardConfig, Action<Board> OnBoardCompleted)
		{
			AsyncTask.Start(boardConfig, delegate(object inData)
			{
				System.Random random = new System.Random();
				Stopwatch stopwatch = new Stopwatch();
				Board board = null;
				stopwatch.Start();
				while (stopwatch.ElapsedMilliseconds < boardConfig.algoTimeoutInMilliseconds)
				{
					Board board2 = new Board();
					board2.rowSize = boardConfig.rowSize;
					board2.columnSize = boardConfig.columnSize;
					board2.usedWords = new List<string>();
					board2.usedQuestions = new List<string>();
					board2.boardCharacters = new List<List<char>>();
					board2.wordPlacements = new List<WordPlacement>();
					for (int i = 0; i < boardConfig.rowSize; i++)
					{
						board2.boardCharacters.Add(new List<char>());
						for (int j = 0; j < boardConfig.columnSize; j++)
						{
							board2.boardCharacters[i].Add(' ');
						}
					}
					BoardCreator.TutorialPlaceWordsOnBoard(boardConfig, board2, 0);
					if (board == null || board.usedWords.Count < board2.usedWords.Count)
					{
						board = board2;
						if (board.usedWords.Count == boardConfig.words.Count)
						{
							break;
						}
					}
				}
				stopwatch.Stop();
				for (int k = 0; k < boardConfig.rowSize; k++)
				{
					for (int l = 0; l < boardConfig.columnSize; l++)
					{
						if (board.boardCharacters[k][l] == ' ')
						{
							string randomCharacters = boardConfig.randomCharacters;
							bool flag = false;
							while (randomCharacters.Length > 0)
							{
								int num = random.Next(0, randomCharacters.Length);
								char value = randomCharacters[num];
								randomCharacters.Remove(num);
								board.boardCharacters[k][l] = value;
								if (!BoardCreator.CheckForFilterWord(boardConfig, board, new Position(k, l)))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								UnityEngine.Debug.LogError("[WordSearch] A random character could not be placed because all letters in the randomCharacters list create a word in filterWords.");
								return null;
							}
						}
					}
				}
				return board;
			}, delegate(object obj)
			{
				OnBoardCompleted(obj as Board);
			});
		}

		private static void PlaceWordsOnBoard(BoardConfig boardConfig, System.Random random, Board board, int wordIndex)
		{
			for (int i = 0; i < boardConfig.words.Count; i++)
			{
				string text = boardConfig.words[i];
				string wordNoSpaces = text.Replace(" ", string.Empty);
				if (string.IsNullOrEmpty(text))
				{
					UnityEngine.Debug.LogWarning("[WordSearch] One of the words is an empty string.");
				}
				else
				{
					BoardCreator.PlaceWordOnBoard(boardConfig, random, board, text, wordNoSpaces);
				}
			}
		}

		private static void PlaceWordOnBoard(BoardConfig boardConfig, System.Random random, Board board, string word, string wordNoSpaces)
		{
			List<Position> possibleBoardIndices = BoardCreator.GetPossibleBoardIndices(board.boardCharacters, wordNoSpaces[0]);
			while (possibleBoardIndices.Count > 0)
			{
				Position position = possibleBoardIndices[random.Next(0, possibleBoardIndices.Count)];
				possibleBoardIndices.Remove(position);
				List<int[]> list = new List<int[]>();
				list.Add(new int[]
				{
					-1,
					-1
				});
				List<int[]> arg_6A_0 = list;
				int[] expr_66 = new int[2];
				expr_66[0] = -1;
				arg_6A_0.Add(expr_66);
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
				List<int[]> arg_B6_0 = list;
				int[] expr_B2 = new int[2];
				expr_B2[0] = 1;
				arg_B6_0.Add(expr_B2);
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
					int index = random.Next(0, list2.Count);
					int[] array = list2[index];
					list2.RemoveAt(index);
					int num = position.row + array[0] * (wordNoSpaces.Length - 1);
					int num2 = position.col + array[1] * (wordNoSpaces.Length - 1);
					if (num >= 0 && num < board.rowSize && num2 >= 0 && num2 < board.columnSize)
					{
						bool flag = true;
						bool flag2 = false;
						for (int i = 0; i < wordNoSpaces.Length; i++)
						{
							int index2 = position.row + array[0] * i;
							int index3 = position.col + array[1] * i;
							char c = board.boardCharacters[index2][index3];
							if (c != ' ' && c != wordNoSpaces[i])
							{
								flag = false;
								break;
							}
							if (c == ' ')
							{
								flag2 = true;
							}
						}
						if (flag && flag2)
						{
							List<Position> list3 = new List<Position>();
							for (int j = 0; j < wordNoSpaces.Length; j++)
							{
								int num3 = position.row + array[0] * j;
								int num4 = position.col + array[1] * j;
								char c2 = board.boardCharacters[num3][num4];
								if (c2 == ' ')
								{
									board.boardCharacters[num3][num4] = wordNoSpaces[j];
									list3.Add(new Position(num3, num4));
								}
							}
							bool flag3 = false;
							for (int k = 0; k < wordNoSpaces.Length; k++)
							{
								int row = position.row + array[0] * k;
								int col = position.col + array[1] * k;
								if (BoardCreator.CheckForFilterWord(boardConfig, board, new Position(row, col)))
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								WordPlacement wordPlacement = new WordPlacement();
								wordPlacement.word = wordNoSpaces;
								wordPlacement.startingPosition = position;
								wordPlacement.verticalDirection = array[0];
								wordPlacement.horizontalDirection = array[1];
								board.wordPlacements.Add(wordPlacement);
								board.usedWords.Add(word);
								if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
								{
									int index4 = boardConfig.words.FindIndex((string a) => a == word);
									board.usedQuestions.Add(boardConfig.words_question[index4]);
								}
								return;
							}
							for (int l = 0; l < list3.Count; l++)
							{
								board.boardCharacters[list3[l].row][list3[l].col] = ' ';
							}
						}
					}
				}
			}
		}

		private static void TutorialPlaceWordsOnBoard(BoardConfig boardConfig, Board board, int wordIndex)
		{
			for (int i = 0; i < boardConfig.words.Count; i++)
			{
				string text = boardConfig.words[i];
				string wordNoSpaces = text.Replace(" ", string.Empty);
				if (string.IsNullOrEmpty(text))
				{
					UnityEngine.Debug.LogWarning("[WordSearch] One of the words is an empty string.");
				}
				else
				{
					BoardCreator.TutorialPlaceWordOnBoard(boardConfig, board, text, wordNoSpaces);
				}
			}
		}

		private static void TutorialPlaceWordOnBoard(BoardConfig boardConfig, Board board, string word, string wordNoSpaces)
		{
			List<Position> possibleBoardIndices = BoardCreator.GetPossibleBoardIndices(board.boardCharacters, wordNoSpaces[0]);
			while (possibleBoardIndices.Count > 0)
			{
				Position position = possibleBoardIndices[0];
				possibleBoardIndices.Remove(position);
				List<int[]> list = new List<int[]>();
				list.Add(new int[]
				{
					-1,
					-1
				});
				List<int[]> arg_5D_0 = list;
				int[] expr_59 = new int[2];
				expr_59[0] = -1;
				arg_5D_0.Add(expr_59);
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
				List<int[]> arg_A9_0 = list;
				int[] expr_A5 = new int[2];
				expr_A5[0] = 1;
				arg_A9_0.Add(expr_A5);
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
					int index = 0;
					int[] array = list2[index];
					list2.RemoveAt(index);
					int num = position.row + array[0] * (wordNoSpaces.Length - 1);
					int num2 = position.col + array[1] * (wordNoSpaces.Length - 1);
					if (num >= 0 && num < board.rowSize && num2 >= 0 && num2 < board.columnSize)
					{
						bool flag = true;
						bool flag2 = false;
						for (int i = 0; i < wordNoSpaces.Length; i++)
						{
							int index2 = position.row + array[0] * i;
							int index3 = position.col + array[1] * i;
							char c = board.boardCharacters[index2][index3];
							if (c != ' ' && c != wordNoSpaces[i])
							{
								flag = false;
								break;
							}
							if (c == ' ')
							{
								flag2 = true;
							}
						}
						if (flag && flag2)
						{
							List<Position> list3 = new List<Position>();
							for (int j = 0; j < wordNoSpaces.Length; j++)
							{
								int num3 = position.row + array[0] * j;
								int num4 = position.col + array[1] * j;
								char c2 = board.boardCharacters[num3][num4];
								if (c2 == ' ')
								{
									board.boardCharacters[num3][num4] = wordNoSpaces[j];
									list3.Add(new Position(num3, num4));
								}
							}
							bool flag3 = false;
							for (int k = 0; k < wordNoSpaces.Length; k++)
							{
								int row = position.row + array[0] * k;
								int col = position.col + array[1] * k;
								if (BoardCreator.CheckForFilterWord(boardConfig, board, new Position(row, col)))
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								WordPlacement wordPlacement = new WordPlacement();
								wordPlacement.word = wordNoSpaces;
								wordPlacement.startingPosition = position;
								wordPlacement.verticalDirection = array[0];
								wordPlacement.horizontalDirection = array[1];
								board.wordPlacements.Add(wordPlacement);
								board.usedWords.Add(word);
								if (SingletonComponent<WordSearchController>.Instance.ActiveMode.modeName == "Quiz")
								{
									int index4 = boardConfig.words.FindIndex((string a) => a == word);
									board.usedQuestions.Add(boardConfig.words_question[index4]);
								}
								return;
							}
							for (int l = 0; l < list3.Count; l++)
							{
								board.boardCharacters[list3[l].row][list3[l].col] = ' ';
							}
						}
					}
				}
			}
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
	}
}
