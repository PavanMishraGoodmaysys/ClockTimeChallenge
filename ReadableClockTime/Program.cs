using ClockTimeToWord;
using ClockTimeToWord.BusinessLogicLayer;
using System;
using System.Globalization;
using System.Threading.Tasks;
using static ClockTimeToWord.BusinessLogicLayer.DecisionMaking;
using TimeZone = ClockTimeToWord.BusinessLogicLayer.DecisionMaking.TimeZone;

namespace ClockTime
{
    /// <summary>
    /// Entry point of the project
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the program
        /// </summary>
        private static void Main()
        {
            try
            {
                Console.WriteLine(ConfigurationSetting.InputDescription);
                Console.WriteLine($"{ConfigurationSetting.ExitSuggestion}\n");

                //===== here it will wait for use to input option

                string userInput = Console.ReadLine();
                //--Receive the user input on console
                while (userInput.ToLower(CultureInfo.CurrentCulture) != "quit" && userInput.ToLower(CultureInfo.CurrentCulture) != "q")
                {
                    UserInputType inputType = string.IsNullOrEmpty(userInput) ? UserInputType.CurrentTime : IsInputClockTime(userInput);

                    //===== make decision to display clock time as user inputed
                    switch (inputType)
                    {
                        case UserInputType.CurrentTime: //================ Display current clock time in word
                            {
                                #region Current time
                                string currentServerTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.ToUniversalTime(),
                                                        TimeZoneInfo.FindSystemTimeZoneById(ConfigurationSetting.GetTimeZone(TimeZone.UniversalStandardTime)))
                                                        .ToString(ConfigurationSetting.TimeFormat, CultureInfo.CurrentCulture);

                                Console.WriteLine(new InvokeFactory().GetVerboseStrategy(LanguageEnumerator.English)
                                    .GetClockTimeToWord(currentServerTime).ResultMessage);
                                #endregion                                
                                break;
                            }
                        case UserInputType.ValidClockTime://================== Display time in word as per user input
                            {
                                #region Display clock time as per given input
                                Console.WriteLine(new InvokeFactory().GetVerboseStrategy(LanguageEnumerator.English)
                                    .GetClockTimeToWord(userInput).ResultMessage);
                                #endregion                                
                                break;
                            }
                        case UserInputType.Help:                        
                            Console.WriteLine(ConfigurationSetting.Help);
                            break;
                        default://===================== display message to use in case he/she is not given expected input
                            {
                                Console.WriteLine(ConfigurationSetting.IncorrectInputMessage);                                
                                break;
                            }
                    }
                    userInput = Console.ReadLine();
                }
                //-- Display THANK YOU

                Console.WriteLine(ConfigurationSetting.Thanks);

            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.LogException(ex));
            }
        }

        /// <summary>
        /// This method will validate command input
        /// </summary>
        /// <param name="input">string receive from user</param>
        /// <returns>UserInputType enum</returns>
        private static UserInputType IsInputClockTime(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                    return UserInputType.InvalidClockTime;
                if (input.ToLower() == "help" || input.ToLower() == "h")
                    return UserInputType.Help;
                string[] splitTime = input.Split(':');

                if (splitTime.Length != 2)//==== user input other than the HH:MM format
                    return UserInputType.InvalidClockTime;
                else if (Convert.ToInt32(splitTime[0], CultureInfo.CurrentCulture) < 0 || Convert.ToInt32(splitTime[0], CultureInfo.CurrentCulture) > 24)//=== hours not in range
                    return UserInputType.InvalidClockTime;
                else if (Convert.ToInt32(splitTime[1], CultureInfo.CurrentCulture) < 0 || Convert.ToInt32(splitTime[1], CultureInfo.CurrentCulture) > 59)//=== minutes not in range
                    return UserInputType.InvalidClockTime;

                return UserInputType.ValidClockTime; //=== user input is valid
            }
            catch
            {
                return UserInputType.InvalidClockTime;
            }
        }
    }
}