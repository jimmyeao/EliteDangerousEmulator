using System;

namespace EliteDangerousEmulator.Interfaces
{
    public interface IGameStateProvider
    {
        // Basic game state
        string GetCommanderName();
        string GetShipType();
        string GetShipTypeLocalised();
        string GetCurrentSystem();
        string GetDockedStatus();
        string GetStationName();
        long GetBalance();
        double GetFuelLevel();

        // Helper methods for random/timestamp generation
        string GetCurrentTimestamp(int offsetSeconds = 0);
        string GetRandomSystemAddress();
        string GetRandomStarPos();
        double GetRandomJumpDistance();
        double GetRandomFuelUsed();
        double GetRandomFuelLevel();

        // Log methods
        void AppendToLog(string message);
        void AppendToJournal(string content);
    }
}