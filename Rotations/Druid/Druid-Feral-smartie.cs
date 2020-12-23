﻿// Changelog
// v1.0 First release
// v1.1 bearform fix
// v1.2 bloodtalons and covenant stuff
// v1.3 low level fix
// v1.4 improved bloodtalons code and alot of small fixes
// v1.5 covenant update :-)
// v1.6 legendary prep
// v1.7 Racials and Trinkets
// v1.8 Mighty Bash added
// v1.9 Bloodtalons fixed
// v2.0 some small changes
// v2.1 spell ids and alot of other stuff
// v2.2 Bloodtalons fix 

using System.Diagnostics;

namespace HyperElk.Core
{
    public class Feraldruid : CombatRoutine
    {
        private bool IsMouseover => API.ToggleIsEnabled("Mouseover");
        //Spell,Auras
        private string CatForm = "Cat Form";
        private string Rip = "Rip";
        private string Rake = "Rake";
        private string Prowl = "Prowl";
        private string TigersFury = "Tiger's Fury";
        private string Shred = "Shred";
        private string Regrowth = "Regrowth";
        private string FerociousBite = "Ferocious Bite";
        private string SavageRoar = "Savage Roar";
        private string Thrash = "Thrash";
        private string Thrashbear = "Thrash Bear";
        private string Swipe = "Swipe";
        private string BrutalSlash = "Brutal Slash";
        private string Moonfire = "Moonfire";
        private string FeralFrenzy = "Feral Frenzy";
        private string Berserk = "Berserk";
        private string Incarnation = "Incarnation: King of the Jungle";
        private string SkullBash = "Skull Bash";
        private string SurvivalInstincts = "Survival Instincts";
        private string Barkskin = "Barkskin";
        private string Renewal = "Renewal";
        private string StampedingRoar = "Stampeding Roar";
        private string Maim = "Maim";
        private string Typhoon = "Typhoon";
        private string PrimalWrath = "Primal Wrath";
        private string BearForm = "Bear Form";
        private string Mangle = "Mangle";
        private string FrenziedRegeneration = "Frenzied Regeneration";
        private string Ironfur = "Ironfur";
        private string PredatorySwiftness = "Predatory Swiftness";
        private string Bloodtalons = "Bloodtalons";
        private string Clearcasting = "Clearcasting";
        private string TravelForm = "Travel Form";
        private string ScentofBlood = "Scent of Blood";
        private string Shadowmeld = "Shadowmeld";
        private string RavenousFrenzy = "Ravenous Frenzy";
        private string ConvoketheSpirits = "Convoke the Spirits";
        private string AdaptiveSwarm = "Adaptive Swarm";
        private string LoneEmpowerment = "Lone Empowerment";
        private string LoneSpirit = "Lone Spirit";
        private string Soulshape = "Soulshape";
        private string ApexPredatorsCraving = "Apex Predator's Craving";
        private string MightyBash = "Mighty Bash";
        private string PhialofSerenity = "Phial of Serenity";
        private string SpiritualHealingPotion = "Spiritual Healing Potion";

        //Talents
        bool TalentLunarInspiration => API.PlayerIsTalentSelected(1, 3);
        bool TalentPredator => API.PlayerIsTalentSelected(1, 1);
        bool TalentRenewal => API.PlayerIsTalentSelected(2, 2);
        bool TalentMightyBash => API.PlayerIsTalentSelected(4, 1);
        bool TalentBalanceAffinity => API.PlayerIsTalentSelected(3, 1);
        bool TalentGuardianAffinity => API.PlayerIsTalentSelected(3, 2);
        bool TalentIncarnation => API.PlayerIsTalentSelected(5, 3);
        bool TalentSavageRoar => API.PlayerIsTalentSelected(5, 2);
        bool TalentScentofBlood => API.PlayerIsTalentSelected(6, 1);
        bool TalentBrutalSlash => API.PlayerIsTalentSelected(6, 2);
        bool TalentPrimalWrath => API.PlayerIsTalentSelected(6, 3);
        bool TalentBloodtalons => API.PlayerIsTalentSelected(7, 2);
        bool TalentFeralFrenzy => API.PlayerIsTalentSelected(7, 3);

