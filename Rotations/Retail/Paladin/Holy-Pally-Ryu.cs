// V2.0 - Sunblood Trinket Support, CS Fix, Glimmer Fix, Maarad's Support and Targeting overhaul
// V2.1 - Focus Build Beta
// V2.2 - Focus Build Launch
using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace HyperElk.Core
{

    public class HolyPally : CombatRoutine
    {

        //Spell Strings
        private string DivineShield = "Divine Shield";
        private string DivineProtection = "Divine Protection";
        private string HolyShock = "Holy Shock";
        private string HolyLight = "Holy Light";
        private string FoL = "Flash of Light";
        private string WoG = "Word of Glory";
        private string BoS = "Blessing of Sacrifice";
        private string LoH = "Lay on Hands";
        private string BF = "Bestow Faith";
        private string LoD = "Light of Dawn";
        private string HoLI = "Holy Light /w Infusion of Light";
        private string FoLI = "Flash of Light /w Infusion of Light";
        private string BoV = "Beacon of Virtue";
        private string CrusaderStrike = "Crusader Strike";
        private string Judgment = "Judgment";
        private string AvengingWrath = "Avenging Wrath";
        private string HolyAvenger = "Holy Avenger";
        private string AuraMastery = "Aura Mastery";
        private string HammerofJustice = "Hammer of Justice";
        private string Cons = "Consecration";
        private string Seraphim = "Seraphim";
        private string HammerofWrath = "Hammer of Wrath";
        private string HolyPrism = "Holy Prism";
        private string LightsHammer = "Light's Hammer";
        private string Forbearance = "Forbearance";
        private string Infusion = "Infusion of Light";
        private string AvengingCrusader = "Avenging Crusader";
        private string Fleshcraft = "Fleshcraft";
        private string DivineToll = "Divine Toll";
        private string DivineTollHealing = "Divine Toll Healing";
        private string VanqusihersHammer = "Vanquisher's Hammer";
        private string AshenHallow = "Ashen Hallow";
        private string LoTM = "Light of the Martyr";
        private string LoTMH = "Light of the Martyr Health";
        private string LoTMM = "Light of the Martyr Moving";
        private string Player = "player";
        private string AoEDPS = "AOEDPS";
        private string AoEDPSRaid = "AOEDPS Raid";
        private string AoEDPSH = "AOEDPS Health";
        private string AoEDPSHRaid = "AOEDPS Health Raid";
        private string WOGMax = "Word of Glory Healing at Max HP";
        private string LODMax = "Light of Dawn Healing at Max HP";
        private string RuleofLaw = "Rule of Law";
        private string GlimmerofLight = "Glimmer of Light";
        private string ShockBarrier = "Shock Barrier";
        private string SoTR = "Shield of the Righteous";
        private string BoST = "Blessing of Sacrifice on Tank Only";
        private string BoL = "Beacon of Light";
        private string BoF = "Beacon of Faith";
        private string Trinket = "Trinket";
        private string HolyLightBeacon = "Holy Light on Beacon";
        private string FoLBeacon = "Flash of Light on Beacon";
        private string HolyLightIBeacon = "Holy Light /w Infusion on Beacon";
        private string FoLIBeacon = "Flash of Light /w Infusion on Beacon";
        private string LoHT = "Lay On Hands on Tank Only";
        private string Quake = "Quake";
        private string Cleanse = "Cleanse";
        private string WoGTank = "Word of Glory on Tank";
        private string DivinePurpose = "Divine Purpose";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";
        private string WoGOverLoD = "WoG Over LoD";
        private string WoGTank2 = "WoG Tank";
        private string HolyShockDPS = "Holy Shock DPS";

        private string CrusaderAura = "Crusader Aura";
        private string DevotionAura = "Devotion Aura";

        private string EatingBuff = "Food Buff";
        private string MageEatingBuff = "Mage Food Buff";

        private string DTPartyAoE = "Divine Toll Party AoE Number";
        private string DTRaidAoE = "Divine Toll Raid AoE Number";
        private string GlimmerPartyAoE = "Glimmer Party AoE Number";
        private string GlimmerRaidAoE = "Glimmer Raid AoE Number";
        private string LoDPartyAoE = "Light of Dawn Party AoE Number";
        private string LoDRaidAoE = "Light of Dawn Raid AoE Number";
        private string AshenHPartyAoE = "Ashen Hallow Party AoE Number";
        private string AshenHRaidAoE = "Ashen Hallow Raid AoE Number";
        private string LightsHammerPartyAoE = "Lights Hammer Party AoE Number";
        private string LightsHammerRaidAoE = "Lights Hammer Raid AoE Number";
        private string BoVPartyAoE = "Beacon of Virtue Party AoE Number";
        private string BoVRaidAoE = "Beacon of Virtue Raid AoE Number";
        private string HolyPrismPartyAoE = "Holy Prism Party AoE Number";
        private string HolyPrismRaidAoE = "Holy Prism Raid AoE Number";
        private string TrinketPartyAoE = "Trinket Party AoE Number";
        private string TrinketRaidAoE = "Trinket Raid AoE Number";
        private string AuraMasteryPartyAoE = "Aura Mastery Party AoE Number";
        private string AuraMasteryRaidAoE = "Aura Mastery Raid AoE Number";
        private string AvengingWrathPartyAoE = "Avenging Wrath Party AoE Number";
        private string AvengingWrathRaidAoE = "Avenging Wrath Raid AoE Number";
        private string AshenHallowOnlyDPS = "Ashen Hallow used for DPS Only";
        private string HolyAvengerPartyAoE = "Holy Avenger Party AoE Number";
        private string HolyAvengerRaidAoE = "Holy Avenger Raid AoE Number";

        private string LowestHPSwap = "Targeting to Lowest over all others";
        private string UntemperedDedication = "Untempered Dedication";
        private string MaraadsDyingBreath = "Maraad's Dying Breath";
        private string ChargedPhylactery = "Charged Phylactery";
        private string Sunblood = "Sunblood Amethyst";
        private string FO = "Focus";
        //Talents
        bool CrusadersMight => API.PlayerIsTalentSelected(1, 1);
        bool BestowFaith => API.PlayerIsTalentSelected(1, 2);
        bool LightsHammerT => API.PlayerIsTalentSelected(1, 3);
        bool Savedbythelight => API.PlayerIsTalentSelected(2, 1);
        bool JudgementofLight => API.PlayerIsTalentSelected(2, 2);
        bool HolyPrismT => API.PlayerIsTalentSelected(2, 3);
        bool FistofJustice => API.PlayerIsTalentSelected(3, 1);
        bool Repentance => API.PlayerIsTalentSelected(3, 2);
        bool BlindingLight => API.PlayerIsTalentSelected(3, 3);
        bool UnbreakableSpirit => API.PlayerIsTalentSelected(4, 1);
        bool Calvalier => API.PlayerIsTalentSelected(4, 2);
        bool RuleofLawTalent => API.PlayerIsTalentSelected(4, 3);
        bool DivinePurposeTalent => API.PlayerIsTalentSelected(5, 1);
        bool HolyAvengerT => API.PlayerIsTalentSelected(5, 2);
        bool SeraphimT => API.PlayerIsTalentSelected(5, 3);
        bool SancifiedWrath => API.PlayerIsTalentSelected(6, 1);
        bool AvengingCrusaderT => API.PlayerIsTalentSelected(6, 2);
        bool Awakening => API.PlayerIsTalentSelected(6, 3);
        bool GlimmerofLightTalent => API.PlayerIsTalentSelected(7, 1);
        bool BeaconofFaith => API.PlayerIsTalentSelected(7, 2);
        bool BeaconofVirtue => API.PlayerIsTalentSelected(7, 3);


        //Stopwatchs / Int's / Strings
        private static readonly Stopwatch SwapWatch = new Stopwatch();
        private static readonly Stopwatch AshenWatch = new Stopwatch();
        private static readonly Stopwatch ConsWatch = new Stopwatch();


        int[] BossCastList = new int[] { 322759, 320141, 320230, 319733, 319733, 323552, 328791, 325360 };
        private int Level => API.PlayerLevel;
        int PlayerHealth => API.TargetHealthPercent;
        string[] units = {"none", "player", "party1", "party2", "party3", "party4" };
        string[] raidunits = {"none", "raid1", "raid2", "raid3", "raid4", "raid5", "raid6", "raid7", "raid8", "raid9", "raid8", "raid9", "raid10", "raid11", "raid12", "raid13", "raid14", "raid16", "raid17", "raid18", "raid19", "raid20", "raid21", "raid22", "raid23", "raid24", "raid25", "raid26", "raid27", "raid28", "raid29", "raid30", "raid31", "raid32", "raid33", "raid34", "raid35", "raid36", "raid37", "raid38", "raid39", "raid40" };
        string[] DispellList = { "Chilled", "Frozen Binds", "Clinging Darkness", "Rasping Scream", "Heaving Retch", "Goresplatter", "Slime Injection", "Gripping Infection", "Debilitating Plague", "Burning Strain", "Blightbeak", "Corroded Claws", "Wasting Blight", "Hurl Spores", "Corrosive Gunk", "Cytotoxic Slash", "Venompiercer", "Wretched Phlegm", "Repulsive Visage", "Soul Split", "Anima Injection", "Bewildering Pollen", "Bramblethorn Entanglement", "Debilitating Poison", "Sinlight Visions", "Siphon Life", "Turn to Stone", "Stony Veins", "Cosmic Artifice", "Wailing Grief", "Shadow Word:  Pain", "Anguished Cries", "Wrack Soul", "Dark Lance", "Insidious Venom", "Charged Anima", "Lost Confidence", "Burden of Knowledge", "Internal Strife", "Forced Confession", "Insidious Venom 2", "Soul Corruption", "Genetic Alteration", "Withering Blight", "Decaying Blight", "Burst" };
        public string[] LegendaryList = new string[] { "none", "Shock Barrier", "Shadowbreaker, Dawn of the Sun" };
        int[] RaidDebuffDispel = new int[]
        {
            347286, // Unshakeable Dread — Spell ID 347286. Players with this debuff are feared. Make sure you dispel them instantly. Prioritize melee players since they are more likely to run into another fear.
            350713, // Slothful Corruption — Spell ID 350713. This is a magic debuff that needs to be dispelled instantly since it not only does damage, but also reduces the Haste and movement speed of affected players.
            350542, // Fragments of Destiny — Spell ID 350542. This is a stacking debuff that should not be very dangerous if handled properly. However, you should still keep track of stacks just in case.
            357298, // Frozen Binds — Spell ID 357298; magic debuff that roots players in place. You can shift out of it to save some Mana.
            351117, // Crushing Dread — Spell ID 351117; magic debuff that you will have to dispel in Phase 2.
        };
        static List<int> MagicDebuffList = new List<int>
        {
            // De Other Side

         //   331379, // Lubricate
            332605, // Hex
            332707, // Shadow Word : Pain
            325725, // Comsic Artifce 

            // Halls of Atonement

            325701, // Siphon Life
            322977, // Sinlight Visions

            // Mists of Tirna Scithe

            324859, // Bramblethorn Entanglment
            328756, // Repulsive Visage
            325224, // Anima Injection

            // Plaguefall

            328180, // Gripping Infection
            329110, // Slime Injection

            // Sanguine Depths

            321038, // Wrack Soul
            335305, // Barbed Shackles

            // Spires of Ascension

            328331, // Forced Confession
            314411, // Lingering Doubt
            317963, // Burden of Knowledge
            327648, // Internal Strife
            327481, // Dark Lance
            322818, // Lost Confidence
            323636, // Insidious Venom
            317661, // Insidious Venom
            // The Necrotic Wake

            324293, // Rasping Scream
            267618, // Drain Fluids
         // 320788, // Frozen Binds
            323347, // Clinging Darkness
          //  198407, // Necrotic Bolt
        //    321807, // Boneflay

            // Theater of Pain

            333708, // Soul Corruption
            319626, // Phantasmal Parasite

            // Tazavesh

            357029, // Magic Hyperlight Bomb
            355641, // Magic Scintillate
            357281, // Magic Energy Slash
            349954, // Magic Purification Protocol
            346844, // Magic Alchemical Residue
            353835, // Magic Suppression
            358131, // Magic Lightning Nova
            356031, // Magic Stasis Beam
            355915, // Magic Glyph of Restraint
            356324, // Magic Empowered Glyph of Restraint
            
            // Affix

            355732, // Melt Soul
        };
        static List<int> DiseaseDebuffList = new List<int>
        {
            // PlagueFall

            328501, // Plague Bomb
            328986, // Violent Detonation
            320512, // Corroded Claws
            324652, // Debilitating Plague
            322232, // Infections Rain
            319070, // Corrosve Gunk
            319898, // Vile Spit

            // Necrotic Wake

            338353, // Goresplatter
            335164, // Disgusting Guts
            320596, // Heaving Retch

            // Theater of Pain

            330700, // Decaying Blight
        };
        static List<int> PoisonDebuffList = new List<int>

        {
            // Mists of Tirna Scithe

            
            326092, // Debiliating Poison
            340288, // Triple Bite

            // Plaguefall

            334926, // Wretched Phelhm
            327515, // Fen Stringer

        };

        List<int> AllDungeonDispel = MagicDebuffList.Concat(DiseaseDebuffList).Concat(PoisonDebuffList).ToList();

        int[] FrozenBinds = new int[]
        {
                      320788, // Frozen Binds

        };
                int[] Bursting = new int[]
        {
                    240443, // Burst

        };
        private string FrozenBind = "320788";
        private string Burst = "240443";
        int[] InteruptList = new int[]
        {
        //    Sanguine Depths
    319654,  //  hungering-drain
    321038,  //wrack-soul
    322433,  // Stoneskin
    321105,  // Sap Lifeblood
    334653,  //Engorge
    335305,  //Barbed Shackles
    326952, // Fiery Cantrip

//DoS
    333227, // Undying rage
    332329,  //  Devoted Sacrifice
    332706,  // Renew
    332666,  //  Heal
    332612,  // Healing Wave
    332084,  // Self Cleaning Cycle
    321764,  // Bark Armor
    333875,  //  Deaths Embrace
    334076,  // Shadowcore
    332605,  // Hex
    332196,  // Discharge
    332234,  // Essential Oil
    320008,  // Frostbolt

//Plaugefall
    319070,  // Corrosive Gunk
    329239,  // Creepy Crawlers
    328094,  // Pestilence Bolt
    321999,  // Viral Globs
    322358,  // Burning Strain
    328338,  // Call Venomfang
    328651,  // Call Venomfang

// Halls of Atonement
    325700,  // collect-sins
    325876,  // curse-of-obliteration
    //--338003,  // wicked-bolt -- (NOT SO IMPORTANT BUT I GUESS CAN BE ADDED IN FOR FUTURE)
    326607, // turn-to-stone
    328322,  // villainous-bolt
    323538, // bolt-of-power
    323552,  // volley-of-power
    325701,  // Siphon-Life

// Mist of Tirna
    322938,  //  harvest-essence
    324914,  // nourish-the-forest
    324776,  // bramblethorn-coat
    321828,  // patty-cake
    326046,  // stimulate-resistance
    337235,  // parasitic-pacification
    322450,  // consumption
    340544, // stimulate-regeneration
    337251,  //  parasitic-incapacitation
    337253, // Parasitic Domination

// SoA
    327413,  // rebellious-fist
    317936,  // forsworn-doctrine
    317963,  // burden-of-knowledge
    328295,  // greater-mending
    328137,  // dark-pulse
    328331,  // forced-confession
    327648,  // Internal Strife

// ToP
    341902,  // unholy-fervor
    341969, // withering-discharge
    342139,  // battle-trance
    330562,  // demoralizing-shout
    330868,  // Necrotic Bolt Volley
    330784,  // Necrotic Bolt
    330810,  // Bind Soul
    333231,  // Searing Death
    341977,  // Meat Shield
    330586,  // Devour Flesh
    342675,  // Bone Spear
    324589,  // Death Bolt

//  NW
    334748,   // drain-fluids
    320462,  // necrotic-bolt
    324293,  // rasping-scream
    320170,  // necrotic-bolt
    338353,  // goresplatter
    333623,  // frostbolt Volley
    328667,  // frostbolt volley
    323190,  // Meat Shield
    335143,  // bonemend
        };
        private string UseLeg => LegendaryList[CombatRoutine.GetPropertyInt("Legendary")];

        //Bools and AoE Checks
        private static bool TargetHasDispellAble(string debuff)
        {
            return API.TargetHasDebuff(debuff, false, true);
        }
        private static bool MouseouverHasDispellAble(string debuff)
        {
            return API.MouseoverHasDebuff(debuff, false, true);
        }
        private static bool UnitHasDispellAble(string debuff, string unit)
        {
            return API.UnitHasDebuff(debuff, unit, false, true);
        }
        private static bool UnitHasBuff(string buff, string unit)
        {
            return API.UnitHasBuff(buff, unit, true, true);
        }
        private static bool PlayerHasDebuff(string buff)
        {
            return API.PlayerHasDebuff(buff, false, false);
        }
        private static bool TargetHasBuff(string buff)
        {
            return API.TargetHasBuff(buff, true, true);
        }
        private static bool MouseoverHasBuff(string buff)
        {
            return API.MouseoverHasBuff(buff, true, false);
        }
        private static bool TargetHasDebuff(string buff)
        {
            return API.TargetHasDebuff(buff, false, true);
        }
        private static bool FocusHasDebuff(string buff)
        {
            return API.FocusHasDebuff(buff, false, true);
        }
        private static bool MouseoverHasDebuff(string buff)
        {
            return API.MouseoverHasDebuff(buff, false, false);
        }
        private static bool UnitHasDebuff(string buff, string unit)
        {
            return API.UnitHasDebuff(buff, unit, false, true);
        }
        private static bool TargethasDebuff(string debuff)
        {
            return API.TargetHasDebuff(debuff, false, true);
        }
        private static string CurrentFrozenbindTarget(string[] units)
        {
            string lowest = "none";
            foreach (string unit in units)
            {
                if (API.UnitHasDebuff("Frozen Binds", unit, false) && API.UnitRange(unit) < 41 && API.UnitHealthPercent(unit) > 0)
                {
                    lowest = unit;
                }
            }
            return lowest;           
        }

       public string LowestInternalWithoutGlimmer(string[] units)
        {
            string lowest = "none";
            int health = 100;
            foreach (string unit in units)
            {
                if (API.UnitRange(unit) <= 40 &&(!API.UnitHasBuff("Glimmer of Light", unit, true) || API.UnitBuffTimeRemaining("Glimmer of Light", unit, true) <= 500 || API.UnitHealthPercent(unit) <= 20) && API.UnitHealthPercent(unit) <= health && API.UnitHealthPercent(unit) > 0)
                {
                    lowest = unit;
                    health = API.UnitHealthPercent(unit);
                }
            }
            return lowest;
        }
        private static string LowestInternalWithoutGlimmer(string[] units, out string lowestGlimmerTank)
        {
            string lowest = "none";
            lowestGlimmerTank = "none";
            int health = 100;
            int healthTank = 100;
            foreach (string unit in units)
            {
                if (lowestGlimmerTank == "none" && API.UnitRoleSpec(unit) == API.TankRole && API.UnitHealthPercent(unit) > 0 && (!API.UnitHasBuff("Glimmer of Light", unit) || API.UnitBuffTimeRemaining("Glimmer of Light", unit) <= 150))
                {
                    lowestGlimmerTank = unit;
                    healthTank = API.UnitHealthPercent(unit);
                }
                if (API.UnitRange(unit) <= 40 && API.UnitHealthPercent(unit) > 0 && API.UnitHealthPercent(unit) != 100 && (!API.UnitHasBuff("Glimmer of Light", unit) || API.UnitBuffTimeRemaining("Glimmer of Light", unit) <= 150) && API.UnitHealthPercent(unit) != 100)
                {
                    if (API.UnitHealthPercent(unit) < health)
                    {
                        lowest = unit;
                        health = API.UnitHealthPercent(unit);
                    }
                    if (API.UnitRoleSpec(unit) == API.TankRole && API.UnitHealthPercent(unit) < healthTank)
                    {
                        lowestGlimmerTank = unit;
                        healthTank = API.UnitHealthPercent(unit);
                    }
                }
            }
            return lowest;
        }
      public  string LowestDispePartylUnit(string[] units)
        {
            string lowest = "none";
            int health = 101;
            foreach (string unit in units)
            {
                if (API.UnitHealthPercent(unit) < health && API.UnitRange(unit) <= 40 && API.UnitHealthPercent(unit) > 0 && (API.UnitHasDebuffDispel(AllDungeonDispel, unit) || API.UnitHasDebuffDispel(Bursting, unit) && API.UnitDebuffStacks(Burst, unit) >= 2 || API.UnitHasDebuffDispel(FrozenBinds, unit) && API.UnitDebuffRemainingTime(FrozenBind, unit) <= 1000))
                {
                    lowest = unit;
                    health = API.UnitHealthPercent(unit);
                }
            }
            return lowest;
        }
        public string LowestDispeRaidlUnit(string[] units)
        {
            string lowest = "none";
            int health = 101;
            foreach (string unit in units)
            {
                if (API.UnitHealthPercent(unit) < health && API.UnitRange(unit) <= 40 && API.UnitHealthPercent(unit) > 0 && API.UnitHasDebuffDispel(RaidDebuffDispel, unit))
                {
                    lowest = unit;
                    health = API.UnitHealthPercent(unit);
                }
            }
            return lowest;
        }
        public string LowestDispelUnit() => API.PlayerIsInRaid ? LowestDispeRaidlUnit(API.raidunits) : LowestDispePartylUnit(API.partyunits);
        public string LowestFrozenBind() => CurrentFrozenbindTarget(units);   
        public string LowestGlimmerUnit() => API.PlayerIsInRaid ? LowestInternalWithoutGlimmer(API.raidunits) : LowestInternalWithoutGlimmer(API.partyunits);
        private int BuffRaidTracking(string buff) => API.raidunits.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffTimeRemaining(buff, p) > 200);
        private int BuffPartyTracking(string buff) => API.partyunits.Count(p => API.UnitHasBuff(buff, p) && API.UnitBuffTimeRemaining(buff, p) > 200);
        private int BuffTracking(string buff) => API.PlayerIsInRaid ? BuffRaidTracking(buff) : BuffPartyTracking(buff);
        private bool QuakingHelper => CombatRoutine.GetPropertyBool("QuakingHelper");
        private bool DTRange => API.PlayerIsInRaid ? API.UnitRangeCountRaid(30) >= DTAoERaidNumber : API.UnitRangeCountParty(30) >= DTAoEPartyNumber;
        private bool LoDRangeRaidCheck => UseLeg == "Shadowbreaker, Dawn of the Sun" ? API.UnitRangeCountRaid(40) >= LoDAoERaidNumber : API.UnitRangeCountRaid(15) >= LoDAoERaidNumber;
        private bool LoDRangePartyCheck => UseLeg == "Shadowbreaker, Dawn of the Sun" ? API.UnitRangeCountParty(40) >= LoDAoEPartyNumber : API.UnitRangeCountParty(15) >= LoDAoEPartyNumber;

        private bool TrinketAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(TrinketLifePercent) >= TrinketAoERaidNumber : API.UnitBelowHealthPercentParty(TrinketLifePercent) >= TrinketAoEPartyNumber;
        private bool GlimmerTracking => API.PlayerIsInRaid ? BuffRaidTracking(GlimmerofLight) < GlimmerAoERaidNumber : BuffPartyTracking(GlimmerofLight) < GlimmerAoEPartyNumber;
        private bool BoLTracking => API.PlayerIsInRaid ? API.unitBuffCountRaid(BoL) < 1 : API.UnitBuffCountParty(BoL) < 1;
        private bool BoFTracking => API.PlayerIsInRaid ? API.unitBuffCountRaid(BoF) < 1 : API.UnitBuffCountParty(BoF) < 1;
        private bool DPSHealthCheck => API.PlayerIsInRaid ? API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber : API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber;
        private bool DPSMouseOver => API.MouseoverHealthPercent <= AoEDPSHLifePercent;
        private bool LoDAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(LoDLifePercent) >= LoDAoERaidNumber : API.UnitBelowHealthPercentParty(LoDLifePercent) >= LoDAoEPartyNumber;
        private bool LoDMaxAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(LoDMaxLifePercent) >= LoDAoERaidNumber : API.UnitBelowHealthPercentParty(LoDMaxLifePercent) >= LoDAoEPartyNumber;
        private bool BoVAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(BoVLifePercent) >= BoVAoERaidNumber : API.UnitBelowHealthPercentParty(BoVLifePercent) >= BoVAoEPartyNumber;
        private bool HPAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(HPLifePercent) >= HolyPrismAoERaidNumber : API.UnitBelowHealthPercentParty(HPLifePercent) > +HolyPrismAoEPartyNumber;
        private bool DTAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(DTLifePercent) >= DTAoERaidNumber : API.UnitBelowHealthPercentParty(DTLifePercent) >= DTAoEPartyNumber;
        private bool AHAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(AHLifePercent) >= AshenHAoERaidNumber : API.UnitBelowHealthPercentParty(AHLifePercent) >= AshenHAoEPartyNumber;
        private bool AMAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(AMLifePercent) >= AuraMasteryAoERaidNumber : API.UnitBelowHealthPercentParty(AMLifePercent) >= AuraMasteryAoEPartyNumber;
        private bool AVAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(AvengingWrathLifePrecent) >= AvengingWrathAoERaidNumber : API.UnitBelowHealthPercentParty(AvengingWrathLifePrecent) >= AvengingWrathAoEPartyNumber;
        private bool HVAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(HolyAvengerLifePrecent) >= HolyAvengerAoERaidNumber : API.UnitBelowHealthPercentParty(HolyAvengerLifePrecent) >= HolyAvengerAoEPartyNumber;
        private bool LHAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(LightsHammerLifePercent) >= LightsHammerAoERaidNumber : API.UnitBelowHealthPercentParty(LightsHammerLifePercent) >= LightsHammerAoEPartyNumber;
        private bool InRange => API.PlayerHasBuff(RuleofLaw) ? API.TargetRange <= 60 : API.TargetRange <= 40;

        private bool LoTMCheck => API.CanCast(LoTM) && InRange && !API.PlayerCanAttackTarget && API.TargetIsUnit() != Player && (API.PlayerIsMoving && (API.TargetHealthPercent <= LoTMMovingLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= LoTMMovingLifePercent) || API.TargetHealthPercent <= LoTMLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= LoTMLifePercent && API.PlayerHealthPercent >= LoTMHealthPercent) && API.TargetHealthPercent > 0;
        private bool LoTMFOCheck => API.CanCast(LoTM) && InFORange && (API.PlayerIsMoving && (API.FocusHealthPercent <= LoTMMovingLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= LoTMMovingLifePercent) || API.FocusHealthPercent <= LoTMLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= LoTMLifePercent && API.PlayerHealthPercent >= LoTMHealthPercent) && API.TargetHealthPercent > 0;
        private bool LoTMLegoCheck => API.CanCast(LoTM) && IsMaarads && InRange && !API.PlayerCanAttackTarget && API.TargetIsUnit() != Player && (API.PlayerIsMoving && (API.TargetHealthPercent <= LoTMMovingLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= LoTMMovingLifePercent) || API.TargetHealthPercent <= LoTMLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= LoTMLifePercent && API.PlayerHealthPercent >= LoTMHealthPercent) && (API.PlayerHasBuff(MaraadsDyingBreath) && (!API.TargetHasBuff(BoL, true) || !API.TargetHasBuff(BoF, true)) || !API.PlayerHasBuff(MaraadsDyingBreath)) && API.TargetHealthPercent > 0;
        private bool LoTMLegoFOCheck => API.CanCast(LoTM) && IsMaarads && InFORange && (API.PlayerIsMoving && (API.FocusHealthPercent <= LoTMMovingLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= LoTMMovingLifePercent) || API.FocusHealthPercent <= LoTMLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= LoTMLifePercent && API.PlayerHealthPercent >= LoTMHealthPercent) && (API.PlayerHasBuff(MaraadsDyingBreath) && (!API.FocusHasBuff(BoL, true) || !API.FocusHasBuff(BoF, true)) || !API.PlayerHasBuff(MaraadsDyingBreath)) && API.TargetHealthPercent > 0;
        private bool HolyShockCheck => API.CanCast(HolyShock) && InRange && (API.TargetHealthPercent <= HolyShockLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 || SpreadGlimmer && (!API.TargetHasBuff(GlimmerofLight, true) || API.TargetBuffTimeRemaining(GlimmerofLight, true) <= 500) && (API.PlayerIsInRaid && BuffRaidTracking(GlimmerofLight) < 9 || API.PlayerIsInGroup && !API.PlayerIsInRaid && BuffPartyTracking(GlimmerofLight) < 6)) && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && API.PlayerCurrentHolyPower < 5 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyShockFOCheck => API.CanCast(HolyShock) && InFORange && (API.FocusHealthPercent <= HolyShockLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 || SpreadGlimmer && (!API.FocusHasBuff(GlimmerofLight, true) || API.FocusBuffTimeRemaining(GlimmerofLight, true) <= 500) && (API.PlayerIsInRaid && BuffRaidTracking(GlimmerofLight) < 9 || API.PlayerIsInGroup && !API.PlayerIsInRaid && BuffPartyTracking(GlimmerofLight) < 6)) && API.FocusHealthPercent > 0 && API.PlayerCurrentHolyPower < 5 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyLightCheck => API.CanCast(HolyLight) && InRange && ((API.TargetHasBuff(BoF, true) || API.TargetHasBuff(BoL, true)) && API.TargetHealthPercent <= HolyLightBeaconLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= HolyLightBeaconLifePercent) || (API.TargetHealthPercent <= HolyLightLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= HolyLightLifePercent) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool HolyLightFOCheck => API.CanCast(HolyLight) && InFORange && ((API.FocusHasBuff(BoF, true) || API.FocusHasBuff(BoL, true)) && API.FocusHealthPercent <= HolyLightBeaconLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= HolyLightBeaconLifePercent) || (API.FocusHealthPercent <= HolyLightLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= HolyLightLifePercent) && API.FocusHealthPercent > 0 && !API.PlayerIsMoving;
        private bool HolyLightInfusionCheck => API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && InRange && ((API.TargetHasBuff(BoF, true) || API.TargetHasBuff(BoL, true)) && (API.TargetHealthPercent <= HolyLightBeaconILifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= HolyLightBeaconILifePercent) || API.TargetHealthPercent <= HoLILifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= HoLILifePercent) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool HolyLightInfusionFOCheck => API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && InFORange && ((API.FocusHasBuff(BoF, true) || API.FocusHasBuff(BoL, true)) && (API.FocusHealthPercent <= HolyLightBeaconILifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= HolyLightBeaconILifePercent) || API.FocusHealthPercent <= HoLILifePercent || TargetHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= HoLILifePercent) && API.FocusHealthPercent > 0 && !API.PlayerIsMoving;
        private bool FlashofLightCheck => API.CanCast(FoL) && InRange && ((API.TargetHasBuff(BoF, true) || API.TargetHasBuff(BoL, true)) && (API.TargetHealthPercent <= FoLBeaconLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= FoLBeaconLifePercent) || (API.TargetHealthPercent <= FoLLifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= FoLLifePercent)) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool FlashofLightFOCheck => API.CanCast(FoL) && InFORange && ((API.TargetHasBuff(BoF, true) || API.TargetHasBuff(BoL, true)) && (API.FocusHealthPercent <= FoLBeaconLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= FoLBeaconLifePercent) || API.FocusHealthPercent <= FoLLifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= FoLLifePercent) && API.FocusHealthPercent > 0 && !API.PlayerIsMoving;
        private bool FlashofLightInfusionCheck => API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && InRange && ((API.TargetHasBuff(BoF) || API.TargetHasBuff(BoL)) && (API.TargetHealthPercent <= FoLBeaconILifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= FoLBeaconILifePercent) || (API.TargetHealthPercent <= FolILifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= FolILifePercent)) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool FlashofLightInfusionFOCheck => API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && InFORange && ((API.TargetHasBuff(BoF, true) || API.TargetHasBuff(BoL, true)) && (API.FocusHealthPercent <= FoLBeaconILifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= FoLBeaconILifePercent) || (API.FocusHealthPercent <= FolILifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= FolILifePercent)) && API.FocusHealthPercent > 0 && !API.PlayerIsMoving;
        private bool WoGCheck => API.CanCast(WoG) && InRange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.TargetHealthPercent <= WoGLifePercent || API.TargetHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5 || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 || API.TargetHealthPercent <= WoGLifePercent) && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGFOCheck => API.CanCast(WoG) && InFORange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.FocusHealthPercent <= WoGLifePercent || API.FocusHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5 || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 || API.FocusHealthPercent <= WoGLifePercent) && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGOverLoDCheck => API.CanCast(WoG) && InRange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.TargetHealthPercent <= WoGOverLoDifePercent || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= WoGOverLoDifePercent) && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGOverLoDFOCheck => API.CanCast(WoG) && InFORange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.FocusHealthPercent <= WoGOverLoDifePercent || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= WoGOverLoDifePercent) && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGTankCheck => WoGTanking && API.CanCast(WoG) && API.TargetRoleSpec == API.TankRole && InRange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.TargetHealthPercent <= WoGTankifePercent || API.TargetHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5 || TargetHasDebuff("Veil of Darkness") && API.TargetHealthPercent < 100 && API.TargetHealthPercent <= WoGTankifePercent) && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGTankFOCheck => WoGTanking && API.CanCast(WoG) && API.FocusRoleSpec == API.TankRole && InFORange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.FocusHealthPercent <= WoGTankifePercent || API.FocusHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5 || FocusHasDebuff("Veil of Darkness") && API.FocusHealthPercent < 100 && API.FocusHealthPercent <= WoGTankifePercent) && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BFCheck => API.CanCast(BF) && BestowFaith && InRange && API.TargetHealthPercent <= BFLifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BFFOCheck => API.CanCast(BF) && BestowFaith && InFORange && API.FocusHealthPercent <= BFLifePercent && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);

        private bool LoDCheck => IsAOE && API.CanCast(LoD) && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (LoDAoE || LoDMaxAoE && API.PlayerCurrentHolyPower == 5) && API.TargetHealthPercent > 0 && (API.PlayerCanAttackTarget || !API.PlayerCanAttackTarget) && (API.PlayerIsMoving || !API.PlayerIsMoving) && (!API.PlayerIsInRaid && LoDRangePartyCheck || API.PlayerIsInRaid && LoDRangeRaidCheck);
        private bool BoVCheck => API.CanCast(BoV) && BeaconofVirtue && InRange && BoVAoE && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BoVFOCheck => API.CanCast(BoV) && BeaconofVirtue && InFORange && BoVAoE && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);

        private bool DTCheck => API.CanCast(DivineToll) && DTRange && API.TargetRange < 31 && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" && DTAoE || UseCovenant == "on AOE" && DTAoE) && NotChanneling && !API.PlayerCanAttackTarget && API.TargetHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool DTFOCheck => API.CanCast(DivineToll) && DTRange && API.FocusRange < 31 && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" && DTAoE || UseCovenant == "on AOE" && DTAoE) && NotChanneling && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyPrismCheck => API.CanCast(HolyPrism) && HolyPrismT && InRange && HPAoE && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyPrismFOCheck => API.CanCast(HolyPrism) && HolyPrismT && InFORange && HPAoE && API.FocusHealthPercent > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool LoHCheck => API.CanCast(LoH) && InRange && API.TargetHealthPercent <= LoHLifePercent && API.TargetHealthPercent > 0 && (LoHTank && API.TargetRoleSpec == API.TankRole || !LoHTank) && !API.TargetHasDebuff(Forbearance, false, false) && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetIsIncombat;
        private bool LoHFOCheck => API.CanCast(LoH) && InFORange && API.FocusHealthPercent <= LoHLifePercent && API.FocusHealthPercent > 0 && (LoHTank && API.FocusRoleSpec == API.TankRole || !LoHTank) && !API.FocusHasBuff(Forbearance, false, false) && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.FocusIsIncombat;
        private bool BoSCheck => API.CanCast(BoS) && InRange && API.TargetHealthPercent <= BoSLifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && API.TargetIsUnit() != Player && (BoSTank && API.TargetRoleSpec == API.TankRole || !BoSTank) && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetIsIncombat;
        private bool BoSFOCheck => API.CanCast(BoS) && InFORange && API.FocusHealthPercent <= BoSLifePercent && API.FocusHealthPercent > 0 && (BoSTank && API.FocusRoleSpec == API.TankRole || !BoSTank) && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.FocusIsIncombat;
        private bool AuraMasteryCheck => API.CanCast(AuraMastery) && AMAoE && API.TargetHealthPercent > 0 && (API.PlayerCanAttackTarget || !API.PlayerCanAttackTarget) && (API.PlayerIsMoving || !API.PlayerIsMoving);

        private bool IsAutoSwap => API.ToggleIsEnabled("Auto Target");
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        public bool IsSunblood => CombatRoutine.GetPropertyBool(Sunblood);

        private bool InMoRange => API.PlayerHasBuff(RuleofLaw) ? API.MouseoverRange <= 60 : API.MouseoverRange <= 40;
        private bool IsMelee => API.TargetRange < 5;

        private bool IsMoMelee => API.MouseoverRange < 5;
        private bool NotChanneling => !API.PlayerIsChanneling;
        //private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        private bool IsDPS => API.ToggleIsEnabled("DPS Auto Target");
        private bool DTHealing => CombatRoutine.GetPropertyBool(DivineTollHealing);
        private bool HSDPS => CombatRoutine.GetPropertyBool(HolyShockDPS);
        private bool BoSTank => CombatRoutine.GetPropertyBool(BoST);
        private bool LoHTank => CombatRoutine.GetPropertyBool(LoHT);
        private bool IsOOC => API.ToggleIsEnabled("OOC");
        private bool AutoAuraSwitch => CombatRoutine.GetPropertyBool("Aura Switch");
        private bool IsDispell => API.ToggleIsEnabled("Dispel");
        private bool IsDPSOnly => API.ToggleIsEnabled("DPS Only");
        private bool IsNotEating => (!API.PlayerHasBuff(MageEatingBuff) || !API.PlayerHasBuff(EatingBuff));

        bool IsTrinkets1 => (UseTrinket1 == "With Cooldowns" && IsCooldowns && !IsSunblood || UseTrinket1 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent && !API.PlayerCanAttackTarget && !IsSunblood || UseTrinket1 == "on AOE" && TrinketAoE && !IsSunblood || (TrinketAoE || UseTrinket1 == "On Cooldown") && API.PlayerCanAttackTarget && IsSunblood);
        bool IsTrinkets2 => (UseTrinket2 == "With Cooldowns" && IsCooldowns || UseTrinket2 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent && !API.PlayerCanAttackTarget || UseTrinket2 == "on AOE" && TrinketAoE);

        //Settings Percents
        private int DivineShieldLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineShield)];
        private int DivineProtectionLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineProtection)];
        private int HolyShockLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyShock)];
        private int LightsHammerLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LightsHammer)];
        private int HolyLightLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLight)];
        private int AvengingWrathLifePrecent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AvengingWrath)];
        private int HolyAvengerLifePrecent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyAvenger)];

        private int HolyLightBeaconLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLightBeacon)];
        private int HolyLightBeaconILifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLightIBeacon)];
        private int FoLLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoL)];
        private int FoLBeaconLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoLBeacon)];
        private int FoLBeaconILifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoLIBeacon)];
        private int WoGLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(WoG)];
        private int WoGMaxLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(WOGMax)];
        private int WoGOverLoDifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(WoGOverLoD)];
        private int WoGTankifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(WoGTank2)];


        private int BoSLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BoS)];
        private int FolILifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoLI)];
        private int HoLILifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HoLI)];
        private int LoHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoH)];
        private int BFLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BF)];
        private int LoDLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoD)];
        private int LoDMaxLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LODMax)];
        private int BoVLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BoV)];
        private int HPLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyPrism)];
        private int LowestHealthSwapPercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LowestHPSwap)];
        private int DTLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineToll)];
        private int AHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AshenHallow)];
        private int LoTMHealthPercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoTMH)];
        private int AMLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AuraMastery)];
        private int LoTMLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoTM)];
        private int LoTMMovingLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoTMM)];
        private int AoEDPSHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AoEDPSH)];
        private int AoEDPSHRaidLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AoEDPSHRaid)];
        private int FleshcraftPercentProc => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Fleshcraft)];
        //   private int AoENumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AoE)];
        //   private int AoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AoERaid)];
        private int AoEDPSNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AoEDPS)];
        private int AoEDPSRaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AoEDPSRaid)];
        private int PhialofSerenityLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private int TrinketLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Trinket)];
     //   private int TankHealth => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Tank Health")];
        private int UnitHealth => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Other Members Health")];
        private int PlayerHP => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Player Health")];
        private int DTAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(DTPartyAoE)];
        private int DTAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(DTRaidAoE)];
        private int GlimmerAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(GlimmerPartyAoE)];
        private int GlimmerAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(GlimmerRaidAoE)];

        private int AshenHAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AshenHPartyAoE)];
        private int AshenHAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AshenHRaidAoE)];
        private int HolyPrismAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(HolyPrismPartyAoE)];
        private int HolyPrismAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(HolyPrismRaidAoE)];
        private int BoVAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(BoVPartyAoE)];
        private int BoVAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(BoVRaidAoE)];
        private int LightsHammerAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(LightsHammerPartyAoE)];
        private int LightsHammerAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(LightsHammerRaidAoE)];
        private int LoDAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(LoDPartyAoE)];
        private int LoDAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(LoDRaidAoE)];
        private int TrinketAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(TrinketPartyAoE)];
        private int TrinketAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(TrinketRaidAoE)];
        private int AuraMasteryAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AuraMasteryPartyAoE)];
        private int AuraMasteryAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AuraMasteryRaidAoE)];
        private int AvengingWrathAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AvengingWrathPartyAoE)];
        private int AvengingWrathAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AvengingWrathRaidAoE)];
        private int HolyAvengerAoEPartyNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(HolyAvengerPartyAoE)];
        private int HolyAvengerAoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(HolyAvengerRaidAoE)];
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Use Covenant")];
        private string UseAV => CDUsage[CombatRoutine.GetPropertyInt("Avenging Wrath Usage")];
        private string UseHV => CDUsage[CombatRoutine.GetPropertyInt("Holy Avenger Usage")];

        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        //Quaking
        private bool Quaking => (API.PlayerIsCasting(false) || API.PlayerIsChanneling) && API.PlayerDebuffRemainingTime(Quake) < 110 && PlayerHasDebuff(Quake);
        private bool SaveQuake => (PlayerHasDebuff(Quake) && API.PlayerDebuffRemainingTime(Quake) > 200 && QuakingHelper || !PlayerHasDebuff(Quake) || !QuakingHelper);
        private bool QuakingHoly => (API.PlayerDebuffRemainingTime(Quake) > HolyLightCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingFlash => (API.PlayerDebuffRemainingTime(Quake) > FlashOfLightCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool QuakingAshen => (API.PlayerDebuffRemainingTime(Quake) > AshenCastTime && PlayerHasDebuff(Quake) || !PlayerHasDebuff(Quake));
        private bool WoGTanking => CombatRoutine.GetPropertyBool(WoGTank);
        private bool CrusaderMacro => CombatRoutine.GetPropertyBool("Crusader Strike Macro");
        private bool JudgementMacro => CombatRoutine.GetPropertyBool("Judgement Macro");
        private bool HammerMacro => CombatRoutine.GetPropertyBool("Hammer of Wrath Macro");
        private bool SooRMacro => CombatRoutine.GetPropertyBool("Shield of the Righteous Macro");
        private bool HolyShockMarco => CombatRoutine.GetPropertyBool("Holy Shock Macro");
        private bool IgnoreCSLogic => CombatRoutine.GetPropertyBool("Ignore CS Logic to DPS");
        private bool SpreadGlimmer => CombatRoutine.GetPropertyBool("Spread Glimmer");


        private bool HPOnBeaconGenerators => (API.LastSpellCastInGame == FoL || API.LastSpellCastInGame == HolyLight || API.LastSpellCastInGame == HolyShock);
        private bool AshenHallowDPS => CombatRoutine.GetPropertyBool(AshenHallowOnlyDPS);
        private bool FocusTargeting => CombatRoutine.GetPropertyBool("Focus Targeting");

        float HolyLightCastTime => 250f / (1f + API.PlayerGetHaste);
        float FlashOfLightCastTime => 150f / (1f + API.PlayerGetHaste);
        float AshenCastTime => 150f / (1f + API.PlayerGetHaste);
        private int UpcomingHolyPower
        {
            get
            {
                if ((API.TargetHasBuff(BoL) || API.TargetHasBuff(BoF) || API.TargetHasBuff(BoV)) && HPOnBeaconGenerators)
                    return API.PlayerCurrentHolyPower + 1;
                return API.PlayerCurrentHolyPower;
            }


        }
        private bool IsMaarads => API.retail_LegendaryID == 7128;
        private bool InFORange => API.FocusRange <= 40;
        //Spell Check Bools
  
        private void CastSpellOnFocus(string target, string spell)
        {
            API.CastSpell(target);
            API.CastSpell(spell);
            return;
        }
        public override void Initialize()
        {

            CombatRoutine.Name = "Holy Pally by Ryu";

            isAutoBindReady = true;
            isHealingRotationFocus = true;

            API.WriteLog("Welcome to Holy Pally FOCUS BUILD v2.2 by Ryu");
            API.WriteLog("For the Quaking helper you just need to create an ingame macro with /stopcasting and bind it under the Macros Tab in Elk :-)");
            API.WriteLog("If you wish to use the Marco's in the marco settings ( OFF BY DEFAULT), PLEASE SEE DISCORD FOR THE MACRO. IT IS IN THE PINS");
            API.WriteLog("All Talents expect PVP Talents and Row 3 talents are supported. All Cooldowns are associated with Cooldown toggle via those with sesttings.");
            API.WriteLog("Auto Bind Supported");
            API.WriteLog("For all ground spells, either use @Cursor or when it is time to place it, the Bot will pause until you've placed it. If you'd perfer to use your own logic for them, please place them on ignore in the spellbook. If using Autobind all placed spells is @player");
            API.WriteLog("Light of Dawn will not work unless you have AOE Toggle on.");
            API.WriteLog("AUTO TARGET WITH FOCUS BUILD ONLY.");
            API.WriteLog("Night Fae Cov is not supported. You can create a /xxx break marco to use those abilties when you would like at this time.");
            API.WriteLog(" If you are not using auto bind,you must set up the binds WITH /Focus [party1] etc and then match them to the Macro's's in the spell book. Enable it the Toggles. This works for both raid and party, however, you must set up the binds WITH /Focus [party1] etc IF YOU DONT UNDERSTAND USE AUTO BIND");
         //   API.WriteLog("For Assist Focus - /target [@focustarget, harm, nodead]");

            //Buff
            CombatRoutine.AddBuff(Infusion, 54149);
            CombatRoutine.AddBuff(AvengingWrath, 31884);
            CombatRoutine.AddBuff(AvengingCrusader, 216331);
            CombatRoutine.AddBuff(Forbearance, 25771);
            CombatRoutine.AddBuff(CrusaderAura, 32223);
            CombatRoutine.AddBuff(DevotionAura, 465);
            CombatRoutine.AddBuff(RuleofLaw, 214202);
            CombatRoutine.AddBuff(GlimmerofLight, 287280);
            CombatRoutine.AddBuff(ShockBarrier, 337824);
            CombatRoutine.AddBuff(BoL, 53563);
            CombatRoutine.AddBuff(BoF, 156910);
            CombatRoutine.AddBuff(Quake, 240447);
            CombatRoutine.AddBuff(DivinePurpose, 223819);
            CombatRoutine.AddBuff(HolyAvenger, 105809);
            CombatRoutine.AddBuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddBuff(BoV, 200025);
            CombatRoutine.AddBuff(EatingBuff, 327786);
            CombatRoutine.AddBuff(MageEatingBuff, 167152);
            CombatRoutine.AddBuff(MaraadsDyingBreath, 340459);
            CombatRoutine.AddBuff(UntemperedDedication, 339990);
            CombatRoutine.AddBuff(ChargedPhylactery, 345549);
            //Debuff
            CombatRoutine.AddDebuff(Forbearance, 25771);
            CombatRoutine.AddDebuff(Cons, 26573);
            CombatRoutine.AddDebuff(Quake, 240447);
            CombatRoutine.AddDebuff("Veil of Darkness", 347704);

            //Dispell Debuff
            CombatRoutine.AddDebuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddDebuffDispell(RaidDebuffDispel);
            CombatRoutine.AddDebuffDispell(MagicDebuffList);
            CombatRoutine.AddDebuffDispell(PoisonDebuffList);
            CombatRoutine.AddDebuffDispell(DiseaseDebuffList);
            CombatRoutine.AddDebuffDispell(FrozenBinds);
            CombatRoutine.AddDebuffDispell(Bursting);
            CombatRoutine.AddDebuff("Inspired", 343503);

            //Spell
            CombatRoutine.AddSpell(HolyShock, 20473, "D2");
            CombatRoutine.AddSpell(FoL, 19750, "D4");
            CombatRoutine.AddSpell(HolyLight, 82326, "D3");
            CombatRoutine.AddSpell(CrusaderStrike, 35395, "D6", "None", "None");
            CombatRoutine.AddSpell(Judgment, 275773, "D5", "None", "None");
            CombatRoutine.AddSpell(LoD, 85222, "R");
            CombatRoutine.AddSpell(AvengingWrath, 31884, "F2");
            CombatRoutine.AddSpell(HolyAvenger, 105809, "F8");
            CombatRoutine.AddSpell(AuraMastery, 31821, "T");
            CombatRoutine.AddSpell(BoS, 6940, "F1");
            CombatRoutine.AddSpell(LoH, 633, "D8");
            CombatRoutine.AddSpell(BoV, 200025);
            CombatRoutine.AddSpell(DivineShield, 642, "D0");
            CombatRoutine.AddSpell(BF, 223306);
            CombatRoutine.AddSpell(HammerofJustice, 853, "F");
            CombatRoutine.AddSpell(Cons, 26573);
            CombatRoutine.AddSpell(DivineProtection, 498);
            CombatRoutine.AddSpell(HammerofWrath, 24275, "D1");
            CombatRoutine.AddSpell(Seraphim, 152262);
            CombatRoutine.AddSpell(AvengingCrusader, 216331, "F2");
            CombatRoutine.AddSpell(HolyPrism, 114165);
            CombatRoutine.AddSpell(WoG, 85673);
            CombatRoutine.AddSpell(LoTM, 183998);
            CombatRoutine.AddSpell(CrusaderAura, 32223);
            CombatRoutine.AddSpell(DevotionAura, 465);
            CombatRoutine.AddSpell(Fleshcraft, 324631);
            CombatRoutine.AddSpell(DivineToll, 304971);
            CombatRoutine.AddSpell(VanqusihersHammer, 328204);
            CombatRoutine.AddSpell(AshenHallow, 316958, "D1", "None", "None", @"/cast [@player] #316958#");
            CombatRoutine.AddSpell(RuleofLaw, 214202);
            CombatRoutine.AddSpell(LightsHammer, 114158, "D1", "None", "None", @"/cast [@player] #114158#");
            CombatRoutine.AddSpell(SoTR, 53600, "D1", "None", "None");
            CombatRoutine.AddSpell(BoL, 53563);
            CombatRoutine.AddSpell(BoF, 156910);
            CombatRoutine.AddSpell(Cleanse, 4987);

            //Conduit
            CombatRoutine.AddConduit("Golden Path", 339114);
            CombatRoutine.AddConduit(UntemperedDedication, 339987);

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Toggle
         //   CombatRoutine.AddToggle("Mouseover");
            //   CombatRoutine.AddToggle("NPC");
            CombatRoutine.AddToggle("Auto Target");
        //    CombatRoutine.AddToggle("DPS Auto Target");
            CombatRoutine.AddToggle("DPS Only");
            CombatRoutine.AddToggle("OOC");
            CombatRoutine.AddToggle("Dispel");

            //Mouseover
            CombatRoutine.AddMacro(HolyShock + "DPS", "D1", "None", "None", @"#showtooltip #20473#
/cast [harm] #20473#
/stopmacro [harm]
/targetenemy
/cast #20473#
/targetlasttarget");
            CombatRoutine.AddMacro(CrusaderStrike + "Marco", "D6", "None", "None", @"#showtooltip #35395#
/cast [harm] #35395#
/stopmacro [harm]
/targetenemy
/cast #35395#
/targetlasttarget");
            CombatRoutine.AddMacro(Judgment + "Marco", "D6", "None", "None", @"#showtooltip #275773#
/cast [harm] #275773#
/stopmacro [harm]
/targetenemy
/cast #275773#
/targetlasttarget");
            CombatRoutine.AddMacro(SoTR + "Marco", "D6", "None", "None", @"#showtooltip #53600#
/cast [harm] #53600#
/stopmacro [harm]
/targetenemy
/cast #53600#
/targetlasttarget");
            CombatRoutine.AddMacro(HammerofWrath + "Marco", "D6", "None", "None", @"#showtooltip #24275#
/cast [harm] #24275#
/stopmacro [harm]
/targetenemy
/cast #24275#
/targetlasttarget");
            CombatRoutine.AddMacro("Trinket1", "F9", "None", "None", @"/use 13");
            CombatRoutine.AddMacro("Trinket2", "F10", "None", "None", @"/use 14");
            CombatRoutine.AddMacro("Assist", "F10", "None", "None", @"/assist");
            CombatRoutine.AddMacro("Stopcast", "F10", "None", "None", @"/stopcast");
            CombatRoutine.AddMacro(HolyLight + FO, "D1", "None", "None", @"/cast [@focus] #82326#");
            CombatRoutine.AddMacro(FoL + FO, "D1", "None", "None", @"/cast [@focus] #19750#");
            CombatRoutine.AddMacro(BoS + FO, "D1", "None", "None", @"/cast [@focus] #6940#");
            CombatRoutine.AddMacro(HolyShock + FO, "D1", "None", "None", @"/cast [@focus] #20473#");
            CombatRoutine.AddMacro(LoH + FO, "D1", "None", "None", @"/cast [@focus] #633#");
            CombatRoutine.AddMacro(BoV + FO, "D1", "None", "None", @"/cast [@focus] #200025#");
            CombatRoutine.AddMacro(HolyPrism + FO, "D1", "None", "None", @"/cast [@focus] #114165#");
            CombatRoutine.AddMacro(DivineToll + FO, "D1", "None", "None", @"/cast [@focus] #304971#");
            CombatRoutine.AddMacro(LoTM + FO, "D1", "None", "None", @"/cast [@focus] #183998#");
            CombatRoutine.AddMacro(WoG + FO, "D1", "None", "None", @"/cast [@focus] #85673#");
            CombatRoutine.AddMacro(BF + FO, "D1", "None", "None", @"/cast [@focus] #223306#");
            CombatRoutine.AddMacro(Cleanse + FO, "D1", "None", "None", @"/cast [@focus] #4987#");
            CombatRoutine.AddMacro(BoL + FO, "D1", "None", "None", @"/cast [@focus] #53563#");
            CombatRoutine.AddMacro(BoF + FO, "D1", "None", "None", @"/cast [@focus] #156910#");

            //     CombatRoutine.AddMacro(BoS + "MO", "D1", "None", "None", @"/cast [@mouseover] #6940#");
            //   CombatRoutine.AddMacro(LoH + "MO", "D1", "None", "None", @"/cast [@mouseover] #633#");
            //   CombatRoutine.AddMacro(BoV + "MO", "D1", "None", "None", @"/cast [@mouseover] #200025#");
            //   CombatRoutine.AddMacro(HolyPrism + "MO", "D1", "None", "None", @"/cast [@mouseover] #114165#");
            //   CombatRoutine.AddMacro(LoD + "MO", "R", "None", "None", @"/cast [@mouseover] #85222#");
            //   CombatRoutine.AddMacro(CrusaderStrike + "MO", "D1", "None", "None", @"/cast [@mouseover] #35395#");
            //   CombatRoutine.AddMacro(Judgment + "MO", "D1", "None", "None", @"/cast [@mouseover] #275773#");
            //   CombatRoutine.AddMacro(HammerofWrath + "MO", "D1", "None", "None", @"/cast [@mouseover] #24275#");
            //   CombatRoutine.AddMacro(DivineToll + "MO", "D1", "None", "None", @"/cast [@mouseover] #304971#");
            //   CombatRoutine.AddMacro(LoTM + "MO", "D1", "None", "None", @"/cast [@mouseover] #183998#");
            //   CombatRoutine.AddMacro(WoG + "MO", "D1", "None", "None", @"/cast [@mouseover] #85673#");
            //   CombatRoutine.AddMacro(BF + "MO", "D1", "None", "None", @"/cast [@mouseover] #223306#");
            //   CombatRoutine.AddMacro(SoTR + "MO", "D1", "None", "None", @"/cast [@mouseover] #53600#");
            //   CombatRoutine.AddMacro(Cleanse + "MO", "D1", "None", "None", @"/cast [@mouseover] #4987#");
            //   CombatRoutine.AddMacro(BoL + "MO", "D1", "None", "None", @"/cast [@mouseover] #53563#");
            //   CombatRoutine.AddMacro(BoF + "MO", "D1", "None", "None", @"/cast [@mouseover] #156910#");

            //Prop
            CombatRoutine.AddProp(DivineShield, DivineShield + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineShield + "is used, set to 0 to disable", "Defense", 20);
            CombatRoutine.AddProp(DivineProtection, DivineProtection + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineProtection + "is used, set to 0 to disable", "Defense", 70);
            CombatRoutine.AddProp(Fleshcraft, "Fleshcraft", API.numbPercentListLong, "Life percent at which " + Fleshcraft + " is used, set to 0 to disable set 100 to use it everytime", "Defense", 0);
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", API.numbPercentListLong, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 45);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", API.numbPercentListLong, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);

            CombatRoutine.AddProp("Legendary", "Select your Legendary", LegendaryList, "Select Your Legendary", "Legendary");

            CombatRoutine.AddProp("Use Covenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + "On Cooldown, with Cooldowns, On AOE, Not Used", "Cooldowns", 0);
            CombatRoutine.AddProp(AvengingWrath, AvengingWrath + " Life Percent", API.numbPercentListLong, "Life percent at which" + AvengingWrath + "is used when AoE Healing Number of Units are at life percent, set to 0 to disable", "Cooldowns", 45);
            CombatRoutine.AddProp(HolyAvenger, HolyAvenger + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyAvenger + "is used when AoE Healing Number of Units are at life percent, set to 0 to disable", "Cooldowns", 45);
            CombatRoutine.AddProp("Avenging Wrath Usage", AvengingWrath + "Usage ", CDUsage, "Use " + AvengingWrath + "On Cooldown with AOE Logic for Healing, With Cooldowns only( you control) or not used at all", "Cooldowns", 1);
            CombatRoutine.AddProp("Holy Avenger Usage", HolyAvenger + "Usage ", CDUsage, "Use " + HolyAvenger + "On Cooldown with AOE Logic for Healing, With Cooldowns only( you control) or not used at all", "Cooldowns", 1);


            // AddProp("MouseoverInCombat", "Only Mouseover in combat", false, "Only Attack mouseover in combat to avoid stupid pulls", "Generic");
            CombatRoutine.AddProp("QuakingHelper", "Quaking Helper", false, "Will cancel casts on Quaking", "Generic");
            CombatRoutine.AddProp("Aura Switch", "Auto Aura Switch", false, "Auto Switch Aura between Crusader Aura|Devotion Aura", "Generic");

            CombatRoutine.AddProp("Crusader Strike Macro", "Crusader Strike Macro", false, "Use CS via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Judgement Macro", "Judgement Macro", false, "Use Judgement via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Hammer of Wrath Macro", "Hammer of Wrath Macro", false, "Use Hammer of Wrath via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Shield of the Righteous Macro", "Shield of the Righteous Macro", false, "Use SooR via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Holy Shock Macro", "Holy Shock Macro", false, "Use Holy Shock via Macro function w/o targeting SPECIAL USE HOLY SHOCK DPS MARCO IN SPELL BINDS - See PINS in Discord for more Info", "Marco");

            CombatRoutine.AddProp(AshenHallowOnlyDPS, AshenHallowOnlyDPS, false, "Ignore ALL Targeting expect for enemey target/rotation during Ashen Hallow", "Ashen Hallow");

            CombatRoutine.AddProp(AoEDPS, "Number of Units needed to be above DPS Health Percent to DPS in party ", API.numbPartyList, " units above for DPS ", "Targeting", 5);
            CombatRoutine.AddProp(AoEDPSRaid, "Number of Units needed to be above DPS Health Percent to DPS in Raid ", API.numbRaidList, " units above for DPS ", "Targeting", 7);
            CombatRoutine.AddProp(AoEDPSH, "Life Percent for Units to be above for DPS and below to return back to Healing", API.numbPercentListLong, "Health percent at which DPS in party" + "is used,", "Targeting", 90);
            CombatRoutine.AddProp(AoEDPSHRaid, "Life Percent for Units to be above for DPS and below to return back to Healing in raid", API.numbPercentListLong, "Health percent at which DPS" + "is used,", "Targeting", 75);

            CombatRoutine.AddProp("Ignore CS Logic to DPS", "Ignore CS Logic to DPS", false, "Should Ignore CS Logic to DPS", "Crusader Strike");

            CombatRoutine.AddProp("Spread Glimmer", "Spread Glimmer", false, "If Rotations should attempt to auto spread glimmer to the number of Glimmer targets set, Overrides Health setting for Holy Shock and uses it on CD.", "Glimmer");

            CombatRoutine.AddProp(HolyShock, HolyShock + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyShock + "is used, set to 0 to disable", "Healing", 98);
            CombatRoutine.AddProp(HolyShockDPS, HolyShock, true, "Should" + HolyShock + "be used to DPS", "Healing");
            CombatRoutine.AddProp(BoST, BoST, true, "If BoS should be on tank only, if for everyone, change to false, set to true by default", "Healing");
            CombatRoutine.AddProp(LoHT, LoHT, true, "If LoH should be on tank only, if for everyone, change to false, set to true by default", "Healing");
            CombatRoutine.AddProp(WoGTank, WoGTank, true, "If WoG should be used when tank is low over LoD when AoE Healing is on, if prefer LoD Healing priority above WoG target, change to false, set to true by default", "Healing");
            CombatRoutine.AddProp(HolyLight, HolyLight + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyLight + "is used, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(HolyLightBeacon, HolyLightBeacon + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyLight + "on your beacon target is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(HolyLightIBeacon, HolyLightIBeacon + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyLight + " with infusion on your beacon target is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(LoTM, LoTM + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoTM + "is used, set to 0 to disable", "Healing", 60);
            CombatRoutine.AddProp(LoTMH, LoTMH + " Player Health Percent", API.numbPercentListLong, "Player Health percent at which" + LoTM + "is used, set to 0 to disable", "Healing", 80);
            CombatRoutine.AddProp(LoTMM, LoTMM + " Player Health Percent", API.numbPercentListLong, "Target Health percent at which" + LoTM + "is used while moving, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(HoLI, HoLI + " Life Percent", API.numbPercentListLong, "Life percent at which" + HoLI + "is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(FoL, FoL + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoL + "is used, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(FoLBeacon, FoLBeacon + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoL + "on your beacon target is used, set to 0 to disable", "Healing", 75);
            CombatRoutine.AddProp(FoLIBeacon, FoLIBeacon + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoL + " with infusion on your beacon target is used, set to 0 to disable", "Healing", 75);
            CombatRoutine.AddProp(FoLI, FoLI + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoLI + "is used, set to 0 to disable", "Healing", 70);
            CombatRoutine.AddProp(WoG, WoG + " Life Percent", API.numbPercentListLong, "Life percent at which" + WoG + "is used, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(WoGTank2, WoGTank2 + " Life Percent", API.numbPercentListLong, "Life percent at which" + WoG + "is used, set to 0 to disable", "Healing", 80);
            CombatRoutine.AddProp(WOGMax, WOGMax + " Life Percent", API.numbPercentListLong, "Life percent at which" + WoG + "is used when at max holy power, set to 0 to disable", "Healing", 100);
            CombatRoutine.AddProp(WoGOverLoD, WoGOverLoD + " Life Percent", API.numbPercentListLong, "Life percent at which" + WoG + "is used over Light of Dawn for your current target, set to 0 to disable", "Healing", 30);
            CombatRoutine.AddProp(BoS, BoS + " Life Percent", API.numbPercentListLong, "Life percent at which" + BoS + "is used, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(LoH, LoH + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoH + "is used, set to 0 to disable", "Healing", 15);
            CombatRoutine.AddProp(BF, BF + " Life Percent", API.numbPercentListLong, "Life percent at which" + BF + "is used, set to 0 to disable", "Healing", 97);

            CombatRoutine.AddProp(AvengingWrathPartyAoE, "Avenging Wrath Units Number in Party", API.numbPartyList, "Number of Units needed for Avenging Wrath to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(AvengingWrathRaidAoE, "Avenging Wrath Units Number in Raid", API.numbRaidList, "Number of Units needed for Avenging Wrath to be used in Raid", "AOE Healing Count", 6);
            CombatRoutine.AddProp(HolyAvengerPartyAoE, "Holy Avenger Units Number in Party", API.numbPartyList, "Number of Units needed for Holy Avenger to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(HolyAvengerRaidAoE, "Holy Avenger Units Number in Raid", API.numbRaidList, "Number of Units needed for Holy Avenger to be used in Raid", "AOE Healing Count", 6);
            CombatRoutine.AddProp(AuraMasteryPartyAoE, "Aura Mastery Units Number in Party", API.numbPartyList, "Number of Units needed for Aura Mastery to be used in Party", "AOE Healing Count", 4);
            CombatRoutine.AddProp(AuraMasteryRaidAoE, "Aura Mastery Units Number in Raid", API.numbRaidList, "Number of Units needed for Aura Mastery to be used in Raid", "AOE Healing Count", 8);
            CombatRoutine.AddProp(TrinketPartyAoE, "Trinket Units Number in Party", API.numbPartyList, "Number of Units needed for Trinkets to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(TrinketRaidAoE, "Trinket Units Number in Raid", API.numbRaidList, "Number of Units needed for Trinkets to be used in Raid", "AOE Healing Count", 6);
            CombatRoutine.AddProp(HolyPrismPartyAoE, "Holy Prism Units Number in Party", API.numbPartyList, "Number of Units needed for Holy Prism to be used Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(HolyPrismRaidAoE, "Holy Prism Units Number in Raid", API.numbRaidList, "Number of Units needed for Holy Prism to be used in Raid", "AOE Healing Count", 6);
            CombatRoutine.AddProp(BoVPartyAoE, "Beacon of Virtue Units Number in Party", API.numbPartyList, "Number of Units needed for Beacon of Virtue to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(BoVRaidAoE, "Beacon of Virtue Units Number in Raid", API.numbRaidList, "Number of Units needed for for Beacon of Virtue to be used in Raid", "AOE Healing Count", 5);
            CombatRoutine.AddProp(LightsHammerPartyAoE, "Lights Hammer Units Number in Party ", API.numbPartyList, "Number of Units needed for Lights Hammer to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(LightsHammerRaidAoE, "Lights Hamnmer Units Number in Raid ", API.numbRaidList, "Number of Units needed for Lights Hammer to be used in Raid", "AOE Healing Count", 6);
            CombatRoutine.AddProp(AshenHPartyAoE, "Ashen Hallow Units Number in Party", API.numbPartyList, "Number of Units needed for Ashen Hallow to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(AshenHRaidAoE, "Ashen Hallow Units Number in Raid", API.numbRaidList, "Number of Units needed for Ashen Hallow to be used in Raid", "AOE Healing Count", 6);
            CombatRoutine.AddProp(LoDPartyAoE, "Light of Dawn Units Number in Party", API.numbPartyList, "Number of Units needed for Light of Dawn to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(LoDRaidAoE, "Light of Dawn Units Number in Raid", API.numbRaidList, "Number of Units needed for for Light of Dawn to be used in Raid", "AOE Healing Count", 5);
            CombatRoutine.AddProp(DTPartyAoE, "Divine Toll Units Number in Party", API.numbPartyList, "Number of Units needed for Divine Toll to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(DTRaidAoE, "Divine Toll Units Number in Raid", API.numbRaidList, "Number of Units needed for Divine Toll to be used in Raid", "AOE Healing Count", 5);
            CombatRoutine.AddProp(GlimmerPartyAoE, "Glimmer Units Number in Party", API.numbPartyList, "Number of Units needed for Glimmer to be used in Party", "AOE Healing Count", 3);
            CombatRoutine.AddProp(GlimmerRaidAoE, "Glimmer Units Number in Raid", API.numbRaidList, "Number of Units needed for Glimmer to be used in Raid", "AOE Healing Count", 6);

            CombatRoutine.AddProp(LoD, LoD + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoD + "is used when AoE Healing Number of units are at life percent, set to 0 to disable", "AOE Healing", 80);
            CombatRoutine.AddProp(LODMax, LODMax + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoD + "is used AoE Healing Number of units are at life percent and you have max holy power, set to 0 to disable", "AOE Healing", 80);
            CombatRoutine.AddProp(BoV, BoV + " Life Percent", API.numbPercentListLong, "Life percent at which" + BoV + "is used AoE Healing Number of units are at life percent, set to 0 to disable", "AOE Healing", 80);
            CombatRoutine.AddProp(LightsHammer, LightsHammer + " Life Percent", API.numbPercentListLong, "Life percent at which" + LightsHammer + "is used when AoE Healing Number of units are at life percent, set to 0 to disable", "AOE Healing", 75);
            CombatRoutine.AddProp(AshenHallow, AshenHallow + " Life Percent", API.numbPercentListLong, "Life percent at which" + AshenHallow + "is used when AoE Healing Number of units are at life percent, set to 0 to disable", "AOE Healing", 65);
            CombatRoutine.AddProp(AuraMastery, AuraMastery + " Life Percent", API.numbPercentListLong, "Life percent at which" + AuraMastery + "is used, set to 0 to disable", "AOE Healing", 30);
            CombatRoutine.AddProp(HolyPrism, HolyPrism + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyPrism + "is used when AoE Healing Number of units are at life percent, set to 0 to disable", "AOE Healing", 90);
            CombatRoutine.AddProp(DivineToll, DivineToll + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineToll + "is used when AoE Healing Number of units are at life percent, set to 0 to disable", "AOE Healing", 75);
            CombatRoutine.AddProp(DivineTollHealing, DivineTollHealing, true, "If Divine Toll should be on Healing, if for both, change to false, set to true by default for healing", "AOE Healing");
            //    CombatRoutine.AddProp(AoE, "Number of Units for AoE Healing ", API.numbPartyList, "units for AoE Healing", "AOE Healing", 3);
            //     CombatRoutine.AddProp(AoERaid, "Number of Units for AoE Healing in raid ", API.numbRaidList, "units for AoE Healing in raid", "AOE Healing", 6);
            CombatRoutine.AddProp(Trinket, Trinket + " Life Percent", API.numbPercentListLong, "Life percent at which " + "Trinkets" + " when AoE Healing Number of units are met should be used, set to 0 to disable", "AOE Healing", 75);

            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket 1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket 2 be used", "Trinket", 0);
            CombatRoutine.AddProp(Sunblood, Sunblood, false, "Is your trinket the" + Sunblood + " ? -- NOTE MUST BE TRINKET SLOT 1 ", "Trinket");

        }

        public override void Pulse()
        {
            string lowestTank;
            var UnitLowest = API.UnitLowest(out lowestTank).ToLower();
            var LowestGlimmer = LowestGlimmerUnit().ToLower();
            var DispelUnitLowest = LowestDispelUnit().ToLower();

            // if (UnitLowest == "none")
            // {
            //     API.WriteLog("Lowest None : " + UnitLowest);
            // }
            // if (UnitLowest != "none")
            // {
            //     API.WriteLog("Lowest : " + UnitLowest);
            // }
            //  API.WriteLog("Lowest None : " + UnitLowest);
            //  API.WriteLog("Lowest Dispel : " + DispelUnitLowest);
            //     API.WriteLog("Lowest Glimmer : " + LowestGlimmer);
            //  API.WriteLog("BoV Talent? : " + BeaconofVirtue);
            //       API.WriteLog("Glimmer Time Rem : " + API.TargetBuffTimeRemaining(GlimmerofLight));
            if (API.NearestEnnemyRange > 5 && CrusaderMacro && API.PlayerIsInCombat)
            {
                API.WriteLog("Not in Crusader Strike Range for Marco OR Nameplate is not visble/off - Rotation is unable to use CS, Please move closer to enemy unit");
            }
            if (API.LastSpellCastInGame == AshenHallow && PlayerCovenantSettings == "Venthyr")
            {
                AshenWatch.Restart();
                API.WriteLog("Ashen Hallow Started");
            }
            if (AshenWatch.ElapsedMilliseconds >= 30000 && AshenWatch.IsRunning)
            {
                AshenWatch.Stop();
                API.WriteLog("Ashen Hallow Ended");
            }
            if (API.LastSpellCastInGame == Cons)
            {
                ConsWatch.Restart();
            }
            if (ConsWatch.IsRunning && ConsWatch.ElapsedMilliseconds >= 12000)
            {
                ConsWatch.Stop();
            }
            if (IsAutoSwap)
            {
                if (LowestGlimmer != "none" && SpreadGlimmer && (IsOOC || API.PlayerIsInCombat))
                {
                    if (!IsDPSOnly && GlimmerofLightTalent && SpreadGlimmer && GlimmerTracking && API.CanCast(HolyShock) && (!API.UnitHasBuff(GlimmerofLight, LowestGlimmer, true) || API.UnitBuffTimeRemaining(GlimmerofLight, LowestGlimmer, true) <= 500) && (API.PlayerIsInRaid && BuffRaidTracking(GlimmerofLight) < 9 || API.PlayerIsInGroup && !API.PlayerIsInRaid && BuffPartyTracking(GlimmerofLight) < 6) && API.UnitHealthPercent(LowestGlimmer) > 0 && API.PlayerCurrentHolyPower < 5 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", LowestGlimmer))
                    {
                        CastSpellOnFocus(LowestGlimmer, HolyShock + FO);
                        if (API.PlayerIsInGroup && !API.PlayerIsInRaid)
                        {
                            API.WriteLog("Number of Units that Have Glimmer? :" + API.UnitBuffCountParty(GlimmerofLight) + " Total Number of Units to keep Glimmer on? " + GlimmerAoEPartyNumber);
                        }
                        if (API.PlayerIsInRaid)
                        {
                            API.WriteLog("Number of Units that Have Glimmer? :" + API.unitBuffCountRaid(GlimmerofLight) + " Total Number of Units to keep Glimmer on? " + GlimmerAoERaidNumber);

                        }
                        return;
                    }
                }
            }           
            if (!API.PlayerIsMounted && !API.PlayerSpellonCursor && (IsOOC || API.PlayerIsInCombat) && IsNotEating)
            {
                if (!IsAutoSwap || IsAutoSwap)
                {
                    if (API.PlayerCurrentCastTimeRemaining > 40 && QuakingHelper && Quaking)
                    {
                        API.CastSpell("Stopcast");
                        API.WriteLog("Debuff Time Remaining for Quake : " + API.PlayerDebuffRemainingTime(Quake));
                        return;
                    }
                    if (API.CanCast(AvengingWrath) && !AvengingCrusaderT && !API.PlayerHasBuff(AvengingWrath) && API.TargetRange <= 40 && (IsCooldowns && UseAV == "With Cooldowns" || UseAV == "On Cooldown" && AVAoE))
                    {
                        API.CastSpell(AvengingWrath);
                        return;
                    }
                    if (API.CanCast(AvengingCrusader) && AvengingCrusaderT && !API.PlayerHasBuff(AvengingCrusader) && API.TargetRange <= 40 && IsCooldowns)
                    {
                        API.CastSpell(AvengingCrusader);
                        return;
                    }
                    if (API.CanCast(HolyAvenger) && HolyAvengerT && API.TargetRange <= 40 && (IsCooldowns && UseHV == "With Cooldowns" || UseHV == "On Cooldown" && HVAoE))
                    {
                        API.CastSpell(HolyAvenger);
                        return;
                    }
                    if (API.CanCast(Seraphim) && SeraphimT && API.TargetRange <= 40 && IsCooldowns && API.PlayerCurrentHolyPower >= 3)
                    {
                        API.CastSpell(Seraphim);
                        return;
                    }
                    if (API.CanCast(DivineShield) && API.PlayerHealthPercent <= DivineShieldLifePercent && API.PlayerHealthPercent > 0 && !API.PlayerHasDebuff(Forbearance))
                    {
                        API.CastSpell(DivineShield);
                        return;
                    }
                    if (API.CanCast(DivineProtection) && API.PlayerHealthPercent <= DivineProtectionLifePercent && API.PlayerHealthPercent > 0)
                    {
                        API.CastSpell(DivineProtection);
                        return;
                    }
                    //       if (IsNpC && API.PlayerHasBuff(HolyAvenger))
                    //     {
                    //       if (API.CanCast(WoG) && API.PlayerCurrentHolyPower > 2 && !API.PlayerCanAttackTarget)
                    //     {
                    //       API.CastSpell(WoG);
                    //         return;
                    //     }
                    //     if (API.CanCast(HolyShock) && !API.PlayerIsCasting(true) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackTarget)
                    //     {
                    //         API.CastSpell(HolyShock);
                    //         return;
                    //     }
                    //     if (API.CanCast(HolyLight) && !API.PlayerIsCasting(true) && API.PlayerCurrentHolyPower < 3 && (API.PlayerLastSpell != HolyLight || API.LastSpellCastInGame != HolyLight) && !API.PlayerCanAttackTarget)
                    //     {
                    //         API.CastSpell(HolyLight);
                    //        return;
                    //   }
                    // }
                    // if (IsNpC)
                    //  {
                    //      if (API.CanCast(WoG) && API.PlayerCurrentHolyPower > 2 && !API.PlayerCanAttackTarget)
                    //      {
                    //          API.CastSpell(WoG);
                    //          return;
                    //      }
                    //      if (API.CanCast(HolyShock) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackTarget)
                    //      {
                    //          API.CastSpell(HolyShock);
                    //         return;
                    //     }
                    //     if (API.CanCast(HolyLight) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackTarget)
                    //     {
                    //         API.CastSpell(HolyLight);
                    //         return;
                    //     }
                    // }
                    if (API.CanCast(Cons) && !API.PlayerHasBuff(DivinePurpose) && API.PlayerIsConduitSelected("Golden Path") && API.PlayerHealthPercent < PlayerHealth && (!ConsWatch.IsRunning || !API.TargetHasDebuff(Cons) && IsMelee) && !API.PlayerIsMoving && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(Cons);
                        return;
                    }
                    if (API.CanCast(Cons) && IsDPSOnly && (API.NearestEnnemyRange < 3 && !ConsWatch.IsRunning || !API.TargetHasDebuff(Cons) && IsMelee) && !API.PlayerIsMoving)
                    {
                        API.CastSpell(Cons);
                        return;
                    }
                    if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1 && NotChanneling && API.TargetRange <= 40 && !API.PlayerHasBuff(ChargedPhylactery))
                    {
                        API.CastSpell("Trinket1");
                    }
                    if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2 && NotChanneling && API.TargetRange <= 40 && !API.PlayerHasBuff(ChargedPhylactery))
                    {
                        API.CastSpell("Trinket2");
                    }
                }
                if (IsAutoSwap)
                {
                    if (UnitLowest != "none")
                    {
                        #region Dispell
                        if (DispelUnitLowest != "none" && IsDispell)
                        {
                            if (API.CanCast(Cleanse) && NotChanneling && API.UnitHealthPercent(DispelUnitLowest) > 0 && (API.UnitHasDebuffDispel(AllDungeonDispel, DispelUnitLowest) || API.UnitHasDebuffDispel(RaidDebuffDispel, DispelUnitLowest) || API.UnitHasDebuffDispel(Bursting, DispelUnitLowest) && API.UnitDebuffStacks(Burst, DispelUnitLowest) >= 2 || API.UnitHasDebuffDispel(FrozenBinds, DispelUnitLowest) && API.UnitDebuffRemainingTime(FrozenBind, DispelUnitLowest) <= 1000))
                            {
                                CastSpellOnFocus(DispelUnitLowest, Cleanse + FO);
                                return;
                            }
                        }
                        #endregion
                        if (!IsDPSOnly)
                        {
                            //Healing
                            if (API.CanCast(BoL) && API.FocusRange <= 40 && BoLTracking && !API.FocusHasBuff(BoL, true) && !API.FocusHasBuff(BoF, true) && (API.FocusRoleSpec == API.TankRole || API.FocusRoleSpec == API.HealerRole) && !BeaconofVirtue && API.FocusHealthPercent > 0)
                            {
                                API.CastSpell(BoL + FO);
                                return;
                            }
                            if (API.CanCast(BoF) && API.FocusRange <= 40 && BoFTracking && !API.FocusHasBuff(BoF, true) && !API.FocusHasBuff(BoL, true) && (API.FocusRoleSpec == API.TankRole || API.FocusRoleSpec == API.HealerRole) && BeaconofFaith && API.FocusHealthPercent > 0)
                            {
                                API.CastSpell(BoF + FO);
                                return;
                            }
                            if (API.CanCast(RuleofLaw) && RuleofLawTalent && !API.PlayerHasBuff(RuleofLaw) && (API.UnitRange(UnitLowest) > 40 || API.UnitRangeCount(20) < 5 && LoDAoE || API.UnitBelowHealthPercent(30) <= 4) && API.UnitHealthPercent(UnitLowest) > 0)
                            {
                                API.CastSpell(RuleofLaw);
                                return;
                            }
                            if (API.CanCast(LoH) && API.UnitHealthPercent(UnitLowest) <= LoHLifePercent && API.UnitHealthPercent(UnitLowest) > 0 && (LoHTank && API.UnitRoleSpec(UnitLowest) == API.TankRole || !LoHTank) && !API.UnitHasDebuff(Forbearance, UnitLowest, false, false) && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.UnitIsIncombat(UnitLowest) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, LoH + FO);
                                return;
                            }
                            if (API.UnitRoleSpec(UnitLowest) != 996 && API.CanCast(BoS) && API.UnitHealthPercent(UnitLowest) <= BoSLifePercent && API.UnitHealthPercent(UnitLowest) > 0 && (BoSTank && API.UnitRoleSpec(UnitLowest) == API.TankRole || !BoSTank) && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.UnitIsIncombat(UnitLowest) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, BoS + FO);
                                return;
                            }
                            if (API.CanCast(LightsHammer) && LightsHammerT && LHAoE)
                            {
                                API.CastSpell(LightsHammer);
                                return;
                            }
                            if (API.CanCast(AshenHallow) && API.TargetRange <= 15 && PlayerCovenantSettings == "Venthyr" && (AHAoE || UseCovenant == "With Cooldowns" && IsCooldowns) && (!QuakingAshen || QuakingAshen && QuakingHelper))
                            {
                                API.CastSpell(AshenHallow);
                                return;
                            }
                            if (API.CanCast(AuraMastery) && AMAoE && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving))
                            {
                                API.CastSpell(AuraMastery);
                                return;
                            }
                            if (API.CanCast(BoV) && BeaconofVirtue && BoVAoE && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, BoV + FO);
                                return;
                            }
                            if (API.CanCast(DivineToll) && DTRange && API.UnitRange(UnitLowest) < 31 && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" && DTAoE || UseCovenant == "on AOE" && DTAoE) && NotChanneling && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, DivineToll + FO);
                                return;
                            }
                            if (API.CanCast(HolyPrism) && HolyPrismT && HPAoE && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, HolyPrism + FO);
                                return;
                            }
                            if (lowestTank != "none" && !IsDPSOnly && WoGTanking && API.CanCast(WoG) && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.UnitHealthPercent(lowestTank) <= WoGTankifePercent || API.UnitHealthPercent(lowestTank) <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5 || UnitHasDebuff("Veil of Darkness", lowestTank) && API.UnitHealthPercent(lowestTank) < 100 && API.UnitHealthPercent(lowestTank) <= WoGTankifePercent) && API.UnitHealthPercent(lowestTank) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", lowestTank))
                            {
                                CastSpellOnFocus(lowestTank, WoG + FO);
                                return;
                            }
                            if (API.CanCast(WoG) && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.UnitHealthPercent(UnitLowest) <= WoGOverLoDifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= WoGOverLoDifePercent) && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, WoG + FO);
                                return;
                            }
                            if (IsAOE && API.CanCast(LoD) && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (LoDAoE || LoDMaxAoE && API.PlayerCurrentHolyPower == 5) && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerCanAttackTarget || !API.PlayerCanAttackTarget) && (API.PlayerIsMoving || !API.PlayerIsMoving) && (!API.PlayerIsInRaid && LoDRangePartyCheck || API.PlayerIsInRaid && LoDRangeRaidCheck))
                            {
                                API.CastSpell(LoD);
                                return;
                            }
                            if (API.CanCast(HammerofWrath) && AshenWatch.IsRunning && API.PlayerCanAttackTarget && API.TargetRange <= 30 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 && API.PlayerCurrentHolyPower <= 4 || API.PlayerHasBuff(AvengingWrath) && Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.TargetIsIncombat)
                            {
                                API.CastSpell(HammerofWrath);
                                return;
                            }
                            if (API.CanCast(Judgment) && API.PlayerCanAttackTarget && API.TargetRange <= 40 && API.TargetHealthPercent > 0 && (API.PlayerIsInGroup && !API.PlayerIsInRaid && JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && JudgementofLight || API.PlayerIsInGroup && !API.PlayerIsInRaid && !JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && !JudgementofLight) && API.TargetIsIncombat)
                            {
                                API.CastSpell(Judgment);
                                return;
                            }
                            if (API.CanCast(Judgment) && JudgementMacro && API.NearestEnnemyRange < 18 && (API.PlayerIsInGroup && !API.PlayerIsInRaid && JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && JudgementofLight || API.PlayerIsInGroup && !API.PlayerIsInRaid && !JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && !JudgementofLight) && API.PlayerIsInCombat)
                            {
                                API.CastSpell(Judgment + "Marco");
                                return;
                            }
                            if (API.CanCast(HammerofWrath) && API.PlayerCanAttackTarget && API.TargetRange <= 30 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 && API.PlayerCurrentHolyPower <= 4 || (API.PlayerHasBuff(AvengingWrath) || AshenWatch.IsRunning) && Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.TargetIsIncombat)
                            {
                                API.CastSpell(HammerofWrath);
                                return;
                            }
                            if ((API.CanCast(HammerofWrath) || !API.SpellISOnCooldown(HammerofWrath) && (API.PlayerHasBuff(AvengingWrath) && Level >= 58 || AshenWatch.IsRunning)) && HammerMacro && API.NearestEnnemyRange < 18 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 || Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.PlayerIsInCombat)
                            {
                                API.CastSpell(HammerofWrath + "Marco");
                                return;
                            }
                            if (API.CanCast(HolyShock) && (API.UnitHealthPercent(UnitLowest) <= HolyShockLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 || SpreadGlimmer && (!API.UnitHasBuff(GlimmerofLight, UnitLowest, true) || API.UnitBuffTimeRemaining(GlimmerofLight, UnitLowest, true) <= 500) && (API.PlayerIsInRaid && BuffRaidTracking(GlimmerofLight) < 9 || API.PlayerIsInGroup && !API.PlayerIsInRaid && BuffPartyTracking(GlimmerofLight) < 6)) && API.UnitHealthPercent(UnitLowest) > 0 && API.PlayerCurrentHolyPower < 5 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, HolyShock + FO);
                                return;
                            }
                            if (API.UnitRoleSpec(UnitLowest) != 996 && API.CanCast(LoTM) && IsMaarads && (API.PlayerIsMoving && (API.UnitHealthPercent(UnitLowest) <= LoTMMovingLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= LoTMMovingLifePercent) || API.UnitHealthPercent(UnitLowest) <= LoTMLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= LoTMLifePercent && API.PlayerHealthPercent >= LoTMHealthPercent) && (API.PlayerHasBuff(MaraadsDyingBreath) && (!API.UnitHasBuff(BoL, UnitLowest, true) || !API.UnitHasBuff(BoF, UnitLowest, true)) || !API.PlayerHasBuff(MaraadsDyingBreath)) && API.UnitHealthPercent(UnitLowest) > 0 && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, LoTM + FO);
                                return;
                            }
                            if ((HSDPS || AshenWatch.IsRunning || IsDPSOnly) && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || IsDPSOnly) && HolyShockMarco && API.NearestEnnemyRange < 18 && API.PlayerIsInCombat)
                            {
                                API.CastSpell(HolyShock + "DPS");
                                return;
                            }
                            if ((HSDPS || AshenWatch.IsRunning || IsDPSOnly) && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || IsDPSOnly) && API.TargetRange <= 40 && API.PlayerCanAttackTarget && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                            {
                                API.CastSpell(HolyShock);
                                return;
                            }
                            if (API.CanCast(WoG) && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.UnitHealthPercent(UnitLowest) <= WoGLifePercent || API.UnitHealthPercent(UnitLowest) <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5 || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 || API.UnitHealthPercent(UnitLowest) <= WoGLifePercent) && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, WoG + FO);
                                return;
                            }
                            if (API.CanCast(CrusaderStrike) && CrusadersMight && (IgnoreCSLogic || API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) >= 100) && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                            {
                                API.CastSpell(CrusaderStrike);
                                return;
                            }
                            if (API.CanCast(CrusaderStrike) && CrusadersMight && (IgnoreCSLogic || API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) >= 100) && CrusaderMacro && API.NearestEnnemyRange < 5 && API.PlayerIsInCombat)
                            {
                                API.CastSpell(CrusaderStrike + "Marco");
                                return;
                            }
                            if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                            {
                                API.CastSpell(CrusaderStrike);
                                return;
                            }
                            if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && CrusaderMacro && API.NearestEnnemyRange < 5 && API.PlayerIsInCombat)
                            {
                                API.CastSpell(CrusaderStrike + "Marco");
                                return;
                            }
                            if (API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && ((API.UnitHasBuff(BoF, UnitLowest, true) || API.UnitHasBuff(BoL, UnitLowest, true)) && (API.UnitHealthPercent(UnitLowest) <= FoLBeaconILifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= FoLBeaconILifePercent) || (API.UnitHealthPercent(UnitLowest) <= FolILifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= FolILifePercent)) && API.UnitHealthPercent(UnitLowest) > 0 && !API.PlayerIsMoving && (!QuakingHelper || QuakingFlash && QuakingHelper) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, FoL + FO);
                                return;
                            }
                            if (API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && ((API.UnitHasBuff(BoF, UnitLowest, true) || API.UnitHasBuff(BoL, UnitLowest, true)) && (API.UnitHealthPercent(UnitLowest) <= HolyLightBeaconILifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= HolyLightBeaconILifePercent) || API.UnitHealthPercent(UnitLowest) <= HoLILifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= HoLILifePercent) && API.UnitHealthPercent(UnitLowest) > 0 && !API.PlayerIsMoving && (!QuakingHelper || QuakingHoly && QuakingHelper) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, HolyLight + FO);
                                return;
                            }
                            if (API.CanCast(FoL) && ((API.UnitHasBuff(BoF, UnitLowest, true) || API.UnitHasBuff(BoL, UnitLowest, true)) && (API.UnitHealthPercent(UnitLowest) <= FoLBeaconLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= FoLBeaconLifePercent) || (API.UnitHealthPercent(UnitLowest) <= FoLLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= FoLLifePercent)) && API.UnitHealthPercent(UnitLowest) > 0 && !API.PlayerIsMoving && (!QuakingHelper || QuakingFlash && QuakingHelper) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, FoL + FO);
                                return;
                            }
                            if (API.CanCast(HolyLight) && ((API.UnitHasBuff(BoF, UnitLowest, true) || API.UnitHasBuff(BoL, UnitLowest, true)) && API.UnitHealthPercent(UnitLowest) <= HolyLightBeaconLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= HolyLightBeaconLifePercent) || (API.UnitHealthPercent(UnitLowest) <= HolyLightLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= HolyLightLifePercent) && API.UnitHealthPercent(UnitLowest) > 0 && !API.PlayerIsMoving && (!QuakingHelper || QuakingHoly && QuakingHelper) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, HolyLight + FO);
                                return;
                            }
                            if (API.CanCast(BF) && BestowFaith && API.UnitHealthPercent(UnitLowest) <= BFLifePercent && API.UnitHealthPercent(UnitLowest) > 0 && (API.PlayerIsMoving || !API.PlayerIsMoving) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, BF + FO);
                                return;
                            }
                            if (API.UnitRoleSpec(UnitLowest) != 996 && API.CanCast(LoTM) && (API.PlayerIsMoving && (API.UnitHealthPercent(UnitLowest) <= LoTMMovingLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= LoTMMovingLifePercent) || API.UnitHealthPercent(UnitLowest) <= LoTMLifePercent || UnitHasDebuff("Veil of Darkness", UnitLowest) && API.UnitHealthPercent(UnitLowest) < 100 && API.UnitHealthPercent(UnitLowest) <= LoTMLifePercent && API.PlayerHealthPercent >= LoTMHealthPercent) && API.UnitHealthPercent(UnitLowest) > 0 && !UnitHasDebuff("Gluttonous Miasma", UnitLowest))
                            {
                                CastSpellOnFocus(UnitLowest, LoTM + FO);
                                return;
                            }
                        }
                    }
                }
                if (!IsAutoSwap)
                {
                    if (API.CanCast(BoL) && BoLTracking && !API.TargetHasBuff(BoL) && !API.TargetHasBuff(BoF) && (API.TargetRoleSpec == API.TankRole || API.TargetRoleSpec == API.HealerRole) && !BeaconofVirtue && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(BoL);
                        return;
                    }
                    if (API.CanCast(BoF) && BoFTracking && !API.TargetHasBuff(BoF) && !API.TargetHasBuff(BoL) && (API.TargetRoleSpec == API.TankRole || API.TargetRoleSpec == API.HealerRole) && BeaconofFaith && API.TargetHealthPercent > 0)
                    {
                        API.CastSpell(BoF);
                        return;
                    }
                    if (UnitLowest != "none" && API.CanCast(RuleofLaw) && RuleofLawTalent && !API.PlayerHasBuff(RuleofLaw) && (API.UnitRange(UnitLowest) > 40 || API.UnitRangeCount(20) < 5 && LoDAoE || API.UnitBelowHealthPercent(30) <= 4) && API.UnitHealthPercent(UnitLowest) > 0)
                    {
                        API.CastSpell(RuleofLaw);
                        return;
                    }
                    if (LoHCheck)
                    {
                        API.CastSpell(LoH);
                        return;
                    }
                    if (BoSCheck)
                    {
                        API.CastSpell(BoS);
                        return;
                    }
                    if (API.CanCast(LightsHammer) && LightsHammerT && InRange && LHAoE)
                    {
                        API.CastSpell(LightsHammer);
                        return;
                    }
                    if (API.CanCast(AshenHallow) && InRange && PlayerCovenantSettings == "Venthyr" && (AHAoE || UseCovenant == "With Cooldowns" && IsCooldowns) && (!QuakingAshen || QuakingAshen && QuakingHelper))
                    {
                        API.CastSpell(AshenHallow);
                        return;
                    }
                    if (AuraMasteryCheck)
                    {
                        API.CastSpell(AuraMastery);
                        return;
                    }
                    if (DTCheck)
                    {
                        API.CastSpell(DivineToll);
                        return;
                    }
                    if (BoVCheck)
                    {
                        API.CastSpell(BoV);
                        return;
                    }
                    if (HolyPrismCheck)
                    {
                        API.CastSpell(HolyPrism);
                        return;
                    }
                    if (!IsDPSOnly && WoGTankCheck)
                    {
                        API.CastSpell(WoG);
                        return;
                    }
                    if (!IsDPSOnly && WoGOverLoDCheck)
                    {
                        API.CastSpell(WoG);
                        return;
                    }
                    if (LoDCheck)
                    {
                        API.CastSpell(LoD);
                        return;
                    }
                    if (API.CanCast(Judgment) && API.PlayerCanAttackTarget && InRange && API.TargetHealthPercent > 0 && (JudgementofLight || !JudgementofLight) && API.PlayerIsInCombat)
                    {
                        API.CastSpell(Judgment);
                        return;
                    }
                    if (API.CanCast(Judgment) && JudgementMacro && API.NearestEnnemyRange < 18 && (JudgementofLight || !JudgementofLight) && API.PlayerIsInCombat)
                    {
                        API.CastSpell(Judgment + "Marco");
                        return;
                    }
                    if ((API.CanCast(HammerofWrath) || !API.SpellISOnCooldown(HammerofWrath) && (API.PlayerHasBuff(AvengingWrath) && Level >= 58 || AshenWatch.IsRunning)) && API.PlayerCanAttackTarget && InRange && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 || Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.PlayerIsInCombat)
                    {
                        API.CastSpell(HammerofWrath);
                        return;
                    }
                    if ((API.CanCast(HammerofWrath) || !API.SpellISOnCooldown(HammerofWrath) && (API.PlayerHasBuff(AvengingWrath) && Level >= 58 || AshenWatch.IsRunning)) && HammerMacro && API.NearestEnnemyRange < 18 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 || Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.PlayerIsInCombat)
                    {
                        API.CastSpell(HammerofWrath + "Marco");
                        return;
                    }
                    if (!IsDPSOnly && HolyShockCheck)
                    {
                        API.CastSpell(HolyShock);
                        return;
                    }
                    if (!IsDPSOnly && LoTMLegoCheck)
                    {
                        API.CastSpell(LoTM);
                        return;
                    }
                    if ((HSDPS || AshenWatch.IsRunning || IsDPSOnly) && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning) && HolyShockMarco && API.NearestEnnemyRange < 18 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(HolyShock + "DPS");
                        return;
                    }
                    if ((HSDPS || AshenWatch.IsRunning || IsDPSOnly) && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || !IsAutoSwap) && InRange && API.PlayerCanAttackTarget && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(HolyShock);
                        return;
                    }
                    if (!IsDPSOnly && WoGCheck)
                    {
                        API.CastSpell(WoG);
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && CrusadersMight && (IgnoreCSLogic || API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) >= 100) && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(CrusaderStrike);
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && CrusadersMight && (IgnoreCSLogic || API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) >= 100) && CrusaderMacro && API.NearestEnnemyRange < 5 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(CrusaderStrike + "Marco");
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(CrusaderStrike);
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && CrusaderMacro && API.NearestEnnemyRange < 5 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(CrusaderStrike + "Marco");
                        return;
                    }
                    if (!IsDPSOnly && FlashofLightInfusionCheck && (!QuakingHelper || QuakingFlash && QuakingHelper))
                    {
                        API.CastSpell(FoL);
                        return;
                    }
                    if (!IsDPSOnly && HolyLightInfusionCheck && (!QuakingHelper || QuakingHoly && QuakingHelper))
                    {
                        API.CastSpell(HolyLight);
                        return;
                    }
                    if (!IsDPSOnly && FlashofLightCheck && (!QuakingHelper || QuakingFlash && QuakingHelper))
                    {
                        API.CastSpell(FoL);
                        return;
                    }
                    if (!IsDPSOnly && HolyLightCheck && (!QuakingHelper || QuakingHoly && QuakingHelper))
                    {
                        API.CastSpell(HolyLight);
                        return;
                    }
                    if (!IsDPSOnly && BFCheck)
                    {
                        API.CastSpell(BF);
                        return;
                    }
                    if (!IsDPSOnly && LoTMCheck)
                    {
                        API.CastSpell(LoTM);
                        return;
                    }
                }
                //DPS
                if (API.TargetIsIncombat || API.PlayerIsInCombat)
                {
                    if (API.CanCast(AshenHallow) && API.TargetRange <= 15 && PlayerCovenantSettings == "Venthyr" && (AHAoE || UseCovenant == "With Cooldowns" && IsCooldowns) && (!QuakingAshen || QuakingAshen && QuakingHelper))
                    {
                        API.CastSpell(AshenHallow);
                        return;
                    }
                    if (API.CanCast(Cons) && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && (API.NearestEnnemyRange < 6 && !ConsWatch.IsRunning || !API.TargetHasDebuff(Cons) && IsMelee) && !API.PlayerIsMoving)
                    {
                        API.CastSpell(Cons);
                        return;
                    }
                    if (API.CanCast(HammerofWrath) && AshenWatch.IsRunning && API.PlayerCanAttackTarget && API.TargetRange <= 30 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 && API.PlayerCurrentHolyPower <= 4 || API.PlayerHasBuff(AvengingWrath) && Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.TargetIsIncombat)
                    {
                        API.CastSpell(HammerofWrath);
                        return;
                    }
                    if (API.CanCast(Judgment) && API.PlayerCanAttackTarget && API.TargetRange <= 40 && API.TargetHealthPercent > 0 && (API.PlayerIsInGroup && !API.PlayerIsInRaid && JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && JudgementofLight || API.PlayerIsInGroup && !API.PlayerIsInRaid && !JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && !JudgementofLight) && API.TargetIsIncombat)
                    {
                        API.CastSpell(Judgment);
                        return;
                    }
                    if (API.CanCast(Judgment) && JudgementMacro && API.NearestEnnemyRange < 18 && (API.PlayerIsInGroup && !API.PlayerIsInRaid && JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && JudgementofLight || API.PlayerIsInGroup && !API.PlayerIsInRaid && !JudgementofLight && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && !JudgementofLight) && API.PlayerIsInCombat)
                    {
                        API.CastSpell(Judgment + "Marco");
                        return;
                    }
                    if (API.CanCast(HammerofWrath) && API.PlayerCanAttackTarget && API.TargetRange <= 30 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 && API.PlayerCurrentHolyPower <= 4 || (API.PlayerHasBuff(AvengingWrath) || AshenWatch.IsRunning) && Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.TargetIsIncombat)
                    {
                        API.CastSpell(HammerofWrath);
                        return;
                    }
                    if ((API.CanCast(HammerofWrath) || !API.SpellISOnCooldown(HammerofWrath) && (API.PlayerHasBuff(AvengingWrath) && Level >= 58 || AshenWatch.IsRunning)) && HammerMacro && API.NearestEnnemyRange < 18 && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 || Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.PlayerIsInCombat)
                    {
                        API.CastSpell(HammerofWrath + "Marco");
                        return;
                    }
                    if ((HSDPS || AshenWatch.IsRunning || IsDPSOnly) && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || IsDPSOnly) && HolyShockMarco && API.NearestEnnemyRange < 18 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(HolyShock + "DPS");
                        return;
                    }
                    if ((HSDPS || AshenWatch.IsRunning || IsDPSOnly) && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || IsDPSOnly) && API.TargetRange <= 40 && API.PlayerCanAttackTarget && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                    {
                        API.CastSpell(HolyShock);
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && CrusadersMight && (IgnoreCSLogic || API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) >= 100) && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                    {
                        API.CastSpell(CrusaderStrike);
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && CrusadersMight && (IgnoreCSLogic || API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) >= 100) && CrusaderMacro && API.NearestEnnemyRange < 5 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(CrusaderStrike + "Marco");
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                    {
                        API.CastSpell(CrusaderStrike);
                        return;
                    }
                    if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && CrusaderMacro && API.NearestEnnemyRange < 5 && API.PlayerIsInCombat)
                    {
                        API.CastSpell(CrusaderStrike + "Marco");
                        return;
                    }
                    if (API.CanCast(DivineToll) && !DTHealing && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber) && NotChanneling && API.PlayerCanAttackTarget && API.TargetRange <= 31 && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                    {
                        API.CastSpell(DivineToll);
                        return;
                    }
                    if (API.CanCast(VanqusihersHammer) && PlayerCovenantSettings == "Necrolord" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !DPSHealthCheck && API.TargetRange <= 40 && API.TargetHealthPercent > 0 && API.TargetIsIncombat)
                    {
                        API.CastSpell(VanqusihersHammer);
                        return;
                    }
                    if (API.CanCast(SoTR) && API.PlayerCanAttackTarget && IsMelee && API.TargetHealthPercent > 0 && API.PlayerCurrentHolyPower > 4 && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || IsDPSOnly) && API.TargetIsIncombat)
                    {
                        API.CastSpell(SoTR);
                        return;
                    }
                    if (API.CanCast(SoTR) && SooRMacro && API.NearestEnnemyRange < 5 && API.PlayerCurrentHolyPower > 4 && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup || AshenWatch.IsRunning || IsDPSOnly))
                    {
                        API.CastSpell(SoTR + "Marco");
                        return;
                    }
                }
                    
                
            }
            
        }

        public override void CombatPulse()
        {
            if (API.CanCast(Fleshcraft) && PlayerCovenantSettings == "Necrolord" && API.PlayerHealthPercent <= FleshcraftPercentProc)
            {
                API.CastSpell(Fleshcraft);
                return;
            }
            if (API.PlayerItemCanUse(PhialofSerenity) && API.PlayerItemRemainingCD(PhialofSerenity) == 0 && API.PlayerHealthPercent <= PhialofSerenityLifePercent)
            {
                API.CastSpell(PhialofSerenity);
                return;
            }
            if (API.PlayerItemCanUse(SpiritualHealingPotion) && API.PlayerItemRemainingCD(SpiritualHealingPotion) == 0 && API.PlayerHealthPercent <= SpiritualHealingPotionLifePercent)
            {
                API.CastSpell(SpiritualHealingPotion);
                return;
            }

        }

        public override void OutOfCombatPulse()
        {
            if (API.PlayerIsMounted)
            {
                if (AutoAuraSwitch && API.CanCast(CrusaderAura) && Level >= 21 && !API.PlayerHasBuff(CrusaderAura))
                {
                    API.CastSpell(CrusaderAura);
                    return;
                }
            }
            else
            {
                if (AutoAuraSwitch && API.CanCast(DevotionAura) && Level >= 21 && !API.PlayerHasBuff(DevotionAura))
                {
                    API.CastSpell(DevotionAura);
                    return;
                }
            }
        }
    }

}
