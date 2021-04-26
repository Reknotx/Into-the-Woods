using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMessage : MonoBehaviour
{
    public void FirePlayerSpell()
    {
        Player.Instance.FireSpell = true;
    }
}
