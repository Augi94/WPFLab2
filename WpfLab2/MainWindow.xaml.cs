using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int widthLocation = 10;
        bool homeVersion = false;
        Button clickedButton;
        List<Object> hvShapes = new List<Object>();
        List<Button> buttonList = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawNote(Button but, bool inHomeVersion)
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Black),
                Height = 20,
                Width = 30,
            };
            ellipse.MouseEnter += ellipse_MouseEnter;
            ellipse.MouseLeave += ellipse_MouseLeave;
            ellipse.MouseUp += ellipse_MouseUp;
            Line line = new Line
            {
                X1 = widthLocation - 5,
                X2 = widthLocation + 35,
                Y1 = 150,
                Y2 = 150,
                Tag = but.Tag.ToString(),
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 3
            };
            Label label = new Label
            {
                FontSize = 13,
                FontWeight = FontWeights.Bold
            };
            if (inHomeVersion)
            {
                label.FontWeight = FontWeights.Normal;
                ellipse.Fill = new SolidColorBrush(Colors.White);
                ellipse.Stroke = new SolidColorBrush(Colors.Black);
                hvShapes.Add(ellipse);
                hvShapes.Add(line);
                hvShapes.Add(label);
                line.StrokeThickness = 2;
                ellipse.StrokeDashArray = new DoubleCollection() { 4 };
            }
            switch (but.Tag.ToString())
            {
                case "C":
                    Canvas.SetTop(ellipse, 140);
                    canvas.Children.Add(line);
                    break;
                case "D":
                    Canvas.SetTop(ellipse, 130);
                    break;
                case "E":
                    Canvas.SetTop(ellipse, 120);
                    break;
                case "F":
                    Canvas.SetTop(ellipse, 110);
                    break;
                case "G":
                    Canvas.SetTop(ellipse, 100);
                    break;
                case "A":
                    Canvas.SetTop(ellipse, 90);
                    break;
                case "B":
                    Canvas.SetTop(ellipse, 80);
                    break;
                case "C2":
                    Canvas.SetTop(ellipse, 70);
                    break;
                case "D2":
                    Canvas.SetTop(ellipse, 60);
                    break;
                case "E2":
                    Canvas.SetTop(ellipse, 50);
                    break;
                case "F2":
                    Canvas.SetTop(ellipse, 40);
                    break;
                case "G2":
                    Canvas.SetTop(ellipse, 30);
                    break;
                case "A2":
                    Canvas.SetTop(ellipse, 20);
                    line.Y1 = 30;
                    line.Y2 = 30;
                    canvas.Children.Add(line);
                    break;

            }
            Canvas.SetLeft(label, widthLocation + 7);
            Canvas.SetTop(label, 170);

            Canvas.SetLeft(ellipse, widthLocation);
            label.Content = but.Tag.ToString().ElementAt(0);
            ellipse.Tag = but.Tag.ToString();
            canvas.Children.Add(label);
            canvas.Children.Add(ellipse);
            if (!inHomeVersion)
                widthLocation += 45;
        }

        void ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var el = (Ellipse)sender;
            if (homeVersion)
            {
                var brush = new SolidColorBrush(Colors.Black);
                colorElementsOnCanvas(brush);
                brush = new SolidColorBrush(Colors.Orange);
                el.Fill = brush;
                foreach (UIElement sh in canvas.Children)
                {
                    if (sh is Line && ((Line)sh).Tag.ToString() == el.Tag.ToString())
                    {
                        ((Line)sh).Stroke = brush;
                    }
                    if (sh is Ellipse && ((Ellipse)sh).Tag.ToString() == el.Tag.ToString())
                    {
                        ((Ellipse)sh).Fill = brush;
                    }
                }
                foreach (Button b in buttonList)
                {
                    if (b.Tag.ToString() == el.Tag.ToString())
                    {
                        b.Background = brush; // orange

                    }
                    else
                        b.Background = new SolidColorBrush(Colors.White);
                }

            }
        }
        private void colorElementsOnCanvas(SolidColorBrush brush)
        {
            foreach (UIElement sh in canvas.Children)
            {
                if (sh is Line)
                    ((Line)sh).Stroke = brush;
                if (sh is Ellipse)
                    ((Ellipse)sh).Fill = brush;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            if (!buttonList.Contains(but))
                buttonList.Add(but);
            clickedButton = but;
            var brush = new SolidColorBrush(Colors.Black);
            if (homeVersion)
            {
                but.Background = brush;
                colorElementsOnCanvas(brush);
                foreach (Button b in buttonList)
                    if (b != clickedButton)
                        b.Background = new SolidColorBrush(Colors.White);
            }

            drawNote(but, false);
            brush = new SolidColorBrush(Colors.SkyBlue);

            if (homeVersion)
            {
                but.Background = brush;
                foreach (UIElement sh in canvas.Children)
                {
                    if (sh is Line && ((Line)sh).Tag.ToString() == but.Tag.ToString())
                        ((Line)sh).Stroke = brush;
                    if (sh is Ellipse && ((Ellipse)sh).Tag.ToString() == but.Tag.ToString())
                        ((Ellipse)sh).Fill = brush;
                }
            }
        }

        void ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            var el = (Ellipse)sender;
            Color c = ((SolidColorBrush)el.Fill).Color;
            if (c != Colors.Orange && c != Colors.SkyBlue)
                el.Fill = new SolidColorBrush(Colors.Black);
        }

        void ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            var el = (Ellipse)sender;
            Color c = ((SolidColorBrush)el.Fill).Color;
            if (c != Colors.Orange && c != Colors.SkyBlue)
                el.Fill = new SolidColorBrush(Colors.Gray);
        }

        private void aMouseEnter(object sender, MouseEventArgs e)
        {
            var but = (Button)sender;
            Color c = ((SolidColorBrush)but.Background).Color;
            if (clickedButton != but && c!=Colors.Orange)
                but.Background = new SolidColorBrush(Colors.Gray);
            if (homeVersion)
                drawNote(but, true);
        }

        private void aMouseLeave(object sender, MouseEventArgs e)
        {
            var but = (Button)sender;
            Color c = ((SolidColorBrush)but.Background).Color;
            if ((clickedButton != but && c != Colors.Orange) || !homeVersion)
                but.Background = new SolidColorBrush(Colors.White);
            if (homeVersion)
                foreach (UIElement sh in hvShapes)
                    canvas.Children.Remove(sh);
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            homeVersion = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            homeVersion = false;
            foreach (Button b in buttonList)
                b.Background = new SolidColorBrush(Colors.White);
            colorElementsOnCanvas(new SolidColorBrush(Colors.Black));
        }
    }
}
