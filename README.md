#Small note
I won't be able to post new updates until 22th January until I get back home. I will keep working and will post new updates later this month for sure. Thanks for being interested in the project; don't lose your hope yet!
#DesktopFidget
A (not so small anymore) project on C# made in Visual Studio 12. Using graphics functions the program cuts out frames out of an image file and then draws them together, resulting in a complete image, as demonstrated here:
http://www.youtube.com/watch?v=tdC5AmRkxtw

##Features (so far):
All the existing animation frames are actually used;

Random head movements;

Random floating movements in the vicinity;

Random movements across the monitor (with decent customization);

Settings file;

Follow the mouse function;

Turn around function;

Left click relocation;

Click through function;

Size customization;

##Where to download:
Click the "(num) Release" button at the top of the list of files.
(https://github.com/slow3586/DesktopFidget/releases)

##How to use:
Check the notification icons area and find the program's icon; right click it and you will find the settings menu.

During first run a .INI file will be created. All the changes to settings you make will be stored there. You can find the .INI file the executable's folder. Feel free to change it manually for advanced settings.

Note that if you want to use random movements you will have to disable 'follow the mouse' function and the other way around.
####Mouse controls:
Hold left mouse button: Move

Double click left mouse button: Cast magic

Click right mouse button: Switch side

##Changelog:
#####2.0.1.2
All the settings are now stored in an .INI file in the executable's folder and will be loaded automatically when the program starts;

Completely remade source code's layout;

Fixed teleportation during side switching if the size was custom;

Improved Fidget's flight direction detection;

#####1.3.1.2
'Follow the mouse' function finally implemented. It will be improved eventually;

Wings now flap faster depending on Fidget's speed during movements;

#####1.2.3.2
MUCH smoother side switching animation (fixed wings/tail/teleportation);

Fixed bugs that occur when you launch multiple instances of the program (now you can have as many Fidgets at the same time as you want!)

Implemented 'Change size' feature;

Changed settings menu layout;

Smoothed animations a little;

#####1.2.2.2
Implemented random movement (at a basic level);

Fixed teleportation during side switching;

Fixed floating formulae;

#####1.1.2.2
The task is no longer displayed in the taskbar, a notification icon has been added instead;

Settings menu implemented;

Click through feature (available in the settings menu);

#####1.1.1.2
Fixed the CPU load issue by cutting the main frame into pieces at the start and then storing the parts for further use;

Implemented 'Switch side' feature;

Fixed the tail's placement;

#####1.0.0.0
First release;