        //General
        private static readonly Stopwatch rakewatch = new Stopwatch();
        private static readonly Stopwatch thrashwatch = new Stopwatch();
        private static readonly Stopwatch brutalwatch = new Stopwatch();
        private int PlayerLevel => API.PlayerLevel;
        private bool isMelee => (TalentBalanceAffinity && API.TargetRange < 9 || !TalentBalanceAffinity && API.TargetRange < 6);
        private bool isMOMelee => (TalentBalanceAffinity && API.MouseoverRange < 9 || !TalentBalanceAffinity && API.MouseoverRange < 6);
        private bool isThrashMelee => (TalentBalanceAffinity && API.TargetRange < 12 || !TalentBalanceAffinity && API.TargetRange < 9);
        private bool isMOThrashMelee => (TalentBalanceAffinity && API.MouseoverRange < 12 || !TalentBalanceAffinity && API.MouseoverRange < 9);
        private bool isKickRange => (TalentBalanceAffinity && API.TargetRange < 17 || !TalentBalanceAffinity && API.TargetRange < 14);
        private bool IncaBerserk => (API.PlayerHasBuff(Incarnation) || API.PlayerHasBuff(Berserk));
        private bool Bloodytalons => (API.PlayerBuffTimeRemaining(Bloodtalons) == 0 && TalentBloodtalons);
        bool IsFeralFrenzy => (UseFeralFrenzy == "with Cooldowns" && IsCooldowns || UseFeralFrenzy == "always");
        bool IsIncarnation => (UseIncarnation == "with Cooldowns" && IsCooldowns || UseIncarnation == "always");
        bool IsBerserk => (UseBerserk == "with Cooldowns" && IsCooldowns || UseBerserk == "always");
        bool IsCovenant => (UseCovenant == "with Cooldowns" && IsCooldowns || UseCovenant == "always" || UseCovenant == "on AOE" && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && IsAOE);
        bool IsTrinkets1 => (UseTrinket1 == "with Cooldowns" && IsCooldowns || UseTrinket1 == "always" || UseTrinket1 == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && isMelee;
        bool IsTrinkets2 => (UseTrinket2 == "with Cooldowns" && IsCooldowns || UseTrinket2 == "always" || UseTrinket2 == "on AOE" && API.TargetUnitInRangeCount >= AOEUnitNumber && IsAOE) && isMelee;


        //CBProperties
        private string UseTrinket1 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket1")];
        private string UseTrinket2 => CDUsageWithAOE[CombatRoutine.GetPropertyInt("Trinket2")];
        public new string[] CDUsage = new string[] { "Not Used", "with Cooldowns", "always" };
        public new string[] CDUsageWithAOE = new string[] { "Not Used", "with Cooldowns", "on AOE", "always" };
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };
        private string UseCovenant => CDUsageWithAOE[CombatRoutine.GetPropertyInt("UseCovenant")];
        private string UseFeralFrenzy => CDUsage[CombatRoutine.GetPropertyInt(FeralFrenzy)];
        private string UseIncarnation => CDUsage[CombatRoutine.GetPropertyInt(Incarnation)];
        private string UseBerserk => CDUsage[CombatRoutine.GetPropertyInt(Berserk)];
        public bool isMouseoverInCombat => CombatRoutine.GetPropertyBool("MouseoverInCombat");
        private bool ProwlOOC => CombatRoutine.GetPropertyBool("ProwlOOC");
        private bool AutoForm => CombatRoutine.GetPropertyBool("AutoForm");
        private bool AutoTravelForm => CombatRoutine.GetPropertyBool("AutoTravelForm");
        private int RegrowthLifePercent => numbList[CombatRoutine.GetPropertyInt(Regrowth)];
        private int RenewalLifePercent => numbList[CombatRoutine.GetPropertyInt(Renewal)];
        private int BarkskinLifePercent => numbList[CombatRoutine.GetPropertyInt(Barkskin)];
        private int SurvivalInstinctsLifePercent => numbList[CombatRoutine.GetPropertyInt(SurvivalInstincts)];
        private int FrenziedRegenerationLifePercent => numbList[CombatRoutine.GetPropertyInt(FrenziedRegeneration)];
        private int IronfurLifePercent => numbList[CombatRoutine.GetPropertyInt(Ironfur)];
        private int BearFormLifePercent => numbList[CombatRoutine.GetPropertyInt(BearForm)];
        private int PhialofSerenityLifePercent => numbList[CombatRoutine.GetPropertyInt(PhialofSerenity)];
        private int SpiritualHealingPotionLifePercent => numbList[CombatRoutine.GetPropertyInt(SpiritualHealingPotion)];

