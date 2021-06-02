using System.Linq;
using System.Diagnostics;
using System;

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

        private string CrusaderAura = "Crusader Aura";
        private string DevotionAura = "Devotion Aura";

        private string EatingBuff = "Food Buff";
        private string MageEatingBuff = "Mage Food Buff";

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
        private static readonly Stopwatch DPSWatch = new Stopwatch();
        private static readonly Stopwatch DispelWatch = new Stopwatch();


        int[] SwapSpeedList = new int[] { 1000, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000 };
        private int Level => API.PlayerLevel;
        int PlayerHealth => API.TargetHealthPercent;

        string[] DispellList = { "Chilled", "Frozen Binds", "Clinging Darkness", "Rasping Scream", "Heaving Retch", "Goresplatter", "Slime Injection", "Gripping Infection", "Debilitating Plague", "Burning Strain", "Blightbeak", "Corroded Claws", "Wasting Blight", "Hurl Spores", "Corrosive Gunk", "Cytotoxic Slash", "Venompiercer", "Wretched Phlegm", "Repulsive Visage", "Soul Split", "Anima Injection", "Bewildering Pollen", "Bramblethorn Entanglement", "Debilitating Poison", "Sinlight Visions", "Siphon Life", "Turn to Stone", "Stony Veins", "Cosmic Artifice", "Wailing Grief", "Shadow Word:  Pain", "Anguished Cries", "Wrack Soul", "Dark Lance", "Insidious Venom", "Charged Anima", "Lost Confidence", "Burden of Knowledge", "Internal Strife", "Forced Confession", "Insidious Venom 2", "Soul Corruption", "Genetic Alteration", "Withering Blight", "Decaying Blight", "Burst" };
        public string[] LegendaryList = new string[] { "None", "Shock Barrier", "Shadowbreaker, Dawn of the Sun" };
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
        private bool InRange => API.PlayerHasBuff(RuleofLaw) ? API.TargetRange <= 60 : API.TargetRange <= 40;
        private bool InMoRange => API.PlayerHasBuff(RuleofLaw) ? API.MouseoverRange <= 60 : API.MouseoverRange <= 40;
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

        bool IsTrinkets1 => (UseTrinket1 == "With Cooldowns" && IsCooldowns || UseTrinket1 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket1 == "on AOE" && TrinketAoE);
        bool IsTrinkets2 => (UseTrinket2 == "With Cooldowns" && IsCooldowns || UseTrinket2 == "On Cooldown" && API.TargetHealthPercent <= TrinketLifePercent || UseTrinket2 == "on AOE" && TrinketAoE);

        //Settings Percents
        private int DivineShieldLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineShield)];
        private int DivineProtectionLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(DivineProtection)];
        private int HolyShockLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyShock)];
        private int LightsHammerLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(LightsHammer)];
        private int HolyLightLifePercent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(HolyLight)];
        private int AvengingWrathLifePrecent => API.numbPercentListLong[CombatRoutine.GetPropertyInt(AvengingWrath)];
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
        private bool LoTMCheck => API.CanCast(LoTM) && InRange && !API.PlayerCanAttackTarget && !API.PlayerIsTargetTarget && (API.PlayerIsMoving && API.TargetHealthPercent <= LoTMMovingLifePercent || API.TargetHealthPercent <= LoTMLifePercent) && API.PlayerHealthPercent >= LoTMHealthPercent && API.TargetHealthPercent > 0;
        private bool LoTMCheckMO => API.CanCast(LoTM) && IsMouseover && API.MouseoverIsUnit() != "player" && InMoRange && !API.PlayerCanAttackMouseover && !API.PlayerIsTargetTarget && (API.PlayerIsMoving && API.MouseoverHealthPercent <= LoTMMovingLifePercent || API.MouseoverHealthPercent <= LoTMLifePercent) && API.PlayerHealthPercent >= LoTMHealthPercent && API.MouseoverHealthPercent > 0;
        private bool HolyShockCheck => API.CanCast(HolyShock) && InRange && API.TargetHealthPercent <= HolyShockLifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && API.PlayerCurrentHolyPower <= 4 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyShockCheckMO => API.CanCast(HolyShock) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= HolyShockLifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && API.PlayerCurrentHolyPower <= 4 && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyLightCheck => API.CanCast(HolyLight) && InRange && ((API.TargetHasBuff(BoF) || API.TargetHasBuff(BoL)) && API.TargetHealthPercent <= HolyLightBeaconLifePercent || API.TargetHealthPercent <= HolyLightLifePercent) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool HolyLightCheckMO => API.CanCast(HolyLight) && InMoRange && IsMouseover && ((API.MouseoverHasBuff(BoF) || API.MouseoverHasBuff(BoL)) && API.MouseoverHealthPercent <= HolyLightBeaconLifePercent || API.MouseoverHealthPercent <= HolyLightLifePercent) && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool HolyLightInfusionCheck => API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && InRange && ((API.TargetHasBuff(BoF) || API.TargetHasBuff(BoL)) && API.TargetHealthPercent <= HolyLightBeaconILifePercent || API.TargetHealthPercent <= HoLILifePercent) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool HolyLightInfusionCheckMO => API.CanCast(HolyLight) && API.PlayerHasBuff(Infusion) && IsMouseover && InMoRange && ((API.MouseoverHasBuff(BoF) || API.MouseoverHasBuff(BoL)) && API.MouseoverHealthPercent <= HolyLightBeaconILifePercent || API.MouseoverHealthPercent <= HoLILifePercent) && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool FlashofLightCheck => API.CanCast(FoL) && InRange && ((API.TargetHasBuff(BoF) || API.TargetHasBuff(BoL)) && API.TargetHealthPercent <= FoLBeaconLifePercent || API.TargetHealthPercent <= FoLLifePercent) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool FlashofLightCheckMO => API.CanCast(FoL) && InMoRange && IsMouseover && ((API.MouseoverHasBuff(BoF) || API.MouseoverHasBuff(BoL)) && API.MouseoverHealthPercent <= FoLBeaconLifePercent || API.MouseoverHealthPercent <= FoLLifePercent) && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool FlashofLightInfusionCheck => API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && InRange && ((API.TargetHasBuff(BoF) || API.TargetHasBuff(BoL)) && API.TargetHealthPercent <= FoLBeaconILifePercent || API.TargetHealthPercent <= FolILifePercent) && API.TargetHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackTarget;
        private bool FlashofLightInfusionCheckMO => API.CanCast(FoL) && API.PlayerHasBuff(Infusion) && IsMouseover && InMoRange && ((API.MouseoverHasBuff(BoF) || API.MouseoverHasBuff(BoL)) && API.MouseoverHealthPercent <= FoLBeaconILifePercent || API.MouseoverHealthPercent <= FolILifePercent) && API.MouseoverHealthPercent > 0 && !API.PlayerIsMoving && !API.PlayerCanAttackMouseover;
        private bool WoGCheck => API.CanCast(WoG) && InRange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.TargetHealthPercent <= WoGLifePercent || API.TargetHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5) && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGOverLoDCheck => API.CanCast(WoG) && InRange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && API.TargetHealthPercent <= WoGOverLoDifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGOverLoDCheckMO => API.CanCast(WoG) && InMoRange && IsMouseover && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && API.MouseoverHealthPercent <= WoGOverLoDifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGCheckMO => API.CanCast(WoG) && InMoRange && IsMouseover && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.MouseoverHealthPercent <= WoGLifePercent || API.MouseoverHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5) && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGTankCheck => WoGTanking && API.CanCast(WoG) && API.TargetRoleSpec == API.TankRole && InRange && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.TargetHealthPercent <= WoGTankifePercent || API.TargetHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5) && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool WoGTankCheckMO => WoGTanking && API.CanCast(WoG) && API.MouseoverRoleSpec == API.TankRole && InMoRange && IsMouseover && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (API.MouseoverHealthPercent <= WoGTankifePercent || API.MouseoverHealthPercent <= WoGMaxLifePercent && API.PlayerCurrentHolyPower == 5) && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BFCheck => API.CanCast(BF) && BestowFaith && InRange && API.TargetHealthPercent <= BFLifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BFCheckMO => API.CanCast(BF) && BestowFaith && InMoRange && IsMouseover && API.MouseoverHealthPercent <= BFLifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool LoDCheck => IsAoEHealing && API.CanCast(LoD) && (IsMouseover || !IsMouseover) && (API.PlayerHasBuff(DivinePurpose) || UpcomingHolyPower >= 3) && (LoDAoE || LoDMaxAoE && API.PlayerCurrentHolyPower == 5) && (API.TargetHealthPercent > 0 || API.MouseoverHealthPercent > 0) && (API.PlayerCanAttackTarget || !API.PlayerCanAttackTarget || API.PlayerCanAttackMouseover || !API.PlayerCanAttackMouseover) && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BoVCheck => API.CanCast(BoV) && BeaconofVirtue && InRange && BoVAoE && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool BoVCheckMO => API.CanCast(BoV) && BeaconofVirtue && InMoRange && IsMouseover && BoVAoE && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool DTCheck => API.CanCast(DivineToll) && DTAoE && (API.PlayerCurrentHolyPower == 0 || API.UnitBelowHealthPercent(35) >= AoENumber) && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && DTAoE) && NotChanneling && !API.PlayerCanAttackTarget && API.TargetHealthPercent > 0 && InRange && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool DTCheckMO => API.CanCast(DivineToll) && IsMouseover && DTAoE && (API.PlayerCurrentHolyPower == 0 || API.UnitBelowHealthPercent(35) >= AoENumber) && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && DTAoE) && NotChanneling && !API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0 && InMoRange && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyPrismCheck => API.CanCast(HolyPrism) && HolyPrismT && InRange && HPAoE && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool HolyPrismCheckMO => API.CanCast(HolyPrism) && HolyPrismT && InMoRange && IsMouseover && HPAoE && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving);
        private bool LoHCheck => API.CanCast(LoH) && InRange && API.TargetHealthPercent <= LoHLifePercent && API.TargetHealthPercent > 0 && (LoHTank && API.TargetRoleSpec == API.TankRole || !LoHTank) && !API.TargetHasDebuff(Forbearance, false, false) && !API.PlayerCanAttackTarget && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetIsIncombat;
        private bool LoHCheckMO => API.CanCast(LoH) && InMoRange && IsMouseover && API.MouseoverHealthPercent <= LoHLifePercent && API.MouseoverHealthPercent > 0 && (LoHTank && API.MouseoverRoleSpec == API.TankRole || !LoHTank) && !API.MouseoverHasDebuff(Forbearance, false, false) && !API.PlayerCanAttackMouseover && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.MouseoverIsIncombat;
        private bool BoSCheck => API.CanCast(BoS) && InRange && API.TargetHealthPercent <= BoSLifePercent && API.TargetHealthPercent > 0 && !API.PlayerCanAttackTarget && !API.PlayerIsTargetTarget && (BoSTank && API.TargetRoleSpec == API.TankRole || !BoSTank) && (API.PlayerIsMoving || !API.PlayerIsMoving) && API.TargetIsIncombat;
        private bool BoSCheckMO => API.CanCast(BoS) && InMoRange && IsMouseover && API.MouseoverIsUnit() != "player" && API.MouseoverHealthPercent <= BoSLifePercent && API.MouseoverHealthPercent > 0 && !API.PlayerCanAttackTarget && !API.PlayerIsTargetTarget & (BoSTank && API.MouseoverRoleSpec == API.TankRole || !BoSTank) && API.MouseoverIsIncombat;
        private bool AuraMasteryCheck => API.CanCast(AuraMastery) && InRange && (IsMouseover || !IsMouseover) && AMAoE && (API.TargetHealthPercent > 0 || API.MouseoverHealthPercent > 0) && (API.PlayerCanAttackTarget || !API.PlayerCanAttackTarget || API.PlayerCanAttackMouseover || !API.PlayerCanAttackMouseover) && (API.PlayerIsMoving || !API.PlayerIsMoving);

        public override void Initialize()
        {

            CombatRoutine.Name = "Holy Pally by Ryu";
            API.WriteLog("Welcome to Holy Pally v1.6 by Ryu");
            API.WriteLog("For the Quaking helper you just need to create an ingame macro with /stopcasting and bind it under the Macros Tab in Elk :-)");
            API.WriteLog("If you wish to use the Crusader Strike Marco, PLEASE SEE DISCORD FOR THE MACRO. IT IS IN THE PINS");
            API.WriteLog("All Talents expect PVP Talents and Row 3 talents are supported. All Cooldowns are associated with Cooldown toggle.");
            API.WriteLog("For all ground spells, either use @Cursor or when it is time to place it, the Bot will pause until you've placed it. If you'd perfer to use your own logic for them, please place them on ignore in the spellbook.");
            API.WriteLog("Light of Dawn will not work unless you have AoE Healing Toggled on.");
            API.WriteLog("Maunual targeting, Auto Tareting, or Mouseover Supported. You need to create /cast [@mouseover] xxxx where xxx is each of the spells that have MO in the bindings in order for Mouseover to work");
            API.WriteLog("Night Fae Cov is not supported. You can create a /xxx break marco to use those abilties when you would like at this time.");
            API.WriteLog("If you wish to use Auto Target, please set your WoW keybinds in the keybinds => Targeting for Self, Party, and Assist Target and then match them to the Macro's's in the spell book. Enable it the Toggles. You must at least have a target for it to swap, friendly or enemy. UNDER TESTING : It can swap back to an enemy, but YOU WILL NEED TO ASSIGN YOUR ASSIST TARGET KEY IT WILL NOT WORK IF YOU DONT DO THIS. If you DO NOT want it to do target enemy swapping, please IGNORE Assist Macro in the Spellbook. This works for both raid and party, however, you must set up the binds. Please watch video in the Discord");
            API.WriteLog("The settings in the Targeting Section have been tested to work well. Change them at your risk and ONLY if you understand them.");
            API.WriteLog("IF YOU USE THE NPC TOGGLE, IT WILL CHANGE THE ROTATION THE NPC HEALING LOGIC (For Shade and Sun King) IT WILL IGNORE ALL OTHER THINGS EXPECT COOLDOWNS, PLEASE TURN IT OFF ONCE YOU HAVE FINISHED HEALING THE NPC");

            //Buff
           // (Food) 327786 - 167152(Mage food)
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

            //Debuff
            CombatRoutine.AddDebuff(Forbearance, 25771);
            CombatRoutine.AddDebuff(Cons, 26573);
            CombatRoutine.AddDebuff(Quake, 240447);

            //Dispell Debuff
            CombatRoutine.AddDebuff("Chilled", 328664);
            CombatRoutine.AddDebuff("Frozen Binds", 320788);
            CombatRoutine.AddDebuff("Clinging Darkness", 323347);
            CombatRoutine.AddDebuff("Rasping Scream", 324293);
            CombatRoutine.AddDebuff("Heaving Retch", 320596);
            CombatRoutine.AddDebuff("Goresplatter", 338353);
            CombatRoutine.AddDebuff("Slime Injection", 329110);
            CombatRoutine.AddDebuff("Gripping Infection", 328180);
            CombatRoutine.AddDebuff("Debilitating Plague", 324652);
            CombatRoutine.AddDebuff("Burning Strain", 322358);
            CombatRoutine.AddDebuff("Blightbeak", 327882);
            CombatRoutine.AddDebuff("Corroded Claws", 320512);
            CombatRoutine.AddDebuff("Wasting Blight", 320542);
            CombatRoutine.AddDebuff("Hurl Spores", 328002);
            CombatRoutine.AddDebuff("Corrosive Gunk", 319070);
            CombatRoutine.AddDebuff("Cytotoxic Slash", 325552);
            CombatRoutine.AddDebuff("Venompiercer", 328395);
            CombatRoutine.AddDebuff("Wretched Phlegm", 334926);
            CombatRoutine.AddDebuff("Repulsive Visage", 328756);
            CombatRoutine.AddDebuff("Soul Split", 322557);
            CombatRoutine.AddDebuff("Anima Injection", 325224);
            CombatRoutine.AddDebuff("Bewildering Pollen", 321968);
            CombatRoutine.AddDebuff("Bramblethorn Entanglement", 324859);
            CombatRoutine.AddDebuff("Debilitating Poison", 326092);
            CombatRoutine.AddDebuff("Sinlight Visions", 339237);
            CombatRoutine.AddDebuff("Siphon Life", 325701);
            CombatRoutine.AddDebuff("Turn to Stone", 326607);
            CombatRoutine.AddDebuff("Stony Veins", 326632);
            CombatRoutine.AddDebuff("Cosmic Artifice", 325725);
            CombatRoutine.AddDebuff("Wailing Grief", 340026);
            CombatRoutine.AddDebuff("Shadow Word:  Pain", 332707);
            CombatRoutine.AddDebuff("Anguished Cries", 325885);
            CombatRoutine.AddDebuff("Wrack Soul", 321038);
            CombatRoutine.AddDebuff("Dark Lance", 327481);
            CombatRoutine.AddDebuff("Insidious Venom", 323636);
            CombatRoutine.AddDebuff("Charged Anima", 338731);
            CombatRoutine.AddDebuff("Lost Confidence", 322818);
            CombatRoutine.AddDebuff("Burden of Knowledge", 317963);
            CombatRoutine.AddDebuff("Internal Strife", 327648);
            CombatRoutine.AddDebuff("Forced Confession", 328331);
            CombatRoutine.AddDebuff("Insidious Venom 2", 317661);
            CombatRoutine.AddDebuff("Soul Corruption", 333708);
            CombatRoutine.AddDebuff("Genetic Alteration", 320248);
            CombatRoutine.AddDebuff("Withering Blight", 341949);
            CombatRoutine.AddDebuff("Decaying Blight", 330700);
            CombatRoutine.AddDebuff("Gluttonous Miasma", 329298);
            CombatRoutine.AddDebuff("Burst", 240443);


            //Spell
            CombatRoutine.AddSpell(HolyShock, 20473, "D2");
            CombatRoutine.AddSpell(FoL, 19750, "D4");
            CombatRoutine.AddSpell(HolyLight, 82326, "D3");
            CombatRoutine.AddSpell(CrusaderStrike, 35395, "D6");
            CombatRoutine.AddSpell(Judgment, 275773, "D5");
            CombatRoutine.AddSpell(LoD, 85222, "R");
            CombatRoutine.AddSpell(AvengingWrath, 31884, "F2");
            CombatRoutine.AddSpell(HolyAvenger, 105809, "F8");
            CombatRoutine.AddSpell(AuraMastery, 31821, "T");
            CombatRoutine.AddSpell(BoS, 6940, "F1");
            CombatRoutine.AddSpell(LoH, 633, "D8");
            CombatRoutine.AddSpell(BoV, 200025, "None");
            CombatRoutine.AddSpell(DivineShield, 642, "D0");
            CombatRoutine.AddSpell(BF, 223306, "None");
            CombatRoutine.AddSpell(HammerofJustice, 853, "F");
            CombatRoutine.AddSpell(Cons, 26573, "None");
            CombatRoutine.AddSpell(DivineProtection, 498, "None");
            CombatRoutine.AddSpell(HammerofWrath, 24275, "None");
            CombatRoutine.AddSpell(Seraphim, 152262, "None");
            CombatRoutine.AddSpell(AvengingCrusader, 216331, "F2");
            CombatRoutine.AddSpell(HolyPrism, 114165, "None");
            CombatRoutine.AddSpell(WoG, 85673, "None");
            CombatRoutine.AddSpell(LoTM, 183998);
            CombatRoutine.AddSpell(CrusaderAura, 32223);
            CombatRoutine.AddSpell(DevotionAura, 465);
            CombatRoutine.AddSpell(Fleshcraft, 324631);
            CombatRoutine.AddSpell(DivineToll, 304971);
            CombatRoutine.AddSpell(VanqusihersHammer, 328204);
            CombatRoutine.AddSpell(AshenHallow, 316958);
            CombatRoutine.AddSpell(RuleofLaw, 214202);
            CombatRoutine.AddSpell(LightsHammer, 114158);
            CombatRoutine.AddSpell(SoTR, 53600);
            CombatRoutine.AddSpell(BoL, 53563);
            CombatRoutine.AddSpell(BoF, 156910);
            CombatRoutine.AddSpell(Cleanse, 4987);

            //Conduit
            CombatRoutine.AddConduit("Golden Path");

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Toggle
            CombatRoutine.AddToggle("Mouseover");
            CombatRoutine.AddToggle("NPC");
            CombatRoutine.AddToggle("Auto Target");
            CombatRoutine.AddToggle("DPS Auto Target");
            CombatRoutine.AddToggle("AoE Healing");
            CombatRoutine.AddToggle("OOC");
            CombatRoutine.AddToggle("Dispel");

            //Mouseover
            CombatRoutine.AddMacro(HolyLight + "MO", "None");
            CombatRoutine.AddMacro(FoL + "MO", "None");
            CombatRoutine.AddMacro(HolyShock + "MO", "None");
            CombatRoutine.AddMacro(BoS + "MO", "None");
            CombatRoutine.AddMacro(LoH + "MO", "None");
            CombatRoutine.AddMacro(BoV + "MO", "None");
            CombatRoutine.AddMacro(HolyPrism + "MO", "None");
            CombatRoutine.AddMacro(LoD + "MO", "R");
            CombatRoutine.AddMacro(CrusaderStrike + "MO");
            CombatRoutine.AddMacro(Judgment + "MO");
            CombatRoutine.AddMacro(HammerofWrath + "MO");
            CombatRoutine.AddMacro(DivineToll + "MO");
            CombatRoutine.AddMacro(LoTM + "MO");
            CombatRoutine.AddMacro(WoG + "MO");
            CombatRoutine.AddMacro(BF + "MO");
            CombatRoutine.AddMacro(SoTR + "MO");
            CombatRoutine.AddMacro(Cleanse + "MO");
            CombatRoutine.AddMacro(BoL + "MO");
            CombatRoutine.AddMacro(BoF + "MO");
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
            CombatRoutine.AddMacro("none", "F1");





            //Prop
            CombatRoutine.AddProp(DivineShield, DivineShield + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineShield + "is used, set to 0 to disable", "Defense", 20);
            CombatRoutine.AddProp(DivineProtection, DivineProtection + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineProtection + "is used, set to 0 to disable", "Defense", 70);
            CombatRoutine.AddProp(Fleshcraft, "Fleshcraft", API.numbPercentListLong, "Life percent at which " + Fleshcraft + " is used, set to 0 to disable set 100 to use it everytime", "Defense", 0);
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", API.numbPercentListLong, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 45);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", API.numbPercentListLong, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);

            CombatRoutine.AddProp("Legendary", "Select your Legendary", LegendaryList, "Select Your Legendary", "Legendary");

            CombatRoutine.AddProp("Use Covenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + "On Cooldown, with Cooldowns, On AOE, Not Used", "Cooldowns", 0);
            CombatRoutine.AddProp(AvengingWrath, AvengingWrath + " Life Percent", API.numbPercentListLong, "Life percent at which" + AvengingWrath + "is used when AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "Cooldowns", 45);
            CombatRoutine.AddProp("Avenging Wrath Usage", AvengingWrath + "Usage ", CDUsage, "Use " + AvengingWrath + "On Cooldown with AOE Logic for Healing, With Cooldowns only( you control) or not used at all", "Cooldowns", 1);

            AddProp("MouseoverInCombat", "Only Mouseover in combat", false, "Only Attack mouseover in combat to avoid stupid pulls", "Generic");
            CombatRoutine.AddProp("QuakingHelper", "Quaking Helper", false, "Will cancel casts on Quaking", "Generic");
            CombatRoutine.AddProp("Aura Switch", "Auto Aura Switch", false, "Auto Switch Aura between Crusader Aura|Devotion Aura", "Generic");

            CombatRoutine.AddProp("Crusader Strike Macro", "Crusader Strike Macro", false, "Use CS via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Judgement Macro", "Judgement Macro", false, "Use Judgement via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Hammer of Wrath Macro", "Hammer of Wrath Macro", false, "Use Hammer of Wrath via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");
            CombatRoutine.AddProp("Shield of the Righteous Macro", "Shield of the Righteous Macro", false, "Use SooR via Marco function w/o targeting - See PINS in Discord for more Info", "Marco");




            CombatRoutine.AddProp("Tank Health", "Tank Health", API.numbPercentListLong, "Life percent at which " + "Tank Health" + "needs to be at to target during DPS Targeting", "Targeting", 90);
            CombatRoutine.AddProp("Other Members Health", "Other Members Health", API.numbPercentListLong, "Life percent at which " + "Other Members Health" + "needs to be at to targeted during DPS Targeting", "Targeting", 90);
            CombatRoutine.AddProp("Player Health", "Player Health", API.numbPercentListLong, "Life percent at which " + "Player Health" + "needs to be at to targeted above all else", "Targeting", 70);
            CombatRoutine.AddProp(AoEDPS, "Number of API.partyunits needed to be above DPS Health Percent to DPS in party ", API.numbPartyList, " API.partyunits above for DPS ", "Targeting", 5);
            CombatRoutine.AddProp(AoEDPSRaid, "Number of API.partyunits needed to be above DPS Health Percent to DPS in Raid ", API.numbRaidList, " API.partyunits above for DPS ", "Targeting", 7);
            CombatRoutine.AddProp(AoEDPSH, "Life Percent for API.partyunits to be above for DPS and below to return back to Healing", API.numbPercentListLong, "Health percent at which DPS in party" + "is used,", "Targeting", 90);
            CombatRoutine.AddProp(AoEDPSHRaid, "Life Percent for API.partyunits to be above for DPS and below to return back to Healing in raid", API.numbPercentListLong, "Health percent at which DPS" + "is used,", "Targeting", 75);


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


            CombatRoutine.AddProp(LoD, LoD + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoD + "is used when AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "AOE Healing", 80);
            CombatRoutine.AddProp(LODMax, LODMax + " Life Percent", API.numbPercentListLong, "Life percent at which" + LoD + "is used AoE Healing Number of API.partyunits are at life percent and you have max holy power, set to 0 to disable", "AOE Healing", 80);
            CombatRoutine.AddProp(BoV, BoV + " Life Percent", API.numbPercentListLong, "Life percent at which" + BoV + "is used AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "AOE Healing", 80);
            CombatRoutine.AddProp(LightsHammer, LightsHammer + " Life Percent", API.numbPercentListLong, "Life percent at which" + LightsHammer + "is used when AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "AOE Healing", 75);
            CombatRoutine.AddProp(AshenHallow, AshenHallow + " Life Percent", API.numbPercentListLong, "Life percent at which" + AshenHallow + "is used when AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "AOE Healing", 65);
            CombatRoutine.AddProp(AuraMastery, AuraMastery + " Life Percent", API.numbPercentListLong, "Life percent at which" + AuraMastery + "is used, set to 0 to disable", "AOE Healing", 30);
            CombatRoutine.AddProp(HolyPrism, HolyPrism + " Life Percent", API.numbPercentListLong, "Life percent at which" + HolyPrism + "is used when AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "AOE Healing", 90);
            CombatRoutine.AddProp(DivineToll, DivineToll + " Life Percent", API.numbPercentListLong, "Life percent at which" + DivineToll + "is used when AoE Healing Number of API.partyunits are at life percent, set to 0 to disable", "AOE Healing", 75);
            CombatRoutine.AddProp(DivineTollHealing, DivineTollHealing, true, "If Divine Toll should be on Healing, if for both, change to false, set to true by default for healing", "AOE Healing");
            CombatRoutine.AddProp(AoE, "Number of API.partyunits for AoE Healing ", API.numbPartyList, " API.partyunits for AoE Healing", "AOE Healing", 3);
            CombatRoutine.AddProp(AoERaid, "Number of API.partyunits for AoE Healing in raid ", API.numbRaidList, " API.partyunits for AoE Healing in raid", "AOE Healing", 6);
            CombatRoutine.AddProp(Trinket, Trinket + " Life Percent", API.numbPercentListLong, "Life percent at which " + "Trinkets" + " when AoE Healing Number of API.partyunits are met should be used, set to 0 to disable", "AOE Healing", 75);

            CombatRoutine.AddProp("Trinket1", "Trinket1 usage", CDUsageWithAOE, "When should trinket 1 be used", "Trinket", 0);
            CombatRoutine.AddProp("Trinket2", "Trinket2 usage", CDUsageWithAOE, "When should trinket 2 be used", "Trinket", 0);
        }

        public override void Pulse()
        {
            var UnitLowest = API.UnitLowest();
            var LowestTankUnit = API.UnitLowest(out string lowestTank);

            if (!API.PlayerIsMounted && !API.PlayerSpellonCursor && (IsOOC || API.PlayerIsInCombat) && IsNotEating && (!TargetHasDebuff("Gluttonous Miasma") || IsMouseover && !MouseoverHasDebuff("Gluttonous Miasma")))
            {
                if (API.PlayerCurrentCastTimeRemaining > 40 && QuakingHelper && Quaking)
                {
                    API.CastSpell("Stopcast");
                    API.WriteLog("Debuff Time Remaining for Quake : " + API.PlayerDebuffRemainingTime(Quake));
                    return;
                }
                if (API.CanCast(AvengingWrath) && !API.PlayerHasBuff(AvengingWrath) && InRange && (IsCooldowns && UseAV == "With Cooldowns" || UseAV == "On Cooldown" && AVAoE))
                {
                    API.CastSpell(AvengingWrath);
                    return;
                }
                if (API.CanCast(AvengingCrusader) && AvengingCrusaderT && !API.PlayerHasBuff(AvengingCrusader) && InRange && IsCooldowns)
                {
                    API.CastSpell(AvengingCrusader);
                    return;
                }
                if (API.CanCast(HolyAvenger) && HolyAvengerT && InRange && IsCooldowns)
                {
                    API.CastSpell(HolyAvenger);
                    return;
                }
                if (API.CanCast(Seraphim) && SeraphimT && InRange && IsCooldowns && API.PlayerCurrentHolyPower >= 3)
                {
                    API.CastSpell(Seraphim);
                    return;
                }
                if (IsNpC && API.PlayerHasBuff(HolyAvenger))
                {
                    if (API.CanCast(WoG) && API.PlayerCurrentHolyPower > 2 && !API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(WoG);
                        return;
                    }
                    if (API.CanCast(WoG) && API.PlayerCurrentHolyPower > 2 && !API.PlayerCanAttackMouseover && IsMouseover)
                    {
                        API.CastSpell(WoG + "MO");
                        return;
                    }
                    if (API.CanCast(HolyShock) && !API.PlayerIsCasting(true) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(HolyShock);
                        return;
                    }
                    if (API.CanCast(HolyShock) && !API.PlayerIsCasting(true) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackMouseover && IsMouseover)
                    {
                        API.CastSpell(HolyShock + "MO");
                        return;
                    }
                    if (API.CanCast(HolyLight) && !API.PlayerIsCasting(true) && API.PlayerCurrentHolyPower < 3 && (API.PlayerLastSpell != HolyLight || API.LastSpellCastInGame != HolyLight) && !API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(HolyLight);
                        return;
                    }
                    if (API.CanCast(HolyLight) && !API.PlayerIsCasting(true) && API.PlayerCurrentHolyPower < 3 && (API.PlayerLastSpell != HolyLight || API.LastSpellCastInGame != HolyLight) && !API.PlayerCanAttackMouseover && IsMouseover)
                    {
                        API.CastSpell(HolyLight + "MO");
                        return;
                    }
                }
                if (IsNpC)
                {
                    if (API.CanCast(WoG) && API.PlayerCurrentHolyPower > 2 && !API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(WoG);
                        return;
                    }
                    if (API.CanCast(WoG) && API.PlayerCurrentHolyPower > 2 && !API.PlayerCanAttackMouseover && IsMouseover)
                    {
                        API.CastSpell(WoG + "MO");
                        return;
                    }
                    if (API.CanCast(HolyShock) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(HolyShock);
                        return;
                    }
                    if (API.CanCast(HolyShock) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackMouseover && IsMouseover)
                    {
                        API.CastSpell(HolyShock + "MO");
                        return;
                    }
                    if (API.CanCast(HolyLight) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackTarget)
                    {
                        API.CastSpell(HolyLight);
                        return;
                    }
                    if (API.CanCast(HolyLight) && API.PlayerCurrentHolyPower < 3 && !API.PlayerCanAttackMouseover && IsMouseover)
                    {
                        API.CastSpell(HolyLight + "MO");
                        return;
                    }
                }
                if (API.CanCast(Cons) && API.PlayerIsConduitSelected("Golden Path") && API.PlayerHealthPercent < PlayerHealth && IsMelee && !API.TargetHasDebuff(Cons) && !API.PlayerIsMoving && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(Cons);
                    return;
                }
                #region Dispell
                if (IsDispell)
                {
                    if (API.CanCast(Cleanse) && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (TargetHasDispellAble(DispellList[i]) && (!TargetHasDispellAble("Frozen Binds") || TargetHasDispellAble("Frozen Binds") && API.TargetDebuffRemainingTime("Frozen Binds", false) <= 1000 || !TargetHasDispellAble("Bursting") || TargetHasDispellAble("Bursting") && API.TargetDebuffStacks("Bursting") >= 2))
                            {
                                API.CastSpell(Cleanse);
                                return;
                            }
                        }
                    }
                    if (API.CanCast(Cleanse) && IsMouseover && NotChanneling)
                    {
                        for (int i = 0; i < DispellList.Length; i++)
                        {
                            if (MouseouverHasDispellAble(DispellList[i]))
                            {
                                API.CastSpell(Cleanse + "MO");
                                return;
                            }
                        }
                    }
                }
                #endregion
                if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1 && NotChanneling && InRange)
                {
                    API.CastSpell("Trinket1");
                }
                if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2 && NotChanneling && InRange)
                {
                    API.CastSpell("Trinket2");
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
                //Healing
                if (API.CanCast(BoL) && BoLTracking && !API.TargetHasBuff(BoL) && !API.TargetHasBuff(BoF) && API.TargetRoleSpec == API.TankRole && !BeaconofVirtue)
                {
                    API.CastSpell(BoL);
                    return;
                }
                if (API.CanCast(BoL) && BoLTracking && !API.MouseoverHasBuff(BoL) && !API.MouseoverHasBuff(BoF) && API.MouseoverRoleSpec == API.TankRole && !BeaconofVirtue)
                {
                    API.CastSpell(BoL + "MO");
                    return;
                }
                if (API.CanCast(BoF) && BoFTracking && !API.TargetHasBuff(BoF) && !API.TargetHasBuff(BoL) && (API.TargetRoleSpec == API.TankRole || API.TargetRoleSpec == API.HealerRole) && BeaconofFaith)
                {
                    API.CastSpell(BoF);
                    return;
                }
                if (API.CanCast(BoF) && BoFTracking && !API.MouseoverHasBuff(BoF) && !API.MouseoverHasBuff(BoL) && (API.MouseoverRoleSpec == API.TankRole || API.MouseoverRoleSpec == API.HealerRole) && BeaconofFaith)
                {
                    API.CastSpell(BoF + "MO");
                    return;
                }
                if (API.CanCast(RuleofLaw) && RuleofLawTalent && !API.PlayerHasBuff(RuleofLaw) && (API.TargetRange > 40 || IsMouseover && API.MouseoverRange > 40) && API.TargetHealthPercent > 0)
                {
                    API.CastSpell(RuleofLaw);
                    return;
                }
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
                if (BoSCheck)
                {
                    API.CastSpell(BoS);
                    return;
                }
                if (BoSCheckMO && !API.MacroIsIgnored(BoS + "MO"))
                {
                    API.CastSpell(BoS + "MO");
                    return;
                }
                if (API.CanCast(LightsHammer) && LightsHammerT && InRange && LHAoE)
                {
                    API.CastSpell(LightsHammer);
                    return;
                }
                if (API.CanCast(AshenHallow) && InRange && PlayerCovenantSettings == "Venthyr" && AHAoE && (!QuakingAshen || QuakingAshen && QuakingHelper))
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
                if (DTCheckMO && !API.MacroIsIgnored(DivineToll + "MO"))
                {
                    API.CastSpell(DivineToll + "MO");
                    return;
                }
                if (BoVCheck)
                {
                    API.CastSpell(BoV);
                    return;
                }
                if (BoVCheckMO && !API.MacroIsIgnored(BoV + "MO"))
                {
                    API.CastSpell(BoV + "MO");
                    return;
                }
                if (HolyPrismCheck)
                {
                    API.CastSpell(HolyPrism);
                    return;
                }
                if (HolyPrismCheckMO && !API.MacroIsIgnored(HolyPrism + "MO"))
                {
                    API.CastSpell(HolyPrism + "MO");
                    return;
                }
                if (WoGTankCheck)
                {
                    API.CastSpell(WoG);
                    return;
                }
                if (WoGTankCheckMO && !API.MacroIsIgnored(WoG + "MO"))
                {
                    API.CastSpell(WoG + "MO");
                    return;
                }
                if (WoGOverLoDCheck)
                {
                    API.CastSpell(WoG);
                    return;
                }
                if (WoGOverLoDCheckMO && !API.MacroIsIgnored(WoG + "MO"))
                {
                    API.CastSpell(WoG + "MO");
                    return;
                }
                if (LoDCheck)
                {
                    API.CastSpell(LoD);
                    return;
                }
                for (int i = 0; i < API.partyunits.Length; i++)
                    for (int j = 0; j < API.raidunits.Length; j++)
                    {
                        if (API.CanCast(Judgment) && InRange && (JudgementMacro && API.PlayerIsInGroup && !API.PlayerIsInRaid && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && API.UnitRange(API.partyunits[i]) < 6 || JudgementMacro && API.PlayerIsInRaid && API.UnitRoleSpec(API.raidunits[j]) == API.TankRole && API.UnitRange(API.raidunits[j]) < 6 || API.PlayerCanAttackTarget) && (JudgementofLight || !JudgementofLight) && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(Judgment);
                            return;
                        }
                        if (API.CanCast(Judgment) && !API.MacroIsIgnored(Judgment + "MO") && IsMouseover && InMoRange && API.PlayerCanAttackMouseover && API.MouseoverIsIncombat && (JudgementofLight || !JudgementofLight) && API.MouseoverHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(Judgment + "MO");
                            return;
                        }
                        if (API.CanCast(HammerofWrath) && (HammerMacro && API.PlayerIsInGroup && !API.PlayerIsInRaid && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && API.UnitRange(API.partyunits[i]) < 6 || HammerMacro && API.PlayerIsInRaid && API.UnitRoleSpec(API.raidunits[j]) == API.TankRole && API.UnitRange(API.raidunits[j]) < 6 || API.PlayerCanAttackTarget) && InRange && (API.TargetHealthPercent <= 20 && Level >= 46 && API.TargetHealthPercent > 0 || Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.TargetHealthPercent > 0) && API.PlayerIsInCombat)
                        {
                            API.CastSpell(HammerofWrath);
                            return;
                        }
                        if (API.CanCast(HammerofWrath) && !API.MacroIsIgnored(HammerofWrath + "MO") && InMoRange && IsMouseover && (API.MouseoverHealthPercent <= 20 && Level >= 46 && API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0 || Level >= 58 && API.PlayerCurrentHolyPower <= 4 && API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0) && API.PlayerIsInCombat)
                        {
                            API.CastSpell(HammerofWrath + "MO");
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
                        if (HSDPS && API.CanCast(HolyShock) && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup) && InRange && API.PlayerCanAttackTarget && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(HolyShock);
                            return;
                        }
                        if (API.CanCast(HolyShock) && !API.MacroIsIgnored(HolyShock + "MO") && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup) && IsMouseover && InMoRange && API.PlayerCanAttackMouseover && API.MouseoverIsIncombat && API.MouseoverHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(HolyShock + "MO");
                            return;
                        }
                        if (WoGCheck)
                        {
                            API.CastSpell(WoG);
                            return;
                        }
                        if (WoGCheckMO && !API.MacroIsIgnored(WoG + "MO"))
                        {
                            API.CastSpell(WoG + "MO");
                            return;
                        }
                        if (API.CanCast(CrusaderStrike) && CrusadersMight && (API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) > 150 || API.PlayerCurrentHolyPower < 3) && (CrusaderMacro && API.PlayerIsInGroup && !API.PlayerIsInRaid && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && API.UnitRange(API.partyunits[i]) < 6 || CrusaderMacro && API.PlayerIsInRaid && API.UnitRoleSpec(API.raidunits[j]) == API.TankRole && API.UnitRange(API.raidunits[j]) < 6 || API.PlayerCanAttackTarget && IsMelee) && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(CrusaderStrike);
                            return;
                        }
                        if (API.CanCast(CrusaderStrike) && !API.MacroIsIgnored(CrusaderStrike + "MO") && IsMouseover && CrusadersMight && API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) > 150 && IsMoMelee && API.PlayerCanAttackMouseover && API.MouseoverIsIncombat && API.MouseoverHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(CrusaderStrike + "MO");
                            return;
                        }
                        if (API.CanCast(CrusaderStrike) && Level >= 25 && !CrusadersMight && API.PlayerCurrentHolyPower < 3 && (CrusaderMacro && API.PlayerIsInGroup && !API.PlayerIsInRaid && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && API.UnitRange(API.partyunits[i]) < 6 || CrusaderMacro && API.PlayerIsInRaid && API.UnitRoleSpec(API.raidunits[j]) == API.TankRole && API.UnitRange(API.raidunits[j]) < 6 || API.PlayerCanAttackTarget && IsMelee) && API.TargetHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(CrusaderStrike);
                            return;
                        }
                        if (API.CanCast(CrusaderStrike) && !API.MacroIsIgnored(CrusaderStrike + "MO") && IsMouseover && Level >= 25 && !CrusadersMight && IsMoMelee && API.PlayerCanAttackMouseover && API.MouseoverIsIncombat && API.MouseoverHealthPercent > 0 && API.PlayerIsInCombat)
                        {
                            API.CastSpell(CrusaderStrike + "MO");
                            return;
                        }
                        if (FlashofLightInfusionCheck && (!QuakingHelper || QuakingFlash && QuakingHelper))
                        {
                            API.CastSpell(FoL);
                            return;
                        }
                        if (FlashofLightInfusionCheckMO && !API.MacroIsIgnored(FoL + "MO") && (!QuakingHelper || QuakingFlash && QuakingHelper))
                        {
                            API.CastSpell(FoL + "MO");
                            return;
                        }
                        if (HolyLightInfusionCheck && (!QuakingHelper || QuakingHoly && QuakingHelper))
                        {
                            API.CastSpell(HolyLight);
                            return;
                        }
                        if (HolyLightInfusionCheckMO && !API.MacroIsIgnored(HolyLight + "MO") && (!QuakingHelper || QuakingHoly && QuakingHelper))
                        {
                            API.CastSpell(HolyLight + "MO");
                            return;
                        }
                        if (FlashofLightCheck && (!QuakingHelper || QuakingFlash && QuakingHelper))
                        {
                            API.CastSpell(FoL);
                            return;
                        }
                        if (FlashofLightCheckMO && !API.MacroIsIgnored(FoL + "MO") && (!QuakingHelper || QuakingFlash && QuakingHelper))
                        {
                            API.CastSpell(FoL + "MO");
                            return;
                        }
                        if (HolyLightCheck && (!QuakingHelper || QuakingHoly && QuakingHelper))
                        {
                            API.CastSpell(HolyLight);
                            return;
                        }
                        if (HolyLightCheckMO && !API.MacroIsIgnored(HolyLight + "MO") && (!QuakingHelper || QuakingHoly && QuakingHelper))
                        {
                            API.CastSpell(HolyLight + "MO");
                            return;
                        }
                        if (BFCheck)
                        {
                            API.CastSpell(BF);
                            return;
                        }
                        if (BFCheckMO && !API.MacroIsIgnored(BF + "MO"))
                        {
                            API.CastSpell(BF + "MO");
                            return;
                        }
                        if (LoTMCheck)
                        {
                            API.CastSpell(LoTM);
                            return;
                        }
                        if (LoTMCheckMO && !API.MacroIsIgnored(LoTM + "MO"))
                        {
                            API.CastSpell(LoTM + "MO");
                            return;
                        }
                        //DPS
                        if (API.PlayerIsInCombat)
                        {
                            if (API.CanCast(DivineToll) && !DTHealing && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && API.PlayerCanAttackTarget && InRange && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(DivineToll);
                                return;
                            }
                            if (API.CanCast(DivineToll) && !API.MacroIsIgnored(DivineToll + "MO") && IsMouseover && !DTHealing && PlayerCovenantSettings == "Kyrian" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && API.PlayerCanAttackMouseover && InMoRange && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(DivineToll + "MO");
                                return;
                            }
                            if (API.CanCast(VanqusihersHammer) && PlayerCovenantSettings == "Necrolord" && (UseCovenant == "With Cooldowns" && IsCooldowns || UseCovenant == "On Cooldown" || UseCovenant == "on AOE" && IsAOE) && NotChanneling && !DPSHealthCheck && InRange && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(VanqusihersHammer);
                                return;
                            }
                            if (API.CanCast(Cons) && API.PlayerUnitInMeleeRangeCount >= 2 && IsMelee && !API.TargetHasDebuff(Cons) && !API.PlayerIsMoving && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(Cons);
                                return;
                            }
                            if (API.CanCast(SoTR) && (SooRMacro && API.PlayerIsInGroup && !API.PlayerIsInRaid && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && API.UnitRange(API.partyunits[i]) < 6 || SooRMacro && API.PlayerIsInRaid && API.UnitRoleSpec(API.raidunits[j]) == API.TankRole && API.UnitRange(API.raidunits[j]) < 6 || API.PlayerCanAttackTarget) && API.PlayerCurrentHolyPower > 4 && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup) && IsMelee && API.TargetHealthPercent > 0)
                            {
                                API.CastSpell(SoTR);
                                return;
                            }
                            if (API.CanCast(SoTR) && !API.MacroIsIgnored(SoTR + "MO") && IsMouseover && API.PlayerCanAttackMouseover && API.MouseoverIsIncombat && API.PlayerCurrentHolyPower > 4 && (API.PlayerIsInGroup && API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber || API.PlayerIsInRaid && API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber || !API.PlayerIsInGroup) && IsMoMelee && API.MouseoverHealthPercent > 0)
                            {
                                API.CastSpell(SoTR + "MO");
                                return;
                            }
                        }
                    }
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
                if (API.PlayerIsInGroup || API.PlayerIsInRaid)
                {
                    for (int j = 0; j < DispellList.Length; j++)
                        for (int i = 0; i < API.partyunits.Length; i++)
                            for (int t = 0; t < API.raidunits.Length; t++)
                            {

                                if (API.PlayerHealthPercent <= PlayerHP && API.TargetIsUnit() != "player")
                                {
                                    API.CastSpell(Player);
                                    return;
                                }
                                if (UnitHasDispellAble(DispellList[j], API.partyunits[i]) && IsDispell && !API.SpellISOnCooldown(Cleanse) && API.TargetIsUnit() != API.partyunits[i])
                                {
                                    API.CastSpell(API.partyunits[i]);
                                    return;
                                }
                                if (UnitLowest != "none" && API.UnitHealthPercent(UnitLowest) <= 10 && API.TargetIsUnit() != UnitLowest)
                                {
                                    API.CastSpell(UnitLowest);
                                    return;
                                }
                                if (LowestTankUnit != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && !UnitHasDebuff("Gluttonous Miasma", LowestTankUnit) && API.UnitHealthPercent(LowestTankUnit) <= TankHealth && API.TargetIsUnit() != LowestTankUnit)
                                {
                                    API.CastSpell(LowestTankUnit);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (UnitLowest != "none" && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && !UnitHasDebuff("Gluttonous Miasma", UnitLowest) && API.UnitHealthPercent(UnitLowest) <= UnitHealth && API.TargetIsUnit() != UnitLowest)
                                {
                                    API.CastSpell(UnitLowest);
                                    SwapWatch.Restart();
                                    return;
                                }
                                if (API.PlayerIsInGroup && !API.PlayerIsInRaid)
                                {
                                    if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRoleSpec(API.partyunits[i]) == API.TankRole && !API.MacroIsIgnored("Assist") && (API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) > 150 && API.SpellCharges(CrusaderStrike) > 0 && CrusadersMight && API.UnitRange(API.partyunits[i]) <= 4 && !CrusaderMacro || !API.SpellISOnCooldown(Judgment) && JudgementofLight && API.UnitRange(API.partyunits[i]) <= 30 || (API.UnitAboveHealthPercentParty(AoEDPSHLifePercent) >= AoEDPSNumber && (!CrusadersMight && !JudgementofLight || !CrusadersMight && JudgementofLight || CrusadersMight || !JudgementofLight)) && API.PlayerIsInCombat && API.TargetIsUnit() != API.partyunits[i]))
                                    {
                                        API.CastSpell(API.partyunits[i]);
                                        API.CastSpell("Assist");
                                        SwapWatch.Restart();
                                        return;
                                    }
                                }
                                else
                                {
                                    if (API.PlayerIsInRaid)
                                    {
                                        if (IsDPS && !API.PlayerCanAttackTarget && API.UnitRoleSpec(API.raidunits[t]) == API.TankRole && !API.MacroIsIgnored("Assist") && (API.SpellISOnCooldown(HolyShock) && API.SpellCDDuration(HolyShock) > 150 && API.SpellCharges(CrusaderStrike) > 0 && CrusadersMight && API.UnitRange(API.raidunits[t]) <= 4 && !CrusaderMacro || !API.SpellISOnCooldown(Judgment) && JudgementofLight && API.UnitRange(API.raidunits[t]) <= 30 || API.UnitAboveHealthPercentRaid(AoEDPSHRaidLifePercent) >= AoEDPSRaidNumber && (!CrusadersMight && !JudgementofLight || !CrusadersMight && JudgementofLight || CrusadersMight || !JudgementofLight)) && (!SwapWatch.IsRunning || SwapWatch.ElapsedMilliseconds >= API.SpellGCDTotalDuration * 10) && API.PlayerIsInCombat && API.TargetIsUnit() != API.raidunits[t])
                                        {
                                            API.CastSpell(API.raidunits[t]);
                                            SwapWatch.Restart();
                                            API.CastSpell("Assist");
                                            return;
                                        }
                                    }
                                }


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




