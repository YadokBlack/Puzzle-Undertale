using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
}
