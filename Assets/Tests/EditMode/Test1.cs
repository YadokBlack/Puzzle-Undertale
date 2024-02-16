using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Tablero;

public class Test1
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test1SimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(1, 1); 
    }

    [Test]
    public void CentrarPosicion()
    {
        int actual = Tablero.CentrarPosicion(5, 10);
        Assert.AreEqual(0, actual);
    }

    [Test]
    [Repeat (1000)]
    public void ObtenerAleatorio_RangoValores()
    {
        int num = Tablero.ObtenerAleatorio();
        Assert.IsTrue( num >= 0 && num < System.Enum.GetValues(typeof(Colores)).Length);
    }

    [Test]
    public void CrearCasilla_AsignarColorMuestra()
    {
        Casilla casilla = Tablero.CrearCasilla(0, new List<Color>() { Color.blue });
        Assert.AreEqual(Color.blue, casilla.colorMuestra);
    }

    [Test]
    public void CrearCasilla_ColorMuestra()
    {
        Casilla casilla = Tablero.CrearCasilla(2, new List<Color>() { Color.blue, Color.red, Color.green });
        Assert.AreEqual(Color.green, casilla.colorMuestra);
    }

    [Test]
    public void GenerarTablero_dimensiones()
    {
        Casilla[] tablero = Tablero.GenerarTablero(2, 3, new List<Color>() { Color.red, Color.yellow });
        Assert.AreEqual(6, tablero.Length);
    }
}
