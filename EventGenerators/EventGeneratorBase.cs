using EliteDangerousEmulator.Interfaces;

namespace EliteDangerousEmulator.EventGenerators
{
    public abstract class EventGeneratorBase
    {
        protected readonly IGameStateProvider _gameState;

        public EventGeneratorBase(IGameStateProvider gameState)
        {
            _gameState = gameState;
        }

        public abstract string GenerateEvents();
    }
}