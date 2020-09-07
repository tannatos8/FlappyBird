using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour
{
    public static Personaje Instancia;
    [Header("Audio cuando se preciona escape")]
    [SerializeField] private AudioClip audioVuelo;
    AudioSource fuenteDeAudio;
    [Header("Estado del personaje")]
    [SerializeField]private EstadoPersonaje estado;
    [Header("Vuelo")]
    [SerializeField] private float vuelo;
    private bool banderaInicio = true;
    private Rigidbody2D personaje;
    private Animator anim;

    //Eventos
    public event Action onMorir;


    private void Awake()
    {
        Instancia = this;
    }

    //Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        fuenteDeAudio = GetComponent<AudioSource>();
        personaje = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estado == EstadoPersonaje.vivo)
        {
            if (banderaInicio)
            {
                ControladorJuego.Instancia.IniciarJuego();
                banderaInicio = false;
            }
            fuenteDeAudio.clip = audioVuelo;
            fuenteDeAudio.Play();
            personaje.velocity = Vector2.up * vuelo;
        }

        if (Input.GetKeyDown(KeyCode.Return) && estado == EstadoPersonaje.muerto)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Vivir()
    {
        anim.SetBool("Vida", true);
        estado = EstadoPersonaje.vivo;
    }

    public void Morir()
    {
        anim.SetBool("Vida", false);
        personaje.rotation = 270;
        estado = EstadoPersonaje.muerto;
        onMorir?.Invoke();
    }
}

public enum EstadoPersonaje
{
    vivo, muerto
}
