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

//I got this example from http://www.codeproject.com/Articles/1966/An-INI-file-handling-class-using-C
//Not made by me

namespace DesktopFidget
{
    public class IniFile
    {
         public string path;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,string val,string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key,string def, StringBuilder retVal,
            int size,string filePath);

       public IniFile(string INIPath) 
       {
           path = INIPath;
       }

        public void IniWriteValue(string Section,string Key,string Value)
        {
            WritePrivateProfileString(Section,Key,Value,this.path);
        }
        
        public string IniReadValue(string Section,string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section,Key,"",temp, 
                                            255, this.path);
            return temp.ToString();

        }

        public static void OpenSettingsFile(bool _loadsettings,bool _savesettings)
        {
            string _inifile = Path.Combine(Application.StartupPath, "fidgetsettings.ini");
            //check if ini file exists
            if (!File.Exists(_inifile)) 
            {
                try
                {
                    using (FileStream fs = File.Create(_inifile))
                    { }
                    MessageBox.Show("INI file was not found and a new one was created successfully! The INI file stores all the settings and can be found in the executable's folder.", "INI SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    OpenSettingsFile(false, true);
                }
                catch (Exception e)
                {
                    MessageBox.Show("INI file was not found and a new one could NOT be created! Error:" + Convert.ToString(e), "INI ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            //define it
            IniFile inifile = new IniFile(_inifile);
            //load settings
            if (_loadsettings)
            {
                try
                {
                    Var.ClickThroughWindow = bool.Parse(inifile.IniReadValue("Common", "ClickThroughWindow"));
                    Var.FollowTheMouse = bool.Parse(inifile.IniReadValue("Common", "FollowTheMouse"));
                    Var.MovementDistance = Convert.ToInt32(inifile.IniReadValue("Common", "MovementDistance"));
                    Var.MovementFrequency = Convert.ToInt32(inifile.IniReadValue("Common", "MovementFrequency"));
                    Var.SizeLevel = Convert.ToInt32(inifile.IniReadValue("Common", "SizeLevel"));
                    Var.WindowStartingX = Convert.ToInt32(inifile.IniReadValue("Advanced", "WindowStartingX"));
                    Var.WindowStartingY = Convert.ToInt32(inifile.IniReadValue("Advanced", "WindowStartingY"));
                    Var.DebugMode = bool.Parse(inifile.IniReadValue("Advanced", "DebugMode"));
                    Var.WindowSizeX = Convert.ToInt32(inifile.IniReadValue("Advanced", "WindowSizeX"));
                    Var.WindowSizeY = Convert.ToInt32(inifile.IniReadValue("Advanced", "WindowSizeY"));
                    Var.HeightBonusMultiplier = Convert.ToInt32(inifile.IniReadValue("Advanced", "HeightBonusMultiplier"));
                    Var.WidthBonusMultiplier = Convert.ToInt32(inifile.IniReadValue("Advanced", "WidthBonusMultiplier"));
                    Var.HeightBonusIncreaseMultiplier = Convert.ToInt32(inifile.IniReadValue("Advanced", "HeightBonusIncreaseMultiplier"));
                    Var.WidthBonusIncreaseMultiplier = Convert.ToInt32(inifile.IniReadValue("Advanced", "WidthBonusIncreaseMultiplier"));
                    Var.FlightSpeedMultiplier = Convert.ToInt32(inifile.IniReadValue("Advanced", "FlightSpeedMultiplier"));

                    Var.IniFileWasLoaded = true;
                    //MessageBox.Show("INI settings loaded successfully!", "INI SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception e)
                {
                    MessageBox.Show("INI file exists but could not be loaded! Perhaps one of the lines is missing or has a wrong parameter? Try deleting the INI file. Error:" + Convert.ToString(e), "INI ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //save settings
            if (_savesettings)
            {
                try
                {
                    inifile.IniWriteValue("Do not edit", "Version", Convert.ToString(Var.ProgramVersion));
                    inifile.IniWriteValue("Common", "ClickThroughWindow", Convert.ToString(Var.ClickThroughWindow));
                    inifile.IniWriteValue("Common", "FollowTheMouse", Convert.ToString(Var.FollowTheMouse));
                    inifile.IniWriteValue("Common", "MovementDistance", Convert.ToString(Var.MovementDistance));
                    inifile.IniWriteValue("Common", "MovementFrequency", Convert.ToString(Var.MovementFrequency));
                    inifile.IniWriteValue("Common", "SizeLevel", Convert.ToString(Var.SizeLevel));
                    inifile.IniWriteValue("Advanced", "WindowStartingX", Convert.ToString(Var.WindowStartingX));
                    inifile.IniWriteValue("Advanced", "WindowStartingY", Convert.ToString(Var.WindowStartingY));
                    inifile.IniWriteValue("Advanced", "DebugMode", Convert.ToString(Var.DebugMode));
                    inifile.IniWriteValue("Advanced", "WindowSizeX", Convert.ToString(Var.WindowSizeX));
                    inifile.IniWriteValue("Advanced", "WindowSizeY", Convert.ToString(Var.WindowSizeY));
                    inifile.IniWriteValue("Advanced", "HeightBonusMultiplier", Convert.ToString(Var.HeightBonusMultiplier));
                    inifile.IniWriteValue("Advanced", "WidthBonusMultiplier", Convert.ToString(Var.WidthBonusMultiplier));
                    inifile.IniWriteValue("Advanced", "HeightBonusIncreaseMultiplier", Convert.ToString(Var.HeightBonusIncreaseMultiplier));
                    inifile.IniWriteValue("Advanced", "WidthBonusIncreaseMultiplier", Convert.ToString(Var.WidthBonusIncreaseMultiplier));
                    inifile.IniWriteValue("Advanced", "FlightSpeedMultiplier", Convert.ToString(Var.FlightSpeedMultiplier));
                    //MessageBox.Show("INI file saved successfully!", "INI SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception e)
                {
                    MessageBox.Show("INI file could not be saved! Perhaps it's set to read-only? Error:" + Convert.ToString(e), "INI ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }
    }
}
