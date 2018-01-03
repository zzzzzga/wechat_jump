using System;
using System.Collections.Generic;
using System.Text;

namespace weChat_Jump
{
    public class PhoneProcessor
    {
        public static readonly string filename = "autojump.png";
        private static readonly string cmd_1 = $"\"adb\" shell screencap -p \"/sdcard/{filename}\"";
        private static readonly string cmd_2 = $"\"adb\" pull \"/sdcard/{filename}\" \".\"";
        private static readonly string cmd_3 = "\"adb\" shell input swipe \"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"";
        public static void GetPhoneScreen()
        {
            // 1. 
            string output = "", error = "";
            WinCmd.Execute(cmd_1, out output, out error);
            WinCmd.Execute(cmd_2, out output, out error);
        }

        public static void Jump(int x1, int y1, int x2, int y2, int time)
        {
            string output = "", error = "";
            WinCmd.Execute(string.Format(cmd_3, x1, y1, x2, y2, time < 200 ? 200: time), out output, out error);
        }
    }
}
