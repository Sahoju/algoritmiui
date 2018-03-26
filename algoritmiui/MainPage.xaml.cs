using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace algoritmiui {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            // initiate blank graph
            DrawBlank();
        }

        private List<Line> points = new List<Line>();
        private List<Line> guidelines = new List<Line>();

        private int[] Generate() {
            Random rand = new Random();
            Stopwatch stopwatch = new Stopwatch();
            
            if (int.TryParse(TxtBox_InputNum.Text, out int amount)) {
                int[] intarray = new int[amount];

                // generate numbers between 0 and 100
                stopwatch.Start();
                for (int i = 0; i < amount; i++) {
                    intarray[i] = rand.Next(0, 100);
                }
                stopwatch.Stop();
                Millisecs.Text =  "Generating numbers: " + stopwatch.ElapsedMilliseconds + " ms.";
                return intarray;
            }
            else {
                Millisecs.Text = "What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.";
                return null;
            }
        } // private int[] Generate

        private void Btn_BubbleSort_Click(object sender, RoutedEventArgs e) {
            Stopwatch stopwatch = new Stopwatch();
            int[] intarray = Generate();
            int[] dataTime = new int[10];
            int[] dataCount = new int[10];
            int dataLoc = 0;
            int temp;

            // bubble sort
            stopwatch.Start();
            for (int i = 0; i < intarray.Length - 2; i++) {
                // get data 10 times during a sort
                // Math.Round is used to get an int from double (intarray.Length is always - 2 from its original length)
                // gets both time and count of iterations
                if (i % (Math.Round(intarray.Length / 10.0, 1)) == 0) {
                    dataTime[dataLoc] = (int)stopwatch.ElapsedMilliseconds;
                    dataCount[dataLoc] = i;
                    dataLoc++;
                }
                for (int j = 0; j < intarray.Length - 2; j++) {
                    if (intarray[j] > intarray[j + 1]) {
                        temp = intarray[j + 1];
                        intarray[j + 1] = intarray[j];
                        intarray[j] = temp;
                    }
                }
            }

            stopwatch.Stop();
            Millisecs.Text += " Sorted: " + stopwatch.ElapsedMilliseconds + " ms.";

            TxtBlock_Results.Text = "dataTime: ";
            for (int i = 0; i < 10; i++) {
                TxtBlock_Results.Text += dataTime[i] + ", ";
            }

            TxtBlock_Results.Text += "\ndataCount: ";
            for (int i = 0; i < 10; i++) {
                TxtBlock_Results.Text += dataCount[i] + ", ";
            }


            DrawPoints(intarray.Length, dataTime, dataCount);
        } // private void Btn_BubbleSort_Click

        private void Btn_QuickSort_Click(object sender, RoutedEventArgs e) {
            Stopwatch stopwatch = new Stopwatch();
            int[] intarray = Generate();
            
            // Quick sort
            stopwatch.Start();
            QuickSort(intarray, 0, intarray.Length - 1);
            stopwatch.Stop();

            Millisecs.Text += " Sorted: " + stopwatch.ElapsedMilliseconds + " ms.";
        }

        private void QuickSort(int[] intarray, int left, int right) {
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
            }

            // Recursive calls
            if (left < j) {
                QuickSort(intarray, left, j);
            }

            if (i < right) {
                QuickSort(intarray, i, right);
            }
        } // private void QuickSort

        private void DrawBlank() {
            // create axles
            Line y_axle = new Line();
            Line x_axle = new Line();

            // line positions
            y_axle.X1 = 10;
            y_axle.X2 = 10;
            y_axle.Y1 = 0;
            y_axle.Y2 = 490;
            x_axle.X1 = 10;
            x_axle.X2 = 700;
            x_axle.Y1 = 490;
            x_axle.Y2 = 490;

            // line properties
            y_axle.Stroke = new SolidColorBrush(Colors.Black);
            y_axle.StrokeThickness = 2;
            x_axle.Stroke = new SolidColorBrush(Colors.Black);
            x_axle.StrokeThickness = 2;

            // draw line on canvas
            Canvas_Graph.Children.Add(y_axle);
            Canvas_Graph.Children.Add(x_axle);

            // generate points in the axles
            // i = 1 to not include origin in the calculations
            // i <= 9 because no point needs to be generated for origin and the tip

            // x axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                line.X1 = 69 * i + 10;
                line.X2 = 69 * i + 10;
                line.Y1 = 485;
                line.Y2 = 495;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 2;
                Canvas_Graph.Children.Add(line);
                points.Add(line);
            }

            // y axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                line.X1 = 5;
                line.X2 = 15;
                line.Y1 = 49 * i;
                line.Y2 = 49 * i;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 2;
                Canvas_Graph.Children.Add(line);
                points.Add(line);
            }

            // generate guidelines

            // x axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                line.X1 = 10;
                line.X2 = 700;
                line.Y1 = 49 * i;
                line.Y2 = 49 * i;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                // need to create a new instance of DoubleCollection to be able to add dashed lines
                line.StrokeDashArray = new DoubleCollection() { 3.5 };
                Canvas_Graph.Children.Add(line);
                guidelines.Add(line);
            }

            // y axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                line.X1 = 69 * i + 10;
                line.X2 = 69 * i + 10;
                line.Y1 = 0;
                line.Y2 = 490;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                line.StrokeDashArray = new DoubleCollection() { 3.5 };
                Canvas_Graph.Children.Add(line);
                guidelines.Add(line);
            }
        } // private void DrawBlank

        private void DrawPoints(int arrayLength, int[] dataTime, int[] dataCount) {
            Ellipse point = new Ellipse();
            point.Width = 5;
            point.Height = 5;
            point.Fill = new SolidColorBrush(Colors.SteelBlue);

            Canvas.SetTop(point, 436);
            Canvas.SetLeft(point, 74);

            Canvas_Graph.Children.Add(point);


        }
    } // public sealed partial class MainPage : Page
} // namespace algoritmiui
