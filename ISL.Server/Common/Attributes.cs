using System;

namespace ISL.Server.Common
{
    /**
 * A series of hardcoded attributes that must be defined.
 * FIXME: Much of these serve only to indicate derivatives, and so would not be
 * needed once this is no longer a hardcoded system.
 */
    public enum Attributes
    {
        // Base Statistics
        ATTR_STR               = 1,
        ATTR_AGI               = 2,
        ATTR_VIT               = 3,
        ATTR_INT               = 4,
        ATTR_DEX               = 5,
        ATTR_WIL               = 6,
        
        // Derived attributes
        ATTR_ACCURACY          = 7,
        ATTR_DEFENSE           = 8,
        ATTR_DODGE             = 9,
        
        ATTR_MAGIC_DODGE       = 10,
        ATTR_MAGIC_DEFENSE     = 11,
        
        ATTR_BONUS_ASPD        = 12,
        
        ATTR_HP                = 13,
        ATTR_MAX_HP            = 14,
        ATTR_HP_REGEN          = 15,
        
        
        // Separate primary movespeed (tiles * second ^-1) and derived movespeed (raw)
        ATTR_MOVE_SPEED_TPS    = 16,
        ATTR_MOVE_SPEED_RAW    = 17,
        
        // Money and inventory size attributes.
        ATTR_GP                = 18,
        ATTR_INV_CAPACITY      = 19,
        
        /**
     * Temporary attributes.
     * @todo Use AutoAttacks instead.
     */
        MOB_ATTR_PHY_ATK_MIN   = 20,
        MOB_ATTR_PHY_ATK_DELTA = 21,
        MOB_ATTR_MAG_ATK       = 22
    }
    ;
}

