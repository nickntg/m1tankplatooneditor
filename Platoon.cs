using System;

namespace M1TankPlatoonEditor
{
    public class Platoon
    {
        public string Name { get; set; }

        public Tank[] Tanks { get; set; }

        public byte[] Buffer { get; set; }

        public int Read(int byteOffset, byte[] buffer)
        {
            const int platoonLength = 24*16;

            Buffer = new byte[platoonLength];
            Array.Copy(buffer, byteOffset, Buffer, 0, platoonLength);

            Tanks = new Tank[4];

            Name = System.Text.Encoding.ASCII.GetString(Buffer, 0, 32).TrimEnd(new[] { '\0' });
            for (var i = 0; i < 4; i++)
            {
                Tanks[i] = new Tank();
                Tanks[i].Read(8 * 16 + i * 4 * 16, Buffer);
            }

            byteOffset += platoonLength;
            return byteOffset;
        }

        public void Upgrade()
        {
            for (var i = 0; i < 4; i++)
            {
                Tanks[i] = new Tank();
                Tanks[i].Upgrade(8 * 16 + i * 4 * 16, Buffer);
            }
        }
    }
}