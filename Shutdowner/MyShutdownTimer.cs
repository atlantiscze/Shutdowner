using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;

namespace Shutdowner
{
    // Handles majority of actual logic in the program.
    public class MyShutdownTimer
    {
        bool isRunning = false;

        int seconds = 0;
        int minutes = 0;
        int hours = 0;

        DispatcherTimer timer;
        MainWindow window;
        ShutdownMode mode;
        NotifyIcon icon;

        // Constructor
        public MyShutdownTimer(MainWindow w)
        {
            window = w;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += tick;
            mode = ShutdownMode.SHUTDOWN_NORMAL;
            icon = new NotifyIcon();
            icon.Icon = Properties.Resources.shutdowner_icon;
            icon.Visible = true;
            icon.DoubleClick += restoreWindow;
        }

        // Triggered by our DispatcherTimer every tick.
        private void tick(object sender, EventArgs e)
        {
            adjustSeconds(-1);
            updateUi();
            if (isRunning && seconds == 0 && minutes == 0 && hours == 0)
            {
                onTimerCompletion();
            }
        }

        // Called automatically once the timer reaches zero.
        private void onTimerCompletion()
        {
            stopTimer();
            updateUi();
            beginShutdown();
        }

        // Force-updates our main window UI with current seconds/minutes/hours.
        public void updateUi()
        {
            window.tbHours.Text = hours.ToString();
            window.tbMinutes.Text = minutes.ToString();
            window.tbSeconds.Text = seconds.ToString();
            if (isRunning)
            {
                window.btnStartTimer.IsEnabled = false;
                window.btnStopTimer.IsEnabled = true;
                icon.Text = "System shutdown in: " + hours + ":" + minutes + ":" + seconds;
                if ((window.cbSendNotifications.IsChecked == true) && (getSecondsLeft() <= 60))
                {
                    icon.ShowBalloonTip(1000, "Shutdowner v1.4", "System shutdown in: " + icon.Text, ToolTipIcon.Info);
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            else
            {
                window.btnStartTimer.IsEnabled = true;
                window.btnStopTimer.IsEnabled = false;
                icon.Text = "Stopped";
            }
            
        }

        // Begins the shutdown/reboot/sleep by calling apropriate OS functions.
        private void beginShutdown()
        {
            ProcessStartInfo psi = null;
            switch (mode)
            {
                case ShutdownMode.SHUTDOWN_NORMAL:
                {
                    psi = new ProcessStartInfo("shutdown", "/s /t 0");
                    break;
                }
                case ShutdownMode.SHUTDOWN_FORCED:
                {
                    psi = new ProcessStartInfo("shutdown", "/s /t 0 /f");
                    break;
                }
                case ShutdownMode.SHUTDOWN_REBOOT:
                {
                    psi = new ProcessStartInfo("shutdown", "/r /t 0");
                    break;
                }
                case ShutdownMode.SHUTDOWN_SLEEP:
                {
                    setTime(0, 5, 0);
                    System.Windows.Forms.Application.SetSuspendState(System.Windows.Forms.PowerState.Suspend, false, false);
                    return;
                }
            }
            if (psi != null)
            {
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
        }

        // Used to stop the timer.
        public void stopTimer()
        {
            isRunning = false;
            timer.Stop();
            updateUi();
        }

        // Used to start the timer.
        public void startTimer()
        {
            isRunning = true;
            timer.Start();
            updateUi();
        }

        // Used to manually set time to a specific value. Overwrites existing timer values.
        public void setTime(int new_hours, int new_minutes, int new_seconds)
        {
            hours = 0;
            minutes = 0;
            seconds = 0;
            adjustHours(new_hours);
            adjustMinutes(new_minutes);
            adjustSeconds(new_seconds);
            updateUi();
        }

        // Adjusts the timer's hour value. If hours get below zero the timer would end up in negative values, so this function resets it.
        public void adjustHours(int hour_change)
        {
            hours += hour_change;

            if (hours < 0)
            {
                seconds = 0;
                minutes = 0;
                hours = 0;
            }
            updateUi();
        }

        // Adjusts the timer's minute value. Can perform conversion to hours if necessary.
        public void adjustMinutes(int minute_change)
        {
            minutes += minute_change;

            if (minutes > 59)
            {
                adjustMinutes(-60);
                adjustHours(1);
            }
            else if (minutes < 0)
            {
                adjustMinutes(60); 
                adjustHours(-1);
            }
            updateUi();
        }

        // Adjusts the timer's second value. Can perform conversion to minutes and hours if necessary.
        public void adjustSeconds(int second_change)
        {
            seconds += second_change;

            if (seconds > 59)
            {
                adjustSeconds(-60);
                adjustMinutes(1);
            }
            if (seconds < 0)
            {
                adjustSeconds(60);
                adjustMinutes(-1);
            }
            updateUi();
        }

        // Sets the shutdown mode.
        public void setShutdownMode(ShutdownMode new_mode)
        {
            mode = new_mode;
        }

        // Returns remaining time in seconds.
        public int getSecondsLeft()
        {
            return (((hours * 60) + minutes) * 60) + seconds;
        }

        // Called when the program is hidden from tray to let user know.
        public void onMinimise()
        {
            icon.ShowBalloonTip(6000, "Shutdowner v1.4", "Shutdowner has been hidden. Double click this icon at any time to return to the program.", ToolTipIcon.Warning);
        }

        // Lets the notifyicon reopen the window on doubleclick event.
        private void restoreWindow(object sender, EventArgs e)
        {
            window.Show();
            window.WindowState = WindowState.Normal;
        }
    }
}
