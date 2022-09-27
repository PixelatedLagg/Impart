using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        VideoStorage video = new VideoStorage("<video src=\"test\" width=\"320\" height=\"240\" controls autoplay></video>", 0);
        Console.WriteLine(video.ToBuilder());
    }
}

/*
< v i d e o   s r c =  "  t  e  s  t  "     w  i  d  t  h  =  "  3  2  0  "     h  e  i  g  h  t  =  "  2  4  0  "     c  o  n  t  r  o  l  s     a  u  t  o  p  l  a  y  >  <  /  v  i  d  e  o  > 
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68

*/