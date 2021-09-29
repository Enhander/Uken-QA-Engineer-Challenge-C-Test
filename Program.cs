using System;
using System.IO;
using System.Collections.Generic;

namespace UkenQAEngineerChallengeCSharpChallenge {
    public class CSharpChallenge {
        public static List<int> LoadNumberSet (string filePath) {
            StreamReader streamReader = new StreamReader(filePath);

            List<int> numberSet = new List<int>();
            string nextLine;

            while ((nextLine = streamReader.ReadLine()) != null) {
                numberSet.Add(Int32.Parse(nextLine));
            }

            return numberSet;
        }

        public static Dictionary<int, int> CountFrequencies (List<int> numberSet) {
            Dictionary<int, int> frequencies = new Dictionary<int, int>();

            foreach (int number in numberSet) {
                if (!frequencies.ContainsKey(number)) {
                    frequencies.Add(number, 1);
                }
                else {
                    frequencies[number]++;
                }
            }

            return frequencies;
        }

        private static Tuple<int, int> FindLeastFrequentNumbers(List<int> numberSet) {
            Dictionary<int, int> frequencies = CountFrequencies(numberSet);
            List<int> leastFrequentNumbers = new List<int>();
            int leastFrequentNumber = Int32.MaxValue;
            int lowestFrequency = Int32.MaxValue;

            foreach (KeyValuePair<int, int> frequency in frequencies) {
                if (frequency.Value < lowestFrequency) {
                    lowestFrequency = frequency.Value;
                    leastFrequentNumbers.Clear();
                    leastFrequentNumbers.Add(frequency.Key);
                }
                else if (frequency.Value == lowestFrequency) {
                    leastFrequentNumbers.Add(frequency.Key);
                }
            }

            if (leastFrequentNumbers.Count > 1) {
                leastFrequentNumbers.Sort();
            }
            leastFrequentNumber = leastFrequentNumbers[0];

            Tuple<int, int> result = new Tuple<int, int>(leastFrequentNumber, lowestFrequency);

            return result;
        }

        public static void Main(string[] args) {
            string directoryPath = @".\src";
            string[] filePaths = Directory.GetFiles(directoryPath);

            foreach (string filePath in filePaths) {
                List<int> numberSet = LoadNumberSet(filePath);
                Tuple<int, int> result = FindLeastFrequentNumbers(numberSet);

                string fileName = Path.GetFileName(filePath);
                int leastFrequentNumber = result.Item1;
                int lowestFrequency = result.Item2;

                Console.WriteLine("File: " + fileName + ", Number: " + leastFrequentNumber + ", Repeated: " + lowestFrequency + " times");
            }

            Console.ReadLine();
        }
    }
}
