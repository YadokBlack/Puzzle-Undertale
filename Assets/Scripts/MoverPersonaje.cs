using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidad = 4f;

    SpriteRenderer imagenPersonaje;  
    float limiteIzquierdo;
    float limiteDerecho;
    float limiteSuperior;
    float limiteInferior;

    void Start()
    {
        imagenPersonaje = GetComponent<SpriteRenderer>();
        Vector2 tamanyoImagenPersonaje = imagenPersonaje.bounds.size;
        CalcularLimitesPersonajePantalla(tamanyoImagenPersonaje);
    }

    private void CalcularLimitesPersonajePantalla(Vector2 sizePlayer)
    {
        limiteIzquierdo = ObtenerLimiteIzquierdo(sizePlayer);
        limiteDerecho = ObtenerLimiteDerecho(sizePlayer);
        limiteInferior = ObtenerLimiteInferior(sizePlayer);
        limiteSuperior = ObtenerLimiteSuperior(sizePlayer);
    }

    private float ObtenerLimiteSuperior(Vector2 sizePlayer)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - sizePlayer.y / 2f;
    }

    private float ObtenerLimiteInferior(Vector2 sizePlayer)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + sizePlayer.y / 2f;
    }

    private float ObtenerLimiteDerecho(Vector2 sizePlayer)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - sizePlayer.x / 2f;
    }

    private float ObtenerLimiteIzquierdo(Vector2 sizePlayer)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + sizePlayer.x / 2f;
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
