using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicTankWarrior : CombatRoutine
    {
        private string Startattack = "Startattack";
        private string RighteousFury = "Righteous Fury";
        private string HammerofWrath = "Hammer of Wrath";
        private string Judgement = "Judgement";
        private string AvengesShield = "Avenger's Shield";
        private string Consecration = "Consecration";
        private string HolyShield = "Holy Shield";
        private string AvengingWrath = "Avenging Wrath";
        private string SealoftheCrusader = "Seal of the Crusader";
        private string SealofVengeance = "Seal of Vengeance";
        private string SealofWisdom = "Seal of Wisdom";
        private string SealofRighteousness = "Seal of Righteousness";
        private string DivineShield = "Divine Shield";
        private string Auras = "Auras";
        private string DevotionAura = "Devotion Aura";
        private string RetributionAura = "Retribution Aura";
        private string SanctityAura = "Sanctity Aura";
        private string CrusaderAura = "Crusader Aura";
        private bool UseAutoAura => CombatRoutine.GetPropertyBool("AutoAuraSwitch");

        private bool UseSealoftheCrusader => CombatRoutine.GetPropertyBool(SealoftheCrusader);
        private bool UseSealofVengeance => CombatRoutine.GetPropertyBool(SealofVengeance);
        private bool UseSealofWisdom => CombatRoutine.GetPropertyBool(SealofWisdom);
        private int SealofWisdomManaProc => CombatRoutine.GetPropertyInt("SealofWisdomMana");
        private bool UseCrusaderAura => CombatRoutine.GetPropertyBool(CrusaderAura);

        private int DivineShieldHealthProc => CombatRoutine.GetPropertyInt(DivineShield);
        string[] AuraList = new string[] { "None", "Devotion Aura", "Retribution Aura" };

        private string UseAura => AuraList[CombatRoutine.GetPropertyInt(Auras)];
        bool IsDevotionAura => UseAura == "Devotion Aura";
        bool IsRetributionAura => UseAura == "Retribution Aura";

        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Protection Paladin";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Protection Paladin by Mufflon12");

            CombatRoutine.AddSpell(RighteousFury);
            CombatRoutine.AddSpell(HammerofWrath);
            CombatRoutine.AddSpell(Judgement);
            CombatRoutine.AddSpell(AvengesShield);
            CombatRoutine.AddSpell(Consecration);
            CombatRoutine.AddSpell(HolyShield);
            CombatRoutine.AddSpell(AvengingWrath);
            CombatRoutine.AddSpell(SealoftheCrusader);
            CombatRoutine.AddSpell(SealofVengeance);
            CombatRoutine.AddSpell(SealofRighteousness);
            CombatRoutine.AddSpell(DivineShield);
          
            CombatRoutine.AddSpell(CrusaderAura);     
            CombatRoutine.AddSpell(DevotionAura);
            CombatRoutine.AddSpell(RetributionAura);


            CombatRoutine.AddDebuff(SealofRighteousness);
            CombatRoutine.AddDebuff(SealofVengeance);
            CombatRoutine.AddDebuff(SealoftheCrusader);
            CombatRoutine.AddDebuff(Consecration);

            CombatRoutine.AddBuff(HolyShield);
            CombatRoutine.AddDebuff(Judgement);
            CombatRoutine.AddBuff(RighteousFury);




            CombatRoutine.AddMacro(Startattack);

            CombatRoutine.AddProp(SealoftheCrusader, SealoftheCrusader, false, "Enable if you want to use " + SealoftheCrusader, "Class Specific");
            CombatRoutine.AddProp(SealofVengeance, SealofVengeance, false, "Enable if you want to use " + SealofVengeance, "Class Specific");

            CombatRoutine.AddProp(SealofWisdom, SealofWisdom, false, "Enable if you want to use " + SealofWisdom, "Seal of Wisdom");
            CombatRoutine.AddProp(SealofWisdom, SealofWisdom + "Mana %", 50, "Seal of Wisdom below XX Mana % ", "Seal of Wisdom");

            CombatRoutine.AddProp(DivineShield, DivineShield + "Health %", 50, "Health % to use " + DivineShield, "Healing");

            CombatRoutine.AddProp(Auras, "Wich Aura", AuraList, "Chose your Aura", "Aura", 0);
            CombatRoutine.AddProp(CrusaderAura, CrusaderAura, false, "Enable if you want to switch to " + CrusaderAura + " When Mounted", "Auras");
            CombatRoutine.AddProp("AutoAuraSwitch", "Auto Aura Switch", false, "Enable if you want to Auto Switch Auras", "Auras");

        }
        public override void Pulse()
        {
            if (API.CanCast(CrusaderAura) && UseAutoAura && UseCrusaderAura && API.Classic_PlayerShapeShiftForm !=7 && API.PlayerIsMounted)
            {
                API.CastSpell(CrusaderAura);
                return;
            }
            if (API.CanCast(DevotionAura) && UseAutoAura && API.Classic_PlayerShapeShiftForm != 1 && !API.PlayerIsMounted && IsDevotionAura)
            {
                API.CastSpell(DevotionAura);
                return;
            }
            if (API.CanCast(RetributionAura) && UseAutoAura && API.Classic_PlayerShapeShiftForm != 2 && !API.PlayerIsMounted && IsRetributionAura)
            {
                API.CastSpell(RetributionAura);
                return;
            }

        }
        public override void CombatPulse()
        {
            if (API.CanCast(DivineShield) && API.PlayerHealthPercent <= DivineShieldHealthProc)
            {
                API.CastSpell(DivineShield);
                return;
            }
            if (API.CanCast(RighteousFury) && !API.PlayerHasBuff(RighteousFury))
            {
                API.CastSpell(RighteousFury);
                return;
            }
            if (IsCooldowns && API.CanCast(AvengingWrath))
            {
                API.CastSpell(AvengingWrath);
                return;
            }
            if (API.CanCast(HammerofWrath) && API.TargetHealthPercent <= 20)
            {
                API.CastSpell(HammerofWrath);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber)
            {
                if (API.CanCast(SealofRighteousness) && API.PlayerTimeInCombat < 500 && !API.TargetHasDebuff(SealofRighteousness))
                {
                    API.CastSpell(SealofRighteousness);
                    return;
                }
                if (API.CanCast(SealofWisdom) && !API.TargetHasDebuff(SealofWisdom) && UseSealofWisdom && API.PlayerMana < SealofWisdomManaProc)
                {
                    API.CastSpell(SealofWisdom);
                    return;
                }
                if (API.CanCast(SealofVengeance) && !API.TargetHasDebuff(SealofVengeance) && UseSealofVengeance && API.PlayerMana > SealofWisdomManaProc)
                {
                    API.CastSpell(SealofVengeance);
                    return;
                }
                if (API.CanCast(SealoftheCrusader) && !API.TargetHasDebuff(SealoftheCrusader) && UseSealoftheCrusader && API.PlayerMana > SealofWisdomManaProc)
                {
                    API.CastSpell(SealoftheCrusader);
                    return;
                }
                if (API.CanCast(HolyShield) && !API.PlayerHasBuff(HolyShield))
                {
                    API.CastSpell(HolyShield);
                    return;
                }
                if (API.CanCast(Consecration) && !API.TargetHasDebuff(Consecration))
                {
                    API.CastSpell(Consecration);
                    return;
                }
                if (API.CanCast(AvengesShield))
                {
                    API.CastSpell(AvengesShield);
                    return;
                }
                if (API.CanCast(Judgement) && API.TargetDebuffRemainingTime(Judgement) < 100)
                {
                    API.CastSpell(Judgement);
                    return;
                }
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE)
            {
                if (API.CanCast(SealofRighteousness) && API.PlayerTimeInCombat < 500 && !API.TargetHasDebuff(SealofRighteousness))
                {
                    API.CastSpell(SealofRighteousness);
                    return;
                }
                if (API.CanCast(SealofWisdom) && !API.TargetHasDebuff(SealofWisdom) && UseSealofWisdom && API.PlayerMana < SealofWisdomManaProc)
                {
                    API.CastSpell(SealofWisdom);
                    return;
                }
                if (API.CanCast(SealofVengeance) && !API.TargetHasDebuff(SealofVengeance) && UseSealofVengeance && API.PlayerMana > SealofWisdomManaProc)
                {
                    API.CastSpell(SealofVengeance);
                    return;
                }
                if (API.CanCast(SealoftheCrusader) && !API.TargetHasDebuff(SealoftheCrusader) && UseSealoftheCrusader && API.PlayerMana > SealofWisdomManaProc)
                {
                    API.CastSpell(SealoftheCrusader);
                    return;
                }
                if (API.CanCast(HolyShield) && !API.PlayerHasBuff(HolyShield))
                {
                    API.CastSpell(HolyShield);
                    return;
                }
                if (API.CanCast(Consecration) && !API.TargetHasDebuff(Consecration))
                {
                    API.CastSpell(Consecration);
                    return;
                }
                if (API.CanCast(AvengesShield))
                {
                    API.CastSpell(AvengesShield);
                    return;
                }
                if (API.CanCast(Judgement) && API.TargetDebuffRemainingTime(Judgement) < 100)
                {
                    API.CastSpell(Judgement);
                    return;
                }
            }

        }
        public override void OutOfCombatPulse()
        {

        }
    }
}
