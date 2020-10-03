using System;
using TrisC.Enums;
using TrisC.Infrastructure.Interfaces;
using TrisC.Models.Interfaces;

namespace TrisC.Infrastructure
{
  public class GameDisplay : IGameDisplay
  {
    public void DrawGame(IGame game)
    {
      var table = game.Scacchiera;
      Console.Clear();
      Console.WriteLine();
      var progressivo = 1;
      PrintRowSeparator();
      for (var y = 0; y <= table.GetUpperBound(0); y++)
      {
        Console.Write(" |");
        for (var x = 0; x <= table.GetUpperBound(1); x++)
        {
          var valore = table[y, x];
          var valoreString = valore.HasValue ? valore.ToString() : progressivo.ToString();
          Console.Write($" {valoreString} |");
          progressivo++;
        }

        Console.WriteLine();
        PrintRowSeparator();
      }
    }

    public int AskForMove(IGame game)
    {
      var currentPlayer = game.CurrentPlayer;
      int? result = null;
      while (!result.HasValue)
      {
        Console.WriteLine($"{currentPlayer} fai la tua mossa! Scegli uno spazio vuoto:");
        var key = Console.ReadLine();
        if (int.TryParse(key, out var resultKey))
          result = resultKey;
        else
          Console.WriteLine("Input non valido!");
      }

      return result.Value;
    }

    public void EndGame(IGame game)
    {
      DrawGame(game);
      Console.WriteLine("Gioco finito! " + game.GameState);
      if (game.GameState == GameState.Win) Console.WriteLine("Il vincitore è: " + game.CurrentPlayer);
    }

    public string AskPlayAgain()
    {
      Console.WriteLine("Desideri giocare ancora? [S] n");
      var response = Console.ReadLine();
      if (string.IsNullOrEmpty(response)) response = "S";
      return response;
    }

    private void PrintRowSeparator()
    {
      Console.WriteLine(" " + new string('-', 13));
    }
  }
}