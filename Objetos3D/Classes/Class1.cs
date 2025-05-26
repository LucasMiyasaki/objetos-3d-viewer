using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos3D.Classes
{
    public class Material
    {
        public Color CoeffAmbient { get; set; }  // Ka
        public Color CoeffDiffuse { get; set; }  // Kd
        public Color CoeffSpecular { get; set; } // Ks
        public float Shininess { get; set; }     // n

        public Material(Color ka, Color kd, Color ks, float shininess)
        {
            CoeffAmbient = ka;
            CoeffDiffuse = kd;
            CoeffSpecular = ks;
            Shininess = shininess;
        }

        // Construtor padrão para valores default
        public Material()
        {
            CoeffAmbient = Color.FromArgb(255, 230, 230); // Ex: (1.0, 0.9, 0.9) [cite: 9]
            CoeffDiffuse = Color.FromArgb(128, 178, 128); // Ex: (0.5, 0.7, 0.5) [cite: 9]
            CoeffSpecular = Color.FromArgb(128, 128, 153); // Ex: (0.5, 0.5, 0.6) [cite: 9]
            Shininess = 10f; // Ex: 10 [cite: 9]
        }

        // Métodos para converter Color para componentes float [0,1]
        public (float r, float g, float b) GetKaFloats() => (CoeffAmbient.R / 255f, CoeffAmbient.G / 255f, CoeffAmbient.B / 255f);
        public (float r, float g, float b) GetKdFloats() => (CoeffDiffuse.R / 255f, CoeffDiffuse.G / 255f, CoeffDiffuse.B / 255f);
        public (float r, float g, float b) GetKsFloats() => (CoeffSpecular.R / 255f, CoeffSpecular.G / 255f, CoeffSpecular.B / 255f);
    }
}
