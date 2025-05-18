using System.Text;
using EliteDangerousEmulator.Interfaces;

namespace EliteDangerousEmulator.EventGenerators
{
    public class CombatEventGenerator : EventGeneratorBase
    {
        public CombatEventGenerator(IGameStateProvider gameState)
            : base(gameState)
        {
        }

        // Placeholder method for backwards compatibility
        public override string GenerateEvents()
        {
            // No longer need to check checkbox states since we're using direct button clicks
            StringBuilder journalBuilder = new StringBuilder();

            // For legacy support, generate a simple combat scenario
            journalBuilder.AppendLine(GenerateUnderAttackEvent());
            journalBuilder.AppendLine(GenerateShieldStateEvent(5, false));
            journalBuilder.AppendLine(GenerateHullDamageEvent(10, 0.8));

            _gameState.AppendToLog("Generated default combat events");
            return journalBuilder.ToString();
        }

        public string GenerateUnderAttackEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"UnderAttack\", \"Target\":\"You\" }}";
        }

        public string GenerateShieldStateEvent(int timeOffset = 0, bool shieldsUp = false)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"ShieldState\", \"ShieldsUp\":{shieldsUp.ToString().ToLower()} }}";
        }

        public string GenerateHullDamageEvent(int timeOffset = 0, double health = 0.8)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"HullDamage\", \"Health\":{health.ToString("0.000000")}, \"PlayerPilot\":true, \"Fighter\":false }}";
        }

        public string GenerateDiedEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Died\", \"KillerName\":\"$UNKNOWN;\", \"KillerName_Localised\":\"Unknown\", \"KillerShip\":\"scout_q\", \"KillerRank\":\"Elite\" }}";
        }

        public string GenerateResurrectEvent(int timeOffset = 0, int cost = 8305590)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Resurrect\", \"Option\":\"rebuy\", \"Cost\":{cost}, \"Bankrupt\":false }}";
        }

        public string GenerateBountyEvent(int timeOffset = 0, int reward = 100000)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Bounty\", \"Rewards\": [ {{ \"Faction\":\"The Dark Wheel\", \"Reward\":{reward} }} ], \"Target\":\"empire_fighter\", \"TotalReward\":{reward}, \"VictimFaction\":\"Empire\" }}";
        }

        public string GenerateFactionKillBondEvent(int timeOffset = 0, int reward = 75000)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"FactionKillBond\", \"Reward\":{reward}, \"AwardingFaction\":\"Federation\", \"VictimFaction\":\"Empire\" }}";
        }

        public string GenerateInterdictionEvent(int timeOffset = 0, bool success = true)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Interdiction\", \"Success\":{success.ToString().ToLower()}, \"Interdicted\":\"Eagle\", \"IsPlayer\":false, \"CombatRank\":2, \"Faction\":\"Pirates\", \"Power\":\"Edmund Mahon\" }}";
        }

        public string GenerateInterdictedEvent(int timeOffset = 0, bool submitted = false)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"Interdicted\", \"Submitted\":{submitted.ToString().ToLower()}, \"Interdictor\":\"Diamondback Scout\", \"IsPlayer\":false, \"CombatRank\":4, \"Faction\":\"Pirates\" }}";
        }

        public string GeneratePvPKillEvent(int timeOffset = 0)
        {
            return $"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(timeOffset)}\", \"event\":\"PVPKill\", \"Victim\":\"CMDR XYZ\", \"CombatRank\":4 }}";
        }
    }
}