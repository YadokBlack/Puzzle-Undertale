using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tablero : MonoBehaviour
{
    const int libre = 0;
    public enum Colores
    {
        Rosa,
        Roja
    }

    public enum Efectos
    {
        permitido,
        bloqueado
    }

    public List<Color> coloresDefinidos = new List<Color> {
        new Color(1.0f, 0.6f, 0.8f),
        Color.red
    };

    public class Casilla
    {
        public Colores color;
        public Efectos efecto;
        public Color colorMuestra;
    }

    public GameObject objetoCasilla;
    public int ancho = 14;
    public int alto = 8;

    public Casilla[] casillasTablero;

    void Start()
    {
        casillasTablero = GenerarTablero();
    }

    public Casilla[] GenerarTablero()
    {
        int numero;
        int numeroCamino = Random.Range(0, alto);
        Casilla[] tablero = new Casilla[ancho * alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                numero = i * alto + j;
                if (numeroCamino != j) 
                {
                    tablero[numero] = CrearCasilla(ObtenerAleatorio(), coloresDefinidos);
                }
                else
                {
                    tablero[numero] = CrearCasilla(libre, coloresDefinidos);
                }
                CrearNuevaCasilla(i, j, tablero[numero]);
            }
        }
        return tablero;
    }

    public static Casilla CrearCasilla(int numAleatorio, List<Color> posiblesMuestras)
    {
        var nuevaCasilla = new Casilla();

        nuevaCasilla.color = (Colores)numAleatorio;
        nuevaCasilla.efecto = (Efectos)numAleatorio;
        nuevaCasilla.colorMuestra = posiblesMuestras[numAleatorio];
        return nuevaCasilla;
    }

    public static int ObtenerAleatorio()
    {
        return Random.Range(0, System.Enum.GetValues(typeof(Colores)).Length);
    }

    public static int CentrarPosicion(int n, int total)
    {        
        return n - (total / 2);
    }

    public void CrearNuevaCasilla(int x, int y, Casilla casilla)
    {
        GameObject nuevaCasilla = Instantiate(objetoCasilla, new Vector3(CentrarPosicion(x, ancho), CentrarPosicion(y, alto), 0f), Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCasilla.GetComponent<SpriteRenderer>();
        spriteRenderer.color = casilla.colorMuestra;

        if (CasillaEsTransitable(casilla))
        {
            DesactivaCollision(nuevaCasilla);
        }
    }

    private bool CasillaEsTransitable(Casilla casilla)
    {
        return casilla.efecto == Efectos.permitido;
    }

    private static void DesactivaCollision(GameObject nuevaCasilla)
    {
        BoxCollider2D boxCollider2D = nuevaCasilla.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
}
