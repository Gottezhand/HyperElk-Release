using System.Linq;
using System.Diagnostics;
using System;

namespace HyperElk.Core
{

    public class ClassicHolyPally : CombatRoutine
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
        private string PartySwap = "Target Swap";
        private string Fleshcraft = "Fleshcraft";
        private string DivineToll = "Divine Toll";
        private string DivineTollHealing = "Divine Toll Healing";
        private string VanqusihersHammer = "Vanquisher's Hammer";
        private string AshenHallow = "Ashen Hallow";
        private string LoTM = "Light of the Martyr";
        private string LoTMH = "Light of the Martyr Health";
        private string LoTMM = "Light of the Martyr Moving";
        private string AoE = "AOE";
        private string Party1 = "party1";
        private string Party2 = "party2";
        private string Party3 = "party3";
        private string Party4 = "party4";
        private string Player = "player";
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string AoEDPS = "AOEDPS";
        private string AoEDPSRaid = "AOEDPS Raid";
        private string AoEDPSH = "AOEDPS Health";
        private string AoEDPSHRaid = "AOEDPS Health Raid";
        private string AoERaid = "AOE Healing Raid";
        private string WOGMax = "Word of Glory Healing at Max HP";
        private string LODMax = "Light of Dawn Healing at Max HP";
        private string TargetHostile = "Target Last Hostile";
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
        private string DivineFavor = "Divine Favor";
        private string DivineIllumination = "Divine Illumination";
        private string CrusaderAura = "Crusader Aura";
        private string DevotionAura = "Devotion Aura";

        private string EatingBuff = "Food Buff";
        private string MageEatingBuff = "Mage Food Buff";
        private string DR = "Downrank";
        private string DR2 = "Downrank 2";
        private string Pri = "Priority";
        private string FLF = "Flash of Light First in Logic Order";
        private string HLF = "Holy Light First in Logic Order";
        //Talents


        //Stopwatchs / Int's / Strings
        private static readonly Stopwatch SwapWatch = new Stopwatch();
        private static readonly Stopwatch DPSWatch = new Stopwatch();
        private static readonly Stopwatch DispelWatch = new Stopwatch();


        int[] SwapSpeedList = new int[] { 1000, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000 };
        private int Level => API.PlayerLevel;
        int PlayerHealth => API.TargetHealthPercent;

        string[] DispellList = { "Chilled", "Frozen Binds", "Clinging Darkness", "Rasping Scream", "Heaving Retch", "Goresplatter", "Slime Injection", "Gripping Infection", "Debilitating Plague", "Burning Strain", "Blightbeak", "Corroded Claws", "Wasting Blight", "Hurl Spores", "Corrosive Gunk", "Cytotoxic Slash", "Venompiercer", "Wretched Phlegm", "Repulsive Visage", "Soul Split", "Anima Injection", "Bewildering Pollen", "Bramblethorn Entanglement", "Debilitating Poison", "Sinlight Visions", "Siphon Life", "Turn to Stone", "Stony Veins", "Cosmic Artifice", "Wailing Grief", "Shadow Word:  Pain", "Anguished Cries", "Wrack Soul", "Dark Lance", "Insidious Venom", "Charged Anima", "Lost Confidence", "Burden of Knowledge", "Internal Strife", "Forced Confession", "Insidious Venom 2", "Soul Corruption", "Genetic Alteration", "Withering Blight", "Decaying Blight", "Burst" };
        public string[] LegendaryList = new string[] { "None", "Shock Barrier", "Shadowbreaker, Dawn of the Sun" };
        public string[] PriorityList = new string[] {"Flash of Light First in Logic Order", "Holy Light First in Logic Order" };
        private string UseLeg => LegendaryList[CombatRoutine.GetPropertyInt("Legendary")];
        private string UsePriority => PriorityList[CombatRoutine.GetPropertyInt("Priority")];


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
        private static bool MouseoverHasDebuff(string buff)
        {
            return API.MouseoverHasDebuff(buff, false, false);
        }
        private static bool UnitHasDebuff(string buff, string unit)
        {
            return API.UnitHasDebuff(buff, unit, false, true);
        }

