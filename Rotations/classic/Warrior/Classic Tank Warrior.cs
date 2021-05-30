using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicTankWarrior : CombatRoutine
    {
        private string BattleShout = "Battle Shout";
        private string DemoralizingShout = "Demoralizing Shout";
        private string ThunderClap = "Thunder Clap";
        private string Rend = "Rend";
        private string HeroicStrike = "Heroic Strike";
        private string Startattack = "Startattack";
        private string SunderArmor = "Sunder Armor";
        private string SunderArmorStacks = "Sunder Armor Stacks";
        private string ShieldSlam = "Shield Slam";
        private string Revenge = "Revenge";
        private string ShieldBlock = "Shield Block";
        private string Devastate = "Devastate";
        private string Bloodrage = "Bloodrage";
        private string SpellReflection = "Spell Reflection";
        private string LastStand = "Last Stand";
        private string ShieldWall = "Shield Wall";
        private string Cleave = "Cleave";

        private string BattleStance = "Battle Stance";
        private string DefensiveStance = "Defensive Stance";
        private bool AutoStanceChange => CombatRoutine.GetPropertyBool("AutoStance");
        private bool UseRend => CombatRoutine.GetPropertyBool(Rend);
        private bool UseDemoralizingShout => CombatRoutine.GetPropertyBool(DemoralizingShout);
        private bool UseThunderClapt => CombatRoutine.GetPropertyBool(ThunderClap);

        private bool UseSunderArmor => CombatRoutine.GetPropertyBool(SunderArmor);
        private int SunderArmorStacksCount => numbList[CombatRoutine.GetPropertyInt(SunderArmorStacks)];
        private int ShieldBlockHealthPercent => CombatRoutine.GetPropertyInt(ShieldBlock);
        private bool UseBloodrage => CombatRoutine.GetPropertyBool(Bloodrage);

        private int BloodrageHealthPercent => CombatRoutine.GetPropertyInt("BloodragePercent");
        private int LastStandHealthPercent => CombatRoutine.GetPropertyInt(LastStand);

        int[] numbList = new int[] { 0, 1, 2, 3, 4, 5 };

        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Tank Warrior";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Tank Warrior by Mufflon12");

            CombatRoutine.AddSpell(BattleShout);
            CombatRoutine.AddSpell(DemoralizingShout);
            CombatRoutine.AddSpell(ThunderClap);
            CombatRoutine.AddSpell(ShieldSlam);
            CombatRoutine.AddSpell(Bloodrage);
            CombatRoutine.AddSpell(SpellReflection);
            CombatRoutine.AddSpell(Rend);
            CombatRoutine.AddSpell(Revenge);
            CombatRoutine.AddSpell(ShieldBlock);
            CombatRoutine.AddSpell(SunderArmor);
            CombatRoutine.AddSpell(HeroicStrike);
            CombatRoutine.AddSpell(LastStand);
            CombatRoutine.AddSpell(Cleave);
            CombatRoutine.AddSpell(BattleStance);
            CombatRoutine.AddSpell(DefensiveStance);

            CombatRoutine.AddBuff(BattleShout);

            CombatRoutine.AddDebuff(DemoralizingShout);
            CombatRoutine.AddDebuff(ThunderClap);
            CombatRoutine.AddDebuff(SunderArmor);
            CombatRoutine.AddDebuff(Rend, 11574);

            CombatRoutine.AddMacro(Startattack);

            CombatRoutine.AddProp("AutoStance", "Auto Stance", true, "Enable if you want to aut change Stance", "Class Specific");
            CombatRoutine.AddProp(Rend, "Rend ", false, "Enable if you want to use Rend", "Class Specific");

            CombatRoutine.AddProp(DemoralizingShout, DemoralizingShout, true, "Enable if you want to use Rend", "Tanking");
            CombatRoutine.AddProp(ThunderClap, ThunderClap, true, "Enable if you want to use Rend", "Tanking");

            CombatRoutine.AddProp(SunderArmor, "Sunder Armor ", false, "Enable if you want to use Sunder Armor", "Tanking");
            CombatRoutine.AddProp(SunderArmorStacks, SunderArmorStacks, numbList, " How many Stack of sunder Armor", "Tanking", 5);
            CombatRoutine.AddProp(ShieldBlock, "Shield Block Life %", 60, "Life Percent to use Shield Block", "Tanking");
            CombatRoutine.AddProp(LastStand, "Last Stand Life %", 60, "Life Percent to use Last Stand", "Tanking");

            CombatRoutine.AddProp(Bloodrage, Bloodrage, false, "Enable if you want to use Bloodrage", "Bloodrage");
            CombatRoutine.AddProp("BloodragePercent", "Bloodrage Health %", 60, "Dont Use Bloodrage below Health %", "Bloodrage");


        }
        public override void Pulse()
        {
        }
        public override void CombatPulse()
        {
            if (!API.PlayerIsAutoAttack)
            {
                API.CastSpell(Startattack);
                return;
            }
            if (API.CanCast(LastStand) && API.PlayerHealthPercent <= LastStandHealthPercent)
            {
                API.CastSpell(LastStand);
                return;
            }
            if (API.CanCast(SpellReflection) && API.TargetIsCasting(true) && API.TargetCurrentCastTimeRemaining < 500)
            {
                API.CastSpell(SpellReflection);
                return;
            }
            if (API.CanCast(Bloodrage) && UseBloodrage && API.PlayerHealthPercent >= BloodrageHealthPercent)
            {
                API.CastSpell(Bloodrage);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber)
            {
                if (API.Classic_PlayerShapeShiftForm != 2 && AutoStanceChange)
                {
                    API.CastSpell(DefensiveStance);
                    return;
                }
                if (API.CanCast(BattleShout) && !API.PlayerHasBuff(BattleShout) && API.PlayerRage >= 10)
                {
                    API.CastSpell(BattleShout);
                    return;
                }
                if (API.CanCast(DemoralizingShout) && UseDemoralizingShout && !API.TargetHasDebuff(DemoralizingShout) && API.PlayerRage >= 10)
                {
                    API.CastSpell(DemoralizingShout);
                    return;
                }
                if (API.CanCast(ThunderClap) && UseThunderClapt && !API.TargetHasDebuff(ThunderClap) && API.PlayerRage >= 20)
                {
                    API.CastSpell(ThunderClap);
                    return;
                }
                if (API.CanCast(Rend) && UseRend && !API.TargetHasDebuff(Rend) && API.PlayerRage >= 10)
                {
                    API.CastSpell(Rend);
                    return;
                }
                if (API.CanCast(ShieldSlam) && API.PlayerRage >= 10)
                {
                    API.CastSpell(ShieldSlam);
                    return;
                }
                if (API.CanCast(Revenge) && API.PlayerRage >= 5)
                {
                    API.CastSpell(Revenge);
                    return;
                }
                if (API.CanCast(ShieldBlock) && API.PlayerHealthPercent <= ShieldBlockHealthPercent)
                {
                    API.CastSpell(ShieldBlock);
                    return;
                }
                if (API.CanCast(SunderArmor) && UseSunderArmor && API.TargetDebuffStacks(SunderArmor) <= SunderArmorStacksCount && API.PlayerRage >= 15)
                {
                    API.CastSpell(SunderArmor);
                    return;
                }
                if (API.CanCast(Cleave) && API.PlayerRage >= 60 && API.Classic_PlayerMainSwingTimer == 0)
                {
                    API.CastSpell(Cleave);
                    return;
                }
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE)
            {
                if (API.Classic_PlayerShapeShiftForm != 2 && AutoStanceChange)
                {
                    API.CastSpell(DefensiveStance);
                    return;
                }
                if (API.CanCast(BattleShout) && !API.PlayerHasBuff(BattleShout) && API.PlayerRage >= 10)
                {
                    API.CastSpell(BattleShout);
                    return;
                }
                if (API.CanCast(DemoralizingShout) && UseDemoralizingShout && !API.TargetHasDebuff(DemoralizingShout) && API.PlayerRage >= 10)
                {
                    API.CastSpell(DemoralizingShout);
                    return;
                }
                if (API.CanCast(ThunderClap) && UseThunderClapt && !API.TargetHasDebuff(ThunderClap) && API.PlayerRage >= 20)
                {
                    API.CastSpell(ThunderClap);
                    return;
                }
                if (API.CanCast(Rend) && UseRend && !API.TargetHasDebuff(Rend) && API.PlayerRage >= 10)
                {
                    API.CastSpell(Rend);
                    return;
                }
                if (API.CanCast(ShieldSlam) && API.PlayerRage >= 10)
                {
                    API.CastSpell(ShieldSlam);
                    return;
                }
                if (API.CanCast(Revenge) && API.PlayerRage >= 5)
                {
                    API.CastSpell(Revenge);
                    return;
                }
                if (API.CanCast(ShieldBlock) && API.PlayerHealthPercent <= ShieldBlockHealthPercent)
                {
                    API.CastSpell(ShieldBlock);
                    return;
                }
                if (API.CanCast(SunderArmor) && UseSunderArmor && API.TargetDebuffStacks(SunderArmor) <= SunderArmorStacksCount && API.PlayerRage >= 15)
                {
                    API.CastSpell(SunderArmor);
                    return;
                }
                if (API.CanCast(HeroicStrike) && API.PlayerRage >= 60 && API.Classic_PlayerMainSwingTimer == 0)
                {
                    API.CastSpell(HeroicStrike);
                    return;
                }
            }

        }
        public override void OutOfCombatPulse()
        {
            if (API.Classic_PlayerShapeShiftForm != 1 && AutoStanceChange)
            {
                API.CastSpell(BattleStance);
                return;
            }
        }
    }
}
