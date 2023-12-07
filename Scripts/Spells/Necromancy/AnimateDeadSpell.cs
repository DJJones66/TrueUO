using Server.Items;
using Server.Mobiles;
using Server.Targeting;

using System;
using System.Collections.Generic;

namespace Server.Spells.Necromancy
{
    public class AnimateDeadSpell : NecromancerSpell
    {
        private static readonly SpellInfo _Info = new SpellInfo(
            "Animate Dead", "Uus Corp",
            203,
            9031,
            Reagent.GraveDust,
            Reagent.DaemonBlood);

        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(1.75);
        public override double RequiredSkill => 40.0;
        public override int RequiredMana => 23;

        public AnimateDeadSpell(Mobile caster, Item scroll)
            : base(caster, scroll, _Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
            Caster.SendLocalizedMessage(1061083); // Animate what corpse?
        }

        private class CreatureGroup
        {
            public readonly Type[] m_Types;
            public readonly SummonEntry[] m_Entries;

            public CreatureGroup(Type[] types, SummonEntry[] entries)
            {
                m_Types = types;
                m_Entries = entries;
            }
        }

        private class SummonEntry
        {
            public readonly Type[] m_ToSummon;
            public readonly int m_Requirement;

            public SummonEntry(int requirement, params Type[] toSummon)
            {
                m_ToSummon = toSummon;
                m_Requirement = requirement;
            }
        }

        private static CreatureGroup FindGroup(Type type)
        {
            for (int i = 0; i < _Groups.Length; ++i)
            {
                CreatureGroup group = _Groups[i];
                Type[] types = group.m_Types;

                bool contains = types.Length == 0;

                for (int j = 0; !contains && j < types.Length; ++j)
                {
                    contains = types[j].IsAssignableFrom(type);
                }

                if (contains)
                {
                    return group;
                }
            }

            return null;
        }

        private static readonly CreatureGroup[] _Groups =
        {
            // Undead group--empty
            new CreatureGroup(SlayerGroup.GetEntryByName(SlayerName.Silver).Types, Array.Empty<SummonEntry>()),
            // Insects
            new CreatureGroup(new[]
            {
                typeof(DreadSpider), typeof(FrostSpider), typeof(GiantSpider), typeof(GiantBlackWidow),
                typeof(BlackSolenInfiltratorQueen), typeof(BlackSolenInfiltratorWarrior),
                typeof(BlackSolenQueen), typeof(BlackSolenWarrior), typeof(BlackSolenWorker),
                typeof(RedSolenInfiltratorQueen), typeof(RedSolenInfiltratorWarrior),
                typeof(RedSolenQueen), typeof(RedSolenWarrior), typeof(RedSolenWorker),
                typeof(TerathanAvenger), typeof(TerathanDrone), typeof(TerathanMatriarch),
                typeof(TerathanWarrior)
                // TODO: Giant beetle? Ant lion? Ophidians?
            },
                new[]
                {
                    new SummonEntry(0, typeof(MoundOfMaggots))
                }),
            // Mounts
            new CreatureGroup(new[]
            {
                typeof(Horse), typeof(Nightmare), typeof(FireSteed),
                typeof(Kirin), typeof(Unicorn)
            }, new[]
               {
                   new SummonEntry(10000, typeof(HellSteed)),
                   new SummonEntry(0, typeof(SkeletalMount))
               }),
            // Elementals
            new CreatureGroup(new[]
            {
                typeof(BloodElemental), typeof(EarthElemental), typeof(SummonedEarthElemental),
                typeof(AgapiteElemental), typeof(BronzeElemental), typeof(CopperElemental),
                typeof(DullCopperElemental), typeof(GoldenElemental), typeof(ShadowIronElemental),
                typeof(ValoriteElemental), typeof(VeriteElemental), typeof(PoisonElemental),
                typeof(FireElemental), typeof(SummonedFireElemental), typeof(SnowElemental),
                typeof(AirElemental), typeof(SummonedAirElemental), typeof(WaterElemental),
                typeof(SummonedAirElemental), typeof (ToxicElemental)
            }, new[]
               {
                   new SummonEntry(5000, typeof(WailingBanshee)),
                   new SummonEntry(0, typeof(Wraith))
               }),
            // Dragons
            new CreatureGroup(new[]
            {
                typeof(AncientWyrm), typeof(Dragon), typeof(GreaterDragon), typeof(SerpentineDragon),
                typeof(ShadowWyrm), typeof(SkeletalDragon), typeof(WhiteWyrm),
                typeof(Drake), typeof(Wyvern), typeof(LesserHiryu), typeof(Hiryu)
            }, new[]
               {
                   new SummonEntry(18000, typeof(SkeletalDragon)),
                   new SummonEntry(10000, typeof(FleshGolem)),
                   new SummonEntry(5000, typeof(Lich)),
                   new SummonEntry(3000, typeof(SkeletalKnight), typeof(BoneKnight)),
                   new SummonEntry(2000, typeof(Mummy)),
                   new SummonEntry(1000, typeof(SkeletalMage), typeof(BoneMagi)),
                   new SummonEntry(0, typeof(PatchworkSkeleton))
               }),
            // Default group
            new CreatureGroup(Type.EmptyTypes, new[]
            {
                new SummonEntry(18000, typeof(LichLord)),
                new SummonEntry(10000, typeof(FleshGolem)),
                new SummonEntry(5000, typeof(Lich)),
                new SummonEntry(3000, typeof(SkeletalKnight), typeof(BoneKnight)),
                new SummonEntry(2000, typeof(Mummy)),
                new SummonEntry(1000, typeof(SkeletalMage), typeof(BoneMagi)),
                new SummonEntry(0, typeof(PatchworkSkeleton))
            })
        };

