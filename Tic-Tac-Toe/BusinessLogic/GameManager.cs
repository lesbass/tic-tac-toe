using System;
using System.Linq;
using TrisC.BusinessLogic.Interfaces;
using TrisC.Enums;
using TrisC.Infrastructure.Interfaces;
using TrisC.Models;
using TrisC.Models.Interfaces;

namespace TrisC.BusinessLogic
{
  public class GameManager : IGameManager
  {
    private readonly IGameDisplay _gameDisplay;
    private IGame _game;

    public GameManager(IGameDisplay gameDisplay)
    {
      _gameDisplay = gameDisplay;
    }

    public void StartNewGame()
    {
      _game = new Game(3, GameState.Running, SetFirstPlayer());
      while (_game.GameState == GameState.Running)
      {
        SwitchPlayer();

        _gameDisplay.DrawGame(_game);

        var mossaValida = false;
        while (!mossaValida)
        {
          var move = _gameDisplay.AskForMove(_game) - 1;
          if (move >= 0 && move <= 8)
          {
            var riga = move / 3;
            var colonna = move % 3;
            var scacchiera = _game.Scacchiera;
            if (scacchiera[riga, colonna] == null)
            {
              scacchiera[riga, colonna] = _game.CurrentPlayer;
              mossaValida = true;
            }
          }
        }

        CheckGameState();
      }

      _gameDisplay.EndGame(_game);

      if (_gameDisplay.AskPlayAgain() == "S") StartNewGame();
    }

    private void CheckGameState()
    {
      var scacchiera = _game.Scacchiera;
      for (var i = 0; i < scacchiera.GetUpperBound(1); i++)
        if (scacchiera[i, 1] != null && scacchiera[i, 1] == scacchiera[i, 0] &&
            scacchiera[i, 1] == scacchiera[i, 2]
            || scacchiera[1, i] != null && scacchiera[1, i] == scacchiera[0, i] &&
            scacchiera[1, i] == scacchiera[2, i])
        {
          _game.GameState = GameState.Win;
          return;
        }

      if (scacchiera[1, 1] != null &&
          (scacchiera[0, 0] == scacchiera[1, 1] && scacchiera[1, 1] == scacchiera[2, 2] ||
           scacchiera[0, 2] == scacchiera[1, 1] &&
           scacchiera[1, 1] == scacchiera[2, 0]))
      {
        _game.GameState = GameState.Win;
        return;
      }

      if (scacchiera.Cast<Pedina?>().All(x => x.HasValue)) _game.GameState = GameState.Draw;
    }

    private Pedina SetFirstPlayer()
    {
      var valori = Enum.GetValues(typeof(Pedina)).Cast<Pedina>().ToList();
      var rnd = new Random();
      return valori[rnd.Next(0, valori.Count)];
    }

    private void SwitchPlayer()
    {
      var valori = Enum.GetValues(typeof(Pedina)).Cast<Pedina>().ToList();
      _game.CurrentPlayer = _game.CurrentPlayer == valori[0] ? valori[1] : valori[0];
    }
  }
}