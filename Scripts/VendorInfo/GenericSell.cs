using Server.Items;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public class GenericSellInfo : IShopSellInfo
    {
        private readonly Dictionary<Type, int> m_Table = new Dictionary<Type, int>();
        private Type[] m_Types;

        public Type[] Types
        {
            get
            {
                if (m_Types == null)
                {
                    m_Types = new Type[m_Table.Keys.Count];
                    m_Table.Keys.CopyTo(m_Types, 0);
                }

                return m_Types;
            }
        }
        public void Add(Type type, int price)
        {
            m_Table[type] = price;
            m_Types = null;
        }

        public int GetSellPriceFor(Item item)
        {
            return GetSellPriceFor(item, null);
        }

        public int GetSellPriceFor(Item item, BaseVendor vendor)
        {
            int price;

            m_Table.TryGetValue(item.GetType(), out price);

            if (vendor != null && BaseVendor.UseVendorEconomy)
            {
                IBuyItemInfo buyInfo = null;

                var infos = vendor.GetBuyInfo();

                for (var index = 0; index < infos.Length; index++)
                {
                    IBuyItemInfo info = infos[index];

                    if (info is GenericBuyInfo info1)
                    {
                        if (info1.EconomyItem && info1.Type == item.GetType())
                        {
                            buyInfo = info1;
                            break;
                        }
                    }
                }

                if (buyInfo != null)
                {
                    int sold = buyInfo.TotalSold;

                    price = (int)(buyInfo.Price * .75);

                    return Math.Max(1, price);
                }
            }

            if (item is BaseArmor armor)
            {
                if (armor.Quality == ItemQuality.Low)
                {
                    price = (int) (price * 0.60);
                }
                else if (armor.Quality == ItemQuality.Exceptional)
                {
                    price = (int) (price * 1.25);
                }

                price += 5 * armor.ArmorAttributes.DurabilityBonus;

                if (price < 1)
                {
                    price = 1;
                }
            }
            else if (item is BaseWeapon weapon)
            {
                if (weapon.Quality == ItemQuality.Low)
                {
                    price = (int) (price * 0.60);
                }
                else if (weapon.Quality == ItemQuality.Exceptional)
                {
                    price = (int) (price * 1.25);
                }

                price += 100 * weapon.WeaponAttributes.DurabilityBonus;

                price += 10 * weapon.Attributes.WeaponDamage;

                if (price < 1)
                {
                    price = 1;
                }
            }
            else if (item is BaseBeverage bev)
            {
                int price1 = price, price2 = price;

                if (bev is Pitcher)
                {
                    price1 = 3;
                    price2 = 5;
                }
                else if (bev is BeverageBottle)
                {
                    price1 = 3;
                    price2 = 3;
                }
                else if (bev is Jug)
                {
                    price1 = 6;
                    price2 = 6;
                }

                if (bev.IsEmpty || bev.Content == BeverageType.Milk)
                {
                    price = price1;
                }
                else
                {
                    price = price2;
                }
            }

            return price;
        }

        public int GetBuyPriceFor(Item item)
        {
            return GetBuyPriceFor(item, null);
        }

        public int GetBuyPriceFor(Item item, BaseVendor vendor)
        {
            return (int)(1.90 * GetSellPriceFor(item, vendor));
        }

        public string GetNameFor(Item item)
        {
            if (item.Name != null)
            {
                return item.Name;
            }

            return item.LabelNumber.ToString();
        }

        public bool IsSellable(Item item)
        {
            if (item.QuestItem)
                return false;

            return IsInList(item.GetType());
        }

        public bool IsResellable(Item item)
        {
            if (item.QuestItem)
                return false;

            return IsInList(item.GetType());
        }

        public bool IsInList(Type type)
        {
            return m_Table.ContainsKey(type);
        }
    }
}
