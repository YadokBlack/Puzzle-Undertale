using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tablero;

public class GeneradorTablero
{
    public Casilla[] Generar(int ancho, int alto, List<Color> colores, System.Func<int, int> ObtenerAltura)
    {
        var casillasTablero = GenerarTablero(ancho, alto, colores);
        GeneraCaminoEnTablero(casillasTablero, ancho, alto, colores, ObtenerAltura);
        return casillasTablero;
    }

    public Casilla[] GenerarTablero(int ancho, int alto, List<Color> colores)
    {
        int numero;
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

    public void GeneraCaminoEnTablero(Casilla[] tablero, int ancho, int alto, List<Color> colores, System.Func<int, int> ObtenerAltura)
    {
        int numero;
        int alturaCamino = ObtenerAltura(alto);
        for (int i = 0; i < ancho; i++)
        {
            numero = i * alto + alturaCamino;
            tablero[numero] = CrearCasilla((int)Efectos.permitido, colores);

            if (i % 2 == 1)
            {
                int anterior = alturaCamino;
                alturaCamino = ObtenerAltura(alto);
                GenerarCaminoVertical(tablero, alto, colores, i, alturaCamino, anterior);
            }
        }
    }

    public void GenerarCaminoVertical(Casilla[] tablero, int alto, List<Color> colores, int columna, int alturaCamino, int alturaAnterior)
    {
        for (int j = Mathf.Min(alturaAnterior, alturaCamino); j <= Mathf.Max(alturaAnterior, alturaCamino); j++)
        {
            var celda = columna * alto + j;
            tablero[celda] = CrearCasilla((int)Efectos.permitido, colores);
        }
    }

    public Casilla CrearCasilla(int tipoCasilla, List<Color> posiblesMuestras)
    {
        var nuevaCasilla = new Casilla();

        nuevaCasilla.efecto = (Efectos)tipoCasilla;
        nuevaCasilla.colorMuestra = posiblesMuestras[tipoCasilla];
        return nuevaCasilla;
    }

    public int ObtenerAleatorio()
    {
        return Random.Range(0, System.Enum.GetValues(typeof(Efectos)).Length);
    }

    public int CentrarPosicion(int n, int total)
    {
        return n - (total / 2);
    }
}


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
        MuestraTablero(new GeneradorTablero().Generar(ancho, alto, coloresDefinidos, (altura) => Random.Range(0, alto)));
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

    private void DesactivaCollision(GameObject nuevaCasilla)
    {
        BoxCollider2D boxCollider2D = nuevaCasilla.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
}
