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
    public GameObject agarre;
    public bool a = false;
    public PlayerItemGrabber numin;
    public string objetoMano;
    public bool quitarCartel=false;
    // Start is called before the first frame update
    void Start()
    {
        numin = gameObject.GetComponent<PlayerItemGrabber>();
        componenteenc = gameObject.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        killText.text =  componenteenc.zombiesAsesinados.ToString();
       
        float fillAmount = componenteenc.vida / 100.0f;
        healthBarFill.fillAmount = fillAmount;
        if (numin.PasarArma() != null)
        {
            objetoMano = numin.PasarArma().name;
            balas.text = numin.PasarArma().GetComponent<ItemData>().balasRestantes.ToString();

        }
        if (quitarCartel)
        {
            agarre.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown("e"))
        {
            quitarCartel = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gunsTag")&&other.GetComponent<ItemData>().isGrabbed==false)
        {
            agarre.gameObject.SetActive(true);
            quitarCartel = false;
        }

      
    }
    private void OnTriggerExit(Collider other)
    {
            agarre.gameObject.SetActive(false);
       
        
    }
}
