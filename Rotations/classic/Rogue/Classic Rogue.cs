using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicWarrior : CombatRoutine
    {
        private string SinisterStrike = "Sinister Strike";
        private string SliceandDice = "Slice and Dice";
        private string Eviscerate = "Eviscerate";
        private string Gouge = "Gouge";
        private string Startattack = "Startattack";
        private string Evasion = "Evasion";
        private string ExposeArmor = "Expose Armor";
        private string Rupture = "Rupture";
        private string Kick = "Kick";
        private string BladeFlurry = "Blade Flurry";
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5 };
        private int SliceandDiceStackCount => numbList[CombatRoutine.GetPropertyInt(SliceandDice)];
        private int EviscerateStackCount => numbList[CombatRoutine.GetPropertyInt(Eviscerate)];
        private int ExposeArmorStackCount => numbList[CombatRoutine.GetPropertyInt(ExposeArmor)];
        private int RuptureStackCount => numbList[CombatRoutine.GetPropertyInt(Rupture)];

        private int EvasionHealthPercent => CombatRoutine.GetPropertyInt(Evasion);

        private bool UseGouge => CombatRoutine.GetPropertyBool(Gouge);
        private bool UseExposeArmor => CombatRoutine.GetPropertyBool("UseExposeArmor");


        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Rogue";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Rogue by Mufflon12");

            CombatRoutine.AddSpell(SliceandDice);
            CombatRoutine.AddSpell(Eviscerate);
            CombatRoutine.AddSpell(SinisterStrike);
            CombatRoutine.AddSpell(Gouge);
            CombatRoutine.AddSpell(ExposeArmor);
            CombatRoutine.AddSpell(Rupture);
            CombatRoutine.AddSpell(Kick);
            CombatRoutine.AddSpell(BladeFlurry);
            CombatRoutine.AddSpell(Evasion);

            CombatRoutine.AddBuff(Rupture);
            CombatRoutine.AddBuff(SliceandDice);
            CombatRoutine.AddBuff(Gouge);
            CombatRoutine.AddBuff(Eviscerate);

            CombatRoutine.AddMacro(Startattack);


            CombatRoutine.AddProp(SliceandDice, SliceandDice, numbList, " How many many Combo Points to use Slice and Dice", "Class Specific", 5);
            CombatRoutine.AddProp(Eviscerate, Eviscerate, numbList, " How many Combo Points to use Eviscerate", "Class Specific", 5);

            CombatRoutine.AddProp(ExposeArmor, ExposeArmor, numbList, " How many many Combo Points to use Expose Armor", "Class Specific", 5);
            CombatRoutine.AddProp("UseExposeArmor", "Expose Armor", true, "Enable if you want to use Expose Armor", "Class Specific");

            CombatRoutine.AddProp(Rupture, Rupture, numbList, " How many many Combo Points to use Rupture", "Class Specific", 2);

            CombatRoutine.AddProp(Gouge, Gouge, true, "Enable if you want to use Gouge", "Class Specific");
            CombatRoutine.AddProp(Evasion, Evasion, 60, "Life Percent to use Evasion", "Class Specific");

        }
        public override void Pulse()
        {

        }
        public override void CombatPulse()
        {
            if (API.CanCast(Evasion) && API.PlayerHealthPercent <= EvasionHealthPercent)
            {
                API.CastSpell(Evasion);
                return;
            }
            if (isInterrupt && API.CanCast(Kick))
            {
                API.CastSpell(Kick);
                return;
            }
            if (!API.PlayerIsAutoAttack)
            {
                API.CastSpell(Startattack);
                return;
            }
            if (IsCooldowns && API.CanCast(BladeFlurry))
            {
                API.CastSpell(BladeFlurry);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && API.TargetRange <= 5)
            {

                if (API.CanCast(SliceandDice) && API.PlayerComboPoints >= SliceandDiceStackCount && !API.PlayerHasBuff(SliceandDice))
                {
                    API.CastSpell(SliceandDice);
                    return;
                }
                if (API.CanCast(Rupture) && API.PlayerComboPoints >= RuptureStackCount && !API.TargetHasDebuff(Rupture))
                {
                    API.CastSpell(Rupture);
                    return;
                }
                if (API.CanCast(Eviscerate) && API.PlayerComboPoints >= EviscerateStackCount && API.PlayerHasBuff(SliceandDice))
                {
                    API.CastSpell(Eviscerate);
                    return;
                }
                if (API.CanCast(Gouge) && UseGouge)
                {
                    API.CastSpell(Gouge);
                    return;
                }
                if (API.CanCast(ExposeArmor) && UseExposeArmor && API.PlayerComboPoints >= ExposeArmorStackCount)
                {
                    API.CanCast(ExposeArmor);
                    return;
                }
                if (API.CanCast(SinisterStrike))
                {
                    API.CastSpell(SinisterStrike);
                    return;
                }
            }
            if ((IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE) && API.TargetRange <= 5)
            {
                if (API.CanCast(SliceandDice) && API.PlayerComboPoints >= SliceandDiceStackCount && !API.PlayerHasBuff(SliceandDice))
                {
                    API.CastSpell(SliceandDice);
                    return;
                }
                if (API.CanCast(Eviscerate) && API.PlayerComboPoints >= EviscerateStackCount && API.PlayerHasBuff(SliceandDice))
                {
                    API.CastSpell(Eviscerate);
                    return;
                }
                if (API.CanCast(Gouge) && UseGouge)
                {
                    API.CastSpell(Gouge);
                    return;
                }
                if (API.CanCast(ExposeArmor) && UseExposeArmor && API.PlayerComboPoints >= ExposeArmorStackCount)
                {
                    API.CanCast(ExposeArmor);
                    return;
                }
                if (API.CanCast(SinisterStrike))
                {
                    API.CastSpell(SinisterStrike);
                    return;
                }
            }

        }
        public override void OutOfCombatPulse()
        {

        }
    }
}
