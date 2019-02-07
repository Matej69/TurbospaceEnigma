using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTypes : MonoBehaviour {

    public enum e_type { MAGMA, ICE, WATER, VEGETATION, ANTI_MATER }
    public enum e_bonusEffect { GOOD, NEUTRAL, BAD }

    public static e_bonusEffect HasBonusEffect(e_type type1, e_type type2)
    {
        if (type1 == e_type.MAGMA) {
            if (type1 == e_type.VEGETATION || type1 == e_type.ICE)
                return e_bonusEffect.GOOD;
            else if (type1 == e_type.WATER || type1 == e_type.ANTI_MATER)
                return e_bonusEffect.BAD;
        }
        else if (type1 == e_type.ICE)
        {
            if (type1 == e_type.VEGETATION || type1 == e_type.WATER)
                return e_bonusEffect.GOOD;
            else if (type1 == e_type.MAGMA || type1 == e_type.ANTI_MATER)
                return e_bonusEffect.BAD;
        }
        else if (type1 == e_type.WATER)
        {
            if (type1 == e_type.MAGMA || type1 == e_type.ANTI_MATER)
                return e_bonusEffect.GOOD;
            else if (type1 == e_type.VEGETATION || type1 == e_type.ICE)
                return e_bonusEffect.BAD;
        }
        else if (type1 == e_type.VEGETATION)
        {
            if (type1 == e_type.WATER || type1 == e_type.ANTI_MATER)
                return e_bonusEffect.GOOD;
            else if (type1 == e_type.MAGMA || type1 == e_type.ICE)
                return e_bonusEffect.BAD;
        }
        else if (type1 == e_type.ANTI_MATER)
        {
            if (type1 == e_type.MAGMA || type1 == e_type.ICE)
                return e_bonusEffect.GOOD;
            else if (type1 == e_type.VEGETATION || type1 == e_type.WATER)
                return e_bonusEffect.BAD;
        }
        return e_bonusEffect.NEUTRAL;
    }



    //Could be later switched with dinamic reading of collider x size
    static public float GetPlanetRadius()
    {
        return 0.75f;
    }


}