        private bool QuakingHelper => CombatRoutine.GetPropertyBool("QuakingHelper");
        private bool DTRange => API.UnitRangeCount(30) >= AoENumber;
        private bool LoDRangeCheck => UseLeg == "Shadowbreaker, Dawn of the Sun" ? API.UnitRangeCount(40) >= AoENumber : API.UnitRangeCount(15) >= AoENumber;
        private bool TrinketAoE => API.UnitBelowHealthPercent(TrinketLifePercent) >= AoENumber;
        private bool GlimmerTracking => API.PlayerIsInRaid ? API.unitBuffCountRaid(GlimmerofLight) <= 8 : API.UnitBuffCountParty(GlimmerofLight) <= 5;
        private bool BoLTracking => API.PlayerIsInRaid ? API.unitBuffCountRaid(BoL) < 1 : API.UnitBuffCountParty(BoL) < 1;
        private bool BoFTracking => API.PlayerIsInRaid ? API.unitBuffCountRaid(BoF) < 1 : API.UnitBuffCountParty(BoF) < 1;
        private bool DPSHealthCheck => API.PlayerIsInRaid ? API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber : API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber;
        private bool DPSMouseOver => API.MouseoverHealthPercent <= AoEDPSHLifePercent;
        private bool LoDAoE => API.UnitBelowHealthPercent(LoDLifePercent) >= AoENumber && LoDRangeCheck;
        private bool LoDMaxAoE => API.UnitBelowHealthPercent(LoDMaxLifePercent) >= AoENumber && LoDRangeCheck;
        private bool BoVAoE => API.UnitBelowHealthPercent(BoVLifePercent) >= AoENumber;
        private bool HPAoE => API.UnitBelowHealthPercent(HPLifePercent) >= AoENumber;
        private bool DTAoE => API.UnitBelowHealthPercent(DTLifePercent) >= AoENumber && DTRange;
        private bool AHAoE => API.UnitBelowHealthPercent(AHLifePercent) >= AoENumber;
        private bool AMAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(AMLifePercent) >= AoERaidNumber : API.UnitBelowHealthPercentParty(AMLifePercent) >= AoENumber;
        private bool AVAoE => API.PlayerIsInRaid ? API.UnitBelowHealthPercentRaid(AvengingWrathLifePrecent) >= AoERaidNumber : API.UnitBelowHealthPercentParty(AvengingWrathLifePrecent) >= AoENumber;
        private bool LHAoE => API.UnitBelowHealthPercent(LightsHammerLifePercent) >= AoENumber;
        private bool IsAutoSwap => API.ToggleIsEnabled("Auto Target");
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        private bool InRange => API.TargetRange <= 40;
        private bool InMoRange => API.MouseoverRange <= 40;
        private bool IsMelee => API.TargetRange < 5;

        private bool IsMoMelee = API.MouseoverRange < 5;
        private bool NotChanneling => !API.PlayerIsChanneling;
        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        private bool IsAoEHealing => API.ToggleIsEnabled("AoE Healing");
        private bool IsDPS => API.ToggleIsEnabled("DPS Auto Target");
        private bool DTHealing => CombatRoutine.GetPropertyBool(DivineTollHealing);
        private bool HSDPS => CombatRoutine.GetPropertyBool(HolyShockDPS);
        private bool BoSTank => CombatRoutine.GetPropertyBool(BoST);
        private bool LoHTank => CombatRoutine.GetPropertyBool(LoHT);
        private bool IsOOC => API.ToggleIsEnabled("OOC");
        private bool AutoAuraSwitch => CombatRoutine.GetPropertyBool("Aura Switch");
        private bool IsDispell => API.ToggleIsEnabled("Dispel");
        private bool IsNpC => API.ToggleIsEnabled("NPC");
        private bool IsNotEating => (!API.PlayerHasBuff(MageEatingBuff) || !API.PlayerHasBuff(EatingBuff));

