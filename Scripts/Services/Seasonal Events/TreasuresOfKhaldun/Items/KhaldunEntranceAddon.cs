namespace Server.Items
{
    public class KhaldunEntranceAddon : BaseAddon
    {
        private static readonly int[,] m_AddOnSimpleComponents =
        {
              {476, 3, 5, 28}// 6	
			, {465, 3, 2, 8}, {465, 3, 1, 10}, {465, 3, 0, 14}// 7	8	9	
			, {466, 3, -1, 14}, {478, 2, 6, 28}, {476, 1, 5, 30}// 10	11	12	
			, {465, -1, 5, 27}, {465, -3, 2, 5}, {465, -3, 1, 6}// 13	14	15	
			, {465, -3, 0, 8}, {465, 2, 3, 5}, {465, 2, 2, 3}// 16	17	18	
			, {1313, -2, -4, 3}, {1313, 0, -4, 0}, {1313, -1, -5, 1}// 19	20	21	
			, {1313, 0, -6, 3}, {1313, -1, -6, 2}, {1313, -2, -6, 5}// 22	23	24	
			, {1929, -2, 1, 4}, {1929, -2, 0, 8}, {1929, -1, 1, 4}// 30	31	32	
			, {1929, 0, 1, 3}, {1929, 0, 0, 8}, {1929, 1, 1, 3}// 33	34	35	
			, {1929, 1, 0, 8}, {1929, -1, 0, 8}, {1928, -2, 0, 2}// 36	37	38	
			, {1928, -2, -1, 2}, {1928, -2, -2, 2}, {1928, -1, 0, 2}// 39	40	41	
			, {1928, -1, -1, 2}, {1928, -1, -2, 0}, {1928, 0, 0, 1}// 42	43	44	
			, {1928, 0, -1, 2}, {1928, 1, 0, 2}, {1928, 1, -1, 2}// 45	46	47	
			, {1928, 1, -1, 8}, {1928, 1, -2, 0}, {1928, -2, -1, 8}// 48	49	50	
			, {1928, -1, -1, 8}, {1928, 0, -1, 8}, {1928, 0, -2, 2}// 51	52	53	
			, {1931, -2, -2, 8}, {1931, -2, -3, 1}, {1931, -1, -2, 6}// 54	55	56	
			, {1931, -1, -3, 1}, {1931, 0, -3, 1}, {1931, 1, -2, 6}// 57	58	59	
			, {1931, 1, -3, 0}, {1931, 0, -2, 8}, {1931, 0, -3, 2}// 60	61	62	
			, {466, -3, -1, 11}, {466, 0, 5, 28}, {466, 2, 5, 24}// 63	64	65	
			, {6005, -3, -2, 4}, {6005, -3, -3, 2}// 66	67
        };

        public override BaseAddonDeed Deed => null;

        [Constructable]
        public KhaldunEntranceAddon()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);

            AddComplexComponent(this, 1313, 0, 1, 2, 1, -1, "", 1);// 25
            AddComplexComponent(this, 1313, -1, 1, 3, 1, -1, "", 1);// 26
            AddComplexComponent(this, 1313, -1, 2, 3, 1, -1, "", 1);// 27
            AddComplexComponent(this, 1313, -2, 2, 5, 1, -1, "", 1);// 28
            AddComplexComponent(this, 1313, 0, 2, 3, 1, -1, "", 1);// 29
        }

        public KhaldunEntranceAddon(Serial serial) : base(serial)
        {
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (!string.IsNullOrEmpty(name))
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            reader.ReadInt();
        }
    }
}
