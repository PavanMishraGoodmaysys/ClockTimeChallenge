using Humanizer;

namespace ClockTimeToWord.BusinessLogicLayer
{
    /// <summary>
    /// Configuration Setting created to put all the configuration related settings
    /// </summary>
    public static class ConfigurationSetting
    {
        /// <summary>
        /// Method will return the expected time format
        /// </summary>
        /// <returns>string</returns>
        public static string TimeFormat => "HH:mm";
        /// <summary>
        /// return the time zone
        /// </summary>
        /// <param name="zone"></param>
        /// <returns></returns>
        public static string GetTimeZone(DecisionMaking.TimeZone zone) => zone.Humanize();
        /// <summary>
        /// display message on load
        /// </summary>
        public static string InputDescription => HumanarizeClockTime.Resource.RemarkResource.DisplayInputDescription;
        /// <summary>
        /// Display how to exist options from program
        /// </summary>
        public static string ExitSuggestion => HumanarizeClockTime.Resource.RemarkResource.ExitSuggestion;
        /// <summary>
        /// Return incorrect input message
        /// </summary>
        public static string IncorrectInputMessage => HumanarizeClockTime.Resource.RemarkResource.IncorrectInputMessage;
        /// <summary>
        /// Return incorrect input message
        /// </summary>
        public static string InvalidInput => HumanarizeClockTime.Resource.RemarkResource.InvalidInput;
        /// <summary>
        /// Minute threshold value
        /// </summary>
        public static int MinutesThreshold => 60;
        /// <summary>
        /// Minute mid value
        /// </summary>
        public static int GetMinuteMaxPast => 30;
        /// <summary>
        /// time formate in 12 hour
        /// </summary>
        public static int HoursIn12Format => 12;
        /// <summary>
        /// Max hours
        /// </summary>
        public static int MaxHours => 24;
        /// <summary>
        /// Return max past hours value
        /// </summary>
        public static int GetMaxPastHoursMinutes => 30;
        /// <summary>
        /// midnight text
        /// </summary>
        public static string MidnightText => "Midnight";
        /// <summary>
        /// exact clock time text
        /// </summary>
        public static string GetClockHourText => "O'clock";
        /// <summary>
        /// test is used to denote 15 and 45
        /// </summary>
        public static string QuarterText => "Quarter";
        /// <summary>
        /// test for 30 minute
        /// </summary>
        public static string HalfText => "Half";
        /// <summary>
        /// past text used when minute <= 30
        /// </summary>
        public static string GetPastText => "past";
        /// <summary>
        /// Display help text
        /// </summary>
        public static string Help => HumanarizeClockTime.Resource.RemarkResource.Help;
        public static string Thanks => HumanarizeClockTime.Resource.RemarkResource.Thanks;
    }
}
