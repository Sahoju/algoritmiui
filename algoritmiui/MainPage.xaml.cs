using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Input;
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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace algoritmiui {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            // add marker lists for each sorting algorithm

            // 0 = data that's not visible to the user
            // 1 = bubble sort
            // 2 = quick sort
            for (int i = 0; i < 3; i++) {
                List<Marker> markerList = new List<Marker>();
                markers.Add(markerList);
            }

            // one marker to rule the highest size, count and time
            Marker markerKing = new Marker(0, 0, 0);
            markers[0].Add(markerKing);
            // one marker for getting the count and time of BubbleSort
            Marker markerBubble = new Marker(0, 0);
            markers[0].Add(markerBubble);
            // one marker for getting the count and time of QuickSort
            Marker markerQuick = new Marker(0, 0);
            markers[0].Add(markerQuick);

            // initiate blank graph
            DrawBlank();

            // outline text blocks
            // DataMax = maximum data across all tests
            TxtBlock_DataMax.Text = "Maximum data" +
                "\n    Max size: " +
                "\n    Max count: " +
                "\n    Max time: " +
                "\n    Marker count: ";

            // DataCur = previous test's highest data
            TxtBlock_DataCur.Text = "Current data" +
                "\n    Size: " +
                "\n    Iter. count: " +
                "\n    Time: ";

            // DataSpe = specific data from a single marker
            TxtBlock_DataSpe.Text = "Specific data" +
                "\n    Array size: " +
                "\n    Iter. count: " +
                "\n    Time: ";
        }
        
        private List<TextBlock> xMarkers = new List<TextBlock>();
        private List<TextBlock> yMarkers = new List<TextBlock>();
        private List<List<Marker>> markers = new List<List<Marker>>();
        private bool micCheck = false;

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

        private void Btn_Sort_Click(object sender, RoutedEventArgs e) {
            int.TryParse(TxtBox_InputNum.Text, out int amount);

            // check if the amount is more than 0
            // also excludes text
            if (amount > 0) {
                int type = 0;
                // get the algorithm selected from the combo box
                var combo = CB_SortingAlgorithm;
                var item = (ComboBoxItem)combo.SelectedItem;

                // turn the chosen algorithm in the combo box to string, and determine from that which algorithm to use
                // try in case no algorithm is chosen
                try {
                    switch (item.Content.ToString()) {
                        case "Bubble sort":

                            // 1 = bubble sort
                            type = 1;
                            // checking for divisions by zero
                            try {
                                markers = Sorter.BubbleSort(markers, amount);
                            }
                            catch {
                                ShowText("Please put a number above 1000." +
                                    "\nWhy? Because the dev is incompetent, that's why! Piss off!");
                                break;
                            }
                            
                            CheckMax(type);
                            Millisecs.Text = markers[0][type].Time + " ms";
                            markers[0][type].IRCount = 0;
                            break;

                        case "Quick sort":
                            // 2 = quick sort
                            type = 2;
                            markers = Sorter.QuickSort(markers, amount);
                            CheckMax(type);
                            Millisecs.Text = markers[0][type].Time + " ms";
                            markers[0][type].IRCount = 0;
                            break;
                    }
                    
                    DrawMarkers();

                    TxtBlock_DataCur.Text = "Current data" +
                        "\n    Size: " + amount +
                        "\n    Iter. count: " + markers[0][type].IRCount +
                        "\n    Time: " + markers[0][type].Time;

                    TxtBlock_DataMax.Text = "Maximum data" +
                        "\n    Max size: " + markers[0][0].Size +
                        "\n    Max count: " + markers[0][0].IRCount +
                        "\n    Max time: " + markers[0][0].Time +
                        "\n    Marker count: " + (markers[1].Count + markers[2].Count);
                    micCheck = false;
                }
                catch (NullReferenceException) {
                    ShowText("Stop right there criminal scum!" +
                        "\nPay the court a fine or serve your sentence!" +
                        "\nYour stolen goods are now forfeit.");
                }
            }
            else {
                ShowText("What the fuck did you just fucking say about me, you little bitch? I’ll have you know I graduated top of my class in the Navy Seals, and I’ve been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You’re fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that’s just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little “clever” comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn’t, you didn’t, and now you’re paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You’re fucking dead, kiddo.");
            }
        } // private void Btn_Sort_Click

        private void CheckMax(int type) {
            // checks if the size of the array is the largest so far
            if (markers[0][type].Size > markers[0][0].Size) {
                markers[0][0].Size = markers[0][type].Size;
            }
            // checks if the count of iterations/recursions is the largest so far
            if (markers[0][type].IRCount > markers[0][0].IRCount) {
                markers[0][0].IRCount = markers[0][type].IRCount;
            }
            // checks if the time elapsed is the longest so far
            if (markers[0][type].Time > markers[0][0].Time) {
                markers[0][0].Time = markers[0][type].Time;
            }

            int i = 1;
            double data = 0;
            // changing texts of each textblock in the x-axle
            foreach (var textblock in xMarkers) {
                data = (markers[0][0].IRCount / 10) * i;
                textblock.Text = data.ToString();
                i++;
            }
            i = yMarkers.Count();

            // changing texts of each textblock in the x-axle
            foreach (var textblock in yMarkers) {
                data = (markers[0][0].Time / 10) * i;
                textblock.Text = data.ToString();
                i--;
            }
        } // private void CheckMax

        private void DrawMarkers() {
            // clear the canvas first, then create the graph again with new data
            Canvas_Graph.Children.Clear();

            // check all marker lists save for 0
            for (int i = 1; i < markers.Count; i++) {
                foreach (var marker in markers[i]) {
                    // calculating the location in the canvas
                    marker.LocY = -500 * ((double)marker.Time / markers[0][0].Time) + 500;
                    marker.LocX = 700 * ((double)marker.IRCount / markers[0][0].IRCount);

                    // if the markers go below the x-axle or to the left of the y-axle, force them to the axles
                    // 494 = height minus half of the width/height of the ellipse
                    if (marker.LocY > 494) {
                        marker.LocY = 494;
                    }

                    Ellipse ellipse = new Ellipse();
                    ellipse.Width = 12;
                    ellipse.Height = 12;
                    ellipse.Fill = new SolidColorBrush(marker.Colon);
                    Canvas.SetTop(ellipse, marker.LocY);
                    Canvas.SetLeft(ellipse, marker.LocX);
                    Canvas_Graph.Children.Add(ellipse);
                }
            }
        } // private void DrawMarkers

        private void Btn_Clear_Click(object sender, RoutedEventArgs e) {
            Canvas_Graph.Children.Clear();
            markers[0][0].Size = 0;
            markers[0][0].IRCount = 0;
            markers[0][0].Time = 0;
            markers[1].Clear();
            markers[2].Clear();

            TxtBlock_DataMax.Text = "Maximum data" +
                "\n    Max size: " +
                "\n    Max count: " +
                "\n    Max time: " +
                "\n    Marker count: ";

            TxtBlock_DataCur.Text = "Current data" +
                "\n    Size: " +
                "\n    Iter. count: " +
                "\n    Time: ";

            TxtBlock_DataSpe.Text = "Specific data" +
                "\n    Array size: " +
                "\n    Iter. count: " +
                "\n    Time: ";

            // resetting values of each x marker
            foreach (var textblock in xMarkers) {
                textblock.Text = "0";
            }

            // resetting values of each y markers
            foreach (var textblock in yMarkers) {
                textblock.Text = "0";
            }

            Millisecs.Text = "";
            micCheck = false;
        } // private void Btn_Clear_Click

        // get the data from each marker by hovering on top of it
        private void Canvas_GraphBase_PointerMoved(object sender, PointerRoutedEventArgs e) {
            // get the current position of the cursor on Canvas_Graph
            PointerPoint point = e.GetCurrentPoint(Canvas_Graph);
            var x = point.Position.X;
            var y = point.Position.Y;
            bool found = false;
            
            for (int i = 1; i < 3; i++) {
                foreach (var marker in markers[i]) {
                    // if the cursor hits the square(!) area of a marker
                    // 8 = width and height of the marker
                    if (x > marker.LocX && x < marker.LocX + 12 && y > marker.LocY && y < marker.LocY + 12) {
                        // generate a new larger ellipse on top of the marker to show the marker whose data is shown
                        // if a previous large ellipse exists, remove it
                        if (micCheck == true) {
                            Canvas_Graph.Children.RemoveAt(Canvas_Graph.Children.Count - 1);
                        } else {
                            micCheck = true;
                        }
                        Ellipse ellipse = new Ellipse();
                        ellipse.Width = 24;
                        ellipse.Height = 24;
                        ellipse.Fill = new SolidColorBrush(marker.Colon);
                        // 6 = half of the original ellipse's width and height
                        Canvas.SetTop(ellipse, marker.LocY - 6);
                        Canvas.SetLeft(ellipse, marker.LocX - 6);
                        Canvas_Graph.Children.Add(ellipse);

                        TxtBlock_DataSpe.Text = "Specific data" +
                            "\n    Array size: " + marker.Size +
                            "\n    Iter. count: " + marker.IRCount +
                            "\n    Time: " + marker.Time;

                        // stop searching if the cursor is on a marker
                        found = true;
                        break;
                    }
                }
                // stop searching if the cursor is on a marker
                if (found == true) {
                    break;
                }
            }
        } // private void Canvas_GraphBase_PointerMoved

        // for debugging purposes
        private async void ShowText(string text) {
            var dialog = new MessageDialog(text);
            var res = await dialog.ShowAsync();
        } // private void ShowText
        private async void ShowNum(long maxtime, long time) {
            var dialog = new MessageDialog(maxtime + " " + time);
            var res = await dialog.ShowAsync();
        } // private void ShowText
    } // public sealed partial class MainPage : Page
} // namespace algoritmiui
