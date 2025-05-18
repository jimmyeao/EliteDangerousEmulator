using EliteDangerousEmulator.Interfaces;
using System;
using System.Windows.Controls;

namespace EliteDangerousEmulator.Services
{
    public class GameStateProvider : IGameStateProvider
    {
        private readonly MainWindow _mainWindow;
        private readonly Random _random = new Random();

        public GameStateProvider(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        // Basic game state
        public string GetCommanderName() => _mainWindow.CommanderTextBox.Text;

        public string GetShipType()
        {
            var selectedShip = _mainWindow.ShipTypeComboBox.SelectedItem as ComboBoxItem;
            return selectedShip?.Content.ToString() ?? "Federation_Corvette";
        }

        public string GetShipTypeLocalised()
        {
            string shipType = GetShipType();
            switch (shipType)
            {
                case "Federation_Corvette": return "Federal Corvette";
                case "Krait_MKII": return "Krait Mk II";
                case "Python": return "Python";
                case "Anaconda": return "Anaconda";
                case "Type9": return "Type-9 Heavy";
                case "Imperial_Cutter": return "Imperial Cutter";
                default: return "Federal Corvette";
            }
        }

        public string GetCurrentSystem() => _mainWindow.CurrentSystemTextBox.Text;

        public string GetDockedStatus()
        {
            var selectedStatus = _mainWindow.DockedStatusComboBox.SelectedItem as ComboBoxItem;
            return selectedStatus?.Content.ToString() ?? "Docked";
        }

        public string GetStationName() => _mainWindow.StationTextBox.Text;

        public long GetBalance()
        {
            long balance;
            if (!long.TryParse(_mainWindow.CreditsTextBox.Text, out balance))
            {
                balance = 979884647;
            }
            return balance;
        }

        public double GetFuelLevel()
        {
            double fuel;
            if (!double.TryParse(_mainWindow.FuelTextBox.Text, out fuel))
            {
                fuel = 50.91;
            }
            return fuel;
        }

        // Helper methods
        public string GetCurrentTimestamp(int offsetSeconds = 0)
        {
            return DateTime.UtcNow.AddSeconds(offsetSeconds).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        public string GetRandomSystemAddress()
        {
            return _random.Next(10000000, 99999999) + _random.Next(10000000, 99999999) + "";
        }

        public string GetRandomStarPos()
        {
            double x = Math.Round(_random.NextDouble() * 400 - 200, 5);
            double y = Math.Round(_random.NextDouble() * 400 - 200, 5);
            double z = Math.Round(_random.NextDouble() * 400 - 200, 5);
            return $"{x},{y},{z}";
        }

        public double GetRandomJumpDistance()
        {
            return Math.Round(_random.NextDouble() * 80, 3);
        }

        public double GetRandomFuelUsed()
        {
            return Math.Round(_random.NextDouble() * 8, 6);
        }

        public double GetRandomFuelLevel()
        {
            return Math.Round(_random.NextDouble() * 32 + 5, 6);
        }

        // UI state methods
        public bool IsTravelStartJumpEnabled() => _mainWindow.TravelStartJumpCheck.IsChecked ?? false;
        public bool IsTravelFSDJumpEnabled() => _mainWindow.TravelFSDJumpCheck.IsChecked ?? false;
        public bool IsTravelLocationEnabled() => _mainWindow.TravelLocationCheck.IsChecked ?? false;
        public bool IsTravelApproachBodyEnabled() => _mainWindow.TravelApproachBodyCheck.IsChecked ?? false;
        public bool IsTravelLeaveBodyEnabled() => _mainWindow.TravelLeaveBodyCheck.IsChecked ?? false;
        public bool IsTravelSupercruiseEntryEnabled() => _mainWindow.TravelSupercruiseEntryCheck.IsChecked ?? false;
        public bool IsTravelSupercruiseExitEnabled() => _mainWindow.TravelSupercruiseExitCheck.IsChecked ?? false;

        public bool IsCombatDiedEnabled() => _mainWindow.CombatDiedCheck.IsChecked ?? false;
        public bool IsCombatHullDamageEnabled() => _mainWindow.CombatHullDamageCheck.IsChecked ?? false;
        public bool IsCombatShieldStateEnabled() => _mainWindow.CombatShieldStateCheck.IsChecked ?? false;
        public bool IsCombatUnderAttackEnabled() => _mainWindow.CombatUnderAttackCheck.IsChecked ?? false;
        public bool IsCombatFactionKillBondEnabled() => _mainWindow.CombatFactionKillBondCheck.IsChecked ?? false;
        public bool IsCombatBountyEnabled() => _mainWindow.CombatBountyCheck.IsChecked ?? false;

        // Logging methods
        public void AppendToLog(string message) => _mainWindow.AppendToLog(message);
        public void AppendToJournal(string content) => _mainWindow.AppendToJournal(content);
    }
}