        private void Target(object obj)
        {
            if (obj is not Corpse c)
            {
                Caster.SendLocalizedMessage(1061084); // You cannot animate that.
            }
            else
            {
                Type type = null;

                if (c.Owner != null)
                {
                    type = c.Owner.GetType();
                }

                if (c.ItemID != 0x2006 || c.Animated || c.Channeled || type == typeof(PlayerMobile) || type == null || (c.Owner != null && c.Owner.Fame < 100) || c.Owner != null && (c.Owner is BaseCreature bc) && (bc.Summoned || bc.IsBonded))
                {
                    Caster.SendLocalizedMessage(1061085); // There's not enough life force there to animate.
                }
                else
                {
                    CreatureGroup group = FindGroup(type);

                    if (group != null)
                    {
                        if (group.m_Entries.Length == 0 || type == typeof(DemonKnight))
                        {
                            Caster.SendLocalizedMessage(1061086); // You cannot animate undead remains.
                        }
                        else if (CheckSequence())
                        {
                            Point3D p = c.GetWorldLocation();
                            Map map = c.Map;

                            if (map != null)
                            {
                                Effects.PlaySound(p, map, 0x1FB);
                                Effects.SendLocationParticles(EffectItem.Create(p, map, EffectItem.DefaultDuration), 0x3789, 1, 40, 0x3F, 3, 9907, 0);

                                Timer.DelayCall(TimeSpan.FromSeconds(2.0), SummonDelay_Callback, new object[] { Caster, c, p, map, group });
                            }
                        }
                    }
                }
            }

            FinishSequence();
        }

        private static readonly Dictionary<Mobile, List<DamageTimer>> _Table = new Dictionary<Mobile, List<DamageTimer>>();

        private static void UnRegister(DamageTimer timer)
        {
            Mobile master = timer.Master;

            if (_Table.TryGetValue(master, out List<DamageTimer> value))
            {
                if (timer.Running)
                {
                    timer.Stop();
                }

                value.Remove(timer);

                if (value.Count == 0)
                {
                    _Table.Remove(master);
                }
            }
        }

        public static void Register(Mobile master, BaseCreature summoned)
        {
            if (master == null)
            {
                return;
            }

            if (!_Table.TryGetValue(master, out List<DamageTimer> value))
            {
                value = new List<DamageTimer>();

                _Table[master] = value;
            }

            List<DamageTimer> list = value;

            list.Add(new DamageTimer(master, summoned));

            if (list.Count > 3)
            {
                Timer.DelayCall(TimeSpan.Zero, () => list[0].Summon.Kill());
            }
        }

