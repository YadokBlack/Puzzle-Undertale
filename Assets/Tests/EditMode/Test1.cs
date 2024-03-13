using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Assert.IsTrue( num >= 0 && num < System.Enum.GetValues(typeof(Efectos)).Length);
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
        Casilla[] tablero = new GeneradorTablero().GenerarTablero(2, 3, new List<Color>() { Color.red, Color.yellow });
        Assert.AreEqual(6, tablero.Length);
    }

    [Test]
    public void GenerarTablero_Cero()
    {
        Casilla[] tablero = new GeneradorTablero().GenerarTablero(0, 0, new List<Color>() { Color.red, Color.yellow });
        Assert.AreEqual(0, tablero.Length);
    }

    [Test]
    public void GenerarCaminoEnTablero_CaminoPermitido()
    {
        GeneradorTablero generador = new GeneradorTablero();
        Casilla[] tablero = generador.GenerarTablero(1, 1, new List<Color>() { Color.red, Color.yellow });
        generador.GeneraCaminoEnTablero(tablero, 1, 1, new List<Color>() { Color.red, Color.yellow }, (altura) => 0);
        Assert.AreEqual(Efectos.permitido, tablero.Single().efecto);
    }

    [Test]
    public void GenerarCaminoEnTablero_CaminoPermitido2()
    {
        GeneradorTablero generador = new GeneradorTablero();
        Casilla[] tablero = generador.GenerarTablero(2, 2, new List<Color>() { Color.red, Color.yellow });
        generador.GeneraCaminoEnTablero(tablero, 2, 2, new List<Color>() { Color.red, Color.yellow }, (altura) => 0);
        Assert.AreEqual(Efectos.permitido, tablero[0].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[1].efecto);
        Assert.AreEqual(Efectos.permitido, tablero[2].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[3].efecto);
    }

    [Test]
    public void GenerarCaminoEnTablero_CaminoVertical()
    {
        GeneradorTablero generador = new GeneradorTablero();
        Casilla[] tablero = generador.GenerarTablero(2, 2, new List<Color>() { Color.red, Color.yellow });
        generador.GenerarCaminoVertical(tablero, 2, new List<Color>() { Color.red, Color.yellow }, 1, 0, 1);
        Assert.AreEqual(Efectos.bloqueado, tablero[0].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[1].efecto);
        Assert.AreEqual(Efectos.permitido, tablero[2].efecto);
        Assert.AreEqual(Efectos.permitido, tablero[3].efecto);
    }

    [Test]
    public void GenerarCaminoEnTablero_CaminoVertical2()
    {
        GeneradorTablero generador = new GeneradorTablero();
        Casilla[] tablero = generador.GenerarTablero(2, 2, new List<Color>() { Color.red, Color.yellow });
        generador.GenerarCaminoVertical(tablero, 2, new List<Color>() { Color.red, Color.yellow }, 1, 1, 1);
        Assert.AreEqual(Efectos.bloqueado, tablero[0].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[1].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[2].efecto);
        Assert.AreEqual(Efectos.permitido, tablero[3].efecto);
    }

    [Test]
    public void GenerarCaminoEnTablero_CaminoVertical3()
    {
        GeneradorTablero generador = new GeneradorTablero();
        Casilla[] tablero = generador.GenerarTablero(2, 2, new List<Color>() { Color.red, Color.yellow });
        generador.GenerarCaminoVertical(tablero, 2, new List<Color>() { Color.red, Color.yellow }, 0, 0, 1);
        Assert.AreEqual(Efectos.permitido, tablero[0].efecto);
        Assert.AreEqual(Efectos.permitido, tablero[1].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[2].efecto);
        Assert.AreEqual(Efectos.bloqueado, tablero[3].efecto);
    }
}
