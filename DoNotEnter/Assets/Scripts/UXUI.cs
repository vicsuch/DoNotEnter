using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UXUI : MonoBehaviour
{
    public TextMeshProUGUI killText;
    public TextMeshProUGUI hogueraText;
    public TextMeshProUGUI balas;
    public SaludJugador componenteenc;
    public Image healthBarFill;
    public Image agarre;
    public bool a = false;
    public PlayerItemGrabber numin;
    public string objetoMano;
    // Start is called before the first frame update
    void Start()
    {
        numin = gameObject.GetComponent<PlayerItemGrabber>();
        componenteenc = gameObject.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        killText.text = "Kills: " + componenteenc.zombiesAsesinados.ToString();
       
        float fillAmount = componenteenc.vida / 100.0f;
        healthBarFill.fillAmount = fillAmount;
        objetoMano=numin.PasarArma().name;
        balas.text = "Blasa: "+ numin.PasarArma().GetComponent<ItemData>().balasRestantes.ToString();


    }
    
    private void OnTriggerEnter(Collider other)
    {
        agarre.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        agarre.gameObject.SetActive(false);
    }
}
