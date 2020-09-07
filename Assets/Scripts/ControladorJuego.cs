using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    public static ControladorJuego Instancia;
    
    [Header("Audio cuando se obtiene punto")]
    [SerializeField] private AudioClip audioPunto;
    [Header("Audio cuando golpe")]
    [SerializeField] private AudioClip audioGolpe;
    AudioSource fuenteDeAudio;
    [Header("Imagen titulo")]
    [SerializeField] private Image imgTitulo;
    [Header("Imagen precionar espacio")]
    [SerializeField] private Image imgPrecionarEspacio;
    [Header("Imagen game over")]
    [SerializeField] private Image imgGameOver;
    [Header("Imagen precionar enter")]
    [SerializeField] private Image imgPrecionarEnter;
    [Header("UI para mostrar el puntaje")]
    [SerializeField] private TextMeshProUGUI txtPuntaje;
    [Header("UI para mostrar el tiempo de juego")]
    [SerializeField] private TextMeshProUGUI txtTiempoJuego;    
    private bool banderaTiempo = true;
    private int puntaje = 0;
    private float tiempoJuegoMinutos;
    private float tiempoJuegoSegundos;

    private void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        fuenteDeAudio = GetComponent<AudioSource>();
        Personaje.Instancia.onMorir += Instancia_onMorir;
    }

    private void Instancia_onMorir()
    {
        banderaTiempo = false;
        FinJuego();
    }

    private void FixedUpdate()
    {
        if (puntaje >= 1)
        {
            IniciarTiempo();
        }
    }

    public void AudioPunto()
    {
        fuenteDeAudio.clip = audioPunto;
        fuenteDeAudio.Play();
    }

    public void AudioGolpe()
    {
        fuenteDeAudio.clip = audioGolpe;
        fuenteDeAudio.Play();
    }

    public void FinJuego()
    {
        imgGameOver.enabled = true;
        imgPrecionarEnter.enabled = true;
    }

    public void IniciarJuego()
    {
        Time.timeScale = 1;
        imgTitulo.enabled = false;
        imgPrecionarEspacio.enabled = false;
        txtTiempoJuego.enabled = true;
        txtPuntaje.enabled = true;
    }

    public void IncrementarPuntaje()
    {
        if (puntaje < 9)
        {
            txtPuntaje.text = string.Concat("0", (puntaje += 1).ToString());
        }
        else
        {
            txtPuntaje.text = (puntaje += 1).ToString();
        }
    }

    public void IncrementarPuntajeMoneda()
    {
        if (puntaje < 9)
        {
            txtPuntaje.text = string.Concat("0", (puntaje += 5).ToString());
        }
        else
        {
            txtPuntaje.text = (puntaje += 5).ToString();
        }
    }

    public void IniciarTiempo()
    {
        if (banderaTiempo)
        {
            if (tiempoJuegoSegundos >= 60)
            {
                tiempoJuegoMinutos += 1;
                tiempoJuegoSegundos = 0;
            }
            else
            {
                tiempoJuegoSegundos += Time.deltaTime;
            }

            if (tiempoJuegoMinutos < 9)
            {
                if (tiempoJuegoSegundos <= 10)
                {
                    txtTiempoJuego.text = string.Concat("0", tiempoJuegoMinutos, ":", "0", Mathf.Floor(tiempoJuegoSegundos).ToString());
                }
                else
                {
                    txtTiempoJuego.text = string.Concat("0", tiempoJuegoMinutos, ":", Mathf.Floor(tiempoJuegoSegundos).ToString());
                }
            }
            else
            {
                if (tiempoJuegoSegundos <= 10)
                {
                    txtTiempoJuego.text = string.Concat(tiempoJuegoMinutos, ":", "0", Mathf.Floor(tiempoJuegoSegundos).ToString());
                }
                else
                {
                    txtTiempoJuego.text = string.Concat(tiempoJuegoMinutos, ":", Mathf.Floor(tiempoJuegoSegundos).ToString());
                }
            }
        }
    }
}
