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
    public partial class GraphicsForm : Form
    {

        public GraphicsForm()
        {
            InitializeComponent();
            //Load up setttings if INI file exists or create it if there's none
            IniFile.OpenSettingsFile(true, false);
            //Define the form
            Var.GraphicsFormInstance = this;
            //Make sure there are no multiple instances of the program with the same name
            int _c=0;
            IntPtr _window;
            do
            {
                _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, "Desktop Fidget" + " Copy " + Convert.ToString(_c));
                if (_window != IntPtr.Zero)
                {
                    _c++;
                }
            }
            while (_window != IntPtr.Zero);
                Var.WINDOW_NAME = "Desktop Fidget" + " Copy " + Convert.ToString(_c);
                this.Text = Var.WINDOW_NAME;
            //
            this.DoubleBuffered = true; //Removing flickering.
            //Cut out frames
            for (int _b=0; _b<6; _b++)
            {
                if (_b != 1)
                {
                    for (int _a = 0; _a < 40; _a++)
                    {
                      Var.CutFrame[_a + (_b * 40)] = SliceMainFrame(_a, _b);
                    }
                }
            }
            //Starting loop threads responsible for changing images for each part
            //of the body.
            Thread threadlbil = new Thread(new ThreadStart(ImageLoops.LowerBodyImageLoop));
            threadlbil.IsBackground = true;
            threadlbil.Start();
            Thread threadtbil = new Thread(new ThreadStart(ImageLoops.UpperBodyImageLoop));
            threadtbil.IsBackground = true;
            threadtbil.Start();
            Thread threadwil = new Thread(new ThreadStart(ImageLoops.WingsImageLoop));
            threadwil.IsBackground = true;
            threadwil.Start();
            Thread threadhcl = new Thread(new ThreadStart(MovementFunctions.HeightCalcLoop));
            threadhcl.IsBackground = true;
            threadhcl.Start();
            Thread threadfm = new Thread(new ThreadStart( MovementFunctions.FidgetsMind ));
            threadfm.IsBackground = true;
            threadfm.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When the form loads this removes the window borders.
            IntPtr _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, Var.WINDOW_NAME);
            if (!Var.DebugMode) { NativeMethods.SetWindowLong(_window, Var.GWL_STYLE, Var.WS_SYSMENU); }
            if (Var.ClickThroughWindow)
            {
                int _initialStyle = NativeMethods.GetWindowLong(_window, -20);
                NativeMethods.SetWindowLong(_window, -20, _initialStyle | 0x80000 | 0x20);
            }
            else
            {
                NativeMethods.SetWindowLong(_window, -20, 0x80000);
            }
            NativeMethods.MoveWindow(_window, Var.WindowStartingX, Var.WindowStartingY, Var.WindowSizeX, Var.WindowSizeY,true);
            NativeMethods.DrawMenuBar(_window);

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
            Bitmap _MAINFRAMEb = (Bitmap)Var.MAINFRAME;
            int _size=64;
            if ((_a < 16 && _b == 0) || (_a > 32 && _b == 0)) { _size = 128; }
            Rectangle _cloneRect = new Rectangle(_a*64, _b*64, 64, _size);
            System.Drawing.Imaging.PixelFormat _format = System.Drawing.Imaging.PixelFormat.Format32bppPArgb;
            Bitmap _result = _MAINFRAMEb.Clone(_cloneRect, _format);
            return _result;
        }

       public static void RefreshTheForm()
        {
            //Quick function to make things more readable. Refresh the
            //form so the changes are visible.
            if (Var.GraphicsFormInstance.IsHandleCreated)
            {
                Var.GraphicsFormInstance.Invoke((MethodInvoker)delegate
                {
                    Var.GraphicsFormInstance.Refresh();
                });
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
            if (!Var.LookingRightWay)
            {
                e.Graphics.TranslateTransform(220 * Var.SizeLevel / 4, 0);
                e.Graphics.ScaleTransform(-1 * 0.25F * Convert.ToSingle(Var.SizeLevel), 1 * 0.25F * Convert.ToSingle(Var.SizeLevel));
            }
            else
            {
                e.Graphics.ScaleTransform(1F * 0.25F * Convert.ToSingle(Var.SizeLevel), 1 * 0.25F * Convert.ToSingle(Var.SizeLevel));
            }

            //debug stuff
            //DebugLabel.Visible = true;
            //DebugLabel.Text = Convert.ToString(Var.ClickThroughWindow);

                 if (Var.TailImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(79 + Var.WidthBonus - (Var.WingSideSwitchBonus / 2), 77 + Var.HeightBonus),   // destination for upper-left point 
                    new Point(143 + Var.WidthBonus - (Var.WingSideSwitchBonus / 2), 77 + Var.HeightBonus),  // destination for upper-right point
                    new Point(63 + Var.WidthBonus - (Var.WingSideSwitchBonus / 2), 138 + Var.HeightBonus)};
                    e.Graphics.DrawImage(Var.TailImage, _destinationPoints);
                 }
                
                 if (Var.RightWingImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(110 + Var.WidthBonus - Var.WingSideSwitchBonus, 17 + Var.HeightBonus),   // destination for upper-left point 
                    new Point(174 + Var.WidthBonus, 17 + Var.HeightBonus),  // destination for upper-right point
                    new Point(110 + Var.WidthBonus - Var.WingSideSwitchBonus, 145 + Var.HeightBonus)};
                    e.Graphics.DrawImage(Var.RightWingImage, _destinationPoints);
                 } 
                if (Var.LowerBodyImage != null) 
                {e.Graphics.DrawImage(Var.LowerBodyImage, new Point(Var.LOWER_BODY_X + Var.WidthBonus, Var.LOWER_BODY_Y + Var.HeightBonus));}

                if (Var.LeftWingImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(55 + Var.WidthBonus, 17 + Var.HeightBonus),   // destination for upper-left point 
                    new Point(119 + Var.WidthBonus - Var.WingSideSwitchBonus, 17 + Var.HeightBonus),  // destination for upper-right point
                    new Point(55 + Var.WidthBonus, 145 + Var.HeightBonus)};
                    e.Graphics.DrawImage(Var.LeftWingImage, _destinationPoints);
                } 

                if (Var.LeftWingImage != null) 
                { e.Graphics.DrawImage(Var.LeftWingImage, new Point(55 + Var.WidthBonus, 17 + Var.HeightBonus)); }

                if (Var.UpperBodyImage != null)
                { e.Graphics.DrawImage(Var.UpperBodyImage, new Point(75 + Var.WidthBonus, 25 + Var.HeightBonus)); }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Var.AllowManualMovement == true)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Var.TurnAroundState = 1;
                }
                else
                {
                    //When the mouse button goes down start listening to mouse movements.
                    Var.LeftMouseButtonDown = true;
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
            IntPtr _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, Var.WINDOW_NAME);
            while(Var.LeftMouseButtonDown)
            {
                Thread.Sleep(40);
                int _newx = Cursor.Position.X;
                int _newy = Cursor.Position.Y;
                NativeMethods.SetWindowPos(_window, 0, _newx-_xdif, _newy-_ydif, Var.WindowSizeX, Var.WindowSizeY, 0x0040);
            }
            //Turn around?
            if (Var.TurnTowardsCenter)
            {
                Var.Rect _windowcurrentpos = new Var.Rect();
                int _screenwidth = Screen.PrimaryScreen.Bounds.Width;
                NativeMethods.GetWindowRect(_window, ref _windowcurrentpos);
                if ((_windowcurrentpos.Left + Var.LOWER_BODY_X > _screenwidth / 2) && Var.LookingRightWay)
                    Var.TurnAroundState = 1;
                else if ((_windowcurrentpos.Left + Var.LOWER_BODY_X < _screenwidth / 2) && !Var.LookingRightWay)
                    Var.TurnAroundState = 1;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //No more moving around.
            Var.LeftMouseButtonDown = false;
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            //Magic!
            Var.ActivateShot = true;
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
                Application.Run(new SettingsForm());
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread threadform2 = new Thread(new ThreadStart(OpenForm2));
            threadform2.IsBackground = true;
            threadform2.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}
