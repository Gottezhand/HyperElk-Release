using System.Diagnostics;
using System.Threading;
namespace HyperElk.Core
{
    public class ClassicWarrior : CombatRoutine
    {
        private string Startattack = "Startattack";
        private string AspectoftheHawk = "Aspect of the Hawk";
        private string AspectoftheViper = "Aspect of the Viper";
        private string HuntersMark = "Hunter's Mark";
        private string ScorpidSting = "Scorpid Sting";
        private string RapidFire = "Rapid Fire";
        private string BestialWrath = "Bestial Wrath";
        private string KillCommand = "Kill Command";
        private string SteadyShot = "Steady Shot";
        private string MultiShot = "Multi-Shot";
        private string ExplosiveTrap = "Explosive Trap";
        private string Volley = "Volley";
        private string ArcaneShot = "Arcane Shot";
        private string SerpentSting = "Serpent Sting";
        private string ConcussiveShot = "Concussive Shot";
        private string WingClip = "Wind Clip";
        private string RaptorStrike = "Raptor Strike";
        private string TrueshotAura = "Trueshot Aura";
        private bool UseArcaneShot => CombatRoutine.GetPropertyBool(ArcaneShot);
        private bool UseSerpentSting => CombatRoutine.GetPropertyBool(SerpentSting);
        private bool UseConcussiveShot => CombatRoutine.GetPropertyBool(ConcussiveShot);
        private bool UseWingClip => CombatRoutine.GetPropertyBool(WingClip);
        private bool UseRaptorStrike => CombatRoutine.GetPropertyBool(RaptorStrike);
        private int AspectoftheViperManaProc => CombatRoutine.GetPropertyInt(AspectoftheViper);


        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Marksman Master";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Marksman Hunter by Mufflon12");
            API.WriteLog("Use /Cast [@Cursor] Explosive Trap");
            CombatRoutine.AddSpell(HuntersMark);
            CombatRoutine.AddSpell(ArcaneShot);
            CombatRoutine.AddSpell(SerpentSting);
            CombatRoutine.AddSpell(RaptorStrike);
            CombatRoutine.AddSpell(WingClip);
            CombatRoutine.AddSpell(ConcussiveShot);
            CombatRoutine.AddSpell(ScorpidSting);
            CombatRoutine.AddSpell(RapidFire);
            CombatRoutine.AddSpell(BestialWrath);
            CombatRoutine.AddSpell(KillCommand);
            CombatRoutine.AddSpell(ExplosiveTrap);
            CombatRoutine.AddSpell(SteadyShot);
            CombatRoutine.AddSpell(MultiShot);
            CombatRoutine.AddSpell(AspectoftheHawk);
            CombatRoutine.AddSpell(AspectoftheViper);
            CombatRoutine.AddSpell(TrueshotAura);

            CombatRoutine.AddBuff(TrueshotAura);
            CombatRoutine.AddBuff(AspectoftheHawk);
            CombatRoutine.AddBuff(AspectoftheViper);

            CombatRoutine.AddDebuff(ScorpidSting);
            CombatRoutine.AddDebuff(ConcussiveShot);
            CombatRoutine.AddDebuff(SerpentSting);
            CombatRoutine.AddDebuff(HuntersMark);

            CombatRoutine.AddProp(RaptorStrike, RaptorStrike, false, "Enable if you want to use" + RaptorStrike, "Meele");
            CombatRoutine.AddProp(WingClip, WingClip, false, "Enable if you want to use" + WingClip, "Meele");


            CombatRoutine.AddProp(ArcaneShot, ArcaneShot, false, "Enable if you want to use" + ArcaneShot, "Range");
            CombatRoutine.AddProp(SerpentSting, SerpentSting, false, "Enable if you want to use" + SerpentSting, "Range");
            CombatRoutine.AddProp(ConcussiveShot, ConcussiveShot, false, "Enable if you want to use" + ConcussiveShot, "Range");

