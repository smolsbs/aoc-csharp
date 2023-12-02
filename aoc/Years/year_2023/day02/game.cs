using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc.Years.year_2023.day02 {
	internal class GameList {
		private const int MAX_RED = 12;
		private const int MAX_GREEN = 13;
		private const int MAX_BLUE = 14;

		public List<Game> game_list = new List<Game>();

		public class Game {
			public int id;
			public List<Round> tries = new List<Round>();

			static public bool CheckIfValidGame(Game game) {
				var aux = GetMinimumDices(game);

				if(aux.red > GameList.MAX_RED)
					return false;
				if (aux.green > GameList.MAX_GREEN)
					return false;
				if (aux.blue > GameList.MAX_BLUE)
					return false;

				return true;
			}

			public static (int red, int green, int blue) GetMinimumDices(Game game) {
				int r = game.tries.Select(v => v.redDice).Max();
				int g = game.tries.Select(v => v.greenDice).Max();
				int b = game.tries.Select(v => v.blueDice).Max();
				return (r,g,b);
			}
		}

		public class Round {
			public int redDice;
			public int greenDice;
			public int blueDice;
		}


		public void LoadGames(string[] gameList) {
			foreach(var gameLine in gameList) {
				Game game = new Game();

				string[] gameInfo = gameLine.Split(": ");
				game.id = int.Parse(gameInfo[0].Split(" ")[1]);

				foreach( var _round in gameInfo[1].Split("; ")) {
					Round round = new Round();
					foreach(var dice in _round.Split(", ")) {
						(int diceNumber, char diceColor) = dice.Split(" ").Chunk(2).Select(v => (int.Parse(v[0]), v[1][0])).First();
						switch(diceColor) {
							case 'r':
								round.redDice = diceNumber;
								break;
							case 'g':
								round.greenDice = diceNumber;
								break;
							case 'b':
								round.blueDice = diceNumber;
								break;
						}
					}
					game.tries.Add(round);
				}
				game_list.Add(game);
			}
			
		}
	}
}
