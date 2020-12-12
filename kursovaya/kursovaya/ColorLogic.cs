using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace kursovaya
{
    public class ColorLogic
    {
        static public double getMax(double a, double b, double c)
        {
            //максимальный элемент из трех
            return Math.Max(Math.Max(a, b), c);
        }

        static public double getMin(double a, double b, double c)
        {
            //минимальный элемент из трех
            return Math.Min(Math.Min(a, b), c);
        }
        static public void HSVToRGB(double H, double S, double V, out int R, out int G, out int B)
        {          
            S /= 100;
            V /= 100;
            double C = V * S; //насыщенность цвета
            double Hi = H / 60; //сектор круга оттенков
            double X = C * (1 - Math.Abs((Hi % 2) - 1)); //точка вдоль трех нижних граней куба RGB
            double RT = 0, GT = 0, BT = 0;

            if (Hi >= 0 && Hi <= 1)
            {
                RT = C; GT = X; BT = 0;
            }
            else if (Hi > 1 && Hi <= 2)
            {
                RT = X; GT = C; BT = 0;
            }
            else if (Hi > 2 && Hi <= 3)
            {
                RT = 0; GT = C; BT = X;
            }
            else if (Hi > 3 && Hi <= 4)
            {
                RT = X; GT = X; BT = C;
            }
            else if (Hi > 4 && Hi <= 5)
            {
                RT = X; GT = 0; BT = C;
            }
            else if (Hi > 5 && Hi <= 6)
            {
                RT = C; GT = 0; BT = X;
            }

            double m = V - C; //выведение дополнительной насыщенности для прибавления ее к RGB
            R = (int)Math.Round((RT + m) * 255);
            G = (int)Math.Round((GT + m) * 255);
            B = (int)Math.Round((BT + m) * 255);
        }

        static public void RGBToHSV(int R, int G, int B, out double H, out double S, out double V)
        {
            double RT, GT, BT;
            RT = R / 255d; //изменить значения R,G,B
            GT = G / 255d; //с диапазона 0..255
            BT = B / 255d; //к диапазону 0..1

            double Cmax = getMax(RT, GT, BT); //максимум из R,G,B
            double Cmin = getMin(RT, GT, BT); //минимум из R,G,B
            double delta = Cmax - Cmin; //разница максимума и минимума

            if (delta == 0) //если все значения R,G,B равны 0
            {
                H = 0;
            }
            else if (Cmax == RT) //если цвет между желтым и пурпурным
            {
                H = ((GT - BT) / delta) % 6;
            }
            else if (Cmax == GT) //если цвет между голубым и желтым
            {
                H = ((BT - RT) / delta) + 2;
            }
            else //если цвет между пурпурным и голубым
            {
                H = ((RT - GT) / delta) + 4;
            }
            H *= 60;
            if (H < 0) H += 360; //если Hue получилось отрицательным, прибавить 360 градусов

            S = Cmax == 0 ? 0 : delta / Cmax;

            V = Cmax;
        }

        static public string addZero(int color)
        {
            /*
             * добавляет ноль слева в HEX код, если число стоит однозначное
             * т.е. если стоит число 7, то надо дописать к нему 0, чтобы получилось 07
             * потому что если написать просто 7, то HEX код будет неверным
             */
            if (color <= 16) return "0";
            else return "";
        }
    }
}