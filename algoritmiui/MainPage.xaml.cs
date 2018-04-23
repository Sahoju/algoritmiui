using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using Windows.UI.Popups;
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

            // add marker lists for each sorting algorithm

            // 0 = aaaaa
            // 1 = bubble sort
            // 2 = quick sort
            for (int i = 0; i < 3; i++) {
                List<Marker> markerList = new List<Marker>();
                markers.Add(markerList);
            }

            // one marker to rule the highest size, count and time
            Marker markerKing = new Marker(0, 0, 0);
            markers[0].Add(markerKing);
            // one marker for getting the count of QuickSort
            Marker markerQuick = new Marker(0);
            markers[0].Add(markerQuick);

            // initiate blank graph
            DrawBlank();
        }
        
        private List<TextBlock> xMarkers = new List<TextBlock>();
        private List<TextBlock> yMarkers = new List<TextBlock>();
        private List<List<Marker>> markers = new List<List<Marker>>();

        private void DrawBlank() {
            // create text blocks
            for (int i = 0; i < 10; i++) {
                TextBlock textblock = new TextBlock();
                textblock.Height = 16;
                textblock.Width = 60;
                textblock.FontSize = 12;
                textblock.Margin = new Thickness(0, 0, 0, 30);
                textblock.Text = "0";
                Grid.SetRow(textblock, i);
                yMarkers.Add(textblock);
                Grid_MarkerTextY.Children.Add(textblock);
            }
            for (int i = 0; i < 10; i++) {
                TextBlock textblock = new TextBlock();
                textblock.Height = 16;
                textblock.Width = 60;
                textblock.FontSize = 12;
                textblock.Margin = new Thickness(5, 0, 0, 0);
                textblock.Text = "0";
                Grid.SetColumn(textblock, i);
                xMarkers.Add(textblock);
                Grid_MarkerTextX.Children.Add(textblock);
            }

            // create axles
            Line y_axle = new Line();
            Line x_axle = new Line();

            // line positions
            y_axle.X1 = 0;
            y_axle.X2 = 0;
            y_axle.Y1 = 0;
            y_axle.Y2 = 500;
            x_axle.X1 = 0;
            x_axle.X2 = 700;
            x_axle.Y1 = 500;
            x_axle.Y2 = 500;

            // line properties
            y_axle.Stroke = new SolidColorBrush(Colors.Black);
            y_axle.StrokeThickness = 2;
            x_axle.Stroke = new SolidColorBrush(Colors.Black);
            x_axle.StrokeThickness = 2;

            // draw line on canvas
            Canvas_GraphBase.Children.Add(y_axle);
            Canvas_GraphBase.Children.Add(x_axle);

            // generate reference marks in the axles
            // i = 1 to not include origin in the calculations
            // i <= 9 because no point needs to be generated for origin and the tip

            // x-axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                // the points are 70 pixels apart from each other and are 10 pixels long
                line.X1 = 70 * i;
                line.X2 = 70 * i;
                line.Y1 = 490;
                line.Y2 = 500;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 2;
                Canvas_GraphBase.Children.Add(line);
            }

            // y axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                // the points are 50 pixels apart from each other and are 10 pixels long
                line.X1 = 0;
                line.X2 = 10;
                line.Y1 = 50 * i;
                line.Y2 = 50 * i;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 2;
                Canvas_GraphBase.Children.Add(line);
            }

            // generate guidelines

            // x axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                line.X1 = 0;
                line.X2 = 700;
                line.Y1 = 50 * i;
                line.Y2 = 50 * i;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                // need to create a new instance of DoubleCollection to be able to add dashed lines
                line.StrokeDashArray = new DoubleCollection() { 3.5 };
                Canvas_GraphBase.Children.Add(line);
            }

            // y axle
            for (int i = 1; i <= 9; i++) {
                Line line = new Line();
                line.X1 = 70 * i;
                line.X2 = 70 * i;
                line.Y1 = 0;
                line.Y2 = 500;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                line.StrokeDashArray = new DoubleCollection() { 3.5 };
                Canvas_GraphBase.Children.Add(line);
            }
        } // private void DrawBlank

        private void DrawMarkers() {
            Canvas_Graph.Children.Clear();

            for(int i = 0; i < markers.Count; i++) {
                foreach(var marker in markers[i]) {
                    marker.LocY = -500 * ((double)marker.Time / (double)markers[0][0].Time) + 500;
                    marker.LocX = 700 * ((double)marker.Count / (double)markers[0][0].Count);

                    // if the markers go below the x-axle or to the left of the y-axle, force them to the axles
                    /*
                    if (marker.LocX < 4) {
                        marker.LocX = 4;
                    }
                    */
                    if (marker.LocY > 496) {
                        marker.LocY = 496;
                    }

                    Ellipse ellipse = new Ellipse();
                    ellipse.Width = 8;
                    ellipse.Height = 8;
                    ellipse.Fill = new SolidColorBrush(marker.Colon);
                    Canvas.SetTop(ellipse, marker.LocY);
                    Canvas.SetLeft(ellipse, marker.LocX);
                    Canvas_Graph.Children.Add(ellipse);
                }
            }
        }
        
        private void DrawLines() {
            bool first = true;
            double previousLocX = 0;
            double previousLocY = 0;

            foreach (var marker in markers[0]) {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(marker.Colon);
                
                if(first) {
                    line.X1 = 0;
                    line.Y1 = 500;
                    line.X2 = marker.LocX - 4;
                    line.Y2 = marker.LocY - 4;
                    previousLocX = marker.LocX;
                    previousLocY = marker.LocY;
                    first = false;
                }
                else {
                    line.X1 = previousLocX;
                    line.Y1 = previousLocY;
                    line.X2 = marker.LocX - 4;
                    line.Y2 = marker.LocY - 4;
                    previousLocX = marker.LocX;
                    previousLocY = marker.LocY;
                }

                line.StrokeThickness = 2;
                Canvas_Graph.Children.Add(line);

                /*
                if (marker.Type == 0 && bubbleFirst == true) {
                    line.X1 = 10;
                    line.Y1 = 490;
                    line.X2 = marker.LocX + 4;
                    line.Y2 = marker.LocY + 4;
                    bubblePreviousLocX = marker.LocX;
                    bubblePreviousLocY = marker.LocY;
                    line.Stroke = new SolidColorBrush(Colors.SteelBlue);
                    bubbleFirst = false;
                } else if (marker.Type == 1 && quickFirst == true) {
                    line.X1 = 10;
                    line.Y1 = 490;
                    line.X2 = marker.LocX + 4;
                    line.Y2 = marker.LocY + 4;
                    quickPreviousLocX = marker.LocX;
                    quickPreviousLocY = marker.LocY;
                    line.Stroke = new SolidColorBrush(Colors.OrangeRed);
                    quickFirst = false;
                } else if (marker.Type == 0) {
                    line.X1 = bubblePreviousLocX + 4;
                    line.Y1 = bubblePreviousLocY + 4;
                    line.X2 = marker.LocX + 4;
                    line.Y2 = marker.LocY + 4;
                    bubblePreviousLocX = marker.LocX;
                    bubblePreviousLocY = marker.LocY;
                    line.Stroke = new SolidColorBrush(Colors.SteelBlue);
                } else {
                    line.X1 = quickPreviousLocX + 4;
                    line.Y1 = quickPreviousLocY + 4;
                    line.X2 = marker.LocX + 4;
                    line.Y2 = marker.LocY + 4;
                    quickPreviousLocX = marker.LocX;
                    quickPreviousLocY = marker.LocY;
                    line.Stroke = new SolidColorBrush(Colors.OrangeRed);
                }
                
                line.StrokeThickness = 2;
                Canvas_Graph.Children.Add(line);
                */
            }
        } // private void DrawLine

        private async void ShowText(string text) {
            var dialog = new MessageDialog(text);
            var res = await dialog.ShowAsync();
        }

        private void CheckMax(Marker marker) {
            // checks if the size of the array is the largest so far
            if (marker.Size > markers[0][0].Size) {
                markers[0][0].Size = marker.Size;
            }
            // checks if the count of iterations/recursions is the largest so far
            if (marker.Count > markers[0][0].Count) {
                markers[0][0].Count = marker.Count;
            }
            // checks if the time elapsed is the longest so far
            if (marker.Time > markers[0][0].Time) {
                markers[0][0].Time = marker.Time;
            }

            int i = 1;
            long data = 0;
            // changing texts of each x marker
            foreach (var textblock in xMarkers) {
                data = (markers[0][0].Time / 10) * i;
                textblock.Text = data.ToString();
                i++;
            }
            i = yMarkers.Count();

            // changing texts of each y markers
            foreach (var textblock in yMarkers) {
                data = (markers[0][0].Count / 10) * i;
                textblock.Text = data.ToString();
                i--;
            }
        }

        private void Btn_Sort_Click(object sender, RoutedEventArgs e) {
            int.TryParse(TxtBox_InputNum.Text, out int amount);

            if (amount > 0) {
                int type = 0;
                var combo = CB_SortingAlgorithm;
                var item = (ComboBoxItem)combo.SelectedItem;
                switch (item.Content.ToString()) {
                    case "Bubble sort":
                        type = 1;
                        markers[type] = Sorter.BubbleSort(markers[type], amount);
                        CheckMax(markers[type][markers[type].Count - 1]);
                        Millisecs.Text = markers[1][markers[1].Count - 1].Time + " ms";
                        break;
                    case "Quick sort":
                        markers = Sorter.QuickSort(markers, amount);
                        CheckMax(markers[2][markers[2].Count - 1]);
                        Millisecs.Text = markers[2][markers[2].Count - 1].Time + " ms";
                        markers[0][1].Count = 0;
                        break;
                }

                DrawMarkers();
            }
        }
    } // public sealed partial class MainPage : Page
} // namespace algoritmiui
