using Autofac;
using TrisC.BusinessLogic;
using TrisC.BusinessLogic.Interfaces;
using TrisC.Infrastructure;
using TrisC.Infrastructure.Interfaces;

namespace TrisC
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<GameManager>().As<IGameManager>();
      builder.RegisterType<GameDisplay>().As<IGameDisplay>();

      var container = builder.Build();

      container.Resolve<IGameManager>().StartNewGame();
    }
  }
}