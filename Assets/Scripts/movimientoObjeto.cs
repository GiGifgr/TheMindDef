using UnityEngine;

public class movimientoObjeto : MonoBehaviour
{
    public Camera camaraJugador;

    public float rangoX = 5f; 
    public float rangoZ = 5f; 

    private Vector3 posicionInicial;

    private bool seVeAhorita;
    private bool seVioAntes;
    private bool dejoDeVerse;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        seVeAhorita = EstaEnPantalla();
        
        if (seVioAntes && !seVeAhorita)
        {
            dejoDeVerse = true;
        }
        
        if (!seVioAntes && seVeAhorita && dejoDeVerse)
        {
            CambiarDeLugar();
            dejoDeVerse = false;
        }

        seVioAntes = seVeAhorita;
    }

    bool EstaEnPantalla()
    {
        Vector3 punto = camaraJugador.WorldToViewportPoint(transform.position);

        return punto.z > 0 &&
               punto.x > 0 && punto.x < 1 &&
               punto.y > 0 && punto.y < 1;
    }

    void CambiarDeLugar()
    {
        float nuevoX = posicionInicial.x + Random.Range(-rangoX, rangoX);
        float nuevoZ = posicionInicial.z + Random.Range(-rangoZ, rangoZ);

        // Mantiene la misma altura (Y)
        transform.position = new Vector3(nuevoX, posicionInicial.y, nuevoZ);
    }
}