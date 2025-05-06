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

            if(objeto != null )
            {
                Objeto3D topo = new Objeto3D(objeto);
                Objeto3D lateral = new Objeto3D(objeto);
                Objeto3D perspectiva = new Objeto3D(objeto);
                Objeto3D cavaleira = new Objeto3D(objeto);
                Objeto3D cabinet = new Objeto3D(objeto);

                pbFrontal.Image = objeto.desenhaObjeto(pbFrontal.Width, pbFrontal.Height, 0, 0);

                topo.AcumularRotacao(0, 157);
                pbTopo.Image = topo.desenhaObjeto(pbTopo.Width, pbTopo.Height, 0, 0);

                lateral.AcumularRotacao(-157, 0);
                pbLateral.Image = lateral.desenhaObjeto(pbLateral.Width, pbLateral.Height, 0, 0);

                perspectiva.AcumularEscala(1, 1, 1);
                Matriz4x4 m = Matriz4x4.ProjecaoPerspectiva(100);
                pbPerspectiva.Image = perspectiva.desenhaObjeto(pbPerspectiva.Width, pbPerspectiva.Height, 0, 0, m);

                cavaleira.AcumularEscala(1, 1, 1);
                m = Matriz4x4.ProjecaoCavaleira();
                pbCavaleira.Image = cavaleira.desenhaObjeto(pbCavaleira.Width, pbCavaleira.Height, 0, 0, m);

                cabinet.AcumularEscala(1, 1, 1);
                m = Matriz4x4.ProjecaoCabinet();
                pbCabinet.Image = cabinet.desenhaObjeto(pbCabinet.Width, pbCabinet.Height, 0, 0, m);

            }
        }
    }
}
