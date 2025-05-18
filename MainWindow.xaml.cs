using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;

namespace EliteDangerousEmulator
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;
        private string _outputDirectory;
        private ApplicationSettings _settings = new ApplicationSettings();
        private const string SettingsFileName = "EliteDangerousEmulatorSettings.json";
        private bool _isInitializing = true;
        public MainWindow()
        {
            InitializeComponent();

            // Set default output directory to Documents/Elite Dangerous
            _outputDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Elite Dangerous");

            OutputDirectoryTextBox.Text = _outputDirectory;

            AppendToLog("Application started");

            // Load saved settings if they exist
            LoadSettings();

            // The LoadSettings method will set the OutputDirectoryTextBox.Text
            // so we need to update _outputDirectory after loading settings
            _outputDirectory = OutputDirectoryTextBox.Text;

            AppendToLog($"Current output directory: {_outputDirectory}");
            _isInitializing = false;
        }
        private void SettingsChanged(object sender, TextChangedEventArgs e)
        {
            SaveSettings();
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            AppendToLog($"CheckBox changed: {(sender as CheckBox)?.Name}");
            SaveSettings();
        }
        private void GenerateStartupEventsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateStartupEvents();
                MessageBox.Show("Startup events generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating startup events: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateTravelEventsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateTravelEvents();
                MessageBox.Show("Travel events generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating travel events: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateCombatEventsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateCombatEvents();
                MessageBox.Show("Combat events generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating combat events: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateExplorationEventsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateExplorationEvents();
                MessageBox.Show("Exploration events generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating exploration events: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Implement these methods to generate the specific event types
        private void GenerateStartupEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();

            if (StartupCommanderCheck.IsChecked == true)
            {
                string commanderName = CommanderTextBox.Text;
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"Commander\", \"FID\":\"F1961982\", \"Name\":\"{commanderName}\" }}");
            }

            if (StartupLoadGameCheck.IsChecked == true)
            {
                string commanderName = CommanderTextBox.Text;
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"LoadGame\", \"FID\":\"F1961982\", \"Commander\":\"{commanderName}\", \"Horizons\":true, \"Odyssey\":true, \"Ship\":\"{GetShipType()}\", \"Ship_Localised\":\"{GetShipTypeLocalised()}\", \"ShipID\":8, \"ShipName\":\"Daedalus\", \"ShipIdent\":\"SGA-1\", \"FuelLevel\":40.000000, \"FuelCapacity\":40.000000, \"GameMode\":\"Group\", \"Group\":\"Bluemryld\", \"Credits\":{GetBalance()}, \"Loan\":0, \"language\":\"English/UK\", \"gameversion\":\"4.0.0.1401\", \"build\":\"r286554/r0 \" }}");
            }

            // Add other startup events based on checkboxes

            AppendToJournal(journalBuilder.ToString());
        }

        private void GenerateTravelEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();

            if (TravelLocationCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"Location\", \"Docked\":{(GetDockedStatus() == "Docked" ? "true" : "false")}, \"StationName\":\"{GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824, \"StationFaction\":{{ \"Name\":\"FleetCarrier\" }}, \"StationGovernment\":\"$government_Carrier;\", \"StationGovernment_Localised\":\"Private Ownership\", \"StationServices\":[ \"dock\", \"autodock\", \"commodities\", \"contacts\", \"crewlounge\", \"rearm\", \"refuel\", \"repair\", \"engineer\", \"flightcontroller\", \"stationoperations\", \"stationMenu\", \"carriermanagement\", \"carrierfuel\", \"socialspace\", \"bartender\" ], \"StationEconomy\":\"$economy_Carrier;\", \"StationEconomy_Localised\":\"Private Enterprise\", \"StationEconomies\":[ {{ \"Name\":\"$economy_Carrier;\", \"Name_Localised\":\"Private Enterprise\", \"Proportion\":1.000000 }} ], \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323, \"StarPos\":[-49.46875,-55.25000,-360.59375], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\" }}");
            }

            if (TravelFSDJumpCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"FSDJump\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{GetCurrentSystem()}\", \"SystemAddress\":319715002691, \"StarPos\":[-41.31250,-58.96875,-354.78125], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\", \"JumpDist\":10.684, \"FuelUsed\":0.728097, \"FuelLevel\":39.271904, \"Factions\":[ {{ \"Name\":\"Azimuth Biotech\", \"FactionState\":\"None\", \"Government\":\"Corporate\", \"Influence\":0.000000, \"Allegiance\":\"Independent\", \"Happiness\":\"$Faction_HappinessBand2;\", \"Happiness_Localised\":\"Happy\", \"MyReputation\":3.179460 }} ], \"SystemFaction\":{{ \"Name\":\"Azimuth Biotech\" }} }}");
            }

            // Add other travel events

            AppendToJournal(journalBuilder.ToString());
        }

        private void GenerateCombatEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();

            if (CombatUnderAttackCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"UnderAttack\", \"Target\":\"You\" }}");
            }

            if (CombatShieldStateCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"ShieldState\", \"ShieldsUp\":false }}");
            }

            if (CombatHullDamageCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"HullDamage\", \"Health\":0.798478, \"PlayerPilot\":true, \"Fighter\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(20)}\", \"event\":\"HullDamage\", \"Health\":0.599972, \"PlayerPilot\":true, \"Fighter\":false }}");
            }

            if (CombatDiedCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(50)}\", \"event\":\"Died\", \"KillerName\":\"$UNKNOWN;\", \"KillerName_Localised\":\"Unknown\", \"KillerShip\":\"scout_q\", \"KillerRank\":\"Elite\" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(70)}\", \"event\":\"Resurrect\", \"Option\":\"rebuy\", \"Cost\":8305590, \"Bankrupt\":false }}");
            }

            // Add other combat events

            AppendToJournal(journalBuilder.ToString());
        }

        private void GenerateExplorationEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();

            if (ExplorationFSSDiscoveryScanCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"FSSDiscoveryScan\", \"Progress\":0.044794, \"BodyCount\":67, \"NonBodyCount\":41, \"SystemName\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323 }}");
            }

            if (ExplorationScanCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"Scan\", \"ScanType\":\"Detailed\", \"BodyName\":\"{GetCurrentSystem()} 7 c\", \"BodyID\":51, \"Parents\":[ {{\"Planet\":46}}, {{\"Star\":0}} ], \"StarSystem\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323, \"DistanceFromArrivalLS\":848.248718, \"TidalLock\":false, \"TerraformState\":\"\", \"PlanetClass\":\"Rocky body\", \"Atmosphere\":\"\", \"AtmosphereType\":\"None\", \"Volcanism\":\"\", \"MassEM\":0.009566, \"Radius\":1481574.125000, \"SurfaceGravity\":1.737044, \"SurfaceTemperature\":239.880905, \"SurfacePressure\":0.000000, \"Landable\":true, \"Materials\":[ {{ \"Name\":\"iron\", \"Percent\":19.096224 }}, {{ \"Name\":\"sulphur\", \"Percent\":18.682741 }}, {{ \"Name\":\"carbon\", \"Percent\":15.710250 }}, {{ \"Name\":\"nickel\", \"Percent\":14.443583 }}, {{ \"Name\":\"phosphorus\", \"Percent\":10.057972 }}, {{ \"Name\":\"manganese\", \"Percent\":7.886545 }}, {{ \"Name\":\"germanium\", \"Percent\":5.551579 }}, {{ \"Name\":\"zinc\", \"Percent\":5.189638 }}, {{ \"Name\":\"ruthenium\", \"Percent\":1.179314 }}, {{ \"Name\":\"tin\", \"Percent\":1.153586 }}, {{ \"Name\":\"tungsten\", \"Percent\":1.048574 }} ], \"Composition\":{{ \"Ice\":0.000000, \"Rock\":0.909904, \"Metal\":0.090096 }}, \"SemiMajorAxis\":2399724364.280701, \"Eccentricity\":0.000782, \"OrbitalInclination\":-0.197379, \"Periapsis\":331.369090, \"OrbitalPeriod\":1296790.897846, \"AscendingNode\":-4.858204, \"MeanAnomaly\":29.433322, \"RotationPeriod\":-1296814.671666, \"AxialTilt\":3.033991, \"WasDiscovered\":true, \"WasMapped\":true }}");
            }

            // Add other exploration events

            AppendToJournal(journalBuilder.ToString());
        }

        // Helper method to append to the journal
        private void AppendToJournal(string content)
        {
            string todayDatePrefix = DateTime.Now.ToString("yyyy-MM-dd");
            string[] existingJournalFiles = Directory.GetFiles(_outputDirectory, $"Journal.{todayDatePrefix}*.log");

            string filePath;

            if (existingJournalFiles.Length > 0)
            {
                Array.Sort(existingJournalFiles);
                filePath = existingJournalFiles[existingJournalFiles.Length - 1];

                File.AppendAllText(filePath, content);
                AppendToLog($"Appended events to existing journal at {filePath}");
            }
            else
            {
                GenerateJournalFile();

                existingJournalFiles = Directory.GetFiles(_outputDirectory, $"Journal.{todayDatePrefix}*.log");
                if (existingJournalFiles.Length > 0)
                {
                    Array.Sort(existingJournalFiles);
                    filePath = existingJournalFiles[existingJournalFiles.Length - 1];
                    File.AppendAllText(filePath, content);
                    AppendToLog($"Appended events to new journal at {filePath}");
                }
            }
        }
        private void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveSettings();
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // WPF-only solution to select a folder
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Select Output Directory",
                Filter = "Folder|*.folder", // Not actually used
                FileName = "Select Folder" // Not actually used
            };

            if (dialog.ShowDialog() == true)
            {
                // Get the selected path without the filename
                _outputDirectory = System.IO.Path.GetDirectoryName(dialog.FileName);
                OutputDirectoryTextBox.Text = _outputDirectory;
                AppendToLog($"Output directory changed to: {_outputDirectory}");
            }
        }
        private void SaveSettings()
        {
            if (_isInitializing)
                return;

           
            try
            {
                //if settings are null return
                if (_settings == null)
                {
                    AppendToLog("Settings are null, cannot save.");
                    return;
                }
                // Update settings from UI controls
                _settings.OutputDirectory = OutputDirectoryTextBox.Text;
                // Startup Event Settings
                _settings.StartupClearSavedGame = StartupClearSavedGameCheck.IsChecked ?? false;
                _settings.StartupCommander = StartupCommanderCheck.IsChecked ?? false;
                _settings.StartupLoadGame = StartupLoadGameCheck.IsChecked ?? false;
                _settings.StartupNewCommander = StartupNewCommanderCheck.IsChecked ?? false;
                _settings.StartupOdyssey = StartupOdysseyCheck.IsChecked ?? false;
                _settings.StartupProgress = StartupProgressCheck.IsChecked ?? false;
                _settings.StartupRank = StartupRankCheck.IsChecked ?? false;
                _settings.StartupReputation = StartupReputationCheck.IsChecked ?? false;

                // Travel Event Settings
                _settings.IncludeTravelEvents = IncludeTravelEventsCheck.IsChecked ?? false;
                _settings.TravelFSDJump = TravelFSDJumpCheck.IsChecked ?? false;
                _settings.TravelLocation = TravelLocationCheck.IsChecked ?? false;
                _settings.TravelApproachBody = TravelApproachBodyCheck.IsChecked ?? false;
                _settings.TravelLeaveBody = TravelLeaveBodyCheck.IsChecked ?? false;
                _settings.TravelSupercruiseEntry = TravelSupercruiseEntryCheck.IsChecked ?? false;
                _settings.TravelSupercruiseExit = TravelSupercruiseExitCheck.IsChecked ?? false;

                // Combat Event Settings
                _settings.CombatDied = CombatDiedCheck.IsChecked ?? false;
                _settings.CombatHullDamage = CombatHullDamageCheck.IsChecked ?? false;
                _settings.CombatShieldState = CombatShieldStateCheck.IsChecked ?? false;
                _settings.CombatUnderAttack = CombatUnderAttackCheck.IsChecked ?? false;
                _settings.CombatFactionKillBond = CombatFactionKillBondCheck.IsChecked ?? false;
                _settings.CombatBounty = CombatBountyCheck.IsChecked ?? false;

                // Exploration Event Settings
                _settings.ExplorationScan = ExplorationScanCheck.IsChecked ?? false;
                _settings.ExplorationFSSDiscoveryScan = ExplorationFSSDiscoveryScanCheck.IsChecked ?? false;
                _settings.ExplorationFSSSignalDiscovered = ExplorationFSSSignalDiscoveredCheck.IsChecked ?? false;
                _settings.ExplorationSAASignalsFound = ExplorationSAASignalsFoundCheck.IsChecked ?? false;
                _settings.ExplorationSellExplorationData = ExplorationSellExplorationDataCheck.IsChecked ?? false;

                // Carrier Event Settings
                _settings.CarrierJumpRequest = CarrierJumpRequestCheck.IsChecked ?? false;
                _settings.CarrierJumpCancelled = CarrierJumpCancelledCheck.IsChecked ?? false;
                _settings.CarrierJump = CarrierJumpCheck.IsChecked ?? false;
                _settings.CarrierDockingPermission = CarrierDockingPermissionCheck.IsChecked ?? false;
                _settings.CarrierDepositFuel = CarrierDepositFuelCheck.IsChecked ?? false;
                _settings.CarrierBuy = CarrierBuyCheck.IsChecked ?? false;
                _settings.CarrierSell = CarrierSellCheck.IsChecked ?? false;
                _settings.GenerateBackpack = BackpackCheck.IsChecked ?? false;
                _settings.GenerateCargo = CargoCheck.IsChecked ?? false;
                _settings.GenerateFCMaterials = FCMaterialsCheck.IsChecked ?? false;
                _settings.GenerateModulesInfo = ModulesInfoCheck.IsChecked ?? false;
                _settings.GenerateNavRoute = NavRouteCheck.IsChecked ?? false;
                _settings.GenerateOutfitting = OutfittingCheck.IsChecked ?? false;
                _settings.GenerateShipLocker = ShipLockerCheck.IsChecked ?? false;
                _settings.GenerateShipyard = ShipyardCheck.IsChecked ?? false;
                _settings.GenerateStatus = StatusCheck.IsChecked ?? false;
                _settings.GenerateJournal = JournalCheck.IsChecked ?? false;

                _settings.IncludeStartupEvents = IncludeStartupEventsCheck.IsChecked ?? false;
                _settings.IncludeExplorationEvents = IncludeExplorationEventsCheck.IsChecked ?? false;
                _settings.IncludeCombatEvents = IncludeCombatEventsCheck.IsChecked ?? false;
                _settings.IncludeStationEvents = IncludeStationEventsCheck.IsChecked ?? false;

                _settings.CommanderName = CommanderTextBox.Text;
                _settings.ShipType = (ShipTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Federation_Corvette";
                _settings.CurrentSystem = CurrentSystemTextBox.Text;
                _settings.DockedStatus = (DockedStatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Docked";
                _settings.StationName = StationTextBox.Text;
                _settings.Credits = CreditsTextBox.Text;
                _settings.Fuel = FuelTextBox.Text;

                _settings.EnableRandomization = EnableRandomizationCheck.IsChecked ?? false;
                _settings.GenerateSimulatedJourney = GenerateSimulatedJourneyCheck.IsChecked ?? false;
                _settings.EmulateThargoidEncounter = EmulateThargoidEncounterCheck.IsChecked ?? false;

                _settings.EnableLiveUpdates = EnableLiveUpdatesCheck.IsChecked ?? false;
                _settings.UpdateInterval = UpdateIntervalTextBox.Text;

                _settings.CarrierJumpMinutes = int.Parse(CarrierJumpMinutesTextBox.Text);
                _settings.CarrierJumpSeconds = int.Parse(CarrierJumpSecondsTextBox.Text);
                // Station Events
                _settings.StationDocked = StationDockedCheck.IsChecked ?? false;
                _settings.StationUndocked = StationUndockedCheck.IsChecked ?? false;
                _settings.StationMarket = StationMarketCheck.IsChecked ?? false;
                _settings.StationOutfitting = StationOutfittingCheck.IsChecked ?? false;
                _settings.StationShipyard = StationShipyardCheck.IsChecked ?? false;
                _settings.StationStoredModules = StationStoredModulesCheck.IsChecked ?? false;
                _settings.StationStoredShips = StationStoredShipsCheck.IsChecked ?? false;
                _settings.StationModuleBuy = StationModuleBuyCheck.IsChecked ?? false;
                _settings.StationModuleSell = StationModuleSellCheck.IsChecked ?? false;
                _settings.StationShipPurchased = StationShipPurchasedCheck.IsChecked ?? false;
                _settings.StationShipSold = StationShipSoldCheck.IsChecked ?? false;
                _settings.StationMarketBuy = StationMarketBuyCheck.IsChecked ?? false;
                _settings.StationMarketSell = StationMarketSellCheck.IsChecked ?? false;
                _settings.StationMissionAccepted = StationMissionAcceptedCheck.IsChecked ?? false;
                _settings.StationMissionCompleted = StationMissionCompletedCheck.IsChecked ?? false;
                _settings.StationMissionFailed = StationMissionFailedCheck.IsChecked ?? false;
                _settings.StationCommunityGoal = StationCommunityGoalCheck.IsChecked ?? false;

                // Convert to JSON and save to file
                string settingsJson = JsonConvert.SerializeObject(_settings, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(SettingsFileName, settingsJson);

                AppendToLog("Settings saved successfully");
            }
            catch (Exception ex)
            {
                AppendToLog($"Error saving settings: {ex.Message}");
            }
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsFileName))
                {
                    string settingsJson = File.ReadAllText(SettingsFileName);
                    _settings = JsonConvert.DeserializeObject<ApplicationSettings>(settingsJson);

                    // Apply settings to UI controls
                    OutputDirectoryTextBox.Text = _settings.OutputDirectory;
                    // Startup Event Settings
                    StartupClearSavedGameCheck.IsChecked = _settings.StartupClearSavedGame;
                    StartupCommanderCheck.IsChecked = _settings.StartupCommander;
                    StartupLoadGameCheck.IsChecked = _settings.StartupLoadGame;
                    StartupNewCommanderCheck.IsChecked = _settings.StartupNewCommander;
                    StartupOdysseyCheck.IsChecked = _settings.StartupOdyssey;
                    StartupProgressCheck.IsChecked = _settings.StartupProgress;
                    StartupRankCheck.IsChecked = _settings.StartupRank;
                    StartupReputationCheck.IsChecked = _settings.StartupReputation;

                    // Travel Event Settings
                    IncludeTravelEventsCheck.IsChecked = _settings.IncludeTravelEvents;
                    TravelFSDJumpCheck.IsChecked = _settings.TravelFSDJump;
                    TravelLocationCheck.IsChecked = _settings.TravelLocation;
                    TravelApproachBodyCheck.IsChecked = _settings.TravelApproachBody;
                    TravelLeaveBodyCheck.IsChecked = _settings.TravelLeaveBody;
                    TravelSupercruiseEntryCheck.IsChecked = _settings.TravelSupercruiseEntry;
                    TravelSupercruiseExitCheck.IsChecked = _settings.TravelSupercruiseExit;

                    // Combat Event Settings
                    CombatDiedCheck.IsChecked = _settings.CombatDied;
                    CombatHullDamageCheck.IsChecked = _settings.CombatHullDamage;
                    CombatShieldStateCheck.IsChecked = _settings.CombatShieldState;
                    CombatUnderAttackCheck.IsChecked = _settings.CombatUnderAttack;
                    CombatFactionKillBondCheck.IsChecked = _settings.CombatFactionKillBond;
                    CombatBountyCheck.IsChecked = _settings.CombatBounty;

                    // Exploration Event Settings
                    ExplorationScanCheck.IsChecked = _settings.ExplorationScan;
                    ExplorationFSSDiscoveryScanCheck.IsChecked = _settings.ExplorationFSSDiscoveryScan;
                    ExplorationFSSSignalDiscoveredCheck.IsChecked = _settings.ExplorationFSSSignalDiscovered;
                    ExplorationSAASignalsFoundCheck.IsChecked = _settings.ExplorationSAASignalsFound;
                    ExplorationSellExplorationDataCheck.IsChecked = _settings.ExplorationSellExplorationData;

                    // Carrier Event Settings
                    CarrierJumpRequestCheck.IsChecked = _settings.CarrierJumpRequest;
                    CarrierJumpCancelledCheck.IsChecked = _settings.CarrierJumpCancelled;
                    CarrierJumpCheck.IsChecked = _settings.CarrierJump;
                    CarrierDockingPermissionCheck.IsChecked = _settings.CarrierDockingPermission;
                    CarrierDepositFuelCheck.IsChecked = _settings.CarrierDepositFuel;
                    CarrierBuyCheck.IsChecked = _settings.CarrierBuy;
                    CarrierSellCheck.IsChecked = _settings.CarrierSell;
                    BackpackCheck.IsChecked = _settings.GenerateBackpack;
                    CargoCheck.IsChecked = _settings.GenerateCargo;
                    FCMaterialsCheck.IsChecked = _settings.GenerateFCMaterials;
                    ModulesInfoCheck.IsChecked = _settings.GenerateModulesInfo;
                    NavRouteCheck.IsChecked = _settings.GenerateNavRoute;
                    OutfittingCheck.IsChecked = _settings.GenerateOutfitting;
                    ShipLockerCheck.IsChecked = _settings.GenerateShipLocker;
                    ShipyardCheck.IsChecked = _settings.GenerateShipyard;
                    StatusCheck.IsChecked = _settings.GenerateStatus;
                    JournalCheck.IsChecked = _settings.GenerateJournal;

                    IncludeStartupEventsCheck.IsChecked = _settings.IncludeStartupEvents;
                    IncludeExplorationEventsCheck.IsChecked = _settings.IncludeExplorationEvents;
                    IncludeCombatEventsCheck.IsChecked = _settings.IncludeCombatEvents;
                    IncludeStationEventsCheck.IsChecked = _settings.IncludeStationEvents;
                    // Station Events
                    StationDockedCheck.IsChecked = _settings.StationDocked;
                    StationUndockedCheck.IsChecked = _settings.StationUndocked;
                    StationMarketCheck.IsChecked = _settings.StationMarket;
                    StationOutfittingCheck.IsChecked = _settings.StationOutfitting;
                    StationShipyardCheck.IsChecked = _settings.StationShipyard;
                    StationStoredModulesCheck.IsChecked = _settings.StationStoredModules;
                    StationStoredShipsCheck.IsChecked = _settings.StationStoredShips;
                    StationModuleBuyCheck.IsChecked = _settings.StationModuleBuy;
                    StationModuleSellCheck.IsChecked = _settings.StationModuleSell;
                    StationShipPurchasedCheck.IsChecked = _settings.StationShipPurchased;
                    StationShipSoldCheck.IsChecked = _settings.StationShipSold;
                    StationMarketBuyCheck.IsChecked = _settings.StationMarketBuy;
                    StationMarketSellCheck.IsChecked = _settings.StationMarketSell;
                    StationMissionAcceptedCheck.IsChecked = _settings.StationMissionAccepted;
                    StationMissionCompletedCheck.IsChecked = _settings.StationMissionCompleted;
                    StationMissionFailedCheck.IsChecked = _settings.StationMissionFailed;
                    StationCommunityGoalCheck.IsChecked = _settings.StationCommunityGoal;
                    CommanderTextBox.Text = _settings.CommanderName;

                    // Set the selected ship type in the combo box
                    foreach (ComboBoxItem item in ShipTypeComboBox.Items)
                    {
                        if (item.Content.ToString() == _settings.ShipType)
                        {
                            ShipTypeComboBox.SelectedItem = item;
                            break;
                        }
                    }

                    CurrentSystemTextBox.Text = _settings.CurrentSystem;

                    // Set the selected docked status in the combo box
                    foreach (ComboBoxItem item in DockedStatusComboBox.Items)
                    {
                        if (item.Content.ToString() == _settings.DockedStatus)
                        {
                            DockedStatusComboBox.SelectedItem = item;
                            break;
                        }
                    }

                    StationTextBox.Text = _settings.StationName;
                    CreditsTextBox.Text = _settings.Credits;
                    FuelTextBox.Text = _settings.Fuel;

                    EnableRandomizationCheck.IsChecked = _settings.EnableRandomization;
                    GenerateSimulatedJourneyCheck.IsChecked = _settings.GenerateSimulatedJourney;
                    EmulateThargoidEncounterCheck.IsChecked = _settings.EmulateThargoidEncounter;

                    EnableLiveUpdatesCheck.IsChecked = _settings.EnableLiveUpdates;
                    UpdateIntervalTextBox.Text = _settings.UpdateInterval;

                    CarrierJumpMinutesTextBox.Text = _settings.CarrierJumpMinutes.ToString();
                    CarrierJumpSecondsTextBox.Text = _settings.CarrierJumpSeconds.ToString();

                    AppendToLog("Settings loaded successfully");
                }
                else
                {
                    AppendToLog("No saved settings found, using defaults");
                }
            }
            catch (Exception ex)
            {
                AppendToLog($"Error loading settings: {ex.Message}");
            }
        }
        private void GenerateStationEventsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GenerateStationEvents();
                MessageBox.Show("Station events generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating station events: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateStationEvents()
        {
            StringBuilder journalBuilder = new StringBuilder();

            if (StationDockedCheck.IsChecked == true)
            {
                // Generate Docked event
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"Docked\", \"StationName\":\"{GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824, \"StationFaction\":{{ \"Name\":\"FleetCarrier\" }}, \"StationGovernment\":\"$government_Carrier;\", \"StationGovernment_Localised\":\"Private Ownership\", \"StationServices\":[ \"dock\", \"autodock\", \"commodities\", \"contacts\", \"crewlounge\", \"rearm\", \"refuel\", \"repair\", \"engineer\", \"flightcontroller\", \"stationoperations\", \"stationMenu\", \"carriermanagement\", \"carrierfuel\", \"socialspace\", \"bartender\" ], \"StationEconomy\":\"$economy_Carrier;\", \"StationEconomy_Localised\":\"Private Enterprise\", \"StationEconomies\":[ {{ \"Name\":\"$economy_Carrier;\", \"Name_Localised\":\"Private Enterprise\", \"Proportion\":1.000000 }} ] }}");
            }

            if (StationUndockedCheck.IsChecked == true)
            {
                // Generate Undocked event
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(10)}\", \"event\":\"Undocked\", \"StationName\":\"{GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824 }}");
            }

            if (StationMarketCheck.IsChecked == true)
            {
                // Generate Market event (simplified)
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(20)}\", \"event\":\"Market\", \"MarketID\":3706669824, \"StationName\":\"{GetStationName()}\", \"StationType\":\"FleetCarrier\", \"StarSystem\":\"{GetCurrentSystem()}\", \"Items\":[] }}");
            }

            if (StationOutfittingCheck.IsChecked == true)
            {
                // Generate Outfitting event (simplified)
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(30)}\", \"event\":\"Outfitting\", \"MarketID\":3706669824, \"StationName\":\"{GetStationName()}\", \"StarSystem\":\"{GetCurrentSystem()}\", \"Items\":[] }}");
            }

            if (StationShipyardCheck.IsChecked == true)
            {
                // Generate Shipyard event (simplified)
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(40)}\", \"event\":\"Shipyard\", \"MarketID\":3706669824, \"StationName\":\"{GetStationName()}\", \"StarSystem\":\"{GetCurrentSystem()}\", \"Horizons\":true, \"AllowCobraMkIV\":false, \"PriceList\":[] }}");
            }

            // Add other station events based on checkboxes
            // ...

            // Append all events to the journal
            if (journalBuilder.Length > 0)
            {
                AppendToJournal(journalBuilder.ToString());
            }
            else
            {
                AppendToLog("No station events were selected to generate");
            }
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _outputDirectory = OutputDirectoryTextBox.Text;

                // Ensure the directory exists
                if (!Directory.Exists(_outputDirectory))
                {
                    Directory.CreateDirectory(_outputDirectory);
                    AppendToLog($"Created directory: {_outputDirectory}");
                }

                // Generate each selected file
                if (BackpackCheck.IsChecked == true)
                    GenerateBackpackFile();

                if (CargoCheck.IsChecked == true)
                    GenerateCargoFile();

                if (FCMaterialsCheck.IsChecked == true)
                    GenerateFCMaterialsFile();

                if (ModulesInfoCheck.IsChecked == true)
                    GenerateModulesInfoFile();

                if (NavRouteCheck.IsChecked == true)
                    GenerateNavRouteFile();

                if (OutfittingCheck.IsChecked == true)
                    GenerateOutfittingFile();

                if (ShipLockerCheck.IsChecked == true)
                    GenerateShipLockerFile();

                if (ShipyardCheck.IsChecked == true)
                    GenerateShipyardFile();

                if (StatusCheck.IsChecked == true)
                    GenerateStatusFile();

                if (JournalCheck.IsChecked == true)
                    GenerateJournalFile();

                AppendToLog("File generation completed successfully!");
                MessageBox.Show("Files generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating files: {ex.Message}");
                MessageBox.Show($"Error generating files: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartLiveUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                // Stop live updates
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
                StartLiveUpdatesButton.Content = "Start Live Updates";
                AppendToLog("Live updates stopped");
            }
            else
            {
                // Start live updates
                _cancellationTokenSource = new CancellationTokenSource();
                StartLiveUpdatesButton.Content = "Stop Live Updates";
                AppendToLog("Live updates started");

                int intervalSeconds;
                if (!int.TryParse(UpdateIntervalTextBox.Text, out intervalSeconds) || intervalSeconds < 1)
                {
                    intervalSeconds = 30;
                    UpdateIntervalTextBox.Text = "30";
                }

                StartLiveUpdatesAsync(intervalSeconds, _cancellationTokenSource.Token);
            }
        }

        private async Task StartLiveUpdatesAsync(int intervalSeconds, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    GenerateButton_Click(null, null);
                    await Task.Delay(intervalSeconds * 1000, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation
            }
            catch (Exception ex)
            {
                AppendToLog($"Error in live update loop: {ex.Message}");
            }
        }

        private void AppendToLog(string message)
        {
            //return if we are still initilaising
            if (_isInitializing)
                return;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            LogTextBox.AppendText($"[{timestamp}] {message}\r\n");
            LogTextBox.ScrollToEnd();
        }

        #region File Generation Methods

        private void GenerateBackpackFile()
        {
            var backpackData = new
            {
                timestamp = GetCurrentTimestamp(),
                @event = "Backpack",
                Items = new List<object>(),
                Components = new List<object>(),
                Consumables = new List<object>(),
                Data = new List<object>()
            };

            string json = JsonConvert.SerializeObject(backpackData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "Backpack.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated Backpack.json at {filePath}");
        }

        private void GenerateCargoFile()
        {
            var cargoData = new
            {
                timestamp = GetCurrentTimestamp(),
                @event = "Cargo",
                Vessel = "Ship",
                Count = 0,
                Inventory = new List<object>()
            };

            string json = JsonConvert.SerializeObject(cargoData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "Cargo.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated Cargo.json at {filePath}");
        }

        private void GenerateFCMaterialsFile()
        {
            var fcMaterialsData = new
            {
                timestamp = GetCurrentTimestamp(),
                @event = "FCMaterials",
                MarketID = 3706669824,
                CarrierName = "MOYA FUEL & LIMPETS",
                CarrierID = "JNZ-4XN",
                Items = new[]
                {
                    new { id = 128962608, Name = "$shipschematic_name;", Name_Localised = "Ship Schematic", Price = 55600, Stock = 0, Demand = 20 },
                    new { id = 128962613, Name = "$vehicleschematic_name;", Name_Localised = "Vehicle Schematic", Price = 70000, Stock = 0, Demand = 10 },
                    new { id = 128962614, Name = "$weaponschematic_name;", Name_Localised = "Weapon Schematic", Price = 35000, Stock = 0, Demand = 10 },
                    new { id = 128961528, Name = "$chemicalsuperbase_name;", Name_Localised = "Chemical Superbase", Price = 826, Stock = 0, Demand = 86 },
                    new { id = 128965842, Name = "$largecapacitypowerregulator_name;", Name_Localised = "Power Regulator", Price = 70500, Stock = 0, Demand = 33 },
                    new { id = 128962609, Name = "$suitschematic_name;", Name_Localised = "Suit Schematic", Price = 98900, Stock = 0, Demand = 37 },
                    new { id = 128961534, Name = "$epoxyadhesive_name;", Name_Localised = "Epoxy Adhesive", Price = 371, Stock = 0, Demand = 41 },
                    new { id = 128965845, Name = "$weaponcomponent_name;", Name_Localised = "Weapon Component", Price = 1566, Stock = 0, Demand = 76 },
                    new { id = 128965844, Name = "$ionbattery_name;", Name_Localised = "Ion Battery", Price = 1246, Stock = 0, Demand = 20 },
                    new { id = 128961527, Name = "$chemicalcatalyst_name;", Name_Localised = "Chemical Catalyst", Price = 1106, Stock = 0, Demand = 20 },
                    new { id = 128064021, Name = "$graphene_name;", Name_Localised = "Graphene", Price = 1300, Stock = 0, Demand = 36 },
                    new { id = 128961526, Name = "$carbonfibreplating_name;", Name_Localised = "Carbon Fibre Plating", Price = 1126, Stock = 0, Demand = 32 }
                }
            };

            string json = JsonConvert.SerializeObject(fcMaterialsData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "FCMaterials.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated FCMaterials.json at {filePath}");
        }

        private void GenerateModulesInfoFile()
        {
            var modulesList = new List<Dictionary<string, object>>();

            // Create individual modules
            modulesList.Add(new Dictionary<string, object> { { "Slot", "MainEngines" }, { "Item", "int_engine_size7_class5" }, { "Power", 10.214400 }, { "Priority", 0 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot04_Size5" }, { "Item", "int_shieldgenerator_size5_class5" }, { "Power", 3.640000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot05_Size4" }, { "Item", "int_guardianfsdbooster_size4" }, { "Power", 1.650000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "MediumHardpoint1" }, { "Item", "hpt_basicmissilerack_fixed_medium" }, { "Power", 1.248000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "MediumHardpoint2" }, { "Item", "hpt_basicmissilerack_fixed_medium" }, { "Power", 1.248000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "MediumHardpoint3" }, { "Item", "hpt_basicmissilerack_fixed_medium" }, { "Power", 1.248000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "TinyHardpoint1" }, { "Item", "hpt_shieldbooster_size0_class5" }, { "Power", 1.200000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "TinyHardpoint2" }, { "Item", "hpt_shieldbooster_size0_class5" }, { "Power", 1.200000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "FrameShiftDrive" }, { "Item", "int_hyperdrive_overcharge_size6_class5" }, { "Power", 0.862500 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "PowerDistributor" }, { "Item", "int_powerdistributor_size6_class5" }, { "Power", 0.820000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "LifeSupport" }, { "Item", "int_lifesupport_size5_class2" }, { "Power", 0.640000 }, { "Priority", 0 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "CargoHatch" }, { "Item", "modularcargobaydoor" }, { "Power", 0.600000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot06_Size4" }, { "Item", "int_fuelscoop_size4_class5" }, { "Power", 0.570000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot12_Size1" }, { "Item", "int_dockingcomputer_advanced" }, { "Power", 0.450000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "SmallHardpoint1" }, { "Item", "hpt_pulselaser_gimbal_small" }, { "Power", 0.390000 }, { "Priority", 0 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "SmallHardpoint2" }, { "Item", "hpt_pulselaser_gimbal_small" }, { "Power", 0.390000 }, { "Priority", 0 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Radar" }, { "Item", "int_sensors_size4_class2" }, { "Power", 0.310000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "TinyHardpoint3" }, { "Item", "hpt_plasmapointdefence_turret_tiny" }, { "Power", 0.200000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "TinyHardpoint4" }, { "Item", "hpt_heatsinklauncher_turret_tiny" }, { "Power", 0.200000 }, { "Priority", 2 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "ShipCockpit" }, { "Item", "type9_cockpit" }, { "Power", 0.000000 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "PowerPlant" }, { "Item", "int_powerplant_size6_class5" }, { "Power", 0.000000 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot00_Size8" }, { "Item", "int_cargorack_size8_class1" }, { "Power", 0.000000 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot01_Size8" }, { "Item", "int_cargorack_size8_class1" }, { "Power", 0.000000 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot02_Size7" }, { "Item", "int_cargorack_size7_class1" }, { "Power", 0.000000 } });
            modulesList.Add(new Dictionary<string, object> { { "Slot", "Slot03_Size6" }, { "Item", "int_cargorack_size6_class1" }, { "Power", 0.000000 } });

            // Add more modules...

            var modulesData = new Dictionary<string, object>
    {
        { "timestamp", GetCurrentTimestamp() },
        { "event", "ModuleInfo" },
        { "Modules", modulesList }
    };

            string json = JsonConvert.SerializeObject(modulesData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "ModulesInfo.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated ModulesInfo.json at {filePath}");
        }
        private void GenerateNavRouteFile()
        {
            var navRouteData = new
            {
                timestamp = GetCurrentTimestamp(),
                @event = "NavRouteClear",
                Route = new List<object>()
            };

            string json = JsonConvert.SerializeObject(navRouteData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "NavRoute.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated NavRoute.json at {filePath}");
        }

        private void GenerateOutfittingFile()
        {
            var outfittingData = new
            {
                timestamp = GetCurrentTimestamp(),
                @event = "Outfitting",
                MarketID = 129032695,
                StationName = "Trailblazer Wish",
                StarSystem = "Bletii",
                Horizons = true,
                Items = new List<object>()
            };

            string json = JsonConvert.SerializeObject(outfittingData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "Outfitting.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated Outfitting.json at {filePath}");
        }

        private void GenerateShipLockerFile()
        {
            var itemsList = new List<Dictionary<string, object>>();
            var componentsList = new List<Dictionary<string, object>>();
            var consumablesList = new List<Dictionary<string, object>>();
            var dataList = new List<Dictionary<string, object>>();

            // Add items
            itemsList.Add(new Dictionary<string, object> { { "Name", "chemicalsample" }, { "Name_Localised", "Chemical Sample" }, { "OwnerID", 0 }, { "Count", 2 } });
            itemsList.Add(new Dictionary<string, object> { { "Name", "biochemicalagent" }, { "Name_Localised", "Biochemical Agent" }, { "OwnerID", 0 }, { "Count", 8 } });
            itemsList.Add(new Dictionary<string, object> { { "Name", "californium" }, { "OwnerID", 0 }, { "Count", 2 } });
            // Add more items...

            // Add components
            componentsList.Add(new Dictionary<string, object> { { "Name", "motor" }, { "OwnerID", 0 }, { "Count", 22 } });
            componentsList.Add(new Dictionary<string, object> { { "Name", "opticalfibre" }, { "Name_Localised", "Optical Fibre" }, { "OwnerID", 0 }, { "Count", 45 } });
            componentsList.Add(new Dictionary<string, object> { { "Name", "opticallens" }, { "Name_Localised", "Optical Lens" }, { "OwnerID", 0 }, { "Count", 16 } });
            // Add more components...

            // Add consumables
            consumablesList.Add(new Dictionary<string, object> { { "Name", "healthpack" }, { "Name_Localised", "Medkit" }, { "OwnerID", 0 }, { "Count", 100 } });
            consumablesList.Add(new Dictionary<string, object> { { "Name", "energycell" }, { "Name_Localised", "Energy Cell" }, { "OwnerID", 0 }, { "Count", 100 } });
            // Add more consumables...

            // Add data items
            dataList.Add(new Dictionary<string, object> { { "Name", "spyware" }, { "OwnerID", 0 }, { "MissionID", 896233394 }, { "Count", 1 } });
            dataList.Add(new Dictionary<string, object> { { "Name", "internalcorrespondence" }, { "Name_Localised", "Internal Correspondence" }, { "OwnerID", 0 }, { "Count", 1 } });
            // Add more data items...

            var shipLockerData = new Dictionary<string, object>
    {
        { "timestamp", GetCurrentTimestamp() },
        { "event", "ShipLocker" },
        { "Items", itemsList },
        { "Components", componentsList },
        { "Consumables", consumablesList },
        { "Data", dataList }
    };

            string json = JsonConvert.SerializeObject(shipLockerData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "ShipLocker.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated ShipLocker.json at {filePath}");
        }
        private void GenerateShipyardFile()
        {
            var priceList = new List<Dictionary<string, object>>();

            priceList.Add(new Dictionary<string, object> { { "id", 0 }, { "ShipType", "type8" }, { "ShipType_Localised", "Type-8 Transporter" }, { "ShipPrice", 0 } });
            priceList.Add(new Dictionary<string, object> { { "id", 0 }, { "ShipType", "type9" }, { "ShipType_Localised", "Type-9 Heavy" }, { "ShipPrice", 0 } });
            priceList.Add(new Dictionary<string, object> { { "id", 0 }, { "ShipType", "sidewinder" }, { "ShipPrice", 28080 } });
            priceList.Add(new Dictionary<string, object> { { "id", 0 }, { "ShipType", "eagle" }, { "ShipPrice", 125114 } });
            priceList.Add(new Dictionary<string, object> { { "id", 0 }, { "ShipType", "vulture" }, { "ShipPrice", 1381844 } });
            // Add more ships...

            var shipyardData = new Dictionary<string, object>
    {
        { "timestamp", GetCurrentTimestamp() },
        { "event", "Shipyard" },
        { "MarketID", 3706669824 },
        { "StationName", "JNZ-4XN" },
        { "StarSystem", "Bletii" },
        { "Horizons", true },
        { "AllowCobraMkIV", false },
        { "PriceList", priceList }
    };

            string json = JsonConvert.SerializeObject(shipyardData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "Shipyard.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated Shipyard.json at {filePath}");
        }
        private void GenerateStatusFile()
        {
            double fuelMain = 50.909992;
            if (double.TryParse(FuelTextBox.Text, out double enteredFuel))
            {
                fuelMain = enteredFuel;
            }

            var statusData = new
            {
                timestamp = GetCurrentTimestamp(),
                @event = "Status",
                Flags = 16842765,
                Flags2 = 0,
                Pips = new[] { 4, 7, 1 },
                FireGroup = 2,
                GuiFocus = 1,
                Fuel = new { FuelMain = fuelMain, FuelReservoir = 0.420638 },
                Cargo = 0.000000,
                LegalState = "Clean",
                Balance = GetBalance(),
                Destination = new { System = 3068527642971, Body = 4, Name = "ArchAngel 1 JNZ-4XN" }
            };

            string json = JsonConvert.SerializeObject(statusData, Newtonsoft.Json.Formatting.Indented);
            string filePath = Path.Combine(_outputDirectory, "Status.json");
            File.WriteAllText(filePath, json);

            AppendToLog($"Generated Status.json at {filePath}");
        }
        private void GenerateCarrierJumpRequestEvent()
        {
            try
            {
                // Calculate jump time based on minutes and seconds settings
                int totalSeconds = 0;

                if (int.TryParse(CarrierJumpMinutesTextBox.Text, out int minutes))
                    totalSeconds += minutes * 60;

                if (int.TryParse(CarrierJumpSecondsTextBox.Text, out int seconds))
                    totalSeconds += seconds;

                string todayDatePrefix = DateTime.Now.ToString("yyyy-MM-dd");
                string[] existingJournalFiles = Directory.GetFiles(_outputDirectory, $"Journal.{todayDatePrefix}*.log");

                // Create the carrier jump request
                StringBuilder jumpRequestBuilder = new StringBuilder();
                jumpRequestBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"CarrierJumpRequest\", \"CarrierID\":\"{StationTextBox.Text}\", \"SystemName\":\"{CurrentSystemTextBox.Text}\", \"Body\":\"A 3\", \"SystemAddress\":3068527642971, \"BodyID\":7, \"DepartureTime\":\"{GetCurrentTimestamp(totalSeconds)}\" }}");

                string filePath;

                // Check if journal files already exist for today
                if (existingJournalFiles.Length > 0)
                {
                    // Sort files by name to find the latest one
                    Array.Sort(existingJournalFiles);
                    filePath = existingJournalFiles[existingJournalFiles.Length - 1];

                    // Append to the existing file
                    File.AppendAllText(filePath, jumpRequestBuilder.ToString());
                    AppendToLog($"Added CarrierJumpRequest event to journal at {filePath}");
                    AppendToLog($"Carrier jump scheduled {minutes} minutes and {seconds} seconds from now");
                }
                else
                {
                    // Create a new journal file with today's date
                    GenerateJournalFile(); // This will create a basic journal file

                    // Now find and append to the newly created file
                    existingJournalFiles = Directory.GetFiles(_outputDirectory, $"Journal.{todayDatePrefix}*.log");
                    if (existingJournalFiles.Length > 0)
                    {
                        Array.Sort(existingJournalFiles);
                        filePath = existingJournalFiles[existingJournalFiles.Length - 1];
                        File.AppendAllText(filePath, jumpRequestBuilder.ToString());
                        AppendToLog($"Added CarrierJumpRequest event to new journal at {filePath}");
                        AppendToLog($"Carrier jump scheduled {minutes} minutes and {seconds} seconds from now");
                    }
                }
            }
            catch (Exception ex)
            {
                AppendToLog($"Error generating Carrier Jump Request: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GenerateJournalFile()
        {
            // First check if a journal file for today already exists
            string todayDatePrefix = DateTime.Now.ToString("yyyy-MM-dd");
            string[] existingJournalFiles = Directory.GetFiles(_outputDirectory, $"Journal.{todayDatePrefix}*.log");

            // Create the content to add
            StringBuilder journalBuilder = new StringBuilder();

            // Don't add file header if we're appending to an existing file
            if (existingJournalFiles.Length == 0)
            {
                // Start with file header for new files only
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp()}\", \"event\":\"Fileheader\", \"part\":1, \"language\":\"English/UK\", \"Odyssey\":true, \"gameversion\":\"4.0.0.1401\", \"build\":\"r286554/r0 \" }}");
            }

            // Add commander info
            string commanderName = CommanderTextBox.Text;
            journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(30)}\", \"event\":\"Commander\", \"FID\":\"F1961982\", \"Name\":\"{commanderName}\" }}");

            // Add basic info
            if (IncludeStartupEventsCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(35)}\", \"event\":\"LoadGame\", \"FID\":\"F1961982\", \"Commander\":\"{commanderName}\", \"Horizons\":true, \"Odyssey\":true, \"Ship\":\"{GetShipType()}\", \"Ship_Localised\":\"{GetShipTypeLocalised()}\", \"ShipID\":8, \"ShipName\":\"Daedalus\", \"ShipIdent\":\"SGA-1\", \"FuelLevel\":40.000000, \"FuelCapacity\":40.000000, \"GameMode\":\"Group\", \"Group\":\"Bluemryld\", \"Credits\":{GetBalance()}, \"Loan\":0, \"language\":\"English/UK\", \"gameversion\":\"4.0.0.1401\", \"build\":\"r286554/r0 \" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(40)}\", \"event\":\"Location\", \"Docked\":{(GetDockedStatus() == "Docked" ? "true" : "false")}, \"StationName\":\"{GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824, \"StationFaction\":{{ \"Name\":\"FleetCarrier\" }}, \"StationGovernment\":\"$government_Carrier;\", \"StationGovernment_Localised\":\"Private Ownership\", \"StationServices\":[ \"dock\", \"autodock\", \"commodities\", \"contacts\", \"crewlounge\", \"rearm\", \"refuel\", \"repair\", \"engineer\", \"flightcontroller\", \"stationoperations\", \"stationMenu\", \"carriermanagement\", \"carrierfuel\", \"socialspace\", \"bartender\" ], \"StationEconomy\":\"$economy_Carrier;\", \"StationEconomy_Localised\":\"Private Enterprise\", \"StationEconomies\":[ {{ \"Name\":\"$economy_Carrier;\", \"Name_Localised\":\"Private Enterprise\", \"Proportion\":1.000000 }} ], \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323, \"StarPos\":[-49.46875,-55.25000,-360.59375], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\" }}");
            }

            // Add exploration events if selected
            if (IncludeExplorationEventsCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(120)}\", \"event\":\"FSSDiscoveryScan\", \"Progress\":0.044794, \"BodyCount\":67, \"NonBodyCount\":41, \"SystemName\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323 }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(140)}\", \"event\":\"Scan\", \"ScanType\":\"Detailed\", \"BodyName\":\"{GetCurrentSystem()} 7 c\", \"BodyID\":51, \"Parents\":[ {{\"Planet\":46}}, {{\"Star\":0}} ], \"StarSystem\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323, \"DistanceFromArrivalLS\":848.248718, \"TidalLock\":false, \"TerraformState\":\"\", \"PlanetClass\":\"Rocky body\", \"Atmosphere\":\"\", \"AtmosphereType\":\"None\", \"Volcanism\":\"\", \"MassEM\":0.009566, \"Radius\":1481574.125000, \"SurfaceGravity\":1.737044, \"SurfaceTemperature\":239.880905, \"SurfacePressure\":0.000000, \"Landable\":true, \"Materials\":[ {{ \"Name\":\"iron\", \"Percent\":19.096224 }}, {{ \"Name\":\"sulphur\", \"Percent\":18.682741 }}, {{ \"Name\":\"carbon\", \"Percent\":15.710250 }}, {{ \"Name\":\"nickel\", \"Percent\":14.443583 }}, {{ \"Name\":\"phosphorus\", \"Percent\":10.057972 }}, {{ \"Name\":\"manganese\", \"Percent\":7.886545 }}, {{ \"Name\":\"germanium\", \"Percent\":5.551579 }}, {{ \"Name\":\"zinc\", \"Percent\":5.189638 }}, {{ \"Name\":\"ruthenium\", \"Percent\":1.179314 }}, {{ \"Name\":\"tin\", \"Percent\":1.153586 }}, {{ \"Name\":\"tungsten\", \"Percent\":1.048574 }} ], \"Composition\":{{ \"Ice\":0.000000, \"Rock\":0.909904, \"Metal\":0.090096 }}, \"SemiMajorAxis\":2399724364.280701, \"Eccentricity\":0.000782, \"OrbitalInclination\":-0.197379, \"Periapsis\":331.369090, \"OrbitalPeriod\":1296790.897846, \"AscendingNode\":-4.858204, \"MeanAnomaly\":29.433322, \"RotationPeriod\":-1296814.671666, \"AxialTilt\":3.033991, \"WasDiscovered\":true, \"WasMapped\":true }}");
            }

            // Add Thargoid encounter events if selected
            if (EmulateThargoidEncounterCheck.IsChecked == true)
            {
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(200)}\", \"event\":\"FSDJump\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"HIP 22460\", \"SystemAddress\":319715002691, \"StarPos\":[-41.31250,-58.96875,-354.78125], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"HIP 22460\", \"BodyID\":0, \"BodyType\":\"Star\", \"JumpDist\":10.684, \"FuelUsed\":0.728097, \"FuelLevel\":39.271904, \"Factions\":[ {{ \"Name\":\"Azimuth Biotech\", \"FactionState\":\"None\", \"Government\":\"Corporate\", \"Influence\":0.000000, \"Allegiance\":\"Independent\", \"Happiness\":\"$Faction_HappinessBand2;\", \"Happiness_Localised\":\"Happy\", \"MyReputation\":3.179460 }} ], \"SystemFaction\":{{ \"Name\":\"Azimuth Biotech\" }} }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(210)}\", \"event\":\"Music\", \"MusicTrack\":\"Unknown_Encounter\" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(211)}\", \"event\":\"Music\", \"MusicTrack\":\"Combat_Unknown\" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(230)}\", \"event\":\"UnderAttack\", \"Target\":\"You\" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(300)}\", \"event\":\"ShieldState\", \"ShieldsUp\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(310)}\", \"event\":\"HullDamage\", \"Health\":0.798478, \"PlayerPilot\":true, \"Fighter\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(330)}\", \"event\":\"HullDamage\", \"Health\":0.599972, \"PlayerPilot\":true, \"Fighter\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(350)}\", \"event\":\"HullDamage\", \"Health\":0.392691, \"PlayerPilot\":true, \"Fighter\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(380)}\", \"event\":\"HullDamage\", \"Health\":0.199117, \"PlayerPilot\":true, \"Fighter\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(400)}\", \"event\":\"Died\", \"KillerName\":\"$UNKNOWN;\", \"KillerName_Localised\":\"Unknown\", \"KillerShip\":\"scout_q\", \"KillerRank\":\"Elite\" }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(430)}\", \"event\":\"Resurrect\", \"Option\":\"rebuy\", \"Cost\":8305590, \"Bankrupt\":false }}");
                journalBuilder.AppendLine($"{{ \"timestamp\":\"{GetCurrentTimestamp(450)}\", \"event\":\"Location\", \"Docked\":true, \"StationName\":\"{GetStationName()}\", \"StationType\":\"FleetCarrier\", \"MarketID\":3706669824, \"StationFaction\":{{ \"Name\":\"FleetCarrier\" }}, \"StationGovernment\":\"$government_Carrier;\", \"StationGovernment_Localised\":\"Private Ownership\", \"StationServices\":[ \"dock\", \"autodock\", \"commodities\", \"contacts\", \"crewlounge\", \"rearm\", \"refuel\", \"repair\", \"engineer\", \"flightcontroller\", \"stationoperations\", \"stationMenu\", \"carriermanagement\", \"carrierfuel\", \"socialspace\", \"bartender\" ], \"StationEconomy\":\"$economy_Carrier;\", \"StationEconomy_Localised\":\"Private Enterprise\", \"StationEconomies\":[ {{ \"Name\":\"$economy_Carrier;\", \"Name_Localised\":\"Private Enterprise\", \"Proportion\":1.000000 }} ], \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"{GetCurrentSystem()}\", \"SystemAddress\":285355264323, \"StarPos\":[-49.46875,-55.25000,-360.59375], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"{GetCurrentSystem()}\", \"BodyID\":0, \"BodyType\":\"Star\" }}");
            }

            string filePath;

            // Check if journal files already exist for today
            if (existingJournalFiles.Length > 0)
            {
                // Sort files by name to find the latest one
                Array.Sort(existingJournalFiles);
                filePath = existingJournalFiles[existingJournalFiles.Length - 1];

                // Append to the existing file
                File.AppendAllText(filePath, journalBuilder.ToString());
                AppendToLog($"Appended to existing Journal file at {filePath}");
            }
            else
            {
                // Create a new journal file with today's date
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd'T'HHmmss.ff");
                string fileName = $"Journal.{timestamp}.01.log";
                filePath = Path.Combine(_outputDirectory, fileName);

                File.WriteAllText(filePath, journalBuilder.ToString());
                AppendToLog($"Generated new Journal file at {filePath}");
            }
        }
        #endregion

        #region Helper Methods

        private string GetCurrentTimestamp(int offsetSeconds = 0)
        {
            return DateTime.UtcNow.AddSeconds(offsetSeconds).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        private string GetShipType()
        {
            ComboBoxItem selectedShip = ShipTypeComboBox.SelectedItem as ComboBoxItem;
            if (selectedShip != null)
            {
                return selectedShip.Content.ToString();
            }
            return "Federation_Corvette";
        }

        private string GetShipTypeLocalised()
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

        private string GetCurrentSystem()
        {
            return CurrentSystemTextBox.Text;
        }

        private string GetDockedStatus()
        {
            ComboBoxItem selectedStatus = DockedStatusComboBox.SelectedItem as ComboBoxItem;
            if (selectedStatus != null)
            {
                return selectedStatus.Content.ToString();
            }
            return "Docked";
        }

        private string GetStationName()
        {
            return StationTextBox.Text;
        }

        private long GetBalance()
        {
            long balance;
            if (!long.TryParse(CreditsTextBox.Text, out balance))
            {
                balance = 979884647;
            }
            return balance;
        }

        #endregion

        private void CarrierJumpRequestButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateCarrierJumpRequestEvent();
        }
    }
}