using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tubo : MonoBehaviour
{
    [Header("Velocidad de los tubos cuando se transladan")]
    [SerializeField] private float velocidadTubo;
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

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (movimientoTubo)
        {
            transform.position += (Vector3.left * velocidadTubo) * Time.deltaTime;
            if ((tiempoTubos -= Time.deltaTime) <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Personaje"))
        {
            ControladorJuego.Instancia.AudioPunto();
            ControladorJuego.Instancia.IncrementarPuntaje();
        }        
    } 
}
