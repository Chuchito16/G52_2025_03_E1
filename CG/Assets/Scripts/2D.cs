using System;
using UnityEngine;

namespace packageGeometria
{
    [Serializable]
    public class Punto2D
    {
        [SerializeField] private double x;
        [SerializeField] private double y;

        public Punto2D() { }

        public Punto2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
    }
}