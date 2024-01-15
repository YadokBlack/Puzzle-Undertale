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

    public class Casilla
    {
        public Colores color;
        public Efectos efecto;
        public Color colorMuestra;
    }

    public GameObject objetoCasilla;
    public int ancho = 14;
    public int alto = 8;

    void Start()
    {
        MostrarTablero();
    }

    void Update()
    {
        
    }

    public void MostrarTablero()
    {
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                CrearNuevaCasilla( i, j, Color.green);
            }
        }
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
