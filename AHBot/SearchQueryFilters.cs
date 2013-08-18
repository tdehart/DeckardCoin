using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHBot
{
    class SQF
    {
        public SQF() { }

        #region SearchQueryFilters variables
        public static String[] SearchOptions = {
        "Class",
        "SupType",
        "SubType",
        "Quality",
        "preferredStat0",
        "preferredStat1",
        "preferredStat2"};

        public static String[] SearchClasses = {
        "Barbarian",
        "DemonHunter",
        "Monk",
        "WitchDoctor",
        "Wizard"};

        public static String[] ItemSupTypes = {
        "1Hand",
        "2Hand",
        "OffHand",
        "Armor",
        "FollowerSpecial"};

        public static String[] ItemSubTypes = {
        "AllOneHandItemTypes", // 0
        "Axe", //1
        "CeremonialKnife", //2
        "HandCrossbow", //3
        "Dagger", //4
        "FistWeapon", //5
        "Mace", //6
        "MightyWeapon", //7
        "Spear", //8
        "Sword", //9
        "Wand", //10
        
        "AllTwoHandItemTypes", // 11
        "TwoHandedAxe", //12
        "Bow", //13
        "Daibo", //14
        "Crossbow", //15
        "TwoHandedMace", //16
        "TwoHandedMightyWeapon", //17
        "Polearm", //18
        "Staff", //19
        "TwoHandedSword", //20
        
        "AllOffHandItemTypes", // 21
        "Mojo", //22
        "Source", //23
        "Quiver", //24
        "Shield", //25
        
        "AllArmorItemTypes", // 26
        "Amulet", //27
        "Belt", //28
        "Boots", //29
        "Bracers", //30
        "ChestArmor", //31
        "Cloak", // 32
        "Gloves", //33
        "Helm", //34
        "Pants", //35
        "MightyBelt", // 36
        "Ring", //37
        "Shoulders", //38
        "SpiritStone", // 39
        "VoodooMask", // 40
        "WizardHat", // 41
        
        "AllFollowerSpecialItemTypes",
        "EnchantressFocus",
        "ScoundrelToken",
        "TemplarRelic", // 45
    };

        public static int[][] ClassRestrictions = {
        new int[] {2,3,5,  10,      14,15,16,      19,22,   23,24,32,   39,40,41}, // Barbarian
        new int[] {2,  5,7,10,12,   14,   16,17,18,19,20,22,23,      36,39,40,41}, // Demon Hunter
        new int[] {2,3,  7,10,12,13,   15,16,17,      20,22,23,24,32,36,   40,41}, // Monk
        new int[] {  3,5,7,10,      14,      17,            23,24,32,36,39,   41}, // Witch Doctor
        new int[] {2,3,5,7,         14,      17,18,      22,   24,32,36,39,40   }  // Wizard
    };

        public static String[] Qualities = {
        "All",
        "Inferior",
        "Normal",
        "Superior",
        "Magic",
        "Rare",
        "Legendary",
    };

        public static String[] Stats = {
        "None",                                         //0
        "Damage",                                       //1 Header
        "AllResistance",                                //2
        "ArcaneResistance",                             //3
        "Armor",                                        //4
        "AttackSpeed",                                  //5
        "BleedChance",                                  //6
        "Block",                                        //7
        "BonusMinimumArcaneWeaponDamage",               //8
        "BonusMinimumColdWeaponDamage",                 //9
        "BonusMinimumFireWeaponDamage",                 //10
        "BonusMinimumHolyWeaponDamage",                 //11
        "BonusMinimumLightningWeaponDamage",            //12
        "BonusMinimumPhysicalDamage",                   //13
        "BonusMinimumPoisonWeaponDamage",               //14
        "BonusMinimumWeaponDamage",                     //15
        "BonusVsElites",                                //16
        "ColdResistance",                               //17
        "CriticalHitChance",                            //18
        "CriticalHitDamage",                            //19
        "FireResistance",                               //20
        "LightningResistance",                          //21
        "MinBleedDamage",                               //22
        "PhysicalDamageToAttacker",                     //23
        "PhysicalResistance",                           //24
        "PoisonResistance",                             //25
        "ReducedDamageFromElites",                      //26
        "ReducedDamageFromMeleeAttacks",                //27
        "ReducedDamageFromRangedAttacks",               //28
        "WeaponDamage",                                 //29
        "CrowdControl",                                 //30 Header
        "ChanceToBlindOnHit",                           //31
        "ChanceToChillOnHit",                           //32
        "ChanceToFearOnHit",                            //33
        "ChanceToFreezeOnHit",                          //34
        "ChanceToImmobilizeOnHit",                      //35
        "ChanceToKnockbackOnHit",                       //36
        "ChanceToSlowOnHit",                            //37
        "ChanceToStunOnHit",                            //38
        "CrowdControlReduction",                        //39
        "Life",                                         //40 Header
        "ExtraHealthFromGlobes",                        //41
        "LifePercent",                                  //42
        "LifeRegeneration",                             //43
        "LifeSteal",                                    //44
        "LifeAfterKill",                                //45
        "LifeOnHit",                                    //46
        "LifePerSpiritSpent",                           //47
        "Resource",                                     //48 Header
        "ArcanePowerOnCrit",                            //49
        "HatredRegeneration",                           //50
        "ManaRegeneration",                             //51
        "MaxArcanePower",                               //52
        "MaxDiscipline",                                //53
        "MaxFury",                                      //54
        "MaxMana",                                      //55
        "SpiritRegeneration",                           //56
        "Attributes",                                   //57 Header
        "Dexterity",                                    //58
        "Experience",                                   //59
        "Intelligence",                                 //60
        "Strength",                                     //61
        "Vitality",                                     //62
        "Skills",                                       //63 Header
        "BarbarianSkillBonusBash",                      //64
        "BarbarianSkillBonusCleave",                    //65
        "BarbarianSkillBonusFrenzy",                    //66
        "BarbarianSkillBonusHammerOfTheAncients",       //67
        "BarbarianSkillBonusOverpower",                 //68
        "BarbarianSkillBonusRend",                      //69
        "BarbarianSkillBonusRevenge",                   //70
        "BarbarianSkillBonusSeismicSlam",               //71
        "BarbarianSkillBonusWeaponThrow",               //72
        "BarbarianSkillBonusWhirlwind",                 //73
        "DemonHunterSkillBonusBolaShot",                //74
        "DemonHunterSkillBonusChakram",                 //75
        "DemonHunterSkillBonusElementalArrow",          //76
        "DemonHunterSkillBonusEntanglingShot",          //77
        "DemonHunterSkillBonusEvasiveFire",             //78
        "DemonHunterSkillBonusGrenades",                //79
        "DemonHunterSkillBonusHungeringArrow",          //80
        "DemonHunterSkillBonusImpale",                  //81
        "DemonHunterSkillBonusMultishot",               //82
        "DemonHunterSkillBonusRapidFire",               //83
        "DemonHunterSkillBonusSpikeTrap",               //84
        "MonkSkillBonusCripplingWave",                  //85
        "MonkSkillBonusCycloneStrike",                  //86
        "MonkSkillBonusDeadlyReach",                    //87
        "MonkSkillBonusExplodingReach",                 //88
        "MonkSkillBonusFistsOfThunder",                 //89
        "MonkSkillBonusLashingTailKick",                //90
        "MonkSkillBonusSweepingWind",                   //91
        "MonkSkillBonusTempestRush",                    //92
        "MonkSkillBonusWaveOfLight",                    //93
        "MonkSkillBonusWayOfTheHundredFists",           //94
        "WitchDoctorSkillBonusAcidCloud",               //95
        "WitchDoctorSkillBonusFirebats",                //96
        "WitchDoctorSkillBonusFirebomb",                //97
        "WitchDoctorSkillBonusHaunt",                   //98
        "WitchDoctorSkillBonusLocustSwarm",             //99
        "WitchDoctorSkillBonusPlagueOfToads",           //100
        "WitchDoctorSkillBonusPoisonDarts",             //101
        "WitchDoctorSkillBonusSpiritBarrage",           //102
        "WitchDoctorSkillBonusSummonZombieDogs",        //103
        "WitchDoctorSkillBonusWallOfZombies",           //104
        "WitchDoctorSkillBonusZombieCharger",           //105
        "WizardSkillBonusArcaneOrb",                    //106
        "WizardSkillBonusArcaneTorrent",                //107
        "WizardSkillBonusBlizzard",                     //108
        "WizardSkillBonusDisintegrate",                 //109
        "WizardSkillBonusElectrocute",                  //110
        "WizardSkillBonusEnergyTwister",                //111
        "WizardSkillBonusExplosiveBlast",               //112
        "WizardSkillBonusHydra",                        //113
        "WizardSkillBonusMagicMissile",                 //114
        "WizardSkillBonusMeteor",                       //115
        "WizardSkillBonusRayOfFrost",                   //116
        "WizardSkillBonusShockPulse",                   //117
        "WizardSkillBonusSpectralBlade",                //118
        "Properties",                                   //119 Header
        "GoldFind",                                     //120
        "HasSockets",                                   //121
        "Indestructible",                               //122
        "MagicFind",                                    //123
        "MovementSpeed",                                //124
        "PickupRadius",                                 //125
        "ReducedLevelRequirement"                       //126
    };

        public static int[][] StatRestrictions = {
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,97,98,100,111,114,119,121,122,126},// All One-Hand Item Types 0
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,119,121,122,126},// Axe 1
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,48,51,55,57,58,59,60,61,62,63,97,98,100,119,121,122,126},// Ceremonial Knife 2
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,48,50,53,57,58,59,60,61,62,119,121,122,126}, // Hand Crossbow 3
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,97,98,100,111,114,119,121,122,126},// Dagger 4
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,47,48,56,57,58,59,60,61,62,119,121,122,126},// Fist Weapon 5
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,119,121,122,126},// Mace 6
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,48,54,57,58,59,60,61,62,119,121,122,126},// Mighty Weapon 7
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,97,98,100,111,114,119,121,122,126},// Spear 8
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,111,114,119,121,122,126},// Sword 9
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,48,49,52,57,58,59,60,61,62,63,111,114,119,121,122,126},// Wand 10
        
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,47,48,54,56,57,58,59,60,61,62,63,67,68,71,73,90,92,93,97,98,100,101,102,104,105,111,114,119,121,122,126},// All Two-Hand Item Types 11
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,67,68,71,73,119,121,122,126},// Two-Handed Axe 12
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,97,98,100,101,102,104,105,119,121,122,126},// Bow 13
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,47,48,56,57,58,59,60,61,62,63,90,92,93,119,121,122,126},// Daibo 14
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,97,98,100,101,102,104,105,119,121,122,126},// Crossbow 15
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,67,68,71,73,119,121,122,126},// Two-Handed Mace 16
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,48,54,57,58,59,60,61,62,63,67,68,71,73,121,122,126},// Two-Handed Mighty Weapon 17
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,67,68,71,73,90,92,93,119,121,122,126},// Polearm 18
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,90,92,93,111,114,119,121,122,126},// Staff 19
        new int[] {0,1,5,6,8,9,10,11,12,14,15,16,19,22,29,30,31,32,33,34,35,36,37,38,40,44,45,46,57,58,59,60,61,62,63,67,68,71,73,119,121,122,126},// Two-Handed Sword 20
        
        new int[] {0,1,2,3,4,6,7,16,17,18,20,21,22,23,24,25,26,27,28,30,31,32,33,34,35,36,37,38,39,40,41,42,43,48,49,50,51,52,53,55,57,58,59,60,61,62,63,67,68,71,73,74,76,77,80,82,83,101,102,104,105,106,108,115,117,118,119,120,121,122,123},// All Off-Hand Item Types 21
        new int[] {0,1,6,16,18,22,23,30,31,32,33,34,35,36,37,38,40,41,42,43,48,51,55,57,58,59,60,61,62,63,101,102,104,105,119,120,121,122,123},// Mojo 22
        new int[] {0,1,6,16,18,22,23,30,31,32,33,34,35,36,37,38,40,41,42,43,48,49,52,57,58,59,60,61,62,63,106,108,115,117,118,119,120,121,122,123},// Source 23
        new int[] {0,1,6,16,18,22,23,30,31,32,33,34,35,36,37,38,40,41,42,43,48,50,53,57,58,59,60,61,62,63,74,76,77,80,82,83,119,120,121,122,123},// Quiver 24
        new int[] {0,1,2,3,4,6,7,16,17,18,20,21,22,23,24,25,26,27,28,30,31,32,33,34,35,36,37,38,39,40,41,42,43,57,58,59,60,61,62,63,63,67,68,71,73,74,76,77,80,82,83,101,102,104,105,106,108,115,117,118,119,120,121,122,123},// Shield 25
        
        new int[] {0,1,2,3,4,5,7,13,17,18,19,20,21,23,24,25,26,27,28,30,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,69,70,72,75,78,79,81,84,85,86,87,88,89,91,94,95,96,99,103,107,109,110,112,113,116,119,120,121,122,123,124,125,126},// All Armor Item Types 26
        new int[] {0,1,2,3,4,5,13,17,18,19,20,21,23,24,25,27,28,30,39,40,41,42,43,45,46,57,58,59,60,61,62,119,120,121,123},// Amulet  27
        new int[] {0,1,2,3,4,17,20,21,23,24,25,30,34,40,41,42,43,44,48,54,57,58,59,60,61,62,63,64,65,66,69,70,72,119,120,121,122,123,125,126},// Belt 28
        new int[] {0,1,2,3,4,17,20,21,23,24,25,30,35,40,41,43,57,58,59,60,61,62,119,120,122,123,124,125,126},// Boots 29
        new int[] {0,1,2,3,4,17,18,20,21,23,24,25,27,28,30,36,40,41,43,57,58,59,60,61,62,119,120,121,122,123,125,126},// Bracers 30
        new int[] {0,1,2,3,4,17,20,21,23,24,25,26,27,28,40,41,42,43,48,50,53,57,58,59,60,61,62,63,75,78,79,81,84,119,120,121,122,123,125,126},// Chest Armor 31
        new int[] {0,1,2,3,4,17,20,21,23,24,25,26,27,28,40,41,42,43,48,50,53,57,58,59,60,61,62,63,75,78,79,81,84,119,120,121,122,123,125,126},// Cloak 32
        new int[] {0,1,2,3,4,5,17,18,19,20,21,23,24,25,30,38,40,41,43,57,58,59,60,61,62,119,120,122,123,125,126}, //Gloves 33
        new int[] {0,1,2,3,4,17,18,20,21,23,24,25,30,33,39,40,41,42,43,47,48,49,51,52,55,56,57,58,59,60,61,62,63,85,86,87,88,89,91,94,95,96,99,103,107,109,110,112,113,116,119,120,121,122,123,125,126},// Helm 34
        new int[] {0,1,2,3,4,17,20,21,23,24,25,30,37,40,41,43,57,58,59,60,61,62,119,120,121,122,123,125,126},// Pants 35
        new int[] {0,1,2,3,4,17,20,21,23,24,25,30,34,40,41,42,43,44,48,54,57,58,59,60,61,62,63,64,65,66,69,70,72,119,120,121,122,123,125,126},// Mighty Belt 36
        new int[] {0,1,2,3,4,5,13,17,18,19,20,21,23,24,25,30,39,40,41,42,43,45,46,57,58,59,60,61,62,119,120,121,123},// Ring 37
        new int[] {0,1,2,3,4,17,20,21,23,24,25,30,32,40,41,42,43,57,58,59,60,61,62,119,120,122,123,125,126},// Shoulders 38
        new int[] {0,1,2,3,4,17,18,20,21,23,24,25,30,33,39,40,41,42,43,47,48,56,57,58,59,60,61,62,63,85,86,87,88,89,91,94,119,120,121,122,123,125,126},// Spirit Stone
        new int[] {0,1,2,3,4,17,18,20,21,23,24,25,30,33,39,40,41,42,43,48,51,55,57,58,59,60,61,62,63,95,96,93,95,96,99,103,119,120,121,122,123,125,126},// Voodoo Mask
        new int[] {0,1,2,3,4,17,18,20,21,23,24,25,30,33,39,40,41,42,43,48,49,52,57,58,59,60,61,62,63,107,109,110,112,113,116,119,120,121,122,123,125,126},// Wizard Hat

        new int[] {},			// All Follower Special Item Types
        new int[] {},			// Enchantress Focus
        new int[] {},			// Scoundrel Token
        new int[] {},			// Templar Relic
    };
        #endregion
        
        //Builds a list of stats for SelectStat()
        public static int[] GetStatList(int itemTypeIndex, int omitStat1Index, int omitStat2Index)
        {
            int[] baseList = StatRestrictions[itemTypeIndex];

            int listLen = baseList.Length;

            // can't omit "None"
            //TODO: Headers can make listLen-=2 so check for this
            if (omitStat1Index == 0) omitStat1Index = -1;
            if (omitStat2Index == 0) omitStat2Index = -1;

            if (omitStat1Index != -1) listLen--;
            if (omitStat2Index != -1) listLen--;


            int[] list = new int[listLen];

            int lPos = 0;
            for (int i = 0; i < baseList.Length; i++)
            {
                if (baseList[i] != omitStat1Index && baseList[i] != omitStat2Index)
                {
                    if (lPos < list.Length)
                    {
                        list[lPos] = baseList[i];
                        lPos++;
                    }
                }
            }
            return list;
        }

        public static int[] GetStatList(int itemTypeIndex, int omitStat1Index) { return GetStatList(itemTypeIndex, omitStat1Index, -1); }
        public static int[] GetStatList(int itemTypeIndex) { return GetStatList(itemTypeIndex, -1, -1); }

        public static int GetSubTypeByName(string subTypeName)
        {
            for (int i = 0; i < ItemSubTypes.Length; i++)
            {
                if (subTypeName.Equals(ItemSubTypes[i])) 
                    return i;
            }
            return -1;
        }

        public static int GetStatByName(string statName)
        {
            for (int i = 0; i < Stats.Length; i++)
            {
                if (statName.Equals(Stats[i]))
                    return i;
            }
            return -1;
        }

        public static int GetQualityByName(string qualityName)
        {
            for (int i = 0; i < Qualities.Length; i++)
            {
                if (qualityName.Equals(Qualities[i])) return i;
            }
            return -1;
        }

        public static int GetSearchOptionByName(string option)
        {
            for (int i = 0; i < SearchOptions.Length; i++)
            {
                if (option.Equals(SearchOptions[i])) return i;
            }
            return -1;
        }

        // returns the first class that can use a given type
        public static int GetClassToUseSubType(int subType)
        {
            for (int c = 0; c < ClassRestrictions.Length; c++)
                if (CanClassUseSubType(c, subType)) return c;
            return -1;
        }
        public static bool CanClassUseSubType(int classIndex, int subType)
        {
            if (classIndex == -1) return false; // -1 = unknown class
            for (int i = 0; i < ClassRestrictions[classIndex].Length; i++)
                if (ClassRestrictions[classIndex][i] == subType) return false;
            return true;
        }

        public static bool CanSubTypeUseStat(int subTypeIndex, int statIndex)
        {
            if (subTypeIndex == -1) return false; // -1 = unknown type
            for (int i = 0; i < StatRestrictions[subTypeIndex].Length; i++)
                if (StatRestrictions[subTypeIndex][i] == statIndex) return true;
            return false;
        }

        //Gets Sup Type index for SelectOption() from an ItemSubTypes index
        public static int GetSupTypeIndex(int subType)
        {
            if (subType <= 10) return 0; // one-handed
            if (subType <= 20) return 1; // two-handed
            if (subType <= 25) return 2; // off-hand
            if (subType <= 41) return 3; // armor
            if (subType <= 45) return 4; // follower
            return -1;
        }

        //Gets Sub Type index for SelectOption() from an ItemSubTypes index
        public static int GetSubTypeIndex(int subType)
        {
            if (subType > 41) return subType - 42; // follower
            if (subType > 25) return subType - 26; // armor
            if (subType > 20) return subType - 21; // off-hand
            if (subType > 10) return subType - 11; // two-handed
            return subType; // one-handed
        }

        //Gets number of options for particular supType
        public static int GetNumOptionsPerPage(int supType)
        {
            if (supType == 0) return 11;
            if (supType == 1) return 10;
            if (supType == 2) return 5;
            if (supType == 3) return 15;
            if (supType == 4) return 4;
            return -1;
        }

        //This is used in SelectOption() so we know number of Sub types for the corresponding Sup type
        public static int GetNumSubTypes(int supType)
        {
            int[] list = new int[] { 11, 10, 5, 16, 4 };

            return list[supType];
        }

        public static int GetNumSupTypes()
        {
            return ItemSupTypes.Length;
        }

        public static int GetNumClasses()
        {
            return SearchClasses.Length;
        }

        public static int GetNumQualities()
        {
            return Qualities.Length;
        }

        //TODO: Implement this
        public static int GetNumCharacters()
        {
            return 1;
        }

        //public static int[] AH_GetPossibleCurStats(int index)
        //{
        //    bool[] matchFlags = new bool[SQF.Stats.Length];
        //    int numMatches = 0;
        //    for (int i = 0; i < matchFlags.Length; i++)
        //        if ()
        //        {
        //            matchFlags[i] = true;
        //            numMatches++;
        //        }

        //    if (numMatches == 0) return new int[] { -1 };

        //    int[] matches = new int[numMatches];
        //    int j = 0;
        //    for (int i = 0; i < matchFlags.Length; i++)
        //    {
        //        if (matchFlags[i])
        //            matches[j++] = i;
        //    }
        //    return matches;
        //}
    }

}
