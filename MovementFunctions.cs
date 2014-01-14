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
    public class MovementFunctions
    {

        public static void HeightCalcLoop()
        {
            //This function calculates the way fidget will float around.
            //Just basic cos stuff.
            //The values can be seen in the paint part of the program when
            //the images are being drawn on the form.
            while (true)
            {
                Var.HeightBonus = Convert.ToInt32(Math.Ceiling(Math.Cos(Var.AngleHeightFlight) * Var.HeightBonusMultiplier));
                Var.WidthBonus = Convert.ToInt32(Math.Ceiling(Math.Cos(Var.AngleWidthFlight) * Var.WidthBonusMultiplier));
                Var.AngleHeightFlight = Var.AngleHeightFlight + Math.PI / Var.HeightBonusIncreaseMultiplier;
                Var.AngleWidthFlight = Var.AngleWidthFlight + Math.PI / Var.WidthBonusIncreaseMultiplier;
                if (Var.AngleHeightFlight > 2 * Math.PI) { Var.AngleHeightFlight = 0; }
                if (Var.AngleWidthFlight > 2 * Math.PI) { Var.AngleWidthFlight = 0; }
                if (Var.AngleHeightFlight < 0) { Var.AngleHeightFlight = Math.PI * 2; }
                if (Var.AngleWidthFlight < 0) { Var.AngleWidthFlight = Math.PI * 2; }
                Thread.Sleep(25);
            }
        }

        private static void FlightMovement(Var.Rect _newwindowpos, Var.Rect _windowcurrentpos, int _sleeptime)
        {
            IntPtr _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, Var.WINDOW_NAME);
            //no moving by user
            Var.AllowManualMovement = false;
            //FIND THE DISTANCE
            int _distancex = _newwindowpos.Left - (_windowcurrentpos.Left + Var.LOWER_BODY_X);
            int _distancey = _newwindowpos.Top - (_windowcurrentpos.Top + Var.LOWER_BODY_Y);
            int _distancesqr = Convert.ToInt32(Math.Floor(Math.Sqrt(Math.Pow(_distancex, 2) + Math.Pow(_distancey, 2))));
            if (_distancesqr > 1000) _distancesqr = 1000;
            if (_distancesqr < 50) return;
            //should we turn around?
            if ((Var.LookingRightWay == true && _distancex < 0) || (Var.LookingRightWay == false && _distancex > 0))
            { Var.TurnAroundState = 1; }
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
                Var.WingsSpeedParameter = Convert.ToInt32((_distancesqr / Var.WingsSpeedMultiplier) * (1 - (Math.Sin(_angle))) + (40 - _distancesqr / (Var.WingsSpeedMultiplier / 2)));
                NativeMethods.MoveWindow(_window,
                    _movex,
                    _movey,
                    Var.WindowSizeX, Var.WindowSizeY, true);
                _angle = _angle + Math.PI / Var.FlightSpeedMultiplier;
                Thread.Sleep(_sleeptime);
            }
            Var.WingsSpeedParameter = 40;
            Var.AllowManualMovement = true;
        }

        public static void FidgetsMind()
        {
            //Init moving
            Random _rnd = new Random();
            IntPtr _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, Var.WINDOW_NAME);
            Var.Rect _windowcurrentpos = new Var.Rect();
            Var.Rect _newwindowpos = new Var.Rect();
            Var.SecondsToNextMovement = Convert.ToInt32(_rnd.Next(Var.MOVEMENT_TIME_MODIFIER - Var.MovementFrequency, (Var.MOVEMENT_TIME_MODIFIER - Var.MovementFrequency) * 2));
            while (true)
            {
                if (Var.FollowTheMouse && Var.MovementDistance == 0)
                {
                    //DebugLabel.Visible = true;
                    //FIND THE TARGET
                    NativeMethods.GetWindowRect(_window, ref _windowcurrentpos);
                    _newwindowpos.Left = Cursor.Position.X + _rnd.Next(-20, 20);
                    _newwindowpos.Top = Cursor.Position.Y + _rnd.Next(-20, 20);
                    int _sleeptime = 15;
                    FlightMovement(_newwindowpos, _windowcurrentpos, _sleeptime);
                }
                //THIS ONE IS RESPONSIBLE FOR COMPLETELY RANDOM MOVEMENTS
                if (Var.MovementDistance != 0 && !Var.FollowTheMouse)
                {
                    if (Var.SecondsSpentBeforeNextMovement > Var.SecondsToNextMovement)
                    {
                        //generate new rnd
                        Var.SecondsToNextMovement = Convert.ToInt32(_rnd.Next(Var.MOVEMENT_TIME_MODIFIER - Var.MovementFrequency, (Var.MOVEMENT_TIME_MODIFIER - Var.MovementFrequency) * 2));
                        //FIND THE TARGET
                        NativeMethods.GetWindowRect(_window, ref _windowcurrentpos);
                        do
                        {
                            _newwindowpos.Top = Convert.ToInt32(_rnd.Next(-Screen.PrimaryScreen.Bounds.Height / 2 * Var.MovementDistance, Screen.PrimaryScreen.Bounds.Height / 2 * Var.MovementDistance));
                            _newwindowpos.Left = Convert.ToInt32(_rnd.Next(-Screen.PrimaryScreen.Bounds.Width / 2 * Var.MovementDistance, Screen.PrimaryScreen.Bounds.Width / 2 * Var.MovementDistance));
                        } while
                            //Make sure it's inside the monitor
                            (
                            0 > _windowcurrentpos.Left + _newwindowpos.Left ||
                            0 > _windowcurrentpos.Top + _newwindowpos.Top ||
                            Screen.PrimaryScreen.Bounds.Width - Var.WindowSizeX < _windowcurrentpos.Left + _newwindowpos.Left ||
                            Screen.PrimaryScreen.Bounds.Height - Var.WindowSizeY < _windowcurrentpos.Top + _newwindowpos.Top
                            );

                        //do the magic
                        int _sleeptime = 30;
                        FlightMovement(_newwindowpos, _windowcurrentpos, _sleeptime);
                        //

                        //WE DID IT!
                        Var.SecondsSpentBeforeNextMovement = 0;
                    }
                    //this isn't the end yet
                    Var.SecondsSpentBeforeNextMovement++;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
