using TrisC.Enums;

namespace TrisC.Models.Interfaces
{
  public interface IGame
  {
    Pedina CurrentPlayer { get; set; }
    GameState GameState { get; set; }
    Pedina?[,] Scacchiera { get; }
  }
}