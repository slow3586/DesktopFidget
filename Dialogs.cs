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
    class Dialogs
    {
        public static void standartDialogs()
        {
            Var.Dialogs[0] = "I like your desktop background.";
            Var.Dialogs[1] = "What does this button do?";
            Var.Dialogs[2] = "What's that?";
            Var.Dialogs[3] = "I wonder what's beyond the screen's borders.";
            Var.Dialogs[4] = "I think I'll stay there for a while.";
            Var.Dialogs[5] = "What are you doing?";
            Var.Dialogs[6] = "Woah! It's really high up there!";
            Var.Dialogs[7] = "I'm sure you can do it, whatever is it you're doing.";
            Var.Dialogs[8] = "This looks like fun!";
            Var.Dialogs[9] = "Oh! Look at the time!";
            Var.Dialogs[10] = "I think I'll stay there for a while.";
            Var.Dialogs[11] = "Where did it go?";
            Var.Dialogs[12] = "I like colorful things.";
            Var.Dialogs[13] = "Who's that?";
            Var.Dialogs[14] = "Can you move this thing a little bit, please?";
            Var.Dialogs[15] = "There's just not enough space for me.";
            Var.Dialogs[16] = "Where did it go?";
            Var.Dialogs[17] = "What's an internet?";
            Var.Dialogs[18] = ":fhappy:";
            Var.Dialogs[19] = ":fsad:";
            Var.Dialogs[20] = "Wow!";
            Var.Dialogs[21] = "Hey!";
            Var.Dialogs[22] = "Huh?";
            Var.Dialogs[23] = "...";
            Var.Dialogs[24] = "This... doesn't look right.";
            Var.Dialogs[25] = "Boo! I say boo.";
            Var.Dialogs[26] = "I'm guardian of the- where did it go!?";
            Var.Dialogs[27] = "I would rather not go there.";
            Var.Dialogs[28] = "Oh.";
            Var.Dialogs[29] = "Can we go find some pictures of cute kittens?";
            Var.Dialogs[30] = "Hey! Click this!";
            Var.Dialogs[31] = "Ha-ha-ha!";
            Var.Dialogs[32] = "How does that even work?";
            Var.Dialogs[33] = "How long have you been looking at the screen?";
            Var.Dialogs[34] = "I'm tired!";
            Var.Dialogs[35] = "I'm hungry!";
            Var.Dialogs[36] = "Have you got something tasty?";
            Var.Dialogs[37] = "Come on! Do it!";
            Var.Dialogs[38] = "I don't like the look of this.";
            Var.Dialogs[39] = "Aww!";
            Var.Dialogs[40] = "I feel kind of lonely.";
            Var.Dialogs[41] = "There must be a way!";
            Var.Dialogs[42] = "Interesting.";
            Var.Dialogs[43] = "On it.";
            Var.Dialogs[44] = "I don't mind that.";
            Var.Dialogs[45] = "No!";
            Var.Dialogs[46] = "Yes!";
            Var.Dialogs[47] = "Hm-m.";
            Var.Dialogs[48] = "Come on, let's get this over with.";
            Var.Dialogs[49] = "Thanks!";
            Var.Dialogs[50] = "Thank you!";
            Var.Dialogs[51] = "That... wasn't nice.";
            Var.Dialogs[52] = "Interesting.";
            Var.Dialogs[53] = "Br-r-r! It's cold!";
            Var.Dialogs[54] = "Ah! What happened?";
            Var.Dialogs[55] = "My wings hurt after all this flying!";
            Var.Dialogs[56] = "*yawn*";
            Var.Dialogs[57] = "I want a pie!";
            Var.Dialogs[58] = "...1 - ((1 + (_cosx)) / 2... Huh? Just a bad dream.";
            Var.Dialogs[59] = "Do you want to see my breathtaking magic?";
            Var.Dialogs[60] = "*fidgets around*";
            Var.Dialogs[61] = "I will bite you!";
            Var.Dialogs[62] = "Hey, I'm not a pet!";
            Var.Dialogs[63] = "That's cute.";
            Var.Dialogs[64] = "I-i-it's so dark!";
            Var.Dialogs[65] = "What? No, I'm not scared!";
            Var.Dialogs[66] = "*blushes*";
            Var.Dialogs[67] = "I like books. With pictures. Lots of pictures.";
            Var.Dialogs[68] = "Love is a great motivation.";
            Var.Dialogs[69] = "I hope you don't mind me being there.";
            Var.Dialogs[70] = "Don't forget to treat other people nicely!";
            Var.Dialogs[71] = "Sometimes it's better to stay silent.";
            Var.Dialogs[72] = "I've made a present for you! But then I ate it...";
            Var.Dialogs[73] = "I like cakes.";
            Var.Dialogs[74] = "A day without sunshine is like, you know, night.";
            Var.Dialogs[75] = "I don't like someone who likes to show off.";
            Var.Dialogs[76] = "If something doesn't fit you, change it.";
            Var.Dialogs[77] = "Between two bad things, I pick the most fun one.";
            Var.Dialogs[78] = "Housework can't kill you, but why take a chance?";
            Var.Dialogs[79] = "Tonight's weather: dark.";
            Var.Dialogs[80] = "Always remember that you are absolutely unique.";
            Var.Dialogs[81] = "I'm an idealist. I don't really know what it means though.";
            Var.Dialogs[82] = "It's hard to change people with words.";
            Var.Dialogs[83] = "Sometimes people think they know a lot about each other.";
            Var.Dialogs[84] = "I want to live forever.";
            Var.Dialogs[85] = "I'm writing a book! It's about cute nimbats doing cute nimbat things.";
            Var.Dialogs[86] = "I didn't actually mean that.";
            Var.Dialogs[87] = "Age doesn't really matter.";
            Var.Dialogs[88] = "If you have a thought, do it.";
            Var.Dialogs[89] = "Did you know that everyone who breathes dies? Air is dangerous!";
            Var.Dialogs[90] = "If at first you don't succeed, try again.";
            Var.Dialogs[91] = "If you want to be thought a liar, always tell the truth.";
            Var.Dialogs[92] = "Forgiveness is better.";
            Var.Dialogs[93] = "(:";
            Var.Dialogs[94] = "Believe you can and you're halfway where you're going.";
            Var.Dialogs[95] = "Change your thoughts and you change your world.";
            Var.Dialogs[96] = "I am prepared for the worst, but hope for the best.";
            Var.Dialogs[97] = "You change your life by changing your heart.";
            Var.Dialogs[98] = "It is in your moments of decision that your destiny is shaped.";
            Var.Dialogs[99] = "Your heart and your hands can do anything together.";
            Var.Dialogs[100] = "Love and desire are the spirit's wings to great deeds.";
            Var.Dialogs[101] = "Hey!";
            Var.Dialogs[102] = "Woah!";
            Var.Dialogs[103] = "Oh!";
            Var.Dialogs[104] = "Okay!";
            Var.Dialogs[105] = "Hah!";
            Var.Dialogs[106] = "Ow!";
            Var.Dialogs[107] = "Wha-!";
            Var.Dialogs[108] = "!?";
            Var.Dialogs[109] = "Nu!";
            Var.Dialogs[110] = "Careful!";
            Var.Dialogs[111] = "Whe-e!";
            Var.Dialogs[112] = "Sto-op!";
            Var.Dialogs[113] = "Whoah!";
            Var.Dialogs[114] = "Aw!";
            Var.Dialogs[115] = "Wah!";
            Var.Dialogs[116] = "Ple-ease!";
        }

        public static void printDialogs()
        {
            string _dialogfile = Path.Combine(Application.StartupPath, "fidgetdialogs.txt");
            try
            {
                using (FileStream _fs = File.Create(_dialogfile))
                { }
                using (System.IO.StreamWriter _fs = new System.IO.StreamWriter(_dialogfile))
                {
                    foreach (string _s in Var.Dialogs)
                    {
                        _fs.WriteLine(_s);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Dialog file could NOT be created! Error:" + Convert.ToString(e), "DIALOG FILE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        public static bool readDialogs()
        {
            string _dialogfile = Path.Combine(Application.StartupPath, "fidgetdialogs.txt");
            if (File.Exists(_dialogfile))
            {
                try
                {
                    using (StreamReader _sr = new StreamReader(_dialogfile))
                    {
                        for (int _z = 0; _z < Var.Dialogs.Length; _z++ )
                        {
                            Var.Dialogs[_z] = _sr.ReadLine();
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Dialog file could NOT be read! Error:" + Convert.ToString(e), "DIALOG FILE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else
            {
                standartDialogs();
                return false;
            }
        }

        public static void refreshForm(int _whattodo)
        {
            IntPtr _window = NativeMethods.FindWindowByCaption(IntPtr.Zero, Var.WINDOW_NAME);
            Var.Rect _windowcurrentpos = new Var.Rect();
            NativeMethods.GetWindowRect(_window, ref _windowcurrentpos);
            if (_whattodo == 1)
            {
                Var.WindowSizeX += Var.DialogToDraw.Length * 10 + Var.LOWER_BODY_X + 100;
            }
            else
            {
                Var.WindowSizeX -= Var.DialogToDraw.Length * 10 + Var.LOWER_BODY_X + 100;
            }
            NativeMethods.MoveWindow(_window,
            _windowcurrentpos.Left,
            _windowcurrentpos.Top,
            Var.WindowSizeX, Var.WindowSizeY, true);
        }

        public static void clearDialog()
        {
            Var.DialogToDraw = null;
        }

        public static void setDialog(int _num, int _length)
        {
            Var.DialogToDraw = Var.Dialogs[_num];
            refreshForm(1);
            if (_length > 0)
            {
                Thread.Sleep(_length);
                refreshForm(0);
                clearDialog();
            }
        }

        public static void dialogTick()
        {
        
            int _realdialogslength = 0;
            for (int _z = 0; _z < Var.Dialogs.Length; _z++)
            {
                if (Var.Dialogs[_z] == null || Var.Dialogs[_z] == "" || String.IsNullOrEmpty(Var.Dialogs[_z]))
                {
                    _realdialogslength=_z;
                    _z = Var.Dialogs.Length;
                }
            }
            Random _rnd = new Random();
            Var.SecondsBeforeNextDialog = _rnd.Next(60*Var.DialogsFrequency/100, 75*Var.DialogsFrequency/100);
            while (true)
            {
                if (Var.SecondsSpentBeforeNextDialog>Var.SecondsBeforeNextDialog && Var.AllowDialogs)
                {
                    Var.SecondsSpentBeforeNextDialog = 0;
                    setDialog(_rnd.Next(_realdialogslength), 8000);
                }
                Var.SecondsSpentBeforeNextDialog++;
                Thread.Sleep(1000);
            }
        }

        public static string getDialog(int _num)
        {
            return Var.Dialogs[_num];
        }

        public static void drawBorder()
        {

        }
    }
}
