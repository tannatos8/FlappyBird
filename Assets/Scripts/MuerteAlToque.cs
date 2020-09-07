using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteAlToque : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Personaje"))
        {
            ControladorJuego.Instancia.AudioGolpe();
            Personaje.Instancia.Morir();
        }
    }
}
