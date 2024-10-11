using System.Globalization;
using UC.Core.Helpers;

namespace uc.api.cms.Common
{
    public class BTCGEN
    {
        private static string lcvTypeSerialDay = "D";
        private static string lcvTypeSerialWeek = "W";
        private static string lcvTypeSerialMonth = "M";
        private static string lcvTypeSerialYear = "Y";
        private static int[] DayOfWeek = new[] { 0, 1, 2, 3, 4, 5, 6, 7 }; 


        public static string FormatChronologyCaption(DateTime inputDate, string formatType)
        {
            string rs = string.Empty;
            if(formatType == "dd-MM-yyyy")
            {
                rs = inputDate.ToString("dd-MM-yyyy");
            }    
            return rs;
        }
        //What function work
        public static List<DateTime> GenFrequenceToReleaseDate(string pvvFrequence, DateTime startDate, DateTime endDate)
        {
            List<DateTime> rs = new List<DateTime>();
            DateTime releaseDate;
            string lvvFrequenceHeader = pvvFrequence[0].ToString();
            int lvnFrequenceNum = int.Parse(pvvFrequence.Substring(1, 1));
            int i = pvvFrequence.IndexOf('#');
            int j = pvvFrequence.IndexOf(':');
            int lvvPublishTimes = 0;
            string lvvFrequenceDetail = null;

            if (i > 0)
            {
                if (j > 0)
                {
                    lvvPublishTimes = int.Parse(pvvFrequence.Substring(i + 1, j - i - 1));
                }
                else
                {
                    lvvPublishTimes = int.Parse(pvvFrequence.Substring(i + 1, 1));
                }
            }

            if (j > 0)
            {
                lvvFrequenceDetail = pvvFrequence.Substring(j + 1).Trim();
            }

            if (lvvFrequenceHeader == lcvTypeSerialDay)
            {
                List<int> getDayOfWeekFrequence = new List<int>();
                if (lvnFrequenceNum == 8)
                {
                    getDayOfWeekFrequence.AddRange(DayOfWeek);
                }
                else if (new[] { 2, 3, 4, 5, 6, 7 }.Contains(lvnFrequenceNum))
                {
                    getDayOfWeekFrequence.AddRange(DayOfWeek.Skip(1).Take(lvnFrequenceNum));
                }

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (getDayOfWeekFrequence.Contains((int)date.DayOfWeek))
                    {
                        try
                        {
                            rs.Add(date);
                        }
                        catch { }
                    }
                }
            }
            else if (lvvFrequenceHeader == lcvTypeSerialMonth)
            {
                string[] splitDates = null, splitDate = null;
                int month = 0, year = 0, numOfDate = 0, dayOfDate = 0, monthOfDate = 0;
                for (var date = startDate; date <= endDate; date = date.AddMonths(lvnFrequenceNum))
                {
                    month = date.Month;
                    year = date.Year;

                    if (!string.IsNullOrEmpty(lvvFrequenceDetail))
                    {
                        splitDates = lvvFrequenceDetail.Split(",");
                        for (int o = 0; o < splitDates.Length; o++)
                        {
                            splitDate = splitDates[o].Split(".");
                            dayOfDate = int.Parse(splitDate[0]);
                            numOfDate = int.Parse(splitDate[1]);

                            monthOfDate = month + numOfDate - 1;
                            if (monthOfDate >= 1 && monthOfDate <= 12)
                            {
                                try
                                {
                                    releaseDate = new DateTime(year, monthOfDate, dayOfDate);
                                    if (startDate <= releaseDate && endDate >= releaseDate)
                                    {
                                        rs.Add(releaseDate);
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        for (int o = 0; o < lvvPublishTimes; o++)
                        {
                            try
                            {
                                releaseDate = new DateTime(year, month, 1);
                                rs.Add(releaseDate);
                            }
                            catch { }
                        }
                    }
                }
            }
            else if (lvvFrequenceHeader == lcvTypeSerialWeek)
            {
                string[] splitWeeks = null, splitWeek = null;
                int startWeek = ISOWeek.GetWeekOfYear(startDate), endWeek = ISOWeek.GetWeekOfYear(endDate);
                int startYear = startDate.Year, endYear = endDate.Year;
                int weeksInYear;
                int dateOfWeek, numOfWeek, dayOfDate, monthOfDate;
                for (int y = startYear; y <= endYear; y++)
                {
                    weeksInYear = ISOWeek.GetWeeksInYear(y);
                    if (startYear == endYear)
                    {
                        if (endWeek >= weeksInYear && endWeek != 1)
                        {
                            weeksInYear = endWeek;
                        }
                        if (endWeek == 1)
                        {
                            weeksInYear += 1;
                        }

                        for (int sWeek = startWeek; sWeek <= weeksInYear; sWeek += lvnFrequenceNum)
                        {
                            if (!string.IsNullOrEmpty(lvvFrequenceDetail))
                            {
                                splitWeeks = lvvFrequenceDetail.Split(",");
                                for (int o = 0; o < splitWeeks.Length; o++)
                                {
                                    splitWeek = splitWeeks[o].Split(".");
                                    dateOfWeek = int.Parse(splitWeek[0]);
                                    if (dateOfWeek == 8)
                                    {
                                        dateOfWeek = 0;
                                    }
                                    else
                                    {
                                        dateOfWeek -= 1;
                                    }

                                    numOfWeek = int.Parse(splitWeek[1]);
                                    numOfWeek = sWeek + numOfWeek - 1;
                                    try
                                    {
                                        releaseDate = DateTimeHelpers.GetDateFromWeekOfYear((System.DayOfWeek)dateOfWeek, numOfWeek, y);
                                        if (startDate <= releaseDate && endDate >= releaseDate)
                                        {
                                            rs.Add(releaseDate);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                for (int o = 0; o < lvvPublishTimes; o++)
                                {
                                    try
                                    {
                                        releaseDate = new DateTime(y, 1, 1);
                                        rs.Add(releaseDate);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    else if (startYear < endYear)
                    {
                        if (y == endYear)
                        {
                            startWeek = 1;
                            weeksInYear = endYear;
                        }

                        for (int sWeek = startWeek; sWeek <= weeksInYear; sWeek += lvnFrequenceNum)
                        {
                            if (!string.IsNullOrEmpty(lvvFrequenceDetail))
                            {
                                splitWeeks = lvvFrequenceDetail.Split(",");
                                for (int o = 0; o < splitWeeks.Length; o++)
                                {
                                    splitWeek = splitWeeks[o].Split(".");
                                    dateOfWeek = int.Parse(splitWeek[0]);
                                    if (dateOfWeek == 8)
                                    {
                                        dateOfWeek = 0;
                                    }
                                    else
                                    {
                                        dateOfWeek -= 1;
                                    }

                                    numOfWeek = int.Parse(splitWeek[1]);
                                    numOfWeek = sWeek + numOfWeek - 1;
                                    try
                                    {
                                        releaseDate = DateTimeHelpers.GetDateFromWeekOfYear((System.DayOfWeek)dateOfWeek, numOfWeek, y);
                                        if (startDate <= releaseDate && endDate >= releaseDate)
                                        {
                                            rs.Add(releaseDate);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                for (int o = 0; o < lvvPublishTimes; o++)
                                {
                                    try
                                    {
                                        releaseDate = new DateTime(y, 1, 1);
                                        rs.Add(releaseDate);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
            else if (lvvFrequenceHeader == lcvTypeSerialYear)
            {
                string[] splitYears = null, splitYear = null;
                int startYear = startDate.Year, endYear = endDate.Year;
                int monthOfYear = 0, numOfYear = 0;
                for (int y = startYear; y <= endYear; y++)
                {
                    if (!string.IsNullOrEmpty(lvvFrequenceDetail))
                    {
                        splitYears = lvvFrequenceDetail.Split(",");
                        for (int o = 0; o < splitYears.Length; o++)
                        {
                            splitYear = splitYears[o].Split(".");
                            monthOfYear = int.Parse(splitYear[0]);
                            numOfYear = int.Parse(splitYear[1]);

                            monthOfYear = monthOfYear + numOfYear - 1;
                            releaseDate = new DateTime(y, monthOfYear, 1);
                            if (startDate <= releaseDate && endDate >= releaseDate)
                            {
                                try
                                {
                                    rs.Add(releaseDate);
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        for (int o = 0; o < lvvPublishTimes; o++)
                        {
                            releaseDate = new DateTime(y, 1, 1);
                            if (startDate <= releaseDate && endDate >= releaseDate)
                            {
                                try
                                {
                                    rs.Add(releaseDate);
                                }
                                catch { }
                            }
                        }
                    }    
                }
            }

            return rs;
        }
        // .....
        public static string GetFrequenceToText(string pvvFrequence, int pvnHeaderPos = 0)
        {
            string lvvFrequenceBrief = null;
            string lvvFrequenceHeader = pvvFrequence[0].ToString();
            int lvnFrequenceNum = int.Parse(pvvFrequence.Substring(1, 1));
            int i = pvvFrequence.IndexOf('#');
            int j = pvvFrequence.IndexOf(':');
            int lvvPublishTimes = 0;
            string lvvFrequenceDetail = null;

            if (i > 0)
            {
                if (j > 0)
                {
                    lvvPublishTimes = int.Parse(pvvFrequence.Substring(i + 1, j - i - 1));
                }
                else
                {
                    lvvPublishTimes = int.Parse(pvvFrequence.Substring(i + 1, 1));
                }
            }

            if (j > 0)
            {
                lvvFrequenceDetail = pvvFrequence.Substring(j + 1).Trim();
            }

            
            if (lvvFrequenceHeader == lcvTypeSerialDay)
            {
                lvvFrequenceBrief = GetStringSerial(lvvFrequenceHeader, lvnFrequenceNum, lvvPublishTimes);
            }
            else if (lvvFrequenceHeader == lcvTypeSerialWeek ||
                     lvvFrequenceHeader == lcvTypeSerialMonth ||
                     lvvFrequenceHeader == lcvTypeSerialYear)
            {
                if (!string.IsNullOrEmpty(lvvFrequenceDetail))
                {
                    lvvFrequenceBrief = GetStringSerial(lvvFrequenceHeader, lvnFrequenceNum, lvvPublishTimes)
                                        + ": "
                                        + GetStringDetail(lvvFrequenceHeader, lvvFrequenceDetail);
                }
                else
                {
                    lvvFrequenceBrief = GetStringSerial(lvvFrequenceHeader, lvnFrequenceNum, lvvPublishTimes);
                }
            }

            lvvFrequenceBrief = lvvFrequenceBrief.ToLower();

            if (pvnHeaderPos == 1 && lvvFrequenceBrief.Contains(":"))
            {
                lvvFrequenceBrief = lvvFrequenceBrief.Substring(0, lvvFrequenceBrief.IndexOf(':'));
            }

            return lvvFrequenceBrief;
        }

        private static string GetStringSerial(string pvvSerialType, int pvnFrequenceNum, int pvvPublishTimes)
        {
            string lvvSerialType = pvvSerialType;
            int lvnFrequenceNum = pvnFrequenceNum;
            int lvvPublishTimes = pvvPublishTimes;
            string lvvStringSerial = null;

            try
            {
                if (lvvSerialType == lcvTypeSerialDay)
                {
                    if (lvnFrequenceNum == 8)
                    {
                        lvvStringSerial = "hàng ngày(từ thứ 2 - CN)";
                    }
                    else if (new[] { 2, 3, 4, 5, 6, 7 }.Contains(lvnFrequenceNum))
                    {
                        lvvStringSerial = $"hàng ngày(từ thứ 2 - {pvnFrequenceNum})";
                    }
                    else
                    {
                        throw new Exception($"Sai kiểu định kì. Kiểu sai là: {lvvSerialType}");
                    }
                }
                else
                {
                    // Handle cases W, M, Y
                    if (lvvSerialType == lcvTypeSerialWeek)
                    {
                        lvvStringSerial = "tuần";
                    }
                    else if (lvvSerialType == lcvTypeSerialMonth)
                    {
                        lvvStringSerial = "tháng";
                    }
                    else if (lvvSerialType == lcvTypeSerialYear)
                    {
                        lvvStringSerial = "năm";
                    }
                    else
                    {
                        throw new Exception($"Sai kiểu định kì. Kiểu sai là: {lvvSerialType}");
                    }

                    lvvStringSerial = $"{NumToLettered(pvnFrequenceNum).Trim()} {lvvStringSerial.Trim()} {NumToLettered(lvvPublishTimes).Trim()} kỳ phát hành";
                }

                return lvvStringSerial;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        private static string GetStringDetail(string pvvSerialType, string pvvFrequenceNum)
        {
            string lvvSerialType = pvvSerialType;
            string lvvFrequenceNum = pvvFrequenceNum;

            int lvnLengthFrequenceNum;
            string lpvStringSerialHeader = ""; // Part before the main string
            string lpvStringSerial = null; // Main part of the string
            int lvnPosition = -1; // Position of the special character ','
            int lvnPos = -1; // Position of the special character '.'
            string lvvSequenceValue = null; // Store value of a specific sequence like 1.2
            string lvvSequenceReturn = null;

            try
            {
                if (lvvSerialType == null)
                {
                    return null;
                }

                // Handle cases for W, M, Y
                switch (lvvSerialType)
                {
                    case "W":
                        lpvStringSerial = "của tuần thứ";
                        lpvStringSerialHeader = "thứ";
                        break;
                    case "M":
                        lpvStringSerial = "của tháng thứ";
                        lpvStringSerialHeader = "ngày";
                        break;
                    case "Y":
                        lpvStringSerial = "của năm thứ";
                        lpvStringSerialHeader = "tháng thứ";
                        break;
                    default:
                        throw new Exception($"Sai kiểu định kì. Kiểu sai là: {lvvSerialType}");
                }

                // Process the details for cases like 2.1, 15.1, 30.1, 15.2, etc.
                lvnLengthFrequenceNum = lvvFrequenceNum.Length;
                while (lvvFrequenceNum.Length > 0)
                {
                    lvnPosition = lvvFrequenceNum.IndexOf(',');

                    // Extract segments like 2.1 for processing
                    if (lvnPosition > 0)
                    {
                        lvvSequenceValue = lvvFrequenceNum.Substring(0, lvnPosition);
                    }
                    else
                    {
                        lvvSequenceValue = lvvFrequenceNum;
                    }

                    // Process each segment 2.1
                    lvnPos = lvvSequenceValue.IndexOf('.');
                    if (lvvSerialType == lcvTypeSerialWeek)
                    {
                        lvvSequenceReturn = (lvvSequenceReturn ?? "") + ", " +
                            ConvertDateToChar(lvvSequenceValue.Substring(0, lvnPos).Trim()) + " " +
                            lpvStringSerial + " " +
                            lvvSequenceValue.Substring(lvnPos + 1).Trim();
                    }
                    else
                    {
                        lvvSequenceReturn = (lvvSequenceReturn ?? "") + ", " +
                            lpvStringSerialHeader + " " +
                            lvvSequenceValue.Substring(0, lvnPos).Trim() + " " +
                            lpvStringSerial + " " +
                            lvvSequenceValue.Substring(lvnPos + 1).Trim();
                    }

                    lvvFrequenceNum = lvnPosition > 0 ? lvvFrequenceNum.Substring(lvnPosition + 1) : "";
                    if (lvnPosition <= 0) break;
                }

                return lvvSequenceReturn?.Trim(", ".ToCharArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        private static string ConvertDateToChar(string pvnNDate)
        {
            string lvvStrDate;

            try
            {
                switch (pvnNDate)
                {
                    case "2":
                        lvvStrDate = "thứ hai";
                        break;
                    case "3":
                        lvvStrDate = "thứ ba";
                        break;
                    case "4":
                        lvvStrDate = "thứ tư";
                        break;
                    case "5":
                        lvvStrDate = "thứ năm";
                        break;
                    case "6":
                        lvvStrDate = "thứ sáu";
                        break;
                    case "7":
                        lvvStrDate = "thứ bảy";
                        break;
                    case "8":
                        lvvStrDate = "chủ nhật";
                        break;
                    default:
                        lvvStrDate = "thứ " + pvnNDate;
                        break;
                }

                return lvvStrDate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string NumToLettered(int digitalCurrency, string tag = null)
        {
            int numLen;
            double digCur = digitalCurrency;
            double tmp1 = 0, tmp2 = 0;
            string retVal = null;
            int digit = 0;

            Dictionary<int, string> letteredNum = new Dictionary<int, string>();
            Dictionary<int, string> levelOfDigit = new Dictionary<int, string>();

            // Procedure to assign values based on Vietnamese number names
            void AssignKeyWords()
            {
                letteredNum[0] = null;
                letteredNum[1] = "một";
                letteredNum[2] = "hai";
                letteredNum[3] = "ba";
                letteredNum[4] = "bốn";
                letteredNum[5] = "năm";
                letteredNum[6] = "sáu";
                letteredNum[7] = "bảy";
                letteredNum[8] = "tám";
                letteredNum[9] = "chín";
                letteredNum[10] = "linh";
                letteredNum[11] = "mười";
                letteredNum[12] = "mười";
                letteredNum[13] = "mười";
                letteredNum[14] = "mười";
                letteredNum[15] = "mười";
                letteredNum[16] = "mười";
                letteredNum[17] = "mười";
                letteredNum[18] = "mười";
                letteredNum[19] = "mười";
                letteredNum[20] = "mốt";
                letteredNum[21] = "lăm";

                levelOfDigit[1] = tag;
                levelOfDigit[2] = "mươi";
                levelOfDigit[3] = "trăm";
                levelOfDigit[4] = "nghìn";
                levelOfDigit[5] = "mươi";
                levelOfDigit[6] = "trăm";
                levelOfDigit[7] = "triệu";
                levelOfDigit[8] = "mươi";
                levelOfDigit[9] = "trăm";
                levelOfDigit[10] = "tỷ";
                levelOfDigit[11] = "mươi";
                levelOfDigit[12] = "trăm";
                levelOfDigit[13] = "ngìn tỷ";
                levelOfDigit[14] = "mươi";
                levelOfDigit[15] = "trăm";
                levelOfDigit[16] = "nghìn nghìn tỷ";
                levelOfDigit[17] = "mươi";
                levelOfDigit[18] = "trăm";
            }

            if (digCur == 0)
            {
                retVal = "không " + tag;
                return retVal;
            }

            AssignKeyWords();
            numLen = digCur.ToString().Length;

            for (int i = 1; i <= numLen; i++)
            {
                tmp2 = tmp1;
                tmp1 = digit;
                digit = (int)(digCur % 10);
                digCur = Math.Truncate(digCur / 10);

                if (digit == 0)
                {
                    if (i % 3 == 1)
                    {
                        if ((digCur % 10 == 0) && (Math.Truncate(digCur / 10) % 10 == 0) && (i != 1))
                        {
                            retVal = letteredNum[digit] + retVal;
                        }
                        else
                        {
                            retVal = letteredNum[digit] + levelOfDigit[i] + " " + retVal;
                        }
                    }
                    else if (i % 3 == 2 && tmp1 != 0)
                    {
                        if (i == 2 || (digCur % 10) != 0)
                        {
                            retVal = letteredNum[10] + " " + retVal;
                        }
                    }
                    else if (i % 3 == 0 && tmp1 != 0 && tmp2 != 0)
                    {
                        retVal = "không" + levelOfDigit[i] + " " + retVal;
                    }
                }
                else
                {
                    if (digit == 1)
                    {
                        if ((digCur % 10 != 1) && (digCur % 10 != 0) && (i % 3 == 1))
                        {
                            retVal = letteredNum[20] + " " + levelOfDigit[i] + " " + retVal;
                        }
                        else if (i % 3 == 2)
                        {
                            retVal = letteredNum[digit + 10] + " " + retVal;
                        }
                        else
                        {
                            retVal = letteredNum[digit] + " " + levelOfDigit[i] + " " + retVal;
                        }
                    }
                    else if (digit == 5 && i % 3 == 1 && (digCur % 10 != 0))
                    {
                        retVal = letteredNum[21] + " " + levelOfDigit[i] + " " + retVal;
                    }
                    else
                    {
                        retVal = letteredNum[digit] + " " + levelOfDigit[i] + " " + retVal;
                    }
                }
            }

            retVal = char.ToUpper(retVal[0]) + retVal.Substring(1);
            return retVal.Trim();
        }
    }
}
