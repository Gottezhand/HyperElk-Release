namespace HyperElk.Core
{
    public class BloodDK : CombatRoutine
    {
        /* DK Spells & Buffs */
        private string Marrowrend = "Marrowrend";
        private string BloodBoil = "Blood Boil";
        private string DeathStrike = "Death Strike";
        private string HeartStrike = "Heart Strike";
        private string AntiMagicShell = "Anti-Magic Shell";
        private string VampiricBlood = "Vampiric Blood";
        private string IceboundFortitude = "Icebound Fortitude";
        private string DancingRuneWeapon = "Dancing Rune Weapon";
        private string DeathAndDecay = "Death and Decay";
        private string MindFreeze = "Mind Freeze";
        private string RuneTap = "Rune Tap";
        private string RaiseDead = "Raise Dead";
        private string AntiMagicZone = "Anti-Magic Zone";
        private string Tombstone = "Tombstone";
        private string BloodTap = "Blood Tap";
        private string Bonestorm = "Bonestorm";
        private string BoneShield = "Bone Shield";
        private string Ossuary = "Ossuary";
        private string Haemostasis = "Haemostasis";
        private string BloodShield = "Blood Shield";
        private string BloodPlague = "Blood Plague";
        private string CrimsonScourge = "Crimson Scourge";
        private string SacrificialPact = "Sacrificial Pact";
        private string ChainsOfIce = "Chains of Ice";
        private string MarkOfBlood = "Mark of Blood";
        private string Asphyxiate = "Asphyxiate";
        private string AbominationLimb = "Abomination Limb";
        private string Lichborne = "Lichborne";
        private string Fleshcraft = "Fleshcraft";
        private string WraithWalk = "Wraith Walk";
        private string DeathChain = "Death Chain";
        private string DeathsCaress = "Death\'s Caress";
        private string DeathCoil = "Death Coil";
        private string Strangulate = "Strangulate";
        private string BloodBarrier = "Blood Barrier";
        private string DarkSimulacrum = "Dark Simulacrum";
        private string Blooddrinker = "Blooddrinker";
        private string MurderousIntent = "Murderous Intent";
        private string FocusedAssault = "Focused Assault";
        private string WillOfTheForsaken = "Will of the Forsaken";
        private string WillToSurvive = "Will to Survive";
        private string SwarmingMist = "Swarming Mist";
        private string ShackleTheUnworthy = "Shackle the Unworthy";
        private string WarStomp = "War Stomp";
        private string BloodFury = "Blood Fury";
        private string AncestralCall = "Ancestral Call";
        private string QuakingPalm = "Quaking Palm";
        private string Mindgames = "Mindgames";
        private string Interrupt = "Interrupt";
        private string HardenedBones = "Hardened Bones";
        private string ForgeborneReveries = "Forgeborne Reveries";
        private string SoulExhaustion = "Soul Exhaustion";
        private string CruciformAxe = "Cruciform Axe";
        private string ReverberatingHammer = "Reverberating Hammer";
        private string DualbladeScythe = "Dualblade Scythe";
        private string BansheeShroud = "Banshee Shroud";
        private string DeathPact = "Death Pact";
        private string ShroudOfPurgatory = "Shroud of Purgatory";
        private string Trinket1 = "Trinket1";
        private string Trinket2 = "Trinket2";
        private string InterruptPriority = "Interrupt Priority";
        private string RemoveFear = "Remove Fear";
        private string RemoveStun = "Remove Stun";
        private string AMSPriority = "AMS Priority";
        private string MovingFleshcraft = "Moving Fleshcraft";
        private string DeathAndDecayUsage = "Death and Decay Usage";
        private string MitigationManagement = "Mitigation Management";
        string[] TrinketSettings = { "Never", "On Cooldown", "Offensive Cooldowns", "Defensive Cooldowns", "Offensive and Defensive Cooldowns" };
        string[] InterruptPrioritySettings = { "High", "Normal" };
        string[] DeathAndDecayUsageSettings = { "Automatic", "Manual" };
        string[] RemoveFearSettings = { "Never", "With Lichborne", "With Racial", "With Lichborne and Racial" };
        string[] RemoveStunSettings = { "Never", "With Icebound Fortitude", "With Racial", "With Icebound Fortitude and Racial" };
        string[] AMSPrioritySettings = { "High", "Low", "Disabled" };
        string[] MovingFleshcraftSettings = { "Yes", "No" };
        string[] MitigationManagementSettings = { "Heavy", "Small" };

        /* Monsters Buffs, Debuffs & Casts */
        int[] HeavyDamageCasts = {  320655, 324394, 338456, 320696, 334488, 338636, 320771, 322557, 340289, 340208, 
                                    340300, 317943, 320966, 323744, 324608, 322736, 320144, 332239, 332181, 336005, 
                                    330713, 329774, 325382, 334929, 341623, 341625, 350828, 350422, 352650, 353969,
                                    353603, 352663, 352649, 352651, 352652, 340016, 327646, 327882, 324527, 325552, 
                                    321178, 319650, 322429, 334326, 335308, 321249, 325254, 326718, 337178, 320069, 
                                    330565, 331316, 331288, 333845, 320644, 330697, 332836, 323515, 324079, 342425, 
                                    332619, 328857, 346681, 346762, 346705, 346685, 353605, 348953, 356923, 351603,
                                    350916, 355477, 348128, 352796, 355889, 355048, 346116, 357281, 351591, 351589 };
        int[] ShellSpellList = {    325701, 321807, 327646, 327882, 324527, 320529, 329110, 325552, 322429, 
                                    320069, 331288, 333845, 330697, 332836, 350894, 349890, 353603, 322232 };
        int[] CastsToStunList = {   326450, 328177, 321935, 336451, 328651, 328400, 322169, 333540, 330586, 357260,
                                    332181, 325701, 325700, 324609, 338022, 334747, 320822, 321807, 322569, 331743,
                                    324987, 325021, 320512, 328429, 355934, 356031, 356407, 355640, 347775, 353835,
                                    350922, 350922, 355132, 357284 };
        int[] DarkSimulacrumList = { 118, 33786, 5782, 203286 };
        string[] ShellDebuffList = { "Jagged Swipe", "Stony Veins", "Rake", "Gushing Wound", "Buzz-Saw", "Beak Slice", "Arcane Lightning", "Decaying Blight", "Change of Heart", "Blackened Armor", "Spreading Misery", "Fragments of Destiny" };
        string[] IceboundFortitudeBuffList = { "Loyal Beast", "Undying Rage", "Rage" };

        /* Consummables */
        private string SpiritualHealingPotion = "Spiritual Healing Potion";

        /* Toggles */
        private string BattleRes = "Battle Res";
        private string Potions = "Potions";
        private string PVP = "PVP";
        private string Range = "Range";

        /* Settings */
        private string TrinketSetting1 => TrinketSettings[CombatRoutine.GetPropertyInt(Trinket1)];
        private string TrinketSetting2 => TrinketSettings[CombatRoutine.GetPropertyInt(Trinket2)];
        private string DeathAndDecayUsageSetting => DeathAndDecayUsageSettings[CombatRoutine.GetPropertyInt(DeathAndDecayUsage)];
        private string InterruptPrioritySetting => InterruptPrioritySettings[CombatRoutine.GetPropertyInt(InterruptPriority)];
        private string RemoveFearSetting => RemoveFearSettings[CombatRoutine.GetPropertyInt(RemoveFear)];
        private string RemoveStunSetting => RemoveStunSettings[CombatRoutine.GetPropertyInt(RemoveStun)];
        private string AMSPrioritySetting => AMSPrioritySettings[CombatRoutine.GetPropertyInt(AMSPriority)];
        private string MovingFleshcraftSetting => MovingFleshcraftSettings[CombatRoutine.GetPropertyInt(MovingFleshcraft)];
        private string MitigationManagementSetting => MitigationManagementSettings[CombatRoutine.GetPropertyInt(MitigationManagement)];

        public override void Initialize()
        {
            CombatRoutine.Name = "Blood DK @Elbob";
            API.WriteLog("TOGGLE EXPLANATION");
            API.WriteLog("AOE: when toggled on, the CR will use blood boil when at least 50% of the monsters around you are in range of blood boil. Kinda ghetto but works most of the time.");
            API.WriteLog("Battle Res: when toggled on, the CR will keep enough runic power for you to send a battle res at any time. Do not have it toggled on when you take a lot of damage! You need the runic power to heal yourself.");
            API.WriteLog("Range: when toggled on, the CR will use range spells (death caress and death coil). It's convenient most of the time, but can be very harmful in some situations. Disable when you want to do chirurgical pulls (torments for example). Doing damage to a mob will pull every other mob around. Could mess difficult pulls. You've been warned.");
            API.WriteLog("Potions: when toggled on, the CR will use health pots (hp < 30%) and healthstones (hp < 40%). Please set Healthstone Life Percent to 0 in the settings. The CR do not use offensive pots at all.");
            API.WriteLog("PVP: enable pvp spells.");
            CombatRoutine.isAutoBindReady = true;
            CombatRoutine.AddSpell(Marrowrend, 195182, "None");
            CombatRoutine.AddSpell(BloodBoil, 50842, "None");
            CombatRoutine.AddSpell(DeathStrike, 49998, "None");
            CombatRoutine.AddSpell(HeartStrike, 206930, "None");
            CombatRoutine.AddSpell(AntiMagicShell, 48707, "None");
            CombatRoutine.AddSpell(VampiricBlood, 55233, "None");
            CombatRoutine.AddSpell(IceboundFortitude, 48792, "None");
            CombatRoutine.AddSpell(DancingRuneWeapon, 49028, "None");
            CombatRoutine.AddSpell(DeathAndDecay, 43265, "None");
            CombatRoutine.AddSpell(MindFreeze, 47528, "None");
            CombatRoutine.AddSpell(RuneTap, 194679, "None");
            CombatRoutine.AddSpell(RaiseDead, 46585, "None");
            CombatRoutine.AddSpell(AntiMagicZone, 51052, "None");
            CombatRoutine.AddSpell(Tombstone, 219809, "None");
            CombatRoutine.AddSpell(BloodTap, 221699, "None");
            CombatRoutine.AddSpell(Bonestorm, 194844, "None");
            CombatRoutine.AddSpell(Lichborne, 49039, "None");
            CombatRoutine.AddSpell(Asphyxiate, 221562, "None");
            CombatRoutine.AddSpell(AbominationLimb, 315443, "None");
            CombatRoutine.AddSpell(SwarmingMist, 311648, "None");
            CombatRoutine.AddSpell(Fleshcraft, 324631, "None");
            CombatRoutine.AddSpell(ChainsOfIce, 45524, "None");
            CombatRoutine.AddSpell(DeathChain, 203173, "None");
            CombatRoutine.AddSpell(DeathsCaress, 195292, "None");
            CombatRoutine.AddSpell(DeathCoil, 47541, "None");
            CombatRoutine.AddSpell(Strangulate, 47476, "None");
            CombatRoutine.AddSpell(DarkSimulacrum, 77606, "None");
            CombatRoutine.AddSpell(MurderousIntent, 207018, "None");
            CombatRoutine.AddSpell(Blooddrinker, 206931, "None");
            CombatRoutine.AddSpell(WillOfTheForsaken, 7744, "None");
            CombatRoutine.AddSpell(ShackleTheUnworthy, 312202, "None");
            CombatRoutine.AddSpell(BloodFury, 20572, "None");
            CombatRoutine.AddSpell(SacrificialPact, 327574, "None");
            CombatRoutine.AddSpell(MarkOfBlood, 206940, "None");
            CombatRoutine.AddSpell(DeathPact, 48743, "None");
            //CombatRoutine.AddSpell(WarStomp, 20549, "None");
            CombatRoutine.AddSpell(AncestralCall, 274738, "None");
            CombatRoutine.AddSpell(QuakingPalm, 107079, "None");
            //CombatRoutine.AddSpell(WillToSurvive, 59752, "None");
            CombatRoutine.AddBuff(BoneShield);
            CombatRoutine.AddBuff(CrimsonScourge);
            CombatRoutine.AddBuff(WraithWalk);
            CombatRoutine.AddBuff(Ossuary);
            CombatRoutine.AddBuff(DancingRuneWeapon);
            CombatRoutine.AddBuff(Haemostasis);
            CombatRoutine.AddBuff(BloodShield);
            CombatRoutine.AddBuff(AntiMagicShell);
            CombatRoutine.AddBuff(RuneTap);
            CombatRoutine.AddBuff(VampiricBlood);
            CombatRoutine.AddBuff(AntiMagicZone);
            CombatRoutine.AddBuff(IceboundFortitude);
            CombatRoutine.AddBuff(BloodBarrier);
            CombatRoutine.AddBuff(Fleshcraft);
            CombatRoutine.AddBuff(Lichborne);
            CombatRoutine.AddBuff(ForgeborneReveries);
            CombatRoutine.AddBuff(BansheeShroud);
            CombatRoutine.AddBuff("Loyal Beast");
            CombatRoutine.AddBuff("Undying Rage");
            CombatRoutine.AddBuff("Rage");
            CombatRoutine.AddBuff("Diviner's Probe");
            CombatRoutine.AddBuff("Ranger's Heartseeker");
            CombatRoutine.AddDebuff(BloodPlague);
            CombatRoutine.AddDebuff(MarkOfBlood);
            CombatRoutine.AddDebuff(SoulExhaustion);
            CombatRoutine.AddDebuff("Jagged Swipe");
            CombatRoutine.AddDebuff("Stony Veins");
            CombatRoutine.AddDebuff("Rake");
            CombatRoutine.AddDebuff("Gushing Wound");
            CombatRoutine.AddDebuff("Buzz-Saw");
            CombatRoutine.AddDebuff("Beak Slice");
            CombatRoutine.AddDebuff("Arcane Lightning");
            CombatRoutine.AddDebuff("Decaying Blight");
            CombatRoutine.AddDebuff("Change of Heart");
            CombatRoutine.AddDebuff("Blackened Armor");
            CombatRoutine.AddDebuff("Spreading Misery");
            CombatRoutine.AddDebuff("Immediate Extermination");
            CombatRoutine.AddDebuff("Fragments of Destiny");
            CombatRoutine.AddDebuff(ShroudOfPurgatory);
            CombatRoutine.AddDebuff(Mindgames);
            CombatRoutine.AddDebuff(DarkSimulacrum);
            CombatRoutine.AddDebuff(FocusedAssault);
            CombatRoutine.AddDebuff(ChainsOfIce);
            CombatRoutine.AddDebuff(CruciformAxe);
            CombatRoutine.AddDebuff(ReverberatingHammer);
            CombatRoutine.AddDebuff(DualbladeScythe);
            CombatRoutine.AddItem(SpiritualHealingPotion, 171267);
            CombatRoutine.AddConduit(HardenedBones);
            CombatRoutine.AddMacro(Trinket1, "None");
            CombatRoutine.AddMacro(Trinket2, "None");
            CombatRoutine.AddMacro(MindFreeze + " MO", "None");
            CombatRoutine.AddMacro(Strangulate + " MO", "None");
            CombatRoutine.AddMacro(Asphyxiate + " MO", "None");
            CombatRoutine.AddToggle(Interrupt);
            CombatRoutine.AddToggle(BattleRes);
            CombatRoutine.AddToggle(Range);
            CombatRoutine.AddToggle(Potions);
            CombatRoutine.AddToggle(PVP);
            CombatRoutine.AddProp(Trinket1, "Use " + "Trinket 1", TrinketSettings, "Use " + "Trinket 1" + " with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp(Trinket2, "Use " + "Trinket 2", TrinketSettings, "Use " + "Trinket 2" + " with Cooldowns", "Trinkets", 0);
            CombatRoutine.AddProp(DeathAndDecayUsage, DeathAndDecayUsage, DeathAndDecayUsageSettings, "Set to manual if you absolutely need DnD to kite. You will have to cast it yourself", "Rotation", 0);
            CombatRoutine.AddProp(InterruptPriority, InterruptPriority, InterruptPrioritySettings, "High: auto-interrupt will cancel your casts and channelings if it has to", Interrupt, 0);
            CombatRoutine.AddProp(RemoveFear, RemoveFear, RemoveFearSettings, "Automatically remove fear effects", "AutoRemoval", 0);
            CombatRoutine.AddProp(RemoveStun, RemoveStun, RemoveStunSettings, "Automatically remove stun effects", "AutoRemoval", 0);
            CombatRoutine.AddProp(AMSPriority, AMSPriority, AMSPrioritySettings, "AMS priority regarding heavy casts and low HP. Heavy casts are not sorted by physical/magical types yet, so AMS might be wasted by the rotation there. " +
                "Priority regarding low HP: high = HP <= 50, low = HP <= 20. AMS will still be casted for AMS debuff and AMS cast list, even if you set Disabled here.", "Rotation", 0);
            CombatRoutine.AddProp(MovingFleshcraft, MovingFleshcraft, MovingFleshcraftSettings, "Can you channel Fleshcraft while moving?", "Rotation", 0);
            CombatRoutine.AddProp(MitigationManagement, MitigationManagement, MitigationManagementSettings, "Heavy will cast more CDs at once. Use on insanely huge damage fights, Small is better is all other scenarios", "Rotation", 0);
        }

        public override void Pulse()
        {
            
        }

        public override void CombatPulse()
        {
            bool HeavyMitigation            = API.PlayerHasBuff(VampiricBlood) || API.PlayerHasBuff(IceboundFortitude) || API.PlayerHasBuff(BloodBarrier) || API.PlayerHasBuff(Fleshcraft);
            bool SmallMitigation            = API.PlayerHasBuff(VampiricBlood) || API.PlayerHasBuff(IceboundFortitude) || API.PlayerHasBuff(BloodBarrier) || API.PlayerHasBuff(Fleshcraft) || 
                                              API.PlayerHasBuff(RuneTap) || API.PlayerHasBuff(DancingRuneWeapon) || (API.PlayerIsConduitSelected(HardenedBones) && API.PlayerHasBuff(Lichborne));
            bool HasMitigation              = (HeavyMitigation && MitigationManagementSetting == "Heavy") || (SmallMitigation && MitigationManagementSetting == "Small");
            int TargetRange                 = API.TargetRange;
            int MouseoverRange              = API.MouseoverRange;
            int PlayerCurrentRunes          = API.PlayerCurrentRunes;
            int TargetUnitInRangeCount      = API.TargetUnitInRangeCount;
            int PlayerHealthPercent         = API.PlayerHealthPercent;
            int PlayerRunicPower            = API.PlayerRunicPower;
            int CurrentCastSpellID          = API.TargetCurrentCastSpellID;
            string PlayerRaceName           = API.PlayerRaceName;
            bool StopCDs                    = API.PlayerHasBuff(ForgeborneReveries) || (API.PlayerHasDebuff(SoulExhaustion, false) && PlayerHealthPercent < 1) || API.PlayerHasDebuff(ShroudOfPurgatory);
            int DeathStrikeCost             = API.PlayerHasBuff(Ossuary) ? 40 : 45;
            int TargetGUIDNPCID             = API.TargetGUIDNPCID;
            string LastSpellCastInGame      = API.LastSpellCastInGame;
            bool MouseoverCanInterrupted    = API.MouseoverCanInterrupted;
            bool PlayerIsTargetTarget       = API.PlayerIsUnitTarget("target");

            /* React to Purgatory
             * Logic: Vampiric Blood to buff healing received
             * Use all the healing we got to survive the lethal damage */
            if (API.PlayerHasDebuff(ShroudOfPurgatory))
            {
                if (API.CanCast(VampiricBlood))
                {
                    API.CastSpell(VampiricBlood);
                    return;
                }

                if (API.PlayerItemCanUse("Healthstone") && API.PlayerItemRemainingCD("Healthstone") == 0)
                {
                    API.CastSpell("Healthstone");
                }

                if (API.PlayerItemCanUse(SpiritualHealingPotion) && API.PlayerItemRemainingCD(SpiritualHealingPotion) == 0)
                {
                    API.CastSpell(SpiritualHealingPotion);
                }

                if (API.CanCast(DeathStrike) && TargetRange < 7 && PlayerRunicPower >= DeathStrikeCost)
                {
                    API.CastSpell(DeathStrike);
                    return;
                }

                if (PlayerRunicPower >= 20)
                {
                    if (API.CanCast(SacrificialPact) && API.PlayerTotemPetDuration > 0)
                    {
                        API.CastSpell(SacrificialPact);
                        return;
                    }

                    if (API.CanCast(RaiseDead) && API.SpellISOnCooldown(SacrificialPact) == false)
                    {
                        API.CastSpell(RaiseDead);
                        return;
                    }
                }

                if (API.PlayerIsTalentSelected(6, 2) && API.CanCast(DeathPact))
                {
                    API.CastSpell(DeathPact);
                    return;
                }
            }

            /* Heartseeker mitigation 
             * RT + TRINKET / RT + AMS / RT + VB + IF / RT + LB + FC
             * heartseeker 1: 	You     => 3 stacks (RT + TRINKET)
             * black arrow:     Offtank
             * heartseeker 2: 	You     => 0 stack (RT + AMS)
             * heartseeker 3: 	Offtank => 3 stacks
             * heartseeker 4: 	You     => 3 stacks (RT + IF + VB)
             * heartseeker 5: 	Offtank => 3 stacks
             * heartseeker 6: 	You     => 0 stack (RT + AMS)
             * heartseeker 7: 	Offtank => 3 stacks
             * heartseeker 8: 	Offtank => 3 stacks
             * heartseeker 9: 	You     => 0 stack (RT + AMS)
             * heartseeker 10:  You     => 1-2 stacks (what's left)
             * heartseeker 11:  Offtank => 3 stacks
             */
            bool HeartseekerMitigation = API.PlayerHasBuff(BloodBarrier) || API.PlayerHasBuff(AntiMagicShell) || API.PlayerHasBuff(IceboundFortitude) || (API.PlayerHasBuff(Fleshcraft) && PlayerCovenantSettings == "Necrolord");
            if (PlayerIsTargetTarget && (CurrentCastSpellID == 353969 || CurrentCastSpellID == 352663 || CurrentCastSpellID == 358181 || CurrentCastSpellID == 358182 || CurrentCastSpellID == 358183 || CurrentCastSpellID == 353965) && HeartseekerMitigation == false && !StopCDs)
            {
                if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0 && !API.PlayerHasBuff(RuneTap))
                {
                    API.CastSpell(RuneTap);
                    return;
                }

                if (API.CanCast(AntiMagicShell) && API.TargetHealthPercent < 96) //don't ams the first so you can reset stacks
                {
                    API.CastSpell(AntiMagicShell);
                    return;
                }

                if (!API.MacroIsIgnored(Trinket1) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && (TrinketSetting1 == "Defensive Cooldowns" || TrinketSetting1 == "Offensive and Defensive Cooldowns"))
                {
                    API.CastSpell(Trinket1);
                    return;
                }

                if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && (TrinketSetting2 == "Defensive Cooldowns" || TrinketSetting2 == "Offensive and Defensive Cooldowns"))
                {
                    API.CastSpell(Trinket2);
                    return;
                }

                if (API.CanCast(VampiricBlood))
                {
                    API.CastSpell(VampiricBlood);
                    return;
                }

                if (API.CanCast(IceboundFortitude))
                {
                    API.CastSpell(IceboundFortitude);
                    return;
                }

                if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne))
                {
                    API.CastSpell(Lichborne);
                    return;
                } 

                if (PlayerCovenantSettings == "Necrolord" && API.CanCast(Fleshcraft) && (API.PlayerIsMoving == false || MovingFleshcraftSetting == "Yes"))
                {
                    API.CastSpell(Fleshcraft);
                    return;
                }
            }

            /* Deathlink requires dmg % mitigation, so we change defensive cd priority for this cast 
             * Ideally, we want Rune Tape + another cd, so we mitigate a maximum of damage to prevent damage on the raid
             */
            bool DeathlinkMitigation = API.PlayerHasBuff(IceboundFortitude) || (API.PlayerIsConduitSelected(HardenedBones) && API.PlayerHasBuff(Lichborne));
            if (PlayerIsTargetTarget && CurrentCastSpellID == 350828 && DeathlinkMitigation == false && !StopCDs)
            {
                if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0 && !API.PlayerHasBuff(RuneTap))
                {
                    API.CastSpell(RuneTap);
                    return;
                }

                if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne))
                {
                    API.CastSpell(Lichborne);
                    return;
                }

                if (API.CanCast(IceboundFortitude))
                {
                    API.CastSpell(IceboundFortitude);
                    return;
                }
            }

            /* Obliterate mitigation
             * Heavy mitigation as there are only 4 people soaking in mythic mode
             * 1st obliterate: RuneTap + IceboundFortitude + Trinket
             * 2nd obliterate: RuneTap + Lichborne + VampiricBlood + Fleshcraft
             */
            bool ObliterateMitigation = API.PlayerHasBuff(IceboundFortitude) || (API.PlayerHasBuff(Fleshcraft) && PlayerCovenantSettings == "Necrolord") || (API.PlayerHasBuff(VampiricBlood) && PlayerCovenantSettings != "Necrolord");
            if (PlayerIsTargetTarget && CurrentCastSpellID == 355352 && ObliterateMitigation == false && !StopCDs)
            {
                if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0 && !API.PlayerHasBuff(RuneTap))
                {
                    API.CastSpell(RuneTap);
                    return;
                }

                if (!API.MacroIsIgnored(Trinket1) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && (TrinketSetting1 == "Defensive Cooldowns" || TrinketSetting1 == "Offensive and Defensive Cooldowns"))
                {
                    API.CastSpell(Trinket1);
                    return;
                }

                if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && (TrinketSetting2 == "Defensive Cooldowns" || TrinketSetting2 == "Offensive and Defensive Cooldowns"))
                {
                    API.CastSpell(Trinket2);
                    return;
                }

                if (API.CanCast(IceboundFortitude))
                {
                    API.CastSpell(IceboundFortitude);
                    return;
                }

                if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne))
                {
                    API.CastSpell(Lichborne);
                    return;
                }

                if (API.CanCast(VampiricBlood))
                {
                    API.CastSpell(VampiricBlood);
                    return;
                }

                if (PlayerCovenantSettings == "Necrolord" && API.CanCast(Fleshcraft) && API.PlayerIsMoving == false)
                {
                    API.CastSpell(Fleshcraft);
                    return;
                }
            }

            /* High Priority Interrupt */
            if (isInterrupt && API.ToggleIsEnabled(Interrupt) && InterruptPrioritySetting == "High")
            {
                if (API.CanCast(MindFreeze))
                {
                    if (MouseoverCanInterrupted && !API.MacroIsIgnored(MindFreeze + " MO") && MouseoverRange <= 15)
                    {
                        API.CastSpell(MindFreeze + " MO");
                        return;
                    }

                    if (TargetRange <= 15)
                    {
                        API.CastSpell(MindFreeze);
                        return;
                    }
                }

                if (API.ToggleIsEnabled(PVP) && API.CanCast(Strangulate))
                {
                    if (MouseoverCanInterrupted && !API.MacroIsIgnored(Strangulate + " MO") && MouseoverRange <= 30)
                    {
                        API.CastSpell(Strangulate + " MO");
                        return;
                    }

                    if (TargetRange <= 30)
                    {
                        API.CastSpell(Strangulate);
                        return;
                    }
                }
            }

            if (API.PlayerIsCasting() == false && API.PlayerSpellonCursor == false && API.PlayerHasBuff(WraithWalk) == false && API.PlayerIsChanneling == false)
            {
                /* Raznal Axe, Hammer and Scythe require physical mitigation at the end of its long cast
                 * Icebound Fortitude and Vampiric Blood have to be kept for Spikes in interphase (patched/nerfed)
                 * We can only use these on the last phase, where the boss throws the Scythe
                 */
                if (API.PlayerHasDebuff(CruciformAxe, false) && API.PlayerDebuffRemainingTime(CruciformAxe, false) <= 300 && !StopCDs)
                {
                    API.WriteLog("Casting mitigation for Axe");
                    if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0 && !API.PlayerHasBuff(RuneTap))
                    {
                        API.CastSpell(RuneTap);
                        return;
                    }

                    if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne))
                    {
                        API.CastSpell(Lichborne);
                        return;
                    }
                }

                bool ScytheMitigation = API.PlayerHasBuff(IceboundFortitude) || API.PlayerHasBuff(VampiricBlood) || API.PlayerHasBuff(BloodBarrier);
                if (((API.PlayerHasDebuff(DualbladeScythe, false) && API.PlayerDebuffRemainingTime(DualbladeScythe, false) <= 300) || (API.PlayerHasDebuff(ReverberatingHammer, false) && API.PlayerDebuffRemainingTime(ReverberatingHammer, false) <= 300)) && ScytheMitigation == false && !StopCDs)
                {
                    API.WriteLog("Casting mitigation for Hammer or Scythe");
                    if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0 && !API.PlayerHasBuff(RuneTap))
                    {
                        API.CastSpell(RuneTap);
                        return;
                    }

                    if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne))
                    {
                        API.CastSpell(Lichborne);
                        return;
                    }

                    if (API.PlayerHasDebuff(DualbladeScythe, false) && API.CanCast(IceboundFortitude))
                    {
                        API.CastSpell(IceboundFortitude);
                        return;
                    }

                    if (API.CanCast(VampiricBlood))
                    {
                        API.CastSpell(VampiricBlood);
                        return;
                    }

                    if (!API.MacroIsIgnored(Trinket1) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && (TrinketSetting1 == "Defensive Cooldowns" || TrinketSetting1 == "Offensive and Defensive Cooldowns"))
                    {
                        API.CastSpell(Trinket1);
                        return;
                    }

                    if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && (TrinketSetting2 == "Defensive Cooldowns" || TrinketSetting2 == "Offensive and Defensive Cooldowns"))
                    {
                        API.CastSpell(Trinket2);
                        return;
                    }
                }

                /* PVP Spells */
                if (API.ToggleIsEnabled(PVP))
                {
                    if (API.CanCast(MurderousIntent) && API.TargetHasDebuff(FocusedAssault) == false && TargetRange <= 10)
                    {
                        API.CastSpell(MurderousIntent);
                        return;
                    }

                    if (API.CanCast(DarkSimulacrum) && (API.TargetIsCasting(false) || API.TargetIsChanneling) && TargetRange <= 40)
                    {
                        for (int i = 0; i < DarkSimulacrumList.Length; i++)
                        {
                            if (CurrentCastSpellID == DarkSimulacrumList[i])
                            {
                                API.CastSpell(DarkSimulacrum);
                                return;
                            }
                        }
                    }

                    if (API.CanCast(ChainsOfIce) && API.TargetHasDebuff(ChainsOfIce) == false && PlayerCurrentRunes >= 3 && TargetRange <= 30 && TargetRange >= 8)
                    {
                        API.CastSpell(ChainsOfIce);
                        return;
                    }

                    if (API.CanCast(DeathChain) && TargetUnitInRangeCount >= 3 && TargetRange <= 10)
                    {
                        API.CastSpell(DeathChain);
                        return;
                    }
                }

                /* Remove Fear Effects */
                if ((API.PlayerIsCC(CCList.FEAR_MECHANIC) || API.PlayerIsCC(CCList.CHARM_MECHANIC)) && TargetRange <= 40 && !StopCDs)
                {
                    if (PlayerRaceName == "Undead" && API.CanCast(WillOfTheForsaken) && (RemoveFearSetting == "With Racial" || RemoveFearSetting == "With Lichborne and Racial"))
                    {
                        API.CastSpell(WillOfTheForsaken);
                        return;
                    }

                    if (API.CanCast(Lichborne) && (RemoveFearSetting == "With Lichborne" || RemoveFearSetting == "With Lichborne and Racial"))
                    {
                        API.CastSpell(Lichborne);
                        return;
                    }
                }

                /* Remove Stun Effects */
                if (API.PlayerIsCC(CCList.STUN) && TargetRange <= 40 && !StopCDs)
                {
                    if (PlayerRaceName == "Human" && API.CanCast(WillToSurvive) && (RemoveStunSetting == "With Racial" || RemoveStunSetting == "With Icebound Fortitude and Racial"))
                    {
                        API.CastSpell(WillToSurvive);
                        return;
                    }

                    if (API.CanCast(IceboundFortitude) && (RemoveStunSetting == "With Icebound Fortitude" || RemoveStunSetting == "With Icebound Fortitude and Racial"))
                    {
                        API.CastSpell(IceboundFortitude);
                        return;
                    }
                }

                /* Interrupt */
                if (isInterrupt && API.ToggleIsEnabled(Interrupt))
                {
                    if (MouseoverCanInterrupted && !API.MacroIsIgnored(MindFreeze + " MO") && API.CanCast(MindFreeze) && MouseoverRange <= 15)
                    {
                        API.CastSpell(MindFreeze + " MO");
                        return;
                    }

                    if (API.CanCast(MindFreeze) && TargetRange <= 15)
                    {
                        API.CastSpell(MindFreeze);
                        return;
                    }

                    if (API.ToggleIsEnabled(PVP))
                    {
                        if (MouseoverCanInterrupted && !API.MacroIsIgnored(Strangulate + " MO") && API.CanCast(Strangulate) && MouseoverRange <= 30)
                        {
                            API.CastSpell(Strangulate + " MO");
                            return;
                        }

                        if (API.CanCast(Strangulate) && TargetRange <= 30)
                        {
                            API.CastSpell(Strangulate);
                            return;
                        }
                    }
                }

                /* React to debuffs and magic casts */
                if (API.CanCast(AntiMagicShell) && !StopCDs)
                {
                    /* AMS for Immediate Extermination when > 3 stacks and time remaining <= 54 (ticks at 52) */
                    if (API.PlayerHasDebuff("Immediate Extermination", false))
                    {
                        if (API.PlayerDebuffStacks("Immediate Extermination") > 3 && API.PlayerDebuffRemainingTime("Immediate Extermination", false) <= 54)
                        {
                            API.WriteLog("Mitigating Immediate Extermination");
                            API.CastSpell(AntiMagicShell);
                            return;
                        }
                    }

                    for (int i = 0; i < ShellDebuffList.Length; i++)
                    {
                        if (API.PlayerHasDebuff(ShellDebuffList[i], false))
                        {
                            API.CastSpell(AntiMagicShell);
                            return;
                        }
                    }

                    if (PlayerIsTargetTarget && (API.TargetIsCasting(false) || API.TargetIsChanneling))
                    {
                        for (int i = 0; i < ShellSpellList.Length; i++)
                        {
                            if (CurrentCastSpellID == ShellSpellList[i])
                            {
                                API.CastSpell(AntiMagicShell);
                                return;
                            }
                        }
                    }
                }

                /* React to Enrage */
                if (PlayerIsTargetTarget && API.CanCast(IceboundFortitude) && !StopCDs && TargetRange <= 15)
                {
                    for (int i = 0; i < IceboundFortitudeBuffList.Length; i++)
                    {
                        if (API.TargetHasBuff(IceboundFortitudeBuffList[i]))
                        {
                            API.CastSpell(IceboundFortitude);
                            return;
                        }
                    }
                }

                if (API.TargetIsCasting(false) || API.TargetIsChanneling)
                {
                    /* Stun */
                    if (API.CanCast(Asphyxiate) || (PlayerRaceName == "Tauren" && API.CanCast(WarStomp)) || (PlayerRaceName == "Pandaren" && API.CanCast(QuakingPalm)))
                    {
                        /* Don't loop API calls */
                        bool MouseoverStun = !API.MacroIsIgnored(Asphyxiate + " MO");
                        int MouseoverCurrentCastSpellID = API.MouseoverCurrentCastSpellID;

                        for (int i = 0; i < CastsToStunList.Length; i++)
                        {
                            if (CurrentCastSpellID == CastsToStunList[i])
                            {
                                if (API.CanCast(Asphyxiate) && TargetRange <= 20)
                                {
                                    API.CastSpell(Asphyxiate);
                                    return;
                                }

                                if (PlayerRaceName == "Tauren" && API.CanCast(WarStomp) && API.PlayerIsMoving == false && TargetRange <= 8)
                                {
                                    API.CastSpell(WarStomp);
                                    return;
                                }
                                
                                if (PlayerRaceName == "Pandaren" && API.CanCast(QuakingPalm) && TargetRange <= 8)
                                {
                                    API.CastSpell(QuakingPalm);
                                    return;
                                }
                            }

                            if (MouseoverStun && MouseoverCurrentCastSpellID == CastsToStunList[i] && API.CanCast(Asphyxiate + " MO") && MouseoverRange <= 20)
                            {
                                API.CastSpell(Asphyxiate + " MO");
                                return;
                            }
                        }
                    }

                    /* Heavy Damage Spells */
                    if (PlayerIsTargetTarget && HasMitigation == false && !StopCDs && API.TargetHealthPercent > 3)
                    {
                        for (int i = 0; i < HeavyDamageCasts.Length; i++)
                        {
                            if (CurrentCastSpellID == HeavyDamageCasts[i])
                            {
                                if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0)
                                {
                                    API.CastSpell(RuneTap);
                                    return;
                                }

                                if (API.CanCast(AntiMagicShell) && AMSPrioritySetting == "High")
                                {
                                    API.CastSpell(AntiMagicShell);
                                    return;
                                }

                                if (API.CanCast(VampiricBlood))
                                {
                                    API.CastSpell(VampiricBlood);
                                    return;
                                }

                                if (API.CanCast(IceboundFortitude))
                                {
                                    API.CastSpell(IceboundFortitude);
                                    return;
                                }

                                if (!API.MacroIsIgnored(Trinket1) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && (TrinketSetting1 == "Defensive Cooldowns" || TrinketSetting1 == "Offensive and Defensive Cooldowns"))
                                {
                                    API.CastSpell(Trinket1);
                                    return;
                                }

                                if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && (TrinketSetting2 == "Defensive Cooldowns" || TrinketSetting2 == "Offensive and Defensive Cooldowns"))
                                {
                                    API.CastSpell(Trinket2);
                                    return;
                                }

                                if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne))
                                {
                                    API.CastSpell(Lichborne);
                                    return;
                                }

                                if (PlayerCovenantSettings == "Necrolord" && API.CanCast(Fleshcraft) && (API.PlayerIsMoving == false || MovingFleshcraftSetting == "Yes"))
                                {
                                    API.CastSpell(Fleshcraft);
                                    return;
                                }

                                if (API.CanCast(AntiMagicShell) && AMSPrioritySetting == "Low")
                                {
                                    API.CastSpell(AntiMagicShell);
                                    return;
                                }

                                if (API.CanCast(DancingRuneWeapon))
                                {
                                    API.CastSpell(DancingRuneWeapon);
                                    return;
                                }
                            }
                        }
                    }
                }

                /* Sacrificial Pact */
                if (API.CanCast(SacrificialPact) && API.PlayerTotemPetDuration < 240 && API.PlayerTotemPetDuration > 0 && PlayerRunicPower >= 20 && !StopCDs)
                {
                    API.CastSpell(SacrificialPact);
                    return;
                }

                /* Raise Dead & Sacrificial Pact */
                if (PlayerHealthPercent <= 35 && PlayerRunicPower >= 20)
                {
                    if (API.CanCast(SacrificialPact) && API.PlayerTotemPetDuration > 0)
                    {
                        API.CastSpell(SacrificialPact);
                        return;
                    }

                    if (API.CanCast(RaiseDead) && API.SpellISOnCooldown(SacrificialPact) == false && API.PlayerHasPet == false)
                    {
                        API.CastSpell(RaiseDead);
                        return;
                    }
                }

                if (PlayerHealthPercent <= 85 && API.CanCast(DeathStrike) && TargetRange < 7 && ((PlayerRunicPower >= DeathStrikeCost && API.ToggleIsEnabled(BattleRes) == false) || PlayerRunicPower >= DeathStrikeCost + 30))
                {
                    API.CastSpell(DeathStrike);
                    return;
                }

                if (API.ToggleIsEnabled(Potions) && !StopCDs)
                {
                    bool LastSpellCastIsHeal = LastSpellCastInGame == DeathStrike || LastSpellCastInGame == "Healthstone" || LastSpellCastInGame == SpiritualHealingPotion;
                    bool ShouldCastHeal = (!LastSpellCastIsHeal && PlayerHealthPercent <= 30) || (LastSpellCastIsHeal && PlayerHealthPercent <= 20);

                    /* Healthstone */
                    if (API.PlayerItemCanUse("Healthstone") && API.PlayerItemRemainingCD("Healthstone") == 0 && ShouldCastHeal)
                    {
                        API.CastSpell("Healthstone");
                    }

                    /* Health Potion */
                    if (API.PlayerItemCanUse(SpiritualHealingPotion) && API.PlayerItemRemainingCD(SpiritualHealingPotion) == 0 && ShouldCastHeal)
                    {
                        API.CastSpell(SpiritualHealingPotion);
                    }
                }

                /* React to Low HP */
                bool TargetIsBoss = API.TargetIsBoss;
                if (((API.TargetHealthPercent >= 5 && TargetIsBoss == false) || TargetIsBoss || PlayerHealthPercent <= 40) && !StopCDs && TargetRange < 30)
                {
                    /* Rune Tap (20% DR, 2 charges, 25s cd)
                    * Logic: constantly use to improve damage/healed received ratio
                    */
                    if (API.CanCast(RuneTap) && API.SpellCharges(RuneTap) > 0 && HasMitigation == false)
                    {
                        if ((API.SpellCharges(RuneTap) == 2 && PlayerHealthPercent <= 80) || (API.SpellCharges(RuneTap) == 1 && PlayerHealthPercent <= 60))
                        {
                            API.CastSpell(RuneTap);
                            return;
                        }
                    }

                    /* Anti Magic Shell (30% of max HP shield & prevent debuff from being applied, 1m cd) 
                    * Logic: if we have 1 stack of stackable debuff and mob has enough HP
                    * cast AMS to prevent application of the next stack
                    * or if target is casting a spell that will give us a debuff to avoid
                    * or cast if we are below 60% to temporize healing
                    */
                    bool AllowedToUseAMS = TargetGUIDNPCID != 176523 && TargetGUIDNPCID != 176581 && TargetGUIDNPCID != 179847;
                    bool AMSHealthPercent = (AMSPrioritySetting == "High" && PlayerHealthPercent <= 50) || (AMSPrioritySetting == "Low" && PlayerHealthPercent <= 20);
                    if (API.CanCast(AntiMagicShell) && HasMitigation == false && AMSHealthPercent && AllowedToUseAMS)
                    {
                        API.CastSpell(AntiMagicShell);
                        return;
                    }

                    /* Vampiric Blood (30% extra health & healing received for 10s, 1.5m cd)
                    * Logic: Use this to get back to full HP quickly 
                    * only if we don't have AMS to prevent spell waste 
                    * or if HP is critical, to help get back to full
                    * could be nice to use proactively, eventually
                    */
                    if (API.CanCast(VampiricBlood) && (PlayerHealthPercent <= 60 || (API.PlayerIsTalentSelected(7, 2) && PlayerHealthPercent <= 70)) && TargetGUIDNPCID != 175731 && HasMitigation == false)
                    {
                        API.CastSpell(VampiricBlood);
                        return;
                    }

                    /* Icebound Fortitude (30% DR & stun immunity for 8s, 3m cd)
                    * Logic: Use this when mobs are heavily buffed (enraged usually)
                    * or when critically low HP to get a better damage/healed received ratio
                    * Do not use when we are fighting Raznal, has we need IF for important stuff in that fight
                    */
                    if (API.CanCast(IceboundFortitude) && ((PlayerHealthPercent <= 60 && HasMitigation == false) || PlayerHealthPercent <= 20) && (TargetGUIDNPCID != 176523 || (TargetGUIDNPCID == 176523 && API.TargetHealthPercent <= 33)))
                    {
                        API.CastSpell(IceboundFortitude);
                        return;
                    }

                    /* Lichborne (+10% leech & charm, fear, sleep immunity for 10s, 2m cd) 
                     * with conduit becomes a very decent def cd */
                    if (API.PlayerIsConduitSelected(HardenedBones) && API.CanCast(Lichborne) && PlayerHealthPercent <= 70 && HasMitigation == false)
                    {
                        API.CastSpell(Lichborne);
                        return;
                    }

                    if (PlayerCovenantSettings == "Necrolord" && API.CanCast(Fleshcraft) && PlayerHealthPercent <= 50 && (API.PlayerIsMoving == false || MovingFleshcraftSetting == "Yes") && AllowedToUseAMS)
                    {
                        API.CastSpell(Fleshcraft);
                        return;
                    }

                    /* Dancing Rune Weapon
                    * Logic: Parry?
                    */
                    if (API.CanCast(DancingRuneWeapon) && PlayerHealthPercent <= 40)
                    {
                        API.CastSpell(DancingRuneWeapon);
                        return;
                    }

                    /* Lichborne (+10% leech & charm, fear, sleep immunity for 10s, 2m cd) 
                    * Logic: hard to cast this spell proactively 
                    * only use if big leech spells are available (death strike, blood boil)
                    * unless we have the conduit Hardened Bones (Lichborne becomes a defensive cooldown)
                    */
                    if (API.PlayerIsConduitSelected(HardenedBones) == false && API.CanCast(Lichborne) && PlayerHealthPercent <= 60 && HasMitigation == false)
                    {
                        if ((API.SpellCharges(BloodBoil) > 0 && API.CanCast(BloodBoil) && TargetUnitInRangeCount > 1) || (PlayerRunicPower >= 45 && API.CanCast(DeathStrike)))
                        {
                            API.CastSpell(Lichborne);
                            return;
                        }
                    }

                    /* Anti-Magic Zone (20% DR for anyone inside for 10s, 2min cd)
                    * Logic: low HP to get a better damage/healed received ratio and not moving
                    */
                    /*if (API.CanCast(AntiMagicZone) && API.PlayerIsMoving == false && ((PlayerHealthPercent <= 50 && HasMitigation == false) || PlayerHealthPercent <= 30))
                    {
                        API.CastSpell(AntiMagicZone);
                        return;
                    }*/

                    if (!API.MacroIsIgnored(Trinket1) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && (TrinketSetting1 == "Defensive Cooldowns" || TrinketSetting1 == "Offensive and Defensive Cooldowns") && PlayerHealthPercent <= 50 && HasMitigation == false)
                    {
                        API.CastSpell(Trinket1);
                        return;
                    }

                    if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && (TrinketSetting2 == "Defensive Cooldowns" || TrinketSetting2 == "Offensive and Defensive Cooldowns") && PlayerHealthPercent <= 50 && HasMitigation == false)
                    {
                        API.CastSpell(Trinket2);
                        return;
                    }
                }

                if (API.PlayerHasDebuff(Mindgames, false) || API.TargetHasBuff(BansheeShroud))
                    return;

                /* Tombstone
                * Logic: Consume bone charges to reduce Dancing Rune Weapon CD whenever possible
                */
                if (API.PlayerIsTalentSelected(1, 3) && API.CanCast(Tombstone) && API.PlayerBuffStacks(BoneShield) >= 5 && API.SpellISOnCooldown(DancingRuneWeapon) && !StopCDs)
                {
                    API.CastSpell(Tombstone);
                    return;
                }

                /* Cooldowns
                * Logic: None
                */
                if (IsCooldowns && TargetRange < 10)
                {
                    if (PlayerRaceName == "Orc" && API.CanCast(BloodFury))
                    {
                        API.CastSpell(BloodFury);
                        return;
                    }

                    if (PlayerRaceName == "Mag'har Orc" && API.CanCast(AncestralCall))
                    {
                        API.CastSpell(AncestralCall);
                        return;
                    }

                    if (PlayerCovenantSettings == "Necrolord" && API.CanCast(AbominationLimb))
                    {
                        API.CastSpell(AbominationLimb);
                        return;
                    }

                    if (PlayerCovenantSettings == "Venthyr" && API.CanCast(SwarmingMist) && (PlayerRunicPower > 10 || PlayerCurrentRunes > 0))
                    {
                        API.CastSpell(SwarmingMist);
                        return;
                    }

                    if (PlayerCovenantSettings == "Kyrian" && API.CanCast(ShackleTheUnworthy))
                    {
                        API.CastSpell(ShackleTheUnworthy);
                        return;
                    }

                    if (API.CanCast(RaiseDead) && API.PlayerHasPet == false)
                    {
                        API.CastSpell(RaiseDead);
                        return;
                    }

                    if (API.CanCast(DancingRuneWeapon))
                    {
                        API.CastSpell(DancingRuneWeapon);
                        return;
                    }

                    if (!API.MacroIsIgnored(Trinket1) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && (TrinketSetting1 == "Offensive Cooldowns" || TrinketSetting1 == "Offensive and Defensive Cooldowns"))
                    {
                        API.CastSpell(Trinket1);
                        return;
                    }

                    if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && (TrinketSetting2 == "Offensive Cooldowns" || TrinketSetting2 == "Offensive and Defensive Cooldowns"))
                    {
                        API.CastSpell(Trinket2);
                        return;
                    }
                }

                /* Trinkets: on cooldown */
                if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(1) && API.PlayerTrinketRemainingCD(1) == 0 && TrinketSetting1 == "On Cooldowns" && TargetRange <= 15)
                {
                    API.CastSpell(Trinket1);
                    return;
                }

                if (!API.MacroIsIgnored(Trinket2) && API.PlayerTrinketIsUsable(2) && API.PlayerTrinketRemainingCD(2) == 0 && TrinketSetting2 == "On Cooldowns" && TargetRange <= 15)
                {
                    API.CastSpell(Trinket2);
                    return;
                }

                /* Blooddrinker: on cooldown */
                if (API.PlayerIsTalentSelected(1, 2) && API.PlayerHasBuff(DancingRuneWeapon) == false && API.CanCast(Blooddrinker) && TargetRange <= 30 && TargetUnitInRangeCount == 1)
                {
                    API.CastSpell(Blooddrinker);
                    return;
                }

                /* Blood Tap
                * Logic: Blood Tap will generally perform better than Relish in Blood when properly used. However, Relish in Blood will provide more benefits passively if Blood Tap isn't being used correctly.
                * In short, here's an example of how you can get good value out of Blood Tap:
                * [1] [Blood Tap] if (you have 2 charges or are less than 5 seconds away from 2 charges of [Blood Tap]) and you have less than 3 Runes.
                * [2] [Blood Tap] if you have 1 charge, less than 3 Runes, and have less than 63 RP.
                * The 63 RP is arbitrary, it can be any number, but you want it low enough that you're only using the Blood Tap when you need it
                * and high enough to have enough time to turn that Rune into a potential Death Strike before you die. You can float a charge of Blood Tap without losing any potential Rune regeneration,
                * so it's best to use this charge only when you are in danger
                */
                if (API.PlayerIsTalentSelected(3, 3) && PlayerCurrentRunes < 3 && API.CanCast(BloodTap) &&
                    (API.SpellCharges(BloodTap) == 2 || (API.SpellCharges(BloodTap) == 1 && API.SpellChargeCD(BloodTap) <= 500) || (API.SpellCharges(BloodTap) == 1 && PlayerRunicPower < 63)))
                {
                    API.CastSpell(BloodTap);
                    return;
                }

                /* Death and Decay
                * Logic: Slow enemies, deal damage and make Heart Strike AoE
                */
                float MeleeRatio = (float)API.PlayerUnitInMeleeRangeCount / (float)TargetUnitInRangeCount * 100;
                if (DeathAndDecayUsageSetting == "Automatic" && PlayerCurrentRunes >= 1 && TargetRange <= 12 && API.CanCast(DeathAndDecay) && API.PlayerIsMoving == false && TargetGUIDNPCID != 120651 && (MeleeRatio >= 50 || API.PlayerHasBuff(CrimsonScourge)))
                {
                    API.CastSpell(DeathAndDecay);
                    return;
                }

                /* Blood Boil
                * Logic: IsAOE == false is meant to be used when there are untankable mobs around
                * Melee range monster ratio wouldnt work, so need to disable AoE toggle to skip it
                */
                if (API.CanCast(BloodBoil) && TargetRange <= 8 && TargetGUIDNPCID != 120651 && ((MeleeRatio >= 50 && API.SpellCharges(BloodBoil) >= 2) || (MeleeRatio >= 100 && API.SpellCharges(BloodBoil) >= 1) || IsAOE == false)) //(API.TargetHasDebuff(BloodPlague) == false && API.SpellCharges(BloodBoil) >= 1) || API.SpellCharges(BloodBoil) >= 2
                {
                    API.CastSpell(BloodBoil);
                    return;
                }

                /* Mark of Blood */
                if (!API.TargetHasDebuff(MarkOfBlood, true) && API.CanCast(MarkOfBlood))
                {
                    API.CastSpell(MarkOfBlood);
                    return;
                }

                if (TargetRange > 0 && TargetRange <= 7)
                {
                    if (API.PlayerIsTalentSelected(7, 3) && API.TargetHealthPercent >= 10)
                    {
                        /* Bonestorm & Death Strike
                        * Logic: Use Death Strike only if Bonestorm isn't available
                        * in order to save runic power for Bonestorm when available
                        * also save runic power if BattleRes is toggled in order to
                        * be able to BR someone in my group when needed
                        */
                        if (API.CanCast(Bonestorm) && ((PlayerRunicPower >= 70 && API.ToggleIsEnabled(BattleRes) == false) || PlayerRunicPower >= 100) && API.PlayerHasBuff(VampiricBlood) == false)
                        {
                            API.CastSpell(Bonestorm);
                            return;
                        }

                        if (API.CanCast(DeathStrike) && ((PlayerRunicPower >= DeathStrikeCost && API.ToggleIsEnabled(BattleRes) == false) || PlayerRunicPower >= DeathStrikeCost + 30) && API.SpellISOnCooldown(Bonestorm))
                        {
                            API.CastSpell(DeathStrike);
                            return;
                        }
                    }
                    else
                    {
                        if (API.CanCast(DeathStrike) && PlayerRunicPower >= DeathStrikeCost * 2) // <- DPS Death Strike, keep enough Runic Power to cast a Death Strike if we need healing
                        {
                            API.CastSpell(DeathStrike);
                            return;
                        }
                    }

                    if (API.PlayerBuffTimeRemaining(BoneShield) < 300 || API.PlayerBuffStacks(BoneShield) <= (API.PlayerHasBuff(DancingRuneWeapon) ? 4 : 7))
                    {
                        if (PlayerCurrentRunes >= 2 && API.CanCast(Marrowrend))
                        {
                            API.CastSpell(Marrowrend);
                            return;
                        }
                    }
                    else
                    {
                        if (PlayerCurrentRunes >= 1 && API.CanCast(HeartStrike))
                        {
                            API.CastSpell(HeartStrike);
                            return;
                        }
                    }
                }

                if (API.ToggleIsEnabled(Range) && TargetRange > 7 && TargetRange <= 30)
                {
                    if (PlayerRunicPower >= 85 && API.CanCast(DeathCoil) && (API.ToggleIsEnabled(BattleRes) == false || (API.ToggleIsEnabled(BattleRes) && PlayerRunicPower >= 100)))
                    {
                        API.CastSpell(DeathCoil);
                        return;
                    }

                    if (PlayerCurrentRunes >= 3 && API.CanCast(DeathsCaress))
                    {
                        API.CastSpell(DeathsCaress);
                        return;
                    }
                }
            }
        }

        public override void OutOfCombatPulse()
        {

        }
    }
}