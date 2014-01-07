using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Media.Media3D;

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
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        private readonly string WINDOW_NAME = "Desktop Fidget"; 
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
        private int LowerBodyState = 160;
        private int TailState = 105;
        private int UpperBodyState1 = 0;
        private int UpperBodyState2 = 0;
        private int UpperBodyState3 = 0;
        private int LeftWingState = 0;
        private int RightWingState = 8;
        private int HeightBonus = 0;
        private int WidthBonus = 0;
        private int HeightBonusUpperBody = 0;
        private int NeedTBS1 = 0;
        private int NeedTBS2 = 0;
        private int NeedTBS3 = 0;
        private int TurnAroundState = 0;
        private Image LeftWingImage;
        private Image RightWingImage;
        private Image UpperBodyImage;
        private Image LowerBodyImage;
        private Image TailImage;
        private Image[] CutFrame = new Image[240];

        public Form1()
        {
            InitializeComponent();
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When the form loads this removes the window borders.
            IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
            SetWindowPos(window, -3, 45, 45, 500, 400, 0x0040);
            DrawMenuBar(window);
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
            System.Drawing.Imaging.PixelFormat format = _MAINFRAMEb.PixelFormat;
            Bitmap _result = _MAINFRAMEb.Clone(_cloneRect, format);
            //Image _i = (Image)_result;
            //Graphics _g = Graphics.FromImage(_i);
            //_result = new Bitmap(63, 63, _g);
            //_g.Dispose();
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
                //Cut out the part we need each time the loop is run.
                LeftWingImage = CutFrame[LeftWingState];
                RightWingImage = CutFrame[RightWingState];
                //Refresh the form for the change to appear.
                RefreshTheForm();
                Thread.Sleep(45);
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
            Random rnd = new Random();
            NeedTBS1 = Convert.ToInt32(rnd.Next(5, 12));
            NeedTBS2 = Convert.ToInt32(rnd.Next(15, 25));
            NeedTBS3 = Convert.ToInt32(rnd.Next(5, 12));
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
                        Thread.Sleep(50);
                    }
                    //Return back to normal.
                    UpperBodyImage = CutFrame[16];
                    ActivateShot = false;
                }
                //Check for turn around state
                if (TurnAroundState == 2)
                {
                    //while (TailState != 104 || TailState != 105) { }
                    LowerBodyImage = CutFrame[32];
                    for (int _a = 33; _a < 40; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(80);
                    }
                    TurnAroundState = 0;
                    LookingRightWay = !LookingRightWay;
                }
                //Looking upwards animation.
                if (UpperBodyState1 == NeedTBS1 )
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(80);
                    }
                    UpperBodyImage = CutFrame[16];
                    //Wait some time in this state before going back to normal.
                    Thread.Sleep(Convert.ToInt32(rnd.Next(700, 1000)));
                    for (int _a = 23; _a < 25; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    UpperBodyState1 = 0;
                    NeedTBS1 = Convert.ToInt32(rnd.Next(5, 12));
                }

                //Looking upwards and then behind animation.
                if (UpperBodyState2 == NeedTBS2)
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    for (int _a = 16; _a < 22; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        if (_a == 16 || _a == 21) { Thread.Sleep(Convert.ToInt32(rnd.Next(500, 800))); }
                        else { Thread.Sleep(100); }
                    }
                    for (int _a = 21; _a > 16; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                       Thread.Sleep(100);
                    }
                    UpperBodyState2 = 0;
                    NeedTBS2 = Convert.ToInt32(rnd.Next(15, 25));
                }

                //Looking behind animation.
                if (UpperBodyState3 == NeedTBS3)
                {
                    for (int _a = 18; _a < 21; _a++)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(80);
                    }
                    UpperBodyImage = CutFrame[21];
                    Thread.Sleep(Convert.ToInt32(rnd.Next(700, 1000)));
                    for (int _a = 21; _a > 17; _a--)
                    {
                        UpperBodyImage = CutFrame[_a];
                        RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    UpperBodyState3 = 0;
                    NeedTBS3 = Convert.ToInt32(rnd.Next(5, 12));
                }

                //At the end of the day if nothing is happening
                //put the normal frame on and wait a second.
                UpperBodyImage = CutFrame[31];
                UpperBodyState1++;
                UpperBodyState2++;
                UpperBodyState3++;
                Thread.Sleep(1000);
            }
        }

        private void LowerBodyImageLoop()
        {
            while (true)
            {
                //Same idea here as in upper body loop but with less random
                //and more looping through the same stuff over and over.
                if (TurnAroundState == 0)
                {
                    LowerBodyImage = CutFrame[LowerBodyState];
                    LowerBodyState++;
                    if (LowerBodyState == 239)
                    {
                        LowerBodyState = 160;
                    }
                }
                else
                {
                    if (TurnAroundState != 2)
                    {
                        TurnAroundState++;
                    }
                }

                TailImage = CutFrame[TailState];
                TailState++;
                if (TailState == 159)
                {
                    TailState = 80;
                }
                RefreshTheForm();
                Thread.Sleep(90);
            }
        }

        private void HeightCalcLoop()
        {
            //This function calculates the way fidget will float around.
            //Just basic cos stuff.
            //The values can be seen in the paint part of the program when
            //the images are being drawn on the form.
            double _angleheight = 0;
            double _angleheightub = 0;
            double _anglewidth = 0;
            while (true)
            {
                HeightBonus = Convert.ToInt32(Math.Ceiling(Math.Cos(_angleheight) * 20));
                HeightBonusUpperBody = Convert.ToInt32(Math.Ceiling(Math.Cos(_angleheightub) * 1));
                WidthBonus = Convert.ToInt32(Math.Ceiling(Math.Cos(_anglewidth) * 10));
                _angleheight = _angleheight + 0.02 ;
                _angleheightub = _angleheightub + 0.04;
                _anglewidth = _anglewidth + 0.0075;
                if (_angleheight == 1) { _angleheight = 0; }
                if (_angleheightub == 1) { _angleheightub = 0; }
                if (_anglewidth == 1) { _anglewidth = 0; }
                Thread.Sleep(25);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                //Check if the image actually exists,
                //then draw the image using magical numbers and taking
                //numbers responsible for flying into account.

            if (!LookingRightWay)
            {
                e.Graphics.TranslateTransform(232, 0);
                e.Graphics.ScaleTransform(-1F, 1);
            }

                 if (TailImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(79 + WidthBonus, 78 + HeightBonus),   // destination for upper-left point of  
                                          // original 
                    new Point(144 + WidthBonus, 78 + HeightBonus),  // destination for upper-right point of  
                                          // original 
                    new Point(63 + WidthBonus, 138 + HeightBonus)};
                     e.Graphics.DrawImage(TailImage, _destinationPoints); 
                 }  
                
                if (RightWingImage != null)
                { e.Graphics.DrawImage(RightWingImage, new Point(110 + WidthBonus, 17 + HeightBonus + HeightBonusUpperBody)); }

                if (LowerBodyImage != null) 
                {e.Graphics.DrawImage(LowerBodyImage, new Point(79 + WidthBonus, 66 + HeightBonus));}
                
                if (LeftWingImage != null) 
                { e.Graphics.DrawImage(LeftWingImage, new Point(55 + WidthBonus, 17 + HeightBonus + HeightBonusUpperBody)); }
                
                if (UpperBodyImage != null) 
                { e.Graphics.DrawImage(UpperBodyImage, new Point(75 + WidthBonus, 25 + HeightBonus + HeightBonusUpperBody)); }
        }

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
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

        private void MoveWindowFunction()
        {
            //Take the difference between the mouses' current position
            //and the program's position and then using that
            //change the program's position.
            int _xdif = Cursor.Position.X -this.Location.X;
            int _ydif = Cursor.Position.Y -this.Location.Y;
            IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            while(LeftMouseButtonDown)
            {
                Thread.Sleep(40);
                int _newx = Cursor.Position.X;
                int _newy = Cursor.Position.Y;
                SetWindowPos(window, 0, _newx-_xdif, _newy-_ydif, 240, 220, 0x0040);
            }
        }

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            //No more moving around.
            LeftMouseButtonDown = false;
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            //Magic!
            ActivateShot = true;
        }
    }
}
