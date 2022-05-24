using System.Collections.Concurrent;
using static ClockTimeToWord.BusinessLogicLayer.DecisionMaking;

namespace ClockTimeToWord
{
    /// <summary>
    /// the class is dedicated only to create and return the user expected instance
    /// </summary>
    public class InvokeFactory // give some suitable name
    {
        /// <summary>
        /// This dictionary create to hold the object of different classed and return as per user requirement
        /// </summary>
        private static ConcurrentDictionary<LanguageEnumerator, IHumanizer> verboseStrategy = new ConcurrentDictionary<LanguageEnumerator, IHumanizer>();
        public InvokeFactory()
        {
            verboseStrategy[LanguageEnumerator.English] = new ClockTimeEnglish();
        }
        /// <summary>
        /// Return instance of requested class as per given input
        /// </summary>
        /// <param name="language">enum</param>
        /// <returns>object</returns>
        public IHumanizer GetVerboseStrategy(LanguageEnumerator language)
        {
            return verboseStrategy[language];
        }

    }
}
