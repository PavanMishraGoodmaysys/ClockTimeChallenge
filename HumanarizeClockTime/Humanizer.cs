using ClockTimeToWord.BusinessLogicLayer;
using ClockTimeToWord.Model;
using Humanizer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClockTimeToWord.Model.StatusModel;

namespace ClockTimeToWord
{
    /// <summary>
    /// Class to handle the string coversion of time.
    /// Implements interface IHumanizer
    /// </summary>
    public class ClockTimeEnglish : IHumanizer
    {
        private static readonly List<int> hoursRange = Enumerable.Range(0, 24).ToList();
        private static readonly List<int> minutesRange = Enumerable.Range(0, 60).ToList();
        #region VALIDATION METHOD
        /// <summary>
        /// The method takes two integer input parameters, one for hours and other one for minutes
        /// It validates the input, and returns validation result accordingly
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <returns>Status</returns>
        private static Status IsValidationPassed(int hours, int minutes)
        {
            try
            {
                StringBuilder validationSms = new StringBuilder();
                if (hours < 0 || minutes < 0)
                    validationSms.AppendLine(HumanarizeClockTime.Resource.RemarkResource.NegativeValue);

                if (hours >= ConfigurationSetting.MaxHours)
                    validationSms.AppendLine(HumanarizeClockTime.Resource.RemarkResource.HoursGreaterThan23);

                if (minutes >= ConfigurationSetting.MinutesThreshold)
                    validationSms.AppendLine(HumanarizeClockTime.Resource.RemarkResource.MinutesGreaterThan59);

                if (validationSms.Length > 0)
                {
                    return new Status { IsValid = false, Message = string.Join(", ", validationSms) };
                }

                return new Status { IsValid = true, Message = "" };
            }
            catch
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// It is the entry method when user request arrives
        /// Implements method from the interface IHumanizer
        /// </summary>
        /// <param name="clockTime"></param>        
        /// <returns>Response</returns>
        public Response GetClockTimeToWord(string clockTime)
        {
            
            try
            {
                if (string.IsNullOrEmpty(clockTime))
                    return new Response { IsSuccess = false, ResultMessage = ConfigurationSetting.InvalidInput };

                string[] splitGmtTime = clockTime.Split(':');
                if (splitGmtTime.Count() < 2 || splitGmtTime.Count() > 2)
                {
                    return new Response { IsSuccess = false, ResultMessage = ConfigurationSetting.InvalidInput };
                }
                int minutes = Convert.ToInt32(splitGmtTime[1], CultureInfo.InvariantCulture);
                Status validate = IsValidationPassed(Convert.ToInt32(splitGmtTime[0], CultureInfo.InvariantCulture), minutes);

                if (!validate.IsValid)
                    return new Response { IsSuccess = false, ResultMessage = validate.Message };

                FillHours();
                FillMinutes();

                return GenerateClockTimeWord(Convert.ToInt32(splitGmtTime[0], CultureInfo.InvariantCulture), minutes);
            }
            catch (Exception ex)
            {
                Task.Run(() => ExceptionHandler.LogException(ex));
                return new Response { IsSuccess = false, ResultMessage = ConfigurationSetting.IncorrectInputMessage };
            }
        }

        #region Clock time to word
        /// <summary>
        /// it will be called after validation passed
        /// it will convert clock timer to human redable word
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        private static Response GenerateClockTimeWord(int hours, int minutes)
        {
            try
            {
                int hrs = minutes > ConfigurationSetting.GetMaxPastHoursMinutes                       ? (hours + 1 == ConfigurationSetting.MaxHours ? ConfigurationSetting.HoursIn12Format : hours + 1)
                       : (hours == 0 || hours == ConfigurationSetting.MaxHours ? ConfigurationSetting.HoursIn12Format : hours);

                string humanReadableClockTime = hours > 0 && minutes == 0
                    ? $"{hourMapping[hours].ToWords()} {minuteMapping[minutes]}"
                    : (hours == 0 && minutes == 0
                            ? (hours == 0 || hours == ConfigurationSetting.MaxHours
                                        ? $"{ConfigurationSetting.MidnightText} {hourMapping[hrs].ToWords()} {minuteMapping[minutes]}"
                                        : $"{hourMapping[hrs].ToWords()} {minuteMapping[minutes]}")
                            : $"{minuteMapping[minutes].Replace("-", " ")} {hourMapping[hrs].ToWords()}");

                humanReadableClockTime = humanReadableClockTime.Humanize();
                humanReadableClockTime = humanReadableClockTime.Replace("O clock", "O'clock");

                return new Response { IsSuccess = true, ResultMessage = humanReadableClockTime };
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// hours concurent dictionary which is thread safe and it is initialised inside the method FillHours()
        /// </summary>
        private static ConcurrentDictionary<int, int> hourMapping = new ConcurrentDictionary<int, int>();

        /// <summary>
        /// minutes concurent dictionary which is thread safe and it is initialised inside the method FillMins()
        /// </summary>
        private static ConcurrentDictionary<int, string> minuteMapping = new ConcurrentDictionary<int, string>();

        /// <summary>
        /// used to inititalise the hours dictionary used to generate the human readable hours word
        /// </summary>
        private static void FillHours()
        {
            int hoursIn12Formate = ConfigurationSetting.HoursIn12Format;
            Parallel.ForEach(hoursRange, h => hourMapping.TryAdd(h, h > hoursIn12Formate ? h - hoursIn12Formate : h));
        }
        /// <summary>
        /// used to inititalise the minutes dictionary used to generate the human readable minute word
        /// </summary>
        private static void FillMinutes()
        {
            Parallel.ForEach(minutesRange, m => minuteMapping.TryAdd(m, (m <= ConfigurationSetting.GetMinuteMaxPast || m == 45)
                ? GetMinWordLessEqual30(m)
                : $"{(ConfigurationSetting.MinutesThreshold - m).ToWords()} to"));
        }

        /// <summary>
        /// This method is used to convert minutes time to verbose
        /// used in method FillMins()
        /// </summary>
        /// <param name="minute"></param>
        /// <returns>string</returns>
        private static string GetMinWordLessEqual30(int minute)
        {
            string minuteInward;
            string past = ConfigurationSetting.GetPastText;
            switch (minute)
            {
                case 0:
                    {
                        minuteInward = ConfigurationSetting.GetClockHourText;
                        break;
                    }
                case 15:
                    {
                        minuteInward = $"{ConfigurationSetting.QuarterText} {past}";
                        break;
                    }
                case 30:
                    {
                        minuteInward = $"{ConfigurationSetting.HalfText} {past}";
                        break;
                    }
                case 45:
                    {
                        minuteInward = $"{ConfigurationSetting.QuarterText} to";
                        break;
                    }
                default:
                    {
                        minuteInward = $"{minute.ToWords()} {past}";
                        break;
                    }
            }
            return minuteInward;
        }
        #endregion
    }
}
