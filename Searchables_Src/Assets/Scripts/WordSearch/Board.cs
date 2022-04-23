using System;
using System.Collections.Generic;

namespace WordSearch
{
	public class Board
	{
		public int rowSize;

		public int columnSize;

		public List<string> usedWords;

		public List<string> usedQuestions;

		public List<List<char>> boardCharacters;

		public List<WordPlacement> wordPlacements;

		public Board Copy()
		{
			Board board = new Board();
			board.rowSize = this.rowSize;
			board.columnSize = this.columnSize;
			board.usedWords = new List<string>(this.usedWords);
			board.usedQuestions = new List<string>(this.usedQuestions);
			board.wordPlacements = new List<WordPlacement>(this.wordPlacements);
			board.boardCharacters = new List<List<char>>();
			for (int i = 0; i < this.boardCharacters.Count; i++)
			{
				board.boardCharacters.Add(new List<char>(this.boardCharacters[i]));
			}
			return board;
		}
	}
}
