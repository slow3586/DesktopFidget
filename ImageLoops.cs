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
    class ImageLoops
    {

        public static void WingsImageLoop()
        {
            while (true)
            {
                //Take the frame we need
                Var.LeftWingImage = Var.CutFrame[Var.LeftWingState];
                Var.RightWingImage = Var.CutFrame[Var.RightWingState];
                //Refresh the form for the change to appear.
                GraphicsForm.RefreshTheForm();
                Thread.Sleep(Var.WingsSpeedParameter);
                Var.LeftWingState++;
                Var.RightWingState++;
                if (Var.LeftWingState == 8) { Var.LeftWingState = 0; }
                if (Var.RightWingState == 16) { Var.RightWingState = 8; }
            }
        }

        public static void UpperBodyImageLoop()
        {
            //Create random numbers that define when the animations
            //will happen.
            Random _rnd = new Random();
            Var.NeedTBS1 = Convert.ToInt32(_rnd.Next(5, 12));
            Var.NeedTBS2 = Convert.ToInt32(_rnd.Next(15, 25));
            Var.NeedTBS3 = Convert.ToInt32(_rnd.Next(5, 12));
            while (true)
            {
                //Check for double click animation.
                if (Var.ActivateShot)
                {
                    //Go through the frames we need using for loop.
                    for (int _a = 25; _a < 31; _a++)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    //Return back to normal.
                    Var.UpperBodyImage = Var.CutFrame[16];
                    Var.ActivateShot = false;
                }

                //Check for turn around state
                if (Var.TurnAroundState == 2)
                {
                    Var.LowerBodyImage = Var.CutFrame[32];
                    int _b = 131;
                    for (int _a = 33; _a < 40; _a++)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        Var.TailImage = Var.CutFrame[_b];
                        _b = _b + 4;
                        Var.WingSideSwitchBonus = Var.WingSideSwitchBonus + 3;
                        GraphicsForm.RefreshTheForm();
                        if (_a < 39)
                        { Thread.Sleep(50); }
                    }
                    //HAHAHAHA I HAVE NO IDEA WHAT I AM DOING
                    Var.TailState = 95;
                    Var.LowerBodyState = 225;
                    Var.LookingRightWay = !Var.LookingRightWay;
                    Var.WingSideSwitchBonus = 0;
                    if (Var.AngleWidthFlight > Math.PI)
                    { Var.AngleWidthFlight = Math.PI - (2 * Math.PI - Var.AngleWidthFlight); }
                    else if (Var.AngleWidthFlight < Math.PI)
                    { Var.AngleWidthFlight = 2 * Math.PI - (Math.PI - Var.AngleWidthFlight); }
                    Var.TurnAroundState = 0;
                }

                //Looking upwards animation.
                if (Var.UpperBodyState1 == Var.NeedTBS1)
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    Var.UpperBodyImage = Var.CutFrame[16];
                    //Wait some time in this state before going back to normal.
                    Thread.Sleep(Convert.ToInt32(_rnd.Next(700, 2000)));
                    for (int _a = 23; _a < 25; _a++)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(100);
                    }
                    Var.UpperBodyState1 = 0;
                    Var.NeedTBS1 = Convert.ToInt32(_rnd.Next(5, 12));
                }

                //Looking upwards and then behind animation.
                if (Var.UpperBodyState2 == Var.NeedTBS2)
                {
                    for (int _a = 25; _a > 21; _a--)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    for (int _a = 16; _a < 22; _a++)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        if (_a == 16 || _a == 21) { Thread.Sleep(Convert.ToInt32(_rnd.Next(500, 2000))); }
                        else { Thread.Sleep(60); }
                    }
                    for (int _a = 21; _a > 16; _a--)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    Var.UpperBodyState2 = 0;
                    Var.NeedTBS2 = Convert.ToInt32(_rnd.Next(15, 25));
                }

                //Looking behind animation.
                if (Var.UpperBodyState3 == Var.NeedTBS3)
                {
                    for (int _a = 18; _a < 21; _a++)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    Var.UpperBodyImage = Var.CutFrame[21];
                    Thread.Sleep(Convert.ToInt32(_rnd.Next(700, 2000)));
                    for (int _a = 21; _a > 17; _a--)
                    {
                        Var.UpperBodyImage = Var.CutFrame[_a];
                        GraphicsForm.RefreshTheForm();
                        Thread.Sleep(60);
                    }
                    Var.UpperBodyState3 = 0;
                    Var.NeedTBS3 = Convert.ToInt32(_rnd.Next(5, 12));
                }

                //At the end of the day if nothing is happening
                //put the normal frame on and wait a second.
                Var.UpperBodyImage = Var.CutFrame[31];
                if (Var.TurnAroundState == 0)
                {
                    Var.UpperBodyState1 = Var.UpperBodyState1 + 0.25;
                    Var.UpperBodyState2 = Var.UpperBodyState2 + 0.25;
                    Var.UpperBodyState3 = Var.UpperBodyState3 + 0.25;
                };
                Thread.Sleep(250);
            }
        }

        public static void LowerBodyImageLoop()
        {
            while (true)
            {
                //Same idea here as in upper body loop but with less random
                //and more looping through the same stuff over and over.
                if (Var.TurnAroundState == 1 && (Var.TailState > 135 && Var.TailState < 140))
                {
                    Var.TurnAroundState++;
                }
                else
                {
                    if (Var.TurnAroundState != 2)
                    {
                        Var.LowerBodyImage = Var.CutFrame[Var.LowerBodyState];
                        Var.LowerBodyState++;
                        if (Var.LowerBodyState == 239)
                        { Var.LowerBodyState = 160; }
                        Var.TailImage = Var.CutFrame[Var.TailState];
                        GraphicsForm.RefreshTheForm();
                        Var.TailState++;
                        if (Var.TailState == 159)
                        { Var.TailState = 80; }
                    }
                }
                if (Var.TurnAroundState == 0)
                {
                    Thread.Sleep(50);
                }
                else { Thread.Sleep(35); }
            }
        }
    }
}
