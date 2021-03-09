using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    
    public void PopUp(Collectable collected)
    {
        Type collectableType = collected.GetType();

        if (collected is Avocado)
        {

        }
        else if (collected is AttackCandy)
        {

        }
        else if (collected is Totem)
        {

        }
        else if (collected is PotionIngredient)
        {

        }
    }
}
