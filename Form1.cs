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

namespace DesktopFidget
{
    public partial class Form1 : Form
    {
        //This is the part that will help the program remove the window borders and
        //put it on top of all other windows.
        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        public struct Rect{
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }    }
        private const int GWL_STYLE = -16;     
        private const int WS_BORDER = 0x00800000;     
        private const int WS_CAPTION = 0x00C00000;   
        private const int WS_SYSMENU = 0x00080000;    
        private const int WS_MINIMIZEBOX = 0x00020000;
  
        //Just setting up variables.
        object MAINFRAME = DesktopFidget.Properties.Resources.ResourceManager.GetObject("MAINFRAME");
        private bool LeftMouseButtonDown = false;
        private bool ActivateShot = false;
        private bool LookingRightWay = true;
        private bool AllowManualMovement = true;
        private int LowerBodyState = 160;
        private int TailState = 105;
        private double UpperBodyState1 = 0;
        private double UpperBodyState2 = 0;
        private double UpperBodyState3 = 0;
        private int LeftWingState = 0;
        private int RightWingState = 8;
        private int HeightBonus = 0;
        private int WidthBonus = 0;
        private int WingSideSwitchBonus = 0;
        private int NeedTBS1 = 0;
        private int NeedTBS2 = 0;
        private int NeedTBS3 = 0;
        private int TurnAroundState = 0;
        private int SpeedDuringFlight = 40;
        private double AngleHeightFlight = 0;
        private double AngleWidthFlight = 0;
        private Image LeftWingImage;
        private Image RightWingImage;
        private Image UpperBodyImage;
        private Image LowerBodyImage;
        private Image TailImage;
        private Image[] CutFrame = new Image[240];

        //Constant variables
        const int MOVEMENT_TIME_MODIFIER = 50;
        const int WINDOW_SIZE_WIDTH = 320;
        const int WINDOW_SIZE_HEIGHT = 360;

        public Form1()
        {
            InitializeComponent();

            int _c=0;
            IntPtr _window;
            do
            {
                _window = FindWindowByCaption(IntPtr.Zero, "Desktop Fidget" + " Copy " + Convert.ToString(_c));
                if (_window != IntPtr.Zero)
                {
                    _c++;
                }
            }
            while (_window != IntPtr.Zero);
                Variables.WINDOW_NAME = "Desktop Fidget" + " Copy " + Convert.ToString(_c);
                this.Text = Variables.WINDOW_NAME;

            this.DoubleBuffered = true; //Removing flickering.
            //Cut out frames
            for (int _b=0; _b<6; _b++)
            {
                if (_b != 1)
                {
                    for (int _a = 0; _a < 40; _a++)
                    {
                      CutFrame[_a + (_b * 40)] = SliceMainFrame(_a, _b);
                    }
                }
            }
            //Starting loop threads responsible for changing images for each part
            //of the body.
            Thread threadlbil = new Thread(new ThreadStart(LowerBodyImageLoop));
            threadlbil.IsBackground = true;
            threadlbil.Start();
            Thread threadtbil = new Thread(new ThreadStart(UpperBodyImageLoop));
            threadtbil.IsBackground = true;
            threadtbil.Start();
            Thread threadwil = new Thread(new ThreadStart(WingsImageLoop));
            threadwil.IsBackground = true;
            threadwil.Start();
            Thread threadhcl = new Thread(new ThreadStart(HeightCalcLoop));
            threadhcl.IsBackground = true;
            threadhcl.Start();
            Thread threadfm = new Thread(new ThreadStart(FidgetsMind));
            threadfm.IsBackground = true;
            threadfm.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When the form loads this removes the window borders.
            IntPtr _window = FindWindowByCaption(IntPtr.Zero, Variables.WINDOW_NAME);
            SetWindowLong(_window, GWL_STYLE, WS_SYSMENU);
            SetWindowPos(_window, -3, 45, 45, WINDOW_SIZE_WIDTH, WINDOW_SIZE_HEIGHT, 0x0040);
            DrawMenuBar(_window);

        }

