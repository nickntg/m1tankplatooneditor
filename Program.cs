using System;
using System.IO;

namespace M1TankPlatoonEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: <Path to ROSTER>");
                return;
            }

            var file = Path.Combine(args[0], "ROSTER");
            if (!File.Exists(file))
            {
                Console.WriteLine("Cannot find {0}", file);
                return;
            }

            if (new FileInfo(file).Length != 3072)
            {
                Console.WriteLine("Invalid file length");
                return;
            }

            var bytes = File.ReadAllBytes(file);
            var platoons = new Platoon[8];

            var offset = 0;
            for (var i = 0; i < 8; i++)
            {
                platoons[i] = new Platoon();
                offset = platoons[i].Read(offset, bytes);
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Press the number of platoon to 'upgrade' or 0 to quit");
                for (var i = 0; i < 8; i++)
                {
                    Console.WriteLine("{0} - {1}", i + 1, platoons[i].Name);
                }

                var ch = Console.ReadKey(true);

                if (ch.KeyChar == '0')
                    return;

                if (Char.IsDigit(ch.KeyChar))
                {
                    var platoonIndex = Convert.ToInt32(ch.KeyChar)-48;
                    if (platoonIndex > 8)
                        continue;

                    platoons[platoonIndex - 1].Upgrade();
                    using (var sw = new StreamWriter(file))
                    {
                        foreach (var platoon in platoons)
                        {
                            sw.BaseStream.Write(platoon.Buffer, 0, platoon.Buffer.Length);
                        }
                    }
                }
            }
        }
    }
}
