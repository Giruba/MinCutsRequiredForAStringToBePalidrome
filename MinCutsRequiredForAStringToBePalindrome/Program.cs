using System;

namespace MinCutsRequiredForAStringToBePalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Minimum number of cuts required for a string" +
                " to be a palindrome!");
            Console.WriteLine();

            Console.WriteLine("Enter the string");
            try
            {
                String input = Console.ReadLine();
                Console.WriteLine("Minimum number of cuts required to " +
                    "make "+input+" a palindrome is "+GetMinCutsForPalindrome(input));
            }
            catch (Exception exception) {
                Console.WriteLine("Thrown exception is " +
                    ""+exception.Message);
            }

            Console.ReadLine();
        }

        private static int GetMinCutsForPalindrome(string inputWord) {
            int lengthOfString = inputWord.Length;
            int[] cuts = new int[lengthOfString+1];

            bool[,] palindromePartitions = new bool[lengthOfString, lengthOfString];

            for (int index = 0; index < lengthOfString; index++) {
                palindromePartitions[index, index] = true;
            }

            for (int lengthOfCurrentWord = 2; lengthOfCurrentWord <= lengthOfString; lengthOfCurrentWord++) {
                for (int startIndexOfCurrentWord = 0; startIndexOfCurrentWord < lengthOfString - lengthOfCurrentWord + 1; startIndexOfCurrentWord++) {
                    int endIndexOfCurrentWord = startIndexOfCurrentWord + lengthOfCurrentWord - 1;

                    if (lengthOfCurrentWord == 2){
                        palindromePartitions[startIndexOfCurrentWord, endIndexOfCurrentWord] =
                           (inputWord[startIndexOfCurrentWord] == inputWord[endIndexOfCurrentWord]);
                    }
                    else {
                        palindromePartitions[startIndexOfCurrentWord, endIndexOfCurrentWord] =
                           (inputWord[startIndexOfCurrentWord] == inputWord[endIndexOfCurrentWord]) &&
                           palindromePartitions[startIndexOfCurrentWord + 1, endIndexOfCurrentWord - 1];
                    }
                }
            }


            for (int indexOverPartitons = 0; indexOverPartitons < lengthOfString; indexOverPartitons++) {
                if (palindromePartitions[0, indexOverPartitons])
                {
                    cuts[indexOverPartitons] = 0;
                }
                else {
                    cuts[indexOverPartitons] = int.MaxValue;
                    for (int indexOverPossiblePartitions = 0; indexOverPossiblePartitions < indexOverPartitons; indexOverPossiblePartitions++) {
                        if (palindromePartitions[indexOverPossiblePartitions + 1, indexOverPartitons] &&
                            cuts[indexOverPossiblePartitions] + 1 < cuts[indexOverPartitons]) {
                            cuts[indexOverPartitons] = 1 + cuts[indexOverPossiblePartitions];
                        }
                    }
                }
            }

            return cuts[lengthOfString-1];
        }
    }
}
