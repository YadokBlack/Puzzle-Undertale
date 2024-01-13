using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidad = 4f;
    float limiteIzquierdo;
    float limiteDerecho;
    float limiteSuperior;
    float limiteInferior;

    void Start()
    {
        limiteIzquierdo = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        limiteDerecho   = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;        
        limiteInferior  = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y; ;
        limiteSuperior  = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 posicionActual =  transform.position;
        Vector3 direccion = new Vector3(horizontal, vertical, 0);
        Vector3 movimiento = direccion * velocidad * Time.deltaTime;

        Vector3 nuevaPosicion = new Vector3(Mathf.Clamp(posicionActual.x + movimiento.x, limiteIzquierdo, limiteDerecho),
                                            Mathf.Clamp(posicionActual.y + movimiento.y, limiteInferior, limiteSuperior),
                                            posicionActual.z);

        transform.position = nuevaPosicion;
    }
}