        public override void Initialize()
        {
            CombatRoutine.Name = "Feral Druid by smartie";
            API.WriteLog("Welcome to smartie`s Feral Druid v2.2");
            API.WriteLog("Create the following mouseover macros and assigned to the bind:");
            API.WriteLog("RakeMO - /cast [@mouseover] Rake");
            API.WriteLog("ThrashMO - /cast [@mouseover] Thrash");
            API.WriteLog("RipMO - /cast [@mouseover] Rip");

            //Spells
            CombatRoutine.AddSpell(CatForm, 768, "NumPad1");
            CombatRoutine.AddSpell(Rip, 1079, "D6");
            CombatRoutine.AddSpell(Rake, 1822, "D2");
            CombatRoutine.AddSpell(Prowl, 5215, "NumPad3");
            CombatRoutine.AddSpell(TigersFury, 5217, "D1");
            CombatRoutine.AddSpell(Shred, 5221, "D3");
            CombatRoutine.AddSpell(Regrowth, 8936, "F2");
            CombatRoutine.AddSpell(FerociousBite, 22568, "D7");
            CombatRoutine.AddSpell(SavageRoar, 52610, "D9");
            CombatRoutine.AddSpell(Thrash, 106830, "D4");
            CombatRoutine.AddSpell(Thrashbear, 77758, "D4");
            CombatRoutine.AddSpell(Swipe, 106785, "D5");
            CombatRoutine.AddSpell(BrutalSlash, 202028, "D5");
            CombatRoutine.AddSpell(Moonfire, 8921, "NumPad2");
            CombatRoutine.AddSpell(FeralFrenzy, 274837, "NumPad4");
            CombatRoutine.AddSpell(Berserk, 106951, "D1");
            CombatRoutine.AddSpell(Incarnation, 102543, "D1");
            CombatRoutine.AddSpell(SkullBash, 106839, "F12");
            CombatRoutine.AddSpell(SurvivalInstincts, 61336, "F1");
            CombatRoutine.AddSpell(Barkskin, 22812, "F1");
            CombatRoutine.AddSpell(Renewal, 108238, "F6");
            CombatRoutine.AddSpell(StampedingRoar, 106898, "F7");
            CombatRoutine.AddSpell(Maim, 22570, "F9");
            CombatRoutine.AddSpell(Typhoon, 132469, "D1");
            CombatRoutine.AddSpell(PrimalWrath, 285381, "D0");
            CombatRoutine.AddSpell(BearForm, 5487, "NumPad4");
            CombatRoutine.AddSpell(Mangle, 33917, "D3");
            CombatRoutine.AddSpell(FrenziedRegeneration, 22842, "D7");
            CombatRoutine.AddSpell(Ironfur, 192081, "D8");
            CombatRoutine.AddSpell(TravelForm, 783, "NumPad6");
            CombatRoutine.AddSpell(MightyBash, 5211, "D1");
            CombatRoutine.AddSpell(RavenousFrenzy, 323546, "D1");
            CombatRoutine.AddSpell(ConvoketheSpirits, 323764, "D1");
            CombatRoutine.AddSpell(AdaptiveSwarm, 325727, "D1");
            CombatRoutine.AddSpell(LoneEmpowerment, 338142, "D1");


            //Macros
            CombatRoutine.AddMacro(Rake + "MO", "D2");
            CombatRoutine.AddMacro(Thrash + "MO", "D6");
            CombatRoutine.AddMacro(Rip + "MO", "D6");
            CombatRoutine.AddMacro("Trinket1", "F9");
            CombatRoutine.AddMacro("Trinket2", "F10");


            //Buffs
            CombatRoutine.AddBuff(Prowl, 5215);
            CombatRoutine.AddBuff(CatForm, 768);
            CombatRoutine.AddBuff(BearForm, 5487);
            CombatRoutine.AddBuff(TigersFury, 5217);
            CombatRoutine.AddBuff(SavageRoar, 52610);
            CombatRoutine.AddBuff(PredatorySwiftness, 69369);
            CombatRoutine.AddBuff(Berserk, 106951);
            CombatRoutine.AddBuff(Bloodtalons, 145152);
            CombatRoutine.AddBuff(Clearcasting, 135700);
            CombatRoutine.AddBuff(SurvivalInstincts, 61336);
            CombatRoutine.AddBuff(TravelForm, 783);
            CombatRoutine.AddBuff(Ironfur, 192081);
            CombatRoutine.AddBuff(FrenziedRegeneration, 22842);
            CombatRoutine.AddBuff(Incarnation, 102543);
            CombatRoutine.AddBuff(ScentofBlood, 285646);
            CombatRoutine.AddBuff(Shadowmeld, 58984);
            CombatRoutine.AddBuff(RavenousFrenzy, 323546);
            CombatRoutine.AddBuff(LoneSpirit, 338041);
            CombatRoutine.AddBuff(Soulshape, 310143);
            CombatRoutine.AddBuff(ApexPredatorsCraving, 339140);

            //Debuff
            CombatRoutine.AddDebuff(Rip, 1079);
            CombatRoutine.AddDebuff(Thrash, 106830);
            CombatRoutine.AddDebuff(Thrashbear, 192090);
            CombatRoutine.AddDebuff(Rake,155722);
            CombatRoutine.AddDebuff(Moonfire, 155625);
            CombatRoutine.AddDebuff(AdaptiveSwarm, 325727);

            //Toggle
            CombatRoutine.AddToggle("Mouseover");

            //Item
            CombatRoutine.AddItem(PhialofSerenity, 177278);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);

