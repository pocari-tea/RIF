using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOC : MonoBehaviour
{
    [SerializeField] private GameObject OPanel;
    [SerializeField] private GameObject CPanel;
    
    public void XBtn()
    {
        StartCoroutine(XCo());
    }
    
    public void XBtn2()
    {
        StartCoroutine(XCo2());
    }

    private IEnumerator XCo()
    {
        OPanel.SetActive(true);
        CPanel.SetActive(false);
        
        yield return null;
    }
    
    private IEnumerator XCo2()
    {
        CPanel.SetActive(false);
        
        yield return null;
    }
}
