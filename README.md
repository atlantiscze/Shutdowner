# About
Shutdowner is a reference project i have created as a demonstration of my work in C# .NET. It is a small WPF application that allows for timed shutdown of your computer. Other actions such as reboot or hibernation are also possible.

# Usage
See the following link for screenshot of the user interface: http://i.imgur.com/DIK8HyT.png

Usage of the program is very easy even for inexperienced user. The main screen shows remaining time in hours:minutes:seconds format. The "+" and "-" buttons below can be used to add or remove time. Two large buttons labeled "Start" and "Stop" can be used to control the shutdown countdown. Next to these resides the main configuration panel. Currently, four options are available below on the configuration panel:
- Regular shutdown: Shuts down the computer as if you pressed the shutdown button in start menu by yourself. If an application refuses to close it may delay or even stop the shutdown, however.
- Forced shutdown: Shuts down the computer as regular shutdown does, but if an application refuses to close it will be forcibly killed. This may result in loss of data in applications such as text editor.
- Reboot: Reboots your computer instead of shutting it down.
- Hibernation/Sleep Mode: Puts your computer into hibernation. The computer shuts down but your work is preserved and reloaded once you turn on your computer again. Please note that this requires for hibernation to be enabled in your system's settings. If hibernation is not enabled, sleep mode will be used instead.

Furthermore, two checkboxes that are by default enabled exist below.
- Notify me one minute before shutdown will generate a pop-up message on your task bar, as well as emit beeping noise when the countdown is below one minute.
- Hide when minimizing option will cause the application to disappear from the main task bar when you minimize it. A small icon will remain in the toolbar that can be doubleclicked to bring up the program again.

# Author
This program has been created by Jakub Štěpánek (@atlantiscze) and is free for use by anyone. By using this program you agree that you do so on your own risk, and that you understand you may lose data in open applications when the shutdown timer reaches zero. I am not responsible for any lost work caused by the program. See the license file for more details.
