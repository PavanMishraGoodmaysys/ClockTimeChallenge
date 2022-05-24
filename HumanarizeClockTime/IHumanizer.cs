
using ClockTimeToWord.Model;

namespace ClockTimeToWord
{
    /// <summary>
    /// To achieve the polymorphism
    /// </summary>
    public interface IHumanizer
    {
        /// <summary>
        /// Method used as a contract to implement in the inheriting class
        /// It receives string parameter containing information about hour and minute(s)
        /// </summary>
        /// <param name="clockTime"></param>        
        /// <returns>Response</returns>
        Response GetClockTimeToWord(string clockTime);
    }
}
