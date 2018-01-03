using ImageSharp;
using System;
using System.IO;
using System.Threading;

namespace weChat_Jump
{
    class Program
    {
        static void Main(string[] args)
        {
            //Image image = new Image("autojump.png");
            //image.BlackWhite();

            //image.Save("autojump.png");
            //foreach(var color in image.Pixels)
            //{
            //    Console.WriteLine($"R: {color.R}, G: {color.G}, B: {color.B}");
            //}
            //string output = "", error = "";
            //WinCmd.Execute("\"python\" \"-V\"", out output, out error);
            //Console.WriteLine(output);
            //Console.WriteLine(error);
            while(true)
            {
                try
                {
                    double rad = 1.392;
                    if (File.Exists(PhoneProcessor.filename))
                    {
                        File.Delete(PhoneProcessor.filename);
                    }
                    while (!File.Exists(PhoneProcessor.filename))
                    {
                        PhoneProcessor.GetPhoneScreen();
                        Thread.Sleep(1000);
                    }
                    
                    var ip = new ImageProcessor(PhoneProcessor.filename);
                    int x1, x2, y1, y2;
                    try
                    {
                        ip.GetImportantPoint(out x1, out y1, out x2, out y2);
                    }
                    catch(Exception ex)
                    {
                        x1 = 0; x2 = 0; y1 = 0; y2 = 0;
                    }
                    int time = Convert.ToInt32(Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)) * rad);
                    ip.GetButtonPos(out x1, out y1, out x2, out y2);
                    PhoneProcessor.Jump(x1, y1, x2, y2, time);
                    Thread.Sleep(new Random().Next(1000, 1500));
                }
                catch(Exception ex)
                {
                    Console.WriteLine("出现错误：" + ex.ToString());
                }
            }
        }
    }
}
