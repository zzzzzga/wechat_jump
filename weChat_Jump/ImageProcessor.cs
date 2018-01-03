using ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace weChat_Jump
{
    public class ImageProcessor
    {
        private int pieceBaseHeight = 20;

        private int pieceBodyWidth = 70;

        public ImageProcessor(string filePath)
        {
            Image = new Image(filePath);
        }

        public Image Image { get; set; }

        public void GetImportantPoint(out int x, out int y, out int borderX, out int borderY)
        {
            int width = Image.Width;
            int height = Image.Height;
            x = 0;
            y = 0;
            borderX = 0;
            borderY = 0;
            Color lastPixel;
            int i, j, k;
            // 扫描棋子
            int scanXBorder = (width / 8);
            int scanStartY = 0;
            for (i = height / 3; i < height * 2 / 3; i+=50)
            {
                lastPixel = Image.Pixels[width * i];
                for(j = 1; j < width; j++)
                {
                    Color currentPixels = Image.Pixels[width * i + j];
                    if (currentPixels != lastPixel)
                    {
                        scanStartY = i - 50;
                        break;
                    }
                }
                if (scanStartY > 0)
                {
                    break;
                }
            }
            Console.WriteLine($"scan_start_y: {scanStartY}");
            int sumX = 0;
            int countX = 0;
            int maxY = 0;
            for(i = scanStartY; i < height * 2 / 3; i++)
            {
                for(j = scanXBorder; j < width - scanXBorder; j ++)
                {
                    Color pixel = Image.Pixels[width * i + j];
                    if (pixel.R<60 && pixel.R > 50 && pixel.G > 53 && pixel.G< 63 && pixel.B > 95 && pixel.B<110)
                    {
                        sumX += j;
                        countX += 1;
                        maxY = Math.Max(i, maxY);
                    }
                }
            }
            x = sumX / countX;
            y = maxY - pieceBaseHeight;

            int borderXStart = 0;
            int borderXEnd = 0;
            int borderXSum = 0;
            int borderXCount = 0;
            if (x < width / 2)
            {
                borderXStart = x;
                borderXEnd = width;
            }
            else
            {
                borderXStart = 0;
                borderXEnd = x;
            }
            for (k = height / 3; k<height*2/3;k++)
            {
                lastPixel = Image.Pixels[width * k];
                if (borderX > 0 || borderY > 0)
                {
                    break;
                }
                borderXSum = 0;
                borderXCount = 0;
                for(j = borderXStart; j < borderXEnd; j++)
                {
                    Color pixel = Image.Pixels[width * k + j];
                    if (Math.Abs(j - x) < pieceBodyWidth)
                    {
                        continue;
                    }
                    if ((Math.Abs(pixel.R - lastPixel.R) + Math.Abs(pixel.G - lastPixel.G) + Math.Abs(pixel.B - lastPixel.B)) > 10)
                    {
                        borderXSum += j;
                        borderXCount += 1;
                    }
                }
                if (borderXSum > 0)
                {
                    borderX = borderXSum / borderXCount;
                }
            }
            lastPixel = Image.Pixels[k * width + x];
            int _i = k + 274;
            for (; _i > k; _i--)
            {
                Color pixel = Image.Pixels[width * _i + x];
                if((Math.Abs(pixel.R - lastPixel.R) + Math.Abs(pixel.G - lastPixel.G) + Math.Abs(pixel.B - lastPixel.B)) < 10)
                {
                    break;
                }
            }
            borderY = (_i + k) / 2;

            int l = _i;
            for(; l < _i + 200; l ++)
            {
                Color pixel = Image.Pixels[width * l + x];
                if ((Math.Abs(pixel.R - 245) + Math.Abs(pixel.G - 245) + Math.Abs(pixel.B - 245)) == 0)
                {
                    borderY = l + 10;
                    break;
                }
            }
        }
        
        public void GetButtonPos(out int x1, out int y1, out int x2, out int y2)
        {
            /*
            w, h = im.size
    left = int(w / 2)
    top = int(1584 * (h / 1920.0))
    left = int(random.uniform(left - 50, left + 50))
    top = int(random.uniform(top - 10, top + 10))    # 随机防 ban
    swipe_x1, swipe_y1, swipe_x2, swipe_y2 = left, top, left, top*/
            x1 = Image.Width / 2;
            y1 = 1584 * (Image.Height / 1920);
            x2 = new Random().Next(x1 - 50, x1 + 50);
            y2 = new Random().Next(y1 - 10, y1 + 10);
        }
    }
}
