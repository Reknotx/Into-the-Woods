using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
///  Author: JT Esmond
///  Date: 2/4/2021
///  <summary>
/// The UI class that handles the physical representation of the day night cycle
///  </summary>

public class ClockUI : MonoBehaviour
{
    /// <summary> Singleton instance of the CLock UI </summary>
    public static ClockUI Instance;

    /// <summary>
    /// variables for the UI script
    /// </summary>
    private int limit;

    public void Awake()
    {

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

    }

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }

    //potential clock coroutine (not needed for now)
    #region
    /*
    private IEnumerator timer()
    {
        sliderHolder.transform.position = new Vector3(-85, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-84.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-83.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-82.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-81.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-81, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-80.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-79.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-78.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-77.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-77, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-76.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-75.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-74.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-73.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-73, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-72.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-71.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-70.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-69.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-69, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-68.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-67.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-66.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-65.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-65, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-64.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-63.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-62.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-61.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-61, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-60.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-59.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-58.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-57.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-57, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-56.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-80.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-79.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-78.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-77.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-77, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-76.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-75.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-74.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-73.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-73, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-72.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-71.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-70.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-69.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-69, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-68.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-67.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-66.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-65.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-65, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-64.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-63.4f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-62.6f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-61.8f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-61, 498.36f, 0);
        yield return new WaitForSeconds(1f);
        sliderHolder.transform.position = new Vector3(-60.2f, 498.36f, 0);
        yield return new WaitForSeconds(1f);
    }*/
    #endregion
}
