using UnityEngine;
using UnityEngine.Assertions;

namespace KeeperHelper.Utils
{
    public static class CSVReader
    {
        public static string[][] Read(string resourcePath, params char[] delimiters)
        {
            TextAsset csvData = Resources.Load<TextAsset>(resourcePath);
            Assert.IsNotNull(csvData);

            string[] lines = csvData.text.Split('\n');

            string[][] data = new string[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] column = lines[i].Split(delimiters);
                data[i] = column;
            }

            return data;
        }
    }
}