            //Prop
            CombatRoutine.AddProp("Trinket1", "Use " + "Trinket 1", CDUsageWithAOE, "Use " + "Trinket 1" + " always, with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp("Trinket2", "Use " + "Trinket 2", CDUsageWithAOE, "Use " + "Trinket 2" + " always, with Cooldowns", "Trinkets", 0);
            AddProp("MouseoverInCombat", "Only Mouseover in combat", false, "Only Attack mouseover in combat to avoid stupid pulls", "Generic");
            CombatRoutine.AddProp("UseCovenant", "Use " + "Covenant Ability", CDUsageWithAOE, "Use " + "Covenant" + " always, with Cooldowns", "Covenant", 0);
            CombatRoutine.AddProp(FeralFrenzy, "Use " + FeralFrenzy, CDUsage, "Use " + FeralFrenzy + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(Incarnation, "Use " + Incarnation, CDUsage, "Use " + Incarnation + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp(Berserk, "Use " + Berserk, CDUsage, "Use " + Berserk + " always, with Cooldowns", "Cooldowns", 0);
            CombatRoutine.AddProp("ProwlOOC", "ProwlOOC", true, "Use Prowl out of Combat", "Generic");
            CombatRoutine.AddProp("AutoForm", "AutoForm", true, "Will auto switch forms", "Generic");
            CombatRoutine.AddProp("AutoTravelForm", "AutoTravelForm", false, "Will auto switch to Travel Form Out of Fight and outside", "Generic");
            CombatRoutine.AddProp(PhialofSerenity, PhialofSerenity + " Life Percent", numbList, " Life percent at which" + PhialofSerenity + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SpiritualHealingPotion, SpiritualHealingPotion + " Life Percent", numbList, " Life percent at which" + SpiritualHealingPotion + " is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(Regrowth, Regrowth + " Life Percent", numbList, "Life percent at which" + Regrowth + "is used, set to 0 to disable", "Defense", 60);
            CombatRoutine.AddProp(Renewal, Renewal + " Life Percent", numbList, "Life percent at which" + Renewal + "is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(Barkskin, Barkskin + " Life Percent", numbList, "Life percent at which" + Barkskin + "is used, set to 0 to disable", "Defense", 40);
            CombatRoutine.AddProp(SurvivalInstincts, SurvivalInstincts + " Life Percent", numbList, "Life percent at which" + SurvivalInstincts + "is used, set to 0 to disable", "Defense", 30);
            CombatRoutine.AddProp(FrenziedRegeneration, FrenziedRegeneration + " Life Percent", numbList, "Life percent at which" + FrenziedRegeneration + "is used, set to 0 to disable", "Defense", 60);
            CombatRoutine.AddProp(Ironfur, Ironfur + " Life Percent", numbList, "Life percent at which" + Ironfur + "is used, set to 0 to disable", "Defense", 90);
            CombatRoutine.AddProp(BearForm, BearForm + " Life Percent", numbList, "Life percent at which rota will go into" + BearForm + "set to 0 to disable", "Defense", 10);
        }
        public override void Pulse()
        {
            //API.WriteLog("Last Spell?: " + API.LastSpellCastInGame);
            // Stopwatch stop
            if (rakewatch.IsRunning && rakewatch.ElapsedMilliseconds > 4000)
            {
                rakewatch.Reset();
                //API.WriteLog("Resetting Rakewatch.");
            }
            if (thrashwatch.IsRunning && thrashwatch.ElapsedMilliseconds > 4000)
            {
                thrashwatch.Reset();
                //API.WriteLog("Resetting thrashwatch.");
            }
            if (brutalwatch.IsRunning && brutalwatch.ElapsedMilliseconds > 4000)
            {
                brutalwatch.Reset();
                //API.WriteLog("Resetting brutalwatch.");
            }

        }
        public override void CombatPulse()
        {
            if (API.PlayerCurrentCastTimeRemaining > 40)
                return;
            if (!API.PlayerIsMounted && !API.PlayerHasBuff(TravelForm))
            {
                if (isInterrupt && API.CanCast(SkullBash) && PlayerLevel >= 26 && isKickRange)
                {
                    API.CastSpell(SkullBash);
                    return;
                }
                if (API.CanCast(RacialSpell1) && isInterrupt && PlayerRaceSettings == "Tauren" && !API.PlayerIsMoving && isRacial && isMelee && API.SpellISOnCooldown(SkullBash))
                {
                    API.CastSpell(RacialSpell1);
                    return;
                }
                if (API.CanCast(MightyBash) && isInterrupt && TalentMightyBash && isMelee && API.SpellISOnCooldown(SkullBash))
                {
                    API.CastSpell(MightyBash);
                    return;
                }
                if (API.PlayerHealthPercent <= RegrowthLifePercent && PlayerLevel >= 3 && API.CanCast(Regrowth) && API.PlayerHasBuff(PredatorySwiftness))
                {
                    API.CastSpell(Regrowth);
                    return;
                }
                if (API.CanCast(SurvivalInstincts) && PlayerLevel >= 32 && API.PlayerHealthPercent <= SurvivalInstinctsLifePercent && !API.PlayerHasBuff(SurvivalInstincts))
                {
                    API.CastSpell(SurvivalInstincts);
                    return;
                }
                if (API.CanCast(Renewal) && API.PlayerHealthPercent <= RenewalLifePercent && TalentRenewal)
                {
                    API.CastSpell(Renewal);
                    return;
                }
                if (API.CanCast(Barkskin) && PlayerLevel >= 24 && API.PlayerHealthPercent <= BarkskinLifePercent)
                {
                    API.CastSpell(Barkskin);
                    return;
                }
                if (API.PlayerItemCanUse("Healthstone") && API.PlayerItemRemainingCD("Healthstone") == 0 && API.PlayerHealthPercent <= HealthStonePercent)
                {
                    API.CastSpell("Healthstone");
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
                if (API.PlayerHealthPercent <= FrenziedRegenerationLifePercent && API.PlayerRage >= 10 && API.CanCast(FrenziedRegeneration) && API.PlayerHasBuff(BearForm) && TalentGuardianAffinity)
                {
                    API.CastSpell(FrenziedRegeneration);
                    return;
                }
                if (API.PlayerHealthPercent <= IronfurLifePercent && API.PlayerRage >= 40 && API.CanCast(Ironfur) && API.PlayerHasBuff(BearForm))
                {
                    API.CastSpell(Ironfur);
                    return;
                }
                if (API.PlayerHealthPercent <= BearFormLifePercent && PlayerLevel >= 8 && AutoForm && API.CanCast(BearForm) && !API.PlayerHasBuff(BearForm))
                {
                    API.CastSpell(BearForm);
                    return;
                }
                if (API.PlayerHealthPercent > BearFormLifePercent && BearFormLifePercent != 0 && API.CanCast(CatForm) && API.PlayerHasBuff(BearForm) && AutoForm)
                {
                    API.CastSpell(CatForm);
                    return;
                }
                rotation();
                return;
            }
        }
        public override void OutOfCombatPulse()
        {
            if (API.PlayerCurrentCastTimeRemaining > 40)
                return;
            if (ProwlOOC && API.CanCast(Prowl) && PlayerLevel >= 17 && !API.PlayerHasBuff(Prowl) && !API.PlayerIsMounted && !API.PlayerHasBuff(TravelForm))
            {
                API.CastSpell(Prowl);
                return;
            }
            if (API.CanCast(TravelForm) && AutoTravelForm && API.PlayerIsOutdoor && !API.PlayerHasBuff(TravelForm))
            {
                API.CastSpell(TravelForm);
                return;
            }
        }
        private void rotation()
        {
            if ((!API.PlayerHasBuff(CatForm) && PlayerLevel >= 5) && !API.PlayerHasBuff(BearForm) && !API.PlayerHasBuff(Soulshape) && AutoForm)
            {
                API.CastSpell(CatForm);
                return;
            }
            if (API.CanCast(Rake) && API.PlayerHasBuff(Prowl) && isMelee && PlayerLevel >= 10 && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
            {
                API.CastSpell(Rake);
                return;
            }
            if (API.PlayerHasBuff(BearForm))
            {
                if (API.CanCast(Thrashbear) && isThrashMelee && PlayerLevel >= 11 && !API.TargetHasDebuff(Thrashbear))
                {
                    API.CastSpell(Thrashbear);
                    return;
                }
                if (API.CanCast(Mangle) && isMelee && PlayerLevel >= 8 && API.TargetHasDebuff(Thrashbear))
                {
                    API.CastSpell(Mangle);
                    return;
                }
                if (API.CanCast(Swipe) && isThrashMelee && PlayerLevel >= 42 && API.TargetHasDebuff(Thrashbear))
                {
                    API.CastSpell(Swipe);
                    return;
                }
            }
            if (PlayerLevel < 5)
            {
                API.WriteLog("Current Level:" + PlayerLevel);
                API.WriteLog("Rota will start working once you reach Level 5");
            }
            if (API.PlayerHasBuff(CatForm) && PlayerLevel >= 5)
            {
                if (API.CanCast(Rake) && PlayerLevel >= 10 && isMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28) && API.PlayerHasBuff(Shadowmeld))
                {
                    API.CastSpell(Rake);
                    return;
                }
                //Cooldowns
                //actions.cooldown +=/ shadowmeld,if= buff.tigers_fury.up & buff.bs_inc.down & combo_points < 4 & dot.rake.pmultiplier < 1.6 & energy > 40
                if (API.CanCast(RacialSpell1) && PlayerRaceSettings == "Night Elf" && isRacial && IsCooldowns && isMelee && (API.PlayerHasBuff(TigersFury) && !IncaBerserk && API.PlayerComboPoints < 4 && API.PlayerEnergy > 40))
                {
                    API.CastSpell(RacialSpell1);
                    return;
                }
                //actions.cooldown +=/ berserking,if= buff.tigers_fury.up | buff.bs_inc.up
                if (API.CanCast(RacialSpell1) && PlayerRaceSettings == "Troll" && isRacial && IsCooldowns && isMelee && (API.PlayerHasBuff(TigersFury) || IncaBerserk))
                {
                    API.CastSpell(RacialSpell1);
                    return;
                }
                if (API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && IsTrinkets1)
                {
                    API.CastSpell("Trinket1");
                }
                if (API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && IsTrinkets2)
                {
                    API.CastSpell("Trinket2");
                }
                if (!TalentIncarnation && PlayerLevel >= 34 && API.PlayerEnergy >= 30 && API.CanCast(Berserk) && IsBerserk && (API.PlayerHasBuff(TigersFury) || API.SpellCDDuration(TigersFury) > 500))
                {
                    API.CastSpell(Berserk);
                    return;
                }
                if (TalentIncarnation && API.PlayerEnergy >= 30 && API.CanCast(Incarnation) && IsIncarnation && (API.PlayerHasBuff(TigersFury) || API.SpellCDDuration(TigersFury) > 500))
                {
                    API.CastSpell(Incarnation);
                    return;
                }
                if (API.CanCast(RavenousFrenzy) && isMelee && IncaBerserk && PlayerCovenantSettings == "Venthyr" && IsCovenant)
                {
                    API.CastSpell(RavenousFrenzy);
                    return;
                }
                //actions.cooldown+=/convoke_the_spirits,if=(dot.rip.remains>4&(buff.tigers_fury.down|buff.tigers_fury.remains<4)&combo_points=0&dot.thrash_cat.ticking&dot.rake.ticking)|fight_remains<5
                if (API.CanCast(ConvoketheSpirits) && isMelee && !API.PlayerIsMoving && PlayerCovenantSettings == "Night Fae" && IsCovenant && (API.TargetDebuffRemainingTime(Rip) > 400 && (API.PlayerBuffTimeRemaining(TigersFury) == 0 || API.PlayerBuffTimeRemaining(TigersFury) < 400) && API.PlayerComboPoints == 0 && API.TargetHasDebuff(Thrash) && API.TargetHasDebuff(Rake)))
                {
                    API.CastSpell(ConvoketheSpirits);
                    return;
                }
                if (API.CanCast(LoneEmpowerment) && isMelee && PlayerCovenantSettings == "Kyrian" && IsCovenant && API.PlayerBuffTimeRemaining(TigersFury) >= 1000 && API.PlayerHasBuff(LoneSpirit))
                {
                    API.CastSpell(LoneEmpowerment);
                    return;
                }
                if (API.CanCast(AdaptiveSwarm) && isMelee && PlayerCovenantSettings == "Necrolord" && IsCovenant && !API.TargetHasDebuff(AdaptiveSwarm))
                {
                    API.CastSpell(AdaptiveSwarm);
                    return;
                }
                if (API.CanCast(TigersFury) && PlayerLevel >= 12 && isMelee &&
                (!TalentPredator && (API.PlayerEnergy <= 40 || IncaBerserk) ||
                TalentPredator && (!API.PlayerHasBuff(TigersFury) || API.PlayerHasBuff(TigersFury) && API.PlayerEnergy <= 40)))
                {
                    API.CastSpell(TigersFury);
                    return;
                }
                if (isMelee && TalentFeralFrenzy && API.CanCast(FeralFrenzy) && API.PlayerComboPoints == 0 && IsFeralFrenzy)
                {
                    API.CastSpell(FeralFrenzy);
                    return;
                }
                if (!API.PlayerHasBuff(Prowl) && !API.PlayerHasBuff(Shadowmeld))
                {
                    // Single Target rota
                    if (API.PlayerUnitInMeleeRangeCount < AOEUnitNumber || !IsAOE)
                    {
                        //Finisher
                        if (API.PlayerComboPoints > 4)
                        {
                            if (TalentSavageRoar && isMelee && API.CanCast(SavageRoar) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 30 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 24) && !API.PlayerHasBuff(SavageRoar))
                            {
                                API.CastSpell(SavageRoar);
                                return;
                            }
                            if (isMelee && PlayerLevel >= 7 && API.CanCast(FerociousBite) && API.PlayerEnergy >= 50 && (API.TargetHasDebuff(Rip) && API.TargetDebuffRemainingTime(Rip) > 300 || PlayerLevel < 21))
                            {
                                API.CastSpell(FerociousBite);
                                return;
                            }
                            if (isMelee && PlayerLevel >= 21 && API.CanCast(Rip) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 16) && (!API.TargetHasDebuff(Rip) || API.TargetDebuffRemainingTime(Rip) <= 300))
                            {
                                API.CastSpell(Rip);
                                return;
                            }
                        }
                        //Generator
                        if (API.PlayerComboPoints < 5)
                        {
                            if (Bloodytalons)
                            {
                                if (API.LastSpellCastInGame == Rake && !rakewatch.IsRunning)
                                {
                                    rakewatch.Start();
                                    //API.WriteLog("Starting Rakewatch.");
                                }
                                if ((API.LastSpellCastInGame == Thrash || API.LastSpellCastInGame == Thrashbear) && !thrashwatch.IsRunning)
                                {
                                    thrashwatch.Start(); thrashwatch.Start();
                                    //API.WriteLog("Starting thrashwatch.");
                                }
                                if (API.LastSpellCastInGame == BrutalSlash && !brutalwatch.IsRunning)
                                {
                                    brutalwatch.Start();
                                    //API.WriteLog("Starting brutalwatch.");
                                }
                                if (API.CanCast(Rake) && PlayerLevel >= 10 && (API.TargetDebuffRemainingTime(Rake) <= 360 && TalentBrutalSlash || !TalentBrutalSlash) && !rakewatch.IsRunning && isMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
                                {
                                    API.CastSpell(Rake);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 11 && !thrashwatch.IsRunning && API.CanCast(Thrash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Thrash);
                                    return;
                                }
                                if (TalentBrutalSlash && !brutalwatch.IsRunning && API.TargetDebuffRemainingTime(Thrash) > 300 && isThrashMelee && API.CanCast(BrutalSlash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 25 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(BrutalSlash);
                                    return;
                                }
                                if (isMelee && PlayerLevel >= 5 && ((brutalwatch.IsRunning && TalentBrutalSlash || TalentBrutalSlash && API.SpellCharges(BrutalSlash) == 0 || !TalentBrutalSlash) && thrashwatch.IsRunning && rakewatch.IsRunning) && API.CanCast(Shred) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Shred);
                                    return;
                                }
                            }
                            if (!Bloodytalons)
                            {
                                if (isMelee && PlayerLevel >= 7 && API.CanCast(FerociousBite) && API.PlayerHasBuff(ApexPredatorsCraving))
                                {
                                    API.CastSpell(FerociousBite);
                                    return;
                                }
                                if (TalentBrutalSlash && API.TargetDebuffRemainingTime(Thrash) > 300 && !API.PlayerHasBuff(Prowl) && isThrashMelee && API.CanCast(BrutalSlash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 25 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(BrutalSlash);
                                    return;
                                }
                                if (API.CanCast(Rake) && PlayerLevel >= 10 && isMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28) && API.TargetDebuffRemainingTime(Rake) <= 360)
                                {
                                    API.CastSpell(Rake);
                                    return;
                                }
                                if (API.CanCast(Moonfire) && !API.PlayerHasBuff(Prowl) && TalentLunarInspiration && API.LastSpellCastInGame != (Moonfire) && (!API.TargetHasDebuff(Moonfire) || API.TargetDebuffRemainingTime(Moonfire) <= 200) && API.TargetRange < 40 && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 30 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 24))
                                {
                                    API.CastSpell(Moonfire);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 11 && !API.PlayerHasBuff(Prowl) && API.CanCast(Thrash) && (!API.TargetHasDebuff(Thrash) || API.TargetDebuffRemainingTime(Thrash) <= 300) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Thrash);
                                    return;
                                }
                                if (isMelee && PlayerLevel >= 5 && (API.TargetDebuffRemainingTime(Thrash) > 300 || PlayerLevel < 11) && !API.PlayerHasBuff(Prowl) && (API.TargetDebuffRemainingTime(Rake) > 360 || PlayerLevel < 10) && API.CanCast(Shred) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Shred);
                                    return;
                                }
                            }
                        }
                    }
                    if (API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && IsAOE)
                    {
                        //Finisher
                        if (API.PlayerComboPoints > 4)
                        {
                            if (IsMouseover && (!isMouseoverInCombat || API.MouseoverIsIncombat) && API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0)
                            {
                                if (API.MouseoverDebuffRemainingTime(Rip) <= 360 && !API.MacroIsIgnored(Rip + "MO") && PlayerLevel >= 21 && API.CanCast(Rip) && !TalentPrimalWrath && isMOMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 16))
                                {
                                    API.CastSpell(Rip + "MO");
                                    return;
                                }
                            }
                            if (isMelee && PlayerLevel >= 7 && API.CanCast(FerociousBite) && !TalentPrimalWrath && API.PlayerEnergy >= 50 && (API.TargetHasDebuff(Rip) && API.TargetDebuffRemainingTime(Rip) > 300 || PlayerLevel < 21))
                            {
                                API.CastSpell(FerociousBite);
                                return;
                            }
                            if (TalentSavageRoar && isMelee && API.CanCast(SavageRoar) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 30 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 24) && !API.PlayerHasBuff(SavageRoar))
                            {
                                API.CastSpell(SavageRoar);
                                return;
                            }
                            if (isMelee && TalentPrimalWrath && API.CanCast(PrimalWrath) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 16))
                            {
                                API.CastSpell(PrimalWrath);
                                return;
                            }
                            if (isMelee && PlayerLevel >= 21 && !TalentPrimalWrath && API.CanCast(Rip) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 16) && (!API.TargetHasDebuff(Rip) || API.TargetDebuffRemainingTime(Rip) <= 300))
                            {
                                API.CastSpell(Rip);
                                return;
                            }
                        }
                        //Generator
                        if (API.PlayerComboPoints < 5)
                        {
                            if (Bloodytalons)
                            {
                                if (API.LastSpellCastInGame == Rake && !rakewatch.IsRunning)
                                {
                                    rakewatch.Start();
                                    //API.WriteLog("Starting Rakewatch.");
                                }
                                if ((API.LastSpellCastInGame == Thrash || API.LastSpellCastInGame == Thrashbear) && !thrashwatch.IsRunning)
                                {
                                    thrashwatch.Start(); thrashwatch.Start();
                                    //API.WriteLog("Starting thrashwatch.");
                                }
                                if (API.LastSpellCastInGame == BrutalSlash && !brutalwatch.IsRunning)
                                {
                                    brutalwatch.Start();
                                    //API.WriteLog("Starting brutalwatch.");
                                }
                                if (API.CanCast(Rake) && PlayerLevel >= 10 && !rakewatch.IsRunning && isMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
                                {
                                    API.CastSpell(Rake);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 11 && !thrashwatch.IsRunning && API.CanCast(Thrash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Thrash);
                                    return;
                                }
                                if (TalentBrutalSlash && !brutalwatch.IsRunning && API.TargetDebuffRemainingTime(Thrash) > 300 && isThrashMelee && API.CanCast(BrutalSlash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 25 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 20 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(BrutalSlash);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 42 && thrashwatch.IsRunning && rakewatch.IsRunning && !TalentBrutalSlash && API.CanCast(Swipe) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Clearcasting) || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
                                {
                                    API.CastSpell(Swipe);
                                    return;
                                }
                            }
                            if (!Bloodytalons)
                            {
                                if (isMelee && PlayerLevel >= 7 && API.CanCast(FerociousBite) && API.PlayerHasBuff(ApexPredatorsCraving))
                                {
                                    API.CastSpell(FerociousBite);
                                    return;
                                }
                                if (isThrashMelee && !API.PlayerHasBuff(Prowl) && API.TargetDebuffRemainingTime(Rake) > 360 && TalentBrutalSlash && API.CanCast(BrutalSlash) && (API.TargetHasDebuff(Thrash) || PlayerLevel < 11) && (API.PlayerHasBuff(Clearcasting) || !API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 25 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 15))
                                {
                                    API.CastSpell(BrutalSlash);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 11 && !API.PlayerHasBuff(Prowl) && API.CanCast(Thrash) && (!API.TargetHasDebuff(Thrash) || API.TargetDebuffRemainingTime(Thrash) <= 300) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Thrash);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 11 && !API.PlayerHasBuff(Prowl) && API.CanCast(Thrash) && TalentScentofBlood && !API.PlayerHasBuff(ScentofBlood) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Thrash);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 42 && !API.PlayerHasBuff(Prowl) && TalentScentofBlood && API.CanCast(Swipe) && API.PlayerHasBuff(ScentofBlood) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Clearcasting) || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
                                {
                                    API.CastSpell(Swipe);
                                    return;
                                }
                                if (API.CanCast(Rake) && PlayerLevel >= 10 && isMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28) && API.TargetDebuffRemainingTime(Rake) <= 360)
                                {
                                    API.CastSpell(Rake);
                                    return;
                                }
                                if (IsMouseover && (!isMouseoverInCombat || API.MouseoverIsIncombat) && API.PlayerCanAttackMouseover && API.MouseoverHealthPercent > 0)
                                {
                                    if (API.MouseoverDebuffRemainingTime(Rake) <= 360 && !API.MacroIsIgnored(Rake + "MO") && PlayerLevel >= 10 && API.CanCast(Rake) && isMOMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
                                    {
                                        API.CastSpell(Rake + "MO");
                                        return;
                                    }
                                    if (API.MouseoverDebuffRemainingTime(Thrash) <= 300 && !API.MacroIsIgnored(Thrash + "MO") && PlayerLevel >= 11 && API.CanCast(Thrash) && isMOThrashMelee && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                    {
                                        API.CastSpell(Thrash + "MO");
                                        return;
                                    }
                                }
                                if (API.CanCast(Moonfire) && !API.PlayerHasBuff(Prowl) && TalentLunarInspiration && API.LastSpellCastInGame != (Moonfire) && (!API.TargetHasDebuff(Moonfire) || API.TargetDebuffRemainingTime(Moonfire) <= 200) && API.TargetRange < 40 && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 30 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 24))
                                {
                                    API.CastSpell(Moonfire);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 42 && !API.PlayerHasBuff(Prowl) && !TalentBrutalSlash && API.CanCast(Swipe) && API.TargetHasDebuff(Thrash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 35 || API.PlayerHasBuff(Clearcasting) || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 28))
                                {
                                    API.CastSpell(Swipe);
                                    return;
                                }
                                if (isThrashMelee && PlayerLevel >= 11 && !API.PlayerHasBuff(Prowl) && API.CanCast(Thrash) && (!API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 40 || API.PlayerHasBuff(Incarnation) && API.PlayerEnergy >= 32 || API.PlayerHasBuff(Clearcasting)))
                                {
                                    API.CastSpell(Thrash);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}