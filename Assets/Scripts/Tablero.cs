using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tablero;


public class Tablero : MonoBehaviour
{
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
        public Efectos efecto;
        public Color colorMuestra;
    }

    public GameObject objetoCasilla;
    public int ancho = 14;
    public int alto = 8;

    public Casilla[] casillasTablero;

    void Start()
    {
        casillasTablero = GenerarTablero(ancho, alto, coloresDefinidos);
        GeneraCaminoEnTablero(casillasTablero, ancho, alto, coloresDefinidos, (altura)=> Random.Range(0, alto));
        MuestraTablero(casillasTablero);
    }

    public static Casilla[] GenerarTablero(int ancho, int alto, List<Color> colores)
    {
        int numero;
        int numeroCamino = Random.Range(0, alto);
        Casilla[] tablero = new Casilla[ancho * alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                numero = i * alto + j;
                //  tablero[numero] = CrearCasilla(ObtenerAleatorio(), colores);
                tablero[numero] = CrearCasilla(1, colores);
            }
        }
        return tablero;
    }

    public static void GeneraCaminoEnTablero(Casilla[] tablero, int ancho, int alto, List<Color> colores, System.Func<int,int> ObtenerAltura)
    {
        int numero;
        int alturaCamino = ObtenerAltura(alto);
        for(int i = 0;i < ancho;i++)
        {
            numero = i * alto + alturaCamino;
            tablero[numero] = CrearCasilla((int)Efectos.permitido, colores);

            if(i%2 == 1)
            {
                int anterior = alturaCamino;
                alturaCamino = ObtenerAltura(alto);

                for ( int j = Mathf.Min(anterior, alturaCamino); j <= Mathf.Max(anterior, alturaCamino); j++)
                {
                    numero = i * alto + j;
                    tablero[numero] = CrearCasilla((int)Efectos.permitido, colores);
                }
            }
        }
    }


    public void MuestraTablero(Casilla[] tablero)
    {
        int numero;
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                numero = i * alto + j;
                GameObject objetoCasilla = MuestraCasilla(i, j, tablero[numero]);

                if (CasillaEsTransitable(tablero[numero])) DesactivaCollision(objetoCasilla);                
            }
        }
    }

    public GameObject MuestraCasilla(int x, int y, Casilla casilla)
    {
        GameObject nuevaCasilla = Instantiate(objetoCasilla, new Vector3(CentrarPosicion(x, ancho), CentrarPosicion(y, alto), 0f), Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCasilla.GetComponent<SpriteRenderer>();
        spriteRenderer.color = casilla.colorMuestra;
        return nuevaCasilla;
    }

    public static Casilla CrearCasilla(int tipoCasilla, List<Color> posiblesMuestras)
    {
        var nuevaCasilla = new Casilla();

        nuevaCasilla.efecto = (Efectos)tipoCasilla;
        nuevaCasilla.colorMuestra = posiblesMuestras[tipoCasilla];
        return nuevaCasilla;
    }

    public static int ObtenerAleatorio()
    {
        return Random.Range(0, System.Enum.GetValues(typeof(Efectos)).Length);
    }

    public static int CentrarPosicion(int n, int total)
    {        
        return n - (total / 2);
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
