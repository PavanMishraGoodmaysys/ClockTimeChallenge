
using System.ComponentModel;

namespace ClockTimeToWord.BusinessLogicLayer
{
    /// <summary>
    /// Decision Making
    /// </summary>
    public static class DecisionMaking
    {
        /// <summary>
        /// Language enum to display clock time in word
        /// </summary>
        public enum LanguageEnumerator
        {
            /// <summary>
            /// To display clock time in English language
            /// </summary>
            English,
            /// <summary>
            /// To display clock time in Hindi language
            /// </summary>
            Hindi
        }
        /// <summary>
        /// TimeZone enums
        /// </summary>
        public enum TimeZone
        {
            /// <summary>
            /// GMT Standard Time
            /// </summary>
            [Description("GMT Standard Time")]
            UniversalStandardTime,
            /// <summary>
            /// India Standard Time
            /// </summary>
            [Description("India Standard Time")]
            IndiaStandardTime
        }
        /// <summary>
        /// User input validation result enums
        /// </summary>
        public enum UserInputType
        {
            /// <summary>
            /// given input is valid
            /// </summary>
            ValidClockTime,
            /// <summary>
            /// given input is not valid
            /// </summary>
            InvalidClockTime,
            /// <summary>
            /// Display curent time
            /// </summary>
            CurrentTime,
            /// <summary>
            /// display help
            /// </summary>
            Help
        }
    }
}
