using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField] private Button continueCampaign;
    [SerializeField] private string pathSavedProgressionCampaign;

    private void Start()
    {
        isCampaignInProgression();
    }

    private void isCampaignInProgression()
    {
        if(new DirectoryInfo(pathSavedProgressionCampaign).GetFiles("*.*").Length == 0)
        {
            continueCampaign.enabled = false;
            Debug.Log("desactivate");
        }
        Debug.Log(new DirectoryInfo(pathSavedProgressionCampaign).GetFiles("*.*").Length);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
       
    }
}
