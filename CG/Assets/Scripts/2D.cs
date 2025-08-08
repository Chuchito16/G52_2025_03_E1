using UnityEngine;
using System;

namespace packagePunto
{
    [Serializable]
    public class Punto2D
    {
        private double x;
        private double y;

        // Constructor vacío (por si Unity lo necesita)
        public Punto2D()
        {
        }

        // Constructor con parámetros
        public Punto2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Get y Set para X
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        // Get y Set para Y
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}

// Update is called once per frame


