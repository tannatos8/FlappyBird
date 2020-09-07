using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [Header("Pre.Fabricados de moneda")]
    [SerializeField] private GameObject moneda;
    [Header("Tiempo en segundos para la creacion de monedas")]
    [SerializeField] private float tiempoCreacionMonedas;
    private float tiempoTemporar;
    private bool banderaProduccion = true;


    // Start is called before the first frame update
    void Start()
    {
        tiempoTemporar = tiempoCreacionMonedas;
        Personaje.Instancia.onMorir += Instancia_onMorir;
    }

    private void Instancia_onMorir()
    {
        banderaProduccion = false;
    }

    private void FixedUpdate()
    {
        if (banderaProduccion)
        {
            tiempoTemporar -= Time.deltaTime;
            if (tiempoTemporar <= 0)
            {
                Instantiate(moneda, new Vector3(transform.position.x, Random.Range(-1.3f, 1.4f), 0), Quaternion.identity, transform);
                tiempoTemporar = tiempoCreacionMonedas;
            }
        }
    }
}
