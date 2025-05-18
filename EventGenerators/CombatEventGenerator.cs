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

        public override string GenerateEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();

            if (_gameState.IsCombatUnderAttackEnabled())
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp()}\", \"event\":\"UnderAttack\", \"Target\":\"You\" }}");
            }

            if (_gameState.IsCombatShieldStateEnabled())
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp()}\", \"event\":\"ShieldState\", \"ShieldsUp\":false }}");
            }

            if (_gameState.IsCombatHullDamageEnabled())
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp()}\", \"event\":\"HullDamage\", \"Health\":0.798478, \"PlayerPilot\":true, \"Fighter\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(20)}\", \"event\":\"HullDamage\", \"Health\":0.599972, \"PlayerPilot\":true, \"Fighter\":false }}");
            }

            if (_gameState.IsCombatDiedEnabled())
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(50)}\", \"event\":\"Died\", \"KillerName\":\"$UNKNOWN;\", \"KillerName_Localised\":\"Unknown\", \"KillerShip\":\"scout_q\", \"KillerRank\":\"Elite\" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{_gameState.GetCurrentTimestamp(70)}\", \"event\":\"Resurrect\", \"Option\":\"rebuy\", \"Cost\":8305590, \"Bankrupt\":false }}");
            }

            return journalBuilder.ToString();
        }
    }
}