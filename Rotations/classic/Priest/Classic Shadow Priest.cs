using System.Diagnostics;

namespace HyperElk.Core
{
    public class ClassicShadowPriest : CombatRoutine
    {
        private string Startattack = "Startattack";
        private string VampiricTouch = "Vampiric Touch";
        private string ShadowWordPain = "Shadow Word: Pain";
        private string MindBlast = "Mind Blast";
        private string Shadowfiend = "Shadowfiend";
        private string ShadowWordDeath = "Shadow Word:  Death";
        private string MindFlay = "Mind Flay";
        private string Shoot = "Shoot";
        private string InnerFire = "Inner Fire";
        private string PowerWordFortitude = "Power Word:  Fortitude";
        private string Stopcast = "Sopcast";
        private string SelfHeal = "Self Heal";
        private string FlashHeal = "FlashHeal";
        private string LesserHeal = "Lesser Heal";
        private string Heal = "Heal";
        private string Renew = "Renew";
        private string PowerWordShield = "Power Word:  Shield";
        private string Smite = "Smite";
        private string Fade = "Fade";
        private int ShadowfiendManaProc => CombatRoutine.GetPropertyInt(Shadowfiend);
        private int ShadowWordDeathHealthProc => CombatRoutine.GetPropertyInt(ShadowWordDeath);
        private bool UseShadowWordDeath => CombatRoutine.GetPropertyBool("UseShadowWordDeath");

        private int WandManaProc => CombatRoutine.GetPropertyInt(Shoot);
        private bool UseWand => CombatRoutine.GetPropertyBool("UseWand");
        private bool UseSmite => CombatRoutine.GetPropertyBool(Smite);

        private bool UseSelfHeal => CombatRoutine.GetPropertyBool(SelfHeal);
        private int FlashHealthProc => CombatRoutine.GetPropertyInt(FlashHeal);
        private int LesserHealHealthProc => CombatRoutine.GetPropertyInt(LesserHeal);
        private int HealHealHealthProc => CombatRoutine.GetPropertyInt(Heal);
        private int RenewHealthProc => CombatRoutine.GetPropertyInt(Renew);
        private int PowerWordShieldHealthProc => CombatRoutine.GetPropertyInt(PowerWordShield);

        private int FadedHealthProc => CombatRoutine.GetPropertyInt(Fade);

