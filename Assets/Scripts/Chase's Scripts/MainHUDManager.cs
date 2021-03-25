using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHUDManager : SingletonPattern<MainHUDManager>
{
    public UI_Inventory inventoryUI;

    public HealthUI healthUI;
}
