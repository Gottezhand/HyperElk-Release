using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicTankWarrior : CombatRoutine
    {
        private string Startattack = "Startattack";
        private string BearForm = "Bear Form";
        private string FaerieFire = "Faerie Fire  (Feral)";
        private string Enrage = "Enrage";
        private string Lacerate = "Lacerate";
        private string Mangle = "Mangle  (Bear)";
        private string Swipe = "Swipe";
        private string DemoralizingRoar = "Demoralizing Roar";
        private string Maul = "Maul";
        private string Bash = "Bash";
        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Feral Tank";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Feral Tank by Mufflon12");

            CombatRoutine.AddSpell(BearForm);
            CombatRoutine.AddSpell(FaerieFire);
            CombatRoutine.AddSpell(Enrage);
            CombatRoutine.AddSpell(Lacerate);
            CombatRoutine.AddSpell(Mangle);
            CombatRoutine.AddSpell(Swipe);
            CombatRoutine.AddSpell(DemoralizingRoar);
            CombatRoutine.AddSpell(Maul);
            CombatRoutine.AddSpell(Bash);
            CombatRoutine.AddMacro(Startattack);
            CombatRoutine.AddDebuff(DemoralizingRoar);
            CombatRoutine.AddDebuff(Lacerate);
            CombatRoutine.AddDebuff(FaerieFire);

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
            if (API.CanCast(BearForm) && API.Classic_PlayerShapeShiftForm != 1)
            {
                API.CastSpell(BearForm);
                return;
            }
            if (API.CanCast(Bash) && isInterrupt && API.Classic_PlayerShapeShiftForm == 1)
            {
                API.CastSpell(Bash);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && API.Classic_PlayerShapeShiftForm == 1)
            {
                if (API.CanCast(DemoralizingRoar) && !API.TargetHasDebuff(DemoralizingRoar) && API.PlayerRage >= 10)
                {
                    API.CastSpell(DemoralizingRoar);
                    return;
                }
                if (API.CanCast(Mangle) && API.PlayerRage >= 20)
                {
                    API.CastSpell(Mangle);
                    return;
                }
                if (API.CanCast(Swipe) && API.PlayerRage >= 20)
                {
                    API.CastSpell(Swipe);
                    return;
                }
                if (API.CanCast(Maul) && API.PlayerRage >= 60 && API.TargetHasDebuff(FaerieFire))
                {
                    API.CastSpell(Maul);
                    return;
                }

            }
            if ((IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE) && API.Classic_PlayerShapeShiftForm == 1)
            {
                if (API.CanCast(FaerieFire) && !API.TargetHasDebuff(FaerieFire) && API.PlayerRage >= 30)
                {
                    API.CastSpell(FaerieFire);
                    return;
                }
                if (API.CanCast(DemoralizingRoar) && !API.TargetHasDebuff(DemoralizingRoar) && API.TargetHasDebuff(FaerieFire) && API.PlayerRage >= 10)
                {
                    API.CastSpell(DemoralizingRoar);
                    return;
                }
                if (API.CanCast(Enrage))
                {
                    API.CastSpell(Enrage);
                    return;
                }
                if (API.CanCast(Lacerate) && API.TargetDebuffStacks(Lacerate) < 5 && API.TargetHasDebuff(FaerieFire) && API.PlayerRage >= 15)
                {
                    API.CastSpell(Lacerate);
                    return;
                }
                if (API.CanCast(Mangle) && API.PlayerRage >= 20)
                {
                    API.CastSpell(Mangle);
                    return;
                }
                if (API.CanCast(Swipe) && API.PlayerRage >= 20 && API.TargetHasDebuff(FaerieFire))
                {
                    API.CastSpell(Swipe);
                    return;
                }
                if (API.CanCast(Maul) && API.PlayerRage >= 60 && API.TargetHasDebuff(FaerieFire))
                {
                    API.CastSpell(Maul);
                    return;
                }



            }
        }
        public override void OutOfCombatPulse()
        {

        }
    }
}
