// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WoWTBGapp.Utils
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        static Settings settings;

        /// <summary>
        /// Gets or sets the current settings. This should always be used
        /// </summary>
        /// <value>The current.</value>
        public static Settings Current
        {
            get { return settings ?? (settings = new Settings()); }
        }


        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        const string PushNotificationsEnabledKey = "push_enabled";
        static readonly bool PushNotificationsEnabledDefault = false;

        const string EmailKey = "email_key";
        readonly string EmailDefault = string.Empty;

        const string DatabaseIdKey = "azure_database";
        static readonly int DatabaseIdDefault = 0;

        const string FirstNameKey = "firstname_key";
        readonly string FirstNameDefault = string.Empty;

        #endregion

        public string Email
        {
            get { return AppSettings.GetValueOrDefault<string>(EmailKey, EmailDefault); }
            set
            {
                if (AppSettings.AddOrUpdateValue(EmailKey, value))
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(UserAvatar));
                }
            }
        }

        public static int DatabaseId
        {
            get { return AppSettings.GetValueOrDefault<int>(DatabaseIdKey, DatabaseIdDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<int>(DatabaseIdKey, value);
            }
        }

        public static int UpdateDatabaseId()
        {
            return DatabaseId++;
        }

        public string FirstName
        {
            get { return AppSettings.GetValueOrDefault<string>(FirstNameKey, FirstNameDefault); }
            set
            {
                if (AppSettings.AddOrUpdateValue(FirstNameKey, value))
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(UserDisplayName));
                }
            }
        }

        const string LastNameKey = "lastname_key";
        readonly string LastNameDefault = string.Empty;
        public string LastName
        {
            get { return AppSettings.GetValueOrDefault<string>(LastNameKey, LastNameDefault); }
            set
            {
                if (AppSettings.AddOrUpdateValue(LastNameKey, value))
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(UserDisplayName));
                }
            }
        }

        public bool PushNotificationsEnabled
        {
            get { return AppSettings.GetValueOrDefault<bool>(PushNotificationsEnabledKey, PushNotificationsEnabledDefault); }
            set
            {
                if (AppSettings.AddOrUpdateValue<bool>(PushNotificationsEnabledKey, value))
                    OnPropertyChanged();
            }
        }


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
            }

        }


        bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected == value)
                    return;
                isConnected = value;
                OnPropertyChanged();
            }
        }


        #region Helpers


        public string UserDisplayName => IsLoggedIn ? $"{FirstName} {LastName}" : "Sign In";

        public string UserAvatar => IsLoggedIn ? Gravatar.GetURL(Email) : "profile_generic.png";

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Email);

        //public bool HasFilters => (ShowPastSessions || FavoritesOnly || (!string.IsNullOrWhiteSpace(FilteredCategories) && !ShowAllCategories));

        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}