using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject OptionsPanel;

    public void OpenPanel()
    {
        OptionsPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        OptionsPanel.SetActive(false);
    }
}
