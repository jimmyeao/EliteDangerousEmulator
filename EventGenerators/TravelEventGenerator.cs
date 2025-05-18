using System.Text;
using EliteDangerousEmulator.Interfaces;

namespace EliteDangerousEmulator.EventGenerators
{
    public class TravelEventGenerator : EventGeneratorBase
    {
        public TravelEventGenerator(IGameStateProvider gameState)
            : base(gameState)
        {
        }

        // We don't need the original GenerateEvents method anymore since we're using direct button clicks
        public override string GenerateEvents()
        {
            // This is now a placeholder that can be removed once we complete the transition to buttons
            StringBuilder journalBuilder = new StringBuilder();
            journalBuilder.AppendLine(GenerateLocationEvent());
            _gameState.AppendToLog("Generated basic travel event");
            return journalBuilder.ToString();
        }

        // Individual event generation methods
        public string GenerateStartJumpEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"StartJump\", \"JumpType\":\"Hyperspace\", \"Taxi\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarClass\":\"K\" }}";
        }

        public string GenerateFSDJumpEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"FSDJump\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarPos\":[{_gameState.GetRandomStarPos()}], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{_gameState.GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\", \"JumpDist\":{_gameState.GetRandomJumpDistance()}, \"FuelUsed\":{_gameState.GetRandomFuelUsed()}, \"FuelLevel\":{_gameState.GetRandomFuelLevel()} }}";
        }

        public string GenerateLocationEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Location\", \"Docked\":{(_gameState.GetDockedStatus() == "Docked" ? "true" : "false")}, \"StationName\":\"{_gameState.GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824, \"StationFaction\":{{ \"Name\":\"FleetCarrier\" }}, \"StationGovernment\":\"$government_Carrier;\", \"StationGovernment_Localised\":\"Private Ownership\", \"StationServices\":[ \"dock\", \"autodock\", \"commodities\", \"contacts\", \"crewlounge\", \"rearm\", \"refuel\", \"repair\", \"engineer\", \"flightcontroller\", \"stationoperations\", \"stationMenu\", \"carriermanagement\", \"carrierfuel\", \"socialspace\", \"bartender\" ], \"StationEconomy\":\"$economy_Carrier;\", \"StationEconomy_Localised\":\"Private Enterprise\", \"StationEconomies\":[ {{ \"Name\":\"$economy_Carrier;\", \"Name_Localised\":\"Private Enterprise\", \"Proportion\":1.000000 }} ], \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarPos\":[{_gameState.GetRandomStarPos()}], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{_gameState.GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\" }}";
        }

        public string GenerateSupercruiseEntryEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"SupercruiseEntry\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()} }}";
        }

        public string GenerateSupercruiseExitEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"SupercruiseExit\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"Body\":\"{_gameState.GetCurrentSystem()} A 2\", \"BodyID\":2, \"BodyType\":\"Planet\" }}";
        }

        public string GenerateApproachBodyEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"ApproachBody\", \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"Body\":\"{_gameState.GetCurrentSystem()} A 3\", \"BodyID\":3 }}";
        }

        public string GenerateLeaveBodyEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"LeaveBody\", \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"Body\":\"{_gameState.GetCurrentSystem()} A 3\", \"BodyID\":3 }}";
        }

        public string GenerateTouchdownEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Touchdown\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"Body\":\"{_gameState.GetCurrentSystem()} A 3\", \"BodyID\":3, \"OnStation\":false, \"OnPlanet\":true, \"Latitude\":45.12345, \"Longitude\":-60.98765 }}";
        }

        public string GenerateLiftoffEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Liftoff\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"Body\":\"{_gameState.GetCurrentSystem()} A 3\", \"BodyID\":3, \"OnStation\":false, \"OnPlanet\":true, \"Latitude\":45.12345, \"Longitude\":-60.98765 }}";
        }

        // Additional travel events can be added here as needed
    }
}