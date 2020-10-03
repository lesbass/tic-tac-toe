using TrisC.Models.Interfaces;

namespace TrisC.Infrastructure.Interfaces
{
  public interface IGameDisplay
  {
    void DrawGame(IGame table);
    int AskForMove(IGame currentPlayer);
    void EndGame(IGame game);
    string AskPlayAgain();
  }
}