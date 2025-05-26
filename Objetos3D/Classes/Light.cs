using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Objetos3D.Classes
{
    public class Light
    {
        public Vector3 Position { get; set; }
        public Color IntensityAmbient { get; set; }
        public Color IntensityDiffuse { get; set; }
        public Color IntensitySpecular { get; set; }

        public Light(Vector3 position, Color ia, Color id, Color ispec)
        {
            Position = position;
            IntensityAmbient = ia;
            IntensityDiffuse = id;
            IntensitySpecular = ispec;
        }

        // Construtor padrão para valores default
        public Light()
        {
            Position = new Vector3(100, 100, 100); // Posição padrão
            IntensityAmbient = Color.FromArgb(25, 25, 25); // Cinza escuro (0.1*255)
            IntensityDiffuse = Color.FromArgb(178, 178, 178); // Cinza claro (0.7*255)
            IntensitySpecular = Color.FromArgb(128, 128, 128); // Cinza médio (0.5*255)
        }

        // Métodos para converter Color para componentes float [0,1]
        public (float r, float g, float b) GetIaFloats() => (IntensityAmbient.R / 255f, IntensityAmbient.G / 255f, IntensityAmbient.B / 255f);
        public (float r, float g, float b) GetIdFloats() => (IntensityDiffuse.R / 255f, IntensityDiffuse.G / 255f, IntensityDiffuse.B / 255f);
        public (float r, float g, float b) GetIsFloats() => (IntensitySpecular.R / 255f, IntensitySpecular.G / 255f, IntensitySpecular.B / 255f);
    }
}
