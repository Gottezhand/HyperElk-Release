using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicWarrior : CombatRoutine
    {
        private string BattleShout = "Battle Shout";
        private string Bloodrage = "Bloodrage";
        private string DemoralizingShout = "Demoralizing Shout";
        private string Execute = "Execute";
        private string Bloodthirst = "Bloodthirst";
        private string Rend = "Rend";
        private string HeroicStrike = "Heroic Strike";
        private string Cleave = "Cleave";
        private string Whirlwind = "Whirlwind";
        private string BerserkerRage = "Berserker Rage";
        private string Pummel = "Pummel";
        private string Startattack = "Startattack";
        private string SweepingStrikes = "Sweeping Strikes";
        private string SunderArmor = "Sunder Armor";
        private string SunderArmorStacks = "Sunder Armor Stacks";

        private string BattleStance = "Battle Stance";
        private string BerserkerStance = "Berserker Stance";
        bool BattleWatch = false;
        private bool AutoStanceChange => CombatRoutine.GetPropertyBool("AutoStance");
        private bool UseBerserkerRage => CombatRoutine.GetPropertyBool(BerserkerRage);
        private bool UseRend => CombatRoutine.GetPropertyBool(Rend);
        private bool UseSunderArmor => CombatRoutine.GetPropertyBool(SunderArmor);
        private int SunderArmorStacksCount => numbList[CombatRoutine.GetPropertyInt(SunderArmorStacks)];

        private static readonly Stopwatch HeroWatch = new Stopwatch();
        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5 };

        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Warrior";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Warrior by Mufflon12");

            CombatRoutine.AddSpell(BattleShout);
            CombatRoutine.AddSpell(Bloodrage);
            CombatRoutine.AddSpell(DemoralizingShout);
            CombatRoutine.AddSpell(Execute);
            CombatRoutine.AddSpell(Bloodthirst);
            CombatRoutine.AddSpell(Rend);
            CombatRoutine.AddSpell(BattleStance);
            CombatRoutine.AddSpell(BerserkerStance);
            CombatRoutine.AddSpell(HeroicStrike);
            CombatRoutine.AddSpell(Cleave);
            CombatRoutine.AddSpell(Whirlwind);
            CombatRoutine.AddSpell(Pummel);
            CombatRoutine.AddSpell(BerserkerRage);
            CombatRoutine.AddSpell(SweepingStrikes);
            CombatRoutine.AddSpell(SunderArmor);
            CombatRoutine.AddBuff(BattleShout);

            CombatRoutine.AddDebuff(DemoralizingShout);
            CombatRoutine.AddDebuff(Rend);

            CombatRoutine.AddMacro(Startattack);

            CombatRoutine.AddProp("AutoStance", "Auto Stance", true, "Enable if you want to aut change Stance", "Class Specific");
            CombatRoutine.AddProp(BerserkerRage, "Berserker Rage ", true, "Enable if you want to use Berserker Rage", "Class Specific");
            CombatRoutine.AddProp(Rend, "Rend ", false, "Enable if you want to use Rend", "Class Specific");
            CombatRoutine.AddProp(SunderArmor, "Sunder Armor ", false, "Enable if you want to use Sunder Armor", "Class Specific");
            CombatRoutine.AddProp(SunderArmorStacks, SunderArmorStacks, numbList, " How many Stack of sunder Armor", "Class Specific", 5);

        }
        public override void Pulse()
        {
            if (HeroWatch.IsRunning && API.LastSpellCastInGame == HeroicStrike)
            {
                HeroWatch.Reset();
            }
        }
        public override void CombatPulse()
        {
            if (!API.PlayerIsAutoAttack)
            {
                API.CastSpell(Startattack);
                return;
            }
            if (isInterrupt && API.CanCast(Pummel) && BattleWatch == false)
            {
                API.CastSpell(Pummel);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && API.Classic_PlayerMainSwingTimer >= 0)
            {
                if (API.Classic_PlayerShapeShiftForm == 1 && UseRend && API.CanCast(Rend) && API.PlayerRage >= 10)
                {
                    API.CastSpell(Rend);
                    return;
                }
                if (API.TargetHasDebuff(Rend) && API.TargetRange < 5 && API.TargetDebuffRemainingTime(Rend) > 300 && API.Classic_PlayerShapeShiftForm == 1 && AutoStanceChange)
                {
                    API.CastSpell(BerserkerStance);
                    return;
                }
                if (API.CanCast(Bloodrage))
                {
                    API.CastSpell(Bloodrage);
                    return;
                }
                if (API.CanCast(BerserkerRage) && API.Classic_PlayerShapeShiftForm == 3 && UseBerserkerRage)
                {
                    API.CastSpell(BerserkerRage);
                    return;
                }
                if (API.CanCast(SweepingStrikes) && (API.Classic_PlayerShapeShiftForm == 1 || API.Classic_PlayerShapeShiftForm == 3) && API.PlayerRage >= 30)
                {
                    API.CastSpell(SweepingStrikes);
                    return;
                }
                if (API.CanCast(Execute))
                {
                    API.CastSpell(Execute);
                    return;
                }
                if (API.CanCast(BattleShout) && !API.PlayerHasBuff(BattleShout) && API.PlayerRage >= 10 && API.TargetRange < 10)
                {
                    API.CastSpell(BattleShout);
                    return;
                }
                if (API.CanCast(DemoralizingShout) && !API.TargetHasDebuff(DemoralizingShout) && API.PlayerRage >= 10)
                {
                    API.CastSpell(DemoralizingShout);
                    return;
                }
                if (API.CanCast(Bloodthirst) && API.PlayerRage >= 30)
                {
                    API.CastSpell(Bloodthirst);
                    return;
                }
                if (API.CanCast(Whirlwind) && API.PlayerUnitInMeleeRangeCount >= 3 && BattleWatch == false && API.PlayerRage >= 25 && API.TargetRange <= 5)
                {
                    API.CastSpell(Whirlwind);
                    return;
                }
                if (API.CanCast(Cleave) && !API.CanCast(Whirlwind) && API.PlayerRage >= 50)
                {
                    API.CastSpell(Cleave);
                    return;
                }
            }
            if ((IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE) && API.Classic_PlayerMainSwingTimer >= 0)
            {
                if (API.Classic_PlayerShapeShiftForm == 1 && UseRend && API.CanCast(Rend) && API.PlayerRage >= 10)
                {
                    API.CastSpell(Rend);
                    return;
                }
                if ((UseRend && API.TargetHasDebuff(Rend) && API.TargetRange < 5 && API.TargetDebuffRemainingTime(Rend) > 300 || !UseRend) && API.Classic_PlayerShapeShiftForm == 1 && AutoStanceChange)
                {
                    API.CastSpell(BerserkerStance);
                    return;
                }
                if (API.CanCast(Bloodrage))
                {
                    API.CastSpell(Bloodrage);
                    return;
                }
                if (API.CanCast(BerserkerRage) && API.Classic_PlayerShapeShiftForm == 3 && UseBerserkerRage)
                {
                    API.CastSpell(BerserkerRage);
                    return;
                }
                if (API.CanCast(Execute))
                {
                    API.CastSpell(Execute);
                    return;
                }
                if (API.CanCast(BattleShout) && !API.PlayerHasBuff(BattleShout) && API.PlayerRage >= 10 && API.TargetRange < 10)
                {
                    API.CastSpell(BattleShout);
                    return;
                }
                if (API.CanCast(DemoralizingShout) && !API.TargetHasDebuff(DemoralizingShout) && API.PlayerRage >= 10)
                {
                    API.CastSpell(DemoralizingShout);
                    return;
                }
                if (API.CanCast(SunderArmor) && UseSunderArmor && API.TargetDebuffStacks(SunderArmor) <= SunderArmorStacksCount && API.PlayerRage >= 15)
                {
                    API.CastSpell(SunderArmor);
                    return;
                }
                if (API.CanCast(Bloodthirst) && API.PlayerRage >= 30)
                {
                    API.CastSpell(Bloodthirst);
                    return;
                }
                if (API.CanCast(HeroicStrike) && !API.CanCast(Bloodthirst) && API.PlayerRage >= 42 && !API.PlayerIsCasting(false) && API.Classic_PlayerMainSwingTimer == 0)
                {
                    API.CastSpell(HeroicStrike);
                    HeroWatch.Start();
                    return;
                }
            }
            
        }
        public override void OutOfCombatPulse()
        {
            if (API.Classic_PlayerShapeShiftForm == 3 && AutoStanceChange)
            {
                API.CastSpell(BattleStance);
                return;
            }
        }
    }
}
