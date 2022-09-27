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