        private static void SummonDelay_Callback(object state)
        {
            object[] states = (object[])state;

            Mobile caster = (Mobile)states[0];
            Corpse corpse = (Corpse)states[1];
            Point3D loc = (Point3D)states[2];
            Map map = (Map)states[3];
            CreatureGroup group = (CreatureGroup)states[4];

            if (corpse.Animated)
            {
                return;
            }

            Mobile owner = corpse.Owner;

            if (owner == null)
            {
                return;
            }

            double necromancy = caster.Skills[SkillName.Necromancy].Value;
            double spiritSpeak = caster.Skills[SkillName.SpiritSpeak].Value;

            int casterAbility = 0;

            casterAbility += (int)(necromancy * 30);
            casterAbility += (int)(spiritSpeak * 70);
            casterAbility /= 10;
            casterAbility *= 18;

            if (casterAbility > owner.Fame)
            {
                casterAbility = owner.Fame;
            }

            if (casterAbility < 0)
            {
                casterAbility = 0;
            }

            Type toSummon = null;
            SummonEntry[] entries = group.m_Entries;

            BaseCreature creature = caster as BaseCreature;

            if (creature?.AIObject is NecroMageAI)
            {
                toSummon = typeof(FleshGolem);
            }

            for (int i = 0; toSummon == null && i < entries.Length; ++i)
            {
                SummonEntry entry = entries[i];

                if (casterAbility < entry.m_Requirement)
                {
                    continue;
                }

                Type[] animates = entry.m_ToSummon;

                toSummon = animates[Utility.Random(animates.Length)];
            }

            if (toSummon == null)
            {
                return;
            }

            BaseCreature summoned = null;

            try
            {
                summoned = Activator.CreateInstance(toSummon) as BaseCreature;
            }
            catch (Exception e)
            {
                Diagnostics.ExceptionLogging.LogException(e);
            }

            if (summoned == null)
            {
                return;
            }

            BaseCreature bc = summoned;

            // to be sure
            bc.Tamable = false;

            bc.ControlSlots = bc is BaseMount ? 1 : 0;

            Effects.PlaySound(loc, map, bc.GetAngerSound());

            BaseCreature.Summon(summoned, false, caster, loc, 0x28, TimeSpan.FromDays(1.0));

            if (summoned is SkeletalDragon dragon)
            {
                Scale(dragon, 50); // lose 50% hp and strength
            }

            summoned.Fame = 0;
            summoned.Karma = -1500;

            summoned.MoveToWorld(loc, map);

            corpse.Hue = 1109;
            corpse.Animated = true;

            Register(caster, summoned);
        }

        public static void Scale(BaseCreature bc, int scalar)
        {
            int toScale;

            toScale = bc.RawStr;
            bc.RawStr = AOS.Scale(toScale, scalar);

            toScale = bc.HitsMaxSeed;

            if (toScale > 0)
            {
                bc.HitsMaxSeed = AOS.Scale(toScale, scalar);
            }

            bc.Hits = bc.Hits; // refresh hits
        }

        public class InternalTarget : Target
        {
            private readonly AnimateDeadSpell _Owner;

            public InternalTarget(AnimateDeadSpell owner)
                : base(10, false, TargetFlags.None)
            {
                _Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                _Owner.Target(o);
            }

            protected override void OnTargetFinish(Mobile from)
            {
                _Owner.FinishSequence();
            }
        }

        public class DamageTimer : Timer
        {
            public Mobile Master { get; }
            public BaseCreature Summon { get; }

            public DamageTimer(Mobile master, BaseCreature summon)
                : base(TimeSpan.FromMilliseconds(1650), TimeSpan.FromMilliseconds(1650))
            {
                Summon = summon;
                Master = master;
                Start();
            }

            protected override void OnTick()
            {
                if (Summon.Deleted)
                {
                    UnRegister(this);
                    Stop();
                }
                else
                {
                    if (Summon.Hits > 0)
                    {
                        Summon.Hits--;
                    }
                    else
                    {
                        Summon.Kill();
                        UnRegister(this);
                        Stop();
                    }
                }
            }
        }
    }
}
