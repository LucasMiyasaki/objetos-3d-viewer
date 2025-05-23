using Objetos3D.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objetos3D
{
    public partial class Form2 : Form
    {
        Objeto3D objeto;
        public Form2(Objeto3D objeto)
        {
            InitializeComponent();
            this.objeto = objeto;

            if (objeto != null)
            {
                Objeto3D topo = new Objeto3D(objeto);
                Objeto3D lateral = new Objeto3D(objeto);
                Objeto3D cavaleira = new Objeto3D(objeto);
                Objeto3D cabinet = new Objeto3D(objeto);

                pbFrontal.Image = objeto.desenhaObjeto(pbFrontal.Width, pbFrontal.Height, false, false);

                topo.AcumularRotacao(0, 157);
                pbTopo.Image = topo.desenhaObjeto(pbTopo.Width, pbTopo.Height, false, false);

                lateral.AcumularRotacao(-157, 0);
                pbLateral.Image = lateral.desenhaObjeto(pbLateral.Width, pbLateral.Height, false, false);

                Matriz4x4 m = Matriz4x4.ProjecaoCavaleira();
                pbCavaleira.Image = cavaleira.desenhaObjeto(pbCavaleira.Width, pbCavaleira.Height, false, false, m);

                m = Matriz4x4.ProjecaoCabinet();
                pbCabinet.Image = cabinet.desenhaObjeto(pbCabinet.Width, pbCabinet.Height, false, false, m);

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Objeto3D perspectiva = new Objeto3D(objeto);

            Matriz4x4 m = Matriz4x4.ProjecaoPerspectiva((float)numericUpDown1.Value);
            pbPerspectiva.Image = perspectiva.desenhaObjeto(pbPerspectiva.Width, pbPerspectiva.Height, false, false, m);
        }
    }
}
