using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI;
using algoritmiui;

namespace algoritmiui {
    static class Sorter {
        private static Stopwatch stopwatch = new Stopwatch();

        private static int[] Generate(int amount) {
            Random rand = new Random();
            int[] intarray = new int[amount];

            // generate numbers between 0 and 100
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < amount; i++) {
                intarray[i] = rand.Next(0, 100);
            }
            stopwatch.Stop();
            return intarray;
        } // private int[] Generate

        public static List<List<Marker>> BubbleSort(List<List<Marker>> markers, int amount) {
            int count = 0;
            int[] intarray = Generate(amount);
            int temp;
            int everyMs = 5;
            long previousElapsedMilliseconds = 0;

            if (intarray.Length <= 10000) {
                everyMs = 5;
            } else if (intarray.Length <= 25000) {
                everyMs = 10;
            } else if (intarray.Length <= 50000) {
                everyMs = 50;
            } else { everyMs = 100; }

            // bubble sort
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < intarray.Length - 2; i++) {
                for (int j = 0; j < intarray.Length - 2; j++) {
                    if (intarray[j] > intarray[j + 1]) {
                        temp = intarray[j + 1];
                        intarray[j + 1] = intarray[j];
                        intarray[j] = temp;
                    }
                }
                // count the amount of iterations
                count++;
                stopwatch.Stop();

                // if stopwatch.ElapsedMilliseconds is dividable with everyMs
                // and if previous milliseconds aren't the same as current elapsed milliseconds
                // then create a new marker
                // this is to avoid the program making many markers with the same elapsed milliseconds
                // in the graph those markers show up on top of each other, not making it a smooth line
                if (stopwatch.ElapsedMilliseconds % everyMs == 0 && previousElapsedMilliseconds != stopwatch.ElapsedMilliseconds) {
                    Marker marker = new Marker(intarray.Length, count, stopwatch.ElapsedMilliseconds, Colors.SteelBlue);
                    markers[1].Add(marker);
                    previousElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                }
                stopwatch.Start();
            }

            stopwatch.Stop();

            if (count > markers[0][0].IRCount) {
                markers[0][0].IRCount = count;
            }

            return markers;
        } // private void Btn_BubbleSort_Click

        public static List<List<Marker>> QuickSort(List<List<Marker>> markers, int amount) {
            int[] intarray = Generate(amount);
            float everyMs = 1;
            long previousElapsedMilliseconds = 0;
            
            /*
            if (intarray.Length <= 10000) {
                everyMs = 5;
            } else if (intarray.Length <= 25000) {
                everyMs = 10;
            } else if (intarray.Length <= 50000) {
                everyMs = 50;
            } else { everyMs = 100; }
            */

            // Quick sort
            stopwatch.Reset();
            stopwatch.Start();
            markers = SortQuickly(intarray, 0, intarray.Length - 1, markers, everyMs, previousElapsedMilliseconds);
            stopwatch.Stop();

            if (markers[0][1].IRCount > markers[0][0].IRCount) {
                markers[0][0].IRCount = markers[0][1].IRCount;
            }

            return markers;
        } // private void Btn_QuickSort_Click

        private static List<List<Marker>> SortQuickly(int[] intarray, int left, int right, List<List<Marker>> markers, float everyMs, long previousElapsedMilliseconds) {
            if (right > 0) {
                int i = left, j = right;
                int pivot = intarray[(left + right) / 2];

                while (i <= j) {
                    // compares pivot to numbers on its left.
                    // when pivot is bigger than left, it searches from the next place in the array.
                    // only when the number on left is higher or equal than pivot will it stop comparing.
                    while (intarray[i].CompareTo(pivot) < 0) {
                        i++;
                    }

                    // compares pivot to numbers on its right.
                    // when pivot is smaller than right, it searches from the next place in the array.
                    // only when the number on right is smaller or equal than pivot will it stop comparing.
                    while (intarray[j].CompareTo(pivot) > 0) {
                        j--;
                    }

                    // compares the places of left and right in the array.
                    // if left is really in the left side of the pivot and right is in the right, then swap places.
                    if (i <= j) {
                        // Swap
                        int temp = intarray[i];
                        intarray[i] = intarray[j];
                        intarray[j] = temp;

                        i++;
                        j--;
                    }
                } // while (i <= j)

                markers[0][1].IRCount++;
                if (stopwatch.ElapsedMilliseconds % everyMs == 0 && stopwatch.ElapsedMilliseconds != previousElapsedMilliseconds) {
                    Marker marker = new Marker(intarray.Length, markers[0][1].IRCount, stopwatch.ElapsedMilliseconds, Colors.OrangeRed);
                    markers[2].Add(marker);
                    previousElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                }

                // Recursive calls
                if (left < j) {
                    markers = SortQuickly(intarray, left, j, markers, everyMs, previousElapsedMilliseconds);
                }

                if (i < right) {
                    markers = SortQuickly(intarray, i, right, markers, everyMs, previousElapsedMilliseconds);
                }
            }

            return markers;
        } // private void QuickSort
    }
}
