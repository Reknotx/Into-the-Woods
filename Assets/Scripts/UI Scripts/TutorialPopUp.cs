using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialPopUp : MonoBehaviour
{
    [System.Serializable]
    public struct TutorialInfo
    {
        [SerializeField]
        private string ClipName;
        public VideoClip clip;
        public RenderTexture texture;
        public string blurb;
        public string description;
    }

    private int currentPage = 0;
    private int maxPages = 7; // Add 1 mentally, since we start at 0.

    public TMPro.TMP_Text blurb;
    public TMPro.TMP_Text details;
    public TMPro.TMP_Text UIPageNumber;
    public RawImage videoImage;

    public VideoPlayer player;

    public List<TutorialInfo> videoInfo;

    private void Awake()
    {

        RefreshUIChoice();
    }
    private void Start()
    {
        Time.timeScale = 0f;
    }

    #region ButtonFunctions
    public void ScrollRight()
    {
        currentPage++;
        if (currentPage > maxPages)
        {
            currentPage = 0;
        }
        RefreshUIChoice();
    } 

    public void ScrollLeft()
    {
        currentPage--;
        if (currentPage < 0)
        {
            currentPage = maxPages;
        }
        RefreshUIChoice();
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    #endregion

    private void RefreshUIChoice()
    {
        UIPageNumber.text = ((currentPage + 1) + "/" + (maxPages + 1));
        TutorialInfo temp = videoInfo[currentPage];

        blurb.text = temp.blurb;
        details.text = temp.description;
        player.targetTexture = temp.texture;
        videoImage.texture = temp.texture;
        player.clip = temp.clip;
    }
}