        public override void Initialize()
        {
            CombatRoutine.Name = "Classic Shadow Priest";
            CombatRoutine.TBCRotation = true;
            API.WriteLog("Classic Shadow Priest by Mufflon12");

            CombatRoutine.AddSpell(VampiricTouch);
            CombatRoutine.AddSpell(ShadowWordPain);
            CombatRoutine.AddSpell(MindBlast);
            CombatRoutine.AddSpell(Shadowfiend);
            CombatRoutine.AddSpell(ShadowWordDeath);
            CombatRoutine.AddSpell(MindFlay);
            CombatRoutine.AddSpell(Shoot);
            CombatRoutine.AddSpell(InnerFire);
            CombatRoutine.AddSpell(PowerWordFortitude);
            CombatRoutine.AddSpell(FlashHeal);
            CombatRoutine.AddSpell(LesserHeal);
            CombatRoutine.AddSpell(Heal);
            CombatRoutine.AddSpell(Renew);
            CombatRoutine.AddSpell(PowerWordShield);
            CombatRoutine.AddSpell(Smite);
            CombatRoutine.AddSpell(Fade);

            CombatRoutine.AddBuff(PowerWordShield);
            CombatRoutine.AddBuff(InnerFire);
            CombatRoutine.AddBuff(PowerWordFortitude);
            CombatRoutine.AddBuff(Renew);

            CombatRoutine.AddDebuff(ShadowWordDeath);
            CombatRoutine.AddDebuff(ShadowWordPain);
            CombatRoutine.AddDebuff(VampiricTouch);

            CombatRoutine.AddMacro(Startattack);
            CombatRoutine.AddMacro(Stopcast);

            CombatRoutine.AddProp(Smite, "Use " + Smite, true, "Enable if you want to use Smite", "Class specific");
            CombatRoutine.AddProp(Shadowfiend, Shadowfiend, 60, "Mana Percent to use Shadow fiend", "Class specific");
            CombatRoutine.AddProp(Fade, Fade, 25, "Health Percent to use Fade", "Class specific");

            CombatRoutine.AddProp(ShadowWordDeath, ShadowWordDeath, 80, "Dont use Shadow Word : Death below Health ", "Shadow Word Death");
            CombatRoutine.AddProp("UseShadowWordDeath", "Use" + ShadowWordDeath, true, "Enable if you want to use Shadow Word Death", "Shadow Word Death");

            CombatRoutine.AddProp("UseWand", "Use " + Shoot, true, "Enable if you want to use Shoot on low Mana", "Wand");
            CombatRoutine.AddProp(Shoot, Shoot + "Mana %", 80, "Use Shoot Below XX Mana % ", "Wand");


            CombatRoutine.AddProp(SelfHeal, "Self Heal ", true, "Enable if the Rotation should tuse Self Heal", "Healing");
            CombatRoutine.AddProp(PowerWordShield, PowerWordShield, 90, "Heal at XX Health % , set to 0 to disable", "Healing");
            CombatRoutine.AddProp(Renew, Renew, 75, "Heal at XX Health % , set to 0 to disable", "Healing");
            CombatRoutine.AddProp(FlashHeal, FlashHeal, 50, "Flash Heal at XX Health % , set to 0 to disable", "Healing");
            CombatRoutine.AddProp(LesserHeal, LesserHeal, 25, "Lesser Heal at XX Health % , set to 0 to disable", "Healing");
            CombatRoutine.AddProp(Heal, Heal, 10, "Heal at XX Health % , set to 0 to disable", "Healing");



        }
        public override void Pulse()
        {
            if (API.CanCast(InnerFire) && API.PlayerBuffStacks(InnerFire) < 5 && !API.PlayerIsMounted)
            {
                API.CastSpell(InnerFire);
                return;
            }
            if (API.CanCast(PowerWordFortitude) && !API.PlayerHasBuff(PowerWordFortitude) && !API.PlayerIsMounted)
            {
                API.CastSpell(PowerWordFortitude);
                return;
            }
        }
        public override void CombatPulse()
        {
            if (API.CanCast(Fade) && API.PlayerHealthPercent <= FadedHealthProc)
            {
                API.CastSpell(Fade);
                return;
            }
            if (UseSelfHeal && !API.PlayerIsCasting(false))
            {
                if (API.CanCast(PowerWordShield) && API.PlayerHealthPercent <= PowerWordShieldHealthProc && !API.PlayerHasBuff(PowerWordShield))
                {
                    API.CastSpell(PowerWordShield);
                    return;
                }
                if (API.CanCast(Renew) && API.PlayerHealthPercent <= RenewHealthProc && !API.PlayerHasBuff(Renew))
                {
                    API.CastSpell(Renew);
                    return;
                }
                if (API.CanCast(FlashHeal) && API.PlayerHealthPercent <= FlashHealthProc)
                {
                    API.CastSpell(FlashHeal);
                    return;
                }
                if (API.CanCast(LesserHeal) && API.PlayerHealthPercent <= LesserHealHealthProc)
                {
                    API.CastSpell(LesserHeal);
                    return;
                }
                if (API.CanCast(Heal) && API.PlayerHealthPercent <= HealHealHealthProc)
                {
                    API.CastSpell(Heal);
                    return;
                }
            }
            if (API.CanCast(Shadowfiend) && API.PlayerMana <= ShadowfiendManaProc)
            {
                API.CastSpell(Shadowfiend);
                return;
            }
            if (API.PlayerMana <= WandManaProc && UseWand && !API.PlayerIsAutoAttack && API.CanCast(Shoot))
            {
                API.CastSpell(Shoot);
                return;
            }
            if (API.PlayerMana <= WandManaProc && UseWand && API.PlayerIsCasting(true) && !API.PlayerIsAutoAttack)
            {
                API.CastSpell(Stopcast);
                return;
            }

            if (IsAOE && API.PlayerUnitInMeleeRangeCount >= AOEUnitNumber)
            {

            }
            if ((IsAOE && API.PlayerUnitInMeleeRangeCount <= AOEUnitNumber || !IsAOE) && (API.PlayerMana >= WandManaProc && UseWand || !UseWand))
            {
                if (API.CanCast(VampiricTouch) && API.TargetDebuffRemainingTime(VampiricTouch) < 200)
                {
                    API.CastSpell(VampiricTouch);
                    return;
                }
                if (API.CanCast(ShadowWordPain) && API.TargetDebuffRemainingTime(ShadowWordPain) < 200)
                {
                    API.CastSpell(ShadowWordPain);
                    return;
                }
                if (API.CanCast(ShadowWordDeath) && !API.TargetHasDebuff(ShadowWordDeath) && UseShadowWordDeath && API.PlayerHealthPercent >= ShadowWordDeathHealthProc)
                {
                    API.CastSpell(ShadowWordDeath);
                    return;
                }
                if (API.CanCast(MindBlast) && !API.PlayerIsCasting(false))
                {
                    API.CastSpell(MindBlast);
                    return;
                }
                if (API.CanCast(Smite) && UseSmite)
                {
                    API.CastSpell(Smite);
                    return;
                }
                if (API.CanCast(MindFlay) && !API.PlayerIsChanneling)
                {
                    API.CastSpell(MindFlay);
                    return;
                }
            }

        }
        public override void OutOfCombatPulse()
        {

        }
    }
}
