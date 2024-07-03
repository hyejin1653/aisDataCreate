using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aisDataCreate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pg = new Program();
            //return resultArray;
            string[] resultArray = pg.createData();
            string[] changeArray = pg.changeInt(resultArray);
            Console.WriteLine(changeArray[0]);

        }

        private string[] createData()
        {
            Random rand = new Random();
            string[] resultArray = new string[30000];
            string longitudeA = Convert.ToString((rand.Next(12400, 13300) * 0.01) * 600000.0); //경도
            //string latitude = Convert.ToString((rand.Next(3000, 3900) * 0.01) * 600000.0); //위도

            //이진법 형태로 배열에 넣어주기
            for (int i = 0; i < resultArray.Length; i++)
            {
                //string mmsi = rand.Next(0, 999999999).ToString("D9"); //mmsi

                string mmsi = "440123400";
                string longitude = Convert.ToString(127.85 * 600000.0);
                string latitude = Convert.ToString(34.37 * 600000.0);
                string status = "15";
                string rot = "0";
                string sog = "15";
                string cog = "8";
                string heading = "6";
                string eta = "08131500";

                string mmsiStr = Convert.ToString(Convert.ToInt32(mmsi), 2);
                string longitudeStr = Convert.ToString(Convert.ToInt32(longitude), 2);
                string latitudeStr = Convert.ToString(Convert.ToInt32(latitude), 2);

                string statusStr = Convert.ToString(Convert.ToInt32(status), 2); 
                string rotStr = Convert.ToString(Convert.ToInt32(rot), 2);
                string sogStr = Convert.ToString(Convert.ToInt32(sog), 2);
                string cogStr = Convert.ToString(Convert.ToInt32(cog), 2);
                string headingStr = Convert.ToString(Convert.ToInt32(heading), 2);
                string etaStr = Convert.ToString(Convert.ToInt32(eta), 2);

                int[] lengthArray = new int[16] { 6, 2, 30, 4, 8, 10, 1, 28, 27, 12, 9, 6, 2, 3, 1, 19 };

                string result = ""; //2진법으로 변경된 데이터를 한 변수에다가 담아서 처리!
                //[2] - MMSI, [7]/[8] - Longitude/Latitude 랜덤숫자 들어가야함!
                for (int l = 0; l < lengthArray.Length; l++)
                {
                    if (l == 0) { result = result + "000001"; }
                    else if (l == 2) { result = result + mmsiStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 3) { result = result + statusStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 4) { result = result + rotStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 5) { result = result + sogStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 7) { result = result + longitudeStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 8) { result = result + latitudeStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 9) { result = result + cogStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 10) { result = result + headingStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else if (l == 11) { result = result + etaStr.PadLeft(lengthArray[l], '0').ToString(); }
                    else
                    {
                        result = result + Convert.ToString(0, 2).PadLeft(lengthArray[l], '0').ToString();
                    }
                }
                resultArray[i] = result;
            }

            return resultArray;
        }

        private string[] changeInt(string[] resultArray)
        {
            string[] returnArray = new string[30000];

            for (int i = 0; i < resultArray.Length; i++)
            {
                string result = "";

                int cnt = resultArray[i].Length / 6;

                for (int l = 0; l < cnt; l++)
                {
                    string binStr = resultArray[i].Substring(l * 6, 6);
                    int changeInt = Convert.ToInt32(binStr, 2);
                    char strCh = Convert.ToChar(changeInt);
                    changeInt = changeInt + 48;
                    if (changeInt >= 88)
                    {
                        changeInt = changeInt + 8;
                    }
                    char code = Convert.ToChar(changeInt);
                    result = result + code;
                }

                returnArray[i] = "!AIVDM,1,1,,A," + result + ",0*53";

            }

            return returnArray;
        }
    }
}
