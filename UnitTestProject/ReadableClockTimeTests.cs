using ClockTimeToWord;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static ClockTimeToWord.BusinessLogicLayer.DecisionMaking;

namespace UnitTestProject
{
    [TestClass]
    public class ReadableClockTimeTests
    {
        /// <summary>
        /// Test current clock time
        /// </summary>
        [TestMethod]
        public void Test_GetReadableClockTime_CurrentTimeTest()
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            string currentGmtTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.ToUniversalTime(), tzi).ToString("HH:MM");

            InvokeFactory objectFactory = new InvokeFactory();
            ClockTimeToWord.Model.Response res = objectFactory.GetVerboseStrategy(LanguageEnumerator.English).GetClockTimeToWord(currentGmtTime);

            Assert.IsTrue(res.IsSuccess);
        }

        /// <summary>
        /// Positive test cases expected correct input from user
        /// </summary>
        [TestMethod]
        public void Test_GetReadableClockTime_InputTime_Positive()
        {
            List<TestDataModel> clockTimeList = CommonMethod.Get_PositiveTestData();
            clockTimeList.ForEach(x =>
            {
                InvokeFactory objectFactory = new InvokeFactory();
                ClockTimeToWord.Model.Response res = objectFactory.GetVerboseStrategy(LanguageEnumerator.English).GetClockTimeToWord(x.ClockTime);
                CommonMethod.PrintTestResultInTextFile(res.ResultMessage);

                Assert.IsTrue(res.IsSuccess);
            });
        }
        /// <summary>
        /// Negative test cases. Handles invalid input from the user.
        /// </summary>
        [TestMethod]
        public void Test_GetReadableClockTime_InputTime_Negative()
        {
            List<TestDataModel> clockTimeList = CommonMethod.Get_NegativeTestData();
            clockTimeList.ForEach(x =>
            {
                InvokeFactory objectFactory = new InvokeFactory();
                ClockTimeToWord.Model.Response res = objectFactory.GetVerboseStrategy(LanguageEnumerator.English).GetClockTimeToWord(x.ClockTime);
                CommonMethod.PrintTestResultInTextFile(res.ResultMessage);

                Assert.IsTrue(!res.IsSuccess);
            });
        }
    }
}
