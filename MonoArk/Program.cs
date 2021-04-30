using System;

namespace MonoArk
{
#if WINDOWS || LINUX

    public static class Program
    {
  
        [STAThread]
        static void Main()
        {
            using (var game = new GameProgram())
                game.Run();
        }
    }
#endif
}
