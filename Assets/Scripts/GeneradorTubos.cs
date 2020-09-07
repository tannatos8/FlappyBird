using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Apple;

public class GeneradorTubos : MonoBehaviour
{
    [Header("Pre.Fabricados del grupo de tubos")]
    [SerializeField] private GameObject gruposTubos;
    [Header("Tiempo en segundos para la creacion de grupos de tubos")]
    [SerializeField] private float tiempoCreacionTubos;
    private float tiempoTemporar;
    private bool banderaProduccion = true;


    // Start is called before the first frame update
    void Start()
    {
        tiempoTemporar = tiempoCreacionTubos;
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
                Instantiate(gruposTubos, new Vector3(transform.position.x, Random.Range(-1f, 1.1f), 0), Quaternion.identity, transform);
                tiempoTemporar = tiempoCreacionTubos;
            }
        }
    }
}
