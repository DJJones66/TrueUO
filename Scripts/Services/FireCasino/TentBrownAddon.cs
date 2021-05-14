namespace Server.Items
{
    public class tent_brownAddon : BaseAddon
    {
        private static readonly int[,] m_AddOnSimpleComponents =
        {
              {2967, 5, 2, 0}, {2881, 5, 1, 0}, {2882, 5, 2, 0}// 8	9	11	
			, {3221, 4, 5, 0}, {3221, -3, 4, 0}, {3206, 5, 1, 4}// 14	15	34	
			, {3083, 5, 2, 0}, {3208, 5, 2, 4}, {3209, 5, 2, 4}// 40	41	44	
			, {2168, 0, -5, 8}, {2881, 5, -4, 0}, {2882, 5, -3, 0}// 54	57	60	
			, {2123, -2, -2, 8}, {2122, 0, -2, 8}, {3206, 5, -4, 4}// 61	62	63	
			, {2167, -1, -2, 8}, {6424, 0, -3, 0}, {3209, 5, -3, 4}// 64	68	69	
			, {3221, 5, -5, 0}, {6419, 0, -2, 0}, {3208, 5, -3, 2}// 70	77	80	
			, {6427, -3, -2, 0}, {2168, 0, -3, 8}, {6426, -2, -2, 0}// 82	89	93	
			, {6424, 0, -4, 0}, {2121, 0, -4, 8}, {6425, 0, -5, 0}// 94	95	99	
			, {6426, -1, -2, 0}, {2167, -3, -2, 8}// 100	102	
		};

        public override BaseAddonDeed Deed => new tent_brownAddonDeed();

        [Constructable]
        public tent_brownAddon()
        {
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
            {
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);
            }

            AddComplexComponent(this, 496, 4, 3, 0, 1118, -1, "", 1);// 1
            AddComplexComponent(this, 2778, 0, 1, 0, 0, -1, "decorative golden rug", 1);// 2
            AddComplexComponent(this, 2786, 0, 2, 0, 0, -1, "decorative golden rug", 1);// 3
            AddComplexComponent(this, 2778, 0, 0, 0, 0, -1, "decorative golden rug", 1);// 4
            AddComplexComponent(this, 500, -4, 3, 0, 1118, -1, "", 1);// 5
            AddComplexComponent(this, 2786, 1, 2, 0, 0, -1, "decorative golden rug", 1);// 6
            AddComplexComponent(this, 2778, -1, 1, 0, 0, -1, "decorative golden rug", 1);// 7
            AddComplexComponent(this, 2785, 3, -1, 0, 0, -1, "decorative golden rug", 1);// 10
            AddComplexComponent(this, 503, -4, 2, 0, 1118, -1, "", 1);// 12
            AddComplexComponent(this, 2783, -3, -1, 0, 0, -1, "decorative golden rug", 1);// 13
            AddComplexComponent(this, 2783, -3, 0, 0, 0, -1, "decorative golden rug", 1);// 16
            AddComplexComponent(this, 2778, 1, 0, 0, 0, -1, "decorative golden rug", 1);// 17
            AddComplexComponent(this, 2783, -3, 1, 0, 0, -1, "decorative golden rug", 1);// 18
            AddComplexComponent(this, 2778, 1, -1, 0, 0, -1, "decorative golden rug", 1);// 19
            AddComplexComponent(this, 2781, -3, 2, 0, 0, -1, "decorative golden rug", 1);// 20
            AddComplexComponent(this, 2785, 3, 1, 0, 0, -1, "decorative golden rug", 1);// 21
            AddComplexComponent(this, 503, -4, -1, 0, 1118, -1, "", 1);// 22
            AddComplexComponent(this, 497, -1, 3, 0, 1118, -1, "", 1);// 23
            AddComplexComponent(this, 2778, 1, 1, 0, 0, -1, "decorative golden rug", 1);// 24
            AddComplexComponent(this, 2786, 2, 2, 0, 0, -1, "decorative golden rug", 1);// 25
            AddComplexComponent(this, 497, 0, 3, 0, 1118, -1, "", 1);// 26
            AddComplexComponent(this, 2778, -2, -1, 0, 0, -1, "decorative golden rug", 1);// 27
            AddComplexComponent(this, 497, 2, 3, 0, 1118, -1, "", 1);// 28
            AddComplexComponent(this, 2778, -2, 0, 0, 0, -1, "decorative golden rug", 1);// 29
            AddComplexComponent(this, 497, 1, 3, 0, 1118, -1, "", 1);// 30
            AddComplexComponent(this, 2786, -2, 2, 0, 0, -1, "decorative golden rug", 1);// 31
            AddComplexComponent(this, 2778, -1, 0, 0, 0, -1, "decorative golden rug", 1);// 32
            AddComplexComponent(this, 497, -2, 3, 0, 1118, -1, "", 1);// 33
            AddComplexComponent(this, 2778, -2, 1, 0, 0, -1, "decorative golden rug", 1);// 35
            AddComplexComponent(this, 2779, 3, 2, 0, 0, -1, "decorative golden rug", 1);// 36
            AddComplexComponent(this, 2778, 0, -1, 0, 0, -1, "decorative golden rug", 1);// 37
            AddComplexComponent(this, 2786, -1, 2, 0, 0, -1, "decorative golden rug", 1);// 38
            AddComplexComponent(this, 2778, 2, -1, 0, 0, -1, "decorative golden rug", 1);// 39
            AddComplexComponent(this, 497, 3, 3, 0, 1118, -1, "", 1);// 42
            AddComplexComponent(this, 498, 4, 2, 0, 1118, -1, "", 1);// 43
            AddComplexComponent(this, 2785, 3, 0, 0, 0, -1, "decorative golden rug", 1);// 45
            AddComplexComponent(this, 498, 4, 1, 0, 1118, -1, "", 1);// 46
            AddComplexComponent(this, 503, -4, 0, 0, 1118, -1, "", 1);// 47
            AddComplexComponent(this, 2778, 2, 0, 0, 0, -1, "decorative golden rug", 1);// 49
            AddComplexComponent(this, 2778, 2, 1, 0, 0, -1, "decorative golden rug", 1);// 50
            AddComplexComponent(this, 497, -3, 3, 0, 1118, -1, "", 1);// 51
            AddComplexComponent(this, 2778, -1, -1, 0, 0, -1, "decorative golden rug", 1);// 52
            AddComplexComponent(this, 503, -4, 1, 0, 1118, -1, "", 1);// 53
            AddComplexComponent(this, 2784, 2, -4, 0, 0, -1, "decorative golden rug", 1);// 55
            AddComplexComponent(this, 2784, 1, -4, 0, 0, -1, "decorative golden rug", 1);// 56
            AddComplexComponent(this, 2778, 1, -2, 0, 0, -1, "decorative golden rug", 1);// 59
            AddComplexComponent(this, 2778, -1, -3, 0, 0, -1, "decorative golden rug", 1);// 65
            AddComplexComponent(this, 2778, -1, -2, 0, 0, -1, "decorative golden rug", 1);// 66
            AddComplexComponent(this, 2778, -2, -2, 0, 0, -1, "decorative golden rug", 1);// 67
            AddComplexComponent(this, 501, 4, -5, 0, 1118, -1, "", 1);// 71
            AddComplexComponent(this, 498, 4, -4, 0, 1118, -1, "", 1);// 72
            AddComplexComponent(this, 503, -4, -4, 0, 1118, -1, "", 1);// 73
            AddComplexComponent(this, 2784, -1, -4, 0, 0, -1, "decorative golden rug", 1);// 74
            AddComplexComponent(this, 498, 4, -3, 0, 1118, -1, "", 1);// 75
            AddComplexComponent(this, 2785, 3, -2, 0, 0, -1, "decorative golden rug", 1);// 76
            AddComplexComponent(this, 2778, 2, -3, 0, 0, -1, "decorative golden rug", 1);// 78
            AddComplexComponent(this, 2778, 1, -3, 0, 0, -1, "decorative golden rug", 1);// 79
            AddComplexComponent(this, 2778, 2, -2, 0, 0, -1, "decorative golden rug", 1);// 81
            AddComplexComponent(this, 2784, -2, -4, 0, 0, -1, "decorative golden rug", 1);// 83
            AddComplexComponent(this, 499, -4, -5, 0, 1118, -1, "", 1);// 84
            AddComplexComponent(this, 2780, -3, -4, 0, 0, -1, "decorative golden rug", 1);// 85
            AddComplexComponent(this, 2778, 0, -2, 0, 0, -1, "decorative golden rug", 1);// 86
            AddComplexComponent(this, 2783, -3, -2, 0, 0, -1, "decorative golden rug", 1);// 87
            AddComplexComponent(this, 2784, 0, -4, 0, 0, -1, "decorative golden rug", 1);// 88
            AddComplexComponent(this, 503, -4, -3, 0, 1118, -1, "", 1);// 90
            AddComplexComponent(this, 2785, 3, -3, 0, 0, -1, "decorative golden rug", 1);// 91
            AddComplexComponent(this, 2778, -2, -3, 0, 0, -1, "decorative golden rug", 1);// 92
            AddComplexComponent(this, 503, -4, -2, 0, 1118, -1, "", 1);// 96
            AddComplexComponent(this, 2783, -3, -3, 0, 0, -1, "decorative golden rug", 1);// 97
            AddComplexComponent(this, 2782, 3, -4, 0, 0, -1, "decorative golden rug", 1);// 98
            AddComplexComponent(this, 2778, 0, -3, 0, 0, -1, "decorative golden rug", 1);// 101
        }

        public tent_brownAddon(Serial serial) : base(serial)
        {
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            var ac = new AddonComponent(item);

            if (!string.IsNullOrEmpty(name))
            {
                ac.Name = name;
            }

            if (hue != 0)
            {
                ac.Hue = hue;
            }

            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
            {
                ac.Light = (LightType)lightsource;
            }

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

    public class tent_brownAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon => new tent_brownAddon();

        [Constructable]
        public tent_brownAddonDeed()
        {
        }

        public tent_brownAddonDeed(Serial serial) : base(serial)
        {
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
