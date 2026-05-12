using UnityEngine;
using UnityEngine.SceneManagement;

public class EsceneManager : MonoBehaviour
{
    [Header("Configuración de Interacción Física (Opcional)")]
    [Tooltip("Nombre de la escena a cargar al presionar 'E' cerca de este objeto.")]
    public string escenaDestino;

    [Tooltip("Arrastra aquí el Text o Canvas (UI) que mostrará la letra 'E'.")]
    public GameObject textoInteraccion;

    [Tooltip("Arrastra aquí el objeto 3D que servirá como borde o halo blanco.")]
    public GameObject objetoResaltado;

    private bool jugadorCerca = false;

    private void Start()
    {
        // Aseguramos que el texto de interacción empiece apagado
        if (textoInteraccion != null)
        {
            textoInteraccion.SetActive(false);
        }

        // Aseguramos que el borde blanco empiece apagado
        if (objetoResaltado != null)
        {
            objetoResaltado.SetActive(false);
        }
    }

    private void Update()
    {
        // Si el jugador está dentro del área y presiona la tecla E
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            CargarEscenaPorNombre(escenaDestino);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto que entró en el área tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (textoInteraccion != null)
            {
                textoInteraccion.SetActive(true); // Muestra el mensaje "E"
            }
            if (objetoResaltado != null)
            {
                objetoResaltado.SetActive(true); // Muestra el borde/halo
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detecta si el jugador salió del área
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (textoInteraccion != null)
            {
                textoInteraccion.SetActive(false); // Oculta el mensaje "E"
            }
            if (objetoResaltado != null)
            {
                objetoResaltado.SetActive(false); // Oculta el borde/halo
            }
        }
    }

    public void CargarEscenaPorNombre(string nombreDeLaEscena)
    {
        if (string.IsNullOrEmpty(nombreDeLaEscena))
        {
            Debug.LogWarning("CambiarEscena: El nombre de la escena está vacío. Asigna un nombre válido.");
            return;
        }

        PrepararCursorParaUI();
        SceneManager.LoadScene(nombreDeLaEscena);
    }

    public void CargarEscenaPorIndice(int indiceEscena)
    {
        PrepararCursorParaUI();
        SceneManager.LoadScene(indiceEscena);
    }

    public void SalirDelJuego()
    {
        Debug.Log("CambiarEscena: Ejecutando Application.Quit(). Cerrando el juego...");
        Application.Quit();
    }

    private void PrepararCursorParaUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
