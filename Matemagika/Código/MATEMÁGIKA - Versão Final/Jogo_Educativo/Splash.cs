using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_Educativo
{
    public partial class Splash : Form
    {
        private int tempo = 0;
        private bool pronto = false;
        private byte qual = 5;

        private Principal princ;

        public Splash(Principal p)
        {
            InitializeComponent();

            this.princ = p;            

            //iniciar conexao
            this.princ.iniciarConexao();

            //personagens
            this.princ.pegarPersonagensNoBD();

            this.pic.Image = this.princ.getPersonagem(5, 1);
            this.tmrTempo.Enabled = true;

            //quantos quizes por leveis
            this.princ.quantosQuizesPorLv();

            //muda daqui para baixo
            this.princ.mostrarLogin();

            if (this.princ.podeClose)
                this.Close();
            else
                this.pronto = true;

            Application.DoEvents();
        }

        private void tmrTempo_Tick(object sender, EventArgs e)
        {
            if (this.pic.Location.X >= this.Width)
                this.pic.Location = new Point(-20, this.pic.Location.Y);
            else
                this.pic.Location = new Point(this.pic.Location.X + 7, this.pic.Location.Y);

            this.Refresh();
            this.pic.Refresh();

            this.tempo += this.tmrTempo.Interval;

            if (this.pronto && this.princ.podeClose && this.tempo >= 140)
                this.Close();

            this.Invalidate();
            this.Update();
            this.Refresh();
            Application.DoEvents();
        }

        public void fechar()
        {
            this.Close();
        }

        private void Splash_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.qual == 6)
                this.qual = 1;
            else
                this.qual++;

            this.pic.Image = this.princ.getPersonagem(this.qual, 1);
            Application.DoEvents();
        }
    }
}
