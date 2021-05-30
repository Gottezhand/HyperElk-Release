using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicFeralDruid : CombatRoutine
    {
        private string CatForm = "Cat Form";
        private string FaerieFire = "Faerie Fire";
        private string Startattack = "Startattack";
        private string TigersFury = "Tiger's Fury";
        private string Shred = "Shred";
        private string PowerShift = "Power Shift";
        private string FerociousBite = "Ferocious Bite";
        private string Hurricane = "Hurricane";
        private string TravelForm = "Travel Form";
        private string AutoTravelForm = "Auto Travel Form";
        private string Stealth = "Stealth";
        private string Claw = "Claw";
        private string Mangle = "Mangle  (Cat)";
        private string Rejuvenation = "Rejuvenation";
        private string Regrowth = "Regrowth";
        private string HealingTouch = "Healing Touch";
        private string SelfHeal = "Self Heal";
        private string MarkoftheWild = "Mark of the Wild";
        private string Rip = "Rip";
        int ShredCounter = 0;
        private bool UseAutoTravelForm => CombatRoutine.GetPropertyBool(AutoTravelForm);
        private bool UsePowerShift => CombatRoutine.GetPropertyBool(PowerShift);
        private bool UseSelfHeal => CombatRoutine.GetPropertyBool(SelfHeal);

        private int RejuvenationPercentProc => numbList[CombatRoutine.GetPropertyInt(Rejuvenation)];
        private int RegrowthPercentProc => numbList[CombatRoutine.GetPropertyInt(Regrowth)];
        private int HealingTouchPercentProc => numbList[CombatRoutine.GetPropertyInt(HealingTouch)];


        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 };

        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Feral Druid";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Feral Druid by Mufflon12");

            CombatRoutine.AddSpell(CatForm);
            CombatRoutine.AddSpell(FaerieFire);
            CombatRoutine.AddSpell(TigersFury);
            CombatRoutine.AddSpell(Shred);
            CombatRoutine.AddSpell(FerociousBite);
            CombatRoutine.AddSpell(TravelForm);
            CombatRoutine.AddSpell(Hurricane);
            CombatRoutine.AddSpell(MarkoftheWild);
            CombatRoutine.AddSpell(Rejuvenation);
            CombatRoutine.AddSpell(Regrowth);
            CombatRoutine.AddSpell(HealingTouch);
            CombatRoutine.AddSpell(Mangle, 33982);
            CombatRoutine.AddSpell(Rip);

            CombatRoutine.AddSpell(Claw);
            CombatRoutine.AddMacro(Startattack);
            CombatRoutine.AddMacro(PowerShift);
            CombatRoutine.AddBuff(MarkoftheWild);
            CombatRoutine.AddBuff(TigersFury);
            CombatRoutine.AddBuff(Stealth);
            CombatRoutine.AddBuff(Rejuvenation);
            CombatRoutine.AddBuff(Regrowth);

            CombatRoutine.AddDebuff(Mangle, 33982);

            CombatRoutine.AddProp(AutoTravelForm, "Auto Travel Form ", false, "Enable if you want to switch auto Switch to Travel Form", "Class Specific");
            CombatRoutine.AddProp(PowerShift, "Powershifting ", true, "Enable if the Rotation should try to Powershift", "Class Specific");

            CombatRoutine.AddProp(SelfHeal, "Self Heal ", true, "Enable if the Rotation should tuse Self Heal", "Healing");
            CombatRoutine.AddProp(Rejuvenation, Rejuvenation + " Life Percent", numbList, "Life percent at which" + Rejuvenation + "is, set to 0 to disable", "Healing", 85);
            CombatRoutine.AddProp(Regrowth, Regrowth + " Life Percent", numbList, "Life percent at which" + Regrowth + "is, set to 0 to disable", "Healing", 60);
            CombatRoutine.AddProp(HealingTouch, HealingTouch + " Life Percent", numbList, "Life percent at which" + HealingTouch + "is, set to 0 to disable", "Healing", 50);

        }
        public override void Pulse()
        {
            if (API.LastSpellCastInGame == Shred && UsePowerShift)
            {
                ShredCounter++;
                return;
            }
            if (!API.PlayerHasBuff(MarkoftheWild))
            {
                API.CastSpell(MarkoftheWild);
                return;
            }
        }
        public override void CombatPulse()
        {
            if (UseSelfHeal && !API.PlayerIsCasting(false))
            {
                if (API.CanCast(Rejuvenation) && API.PlayerHealthPercent <= RejuvenationPercentProc && !API.PlayerHasBuff(Rejuvenation))
                {
                    API.CastSpell(Rejuvenation);
                    return;
                }
                if (API.CanCast(Regrowth) && API.PlayerHealthPercent <= RegrowthPercentProc && !API.PlayerHasBuff(Regrowth))
                {
                    API.CastSpell(Regrowth);
                    return;
                }
                if (API.CanCast(HealingTouch) && API.PlayerHealthPercent <= HealingTouchPercentProc)
                {
                    API.CastSpell(HealingTouch);
                    return;
                }
            }
            if (!API.PlayerIsAutoAttack)
            {
                API.CastSpell(Startattack);
                return;
            }
            if (API.CanCast(CatForm) && API.Classic_PlayerShapeShiftForm != 3)
            {
                API.CastSpell(CatForm);
                return;
            }
            if (API.CanCast(TigersFury) && API.PlayerEnergy >= 100 && !API.PlayerHasBuff(TigersFury) && API.PlayerTimeInCombat <= 5000)
            {
                API.CastSpell(TigersFury);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && !API.PlayerHasBuff(Stealth))
            {
                if (API.PlayerUnitInMeleeRangeCount >= 5 && API.CanCast(Hurricane))
                {
                    API.CastSpell(Hurricane);
                    return;
                }
                if (API.CanCast(FerociousBite) && API.PlayerComboPoints == 4 && API.PlayerEnergy >= 30)
                {
                    API.CastSpell(FerociousBite);
                    return;
                }
                if (API.CanCast(FerociousBite) && API.PlayerComboPoints == 5 && API.PlayerEnergy >= 30)
                {
                    API.CastSpell(FerociousBite);
                    return;
                }
                if (API.CanCast(Rip) && (API.PlayerComboPoints >= 5 && API.PlayerEnergy >= 30 || API.PlayerComboPoints >= 4 && API.PlayerEnergy >= 30 && API.TargetDebuffRemainingTime(Mangle) <= 200))
                {
                    API.CastSpell(Rip);
                    return;
                }
                if (API.CanCast(Mangle) && API.PlayerEnergy >= 45 && !API.TargetHasDebuff(Mangle))
                {
                    API.CastSpell(Mangle);
                    return;
                }
                if (API.CanCast(Claw) && API.PlayerComboPoints < 5 && API.PlayerEnergy >= 35)
                {
                    API.CastSpell(Claw);
                    return;
                }
                if (API.PlayerIsInGroup)
                {
                    if (API.CanCast(FerociousBite) && API.PlayerComboPoints == 5 && API.PlayerEnergy >= 30)
                    {
                        API.CastSpell(FerociousBite);
                        return;
                    }
                    if (API.CanCast(Rip) && (API.PlayerComboPoints >= 5 && API.PlayerEnergy >= 30 || API.PlayerComboPoints >= 4 && API.PlayerEnergy >= 30 && API.TargetDebuffRemainingTime(Mangle) <= 200))
                    {
                        API.CastSpell(Rip);
                        return;
                    }
                    if (API.CanCast(Mangle) && API.PlayerEnergy >= 45 && !API.TargetHasDebuff(Mangle))
                    {
                        API.CastSpell(Mangle);
                        return;
                    }
                    if (API.CanCast(Claw) && API.PlayerComboPoints < 5 && API.PlayerEnergy >= 35)
                    {
                        API.CastSpell(Claw);
                        return;
                    }
                    if (API.CanCast(Shred) && API.PlayerEnergy >= 60 && (ShredCounter <= 2 && UsePowerShift || !UsePowerShift))
                    {
                        API.CastSpell(Shred);
                        return;
                    }
                    if (ShredCounter > 2 && API.CanCast(CatForm) && UsePowerShift)
                    {
                        API.CastSpell(PowerShift);
                        return;
                    }
                }

            }
            if ((IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE) && !API.PlayerHasBuff(Stealth))
            {
                if (API.CanCast(FerociousBite) && API.PlayerComboPoints == 5 && API.PlayerEnergy >= 30)
                {
                    API.CastSpell(FerociousBite);
                    return;
                }
                if (API.CanCast(Rip) && (API.PlayerComboPoints >= 5 && API.PlayerEnergy >= 30 || API.PlayerComboPoints >= 4 && API.PlayerEnergy >= 30 && API.TargetDebuffRemainingTime(Mangle) <= 200))
                {
                    API.CastSpell(Rip);
                    return;
                }
                if (API.CanCast(Mangle) && API.PlayerEnergy >= 45 && !API.TargetHasDebuff(Mangle))
                {
                    API.CastSpell(Mangle);
                    return;
                }
                if (API.CanCast(Claw) && API.PlayerComboPoints < 5 && API.PlayerEnergy >= 35)
                {
                    API.CastSpell(Claw);
                    return;
                }
                if (API.PlayerIsInGroup)
                {
                    if (API.CanCast(FerociousBite) && API.PlayerComboPoints == 5 && API.PlayerEnergy >= 30)
                    {
                        API.CastSpell(FerociousBite);
                        return;
                    }
                    if (API.CanCast(Rip) && (API.PlayerComboPoints >= 5 && API.PlayerEnergy >= 30 || API.PlayerComboPoints >= 4 && API.PlayerEnergy >= 30 && API.TargetDebuffRemainingTime(Mangle) <= 200))
                    {
                        API.CastSpell(Rip);
                        return;
                    }
                    if (API.CanCast(Mangle) && API.PlayerEnergy >= 45 && !API.TargetHasDebuff(Mangle))
                    {
                        API.CastSpell(Mangle);
                        return;
                    }
                    if (API.CanCast(Claw) && API.PlayerComboPoints < 5 && API.PlayerEnergy >= 35)
                    {
                        API.CastSpell(Claw);
                        return;
                    }
                    if (API.CanCast(Shred) && API.PlayerEnergy >= 60 && (ShredCounter <= 2 && UsePowerShift || !UsePowerShift))
                    {
                        API.CastSpell(Shred);
                        return;
                    }
                    if (ShredCounter > 2 && API.CanCast(CatForm) && UsePowerShift)
                    {
                        API.CastSpell(PowerShift);
                        return;
                    }
                }

            }

        }
        public override void OutOfCombatPulse()
        {
            if (API.CanCast(TravelForm) && UseAutoTravelForm && API.Classic_PlayerShapeShiftForm != 4 && API.PlayerIsOutdoor)
            {
                API.CastSpell(TravelForm);
                return;
            }
        }
    }
}
