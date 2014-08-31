using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace DesktopFidget
{
    public class Var
    {
        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
        public static int GWL_STYLE = -16;
        public static int WS_BORDER = 0x00800000;
        public static int WS_CAPTION = 0x00C00000;
        public static int WS_SYSMENU = 0x00080000;
        public static int WS_MINIMIZEBOX = 0x00020000;

        //WHAT
        public static GraphicsForm GraphicsFormInstance;

        public static object MAINFRAME = DesktopFidget.Properties.Resources.ResourceManager.GetObject("MAINFRAME");
        public static bool LeftMouseButtonDown = false;
        public static bool ActivateShot = false;
        public static bool LookingRightWay = true;
        public static bool AllowManualMovement = true;
        public static int LowerBodyState = 160;
        public static int TailState = 105;
        public static double UpperBodyState1 = 0;
        public static double UpperBodyState2 = 0;
        public static double UpperBodyState3 = 0;
        public static int LeftWingState = 0;
        public static int RightWingState = 8;
        public static int HeightBonus = 0;
        public static int WidthBonus = 0;
        public static int ZBonus = 0;
        public static int WingSideSwitchBonus = 0;
        public static int NeedTBS1 = 0;
        public static int NeedTBS2 = 0;
        public static int NeedTBS3 = 0;
        public static int TurnAroundState = 0;
        public static double AngleHeightFlight = 0;
        public static double AngleWidthFlight = 0;
        public static double AngleZFlight = 0;
        public static Image LeftWingImage;
        public static Image RightWingImage;
        public static Image UpperBodyImage;
        public static Image LowerBodyImage;
        public static Image TailImage;
        public static Image[] CutFrame = new Image[240];

        public static int MOVEMENT_TIME_MODIFIER = 50;
        public static int LOWER_BODY_X = 79;
        public static int LOWER_BODY_Y = 66;
        public static int WingsSleepParameterDefault = 37;
        public static int WingsSleepParameter = 37;
        public static int WingsSleepMultiplier = 140;
        public static int SleepDuringMouseFollow = 15;
        public static int SleepDuringRandomMoves = 27;

        public static bool DebugMode = false;
        public static int WindowSizeX = 230;
        public static int WindowSizeY = 220;
        public static int WindowStartingX = 45;
        public static int WindowStartingY = 45;
        public static int HeightBonusMultiplier = 15;
        public static int WidthBonusMultiplier = 20;
        public static int ZBonusMultiplier = 5;
        public static int HeightBonusIncreaseMultiplier = 150;
        public static int WidthBonusIncreaseMultiplier = 250;
        public static int ZBonusIncreaseMultiplier = 1000;
        public static int FlightSpeedMultiplier = 500;

        public static int ProgramVersion = 2232;
        public static string WINDOW_NAME = "Desktop Fidget";
        public static bool ClickThroughWindow = false;
        public static bool FollowTheMouse = false;
        public static bool TurnTowardsCenter = false;
        public static int MovementDistance = 0;
        public static int MovementFrequency = 0;
        public static int SecondsToNextMovement = 0;
        public static int AnimationsFrequency = 100;
        public static int AlphaLevel = 100;
        public static float SizeLevel = 1;
        public static int SecondsSpentBeforeNextMovement = 0;
        public static bool IniFileWasLoaded = false;
        public static bool Shadow = false;
        public static bool SettingsMenuIsOpen = false;

        public static String[] Dialogs = new String[200];
        public static String DialogToDraw;
        public static bool AllowDialogs = false;
        public static string Mods="";
        public static int DialogsFrequency = 100;
        public static int SecondsSpentBeforeNextDialog = 0;
        public static int SecondsBeforeNextDialog = 0;
    }
}
