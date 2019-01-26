
using UnityEngine;
using UnityEngine.Serialization;

public class TowerUI : MonoBehaviour
{
    public GameObject permitted;
    public GameObject notPremitted;
    public GameObject confirmUI, sellUI;
    public Collider sellCollider; 

    public void ActivePermittedArea(bool permittedArea)
    {
        permitted.SetActive(permittedArea);
        notPremitted.SetActive(!permittedArea);
    }

    public void ActiveConfirmUI(bool active)
    {
        confirmUI.SetActive(active);
    }
    
    public void ActiveSellUI(bool active)
    {
        sellUI.SetActive(active);
    }

    public void Disable()
    {
        permitted.SetActive(false);
        notPremitted.SetActive(false);
        confirmUI.SetActive(false);
        sellUI.SetActive(false);
        sellCollider.enabled = true;
    }
}
