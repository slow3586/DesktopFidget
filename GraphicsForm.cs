using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
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

            //Load INI
            IniFile.OpenSettingsFile(true, false);
            //Load dialogs
            if (Dialogs.readDialogs())
            {
                Var.Mods = Var.Mods + "Custom dialogs loaded.";
            }
            //Rename window
            Var.GraphicsFormInstance = this;
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
            this.DoubleBuffered = true;

            //Cut frames
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
            threadlbil.Name = "Lower Body Image Loop";
            threadlbil.IsBackground = true;
            threadlbil.Start();
            Thread threadtbil = new Thread(new ThreadStart(ImageLoops.UpperBodyImageLoop));
            threadtbil.Name = "Upper Body Image Loop";
            threadtbil.IsBackground = true;
            threadtbil.Start();
            Thread threadwil = new Thread(new ThreadStart(ImageLoops.WingsImageLoop));
            threadwil.Name = "Wings Image Loop";
            threadwil.IsBackground = true;
            threadwil.Start();
            Thread threadhcl = new Thread(new ThreadStart(MovementFunctions.HeightCalcLoop));
            threadhcl.Name = "Height Calculation Loop";
            threadhcl.IsBackground = true;
            threadhcl.Start();
            Thread threadfm = new Thread(new ThreadStart( MovementFunctions.FidgetsMind));
            threadfm.Name = "Random Movements Functions";
            threadfm.IsBackground = true;
            threadfm.Start();
            Thread threaddialogs = new Thread(new ThreadStart(Dialogs.dialogTick));
            threaddialogs.Name = "Dialogs Loop";
            threaddialogs.IsBackground = true;
            threaddialogs.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Remove borders
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
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            int ZBonus_WidthBonus = Var.ZBonus - Var.WidthBonus;
            int ZBonus_HeightBonus = Var.ZBonus + Var.HeightBonus;
            int WingSideSwitchBonus_Over2 = Var.WingSideSwitchBonus / 2;
            int ZBonus_HeightBonus_Over2 = ZBonus_HeightBonus / 2;
            int ZBonus_HeightBonus_Minus_WingSideSwitchBonus_Over2 = ZBonus_HeightBonus - WingSideSwitchBonus_Over2;
            int WidthBonus_Minus_WingSideSwitchBonus = Var.WidthBonus - Var.WingSideSwitchBonus;
            int WidthBonus_Minus_WingSideSwitchBonus_Over2 = Var.WidthBonus + WingSideSwitchBonus_Over2;
            if (!Var.LookingRightWay)
            {

                e.Graphics.TranslateTransform(220 * Var.SizeLevel, 0);
                e.Graphics.ScaleTransform(-1 * Var.SizeLevel,Var.SizeLevel);
            }
            else
            {
                e.Graphics.ScaleTransform(Var.SizeLevel, Var.SizeLevel);
            }

            //SHADOWS
            if (Var.Shadow)
            {
                ColorMatrix cm = new ColorMatrix();
                cm.Matrix22 = 0.0f;
                cm.Matrix00 = 0.0f;
                cm.Matrix11 = 0.0f;
                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                Rectangle _rect64 = new Rectangle(0, 0, 64, 64);
                Rectangle _rect128 = new Rectangle(0, 0, 64, 128);
                if (Var.TailImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(79 - ZBonus_WidthBonus - WingSideSwitchBonus_Over2, 77 + ZBonus_HeightBonus_Over2),   // destination for upper-left point 
                    new Point(143 - ZBonus_WidthBonus - WingSideSwitchBonus_Over2, 77 + ZBonus_HeightBonus_Over2),  // destination for upper-right point
                    new Point(63 - ZBonus_WidthBonus - WingSideSwitchBonus_Over2, 138 + ZBonus_HeightBonus_Over2)};
                    e.Graphics.DrawImage(Var.TailImage, _destinationPoints, _rect64, GraphicsUnit.Pixel, ia);
                }

                if (Var.RightWingImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(110 - ZBonus_WidthBonus - Var.WingSideSwitchBonus, 17 + ZBonus_HeightBonus_Over2),   // destination for upper-left point 
                    new Point(174 - ZBonus_WidthBonus, 17 + ZBonus_HeightBonus_Over2),  // destination for upper-right point
                    new Point(110 - ZBonus_WidthBonus - Var.WingSideSwitchBonus, 145 + ZBonus_HeightBonus_Over2)};
                    e.Graphics.DrawImage(Var.RightWingImage, _destinationPoints, _rect128, GraphicsUnit.Pixel, ia);
                }
                if (Var.LowerBodyImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(Var.LOWER_BODY_X - ZBonus_WidthBonus, Var.LOWER_BODY_Y + ZBonus_HeightBonus_Over2),   // destination for upper-left point 
                    new Point(Var.LOWER_BODY_X - ZBonus_WidthBonus + 64, Var.LOWER_BODY_Y + ZBonus_HeightBonus_Over2),  // destination for upper-right point
                    new Point(Var.LOWER_BODY_X - ZBonus_WidthBonus, Var.LOWER_BODY_Y + Var.HeightBonus + 64 + Var.ZBonus / 2)};
                    e.Graphics.DrawImage(Var.LowerBodyImage, _destinationPoints, _rect64, GraphicsUnit.Pixel, ia);
                }

                if (Var.LeftWingImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(55 - ZBonus_WidthBonus, 17 + ZBonus_HeightBonus_Over2),   // destination for upper-left point 
                    new Point(119 - ZBonus_WidthBonus - Var.WingSideSwitchBonus, 17 + ZBonus_HeightBonus_Over2),  // destination for upper-right point
                    new Point(55 - ZBonus_WidthBonus, 145 + ZBonus_HeightBonus_Over2)};
                    e.Graphics.DrawImage(Var.LeftWingImage, _destinationPoints, _rect128, GraphicsUnit.Pixel, ia);
                }

                if (Var.UpperBodyImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(75 - ZBonus_WidthBonus, 25 + ZBonus_HeightBonus_Over2),   // destination for upper-left point 
                    new Point(75 - ZBonus_WidthBonus + 64, 25 + ZBonus_HeightBonus_Over2),  // destination for upper-right point
                    new Point(75 - ZBonus_WidthBonus, 25 + 64 + ZBonus_HeightBonus_Over2)};
                    e.Graphics.DrawImage(Var.UpperBodyImage, _destinationPoints, _rect64, GraphicsUnit.Pixel, ia);
                }
            }

                //FRONT
                 if (Var.TailImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(79 + WidthBonus_Minus_WingSideSwitchBonus_Over2, 77 + Var.HeightBonus),   // destination for upper-left point 
                    new Point(143 + WidthBonus_Minus_WingSideSwitchBonus_Over2, 77 + Var.HeightBonus),  // destination for upper-right point
                    new Point(63 + WidthBonus_Minus_WingSideSwitchBonus_Over2, 138 + Var.HeightBonus)};
                    e.Graphics.DrawImage(Var.TailImage, _destinationPoints);
                 }
                
                 if (Var.RightWingImage != null)
                 {
                     Point[] _destinationPoints = {
                    new Point(110 + WidthBonus_Minus_WingSideSwitchBonus, 17 + Var.HeightBonus),   // destination for upper-left point 
                    new Point(174 + Var.WidthBonus, 17 + Var.HeightBonus),  // destination for upper-right point
                    new Point(110 + WidthBonus_Minus_WingSideSwitchBonus, 145 + Var.HeightBonus)};
                    e.Graphics.DrawImage(Var.RightWingImage, _destinationPoints);
                 } 
                if (Var.LowerBodyImage != null) 
                {e.Graphics.DrawImage(Var.LowerBodyImage, new Point(Var.LOWER_BODY_X + Var.WidthBonus, Var.LOWER_BODY_Y + Var.HeightBonus));}

                if (Var.LeftWingImage != null)
                {
                    Point[] _destinationPoints = {
                    new Point(55 + Var.WidthBonus, 17 + Var.HeightBonus),   // destination for upper-left point 
                    new Point(119 + WidthBonus_Minus_WingSideSwitchBonus, 17 + Var.HeightBonus),  // destination for upper-right point
                    new Point(55 + Var.WidthBonus, 145 + Var.HeightBonus)};
                    e.Graphics.DrawImage(Var.LeftWingImage, _destinationPoints);
                } 

                if (Var.UpperBodyImage != null)
                { e.Graphics.DrawImage(Var.UpperBodyImage, new Point(75 + Var.WidthBonus, 25 + Var.HeightBonus)); }

                //DIALOG
               if (Var.DialogToDraw != null)
               {
                   int _tempWidthBonus = Var.WidthBonus;
                   if (!Var.LookingRightWay)
                   {
                       e.Graphics.TranslateTransform(220 * Var.SizeLevel / 4, 0);
                       e.Graphics.ScaleTransform(-1 * Var.SizeLevel, Var.SizeLevel);
                       _tempWidthBonus = -_tempWidthBonus;
                   }
                   else
                   {
                       e.Graphics.ScaleTransform(Var.SizeLevel, Var.SizeLevel);
                   }

                                         Rectangle _rect = new Rectangle
                       (Var.LOWER_BODY_X + 75 + _tempWidthBonus, Var.LOWER_BODY_Y - 33 + Var.HeightBonus
                       , 20 + Convert.ToInt16(9.5 * Var.DialogToDraw.Length), Var.LOWER_BODY_Y - 43);
                       Rectangle _rectpie = new Rectangle
                       (Var.LOWER_BODY_X + 25 + _tempWidthBonus, Var.LOWER_BODY_Y - 40 + Var.HeightBonus
                       , Var.LOWER_BODY_X + 21, Var.LOWER_BODY_Y - 36);
                       e.Graphics.DrawRectangle(new Pen(Color.Black, 2), _rect);
                       e.Graphics.DrawPie(new Pen(Color.Black, 2), _rectpie, 90.0F, 45.0F);
                       e.Graphics.FillRectangle(Brushes.White, _rect);
                       e.Graphics.FillPie(Brushes.White, _rectpie, 90.0F, 45.0F);

                        GraphicsPath _gp = new GraphicsPath();
                        GraphicsPath _gpshadow = new GraphicsPath();
                        PointF _p = new PointF(Var.LOWER_BODY_X + 85 + _tempWidthBonus, Var.LOWER_BODY_Y - 31 + Var.HeightBonus);
                        PointF _pshadow = new PointF(Var.LOWER_BODY_X + 84 + _tempWidthBonus, Var.LOWER_BODY_Y - 30 + Var.HeightBonus);
                        if (Var.DialogToDraw != null)
                            _gp.AddString(Var.DialogToDraw, new FontFamily("Courier New"), (int)FontStyle.Bold, 15, _p, StringFormat.GenericDefault);
                        if (Var.DialogToDraw != null)
                            _gpshadow.AddString(Var.DialogToDraw, new FontFamily("Courier New"), (int)FontStyle.Bold, 15, _pshadow, StringFormat.GenericDefault);
                        e.Graphics.FillPath(Brushes.Black, _gpshadow);
                        e.Graphics.DrawPath(new Pen(Color.White, 2), _gp);
                        e.Graphics.FillPath(Brushes.Black, _gp);
               }
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
                    threadwil.Name = "Manual Mouse Window Movement Function";
                    threadwil.IsBackground = true;
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
            Random _rnd = new Random();
            if (Var.AllowDialogs)
            {
                if (Var.DialogToDraw != null)
                    Dialogs.refreshForm(0);

                Dialogs.setDialog(_rnd.Next(16) + 101, 0);
            }

            while(Var.LeftMouseButtonDown)
            {
                Thread.Sleep(20);
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
            if (Var.AllowDialogs)
            {
                Dialogs.refreshForm(0);
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
            Application.Run(new SettingsForm());
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Var.SettingsMenuIsOpen)
            {
                Thread threadform2 = new Thread(new ThreadStart(OpenForm2));
                threadform2.Name = "Settings Menu Form";
                threadform2.IsBackground = true;
                threadform2.Start();
                Var.SettingsMenuIsOpen = true;
            }
            else
            {
                MessageBox.Show("Settings menu is already open!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.CanFocus)
            {
                this.Focus();
            }
        }
    }
}
