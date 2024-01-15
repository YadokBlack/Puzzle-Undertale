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

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void CrearNuevaCasilla(float x, float y, Color color)
    {
        GameObject nuevaCasilla = Instantiate(objetoCasilla, new Vector3(x, y, 0f), Quaternion.identity);
        SpriteRenderer spriteRenderer = nuevaCasilla.GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }
}
