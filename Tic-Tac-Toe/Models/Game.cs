using TrisC.Enums;
using TrisC.Models.Interfaces;

namespace TrisC.Models
{
  public class Game : IGame
  {
    public Game(int side, GameState gameState, Pedina currentPlayer)
    {
      Scacchiera = new Pedina?[side, side];
      GameState = gameState;
      CurrentPlayer = currentPlayer;
    }

    public Pedina CurrentPlayer { get; set; }
    public GameState GameState { get; set; }
    public Pedina?[,] Scacchiera { get; }
  }
}