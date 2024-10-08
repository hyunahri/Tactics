﻿namespace Items
{
    /// <summary>
    /// There's no fundamental difference between weapon types, it's just used to control itemization for the different classes.
    /// i.e. a warrior can only equip a weapon of type SWORD1H or SWORD2H, etc.
    /// Should try to keep to general themes within each type though, maybe 2H reduces initiative but increases damage, etc.
    /// </summary>
    public enum WeaponTypes
    {
        DAGGER = 1000,
        SWORD1H = 1100,
        SWORD2H = 1101,
        
        AXE1H = 1200,
        AXE2H = 1201,
        
        SPEAR1H = 1300,
        SPEAR2H = 1301,
        
        SHIELDSMALL = 2000,
        SHIELDLARGE = 2001,
        
        BOW = 3000,
        CROSSBOW = 3001,
        
        STAFF = 4000,
        WAND = 4001,
        
    }
}