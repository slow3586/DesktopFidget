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
        private bool LeftMouseButtonDown = false;
        private bool ActivateShot = false;
        private int LowerBodyState1 = 0;
        private int LowerBodyState2 = 4;
        private int TailState1 = 0;
        private int TailState2 = 3;
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
        private Image LeftWingImage;
        private Image RightWingImage;
        private Image TopBodyImage;
        private Image LowerBodyImage;
        private Image TailImage;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; //Removing flickering.
            //Starting loop threads responsible for changing images for each part
            //of the body.
            Thread threadlbil = new Thread(new ThreadStart(LowerBodyImageLoop));
            threadlbil.Start();
            Thread threadtbil = new Thread(new ThreadStart(UpperBodyImageLoop));
            threadtbil.Start();
            Thread threadwil = new Thread(new ThreadStart(WingsImageLoop));
            threadwil.Start();
            Thread threadhcl = new Thread(new ThreadStart(HeightCalcLoop));
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

        private Bitmap SliceMainFrame(int _x, int _y, int _size)
        {
            //This is the part where the program kills the cpu.
            //Threads request the main image to be cut in pieces each few mseconds
            //through this function. You might wanna take a look at the image
            //if you haven't yet.
            object MAINFRAME = DesktopFidget.Properties.Resources.ResourceManager.GetObject("MAINFRAME");
            Bitmap MAINFRAMEb = (Bitmap)MAINFRAME;
            Rectangle cloneRect = new Rectangle(_x*64, _y*64, 64, _size);
            System.Drawing.Imaging.PixelFormat format = MAINFRAMEb.PixelFormat;
            Bitmap result = MAINFRAMEb.Clone(cloneRect, format);
            return result;
        }

        private void RefreshTheForm()
        {
            //Quick function to make things more readable. Refresh the
            //form so the changes are visible.
            //This part causes the program to throw an exception
            //when it's being closed because it will still try to invoke.
            //My attempts at fixing this have been useless so far.
                this.Invoke((MethodInvoker)delegate
                {
                    this.Refresh();
                });
        }

        private void WingsImageLoop()
        {
            while (true)
            {
                //Cut out the part we need each time the loop is run.
                LeftWingImage = SliceMainFrame(LeftWingState, 0, 128);
                RightWingImage = SliceMainFrame(RightWingState, 0, 128);
                //Refresh the form for the change to appear.
                RefreshTheForm();
                Thread.Sleep(20);
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
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
                        RefreshTheForm();
                        Thread.Sleep(50);
                    }
                    //Return back to normal.
                    TopBodyImage = SliceMainFrame(16, 0, 64);
                    ActivateShot = false;
                }

                //Looking upwards animation.
                if (UpperBodyState1 == NeedTBS1 )
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
                        RefreshTheForm();
                        Thread.Sleep(80);
                    }
                    TopBodyImage = SliceMainFrame(16, 0, 64);
                    //Wait some time in this state before going back to normal.
                    Thread.Sleep(Convert.ToInt32(rnd.Next(700, 1000)));
                    for (int _a = 23; _a < 25; _a++)
                    {
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
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
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
                        RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    for (int _a = 16; _a < 22; _a++)
                    {
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
                        RefreshTheForm();
                        if (_a == 16 || _a == 21) { Thread.Sleep(Convert.ToInt32(rnd.Next(500, 800))); }
                        else { Thread.Sleep(100); }
                    }
                    for (int _a = 21; _a > 16; _a--)
                    {
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
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
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
                        RefreshTheForm();
                        Thread.Sleep(80);
                    }
                    TopBodyImage = SliceMainFrame(21, 0, 64);
                    Thread.Sleep(Convert.ToInt32(rnd.Next(700, 1000)));
                    for (int _a = 21; _a > 17; _a--)
                    {
                        TopBodyImage = SliceMainFrame(_a, 0, 64);
                        RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    UpperBodyState3 = 0;
                    NeedTBS3 = Convert.ToInt32(rnd.Next(5, 12));
                }

                //At the end of the day if nothing is happening
                //put the normal frame on and wait a second.
                TopBodyImage = SliceMainFrame(31, 0, 64);
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
                LowerBodyImage = SliceMainFrame(LowerBodyState1, LowerBodyState2, 64);
                LowerBodyState1++;
                if (LowerBodyState1 == 40 && LowerBodyState2 == 4)
                {
                    LowerBodyState1 = 0;
                    LowerBodyState2 = 5;
                }
                if (LowerBodyState1 == 39 && LowerBodyState2 == 5)
                {
                    LowerBodyState1 = 0;
                    LowerBodyState2 = 4;
                }
                RefreshTheForm();

                TailImage = SliceMainFrame(TailState1, TailState2, 64);
                TailState1++;
                if (TailState1 == 40 && TailState2 == 2)
                {
                    TailState1 = 0;
                    TailState2 = 3;
                }
                if (TailState1 == 39 && TailState2 == 3)
                {
                    TailState1 = 0;
                    TailState2 = 2;
                }
                RefreshTheForm();
                Thread.Sleep(40);
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
                Thread.Sleep(20);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                //Check if the image actually exists,
                //then draw the image using magical numbers and taking
                //numbers responsible for flying into account.
                if (RightWingImage != null) 
                {e.Graphics.DrawImage(RightWingImage, new Point(110+WidthBonus, 17 + HeightBonus + HeightBonusUpperBody)); }
                
                if (TailImage != null) 
                {e.Graphics.DrawImage(TailImage, new Point(79 + WidthBonus, 74 + HeightBonus));}
                
                if (LowerBodyImage != null) 
                {e.Graphics.DrawImage(LowerBodyImage, new Point(79 + WidthBonus, 66 + HeightBonus));}
                
                if (LeftWingImage != null) 
                { e.Graphics.DrawImage(LeftWingImage, new Point(55 + WidthBonus, 17 + HeightBonus + HeightBonusUpperBody)); }
                
                if (TopBodyImage != null) 
                { e.Graphics.DrawImage(TopBodyImage, new Point(75 + WidthBonus, 25 + HeightBonus + HeightBonusUpperBody)); }
        }

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            //When the mouse button goes down start listening to mouse movements.
            LeftMouseButtonDown = true;
            Thread threadwil = new Thread(new ThreadStart(MoveWindowFunction));
            threadwil.Start();
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
