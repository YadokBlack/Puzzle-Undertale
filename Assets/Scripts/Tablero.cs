using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tablero : MonoBehaviour
{
    public enum Colores
    {
        Rosa,
        Roja
    }

    public enum Efectos
    {
        pasa,
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
        GenerarTablero();
    }

    public void GenerarTablero()
    {
        int numero;
        int numeroCamino = Random.Range(0, alto);
        casillasTablero = new Casilla[ancho * alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                numero = i * alto + j;
                if (numeroCamino != j) 
                {
                    GeneraCasillaAleatoria(numero);                    
                }
                else
                {
                    GeneraCasillaLibre(numero);
                }
                CrearNuevaCasilla(i, j, numero);
            }
        }
    }

    private void GeneraCasillaLibre(int num)
    {
        casillasTablero[num] = CrearCasilla(0, coloresDefinidos);
    }

    private void GeneraCasillaAleatoria(int num)
    {
        int numAleatorio = ObtenerAleatorio();

        casillasTablero[num] = CrearCasilla(numAleatorio, coloresDefinidos);
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

    public void CrearNuevaCasilla(int x, int y, int num)
    {
        GameObject nuevaCasilla = Instantiate(objetoCasilla, new Vector3(CentrarPosicion(x, ancho), CentrarPosicion(y, alto), 0f), Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCasilla.GetComponent<SpriteRenderer>();
        spriteRenderer.color = casillasTablero[num].colorMuestra;

        if (CasillaEsPasa(num))
        {
            DesactivaCollision(nuevaCasilla);
        }
    }

    private bool CasillaEsPasa(int num)
    {
        return casillasTablero[num].efecto == Efectos.pasa;
    }

    private static void DesactivaCollision(GameObject nuevaCasilla)
    {
        BoxCollider2D boxCollider2D = nuevaCasilla.GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }
}
