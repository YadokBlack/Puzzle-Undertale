using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidad = 4f;

    SpriteRenderer imagenPersonaje;
    Vector2 tamanyoImagenPersonaje;    
    float limiteIzquierdo;
    float limiteDerecho;
    float limiteSuperior;
    float limiteInferior;

    void Start()
    {
        CalcularLimitesPantalla();
    }

    private void CalcularLimitesPantalla()
    {
        imagenPersonaje = GetComponent<SpriteRenderer>();
        tamanyoImagenPersonaje = imagenPersonaje.bounds.size;
        limiteIzquierdo = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + tamanyoImagenPersonaje.x / 2f;
        limiteDerecho = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - tamanyoImagenPersonaje.x / 2f;
        limiteInferior = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + tamanyoImagenPersonaje.y / 2f;
        limiteSuperior = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + -tamanyoImagenPersonaje.y / 2f;
    }

    void Update()
    {
        MuevePersonaje();
    }

    private void MuevePersonaje()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direccion = new Vector3(horizontal, vertical, 0);
        Vector3 movimiento = direccion * velocidad * Time.deltaTime;
        Vector3 nuevaPosicion = LimitarSalidaPantalla(movimiento);

        transform.position = nuevaPosicion;
    }

    Vector3 LimitarSalidaPantalla(Vector3 movimiento)
    {
        Vector3 posicionActual = transform.position;
        return new Vector3(Mathf.Clamp(posicionActual.x + movimiento.x, limiteIzquierdo, limiteDerecho),
                                            Mathf.Clamp(posicionActual.y + movimiento.y, limiteInferior, limiteSuperior),
                                            posicionActual.z);
    }
}
