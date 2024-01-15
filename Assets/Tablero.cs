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
        casillasTablero = new Casilla[ancho * alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                numero = i * alto + j;
                GeneraCasillaAleatoria(numero);
                CrearNuevaCasilla(i, j, casillasTablero[numero].colorMuestra);
            }
        }
    }

    private void GeneraCasillaAleatoria(int num)
    {
        int numAleatorio = ObtenerAleatorio();
        casillasTablero[num] = new Casilla();
        casillasTablero[num].color = (Colores)numAleatorio;
        casillasTablero[num].efecto = (Efectos)numAleatorio;
        casillasTablero[num].colorMuestra = coloresDefinidos[numAleatorio];
    }

    private int ObtenerAleatorio()
    {
        return Random.Range(0, System.Enum.GetValues(typeof(Colores)).Length);
    }

    private int CentrarPosicion(int n, int total)
    {
        return n - (total / 2);
    }

    public void CrearNuevaCasilla(int x, int y, Color color)
    {
        GameObject nuevaCasilla = Instantiate(objetoCasilla, new Vector3(CentrarPosicion(x, ancho), CentrarPosicion(y, alto), 0f), Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCasilla.GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
}