        private void MovementFunction(Rect _newwindowpos, Rect _windowcurrentpos, int _sleeptime)
        {
            IntPtr _window = FindWindowByCaption(IntPtr.Zero, Variables.WINDOW_NAME);
            //FIND THE DISTANCE
            int _distancex = _newwindowpos.Left - _windowcurrentpos.Left;
            int _distancey = _newwindowpos.Top - _windowcurrentpos.Top;
            int _distancesqr = Convert.ToInt32(Math.Floor(Math.Sqrt(Math.Pow(_distancex,2)+ Math.Pow(_distancey,2))));
            if (_distancesqr > 1000) _distancesqr = 1000;
            if (_distancesqr < 50) return;
            //should we turn around?
            if ((LookingRightWay == true && _distancex < 0) || (LookingRightWay == false && _distancex > 0))
            {
                TurnAroundState = 1;
            }
            //START MOVING TOWARDS IT
            double _angle = 0;
            double _cosx = Math.Sin(_angle);
            double _cosy = Math.Sin(_angle);
            int _movex = 0;
            int _movey = 0;
            while (_angle < Math.PI)
            {
                _cosx = Math.Cos(_angle);
                _cosy = Math.Cos(_angle);
                //big scary formulae o' flight
                _movex = _windowcurrentpos.Left + Convert.ToInt32(Math.Floor(_newwindowpos.Left * (1 - ((1 + (_cosx)) / 2))));
                _movey = _windowcurrentpos.Top + Convert.ToInt32(Math.Floor(_newwindowpos.Top * (1 - ((1 + (_cosy)) / 2))));
                SpeedDuringFlight = Convert.ToInt32((_distancesqr/150) * (1 - (Math.Sin(_angle))) + (40-_distancesqr/75));
                MoveWindow(_window,
                    _movex,
                    _movey,
                    WINDOW_SIZE_WIDTH, WINDOW_SIZE_HEIGHT, true);
                _angle = _angle + Math.PI / 500;
                Thread.Sleep(_sleeptime);
            }
            SpeedDuringFlight = 40;
        }

        private void FidgetsMind()
        {
            //Init moving
            Random _rnd = new Random();
            IntPtr _window = FindWindowByCaption(IntPtr.Zero, Variables.WINDOW_NAME);
            Rect _windowcurrentpos = new Rect();
            Rect _newwindowpos = new Rect();
            Variables.SecondsToNextMovement = Convert.ToInt32(_rnd.Next(MOVEMENT_TIME_MODIFIER - Variables.MovementFrequency, (MOVEMENT_TIME_MODIFIER - Variables.MovementFrequency) * 2));
            while (true)
            {
                if (Variables.FollowTheMouse && Variables.MovementDistance == 0)
                {
                    //DebugLabel.Visible = true;
                    //FIND THE TARGET
                    GetWindowRect(_window, ref _windowcurrentpos);
                    _newwindowpos.Left = Cursor.Position.X - WINDOW_SIZE_WIDTH / 2 - _windowcurrentpos.Left + _rnd.Next(-20, 20);
                    _newwindowpos.Top = Cursor.Position.Y - WINDOW_SIZE_HEIGHT / 2 - _windowcurrentpos.Top + _rnd.Next(-20, 20);
                    int _sleeptime = 15;
                    MovementFunction(_newwindowpos, _windowcurrentpos, _sleeptime);
                }
                //THIS ONE IS RESPONSIBLE FOR COMPLETELY RANDOM MOVEMENTS
                if (Variables.MovementDistance != 0 && !Variables.FollowTheMouse)
                {
                    if (Variables.SecondsSpentBeforeNextMovement > Variables.SecondsToNextMovement)
                    {
                        //generate new rnd
                        Variables.SecondsToNextMovement = Convert.ToInt32(_rnd.Next(MOVEMENT_TIME_MODIFIER - Variables.MovementFrequency, (MOVEMENT_TIME_MODIFIER - Variables.MovementFrequency) * 2));
                        //no moving by user
                        AllowManualMovement = false;
                        //FIND THE TARGET
                        GetWindowRect(_window, ref _windowcurrentpos);
                        do
                        {
                            _newwindowpos.Top = Convert.ToInt32(_rnd.Next(-10 * Variables.MovementDistance, 10 * Variables.MovementDistance));
                            _newwindowpos.Left = Convert.ToInt32(_rnd.Next(-10 * Variables.MovementDistance, 10 * Variables.MovementDistance));
                        } while
                            //Make sure it's inside the monitor
                            (
                            0 > _windowcurrentpos.Left + _newwindowpos.Left ||
                            0 > _windowcurrentpos.Top + _newwindowpos.Top ||
                            Screen.PrimaryScreen.Bounds.Width - WINDOW_SIZE_WIDTH < _windowcurrentpos.Left + _newwindowpos.Left ||
                            Screen.PrimaryScreen.Bounds.Height - WINDOW_SIZE_HEIGHT < _windowcurrentpos.Top + _newwindowpos.Top
                            );

                        //do the magic
                        int _sleeptime = 30;
                        MovementFunction(_newwindowpos, _windowcurrentpos, _sleeptime);
                        //

                        //WE DID IT!
                        //should we turn around again?
                        if (_windowcurrentpos.Left < Screen.PrimaryScreen.Bounds.Width * 0.4F &&
                            _newwindowpos.Left > Screen.PrimaryScreen.Bounds.Width * 0.4F
                            ) { TurnAroundState = 1; }
                        if (_windowcurrentpos.Left > Screen.PrimaryScreen.Bounds.Width * 0.6F &&
                            _newwindowpos.Left < Screen.PrimaryScreen.Bounds.Width * 0.6F
                            ) { TurnAroundState = 1; }
                        AllowManualMovement = true;
                        Variables.SecondsSpentBeforeNextMovement = 0;
                    }
                    //this isn't the end yet
                    Variables.SecondsSpentBeforeNextMovement++;
                }
                Thread.Sleep(1000);
            }
        }