        bool IsTrinkets1 => (UseTrinket1 == "With Cooldowns" && IsCooldowns || UseTrinket1 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket1 == "on AOE" && TrinketAoE) && !API.PlayerCanAttackTarget;
        bool IsTrinkets2 => (UseTrinket2 == "With Cooldowns" && IsCooldowns || UseTrinket2 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket2 == "on AOE" && TrinketAoE) && !API.PlayerCanAttackTarget;

        //Settings Percents
        private int DivineShieldLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineShield)];
        private int DivineFavorLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineFavor)];
        private int HolyShockLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyShock)];
        private int LightsHammerLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LightsHammer)];
        private int HolyLightLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLight)];
        private int FoLLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoL)];
        private int AvengingWrathLifePrecent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AvengingWrath)];
        private int DivineIllMana => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineIllumination)];
        private int HolyLightLifDRePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLight + DR)];
        private int FoLLifeDRPercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoL + DR)];
        private int HolyLightDR2LifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLight + DR2)];
        private int FoLLifeDR2Percent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoL + DR2)];

        private int BoSLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BoS)];
        private int FolILifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(FoLI)];
        private int HoLILifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HoLI)];
        private int LoHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoH)];
        private int BFLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BF)];
        private int LoDLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoD)];
        private int LoDMaxLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LODMax)];
        private int BoVLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(BoV)];
        private int HPLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyPrism)];
        private int PartySwapPercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PartySwap)];
        private int DTLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineToll)];
        private int AHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AshenHallow)];
        private int LoTMHealthPercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoTMH)];
        private int AMLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AuraMastery)];
        private int LoTMLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoTM)];
        private int LoTMMovingLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LoTMM)];
        private int AoEDPSHLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AoEDPSH)];
        private int AoEDPSHRaidLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AoEDPSHRaid)];
        private int FleshcraftPercentProc => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Fleshcraft)];
        private int AoENumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AoE)];
        private int AoERaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AoERaid)];
        private int AoEDPSNumber => API.numbPartyList[CombatRoutine.GetPropertyInt(AoEDPS)];
        private int AoEDPSRaidNumber => API.numbRaidList[CombatRoutine.GetPropertyInt(AoEDPSRaid)];
        private int PhialofSerenityLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];
        private int TrinketLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(Trinket)];
        private int TankHealth => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Tank Health")];
        private int UnitHealth => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Other Members Health")];
        private int PlayerHP => API.numbPercentListLong[CombatRoutine.GetPropertyInt("Player Health")];

        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Use Covenant")];
        private string UseAV => CDUsage[CombatRoutine.GetPropertyInt("Avenging Wrath Usage")];
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
        private bool HPOnBeaconGenerators => (API.LastSpellCastInGame == FoL || API.LastSpellCastInGame == HolyLight || API.LastSpellCastInGame == HolyShock);

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

        //Spell Check Bools

        private bool HolyShockCheck => API.CanCast(HolyShock) && API.TargetRange <= 20 && API.TargetHealthPercent <= HolyShockLifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && API.PlayerIsMoving;
        private bool HolyShockCheckMO => API.CanCast(HolyShock) && API.MouseoverRange <= 20 && IsMouseover && API.MouseoverHealthPercent <= HolyShockLifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyLightCheck => API.CanCast(HolyLight) && InRange && API.TargetHealthPercent <= HolyLightLifePercent && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool HolyLightDRCheck => API.CanCast(HolyLight) && InRange && API.TargetHealthPercent <= HolyLightLifDRePercent && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool HolyLightDR2Check => API.CanCast(HolyLight) && InRange && API.TargetHealthPercent <= HolyLightDR2LifePercent && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;

        private bool HolyLightCheckMO => API.CanCast(HolyLight) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= HolyLightLifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool HolyLightDRCheckMO => API.CanCast(HolyLight) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= HolyLightLifDRePercent && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;

        private bool HolyLightDR2CheckMO => API.CanCast(HolyLight) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= HolyLightDR2LifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;


        private bool FlashofLightCheck => API.CanCast(FoL) && InRange && API.TargetHealthPercent <= FoLLifePercent && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool FlashofLightDRCheck => API.CanCast(FoL) && InRange && API.TargetHealthPercent <= FoLLifeDRPercent && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool FlashofLightDR2Check => API.CanCast(FoL) && InRange && API.TargetHealthPercent <= FoLLifeDR2Percent && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;

        private bool FlashofLightCheckMO => API.CanCast(FoL) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= FoLLifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool FlashofLightDRCheckMO => API.CanCast(FoL) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= FoLLifeDRPercent && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool FlashofLightDR2CheckMO => API.CanCast(FoL) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= FoLLifeDR2Percent && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;


        private bool LoHCheck => API.CanCast(LoH) && InRange && API.TargetHealthPercent <= LoHLifePercent && API.TargetHealthPercent > 0 && !API.TargetHasDebuff(Forbearance, false, false) && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetIsIncombat;
        private bool LoHCheckMO => API.CanCast(LoH) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= LoHLifePercent && API.MouseoverHealthPercent > 0 && !API.MouseoverHasDebuff(Forbearance, false, false) && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.MouseoverIsIncombat;


        public override void Initialize()
        {

            CombatRoutine.Name = "Classic Holy Pally by Ryu";
            CombatRoutine.TBCRotation = true;

            API.WriteLog("Welcome to Holy Pally v1.1 by Ryu");
            API.WriteLog("For all ground spells, either use @Cursor or when it is time to place it, the Bot will pause until you've placed it. If you'd perfer to use your own logic for them, please place them on ignore in the spellbook.");
            API.WriteLog("Maunual targeting, Auto Tareting, or Mouseover Supported. You need to create /cast [@mouseover] xxxx where xxx is each of the spells that have MO in the bindings in order for Mouseover to work");
            API.WriteLog("The settings in the Targeting Section have been tested to work well. Change them at your risk and ONLY if you understand them.");

            //Buff
            // (Food) 327786 - 167152(Mage food)
            CombatRoutine.AddBuff(Forbearance, 25771);
            CombatRoutine.AddBuff(DevotionAura, 465);


            //Debuff
            CombatRoutine.AddDebuff(Forbearance, 25771);

            //Dispell Debuff



            //Spell
            CombatRoutine.AddSpell(HolyShock, 20473, "D2");
            CombatRoutine.AddSpell(FoL, 19750, "D4");
            CombatRoutine.AddSpell(HolyLight, 27136, "D3");
            CombatRoutine.AddSpell(Judgment, 275773, "D5");
            CombatRoutine.AddSpell(LoH, 633, "D8");
            CombatRoutine.AddSpell(DivineShield, 1020, "D0");
            CombatRoutine.AddSpell(DevotionAura, 465);
            CombatRoutine.AddSpell(DivineFavor, 20216);
            CombatRoutine.AddSpell(DivineIllumination, 31842);
            //Conduit

            //Item


            //Toggle
            CombatRoutine.AddToggle("Mouseover");
            //   CombatRoutine.AddToggle("NPC");
            CombatRoutine.AddToggle("Auto Target");
            //   CombatRoutine.AddToggle("DPS Auto Target");
          //  CombatRoutine.AddToggle("AoE Healing");
            CombatRoutine.AddToggle("OOC");
            //   CombatRoutine.AddToggle("Dispel");

            //Downrank
            CombatRoutine.AddMacro(HolyLight + DR);
            CombatRoutine.AddMacro(FoL + DR);
            CombatRoutine.AddMacro(HolyLight + DR2);
            CombatRoutine.AddMacro(FoL + DR2);
            //Mouseover
            CombatRoutine.AddMacro(HolyLight + "MO", "None");
            CombatRoutine.AddMacro(HolyLight + DR + "MO", "None");
            CombatRoutine.AddMacro(HolyLight + DR2 + "MO", "None");

            CombatRoutine.AddMacro(FoL + "MO", "None");
            CombatRoutine.AddMacro(FoL + DR + "MO", "None");
            CombatRoutine.AddMacro(FoL + DR2 + "MO", "None");

            CombatRoutine.AddMacro(HolyShock + "MO", "None");
            CombatRoutine.AddMacro(LoH + "MO", "None");
            CombatRoutine.AddMacro(Judgment + "MO");
            CombatRoutine.AddMacro(Trinket1);
            CombatRoutine.AddMacro(Trinket2);
            CombatRoutine.AddMacro(TargetHostile);
            CombatRoutine.AddMacro("Assist");
            CombatRoutine.AddMacro("Stopcast", "F10");
            CombatRoutine.AddMacro(Player);
            CombatRoutine.AddMacro(Party1);
            CombatRoutine.AddMacro(Party2);
            CombatRoutine.AddMacro(Party3);
            CombatRoutine.AddMacro(Party4);
            CombatRoutine.AddMacro("raid1");
            CombatRoutine.AddMacro("raid2");
            CombatRoutine.AddMacro("raid3");
            CombatRoutine.AddMacro("raid4");
            CombatRoutine.AddMacro("raid5");
            CombatRoutine.AddMacro("raid6");
            CombatRoutine.AddMacro("raid7");
            CombatRoutine.AddMacro("raid8");
            CombatRoutine.AddMacro("raid9");
            CombatRoutine.AddMacro("raid10");
            CombatRoutine.AddMacro("raid11");
            CombatRoutine.AddMacro("raid12");
            CombatRoutine.AddMacro("raid13");
            CombatRoutine.AddMacro("raid14");
            CombatRoutine.AddMacro("raid15");
            CombatRoutine.AddMacro("raid16");
            CombatRoutine.AddMacro("raid17");
            CombatRoutine.AddMacro("raid18");
            CombatRoutine.AddMacro("raid19");
            CombatRoutine.AddMacro("raid20");
            CombatRoutine.AddMacro("raid21");
            CombatRoutine.AddMacro("raid22");
            CombatRoutine.AddMacro("raid23");
            CombatRoutine.AddMacro("raid24");
            CombatRoutine.AddMacro("raid25");
            CombatRoutine.AddMacro("raid26");
            CombatRoutine.AddMacro("raid27");
            CombatRoutine.AddMacro("raid28");
            CombatRoutine.AddMacro("raid29");
            CombatRoutine.AddMacro("raid30");
            CombatRoutine.AddMacro("raid31");
            CombatRoutine.AddMacro("raid32");
            CombatRoutine.AddMacro("raid33");
            CombatRoutine.AddMacro("raid34");
            CombatRoutine.AddMacro("raid35");
            CombatRoutine.AddMacro("raid36");
            CombatRoutine.AddMacro("raid37");
            CombatRoutine.AddMacro("raid38");
            CombatRoutine.AddMacro("raid39");
            CombatRoutine.AddMacro("raid40");





            //Prop
            CombatRoutine.AddProp(DivineShield, DivineShield + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineShield + "is used, set to 0 to disable", "Defense", 20);


            AddProp("MouseoverInCombat", "Only Mouseover in combat", false, "Only Attack mouseover in combat to avoid stupid pulls", "Generic");




            CombatRoutine.AddProp(Pri, Pri, PriorityList, "Logic Listing for Healing", "Logic Listing for Healing", 0);
            //    CombatRoutine.AddProp("Tank Health", "Tank Health", API.numbPercentListLong, "Life percent at which " + "Tank Health" + "needs to be at to target during DPS Targeting", "Targeting", 90);
            CombatRoutine.AddProp("Other Members Health", "Other Members Health", API.numbPercentListLong, "Life percent at which " + "Other Members Health" + "needs to be at to targeted during DPS Targeting", "Targeting", 90);
            CombatRoutine.AddProp("Player Health", "Player Health", API.numbPercentListLong, "Life percent at which " + "Player Health" + "needs to be at to targeted above all else", "Targeting", 70);
            //     CombatRoutine.AddProp(AoEDPS, "Number of API.partyunits needed to be above DPS Health Percent to DPS in party ", API.numbPartyList, " API.partyunits above for DPS ", "Targeting", 5);
            //   CombatRoutine.AddProp(AoEDPSRaid, "Number of API.partyunits needed to be above DPS Health Percent to DPS in Raid ", API.numbRaidList, " API.partyunits above for DPS ", "Targeting", 7);
            // CombatRoutine.AddProp(AoEDPSH, "Life Percent for API.partyunits to be above for DPS and below to return back to Healing", API.numbPercentListLong, "Health percent at which DPS in party" + "is used,", "Targeting", 90);
            // CombatRoutine.AddProp(AoEDPSHRaid, "Life Percent for API.partyunits to be above for DPS and below to return back to Healing in raid", API.numbPercentListLong, "Health percent at which DPS" + "is used,", "Targeting", 75);


            CombatRoutine.AddProp(HolyShock, HolyShock + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyShock + "is used, set to 0 to disable", "Healing", 98);
            CombatRoutine.AddProp(HolyLight, HolyLight + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyLight + "is used, set to 0 to disable", "Healing", 95);
            CombatRoutine.AddProp(FoL, FoL + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoL + "is used, set to 0 to disable", "Healing", 65);
            CombatRoutine.AddProp(LoH, LoH + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoH + "is used, set to 0 to disable", "Healing", 15);
            CombatRoutine.AddProp(DivineFavor, DivineFavor + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineFavor + "is used, set to 0 to disable", "Healing", 45);
            CombatRoutine.AddProp(DivineIllumination, DivineIllumination + " Mana Percent", API.numbPercentListLong, "Mana percent at which" + DivineIllumination + "is used, set to 0 to disable", "Healing", 55);
            CombatRoutine.AddProp(HolyLight + DR, HolyLight + DR + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyLight + DR +  "is used, set to 0 to disable", "Downrank", 95);
            CombatRoutine.AddProp(FoL + DR, FoL + DR + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoL + DR + "is used, set to 0 to disable", "Downrank", 65);
            CombatRoutine.AddProp(HolyLight + DR2, HolyLight + DR2 + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyLight + DR2 +  "is used, set to 0 to disable", "Downrank", 95);
            CombatRoutine.AddProp(FoL + DR2, FoL + DR2 + " Life Percent", API.numbPercentListLong, "Life percent at which" + FoL + DR2 + "is used, set to 0 to disable", "Downrank", 65);


            CombatRoutine.AddProp(AoE, "Number of units for AoE Healing ", API.numbPartyList, " API.partyunits for AoE Healing", "AOE Healing", 3);
            CombatRoutine.AddProp(AoERaid, "Number of units for AoE Healing in raid ", API.numbRaidList, " API.partyunits for AoE Healing in raid", "AOE Healing", 6);
            CombatRoutine.AddProp(Trinket, Trinket + " Life Percent", API.numbPercentListLong, "Life percent at which " + "Trinkets" + " when AoE Healing Number of API.partyunits are met should be used, set to 0 to disable", "AOE Healing", 75);

            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket 1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket 2 be used", "Trinket", 0);
        }

        public override void Pulse()
        {
            var UnitLowestParty = API.UnitLowestParty();
            var UnitLowestRaid = API.UnitLowestRaid();
         //   var LowestTankUnitParty = API.UnitLowestParty(out string lowestTankParty);
         //   var LowestTankUnitRaid = API.UnitLowestRaid(out string lowestTankRaid);

            if (!API.PlayerIsMounted && !API.PlayerSpellonCursor && (IsOOC || API.PlayerIsInCombat))
            {
                //  #region Dispell
                //         if (IsDispell)
                //          {
                //              if (API.CanCast(Cleanse) && NotChanneling)
                //              {
                //                  for (int i = 0; i < DispellList.Length; i++)
                //                  {
                //                      if (TargetHasDispellAble(DispellList[i]) && (!TargetHasDispellAble("Frozen Binds") || TargetHasDispellAble("Frozen Binds") && API.TargetDebuffRemainingTime("Frozen Binds", false) <= 1000 || !TargetHasDispellAble("Bursting") || TargetHasDispellAble("Bursting") && API.TargetDebuffStacks("Bursting") >= 2))
                // {
                //     API.CastSpell(Cleanse);
                //     return;
                // }
                // }
                // }
                // if (API.CanCast(Cleanse) && IsMouseover && NotChanneling)
                // {
                //                    for (int i = 0; i < DispellList.Length; i++)
                //                   {
                //                       if (MouseouverHasDispellAble(DispellList[i]))
                //                       {
                //                           API.CastSpell(Cleanse + "MO");
                //                         return;
                //                    }
                //                }
                //           }
                //       }
                //   #endregion
                if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1 && NotChanneling && InRange)
                {
                    API.CastSpell("Trinket1");
                }
                if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2 && NotChanneling && InRange)
                {
                    API.CastSpell("Trinket2");
                }
                if (API.CanCast(DivineShield) && API.PlayerHealthPercent <= DivineShieldLifePercent && API.PlayerHealthPercent > 0 && (!API.PlayerHasDebuff(Forbearance, false, false) || !API.PlayerHasDebuff(Forbearance)) && API.PlayerIsInCombat)
                {
                    API.CastSpell(DivineShield);
                    return;
                }
                if (API.CanCast(DivineIllumination) && API.PlayerMana <= DivineIllMana && API.PlayerIsInCombat)
                {
                    API.CastSpell(DivineIllumination);
                    return;
                }
                //Healing
                if (LoHCheck)
                {
                    API.CastSpell(LoH);
                    return;
                }
                if (LoHCheckMO && !API.MacroIsIgnored(LoH + "MO"))
                {
                    API.CastSpell(LoH + "MO");
                    return;
                }
                if (API.CanCast(DivineFavor) && InRange && API.TargetHealthPercent <= DivineFavorLifePercent && API.PlayerIsInCombat && !API.PlayerCanAttackTarget)
                {
                    API.CastSpell(DivineFavor);
                    return;
                }
                if (HolyShockCheck)
                {
                    API.CastSpell(HolyShock);
                    return;
                }
                if (HolyShockCheckMO && !API.MacroIsIgnored(HolyShock + "MO"))
                {
                    API.CastSpell(HolyShock + "MO");
                    return;
                }
                if (UsePriority == FLF)
                {
                    if (FlashofLightCheck)
                    {
                        API.CastSpell(FoL);
                        return;
                    }
                    if (FlashofLightDR2Check)
                    {
                        API.CastSpell(FoL + DR2);
                        return;
                    }
                    if (FlashofLightDRCheck)
                    {
                        API.CastSpell(FoL + DR);
                        return;
                    }
                    if (FlashofLightCheckMO && !API.MacroIsIgnored(FoL + "MO"))
                    {
                        API.CastSpell(FoL + "MO");
                        return;
                    }
                    if (FlashofLightDR2CheckMO && !API.MacroIsIgnored(FoL + DR2 + "MO"))
                    {
                        API.CastSpell(FoL + DR2 + "MO");
                        return;
                    }
                    if (FlashofLightDRCheckMO && !API.MacroIsIgnored(FoL + DR + "MO"))
                    {
                        API.CastSpell(FoL + DR + "MO");
                        return;
                    }
                    if (HolyLightCheck)
                    {
                        API.CastSpell(HolyLight);
                        return;
                    }
                    if (HolyLightDR2Check)
                    {
                        API.CastSpell(HolyLight + DR2);
                        return;
                    }
                    if (HolyLightDRCheck)
                    {
                        API.CastSpell(HolyLight + DR);
                        return;
                    }
                    if (HolyLightCheckMO && !API.MacroIsIgnored(HolyLight + "MO"))
                    {
                        API.CastSpell(HolyLight + "MO");
                        return;
                    }
                    if (HolyLightDR2CheckMO && !API.MacroIsIgnored(HolyLight + DR2 + "MO"))
                    {
                        API.CastSpell(HolyLight + DR2 + "MO");
                        return;
                    }
                    if (HolyLightDRCheckMO && !API.MacroIsIgnored(HolyLight + DR + "MO"))
                    {
                        API.CastSpell(HolyLight + DR + "MO");
                        return;
                    }
                }
                if (UsePriority == HLF)
                {
                    if (HolyLightCheck)
                    {
                        API.CastSpell(HolyLight);
                        return;
                    }
                    if (HolyLightDR2Check)
                    {
                        API.CastSpell(HolyLight + DR2);
                        return;
                    }
                    if (HolyLightDRCheck)
                    {
                        API.CastSpell(HolyLight + DR);
                        return;
                    }
                    if (HolyLightCheckMO && !API.MacroIsIgnored(HolyLight + "MO"))
                    {
                        API.CastSpell(HolyLight + "MO");
                        return;
                    }
                    if (HolyLightDR2CheckMO && !API.MacroIsIgnored(HolyLight + DR2 + "MO"))
                    {
                        API.CastSpell(HolyLight + DR2 + "MO");
                        return;
                    }
                    if (HolyLightDRCheckMO && !API.MacroIsIgnored(HolyLight + DR + "MO"))
                    {
                        API.CastSpell(HolyLight + DR + "MO");
                        return;
                    }
                    if (FlashofLightCheck)
                    {
                        API.CastSpell(FoL);
                        return;
                    }
                    if (FlashofLightDR2Check)
                    {
                        API.CastSpell(FoL + DR2);
                        return;
                    }
                    if (FlashofLightDRCheck)
                    {
                        API.CastSpell(FoL + DR);
                        return;
                    }
                    if (FlashofLightCheckMO && !API.MacroIsIgnored(FoL + "MO"))
                    {
                        API.CastSpell(FoL + "MO");
                        return;
                    }
                    if (FlashofLightDR2CheckMO && !API.MacroIsIgnored(FoL + DR2 + "MO"))
                    {
                        API.CastSpell(FoL + DR2 + "MO");
                        return;
                    }
                    if (FlashofLightDRCheckMO && !API.MacroIsIgnored(FoL + DR + "MO"))
                    {
                        API.CastSpell(FoL + DR + "MO");
                        return;
                    }
                }
                //DPS
                //          if (API.PlayerIsInCombat)
                //        {
                //
                //      }
            }
            if (IsAutoSwap && (IsOOC || API.PlayerIsInCombat))
            {
                if (!API.PlayerIsInGroup && !API.PlayerIsInRaid)
                {
                    if (API.PlayerHealthPercent <= PlayerHP)
                    {
                        API.CastSpell(Player);
                        return;
                    }
                }
                if (API.PlayerIsInGroup && !API.PlayerIsInRaid)
                {
                    for (int i = 0; i < API.partyunits.Length; i++)
                    //     for (int j = 0; j < DispellList.Length; j++)
                    {
                        if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                        {
                            API.CastSpell(Player);
                            return;
                        }
                        if (UnitLowestParty != "none" && API.UnitHealthPercent(UnitLowestParty) <= 10 && API.TargetIsUnit() != UnitLowestParty)
                        {
                            API.CastSpell(UnitLowestParty);
                            return;
                        }
                        //       if (UnitHasDispellAble(DispellList[j], API.partyunits[i]) && IsDispell && !API.SpellISOnCooldown(NaturesCure) && API.TargetIsUnit() != API.partyunits[i])
                        //     {
                        //       API.CastSpell(API.partyunits[i]);
                        //     return;
                        // }
                        if (UnitLowestParty != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(UnitLowestParty) <= UnitHealth && API.TargetIsUnit() != UnitLowestParty)
                        {
                            API.CastSpell(UnitLowestParty);
                            SwapWatch.Restart();
                            return;
                        }
                    }
                }
                if (API.PlayerIsInRaid)
                {
                    for (int t = 0; t < API.raidunits.Length; t++)

                    {
                        if (UnitLowestRaid != "none" && API.UnitHealthPercent(UnitLowestRaid) <= 10 && API.TargetIsUnit() != UnitLowestRaid)
                        {
                            API.CastSpell(UnitLowestRaid);
                            return;
                        }
                        if (UnitLowestRaid != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.UnitHealthPercent(UnitLowestRaid) <= UnitHealth && API.TargetIsUnit() != UnitLowestRaid)
                        {
                            API.CastSpell(UnitLowestRaid);
                            SwapWatch.Restart();
                            return;
                        }
                    }
                }

            }
        }
    

        public override void CombatPulse()
        {


        }

        public override void OutOfCombatPulse()
        {

        }
    }
    
}




