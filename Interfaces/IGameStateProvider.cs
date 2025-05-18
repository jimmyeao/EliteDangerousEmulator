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

        // UI state - gets settings from UI elements
        bool IsTravelStartJumpEnabled();
        bool IsTravelFSDJumpEnabled();
        bool IsTravelLocationEnabled();
        bool IsTravelApproachBodyEnabled();
        bool IsTravelLeaveBodyEnabled();
        bool IsTravelSupercruiseEntryEnabled();
        bool IsTravelSupercruiseExitEnabled();

        bool IsCombatDiedEnabled();
        bool IsCombatHullDamageEnabled();
        bool IsCombatShieldStateEnabled();
        bool IsCombatUnderAttackEnabled();
        bool IsCombatFactionKillBondEnabled();
        bool IsCombatBountyEnabled();

        // Add similar methods for exploration, station, carrier events

        // Log method
        void AppendToLog(string message);
        void AppendToJournal(string content);
    }
}