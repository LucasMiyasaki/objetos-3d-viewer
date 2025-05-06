using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Objetos3D.Classes
{
    public class Matriz4x4
    {
        private float[,] M = new float[4, 4];

        public Matriz4x4()
        {
            Identity();
        }

        public Matriz4x4(Matriz4x4 matriz)
        {
            this.M = matriz.M;
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
            float x = v.x * m.M[0, 0] + v.y * m.M[0, 1] + v.z * m.M[0, 2] + m.M[0, 3];
            float y = v.x * m.M[1, 0] + v.y * m.M[1, 1] + v.z * m.M[1, 2] + m.M[1, 3];
            float z = v.x * m.M[2, 0] + v.y * m.M[2, 1] + v.z * m.M[2, 2] + m.M[2, 3];
            float w = v.x * m.M[3, 0] + v.y * m.M[3, 1] + v.z * m.M[3, 2] + m.M[3, 3];

            if (w != 0)
            {
                x /= w;
                y /= w;
                z /= w;
            }

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

        public static Matriz4x4 ProjecaoPerspectiva(float d)
        {
            var m = new Matriz4x4();
            m.M[2, 3] = -1f / d;  // Perspectiva no eixo Z
            m.M[3, 3] = 0;
            return m;
        }

        public static Matriz4x4 ProjecaoCavaleira()
        {
            var m = new Matriz4x4();
            float angulo = (float)(45 * Math.PI / 180.0); // 45°
            m.M[0, 2] = (float)(-Math.Cos(angulo));
            m.M[1, 2] = (float)(-Math.Sin(angulo));
            return m;
        }

        public static Matriz4x4 ProjecaoCabinet()
        {
            var m = new Matriz4x4();
            float angulo = (float)(45 * Math.PI / 180.0); // 45°
            m.M[0, 2] = (float)(-0.5 * Math.Cos(angulo));
            m.M[1, 2] = (float)(-0.5 * Math.Sin(angulo));
            return m;
        }

    }
}
