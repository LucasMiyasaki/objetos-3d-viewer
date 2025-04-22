using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Objetos3D.Classes
{
    internal class Matriz4x4
    {
        private float[,] M = new float[4, 4];

        public Matriz4x4()
        {
            Identity();
        }

        public void Identity()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    M[i, j] = (i == j) ? 1f : 0f;
        }

        public static Matriz4x4 RotationX(float angle)
        {
            var m = new Matriz4x4();
            float c = (float)Math.Cos(angle);
            float s = (float)Math.Sin(angle);
            m.M[1, 1] = c;
            m.M[1, 2] = -s;
            m.M[2, 1] = s;
            m.M[2, 2] = c;
            return m;
        }

        public static Matriz4x4 RotationY(float angle)
        {
            var m = new Matriz4x4();
            float c = (float)Math.Cos(angle);
            float s = (float)Math.Sin(angle);
            m.M[0, 0] = c;
            m.M[0, 2] = s;
            m.M[2, 0] = -s;
            m.M[2, 2] = c;
            return m;
        }

        public static Matriz4x4 RotationZ(float angle)
        {
            var m = new Matriz4x4();
            float c = (float)Math.Cos(angle);
            float s = (float)Math.Sin(angle);
            m.M[0, 0] = c;
            m.M[0, 1] = -s;
            m.M[1, 0] = s;
            m.M[1, 1] = c;
            return m;
        }

        public static Matriz4x4 Escala(float sx, float sy, float sz)
        {
            var m = new Matriz4x4();
            m.M[0, 0] = sx;
            m.M[1, 1] = sy;
            m.M[2, 2] = sz;
            return m;
        }

        public static (float x, float y, float z) Transform((float x, float y, float z) v, Matriz4x4 m)
        {
            float x = v.x * m.M[0, 0] + v.y * m.M[0, 1] + v.z * m.M[0, 2];
            float y = v.x * m.M[1, 0] + v.y * m.M[1, 1] + v.z * m.M[1, 2];
            float z = v.x * m.M[2, 0] + v.y * m.M[2, 1] + v.z * m.M[2, 2];
            return (x, y, z);
        }

        public static Matriz4x4 operator *(Matriz4x4 a, Matriz4x4 b)
        {
            var r = new Matriz4x4();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    r.M[i, j] = 0;
                    for (int k = 0; k < 4; k++)
                        r.M[i, j] += a.M[i, k] * b.M[k, j];
                }
            return r;
        }
    }
}
