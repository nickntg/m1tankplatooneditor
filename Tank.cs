namespace M1TankPlatoonEditor
{
    public class Tank
    {
        public byte Sabot { get; set; }

        public byte Heat { get; set; }

        public byte[] Ratings { get; set; }

        public void Read(int offset, byte[] buffer)
        {
            Ratings = new byte[4];

            Sabot = buffer[offset + 2];
            Heat = buffer[offset + 4];

            for (var i = 0; i < 4; i++)
            {
                Ratings[i] = buffer[offset + 12 + i*14];
            }
        }

        public void Upgrade(int offset, byte[] buffer)
        {
            buffer[offset + 2] = 30;
            buffer[offset + 4] = 20;

            for (var i = 0; i < 4; i++)
            {
                buffer[offset + 12 + i * 14] = 6;
            }

            Read(offset, buffer);
        }
    }
}