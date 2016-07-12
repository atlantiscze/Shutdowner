using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shutdowner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyShutdownTimer myTimer;
        public int SMALL_VALUE = 1;      // Buttons with "+" or "-" will adjust the value by this number.
        public int LARGE_VALUE = 10;     // Buttons with "++" or "--" will adjust the value by this number.

        public MainWindow()
        {
            myTimer = new MyShutdownTimer(this);
            InitializeComponent();
            // Initialise the timer and set the initial time to five minutes.
            myTimer.setTime(0, 5, 0); // This also updates the UI values.
        }

        // Start/Stop buttons

        private void btnStartTimer_Click(object sender, RoutedEventArgs e)
        {
            myTimer.startTimer();
        }

        private void btnStopTimer_Click(object sender, RoutedEventArgs e)
        {
            myTimer.stopTimer();
        }

        // Hour adjustment buttons

        private void btnAddHour_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustHours(SMALL_VALUE);
        }
        private void btnAddHours_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustHours(LARGE_VALUE);
        }
        private void btnRemoveHours_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustHours(-LARGE_VALUE);
        }
        private void btnRemoveHour_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustHours(-SMALL_VALUE);
        }

        // Minute adjustment buttons

        private void btnAddMinute_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustMinutes(SMALL_VALUE);
        }
        private void btnAddMinutes_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustMinutes(LARGE_VALUE);
        }
        private void btnRemoveMinutes_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustMinutes(-LARGE_VALUE);
        }
        private void btnRemoveMinute_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustMinutes(-SMALL_VALUE);
        }

        // Second adjustment buttons

        private void btnAddSecond_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustSeconds(SMALL_VALUE);
        }
        private void btnAddSeconds_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustSeconds(LARGE_VALUE);
        }
        private void btnRemoveSeconds_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustSeconds(-LARGE_VALUE);
        }
        private void btnRemoveSecond_Click(object sender, RoutedEventArgs e)
        {
            myTimer.adjustSeconds(-SMALL_VALUE);
        }

        // Shutdown mode radio button toggles

        private void rbShutdownRegular_Checked(object sender, RoutedEventArgs e)
        {
            myTimer.setShutdownMode(ShutdownMode.SHUTDOWN_NORMAL);
        }
        private void rbShutdownForced_Checked(object sender, RoutedEventArgs e)
        {
            myTimer.setShutdownMode(ShutdownMode.SHUTDOWN_FORCED);
        }
        private void rbShutdownReboot_Checked(object sender, RoutedEventArgs e)
        {
            myTimer.setShutdownMode(ShutdownMode.SHUTDOWN_REBOOT);
        }
        private void rbShutdownSleep_Checked(object sender, RoutedEventArgs e)
        {
            myTimer.setShutdownMode(ShutdownMode.SHUTDOWN_SLEEP);
        }

        // On minimizing hides the program from task bar. The toolbar icon remains active and can be doubleclicked to open the program again.

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized && (cbBetterMinimising.IsChecked == true))
            {
                Hide();
                myTimer.onMinimise();
            }
        }
    }
}
