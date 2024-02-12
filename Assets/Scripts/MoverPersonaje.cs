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
        transform.position = CrearNuevaPosicionLimitada((float)Input.GetAxis("Horizontal"), (float)Input.GetAxis("Vertical"));
    }

    Vector3 CrearNuevaPosicionLimitada(float horizontal, float vertical)
    {
        return LimitarSalidaPantalla(new Vector3(horizontal, vertical, 0) * velocidad * Time.deltaTime);
    }

    Vector3 LimitarSalidaPantalla(Vector3 movimiento)
    {
        return new Vector3(Mathf.Clamp(transform.position.x + movimiento.x, limiteIzquierdo, limiteDerecho),
                           Mathf.Clamp(transform.position.y + movimiento.y, limiteInferior, limiteSuperior),
                                       transform.position.z);
    }
}
