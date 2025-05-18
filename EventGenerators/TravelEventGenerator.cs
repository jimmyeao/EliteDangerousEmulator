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

        public override string GenerateEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();
            int eventCount = 0;

            _gameState.AppendToLog("Starting to generate travel events...");

            // StartJump should come before FSDJump with proper timing
            if (_gameState.IsTravelStartJumpEnabled() && _gameState.IsTravelFSDJumpEnabled())
            {
                // First emit the StartJump (happens ~15-20 seconds before FSDJump)
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp()}\", \"event\":\"StartJump\", \"JumpType\":\"Hyperspace\", \"Taxi\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarClass\":\"K\" }}");
                eventCount++;

                // Then emit the FSDJump (with timestamp ~18 seconds later)
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(18)}\", \"event\":\"FSDJump\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarPos\":[{_gameState.GetRandomStarPos()}], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{_gameState.GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\", \"JumpDist\":{_gameState.GetRandomJumpDistance()}, \"FuelUsed\":{_gameState.GetRandomFuelUsed()}, \"FuelLevel\":{_gameState.GetRandomFuelLevel()} }}");
                eventCount++;
            }
            // If only FSDJump is selected (without StartJump)
            else if (_gameState.IsTravelFSDJumpEnabled())
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp()}\", \"event\":\"FSDJump\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarPos\":[{_gameState.GetRandomStarPos()}], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{_gameState.GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\", \"JumpDist\":{_gameState.GetRandomJumpDistance()}, \"FuelUsed\":{_gameState.GetRandomFuelUsed()}, \"FuelLevel\":{_gameState.GetRandomFuelLevel()} }}");
                eventCount++;
            }
            // If only StartJump is selected (without FSDJump)
            else if (_gameState.IsTravelStartJumpEnabled())
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp()}\", \"event\":\"StartJump\", \"JumpType\":\"Hyperspace\", \"Taxi\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarClass\":\"K\" }}");
                eventCount++;
            }

            if (_gameState.IsTravelLocationEnabled())
            {
                // We'll add Location after any jumps to maintain logical sequence
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(25)}\", \"event\":\"Location\", \"Docked\":{(_gameState.GetDockedStatus() == "Docked" ? "true" : "false")}, \"StationName\":\"{_gameState.GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824, \"StationFaction\":{{ \"Name\":\"FleetCarrier\" }}, \"StationGovernment\":\"$government_Carrier;\", \"StationGovernment_Localised\":\"Private Ownership\", \"StationServices\":[ \"dock\", \"autodock\", \"commodities\", \"contacts\", \"crewlounge\", \"rearm\", \"refuel\", \"repair\", \"engineer\", \"flightcontroller\", \"stationoperations\", \"stationMenu\", \"carriermanagement\", \"carrierfuel\", \"socialspace\", \"bartender\" ], \"StationEconomy\":\"$economy_Carrier;\", \"StationEconomy_Localised\":\"Private Enterprise\", \"StationEconomies\":[ {{ \"Name\":\"$economy_Carrier;\", \"Name_Localised\":\"Private Enterprise\", \"Proportion\":1.000000 }} ], \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{_gameState.GetCurrentSystem()}\", \"SystemAddress\":{_gameState.GetRandomSystemAddress()}, \"StarPos\":[{_gameState.GetRandomStarPos()}], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{_gameState.GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\" }}");
                eventCount++;
            }

            _gameState.AppendToLog($"Generated {eventCount} travel events");

            return journalBuilder.ToString();
        }
    }
}