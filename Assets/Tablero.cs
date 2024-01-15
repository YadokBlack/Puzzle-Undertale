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
    public int ancho = 12;
    public int alto = 8;

    void Start()
    {
        CrearTablero();
    }

    void Update()
    {
        
    }

    public void CrearTablero()
    {
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                CrearNuevaCasilla(i, j, Color.green);
            }
        }
    }

    public void CrearNuevaCasilla(float x, float y, Color color)
    {
        GameObject nuevaCasilla = Instantiate(objetoCasilla, new Vector3(x, y, 0f), Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCasilla.GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
}