        private Bitmap SliceMainFrame(int _a, int _b)
        {
            //0 - 7 (64x128) - LEFT WING
            //8 - 15 (64x128) - RIGHT WING
            //16 - 18 - LOOK UPWARDS
            //19 - 21 - LOOK BACKWARDS
            //22 - 25 - BEFORE/AFTER LOOK UPWARDS
            //26 - 31 - USE MAGIC
            //32 - EMPTY
            //33 - 39 (64x128) - TURN AROUND
            //40 - 79 - EMPTY
            //80 - 158 - TAIL ANIMATION
            //160 - 238 - LOWER BODY ANIMATION 
            //159 AND 239 - EMPTY
            Bitmap _MAINFRAMEb = (Bitmap)MAINFRAME;
            int _size=64;
            if ((_a < 16 && _b == 0) || (_a > 32 && _b == 0)) { _size = 128; }
            Rectangle _cloneRect = new Rectangle(_a*64, _b*64, 64, _size);
            System.Drawing.Imaging.PixelFormat _format = System.Drawing.Imaging.PixelFormat.Format32bppPArgb;
            Bitmap _result = _MAINFRAMEb.Clone(_cloneRect, _format);
            return _result;
        }

        private void RefreshTheForm()
        {
            //Quick function to make things more readable. Refresh the
            //form so the changes are visible.
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Refresh();
                });
            }
        }

        private void WingsImageLoop()
        {
            while (true)
            {
                //Take the frame we need
                LeftWingImage = CutFrame[LeftWingState];
                RightWingImage = CutFrame[RightWingState];
                //Refresh the form for the change to appear.
                RefreshTheForm();
                Thread.Sleep(SpeedDuringFlight);
                LeftWingState++;
                RightWingState++;
                if (LeftWingState == 8) { LeftWingState = 0; }
                if (RightWingState == 16) { RightWingState = 8; }
            }
        }

        private void UpperBodyImageLoop()
        {
            //Create random numbers that define when the animations
            //will happen.
            Random _rnd = new Random();
            NeedTBS1 = Convert.ToInt32(_rnd.Next(5, 12));
            NeedTBS2 = Convert.ToInt32(_rnd.Next(15, 25));
            NeedTBS3 = Convert.ToInt32(_rnd.Next(5, 12));
            while (true)
            {
                //Check for double click animation.
                if (ActivateShot)
                {
                    //Go through the frames we need using for loop.
                    for (int _a = 25; _a < 31; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    //Return back to normal.
                    UpperBodyImage = CutFrame[16];
                    ActivateShot = false;
                }

                //Check for turn around state
                if (TurnAroundState == 2)
                {
                    LowerBodyImage = CutFrame[32];
                    int _b = 131;
                    for (int _a = 33; _a < 40; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        TailImage = CutFrame[_b];
                        _b = _b + 4;
                        WingSideSwitchBonus = WingSideSwitchBonus + 3;
                        RefreshTheForm();
                        if (_a < 39)
                        { Thread.Sleep(50); }
                    }
                    //HAHAHAHA I HAVE NO IDEA WHAT I AM DOING
                    TailState = 95;
                    LowerBodyState = 225;
                    LookingRightWay = !LookingRightWay;
                    WingSideSwitchBonus = 0;
                    if (AngleWidthFlight > Math.PI)
                    { AngleWidthFlight = Math.PI-(2*Math.PI-AngleWidthFlight); }
                    else if (AngleWidthFlight < Math.PI)
                    { AngleWidthFlight = 2*Math.PI-(Math.PI-AngleWidthFlight); }
                    TurnAroundState = 0;
                }

                //Looking upwards animation.
                if (UpperBodyState1 == NeedTBS1 )
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    UpperBodyImage = CutFrame[16];
                    //Wait some time in this state before going back to normal.
                    Thread.Sleep(Convert.ToInt32(_rnd.Next(700, 2000)));
                    for (int _a = 23; _a < 25; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    UpperBodyState1 = 0;
                    NeedTBS1 = Convert.ToInt32(_rnd.Next(5, 12));
                }

                //Looking upwards and then behind animation.
                if (UpperBodyState2 == NeedTBS2)
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    for (int _a = 16; _a < 22; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        if (_a == 16 || _a == 21) { Thread.Sleep(Convert.ToInt32(_rnd.Next(500, 2000))); }
                        else { Thread.Sleep(60); }
                    }
                    for (int _a = 21; _a > 16; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                       Thread.Sleep(60);
                    }
                    UpperBodyState2 = 0;
                    NeedTBS2 = Convert.ToInt32(_rnd.Next(15, 25));
                }

                //Looking behind animation.
                if (UpperBodyState3 == NeedTBS3)
                {
                    for (int _a = 18; _a < 21; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    UpperBodyImage = CutFrame[21];
                    Thread.Sleep(Convert.ToInt32(_rnd.Next(700, 2000)));
                    for (int _a = 21; _a > 17; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    UpperBodyState3 = 0;
                    NeedTBS3 = Convert.ToInt32(_rnd.Next(5, 12));
                }

                //At the end of the day if nothing is happening
                //put the normal frame on and wait a second.
                UpperBodyImage = CutFrame[31];
                if (TurnAroundState == 0)
                {
                    UpperBodyState1 = UpperBodyState1 + 0.25;
                    UpperBodyState2 = UpperBodyState2 + 0.25;
                    UpperBodyState3 = UpperBodyState3 + 0.25;
                };
                Thread.Sleep(250);
            }
        }

        private void LowerBodyImageLoop()
        {
            while (true)
            {
                //Same idea here as in upper body loop but with less random
                //and more looping through the same stuff over and over.
                if (TurnAroundState == 1 && (TailState > 135 && TailState < 140))
                {
                    TurnAroundState++;
                }
                else
                {
                    if (TurnAroundState != 2)
                    {
                        LowerBodyImage = CutFrame[LowerBodyState];
                        LowerBodyState++;
                        if (LowerBodyState == 239)
                        { LowerBodyState = 160; }
                        TailImage = CutFrame[TailState];
                        RefreshTheForm();
                        TailState++;
                        if (TailState == 159)
                        { TailState = 80; }
                    }
                }
                if (TurnAroundState == 0)
                {
                    Thread.Sleep(50);
                }
                else { Thread.Sleep(35); }
            }
        }

        private void HeightCalcLoop()
        {
            //This function calculates the way fidget will float around.
            //Just basic cos stuff.
            //The values can be seen in the paint part of the program when
            //the images are being drawn on the form.
            while (true)
            {
                HeightBonus = Convert.ToInt32(Math.Ceiling(Math.Cos(AngleHeightFlight) * 20));
                WidthBonus = Convert.ToInt32(Math.Ceiling(Math.Cos(AngleWidthFlight) * 20));
                    AngleHeightFlight = AngleHeightFlight + Math.PI / 150;
                    AngleWidthFlight = AngleWidthFlight + Math.PI / 250;
                if (AngleHeightFlight > 2*Math.PI) { AngleHeightFlight = 0; }
                if (AngleWidthFlight > 2 * Math.PI) { AngleWidthFlight = 0; }
                if (AngleHeightFlight < 0) { AngleHeightFlight = Math.PI*2; }
                if (AngleWidthFlight < 0) { AngleWidthFlight = Math.PI*2; }
                Thread.Sleep(25);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                //Check if the image actually exists,
                //then draw the image using magical numbers and taking
                //numbers responsible for flying into account.
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            if (!LookingRightWay)
            {
                e.Graphics.TranslateTransform(220, 0);
                e.Graphics.ScaleTransform(-1 * 0.25F * Convert.ToSingle(Variables.SizeLevel), 1 * 0.25F * Convert.ToSingle(Variables.SizeLevel));
            }
            else
            {
                e.Graphics.ScaleTransform(1F * 0.25F * Convert.ToSingle(Variables.SizeLevel), 1 * 0.25F * Convert.ToSingle(Variables.SizeLevel));
            }

                 if (TailImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(79 + WidthBonus - (WingSideSwitchBonus / 2), 77 + HeightBonus),   // destination for upper-left point 
                    new Point(143 + WidthBonus - (WingSideSwitchBonus / 2), 77 + HeightBonus),  // destination for upper-right point
                    new Point(63 + WidthBonus - (WingSideSwitchBonus / 2), 138 + HeightBonus)};
                    e.Graphics.DrawImage(TailImage, _destinationPoints);
                 }
                
                 if (RightWingImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(110 + WidthBonus - WingSideSwitchBonus, 17 + HeightBonus),   // destination for upper-left point 
                    new Point(174 + WidthBonus, 17 + HeightBonus),  // destination for upper-right point
                    new Point(110 + WidthBonus - WingSideSwitchBonus, 145 + HeightBonus)};
                    e.Graphics.DrawImage(RightWingImage, _destinationPoints);
                 } 
                if (LowerBodyImage != null) 
                {e.Graphics.DrawImage(LowerBodyImage, new Point(79 + WidthBonus, 66 + HeightBonus));}

                if (LeftWingImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(55 + WidthBonus, 17 + HeightBonus),   // destination for upper-left point 
                    new Point(119 + WidthBonus - WingSideSwitchBonus, 17 + HeightBonus),  // destination for upper-right point
                    new Point(55 + WidthBonus, 145 + HeightBonus)};
                    e.Graphics.DrawImage(LeftWingImage, _destinationPoints);
                } 

                if (LeftWingImage != null) 
                { e.Graphics.DrawImage(LeftWingImage, new Point(55 + WidthBonus, 17 + HeightBonus)); }

                if (UpperBodyImage != null)
                { e.Graphics.DrawImage(UpperBodyImage, new Point(75 + WidthBonus, 25 + HeightBonus)); }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (AllowManualMovement == true)
            {
                if (e.Button == MouseButtons.Right)
                {
                    TurnAroundState = 1;
                }
                else
                {
                    //When the mouse button goes down start listening to mouse movements.
                    LeftMouseButtonDown = true;
                    Thread threadwil = new Thread(new ThreadStart(MoveWindowFunction));
                    threadwil.Start();
                }
            }
        }

        private void MoveWindowFunction()
        {
            //Take the difference between the mouses' current position
            //and the program's position and then using that
            //change the program's position.
            int _xdif = Cursor.Position.X -this.Location.X;
            int _ydif = Cursor.Position.Y -this.Location.Y;
            IntPtr _window = FindWindowByCaption(IntPtr.Zero, Variables.WINDOW_NAME);
            while(LeftMouseButtonDown)
            {
                Thread.Sleep(40);
                int _newx = Cursor.Position.X;
                int _newy = Cursor.Position.Y;
                SetWindowPos(_window, 0, _newx-_xdif, _newy-_ydif, WINDOW_SIZE_WIDTH, WINDOW_SIZE_HEIGHT, 0x0040);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //No more moving around.
            LeftMouseButtonDown = false;
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            //Magic!
            ActivateShot = true;
        }

        private void OpenForm2()
        {
            FormCollection fc = Application.OpenForms;
            int _a=0;
            foreach (Form frm in fc)
            {
                if (frm.Text == "DFidget Settings")
                {
                    _a++;
                }
            }
            if(_a==0)
                Application.Run(new Form2());
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread threadform2 = new Thread(new ThreadStart(OpenForm2));
            threadform2.IsBackground = true;
            threadform2.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    public class Variables
    {
        public static string WINDOW_NAME = "Desktop Fidget"; 
        public static bool ClickThroughWindow = false;
        public static bool FollowTheMouse = false;
        public static int MovementDistance = 0;
        public static int MovementFrequency = 0;
        public static int SecondsToNextMovement = 0;
        public static int AlphaLevel = 100;
        public static int SizeLevel = 4;
        public static int SecondsSpentBeforeNextMovement = 0;
    }
}
