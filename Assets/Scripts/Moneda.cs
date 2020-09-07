using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [Header("Velocidad de las monedas cuando se transladan")]
    [SerializeField] private float velocidadMoneda;
    private bool movimientoTubo = true;
    private float tiempoTubos = 4;

    // Start is called before the first frame update
    void Start()
    {
        Personaje.Instancia.onMorir += Instancia_onMorir;
    }

    void OnDestroy()
    {
        Personaje.Instancia.onMorir -= Instancia_onMorir;
    }

    private void Instancia_onMorir()
    {
        movimientoTubo = false;
    }

    private void FixedUpdate()
    {
        if (movimientoTubo)
        {
            transform.position += (Vector3.left * velocidadMoneda) * Time.deltaTime;
            if ((tiempoTubos -= Time.deltaTime) <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Personaje"))
        {
            ControladorJuego.Instancia.AudioPunto();
            ControladorJuego.Instancia.IncrementarPuntajeMoneda();
            Destroy(gameObject);
        }
    }
}
