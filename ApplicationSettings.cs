using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteDangerousEmulator
{
    public class ApplicationSettings
    {
        // Output Directory
        public string OutputDirectory { get; set; }

        // File Selection
        public bool GenerateBackpack { get; set; } = true;
        public bool GenerateCargo { get; set; } = true;
        public bool GenerateFCMaterials { get; set; } = true;
        public bool GenerateModulesInfo { get; set; } = true;
        public bool GenerateNavRoute { get; set; } = true;
        public bool GenerateOutfitting { get; set; } = true;
        public bool GenerateShipLocker { get; set; } = true;
        public bool GenerateShipyard { get; set; } = true;
        public bool GenerateStatus { get; set; } = true;
        public bool GenerateJournal { get; set; } = true;

        // Journal Settings
        public bool IncludeStartupEvents { get; set; } = true;
        public bool IncludeExplorationEvents { get; set; } = true;
        public bool IncludeCombatEvents { get; set; } = true;
        public bool IncludeStationEvents { get; set; } = true;

        // Game State
        public string CommanderName { get; set; } = "Jimmyeao";
        public string ShipType { get; set; } = "Federation_Corvette";
        public string CurrentSystem { get; set; } = "HIP 22410";
        public string DockedStatus { get; set; } = "Docked";
        public string StationName { get; set; } = "JNZ-4XN";
        public string Credits { get; set; } = "979884647";
        public string Fuel { get; set; } = "50.91";

        // Station Events
        public bool StationDocked { get; set; } = true;
        public bool StationUndocked { get; set; } = true;
        public bool StationMarket { get; set; } = false;
        public bool StationOutfitting { get; set; } = false;
        public bool StationShipyard { get; set; } = false;
        public bool StationStoredModules { get; set; } = false;
        public bool StationStoredShips { get; set; } = false;
        public bool StationModuleBuy { get; set; } = false;
        public bool StationModuleSell { get; set; } = false;
        public bool StationShipPurchased { get; set; } = false;
        public bool StationShipSold { get; set; } = false;
        public bool StationMarketBuy { get; set; } = false;
        public bool StationMarketSell { get; set; } = false;
        public bool StationMissionAccepted { get; set; } = false;
        public bool StationMissionCompleted { get; set; } = false;
        public bool StationMissionFailed { get; set; } = false;
        public bool StationCommunityGoal { get; set; } = false;

        // Customizations
        public bool EnableRandomization { get; set; } = false;
        public bool GenerateSimulatedJourney { get; set; } = false;
        public bool EmulateThargoidEncounter { get; set; } = true;

        // Live Updates
        public bool EnableLiveUpdates { get; set; } = false;
        public string UpdateInterval { get; set; } = "30";

        // Carrier Jump
        public int CarrierJumpMinutes { get; set; } = 25;
        public int CarrierJumpSeconds { get; set; } = 0;

        // Startup Events
        public bool StartupClearSavedGame { get; set; } = false;
        public bool StartupCommander { get; set; } = true;
        public bool StartupLoadGame { get; set; } = true;
        public bool StartupNewCommander { get; set; } = false;
        public bool StartupOdyssey { get; set; } = true;
        public bool StartupProgress { get; set; } = false;
        public bool StartupRank { get; set; } = true;
        public bool StartupReputation { get; set; } = true;

        // Travel Events
        public bool IncludeTravelEvents { get; set; } = true;
        public bool TravelFSDJump { get; set; } = true;
        public bool TravelLocation { get; set; } = true;
        public bool TravelApproachBody { get; set; } = false;
        public bool TravelLeaveBody { get; set; } = false;
        public bool TravelSupercruiseEntry { get; set; } = false;
        public bool TravelSupercruiseExit { get; set; } = false;

        // Combat Events
        public bool CombatDied { get; set; } = true;
        public bool CombatHullDamage { get; set; } = true;
        public bool CombatShieldState { get; set; } = true;
        public bool CombatUnderAttack { get; set; } = true;
        public bool CombatFactionKillBond { get; set; } = false;
        public bool CombatBounty { get; set; } = false;

        // Exploration Events
        public bool ExplorationScan { get; set; } = true;
        public bool ExplorationFSSDiscoveryScan { get; set; } = true;
        public bool ExplorationFSSSignalDiscovered { get; set; } = false;
        public bool ExplorationSAASignalsFound { get; set; } = false;
        public bool ExplorationSellExplorationData { get; set; } = false;

        // Carrier Events
        public bool CarrierJumpRequest { get; set; } = true;
        public bool CarrierJumpCancelled { get; set; } = false;
        public bool CarrierJump { get; set; } = false;
        public bool CarrierDockingPermission { get; set; } = false;
        public bool CarrierDepositFuel { get; set; } = false;
        public bool CarrierBuy { get; set; } = false;
        public bool CarrierSell { get; set; } = false;
    }
}