            CombatRoutine.AddProp(AspectoftheViper, AspectoftheViper, 20, "Switch to " + AspectoftheViper + "blow XX Mana %, set to 0 to disable", "Aspects");


        }
        public override void Pulse()
        {

        }
        public override void CombatPulse()
        {
            if (API.CanCast(TrueshotAura) && !API.PlayerHasBuff(TrueshotAura))
            {
                API.CastSpell(TrueshotAura);
                return;
            }
            if (API.CanCast(AspectoftheViper) && !API.PlayerHasBuff(AspectoftheViper) && API.PlayerMana <= AspectoftheViperManaProc)
            {
                API.CastSpell(AspectoftheViper);
                return;
            }
            if (API.CanCast(AspectoftheHawk) && !API.PlayerHasBuff(AspectoftheHawk) && API.PlayerMana >= AspectoftheViperManaProc)
            {
                API.CastSpell(AspectoftheHawk);
                return;
            }
            if (API.CanCast(RapidFire) && IsCooldowns)
            {
                API.CastSpell(RapidFire);
                return;
            }
            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber && !API.PlayerIsCasting(true) && !API.PlayerIsAutoAttack && API.PetIsIncombat)
            {
                if (API.CanCast(MultiShot))
                {
                    API.CastSpell(MultiShot);
                    return;
                }
                if (API.CanCast(ExplosiveTrap))
                {
                    API.CastSpell(MultiShot);
                    return;
                }
                if (API.CanCast(Volley))
                {
                    API.CastSpell(Volley);
                    return;
                }
                if (API.CanCast(RaptorStrike) && UseRaptorStrike && API.TargetRange <= 8)
                {
                    API.CastSpell(RaptorStrike);
                    return;
                }
                if (API.CanCast(WingClip) && UseWingClip && API.TargetRange <= 8)
                {
                    API.CastSpell(WingClip);
                    return;
                }
                if (API.CanCast(HuntersMark) && !API.TargetHasDebuff(HuntersMark) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(HuntersMark);
                    return;

                }
                if (API.CanCast(ScorpidSting) && !API.TargetHasDebuff(ScorpidSting) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(ScorpidSting);
                    return;
                }
                if (API.CanCast(BestialWrath))
                {
                    API.CastSpell(BestialWrath);
                    return;
                }
                if (API.CanCast(KillCommand))
                {
                    API.CastSpell(KillCommand);
                    return;
                }
                if (API.CanCast(SteadyShot))
                {
                    API.CastSpell(SteadyShot);
                    return;
                }
                if (API.CanCast(MultiShot) && !API.CanCast(SteadyShot))
                {
                    API.CastSpell(MultiShot);
                    return;
                }
                if (API.CanCast(ConcussiveShot) && UseConcussiveShot && !API.TargetHasDebuff(ConcussiveShot) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(ConcussiveShot);
                    return;
                }
                if (API.CanCast(SerpentSting) && UseSerpentSting && !API.TargetHasDebuff(SerpentSting) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(SerpentSting);
                    return;
                }
                if (API.CanCast(ArcaneShot) && UseArcaneShot && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(ArcaneShot);
                    return;
                }
            }
            if ((IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE) && !API.PlayerIsCasting(true) && !API.PlayerIsAutoAttack && API.PetIsIncombat)
            {
                if (API.CanCast(RaptorStrike) && UseRaptorStrike && API.TargetRange <= 8)
                {
                    API.CastSpell(RaptorStrike);
                    return;
                }
                if (API.CanCast(WingClip) && UseWingClip && API.TargetRange <= 8)
                {
                    API.CastSpell(WingClip);
                    return;
                }
                if (API.CanCast(HuntersMark) && !API.TargetHasDebuff(HuntersMark) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(HuntersMark);
                    return;

                }
                if (API.CanCast(ScorpidSting) && !API.TargetHasDebuff(ScorpidSting) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(ScorpidSting);
                    return;
                }
                if (API.CanCast(BestialWrath))
                {
                    API.CastSpell(BestialWrath);
                    return;
                }
                if (API.CanCast(KillCommand))
                {
                    API.CastSpell(KillCommand);
                    return;
                }
                if (API.CanCast(SteadyShot))
                {
                    API.CastSpell(SteadyShot);
                    return;
                }
                if (API.CanCast(MultiShot) && !API.CanCast(SteadyShot))
                {
                    API.CastSpell(MultiShot);
                    return;
                }
                if (API.CanCast(ConcussiveShot) && UseConcussiveShot && !API.TargetHasDebuff(ConcussiveShot) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(ConcussiveShot);
                    return;
                }
                if (API.CanCast(SerpentSting) && UseSerpentSting && !API.TargetHasDebuff(SerpentSting) && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(SerpentSting);
                    return;
                }
                if (API.CanCast(ArcaneShot) && UseArcaneShot && API.TargetRange >= 10 && API.TargetRange <= 35)
                {
                    API.CastSpell(ArcaneShot);
                    return;
                }
            }
        }
        public override void OutOfCombatPulse()
        {

        }
    }
}
