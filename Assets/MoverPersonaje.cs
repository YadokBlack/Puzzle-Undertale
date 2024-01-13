using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidad = 4f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 posicionActual =  transform.position;
        Vector3 direccion = new Vector3(horizontal, vertical, 0);
        Vector3 movimiento = direccion * velocidad * Time.deltaTime;

        Vector3 nuevaPosicion = new Vector3(posicionActual.x + movimiento.x,
                                            posicionActual.y + movimiento.y,
                                            posicionActual.z);

        transform.position = nuevaPosicion;
    }
}