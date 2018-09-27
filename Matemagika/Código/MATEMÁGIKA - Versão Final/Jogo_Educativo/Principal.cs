/*atencao esta é a versao mais nova, o multiplayer provavelmente contém falhas inusitadas*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//para deixar as bordas dos botoes circulares
using System.Drawing.Drawing2D;
//para conseguir o endereco do arquivo executavel
using System.IO;
//para usar o banco
using System.Data.SqlClient;
//ip
using System.Net;
using System.Net.Sockets;
//fonte personalizada
using System.Drawing.Text;
using System.Runtime.InteropServices;
//musica
using System.Media;

namespace Jogo_Educativo
{
    public partial class Principal : Form
    {
        //variaveis gerais
        protected byte etapa = 0;
        //0: nao esta no jogo (login, cadastro,...)
        //1: home (btn: leveis, personagens, explicacao do jogo, logout)
        //2: leveis
        //3: explicacao
        //4: jogo
        //5: personagens
        //6: explicacao jogo
        //7: creditos das imagens
        //8: ranking
        //9: jogar com amigos
        protected bool antesPersEhMenu = true;
        protected string nomeUsuario = "Anonymous";
        protected bool ehAnonimo = false;
        protected int xp;
        protected int moedas;

        //banco
        protected string connStr = Jogo_Educativo.Properties.Settings.Default.BDPRII17188ConnectionString1;
        protected SqlConnection con;

        //leveis
        protected const int yBtnLvInicial = 95;
        protected const int xBtnLvE = 80;
        protected const int pxlsXEntreBtnLv = 70;
        protected const int pxlsYEntreBtnLv = 30;
        protected byte qtsLv;
        protected string[] leveis;
        protected byte[] estrelas;
        protected byte[] qtdQuizesPorLv;

        //explicacao
        protected string[] contextualizacao;
        protected int qlContext;
        protected string[] explicacao;
        protected int qlExplic;
        protected const int qtdCaract = 34;
        protected const int xPersPararExp = 185;
        protected int qtdCaracAtual = 0;
        //explicacao do jogo
        protected bool[] poderPulandoRevivendoQuiz;
        protected bool jahViuExplicacao;


        //variaveis gerais do jogo
        protected const int qtdVidas = 3;
        protected const int qtdQuizesErradosPerde = 2;
        protected bool jogando = false;
        protected bool morreu = false;
        protected int levelAtual = 1;
        protected byte personagem = 0; //com qual personagem ele está
        protected int vidas = qtdVidas;
        protected int qtdQuizesErrados = 0;

        //xp
        protected const int qtdXpTemOPrimeiroNivel = 700;
        protected const int qtdXpSomaACadaNivel = 500;
        protected const int qntXpGanhaPorLevel = 250;
        protected const int qntXpGanhaPorQuiz = 100;
        protected const int qntXpGanhaPorQuizNormal = 25;

        //obstaculos, quizBox e inimigos
        protected const int pxlsLadoBateu = 15;
        protected int qtdObst;
        protected int qtdInim;
        protected byte[,] dirQlInim;
        protected int qlQuizBox;
        //0:morreu, 1:direita, 2:esquerda

        //andando
        protected const int qtdAndar = 30;
        protected const int qtdAndarInimigos = 10;
        protected bool clicando = false;
        protected bool viradoDireita = true;
        protected int pos = 0;
        protected int posFinal;
        protected const int pxlsPosFinal = 350;
        protected Image[,] personagens;

        //pulando
        protected const int qtdPular = 15;
        protected const int quantoSubirPorVez = 75;
        protected int quantoSubir = quantoSubirPorVez;
        protected bool pulou2Vez = false;
        protected int quantoSubiu = 0;
        protected const int chao = 370;
        protected bool subindo = true;

        //revivendo
        protected const int tempoDeReviver = 5000; //em milisegundos
        protected int tempoRevivendo = 0;

        //quiz
        //variaveis de controle
        protected bool fazendoQuiz = false;
        protected byte tipoQuiz = 0;
        //0: normal, 1: especial durante o level, 2: especial no final do level
        protected int opcaoCerta;
        //poder
        protected const int qtdPixelsPoderX = 250;
        protected const int qtdAndaPoder = 40;
        protected int qtdPoderAtual = 0;
        protected bool viradoDireitaPoder;
        //tempo
        protected const int tempoQuiz = 5000;
        protected int qntTempoResta = tempoQuiz;
        protected const int qtsVezesMaisLento = 7;
        //$$
        protected const int qtdMoedasQuizBox = 300;
        protected const int qtdMoedasQuizFinal = 150;
        //frases
        protected const string fraseQuizesErrados = "Quizes Errados: ";
        protected const string fraseTempoRestante = "Tempo: ";

        //personagens
        protected String[] nomePersonagens;
        protected int[] precoPersonagens;

        //usuarios disponiveis para jogar contra
        //usuarios disponiveis para jogar contra
        protected bool jogMultiplayer;
        protected string[,] nomesIpUsDispon;
        protected int[] lvsUsDispon;
        protected int[] portasUsDispon;
        protected IPAddress ipAddress;
        protected IPEndPoint ipEnd_servidor;
        protected Socket sock_Servidor;
        protected int porta; //soh será usada no multiplayer se outro usuario quiser jogar com ele
        protected int contRepeticao = 0;
        public Socket m_clientSocket; //será usado caso o jogador queira jogar com outro disponivel
        private Socket m_mainSocket; // será usado para criar um servidor
        private Socket[] m_workerSocket = new Socket[10];
        private int m_clientCount = 0; // indica quantos clientes estao conectados
        public AsyncCallback pfnWorkerCallBack;
        const int MAX_CLIENTS = 2;


        //fonte personalizada
        [DllImport("gdi32.dll")]
        protected static extern IntPtr AddFontMemResourceEx(IntPtr pbfont, uint cbfont,
                              IntPtr pdv, [In] ref uint pcFonts);

        protected FontFamily ff;
        protected Font font;

        //aux
        protected int aux = 0;

        //splash
        Splash splash;
        public bool podeClose = false;

        //thread
        protected Thread t;

        //musica
        protected SoundPlayer music;

        public Principal()
        {
            InitializeComponent();

            this.picPoder.Width = qtdAndaPoder;

            splash = new Splash(this);
            splash.FormClosed += (s, agr) => this.Show();
            splash.Show();
            this.Hide();

            splash.Invalidate();
            splash.Update();
            splash.Refresh();
            this.podeClose = true;

            this.pegarIp();

            //Thread t = new Thread(this.aceitarConexoes);
            this.t = new Thread(this.aceitarConexoes);

            Application.DoEvents();
        }


        //socket e ip
        protected void pegarIp()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            this.ipAddress = ipHostInfo.AddressList.LastOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            this.porta = new Random().Next(2003, 47808);

            /*ipEnd_servidor = new IPEndPoint(this.ipAddress, this.porta);

            sock_Servidor = new Socket(AddressFamily.InterNetwork,
                                                            SocketType.Stream,
                                                            ProtocolType.IP);
            sock_Servidor.Bind(ipEnd_servidor);*/
        }

        protected void aceitarConexoes()
        {
            //Task.Run(() => sock_Servidor.Accept());

            while (true)
            {
                //se ele nao estiver jogando
                if (this.etapa != 3 && this.etapa != 4 && this.etapa != 6)
                {
                    //aceitar requisicoes

                    //sock_Servidor.Listen(10);
                    //Socket handler;
                    m_mainSocket.Listen(2);
                    try
                    {
                        for (contRepeticao = contRepeticao; contRepeticao <= 6; contRepeticao++)
                            m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
                    }
                    catch (SocketException se)
                    {
                        MessageBox.Show(se.Message);
                        return;
                    }
                }
            }
        }

        protected void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                // aqui nos completamos/finalizamos o BeginAcceps assyncronos call
                // chamando EndAccept() - o qual retorna um referencia
                // para um novo socket
                m_workerSocket[m_clientCount] = m_mainSocket.EndAccept(asyn);
                // agora deixaremos o worker socket fazer o processamento futuro
                // do cliente recem conectado
                WaitForData(m_workerSocket[m_clientCount]);
                // incrementa o contador de clientes
                ++m_clientCount;
                // mostra um mensagem indicando que foi conectado

                // Desde que o Socket principal esta livre para tratar novas conexões
                // vamos voltar a tentar tratar essas conexões
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1",
                    "\n OnClientConnect: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        protected void WaitForData(System.Net.Sockets.Socket soc)
        {
           // while (true)
            //{
                try
                {
                    if (pfnWorkerCallBack == null)
                    {
                        // Especifica a callback a ser chamada quando qualquer
                        // atividade de escrita for detecta no socket cliente conectado
                        pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                    }
                    SocketPacket theSocPkt = new SocketPacket();
                    theSocPkt.m_currentSocket = soc;
                    // inicializa a recepção de dados
                    soc.BeginReceive(theSocPkt.databuffer, 0,
                                                   theSocPkt.databuffer.Length,
                                                   SocketFlags.None,
                                                   pfnWorkerCallBack, theSocPkt);

                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.Message);
                }
            //}

        }

        public class SocketPacket
        {
            public System.Net.Sockets.Socket m_currentSocket;
            public byte[] databuffer = new byte[256];
        }

        protected void ex(object sender, EventArgs e)
        {
            this.desmostrarUsuariosDispon();
            this.etapa = 9;
            this.iniciarLevel();
            Application.DoEvents();
        }

        protected void criarServidor()
        {
            if (m_mainSocket == null)
            {
                try
                {
                    string portStr = this.porta.ToString();
                    int port = System.Convert.ToInt32(portStr);
                    // cria o Socket para ficar escutando 
                    m_mainSocket = new Socket(AddressFamily.InterNetwork,
                                                                    SocketType.Stream,
                                                                    ProtocolType.Tcp);

                    IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
                    //MessageBox.Show(ipAddress.ToString() + " " + port.ToString()); TESTE
                    // bind to local IP Address
                    m_mainSocket.Bind(ipLocal);
                    // start listening
                    m_mainSocket.Listen(4);

                    m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.Message);

                }
            }
        }

        protected void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int iRx = 0;
                // A execução completa de BeginReceive() chamada assincronamente
                // por EndReceive() retorna o numero de caracteres escritos no 
                //stream pelo cliente
                iRx = socketData.m_currentSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                int charlen = d.GetChars(socketData.databuffer, 0, iRx, chars, 0);

                System.String szData = new System.String(chars);

                if (szData.Length > 2)
                {
                    if (szData.Substring(0, 3) == "req") //mensagem que está chegando é "r\0"
                    {
                        string nomeOponente = szData.Substring(3);

                        if (nomeOponente.Length > 10)
                            nomeOponente = nomeOponente.Substring(0, 7) + "...";

                        //DialogResult dialogResult = MessageBox.Show("Você deseja jogar contra " + msg + "?", "Multiplayer", MessageBoxButtons.YesNo);
                        DialogResult dialogResult = MessageBox.Show("Você foi desafiado! Quer aceitar o desafio? \n Por: " + nomeOponente, "Multiplayer", MessageBoxButtons.YesNo);

                        //enviar a mensagem de resposta
                        if (dialogResult == DialogResult.Yes)
                        {
                            //se conectar a este cliente                        
                            string cmd_s = "Select ip, porta from usDisponJogarContra where nome=@nome";
                            SqlCommand cmd = new SqlCommand(cmd_s, con);
                            cmd.Parameters.AddWithValue("@nome", szData.Substring(3));

                            //con.Open();
                            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();

                            adapt.Fill(ds);

                            string IPBD = "";
                            string PORTABD = "";
                            if (ds.Tables[0].Rows.Count == 1)
                            {
                                DataRow dr = ds.Tables[0].Rows[0];
                                //MessageBox.Show(dr.ItemArray[0].ToString() + dr.ItemArray[1].ToString()); //para testar                               
                                IPBD = dr.ItemArray[0].ToString();
                                PORTABD = dr.ItemArray[1].ToString();

                            }
                            else
                            {
                                MessageBox.Show("Erro, por favor, tente novamente!");
                                return;
                            }


                            m_clientSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp);
                            IPAddress ip = IPAddress.Parse(IPBD);
                            int iPortNo = System.Convert.ToInt32(PORTABD);

                            IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);

                            m_clientSocket.Connect(ipEnd);

                            int lv = new Random().Next(1, this.leveis.Length + 1);
                            SendData("sim" + lv.ToString());
                            this.pegarLeveisNoBD();

                            // this.desmostrarBtnUsuariosDispon();
                            this.jogMultiplayer = true;
                            //enviar "S" + lv aleatorio
                            //fazer              
                            this.Invoke(new EventHandler(this.ex));
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            SendData("nao");
                            if (m_clientSocket != null)
                            {
                                m_clientSocket.Close();
                                m_clientSocket = null;
                            }
                        }
                    }
                    else

                if (szData.Substring(0, 3) == "sim")
                    {
                        //MessageBox.Show("aceitou");
                        this.jogMultiplayer = true;
                        int lv = Convert.ToInt32(szData.Substring(3));
                        this.pegarLeveisNoBD();
                        this.desmostrarBtnUsuariosDispon();
                        this.Invoke(new EventHandler(this.ex));
                        //if (picLoading.Visible == true)
                          //  picLoading.Visible = false;
                    }
                    else

                if (szData.Substring(0, 3) == "nao")
                    {
                        MessageBox.Show("Seu amigo não aceitou jogar contra você!");
                        this.btnVoltar.Visible = true;
                        CloseSockets();
                    }
                    else

                if (szData.Substring(0, 6) == "acabou")
                    {
                        //parar jogo
                        this.tmrCarregarPoder.Enabled = false;
                        this.tmrInimigos.Enabled = false;
                        /*this.btnVoltar.Visible = false;
                        this.picBtnVoltar.Visible = false;
                        this.btnHome.Visible = false;
                        this.picBtnHome.Visible = false;
                        this.prgPoder.Visible = false;
                        this.lbPoder.Visible = false;*/
                        this.jogMultiplayer = false;
                        this.Invoke(new EventHandler(this.Acabou));


                        MessageBox.Show("VOCÊ PERDEU! Seu amigo terminou mais rápido que você! Treine mais para vencê-lo da próxima vez!");

                        EventHandler e1 = (sender, args) => this.tirarJogoDaTela();
                        this.Invoke(e1);
                        EventHandler e2 = (sender, args) => this.mostrarUsuariosDispon();
                        this.Invoke(e2);
                        m_clientSocket.Close();
                        t.Abort();

                        Application.DoEvents();
                        //tem que mudar alguma coisa aqui no socket? (tipo a porta)
                        //fazer
                    }
                    /*else
                        if (szData == "r\0")
                        MessageBox.Show("erro");
                    */
                    WaitForData(socketData.m_currentSocket);
                }
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1",
                    "\n OnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }


        }

        protected void SendData(string dados)
        {
            try
            {
                Object objData = dados;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());

                if (m_clientSocket != null)
                {
                    m_clientSocket.Send(byData);
                    return;
                }

                for (int i = 0; i < m_clientCount; i++)
                {
                    if (m_workerSocket[i] != null)
                    {
                        if (m_workerSocket[i].Connected)
                        {
                            m_workerSocket[i].Send(byData);
                        }
                    }
                }

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        protected void CloseSockets()
        {
            if (m_mainSocket != null)
            {
                m_mainSocket.Close();
            }
            for (int i = 0; i < m_clientCount; i++)
            {
                if (m_workerSocket[i] != null)
                {
                    m_workerSocket[i].Close();
                    m_workerSocket[i] = null;
                }
            }
        }

        //para invoke
        protected void Acabou(object sender, EventArgs args)
        {
            this.btnVoltar.Visible = false;
            this.btnVoltar.Enabled = true;
            this.picBtnVoltar.Visible = false;
            this.btnHome.Visible = false;
            this.picBtnHome.Visible = false;
            this.prgPoder.Visible = false;
            this.lbPoder.Visible = false;
        }

        //fonte personalizada

        private void loadFont()
        {
            byte[] fontArray = Jogo_Educativo.Properties.Resources._8_BIT_WONDER;
            int dataLength = Jogo_Educativo.Properties.Resources._8_BIT_WONDER.Length;

            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);

            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            uint cFonts = 0;

            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();

            pfc.AddMemoryFont(ptrData, dataLength);

            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }

        private void AllocFont(Font f, Control c, float size)
        {
            FontStyle fontstyle = FontStyle.Regular;

            c.Font = new Font(ff, size, fontstyle);
        }


        //leveis
        protected void mostrarBtnLeveis()
        {
            this.etapa = 2;

            this.pegarLeveisNoBD();

            this.btnLeveis = new Jogo_Educativo.RoundedPictureBox[this.qtsLv];
            this.lbLeveis = new System.Windows.Forms.Label[this.qtsLv];
            this.picEstrelas = new System.Windows.Forms.PictureBox[this.qtsLv, 3];

            this.lbTitulo.Location = new Point(270, 5);
            this.lbTitulo.Text = "LEVELS";
            this.lbTitulo.Visible = true;

            int qtdHeight = 0;
            for (int i = 0; i < this.qtsLv; i++)
            {
                // "botao"
                this.btnLeveis[i] = new Jogo_Educativo.RoundedPictureBox();
                if (this.estrelas[i] == 0 && i > 0 && this.estrelas[i - 1] == 0)
                {
                    this.btnLeveis[i].Enabled = false;
                    this.btnLeveis[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
                }
                else
                {
                    this.btnLeveis[i].Enabled = true;

                    if (this.estrelas[i] == 0)
                        this.btnLeveis[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));
                    else
                        this.btnLeveis[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(197)))), ((int)(((byte)(3)))));
                }

                this.btnLeveis[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                this.btnLeveis[i].Cursor = System.Windows.Forms.Cursors.Hand;
                this.btnLeveis[i].Size = new System.Drawing.Size(196, 70);

                int x;
                if (i % 4 != 0)
                    x = this.btnLeveis[i - 1].Location.X;
                else
                {
                    if (i == 0)
                        x = xBtnLvE;
                    else
                    {
                        int quo = i / 4;
                        x = this.btnLeveis[i - 1].Location.X + this.btnLeveis[i - 1].Width + pxlsXEntreBtnLv;
                        qtdHeight = 0;
                    }
                }

                this.btnLeveis[i].Location = new System.Drawing.Point(x, yBtnLvInicial + qtdHeight);
                this.btnLeveis[i].TabIndex = 26 + i;
                this.btnLeveis[i].Tag = i;

                this.btnLeveis[i].Invalidate();
                this.btnLeveis[i].Update();
                this.btnLeveis[i].Refresh();
                this.Controls.Add(this.btnLeveis[i]);

                this.btnLeveis[i].Click += new System.EventHandler(this.btnLeveis_Click);
                this.btnLeveis[i].MouseEnter += new System.EventHandler(this.btnLeveis_MouseEnter);
                this.btnLeveis[i].MouseLeave += new System.EventHandler(this.btnLeveis_MouseLeave);

                if (this.estrelas[i] == 0)
                    this.btnLeveis[i].Focus();

                qtdHeight += this.btnLeveis[i].Height + pxlsYEntreBtnLv;

                // label
                this.lbLeveis[i] = new System.Windows.Forms.Label();
                this.lbLeveis[i].AutoSize = true;
                if (this.estrelas[i] == 0 && i > 0 && this.estrelas[i - 1] == 0)
                {
                    this.lbLeveis[i].Enabled = false;
                    this.lbLeveis[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
                }
                else
                {
                    this.lbLeveis[i].Enabled = true;

                    if (this.estrelas[i] == 0)
                        this.lbLeveis[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));
                    else
                        this.lbLeveis[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(197)))), ((int)(((byte)(3)))));
                }
                this.lbLeveis[i].Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lbLeveis[i].ForeColor = System.Drawing.SystemColors.HighlightText;
                this.lbLeveis[i].Location = new System.Drawing.Point(this.btnLeveis[i].Location.X + 9, this.btnLeveis[i].Location.Y + 11);
                this.lbLeveis[i].Size = new System.Drawing.Size(122, 19);
                this.lbLeveis[i].Cursor = System.Windows.Forms.Cursors.Hand;
                this.lbLeveis[i].Text = "Lv" + Convert.ToString(i + 1) + ": " + this.leveis[i];
                this.lbLeveis[i].Tag = i;

                this.lbLeveis[i].Visible = true;

                this.lbLeveis[i].Invalidate();
                this.lbLeveis[i].Update();
                this.lbLeveis[i].Refresh();
                this.Controls.Add(this.lbLeveis[i]);

                this.btnLeveis[i].SendToBack();
                this.lbLeveis[i].BringToFront();

                this.lbLeveis[i].Click += new System.EventHandler(this.btnLeveis_Click);
                this.lbLeveis[i].MouseEnter += new System.EventHandler(this.btnLeveis_MouseEnter);
                this.lbLeveis[i].MouseLeave += new System.EventHandler(this.btnLeveis_MouseLeave);

                int ultimo;
                if (this.estrelas[i] == 4)
                    ultimo = 3;
                else
                    ultimo = this.estrelas[i];

                for (int ipic = 0; ipic < ultimo; ipic++)
                {
                    // pictureBox
                    this.picEstrelas[i, ipic] = new System.Windows.Forms.PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(this.picEstrelas[i, ipic])).BeginInit();
                    if (this.estrelas[i] == 0 && i > 0 && this.estrelas[i - 1] == 0)
                    {
                        this.picEstrelas[i, ipic].Enabled = false;
                        this.picEstrelas[i, ipic].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
                    }
                    else
                    {
                        this.picEstrelas[i, ipic].Enabled = true;

                        if (this.estrelas[i] == 0)
                            this.picEstrelas[i, ipic].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));
                        else
                            this.picEstrelas[i, ipic].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(197)))), ((int)(((byte)(3)))));
                    }

                    if (this.estrelas[i] == 4)
                        this.picEstrelas[i, ipic].Image = global::Jogo_Educativo.Properties.Resources.gold_star;
                    else
                        this.picEstrelas[i, ipic].Image = global::Jogo_Educativo.Properties.Resources.star_not_gold;

                    if (ipic == 0)
                        this.picEstrelas[i, ipic].Location = new System.Drawing.Point(this.btnLeveis[i].Location.X + 117, this.btnLeveis[i].Location.Y + 40);
                    else
                        this.picEstrelas[i, ipic].Location = new System.Drawing.Point(this.picEstrelas[i, ipic - 1].Location.X + 20, this.btnLeveis[i].Location.Y + 40);

                    this.picEstrelas[i, ipic].Size = new System.Drawing.Size(16, 16);
                    this.picEstrelas[i, ipic].TabStop = false;
                    this.picEstrelas[i, ipic].Cursor = System.Windows.Forms.Cursors.Hand;

                    this.picEstrelas[i, ipic].Tag = i;
                    this.picEstrelas[i, ipic].Visible = true;
                    this.picEstrelas[i, ipic].BringToFront();

                    this.picEstrelas[i, ipic].Invalidate();
                    this.picEstrelas[i, ipic].Update();
                    this.picEstrelas[i, ipic].Refresh();
                    this.Controls.Add(this.picEstrelas[i, ipic]);

                    this.picEstrelas[i, ipic].BringToFront();

                    this.picEstrelas[i, ipic].Click += new System.EventHandler(this.btnLeveis_Click);
                    this.picEstrelas[i, ipic].MouseEnter += new System.EventHandler(this.btnLeveis_MouseEnter);
                    this.picEstrelas[i, ipic].MouseLeave += new System.EventHandler(this.btnLeveis_MouseLeave);
                }
            }

            this.btnMostrarPersonagens2.Visible = true;
            if (this.ipAddress != null)
                this.btnJogarContraAmigos.Visible = true;
            this.btnVoltar.Location = new Point(18, 18);
            this.btnVoltar.Enabled = true;
            this.btnVoltar.Visible = true;
        }

        protected int tagLevel(object sender)
        {
            if (sender.GetType() == typeof(RoundedPictureBox))
                return Convert.ToInt16(((RoundedPictureBox)sender).Tag);
            else
                if (sender.GetType() == typeof(Label))
                return Convert.ToInt16(((Label)sender).Tag);
            else
                return Convert.ToInt16(((PictureBox)sender).Tag);
        }

        protected void btnLeveis_Click(object sender, EventArgs e)
        {
            this.desmostrarLeveis();

            this.levelAtual = this.tagLevel(sender) + 1;

            this.comecarExplicacao();
        }

        protected void btnLeveis_MouseEnter(object sender, EventArgs e)
        {
            int tag = this.tagLevel(sender);

            Color cor;
            if (this.estrelas[tag] == 0)
                cor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            else
                cor = System.Drawing.Color.Lime;

            this.btnLeveis[tag].BackColor = cor;
            this.lbLeveis[tag].BackColor = cor;

            int ultimo;
            if (this.estrelas[tag] == 4)
                ultimo = 3;
            else
                ultimo = this.estrelas[tag];
            for (int i = 0; i < ultimo; i++)
                this.picEstrelas[tag, i].BackColor = cor;
        }

        protected void btnLeveis_MouseLeave(object sender, EventArgs e)
        {
            int tag = this.tagLevel(sender);

            Color cor;
            if (this.estrelas[tag] == 0)
                cor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));
            else
                cor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(197)))), ((int)(((byte)(3)))));

            this.btnLeveis[tag].BackColor = cor;
            this.lbLeveis[tag].BackColor = cor;

            int ultimo;
            if (this.estrelas[tag] == 4)
                ultimo = 3;
            else
                ultimo = this.estrelas[tag];
            for (int i = 0; i < ultimo; i++)
                this.picEstrelas[tag, i].BackColor = cor;
        }

        protected void desmostrarLeveis()
        {
            if (this.etapa != 6)
                this.btnVoltar.Visible = false;

            this.etapa = 0;
            this.btnMostrarPersonagens2.Visible = false;
            this.btnJogarContraAmigos.Visible = false;

            this.lbTitulo.Visible = false;
            for (int i = 0; i < this.qtsLv; i++)
            {
                this.btnLeveis[i].Visible = false;
                this.lbLeveis[i].Visible = false;

                int ultimo;
                if (this.estrelas[i] == 4)
                    ultimo = 3;
                else
                    ultimo = this.estrelas[i];
                for (int ipic = 0; ipic < ultimo; ipic++)
                    this.picEstrelas[i, ipic].Visible = false;
            }
        }

        private void btnJogarContraAmigos_Click(object sender, EventArgs e)
        {
            this.colocarUsOnlineNoBD(true);
            this.usuariosDisponiveis();
            this.desmostrarLeveis();
            this.mostrarUsuariosDispon();
            this.criarServidor();
            Application.DoEvents();
        }

        protected void mostrarUsuariosDispon()
        {
            this.etapa = 9;

            this.lbTitulo.Location = new Point(120, this.lbTitulo.Location.Y);
            this.lbTitulo.Text = "MULTIPLAYER";
            this.lbTitulo.Visible = true;

            this.mostrarBtnUsuariosDispon();

            this.btnVoltar.Visible = true;
            this.btnRefresh.Visible = true;

            this.aux = 10;
            this.tmrAux.Interval = 10000;
            this.tmrAux.Enabled = true;
        }

        protected void mostrarBtnUsuariosDispon()
        {
            if (this.lvsUsDispon.Length == 0)
            {
                this.lbNomeAmigos = new System.Windows.Forms.Label[1];

                this.lbNomeAmigos[0] = new System.Windows.Forms.Label();
                this.lbNomeAmigos[0].AutoSize = true;
                this.lbNomeAmigos[0].Enabled = true;
                this.lbNomeAmigos[0].BackColor = System.Drawing.Color.Transparent;
                this.lbNomeAmigos[0].Font = new System.Drawing.Font("Century Gothic", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lbNomeAmigos[0].ForeColor = System.Drawing.Color.Turquoise;
                this.lbNomeAmigos[0].Location = new System.Drawing.Point(this.lbTitulo.Location.X - 70, this.lbTitulo.Location.Y + this.lbTitulo.Height + 40);

                this.lbNomeAmigos[0].Text = "Não há usuários online! \n\rTente novamente mais tarde...";

                this.lbNomeAmigos[0].Visible = true;

                this.lbNomeAmigos[0].Invalidate();
                this.lbNomeAmigos[0].Update();
                this.lbNomeAmigos[0].Refresh();
                this.Controls.Add(this.lbNomeAmigos[0]);

                this.picLoading.Visible = true;

                this.btnVoltar.Visible = true;

                return;
            }

            this.btnJogarContraAmigoEspecifico = new RoundedPictureBox[this.lvsUsDispon.Length];
            this.lbNomeAmigos = new Label[this.lvsUsDispon.Length];
            this.lbNivelAmigos = new Label[this.lvsUsDispon.Length];

            int qtdHeight = 0;
            for (int i = 0; i < this.nomesIpUsDispon.Length / 2; i++)
            {
                // "botao"
                this.btnJogarContraAmigoEspecifico[i] = new Jogo_Educativo.RoundedPictureBox();
                this.btnJogarContraAmigoEspecifico[i].Enabled = true;
                this.btnJogarContraAmigoEspecifico[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));

                this.btnJogarContraAmigoEspecifico[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                this.btnJogarContraAmigoEspecifico[i].Cursor = System.Windows.Forms.Cursors.Hand;
                this.btnJogarContraAmigoEspecifico[i].Size = new System.Drawing.Size(275, 50);

                int x;
                if (i % 6 != 0)
                {
                    qtdHeight += this.btnJogarContraAmigoEspecifico[i].Height + 12;
                    x = this.btnJogarContraAmigoEspecifico[i - 1].Location.X;
                }
                else
                {
                    if (i == 0)
                        x = 130;
                    else
                    {
                        qtdHeight = 0;
                        int quo = i / 6;
                        x = this.btnJogarContraAmigoEspecifico[i - 1].Location.X + this.btnJogarContraAmigoEspecifico[i - 1].Width + pxlsXEntreBtnLv;
                        qtdHeight = 0;
                    }
                }

                this.btnJogarContraAmigoEspecifico[i].Location = new System.Drawing.Point(x, yBtnLvInicial + qtdHeight + 3);
                this.btnJogarContraAmigoEspecifico[i].TabIndex = 26 + i;
                this.btnJogarContraAmigoEspecifico[i].Tag = i;

                this.btnJogarContraAmigoEspecifico[i].Invalidate();
                this.btnJogarContraAmigoEspecifico[i].Update();
                this.btnJogarContraAmigoEspecifico[i].Refresh();
                this.Controls.Add(this.btnJogarContraAmigoEspecifico[i]);

                this.btnJogarContraAmigoEspecifico[i].Click += new System.EventHandler(this.btnJogarContraAmigoEspecifico_Click);
                this.btnJogarContraAmigoEspecifico[i].MouseEnter += new System.EventHandler(this.btnJogarContraAmigoEspecifico_MouseEnter);
                this.btnJogarContraAmigoEspecifico[i].MouseLeave += new System.EventHandler(this.btnJogarContraAmigoEspecifico_MouseLeave);

                // NOME
                this.lbNomeAmigos[i] = new System.Windows.Forms.Label();
                this.lbNomeAmigos[i].AutoSize = true;
                this.lbNomeAmigos[i].Enabled = true;
                this.lbNomeAmigos[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));
                this.lbNomeAmigos[i].Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lbNomeAmigos[i].ForeColor = System.Drawing.SystemColors.HighlightText;
                this.lbNomeAmigos[i].Location = new System.Drawing.Point(this.btnJogarContraAmigoEspecifico[i].Location.X + 9, this.btnJogarContraAmigoEspecifico[i].Location.Y + 9);
                this.lbNomeAmigos[i].Cursor = System.Windows.Forms.Cursors.Hand;

                this.lbNomeAmigos[i].Text = this.nomesIpUsDispon[i, 0] + " - ";
                this.lbNomeAmigos[i].Tag = i;

                this.lbNomeAmigos[i].Visible = true;

                this.lbNomeAmigos[i].Invalidate();
                this.lbNomeAmigos[i].Update();
                this.lbNomeAmigos[i].Refresh();
                this.Controls.Add(this.lbNomeAmigos[i]);

                this.btnJogarContraAmigoEspecifico[i].SendToBack();
                this.lbNomeAmigos[i].BringToFront();

                this.lbNomeAmigos[i].Click += new System.EventHandler(this.btnJogarContraAmigoEspecifico_Click);
                this.lbNomeAmigos[i].MouseEnter += new System.EventHandler(this.btnJogarContraAmigoEspecifico_MouseEnter);
                this.lbNomeAmigos[i].MouseLeave += new System.EventHandler(this.btnJogarContraAmigoEspecifico_MouseLeave);


                // NÍVEL
                this.lbNivelAmigos[i] = new System.Windows.Forms.Label();
                this.lbNivelAmigos[i].AutoSize = true;
                this.lbNivelAmigos[i].Enabled = true;
                this.lbNivelAmigos[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));
                this.lbNivelAmigos[i].Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lbNivelAmigos[i].ForeColor = System.Drawing.SystemColors.HighlightText;
                this.lbNivelAmigos[i].Location = new System.Drawing.Point(this.lbNomeAmigos[i].Location.X + this.lbNomeAmigos[i].Width + 2, this.lbNomeAmigos[i].Location.Y + 1);
                this.lbNivelAmigos[i].Cursor = System.Windows.Forms.Cursors.Hand;

                this.lbNivelAmigos[i].Text = "Level " + this.lvsUsDispon[i];
                this.lbNivelAmigos[i].Tag = i;

                this.lbNivelAmigos[i].Visible = true;

                this.lbNivelAmigos[i].Invalidate();
                this.lbNivelAmigos[i].Update();
                this.lbNivelAmigos[i].Refresh();
                this.Controls.Add(this.lbNivelAmigos[i]);

                this.btnJogarContraAmigoEspecifico[i].SendToBack();
                this.lbNivelAmigos[i].BringToFront();

                this.lbNivelAmigos[i].Click += new System.EventHandler(this.btnJogarContraAmigoEspecifico_Click);
                this.lbNivelAmigos[i].MouseEnter += new System.EventHandler(this.btnJogarContraAmigoEspecifico_MouseEnter);
                this.lbNivelAmigos[i].MouseLeave += new System.EventHandler(this.btnJogarContraAmigoEspecifico_MouseLeave);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.usuariosDisponiveis();

            this.desmostrarBtnUsuariosDispon();

            this.mostrarBtnUsuariosDispon();
            Application.DoEvents();
        }

        protected void desmostrarBtnUsuariosDispon()
        {
            if (this.btnJogarContraAmigoEspecifico != null)
            {
                for (int i = 0; i < this.btnJogarContraAmigoEspecifico.Length; i++)
                {
                    if (this.btnJogarContraAmigoEspecifico[i] != null)
                        this.btnJogarContraAmigoEspecifico[i].Visible = false;
                    if (this.lbNomeAmigos[i] != null)
                        this.lbNomeAmigos[i].Visible = false;
                    if (this.lbNivelAmigos[i] != null)
                        this.lbNivelAmigos[i].Visible = false;
                    if (picLoading.Visible == true)
                        picLoading.Visible = false;
                }
            }
            else
            {
                this.lbNomeAmigos[0].Visible = false;
                this.picLoading.Visible = false;
            }
        }

        protected void desmostrarUsuariosDispon()
        {
            this.etapa = 0;

            this.tmrAux.Enabled = false;

            this.lbTitulo.Visible = false;
            this.btnVoltar.Visible = false;
            this.btnRefresh.Visible = false;

            this.desmostrarBtnUsuariosDispon();
        }

        protected void btnJogarContraAmigoEspecifico_Click(object sender, EventArgs e)
        {
            int tag = this.tagLevel(sender);

            if (!this.usDispon(this.nomesIpUsDispon[tag, 1], this.portasUsDispon[tag]))
            {
                MessageBox.Show("Usuário não está mais disponível!");
                this.btnRefresh_Click(null, null);
                return;
            }

            //mandar socket ao outro usuario e ver se ele aceita jogar multiplayer (informacoes: "con" + nomeUsuario + " Lv" + lv; "ip")
            try
            {
                m_clientSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(this.nomesIpUsDispon[tag, 1]);
                int iPortNo = System.Convert.ToInt32(this.portasUsDispon[tag]);

                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);

                m_clientSocket.Connect(ipEnd);

                if (m_clientSocket.Connected)
                {
                    try
                    {
                        //enviara requisicao
                        SendData("req" + this.nomeUsuario); //this.nomesIpUsDispon[tag, 1] + this.lvsUsDispon
                        MessageBox.Show("Convite enviado!");

                    }
                    catch (SocketException se)
                    {
                        string str;
                        str = "\nConnection failed, is the server running?\n" + se.Message;
                        MessageBox.Show(str);
                    }
                }
            }
            catch (SocketException se)
            {
                string str;
                str = "\nConnection failed, is the server running?\n" + se.Message;
                MessageBox.Show(str);
            }
        }

        protected void btnJogarContraAmigoEspecifico_MouseEnter(object sender, EventArgs e)
        {
            int tag = this.tagLevel(sender);

            Color cor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));

            this.btnJogarContraAmigoEspecifico[tag].BackColor = cor;
            this.lbNomeAmigos[tag].BackColor = cor;
            this.lbNivelAmigos[tag].BackColor = cor;
        }

        protected void btnJogarContraAmigoEspecifico_MouseLeave(object sender, EventArgs e)
        {
            int tag = this.tagLevel(sender);

            Color cor = cor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(132)))), ((int)(((byte)(254)))));

            this.btnJogarContraAmigoEspecifico[tag].BackColor = cor;
            this.lbNomeAmigos[tag].BackColor = cor;
            this.lbNivelAmigos[tag].BackColor = cor;
        }


        //explicacao e contextualizacao
        protected void comecarExplicacao()
        {
            //pegar do banco explicacoes baseado no nivelAtual
            this.pegarExplicacaoNoBD();

            //pula as linhas nas Strings
            for (int i = 0; i < this.contextualizacao.Length; i++)
                this.pularLinhaStr(ref this.contextualizacao[i], qtdCaract);
            for (int i = 0; i < this.explicacao.Length; i++)
                this.pularLinhaStr(ref this.explicacao[i], qtdCaract);

            if (this.etapa != 6) // se nao explicacao do jogo
            {
                this.etapa = 3;

                this.viradoDireita = false;
                this.colocarImagemPers();
                this.picPers.Location = new Point(802, 371);

                this.picPers.Visible = true;

                this.btnVoltar.Location = new Point(18, 18);
                this.btnVoltar.Visible = true;
            }
            else
            {
                this.viradoDireita = true;

                this.lbCreditos.Visible = true;

                this.jahViuExplicacao = false;
            }

            this.mexerExplicacaoX();

            this.picExplicacao.BringToFront();
            this.lbExplicacao.BringToFront();
            this.picPers.BringToFront();

            this.picExplicacao.Visible = true;
            this.lbExplicacao.Text = "";
            this.lbExplicacao.Visible = true;

            this.lbExplicacao.BringToFront();

            this.btnFrente.Text = "Próximo";
            this.btnTras.Text = "Voltar";
            this.btnTras.BringToFront();
            this.btnTras.Visible = false;
            this.btnFrente.BringToFront();
            this.btnFrente.Visible = true;

            this.tmrEscreverExplic.Enabled = true;

            Application.DoEvents();
        }

        protected void mexerExplicacaoX()
        {
            int qntParaFrente;
            if (!this.viradoDireita)
            {
                qntParaFrente = this.picPers.Location.X - this.picExplicacao.Location.X - this.picExplicacao.Width + 70;
                this.picExplicacao.Image = global::Jogo_Educativo.Properties.Resources.balaoD;
            }
            else
            {
                qntParaFrente = (this.picPers.Location.X + this.picPers.Width - 70) - this.picExplicacao.Location.X;
                this.picExplicacao.Image = global::Jogo_Educativo.Properties.Resources.balaoE;
            }

            this.picExplicacao.Location = new Point(this.picExplicacao.Location.X + qntParaFrente, this.picExplicacao.Location.Y);
            this.lbExplicacao.Location = new Point(this.lbExplicacao.Location.X + qntParaFrente, this.lbExplicacao.Location.Y);
            this.btnFrente.Location = new Point(this.btnFrente.Location.X + qntParaFrente, this.btnFrente.Location.Y);
            this.btnTras.Location = new Point(this.btnTras.Location.X + qntParaFrente, this.btnTras.Location.Y);
        }

        protected void mostrarSegundaParteExplicacao()
        {
            this.jogando = false;
            this.tmrCarregarPoder.Enabled = false;
            this.tmrInimigos.Enabled = false;

            this.poderPulandoRevivendoQuiz = new bool[4];
            this.poderPulandoRevivendoQuiz[0] = this.tmrJogarPoder.Enabled;
            this.poderPulandoRevivendoQuiz[1] = this.tmrPular.Enabled;
            this.poderPulandoRevivendoQuiz[2] = this.tmrRevivendo.Enabled;
            this.poderPulandoRevivendoQuiz[3] = this.tmrTempoQuiz.Enabled;

            this.tmrJogarPoder.Enabled = false;
            this.tmrPular.Enabled = false;
            this.tmrRevivendo.Enabled = false;
            this.tmrTempoQuiz.Enabled = false;

            this.picPers.Visible = true;

            this.lbExplicacao.Text = "";
            this.picExplicacao.Visible = true;
            this.lbExplicacao.Visible = true;

            this.arrumarBotoesExp();
            this.btnFrente.Visible = true;
            this.btnFrente.Focus();
            this.qtdCaracAtual = 0;
            this.tmrEscreverExplic.Enabled = true;

            this.btnVoltar.Visible = true;

            if(this.etapa == 6)
                this.lbCreditos.Visible = true;
        }

        protected void pularLinhaStr(ref String str, int qtdCaracteres)
        {
            str += " ";
            int iEspacoAtual = 0;
            int iEspacoAntigo = 0;

            for (; ; )
            {
                int iEspaco = 0;

                while (iEspaco - iEspacoAtual <= qtdCaracteres)
                {
                    iEspacoAntigo = iEspaco;
                    iEspaco = str.IndexOf(" ");

                    if (iEspaco < 0)
                    {
                        iEspacoAntigo = iEspaco;
                        break;
                    }

                    //substituir
                    str = str.Remove(iEspaco, 1);
                    str = str.Insert(iEspaco, "_");
                }

                if (iEspacoAntigo == str.Length - 1 || iEspacoAntigo < 0)
                    break;

                str = str.Insert(iEspacoAntigo + 1, "\n");
                iEspacoAtual = iEspacoAntigo + 2;
            }

            str = (str.Replace("_", " ")).Substring(0, str.Length);
        }

        protected void btnFrente_Click(object sender, EventArgs e)
        {
            this.tmrEscreverExplic.Enabled = false;

            if (this.qlExplic >= 0)
            {
                this.qlExplic++;

                if (this.qlExplic >= this.explicacao.Length)
                {
                    this.lbCreditos.Visible = false;
                    this.desmostrarExplicacao(this.etapa != 6);

                    if (this.etapa == 6)
                    {
                        this.voltarJogarExplicacao();
                        this.tmrJogarPoder.Enabled = this.poderPulandoRevivendoQuiz[0];
                        this.tmrPular.Enabled = this.poderPulandoRevivendoQuiz[1];
                        this.tmrRevivendo.Enabled = this.poderPulandoRevivendoQuiz[2];
                        this.tmrTempoQuiz.Enabled = this.poderPulandoRevivendoQuiz[3];
                        this.jahViuExplicacao = true;
                        this.Focus();
                    }
                    else
                        this.iniciarLevel();
                    return;
                }
            }
            else
            {
                this.qlContext++;

                if (this.qlContext >= this.contextualizacao.Length)
                {
                    this.qlExplic = 0;

                    this.lbCreditos.Visible = false;
                    this.desmostrarExplicacao(false);

                    if (this.etapa == 6)
                        this.voltarJogarExplicacao();
                    else
                    {
                        //personagem anda ateh o outro lado
                        this.aux = 3;
                        this.tmrAux.Interval = 20;
                        this.tmrAux.Enabled = true;
                        return;
                    }
                }
            }

            this.qtdCaracAtual = 0;
            this.tmrEscreverExplic.Enabled = true;
        }

        protected void voltarJogarExplicacao()
        {
            this.jogando = true;
            this.tmrCarregarPoder.Enabled = true;
            this.tmrInimigos.Enabled = true;
            this.Focus();
        }

        protected void btnTras_Click(object sender, EventArgs e)
        {
            this.tmrEscreverExplic.Enabled = false;

            if (this.qlExplic > 0)
            {
                this.qlExplic--;

                if (this.qlExplic == 0)
                    this.btnTras.Visible = false;
            }
            else
            {
                this.qlContext--;

                if (this.qlContext == 0)
                    this.btnTras.Visible = false;
            }

            this.btnFrente.Text = "Próximo";

            this.qtdCaracAtual = 0;
            this.tmrEscreverExplic.Enabled = true;
        }

        protected void tmrEscreverExplic_Tick(object sender, EventArgs e)
        {
            if (this.qtdCaracAtual == 0)
                this.arrumarBotoesExp();

            if (this.qlExplic >= 0)
            {
                if (this.qtdCaracAtual + 2 < this.explicacao[this.qlExplic].Length - 1
                    && this.explicacao[this.qlExplic].Substring(this.qtdCaracAtual, 2) == "\n")
                    this.qtdCaracAtual += 3;
                else
                    this.qtdCaracAtual++;

                this.lbExplicacao.Text = this.explicacao[this.qlExplic].Substring(0, this.qtdCaracAtual);

                if (this.qtdCaracAtual == this.explicacao[this.qlExplic].Length - 1)
                    this.tmrEscreverExplic.Enabled = false;
            }
            else
            {
                if (this.qtdCaracAtual + 2 < this.contextualizacao[this.qlContext].Length - 1
                    && this.contextualizacao[this.qlContext].Substring(this.qtdCaracAtual, 2) == "\n")
                    this.qtdCaracAtual += 3;
                else
                    this.qtdCaracAtual++;

                this.lbExplicacao.Text = this.contextualizacao[this.qlContext].Substring(0, this.qtdCaracAtual);

                if (this.qtdCaracAtual == this.contextualizacao[this.qlContext].Length - 1)
                    this.tmrEscreverExplic.Enabled = false;
            }

            Application.DoEvents();
        }

        protected void arrumarBotoesExp()
        {
            if (this.qlExplic >= 0)
            {
                if (this.qlExplic == 0)
                    this.btnTras.Visible = false;
                else
                if (this.qlExplic == 1)
                    this.btnTras.Visible = true;

                if (this.qlExplic == this.explicacao.Length - 1)
                {
                    if (this.levelAtual == 0)
                        this.btnFrente.Text = "Entendi!";
                    else
                        this.btnFrente.Text = "Iniciar Level";
                }
            }
            else
            {
                if (this.qlContext == 0)
                {
                    this.btnTras.Visible = false;
                    this.btnTras.Focus();
                }
                else
                    this.btnTras.Visible = true;

                //se eh o ultimo
                if (this.qlContext == this.contextualizacao.Length - 1)
                    this.btnFrente.Text = "Entendi!";
                else
                    this.btnFrente.Text = "Próximo";
            }

            if (this.qtdCaracAtual == 0)
                this.btnFrente.Focus();

            Application.DoEvents();
        }

        protected void desmostrarExplicacao(bool desmostrarTudo)
        {
            if (desmostrarTudo)
            {
                this.etapa = 0;
                this.picPers.Visible = false;
                this.btnVoltar.Visible = false;

                if (this.aux == 3)
                    this.tmrAux.Enabled = false;
            }

            this.btnFrente.Visible = false;
            this.btnFrente.Text = "Próximo";
            this.btnTras.Visible = false;
            this.picExplicacao.Visible = false;
            this.lbExplicacao.Visible = false;
            this.lbCreditos.Visible = false;
        }

        protected void lbCreditos_Click(object sender, EventArgs e)
        {
            this.desmostrarExplicacao(true);
            
            for (int i = 0; i < this.qtdInim; i++)
                this.imgInimigos[i].Visible = false;
            for (int i = 0; i < this.qtdObst; i++)
                this.imgObstaculos[i].Visible = false;
            this.lbVidas.Visible = false;
            this.lbQuizesErrados.Visible = false;
            this.lbPoder.Visible = false;
            this.prgPoder.Visible = false;
            this.picMoedas.Visible = false;
            this.lbMoedas.Visible = false;
            this.picBtnHome.Visible = false;
            this.btnHome.Visible = false;
            this.picBtnVoltar.Visible = false;
            this.lbXp.Visible = false;
            this.lbIndicarXP.Visible = false;
            this.lbNivel.Visible = false;
            this.prgXp.Visible = false;
            this.picQuizBox.Visible = false;
            this.btnVoltar.Enabled = true;

            this.mostrarCreditos();
            Application.DoEvents();
        }

        protected void mostrarCreditos()
        {
            this.etapa = 7;

            this.btnVoltar.Location = new Point(18, 18);
            this.lbTitulo.Location = new Point(155, this.lbTitulo.Location.Y);
            this.lbTitulo.Text = "CREDITOS";
            this.lbTitulo.Visible = true;
            this.lsbCreditos.Visible = true;
            this.btnVoltar.Visible = true;
        }

        protected void desmostrarCreditos()
        {
            this.etapa = 0;

            this.lsbCreditos.Visible = false;
            this.lbTitulo.Visible = false;
        }


        //comeco do level
        protected void iniciarLevel()
        {
            this.morreu = false;

            if (this.etapa != 6)
                this.etapa = 4;

            this.viradoDireita = true;
            this.colocarImagemPers();
            this.picPers.Location = new Point(320, 371);
            this.picPers.Visible = true;

            this.BackgroundImage = global::Jogo_Educativo.Properties.Resources.Wallpaper1;

            //variaveis gerais
            this.vidas = qtdVidas;
            this.qtdQuizesErrados = 0;
            this.prgPoder.Value = 0;
            this.colocarVidasQuizesErradosNaTela();
            this.lbVidas.Visible = true;
            this.lbQuizesErrados.Visible = true;

            Random rnd = new Random();

            if (this.levelAtual == 0)
            {
                this.qtdObst = 2;
                this.qtdInim = 1;
                this.qlQuizBox = rnd.Next(0, this.qtdInim);
            }
            else
            {
                if(this.levelAtual == 1)
                {
                    this.qtdObst = 1 + this.levelAtual;
                    this.qtdInim = this.levelAtual;
                }
                else
                {
                    this.qtdObst = 3;
                    this.qtdInim = 2;
                }

                this.qlQuizBox = rnd.Next(-1, this.qtdInim);
            }

            this.imgObstaculos = new PictureBox[this.qtdObst];

            this.imgInimigos = new PictureBox[this.qtdInim];
            this.dirQlInim = new byte[this.qtdInim, 2];

            int x = rnd.Next(400 + qtdAndar + this.picPers.Width, 900);

            for (int i = 0; i < this.qtdObst; i++)
            {
                if (i != 0)
                {
                    x += rnd.Next(this.imgInimigos[i - 1].Width + 151, this.imgInimigos[i - 1].Width + 1 + 400);

                    if (i - 1 == this.qlQuizBox)
                    {
                        int posQB = rnd.Next(this.imgObstaculos[i - 1].Location.X + this.imgObstaculos[i - 1].Width + 1, x - this.picQuizBox.Width);
                        this.picQuizBox.Location = new Point(posQB, this.picPers.Location.Y - 2 * quantoSubirPorVez + 15);
                        this.picQuizBox.Visible = true;
                    }
                }

                this.setObstaculo(i, x);

                //se nao eh o ultimo
                if (i != this.qtdObst - 1)
                {
                    x += rnd.Next(this.imgObstaculos[i].Width + 151, this.imgObstaculos[i].Width + 1 + 400);

                    this.setInimigo(i, x);
                }
            }

            this.pos = this.picPers.Location.X;
            this.posFinal = x + this.imgObstaculos[this.qtdObst - 1].Width + 2 * qtdAndar;

            this.picFinish.Location = new Point(this.posFinal + this.picPers.Width / 2, this.picFinish.Location.Y);
            this.picFinish.Visible = true;

            this.picFinal.Location = new Point(this.posFinal + pxlsPosFinal + this.picPers.Width, this.picFinal.Location.Y);
            this.picFinal.Visible = true;

            //xp
            this.colocarXPTela();
            this.lbXp.Location = new Point(688, 52);
            this.lbIndicarXP.Location = new Point(818, 32);
            this.lbNivel.Location = new Point(794, 12);
            this.prgXp.Location = new Point(667, 19);
            this.lbXp.Visible = true;
            this.lbIndicarXP.Visible = true;
            this.lbNivel.Visible = true;
            this.prgXp.Visible = true;

            //moedas
            this.colocarMoedasNaTela();
            this.lbMoedas.Location = new Point(694, 98);
            this.picMoedas.Location = new Point(802, 92);
            this.lbMoedas.Visible = true;
            this.picMoedas.Visible = true;

            this.prgPoder.Visible = true;
            this.lbPoder.Visible = true;

            if (this.levelAtual == 0)
            {
                this.jogando = false;
                this.tmrInimigos.Enabled = false;
                this.tmrCarregarPoder.Enabled = false;
            }
            else
            {
                this.jogando = true;
                this.tmrInimigos.Enabled = true;
                this.tmrCarregarPoder.Enabled = true;
            }

            //mostrar qual level em label
            if (this.levelAtual > 0)
            {
                this.aux = 0;
                this.tmrAux.Interval = 2000;
                this.lbGrande.Text = "Lv" + this.levelAtual + "- " + this.leveis[this.levelAtual - 1];
                this.lbGrande.Height = 73;
                this.lbGrande.Visible = true;
                this.tmrAux.Enabled = true;
            }

            this.btnVoltar.Location = new Point(18, 118);
            this.btnVoltar.Visible = true;
            this.picBtnVoltar.Visible = true;

            if(etapa != 6)
            {
                this.btnHome.Visible = true;
                this.picBtnHome.Visible = true;
            }

            this.btnVoltar.Enabled = false;
            this.btnHome.Enabled = false;

            this.music.Stop();
            this.music = new System.Media.SoundPlayer();
            this.music.Stream = Properties.Resources.JOGO_Magicka;
            this.music.PlayLooping();

            this.Focus();
        }


        //setters (obstaculo e inimigo)
        protected void setObstaculo(int i, int x)
        {
            Random rnd = new Random();
            byte obstaculo = (byte)rnd.Next(1, 5);

            this.imgObstaculos[i] = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgObstaculos[i])).BeginInit();
            this.imgObstaculos[i].BackColor = System.Drawing.Color.Transparent;
            this.imgObstaculos[i].TabStop = true;
            this.imgObstaculos[i].SizeMode = PictureBoxSizeMode.StretchImage;
            this.imgObstaculos[i].Size = new System.Drawing.Size(71, 64);
            this.imgObstaculos[i].BackColor = Color.Transparent;

            switch (obstaculo)
            {
                case 1:
                    this.imgObstaculos[i].Image = global::Jogo_Educativo.Properties.Resources.obstaculo1___Cópia;
                    this.imgObstaculos[i].Size = new System.Drawing.Size(71, 64);
                    break;
                case 2:
                    this.imgObstaculos[i].Image = global::Jogo_Educativo.Properties.Resources.obstaculo2___Cópia;
                    this.imgObstaculos[i].Size = new System.Drawing.Size(77, 56);
                    break;
                case 3:
                    this.imgObstaculos[i].Image = global::Jogo_Educativo.Properties.Resources.obstaculo3___Cópia;
                    this.imgObstaculos[i].Size = new System.Drawing.Size(69, 46);
                    break;
                default:
                    this.imgObstaculos[i].Image = global::Jogo_Educativo.Properties.Resources.obstaculo4___Cópia;
                    this.imgObstaculos[i].Size = new System.Drawing.Size(69, 54);
                    break;
            }
           
            int y;
            //obstaculo no alto
            if (obstaculo >= 5)
                y = 296; //um pouco menos que o teto
            else
                y = 484 - this.imgObstaculos[i].Height;
            this.imgObstaculos[i].Location = new System.Drawing.Point(x, y);
            this.imgObstaculos[i].TabIndex = 3;
            this.imgObstaculos[i].TabStop = false;

            //this.imgObstaculos[i].BackColor = System.Drawing.Color.Transparent;

            this.imgObstaculos[i].Invalidate();
            this.imgObstaculos[i].Update();
            this.imgObstaculos[i].Refresh();
            this.Controls.Add(this.imgObstaculos[i]);
            Application.DoEvents();
        }

        protected void setInimigo(int i, int x)
        {
            Random rnd = new Random();

            byte dir = (byte)rnd.Next(1, 3);
            byte ql = (byte)rnd.Next(1, 4);
            this.dirQlInim[i, 0] = dir;
            this.dirQlInim[i, 1] = ql;

            //inimigo pode ser no alto ou em baixo
            int y;
            if (i != this.qlQuizBox && this.dirQlInim[i, 1] == 1 && rnd.Next(1, 3) == 1) //50% de chance se eh inimigo específico
                y = 296; //um pouco menos que o teto
            else
                y = 391;

            this.imgInimigos[i] = new System.Windows.Forms.PictureBox();
            this.imgInimigos[i].BackColor = System.Drawing.Color.Transparent;
            this.imgInimigos[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgInimigos[i].Location = new System.Drawing.Point(x, y);
            this.imgInimigos[i].Size = new System.Drawing.Size(75, 80);
            this.imgInimigos[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            switch (ql)
            {
                case 1:
                    if (dir == 1)
                        this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo1D___Cópia;
                    else
                        this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo1E___Cópia;
                    break;
                case 2:
                    if (dir == 1)
                        this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo2D___Cópia;
                    else
                        this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo2E___Cópia;
                    break;
                default:
                    if (dir == 1)
                        this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo3D___Cópia;
                    else
                        this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo3E___Cópia;
                    break;
            }

            this.imgInimigos[i].Invalidate();
            this.imgInimigos[i].Update();
            this.imgInimigos[i].Refresh();
            this.Controls.Add(this.imgInimigos[i]);
            Application.DoEvents();
        }


        //final do level
        protected void morrer()
        {
            if (!tmrRevivendo.Enabled)
            {
                this.vidas--;
                this.colocarVidasQuizesErradosNaTela();

                if (this.vidas <= 0)
                {
                    this.morreu = true;
                    this.jogando = false;
                    this.morrerRealmente();
                }
                else
                    tmrRevivendo.Enabled = true;

                Application.DoEvents();
            }
        }

        protected void morrerRealmente()
        {
            this.colocarImagemPers();

            this.lbGrande.Text = "VOCÊ PERDEU!";
            this.lbGrande.Height = 73;
            this.lbGrande.Visible = true;

            this.lbGrande.BringToFront();

            this.aux = 4;
            this.tmrAux.Interval = 1500;
            this.tmrAux.Enabled = true;
        }

        protected void ganhar()
        {
            if (this.matouTodosInimigos())
            {
                this.jogando = false;

                this.tmrCarregarPoder.Enabled = false;
                this.tmrInimigos.Enabled = false;

                this.btnVoltar.Visible = false;
                this.picBtnVoltar.Visible = false;
                this.btnHome.Visible = false;
                this.picBtnHome.Visible = false;

                this.prgPoder.Visible = false;

                this.lbPoder.Visible = false;

                this.aux = 1;
                this.tmrAux.Interval = 200;
                this.tmrAux.Enabled = true;
            }
            else
                MessageBox.Show("Mate todos os inimigos para ganhar!");
        }

        protected bool matouTodosInimigos()
        {
            for (int i = 0; i <= qtdInim - 1; i++)
                if (this.dirQlInim[i, 0] != 0)
                    return false;

            return true;
        }

        protected void pararJogo()
        {
            this.tmrAux.Enabled = false;
            this.tmrCarregarPoder.Enabled = false;
            this.tmrEscreverExplic.Enabled = false;
            this.tmrInimigos.Enabled = false;
            this.tmrJogarPoder.Enabled = false;
            this.tmrPular.Enabled = false;
            this.tmrRevivendo.Enabled = false;
            this.tmrTempoQuiz.Enabled = false;
        }

        protected void finalizarJogo(bool continuarJogo)
        {
            if (this.etapa != 6)
                this.etapa = 0;

            this.pararJogo();

            if (continuarJogo)
                this.aux = 6;
            else
                this.aux = 7;

            this.tmrAux.Interval = this.tmrInimigos.Interval + 10;
            this.tmrAux.Enabled = true;
        }

        protected void tirarJogoDaTela()
        {
            this.btnHome.Visible = false;
            this.btnVoltar.Visible = false;
            this.picBtnVoltar.Visible = false;
            this.picBtnHome.Visible = false;
            this.picPers.Visible = false;
            this.prgPoder.Visible = false;
            this.pnlQuiz.Visible = false;
            this.lbVidas.Visible = false;
            this.lbQuizesErrados.Visible = false;
            this.lbPoder.Visible = false;
            this.lbXp.Visible = false;
            this.lbIndicarXP.Visible = false;
            this.lbNivel.Visible = false;
            this.prgXp.Visible = false;
            this.picPoder.Visible = false;
            this.picQuizBox.Visible = false;
            this.lbMoedas.Visible = false;
            this.picMoedas.Visible = false;
            this.picFinal.Visible = false;
            this.picFinish.Visible = false;
            this.lbGrande.Visible = false;

            if (this.imgInimigos != null)
            {
                foreach (PictureBox pic in this.imgInimigos)
                    if (pic != null)
                        pic.Visible = false;
                this.imgInimigos = null;
            }

            if (this.imgObstaculos != null)
            {
                foreach (PictureBox pic in this.imgObstaculos)
                    if (pic != null)
                        pic.Visible = false;
                this.imgObstaculos = null;
            }

            if (this.picEstrelasGanhou != null)
            {
                foreach (PictureBox pic in this.picEstrelasGanhou)
                    if (pic != null)
                        pic.Visible = false;
                this.picEstrelasGanhou = null;
            }
        }


        //mexer/andar
        protected void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.clicando && this.jogando && !this.fazendoQuiz)
            {
                if (e.KeyValue == 37)//left arrow
                {
                    if (this.pos >= qtdAndar)
                    {
                        if (this.viradoDireita)
                        {
                            //colocar imagem virado para esquerda
                            this.viradoDireita = false;
                            this.colocarImagemPers();
                        }

                        if (this.bateuEmObstaculo(0, 2) == -2)
                            this.andarPers(false);
                    }
                }
                else if (e.KeyValue == 38)//up arrow
                {
                    if (!this.pulou2Vez)
                    {
                        if (this.tmrPular.Enabled)
                        {
                            this.pulou2Vez = true;
                            this.quantoSubir = this.quantoSubiu + quantoSubirPorVez;
                        }

                        this.tmrPular.Enabled = true;
                    }
                }
                else if (e.KeyValue == 39)//right arrow
                {
                    if (this.pos + qtdAndar <= this.posFinal)
                    {
                        if (!this.viradoDireita)
                        {
                            //colocar imagem virado para direita
                            this.viradoDireita = true;
                            this.colocarImagemPers();
                        }

                        if (this.bateuEmObstaculo(0, 1) == -2)
                            this.andarPers(true);

                        if (this.etapa == 6 && !this.jahViuExplicacao && this.picQuizBox.Visible && this.picPers.Location.X + this.picPers.Width + 170 >= this.picQuizBox.Location.X)
                        {
                            this.jahViuExplicacao = false;

                            this.mostrarSegundaParteExplicacao();
                        }
                    }
                    else
                    {
                        this.ganhar();
                        return;
                    }
                }
                else if (e.KeyValue == 32)//space bar
                {
                    if (this.prgPoder.Value == this.prgPoder.Maximum && !this.tmrJogarPoder.Enabled)
                        this.mostrarQuiz();
                }

                if (e.KeyValue == 37 || e.KeyValue == 39)
                {
                    if (!this.tmrPular.Enabled && this.picPers.Location.Y <= chao && this.bateuEmObstaculo(0, 4) == -2)
                    {
                        this.subindo = false;
                        this.tmrPular.Enabled = true;
                    }

                    //this.picBxCenario.Refresh();

                    this.bateuEmInimigo(0);
                }
            }

            this.clicando = true;
            Application.DoEvents();
        }

        protected void colocarImagemPers()
        {
            //0:morto direita, 1:direita, 2:esquerda, 3:morto esquerda
            int lado;

            if (!this.morreu || (this.etapa != 4 && this.etapa != 6))
            {
                if (this.viradoDireita)
                    lado = 1;
                else
                    lado = 2;
            }
            else
            {
                if (this.viradoDireita)
                    lado = 0;
                else
                    lado = 3;
            }


            this.picPers.Image = this.personagens[this.personagem, lado];
        }

        protected void Principal_KeyUp(object sender, KeyEventArgs e)
        {
            this.clicando = false;
        }

        protected void andarPers(bool direita)
        {
            if (direita)
                this.pos += qtdAndar;
            else
                this.pos -= qtdAndar;

            int qtd;
            if (direita)
                //se anda pra direita os obstaculos e inimigos ficam mais perto
                qtd = -qtdAndar;
            else
                qtd = qtdAndar;

            //deixar inimigos e obstaculos mais longe
            for (int i = 0; i < this.qtdInim; i++)
            {
                this.imgObstaculos[i].Location = new Point(this.imgObstaculos[i].Location.X + qtd, this.imgObstaculos[i].Location.Y);
                this.imgInimigos[i].Location = new Point(this.imgInimigos[i].Location.X + qtd, this.imgInimigos[i].Location.Y);

                if (this.qlQuizBox == i)
                    this.picQuizBox.Location = new Point(this.picQuizBox.Location.X + qtd, this.picQuizBox.Location.Y);
            }
            this.imgObstaculos[this.qtdObst - 1].Location = new Point(this.imgObstaculos[this.qtdObst - 1].Location.X + qtd, this.imgObstaculos[this.qtdObst - 1].Location.Y);
            this.picFinish.Location = new Point(this.picFinish.Location.X + qtd, this.picFinish.Location.Y);
            this.picFinal.Location = new Point(this.picFinal.Location.X + qtd, this.picFinal.Location.Y);

            this.Invalidate();
            this.Update();
            this.Refresh();
        }

        protected void andarSerioPers(bool direita)
        {
            int qntAnda = (int)qtdAndar / 2;

            if (direita)
                this.picPers.Location = new Point(this.picPers.Location.X + qntAnda, this.picPers.Location.Y);
            else
                this.picPers.Location = new Point(this.picPers.Location.X - qntAnda, this.picPers.Location.Y);

            this.Invalidate();
            this.Update();
            this.Refresh();
        }

        protected void andarInimigo(int i, bool direita)
        {
            if (direita)
                this.imgInimigos[i].Location = new Point(this.imgInimigos[i].Location.X + qtdAndarInimigos, this.imgInimigos[i].Location.Y);
            else
                this.imgInimigos[i].Location = new Point(this.imgInimigos[i].Location.X - qtdAndarInimigos, this.imgInimigos[i].Location.Y);

            this.Invalidate();
            this.Update();
            this.Refresh();
            
        }

        protected void mudarDirecaoInim(int i, bool morreu)
        {
            byte aux = this.dirQlInim[i, 0];
            if (morreu)
                this.dirQlInim[i, 0] = 0;
            else
                if (this.dirQlInim[i, 0] == 1)
                this.dirQlInim[i, 0] = 2;
            else
                this.dirQlInim[i, 0] = 1;

            switch (this.dirQlInim[i, 1])
            {
                case 1:
                    switch (this.dirQlInim[i, 0])
                    {
                        case 0:
                            //adicionar inimigo morto
                            if (aux == 1)
                                this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo1DMorreu___Cópia;
                            else
                                this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo1EMorreu___Cópia;
                            break;
                        case 1:
                            this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo1D___Cópia;
                            break;
                        case 2:
                            this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo1E___Cópia;
                            break;
                    }
                    break;
                case 2:
                    switch (this.dirQlInim[i, 0])
                    {
                        case 0:
                            //adicionar inimigo morto
                            if (aux == 1)
                                this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo2DMorreu___Cópia;
                            else
                                this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo2EMorreu___Cópia;
                            break;
                        case 1:
                            this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo2D___Cópia;
                            break;
                        case 2:
                            this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo2E___Cópia;
                            break;
                    }
                    break;
                case 3:
                    switch (this.dirQlInim[i, 0])
                    {
                        case 0:
                            //adicionar inimigo morto
                            if (aux == 1)
                                this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo3DMorreu___Cópia;
                            else
                                this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo3EMorreu___Cópia;
                            break;
                        case 1:
                            this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo3D___Cópia;
                            break;
                        case 2:
                            this.imgInimigos[i].Image = global::Jogo_Educativo.Properties.Resources.inimigo3E___Cópia;
                            break;
                    }
                    break;
            }
        }


        //bateu
        protected int bateuEmObstaculo(int img, byte lado)
        //img: 0:pers, 1-(qtdInim-1): inimigos
        //lado: 1:direita, 2:esquerda, 3:cima, 4:baixo
        //retorno: -2: nao bateu, -1: bateu em QuizBox, 0-(this.qtdObst-1): qual obst
        {
            int qtd = 0;

            switch (lado)
            {
                case 1:
                case 2:
                    if (img == 0)
                        qtd = qtdAndar;
                    else
                        qtd = qtdAndarInimigos;
                    break;
                case 3:
                case 4:
                    qtd = qtdPular;
                    break;
            }

            //direcao: 0:todas, 1:direita, 2:esquerda, 3:cima, 4:baixo
            //qtd: (qtdAndar, qtdAndarInimigo ou qtdPular) smp positivo
            for (int i = 0; i <= this.qtdObst - 1; i++)
                if (this.bateuEmObstaculoEspecifico(i, img, lado, qtd))
                    return i;

            if (img == 0)
                if (this.picQuizBox.Visible && this.bateuEmObstaculoEspecifico(-1, 0, lado, qtd))
                {
                    //soh mostra o quiz se está embaixo e foi o personagem
                    if (img == 0 && lado == 3 && !this.tmrTempoQuiz.Enabled)
                    {
                        this.tipoQuiz = 1;
                        this.mostrarQuiz();
                    }

                    return -1;
                }

            return -2;
        }

        protected bool bateuEmObstaculoEspecifico(int i, int img, byte lado, int qtd)
        //img: 0:pers, 1-(qtdInim-1): inim
        //lado: 0:todas, 1:direita, 2:esquerda, 3:cima, 4:baixo
        {
            PictureBox auxOutro;

            if (img == 0)
                auxOutro = this.picPers;
            else
                auxOutro = this.imgInimigos[img - 1];

            PictureBox auxObst;
            if (i < 0)
                auxObst = this.picQuizBox;
            else
                auxObst = this.imgObstaculos[i];

            bool colidiuX = true;
            //cima ou baixo
            if (lado == 3 || lado == 4 || lado == 0)
            {
                if (auxObst.Width >= auxOutro.Width - 2 * pxlsLadoBateu)
                    colidiuX = (auxOutro.Location.X + auxOutro.Width - pxlsLadoBateu >= auxObst.Location.X &&
                        auxOutro.Location.X + auxOutro.Width - pxlsLadoBateu <= auxObst.Location.X + auxObst.Width) ||
                        (auxOutro.Location.X + pxlsLadoBateu >= auxObst.Location.X &&
                        auxOutro.Location.X + pxlsLadoBateu <= auxObst.Location.X + auxObst.Width);
                else
                    colidiuX = (auxObst.Location.X >= auxOutro.Location.X + pxlsLadoBateu &&
                        auxObst.Location.X <= auxOutro.Location.X + auxOutro.Width - pxlsLadoBateu) ||
                        (auxObst.Location.X + auxObst.Width >= auxOutro.Location.X + pxlsLadoBateu &&
                        auxObst.Location.X + auxObst.Width <= auxOutro.Location.X + auxOutro.Width - pxlsLadoBateu);
            }
            else
            //direita
            if (lado == 1)
            {
                if (auxObst.Width >= auxOutro.Width + qtd - pxlsLadoBateu)
                    colidiuX = auxOutro.Location.X + auxOutro.Width + qtd - pxlsLadoBateu >= auxObst.Location.X &&
                        auxOutro.Location.X <= auxObst.Location.X;
                else
                    colidiuX = auxObst.Location.X >= auxOutro.Location.X &&
                        auxObst.Location.X <= auxOutro.Location.X + auxOutro.Width + qtd - pxlsLadoBateu;
            }
            else
            //esquerda
            if (lado == 2)
            {
                if (auxObst.Width >= auxOutro.Width + qtd - pxlsLadoBateu)
                    colidiuX = auxOutro.Location.X - qtd + pxlsLadoBateu <= auxObst.Location.X + auxObst.Width &&
                        auxOutro.Location.X + auxOutro.Width >= auxObst.Location.X + auxObst.Width;
                else
                    colidiuX = auxObst.Location.X + auxObst.Width >= auxOutro.Location.X - qtd + pxlsLadoBateu &&
                        auxObst.Location.X + auxObst.Width <= auxOutro.Location.X + auxOutro.Width;
            }

            if (!colidiuX)
                return false;

            //se inimigo colidiu em X em obstaculo, retorna true
            if (colidiuX && img > 0 && i >= 0)
                return true;

            bool colidiuY = true;
            //direita ou esquerda
            if (lado == 1 || lado == 2 || lado == 0)
            {
                if (auxObst.Height >= auxOutro.Height)
                    colidiuY = (auxOutro.Location.Y >= auxObst.Location.Y &&
                        auxOutro.Location.Y <= auxObst.Location.Y + auxObst.Height) ||
                        (auxOutro.Location.Y + auxOutro.Height >= auxObst.Location.Y &&
                        auxOutro.Location.Y <= auxObst.Location.Y);
                else
                    colidiuY = (auxObst.Location.Y + auxObst.Height >= auxOutro.Location.Y &&
                        auxObst.Location.Y + auxObst.Height <= auxOutro.Location.Y + auxOutro.Height) ||
                        (auxObst.Location.Y <= auxOutro.Location.Y + auxOutro.Height &&
                        auxObst.Location.Y >= auxOutro.Location.Y);
            }
            else
            //cima
            if (lado == 3)
            {
                if (auxObst.Height >= auxOutro.Height + qtd)
                    colidiuY = auxOutro.Location.Y >= auxObst.Location.Y &&
                        auxOutro.Location.Y - qtd <= auxObst.Location.Y + auxObst.Height;
                else
                    colidiuY = auxObst.Location.Y + auxObst.Height >= auxOutro.Location.Y - qtd &&
                        auxObst.Location.Y + auxObst.Height <= auxOutro.Location.Y;
            }
            else
            //baixo
            if (lado == 4)
            {
                if (auxObst.Height >= auxOutro.Height + qtd)
                    colidiuY = (auxOutro.Location.Y + auxOutro.Height + qtd >= auxObst.Location.Y &&
                        auxOutro.Location.Y <= auxObst.Location.Y) || auxOutro.Location.Y + qtd >= chao;
                else
                    colidiuY = (auxObst.Location.Y <= auxOutro.Location.Y + auxOutro.Height + qtd &&
                        auxObst.Location.Y >= auxOutro.Location.Y) || auxOutro.Location.Y + qtd >= chao;
            }
            
            return colidiuY;
        }

        protected bool bateuEmInimigo(int img)
        //img: 0:pers, 1-(qtdInim-1): inim
        {
            //se o personagem morreu e está revivendo, nao morre de novo
            if ((img == 0 && this.tmrRevivendo.Enabled) || this.morreu)
                return false;

            //ver se bateu
            for (int i = 0; i <= this.qtdInim - 1; i++)
                if (this.bateuEmInimigoEspecifico(i, img))
                {
                    if (img == 0)
                        this.morrer();

                    return true;
                }

            return false;
        }

        protected bool bateuEmInimigoEspecifico(int i, int img)
        {
            //se inimigo está morto
            if (this.dirQlInim[i, 0] == 0 || img - 1 == i || this.imgInimigos == null)
                return false;


            PictureBox aux;
            if (img == 0)
                aux = this.picPers;
            else
                //if (img > 0 && img < this.qtdInim)
                aux = this.imgInimigos[img - 1];


            bool colidiuX;
            if (this.imgInimigos[i].Width >= aux.Width)
                colidiuX = (aux.Location.X + aux.Width >= this.imgInimigos[i].Location.X &&
                    aux.Location.X <= this.imgInimigos[i].Location.X) ||
                    (aux.Location.X <= this.imgInimigos[i].Location.X + this.imgInimigos[i].Width &&
                    aux.Location.X + aux.Width >= this.imgInimigos[i].Location.X + this.imgInimigos[i].Width);
            else
                colidiuX = (this.imgInimigos[i].Location.X >= aux.Location.X &&
                    this.imgInimigos[i].Location.X <= aux.Location.X + aux.Width) ||
                    (this.imgInimigos[i].Location.X + this.imgInimigos[i].Width >= aux.Location.X &&
                    this.imgInimigos[i].Location.X + this.imgInimigos[i].Width <= aux.Location.X + aux.Width);

            if (!colidiuX)
                return false;

            if (this.imgInimigos[i].Height >= aux.Height)
                return (aux.Location.Y >= this.imgInimigos[i].Location.Y &&
                    aux.Location.Y <= this.imgInimigos[i].Location.Y + this.imgInimigos[i].Height) ||
                    (aux.Location.Y + aux.Height >= this.imgInimigos[i].Location.Y &&
                    aux.Location.Y <= this.imgInimigos[i].Location.Y);
            return (this.imgInimigos[i].Location.Y + this.imgInimigos[i].Height >= aux.Location.Y &&
                    this.imgInimigos[i].Location.Y + this.imgInimigos[i].Height <= aux.Location.Y) ||
                    (this.imgInimigos[i].Location.Y <= aux.Location.Y + aux.Height &&
                    this.imgInimigos[i].Location.Y >= aux.Location.Y);
        }

        protected byte poderMatou(int i)
        //0: nao matou inimigo nem bateu em obstaculo
        //1: matou inimigo
        //2: bateu obstaculo
        {
            bool chegaEmInimigo = this.poderChegaEmInimigo(i);

            int nObst;
            byte nLado;
            if (this.viradoDireitaPoder)
            {
                nLado = 1;

                if (this.picPers.Location.X <= this.imgObstaculos[i].Location.X)
                    nObst = i;
                else
                    nObst = i + 1;
            }
            else
            {
                nLado = 2;

                if (this.picPers.Location.X >= this.imgObstaculos[i].Location.X)
                    nObst = i + 1;
                else
                    nObst = i;
            }

            bool bateuEmObst = this.bateuEmObstaculoEspecifico(nObst, 0, nLado, this.qtdPoderAtual);

            if (chegaEmInimigo)
            {
                bool bateuObstAntes;
                if (this.viradoDireitaPoder)
                    bateuObstAntes = this.imgObstaculos[nObst].Location.X <= this.imgInimigos[i].Location.X;
                else
                    bateuObstAntes = this.imgObstaculos[nObst].Location.X + this.imgObstaculos[nObst].Width >= this.imgInimigos[i].Location.X + this.imgInimigos[i].Width;

                //bateu em obstaculo antes
                if (bateuEmObst && bateuObstAntes)
                    return 2;
                else
                    return 1;
            }
            else
                if (bateuEmObst)
                return 2;
            else
                return 0;
        }

        protected bool poderChegaEmInimigo(int i)
        {
            //se inimigo está morto
            if (this.dirQlInim[i, 0] == 0)
                return false;


            bool colidiuX;
            if (this.imgInimigos[i].Width >= this.picPoder.Width)
                colidiuX = (this.picPoder.Location.X + this.picPoder.Width >= this.imgInimigos[i].Location.X &&
                    this.picPoder.Location.X <= this.imgInimigos[i].Location.X) ||
                    (this.picPoder.Location.X <= this.imgInimigos[i].Location.X + this.imgInimigos[i].Width &&
                    this.picPoder.Location.X + this.picPoder.Width >= this.imgInimigos[i].Location.X + this.imgInimigos[i].Width);
            else
                colidiuX = (this.imgInimigos[i].Location.X >= this.picPoder.Location.X &&
                    this.imgInimigos[i].Location.X <= this.picPoder.Location.X + this.picPoder.Width) ||
                    (this.imgInimigos[i].Location.X + this.imgInimigos[i].Width >= this.picPoder.Location.X &&
                    this.imgInimigos[i].Location.X + this.imgInimigos[i].Width <= this.picPoder.Location.X + this.picPoder.Width);

            if (!colidiuX)
                return false;

            if (this.imgInimigos[i].Height >= this.picPoder.Height)
                return (this.picPoder.Location.Y >= this.imgInimigos[i].Location.Y &&
                    this.picPoder.Location.Y <= this.imgInimigos[i].Location.Y + this.imgInimigos[i].Height) ||
                    (this.picPoder.Location.Y + this.picPoder.Height >= this.imgInimigos[i].Location.Y &&
                    this.picPoder.Location.Y <= this.imgInimigos[i].Location.Y);
            return (this.imgInimigos[i].Location.Y + this.imgInimigos[i].Height >= this.picPoder.Location.Y &&
                    this.imgInimigos[i].Location.Y + this.imgInimigos[i].Height <= this.picPoder.Location.Y) ||
                    (this.imgInimigos[i].Location.Y <= this.picPoder.Location.Y + this.picPoder.Height &&
                    this.imgInimigos[i].Location.Y >= this.picPoder.Location.Y);
        }


        //timers
        protected void tmrPular_Tick(object sender, EventArgs e)
        {
            if (this.subindo)
            {
                this.quantoSubiu += qtdPular;

                if (this.quantoSubiu >= quantoSubir || this.bateuEmObstaculo(0, 3) >= -1)
                    this.subindo = false;
                else
                {
                    this.picPers.Location = new Point(this.picPers.Location.X, this.picPers.Location.Y - qtdPular);
                    this.bateuEmInimigo(0);
                }
            }
            else
            {
                bool continuarDescendo;
                int iObstBateu = -1;
                if (this.picPers.Location.Y + qtdPular > chao)
                    continuarDescendo = false;
                else
                {
                    iObstBateu = this.bateuEmObstaculo(0, 4);
                    if (iObstBateu <= -1)
                        continuarDescendo = true;
                    else
                        continuarDescendo = false;
                }
                    

                if (continuarDescendo)
                {
                    this.picPers.Location = new Point(this.picPers.Location.X, this.picPers.Location.Y + qtdPular);

                    this.bateuEmInimigo(0);
                }
                else
                {
                    tmrPular.Enabled = false;

                    if (iObstBateu <= -1)
                        this.picPers.Location = new Point(this.picPers.Location.X, 371);
                    else
                        this.picPers.Location = new Point(this.picPers.Location.X, this.imgObstaculos[iObstBateu].Location.Y - 1 - this.picPers.Height);                    

                    this.quantoSubir = quantoSubirPorVez;
                    this.pulou2Vez = false;
                    this.subindo = true;
                    this.quantoSubiu = 0;
                }
            }

            //this.picBxCenario.Refresh();
        }

        protected void tmrInimigos_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= this.qtdInim - 1; i++)
                if (this.dirQlInim[i, 0] != 0)
                {
                    if (this.bateuEmObstaculo(i + 1, this.dirQlInim[i, 0]) >= 0 ||
                        this.bateuEmInimigo(i))
                        this.mudarDirecaoInim(i, false);

                    this.andarInimigo(i, this.dirQlInim[i, 0] == 1);

                    this.bateuEmInimigo(0);
                }

            Application.DoEvents();
        }

        protected void tmrRevivendo_Tick(object sender, EventArgs e)
        {
            this.tempoRevivendo += this.tmrRevivendo.Interval;
            this.picPers.Visible = !this.picPers.Visible;

            if (this.tempoRevivendo >= tempoDeReviver)
            {
                this.tmrRevivendo.Enabled = false;
                this.picPers.Visible = true;

                this.tempoRevivendo = 0;

                if(jogando)
                    this.bateuEmInimigo(0);
            }
        }

        protected void tmrPoder_Tick(object sender, EventArgs e)
        {
            if (this.prgPoder.Value + 18 <= this.prgPoder.Maximum)
                this.prgPoder.Value += 18;
            else
                this.prgPoder.Value = this.prgPoder.Maximum;
        }

        protected void tmrJogarPoder_Tick(object sender, EventArgs e)
        {
            if (this.qtdPoderAtual >= qtdPixelsPoderX)
            {
                this.tmrJogarPoder.Enabled = false;
                this.picPoder.Visible = false;
                this.qtdPoderAtual = 0;

                this.Invalidate();
                this.Update();
                this.Refresh();
                Application.DoEvents();
                return;
            }

            //colocar poder na tela (Width:qtdAndaPoderPixels, Location.X:)
            if (this.viradoDireitaPoder)
                this.picPoder.Location = new Point(this.picPers.Location.X + this.picPers.Width + this.qtdPoderAtual + 1, this.picPoder.Location.Y);
            else
                this.picPoder.Location = new Point(this.picPers.Location.X - this.qtdPoderAtual - this.picPoder.Width - 1, this.picPoder.Location.Y);

            this.picPoder.Visible = true;

            for (int i = 0; i < qtdInim; i++)
            {
                byte nPoder = this.poderMatou(i);
                //0: nao matou inimigo nem bateu em obstaculo
                //1: matou inimigo
                //2: bateu obstaculo

                if (nPoder != 0)
                {
                    if (nPoder == 1)
                        this.mudarDirecaoInim(i, true);

                    this.qtdPoderAtual = 0;
                    this.tmrJogarPoder.Enabled = false;

                    this.picPoder.Visible = false;

                    this.Invalidate();
                    this.Update();
                    this.Refresh();
                    Application.DoEvents();

                    /*this.aux = 2;
                    this.tmrAux.Interval = 20;
                    this.tmrAux.Enabled = true;*/

                    return;
                }
            }

            this.qtdPoderAtual += qtdAndaPoder;
            Application.DoEvents();
        }

        protected void tmrAux_Tick(object sender, EventArgs e)
        {
            switch (this.aux)
            {
                case 0:
                    this.lbGrande.Visible = false;
                    this.tmrAux.Enabled = false;
                    Application.DoEvents();
                    break;
                case 1:
                    if (this.pos + qtdAndar <= this.posFinal + pxlsPosFinal)
                    {
                        this.pos += (int)qtdAndar / 2;
                        this.andarSerioPers(true);
                        Application.DoEvents();
                        Application.DoEvents();
                    }
                    else
                    {
                        this.aux = 0;
                        this.tmrAux.Enabled = false;

                        this.tipoQuiz = 2;
                        this.mostrarQuiz();

                        //o que viria aqui está em ACABARQUIZ
                    }
                    break;
                /*
                case 2:
                    this.picPoder.Visible = false;

                    this.Invalidate();
                    this.Update();
                    this.Refresh();
                    Application.DoEvents();
                    break;*/
                case 3:
                    if (this.picPers.Location.X + (int)qtdAndar / 2 > xPersPararExp)
                    {
                        this.andarSerioPers(false);
                        Application.DoEvents();
                    }
                    else
                    {
                        //this.picPers.Location = new Point(xPersPararExp, this.picPers.Location.Y);

                        this.tmrAux.Enabled = false;

                        this.viradoDireita = true;
                        this.colocarImagemPers();

                        this.mexerExplicacaoX();

                        this.btnFrente.Text = "Próximo";
                        this.mostrarSegundaParteExplicacao();

                        Application.DoEvents();
                    }
                    break;
                case 4:
                    this.tmrAux.Enabled = false;
                    this.finalizarJogo(true);
                    break;
                case 5:
                    this.tmrAux.Enabled = false;
                    this.finalizarJogo(false);
                    break;
                case 6:
                    //quando perdeu
                    this.tmrAux.Enabled = false;
                    this.tirarJogoDaTela();
                    if (this.etapa == 6)
                        this.btnExplicacaoJogo.PerformClick();
                    else
                        this.iniciarLevel();
                    Application.DoEvents();
                    break;
                case 7:
                    //quando ganhou
                    this.tmrAux.Enabled = false;
                    this.tirarJogoDaTela();
                    if (this.etapa == 6)
                        this.mostrarHome(true);
                    else
                        this.mostrarBtnLeveis();
                    Application.DoEvents();
                    break;
                case 8:
                    this.tmrAux.Enabled = false;
                    this.tirarJogoDaTela();
                    if (this.etapa == 6)
                    {
                        this.desmostrarExplicacao(true);
                        this.mostrarHome(true);
                    }
                    else
                        this.mostrarBtnLeveis();
                    this.tocarMusicMenu();
                    Application.DoEvents();
                    break;
                case 9:
                    this.tmrAux.Enabled = false;
                    this.tirarJogoDaTela();
                    this.mostrarHome(true);
                    this.tocarMusicMenu();
                    Application.DoEvents();
                    break;
                case 10:
                    this.btnRefresh.PerformClick();
                    Application.DoEvents();
                    break;
            }
        }


        //quiz 
        protected void mostrarQuiz()
        {
            if (this.tipoQuiz == 0)
                this.prgPoder.Value = 0;

            //tudo fica Xvezes mais lento            
            this.tmrCarregarPoder.Interval *= qtsVezesMaisLento;
            this.tmrInimigos.Interval *= qtsVezesMaisLento;
            this.tmrPular.Interval *= qtsVezesMaisLento;
            this.tmrRevivendo.Interval *= qtsVezesMaisLento;
            this.tmrJogarPoder.Interval *= qtsVezesMaisLento;
            //usuario nao pode mexer
            this.fazendoQuiz = true;

            string[] opcoes = this.pegarOpcoesQuizNoBD();

            this.embaralharOpcoes(ref opcoes);
            //opcoes agora esta embaralhada
            //this.opcaoCerta está com valor

            lbPergunta.Text = opcoes[opcoes.Length - 1]; //ultimo string do vetor
            btnOpcao1.Text = opcoes[0];
            btnOpcao2.Text = opcoes[1];
            btnOpcao3.Text = opcoes[2];
            btnOpcao4.Text = opcoes[3];

            //quizes especiais
            if (this.tipoQuiz != 0)
            {
                this.qntTempoResta = 2 * tempoQuiz;
                this.pnlQuiz.BackColor = Color.Gold;
            }
            else
                this.pnlQuiz.BackColor = Color.ForestGreen;

            //mostra o quiz
            pnlQuiz.Visible = true;
            this.tmrTempoQuiz.Enabled = true;

            Application.DoEvents();
        }

        protected void embaralharOpcoes(ref string[] opcoes)
        {
            int[] restoOp = new int[4];
            restoOp[0] = 0;
            restoOp[1] = 1;
            restoOp[2] = 2;
            restoOp[3] = 3;

            string[] opcoesEmb = new string[5];
            int qtsOp = 0;
            Random rnd = new Random();

            int random;
            //int qual=0;
            for (; ; )
            {
                random = rnd.Next(0, restoOp.Length - qtsOp);
                opcoesEmb[qtsOp] = opcoes[restoOp[random]];
                qtsOp++;

                if (restoOp[random] == 0)
                    this.opcaoCerta = qtsOp;

                if (qtsOp > 3)
                    break;

                for (int i = random; i < restoOp.Length - qtsOp; i++)
                    restoOp[i] = restoOp[i + 1];
            }

            opcoesEmb[4] = opcoes[4];
            opcoes = opcoesEmb;
        }

        protected void acabarQuiz(bool acertou)
        {
            tmrTempoQuiz.Enabled = false;

            if (this.tipoQuiz == 2)
            //quiz no final de cada level
            {
                int estrelas = this.vidas - this.qtdQuizesErrados;

                if (acertou)
                {
                    estrelas++;

                    if (this.levelAtual > 0)
                    {
                        //ganha moedas
                        this.ganharMoedasNoBD();
                        this.colocarMoedasNaTela();
                    }
                }

                if (estrelas < 0)
                    estrelas = 0;

                if (this.levelAtual > 0)
                {
                    //colocar estrelas no banco
                    this.colocarEstrelasNoBD(estrelas);

                    //colocar XP no banco
                    if (acertou)
                        this.ganharXPNoBD(2);
                    else
                        this.ganharXPNoBD(1);
                }

                if (this.etapa == 6)
                    this.lbGrande.Text = "VOCÊ ESTÁ PRONTO \nPARA JOGAR! \n";
                else
                    this.lbGrande.Text = "VOCÊ GANHOUUU!! \n \n";

                if(this.jogMultiplayer)
                {
                    MessageBox.Show("VOCÊ GANHOU DO SEU AMIGO!! Muito beem!");
                    SendData("acabou");

                    //enviar socket ao outro usuario dizendo que ele perdeu (enviar "fim")
                    //tem que mudar alguma coisa aqui no socket? (tipo a porta)
                    //fazer
                }

                this.lbGrande.Visible = true;
                this.tmrInimigos.Enabled = false;

                int qtd = estrelas;
                if (qtd == 4)
                    qtd = 3;

                this.picEstrelasGanhou = new PictureBox[qtd];
                for (int i = 0; i < qtd; i++)
                {
                    // pictureBox
                    this.picEstrelasGanhou[i] = new System.Windows.Forms.PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(this.picEstrelasGanhou[i])).BeginInit();
                    this.picEstrelasGanhou[i].BackColor = Color.White;

                    if (estrelas >= 4)
                        this.picEstrelasGanhou[i].Image = global::Jogo_Educativo.Properties.Resources.gold_star;
                    else
                        this.picEstrelasGanhou[i].Image = global::Jogo_Educativo.Properties.Resources.star_not_gold;

                    this.picEstrelasGanhou[i].Size = new System.Drawing.Size(50, 50);

                    if (i == 0)
                        this.picEstrelasGanhou[i].Location = new System.Drawing.Point(this.lbGrande.Location.X + this.lbGrande.Width - 3 * this.picEstrelasGanhou[i].Width - 50, this.lbGrande.Location.Y + 80);
                    else
                        this.picEstrelasGanhou[i].Location = new System.Drawing.Point(this.picEstrelasGanhou[i - 1].Location.X + this.picEstrelasGanhou[i - 1].Width + 5, this.picEstrelasGanhou[i - 1].Location.Y);

                    this.picEstrelasGanhou[i].TabStop = false;

                    this.picEstrelasGanhou[i].SizeMode = PictureBoxSizeMode.StretchImage;

                    this.picEstrelasGanhou[i].Visible = true;
                    this.picEstrelasGanhou[i].BringToFront();

                    this.picEstrelasGanhou[i].Invalidate();
                    this.picEstrelasGanhou[i].Update();
                    this.picEstrelasGanhou[i].Refresh();
                    this.Controls.Add(this.picEstrelasGanhou[i]);

                    this.picEstrelasGanhou[i].BringToFront();
                }

                this.aux = 5;
                this.tmrAux.Interval = 4000;
                this.tmrAux.Enabled = true;

                Application.DoEvents();
            }
            else if (this.tipoQuiz == 1)
            //quiz bonus
            {
                if (acertou && this.levelAtual > 0)
                {
                    //moedas
                    this.ganharMoedasNoBD();
                    this.colocarMoedasNaTela();
                }

                this.qlQuizBox = -1;
                this.picQuizBox.Visible = false;
                //esse obstaculo de quiz especial nao está disponivel mais
            }
            else
            //quiz normal
            {
                if (acertou)
                {
                    if (this.qtdQuizesErrados > 0)
                        this.qtdQuizesErrados = 0;

                    if (this.levelAtual > 0)
                        this.ganharXPNoBD(0);

                    this.picPoder.Location = new Point(this.picPoder.Location.X, this.picPers.Location.Y + 30);

                    this.viradoDireitaPoder = this.viradoDireita;

                    //jogar poder
                    this.tmrJogarPoder.Enabled = true;
                }
                else
                {
                    this.qtdQuizesErrados++;
                    this.colocarVidasQuizesErradosNaTela();
                }
            }

            if (this.tipoQuiz != 2)
                this.colocarXPTela();

            this.tipoQuiz = 0;
            this.qntTempoResta = tempoQuiz;

            this.tmrCarregarPoder.Interval /= qtsVezesMaisLento;
            this.tmrInimigos.Interval /= qtsVezesMaisLento;
            this.tmrPular.Interval /= qtsVezesMaisLento;
            this.tmrRevivendo.Interval /= qtsVezesMaisLento;
            this.tmrJogarPoder.Interval /= qtsVezesMaisLento;

            pnlQuiz.Visible = false;
            this.Focus();
            this.fazendoQuiz = false;

            //todos botoes com cor branca
            btnOpcao1.BackColor = Color.FromArgb(255, 250, 240);
            btnOpcao2.BackColor = Color.FromArgb(255, 250, 240);
            btnOpcao3.BackColor = Color.FromArgb(255, 250, 240);
            btnOpcao4.BackColor = Color.FromArgb(255, 250, 240);

            if (this.qtdQuizesErrados >= qtdQuizesErradosPerde)
                this.morrerRealmente();
        }

        protected void btnOpcao1_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt16(((Button)sender).Tag);

            tmrTempoQuiz.Enabled = false;

            if (tag == this.opcaoCerta)
            {
                ((Button)sender).BackColor = Color.LimeGreen;
                this.acabarQuiz(true);
            }
            else
            {
                ((Button)sender).BackColor = Color.DarkRed;
                this.acabarQuiz(false);
            }
        }

        protected void tmrTempoQuiz_Tick(object sender, EventArgs e)
        {
            this.qntTempoResta -= 100;

            if (this.qntTempoResta == 0)
                this.acabarQuiz(false);
            else
            {
                float tempoResta = ((float)this.qntTempoResta) / 1000;
                lbTempoQuiz.Text = fraseTempoRestante + tempoResta;
            }
        }


        //XP
        protected int[] xpNivel(int nxp)
        //nvl, xpNoNlv, xpTotlNvl
        {
            int nvl = 1;
            int xpTotalNvl = qtdXpTemOPrimeiroNivel;

            while (nxp >= xpTotalNvl)
            {
                nvl++;
                nxp -= xpTotalNvl;

                xpTotalNvl += qtdXpSomaACadaNivel;
            }

            int[] ret = new int[3];
            ret[0] = nvl;
            ret[1] = nxp;
            ret[2] = xpTotalNvl;
            return ret;
        }

        protected void colocarXPTela()
        {
            int[] nlvXp = this.xpNivel(this.xp);
            this.lbNivel.Text = "Nv" + nlvXp[0];
            this.prgXp.Maximum = nlvXp[2];
            this.prgXp.Value = nlvXp[1];
            this.lbXp.Text = nlvXp[1] + "/" + nlvXp[2];
        }


        //VISUAL JOGO
        protected void colocarMoedasNaTela()
        {
            string m = Convert.ToString(this.moedas);

            while (m.Length < 6)
                m = " " + m;

            this.lbMoedas.Text = m;
        }

        protected void colocarVidasQuizesErradosNaTela()
        {
            this.lbVidas.Text = "Vidas: " + this.vidas;
            lbQuizesErrados.Text = fraseQuizesErrados + this.qtdQuizesErrados + "/" + qtdQuizesErradosPerde;
        }


        //personagens
        protected void mostrarPersonagens()
        {
            this.etapa = 5;

            this.pegarPersonagensNoBD();

            this.lbTitulo.Location = new Point(150, 5);
            this.lbTitulo.Text = "PERSONAGENS";
            this.lbTitulo.Visible = true;

            this.colocarMoedasNaTela();
            this.lbMoedas.Location = new Point(706, 82);
            this.picMoedas.Location = new Point(814, 75);
            this.lbMoedas.Visible = true;
            this.picMoedas.Visible = true;

            this.lbNomePersonagens = new System.Windows.Forms.Label[this.precoPersonagens.Length];
            this.lbPrecoPersonagens = new System.Windows.Forms.Label[this.precoPersonagens.Length];
            this.imgPersonagens = new System.Windows.Forms.PictureBox[this.precoPersonagens.Length];

            for (int i = 0; i < this.precoPersonagens.Length; i++)
            {
                //label NOME
                this.lbNomePersonagens[i] = new System.Windows.Forms.Label();
                this.lbNomePersonagens[i].AutoSize = true;
                this.lbNomePersonagens[i].BackColor = System.Drawing.Color.Transparent;
                this.lbNomePersonagens[i].Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Underline);
                //this.loadFont();
                //this.AllocFont(font, this.lbNomePersonagens[i], 12);
                this.lbNomePersonagens[i].ForeColor = System.Drawing.SystemColors.HighlightText;
                this.lbNomePersonagens[i].ForeColor = System.Drawing.Color.Black;

                if (i == 0)
                    this.lbNomePersonagens[i].Location = new System.Drawing.Point(5, 140);
                else
                    this.lbNomePersonagens[i].Location = new System.Drawing.Point(this.imgPersonagens[i - 1].Location.X + this.imgPersonagens[i - 1].Width + 20, this.lbNomePersonagens[i - 1].Location.Y);

                this.lbNomePersonagens[i].Size = new System.Drawing.Size(122, 19);
                this.lbNomePersonagens[i].Cursor = System.Windows.Forms.Cursors.Hand;
                if(this.nomePersonagens[i].IndexOf("\n") == -1)
                    this.pularLinhaStr(ref this.nomePersonagens[i], 8);
                this.lbNomePersonagens[i].Text = this.nomePersonagens[i];
                this.lbNomePersonagens[i].Tag = i;

                this.lbNomePersonagens[i].Visible = true;

                this.lbNomePersonagens[i].Click += new System.EventHandler(this.clickPers_Click);

                this.lbNomePersonagens[i].Invalidate();
                this.lbNomePersonagens[i].Update();
                this.lbNomePersonagens[i].Refresh();
                this.Controls.Add(this.lbNomePersonagens[i]);


                //label PRECO
                this.lbPrecoPersonagens[i] = new System.Windows.Forms.Label();
                this.lbPrecoPersonagens[i].AutoSize = true;
                this.lbPrecoPersonagens[i].BackColor = System.Drawing.Color.Transparent;
                this.lbPrecoPersonagens[i].Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.lbPrecoPersonagens[i].Location = new System.Drawing.Point(this.lbNomePersonagens[i].Location.X + 10, 220);
               // this.loadFont();
                //this.AllocFont(font, this.lbPrecoPersonagens[i], 11);
                this.lbPrecoPersonagens[i].ForeColor = System.Drawing.SystemColors.HighlightText;
                this.lbPrecoPersonagens[i].ForeColor = System.Drawing.Color.Red;

                this.lbPrecoPersonagens[i].Cursor = System.Windows.Forms.Cursors.Hand;

                if (this.precoPersonagens[i] == 0)
                {
                    this.lbPrecoPersonagens[i].Text = "Obtido";
                    this.lbPrecoPersonagens[i].ForeColor = System.Drawing.Color.Lime;
                }
                else
                {
                    this.lbPrecoPersonagens[i].Text = "$" + Convert.ToString(this.precoPersonagens[i]) + ",00";
                    this.lbPrecoPersonagens[i].ForeColor = System.Drawing.Color.Red;
                }
                this.lbPrecoPersonagens[i].Tag = i;

                this.lbPrecoPersonagens[i].Visible = true;

                this.lbPrecoPersonagens[i].Click += new System.EventHandler(this.clickPers_Click);

                this.lbPrecoPersonagens[i].Invalidate();
                this.lbPrecoPersonagens[i].Update();
                this.lbPrecoPersonagens[i].Refresh();
                this.Controls.Add(this.lbPrecoPersonagens[i]);


                // imagem PERSONAGENS
                this.imgPersonagens[i] = new System.Windows.Forms.PictureBox();
                ((System.ComponentModel.ISupportInitialize)(this.imgPersonagens[i])).BeginInit();
                this.imgPersonagens[i].BackColor = System.Drawing.Color.Transparent;

                this.imgPersonagens[i].Image = this.personagens[i, 1];

                this.imgPersonagens[i].Location = new System.Drawing.Point(this.lbNomePersonagens[i].Location.X, this.lbPrecoPersonagens[i].Location.Y + lbPrecoPersonagens[i].Height + 35);

                this.imgPersonagens[i].Size = new System.Drawing.Size(128, 185);
                this.imgPersonagens[i].TabStop = true;
                this.imgPersonagens[i].Cursor = System.Windows.Forms.Cursors.Hand;
                this.imgPersonagens[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.imgPersonagens[i].Tag = i;
                this.imgPersonagens[i].Visible = true;

                this.imgPersonagens[i].Click += new System.EventHandler(this.clickPers_Click);

                this.imgPersonagens[i].Invalidate();
                this.imgPersonagens[i].Update();
                this.imgPersonagens[i].Refresh();
                this.Controls.Add(this.imgPersonagens[i]);
            }

            this.btnVoltar.Location = new Point(18, 18);
            this.btnVoltar.Visible = true;
        }

        protected void clickPers_Click(object sender, EventArgs e)
        {
            this.pegarMoedasXPNoBD();
            this.pegarPersonagensNoBD();

            int tag = this.tagPers(sender);

            if (this.precoPersonagens[tag] == 0)
            {
                this.personagem = (byte)(tag + 1);
                return;
            }

            if (this.precoPersonagens[tag] < this.moedas)
                this.moedas -= this.precoPersonagens[tag];
            else
            {
                MessageBox.Show("Você não tem dinheiro para a compra!");
                return;
            }

            //inserir personagem comprado
            this.inserirPersonagemComprado(tag + 1);

            this.personagem = (byte)tag;

            this.mostrarPersonagens();

            Application.DoEvents();
            MessageBox.Show("Compra realizada com sucesso!");
            Application.DoEvents();
        }

        protected int tagPers(object sender)
        {
            if (sender.GetType() == typeof(Label))
                return Convert.ToInt16(((Label)sender).Tag);
            else
                return Convert.ToInt16(((PictureBox)sender).Tag);
        }

        protected void desmostrarPersonagens()
        {
            this.lbTitulo.Visible = false;
            this.lbMoedas.Visible = false;
            this.picMoedas.Visible = false;

            for (int i = 0; i < this.precoPersonagens.Length; i++)
            {
                this.lbNomePersonagens[i].Visible = false;
                this.lbPrecoPersonagens[i].Visible = false;
                this.imgPersonagens[i].Visible = false;
            }

            this.lbNomePersonagens = null;
            this.lbPrecoPersonagens = null;
            this.imgPersonagens = null;
            this.btnVoltar.Visible = false;
        }

        protected void btnMostrarPersonagens_Click(object sender, EventArgs e)
        {
            if (this.etapa == 2)
            {
                this.antesPersEhMenu = false;
                this.desmostrarLeveis();
            }
            else
            {
                this.antesPersEhMenu = true;
                this.desmostrarHome();
            }

            this.mostrarPersonagens();
            Application.DoEvents();
        }


        //voltar e home
        protected void btnHome_Click(object sender, EventArgs e)
        {
            this.pararJogo();
            this.aux = 9;
            this.tmrAux.Interval = this.tmrInimigos.Interval + 10;
            this.tmrAux.Enabled = true;
            Application.DoEvents();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            switch (this.etapa)
            {
                case 2:
                    this.desmostrarLeveis();
                    this.mostrarHome(false);
                    Application.DoEvents();
                    break;
                case 3:
                    this.desmostrarExplicacao(true);
                    this.mostrarBtnLeveis();
                    Application.DoEvents();
                    break;
                case 5:
                    this.desmostrarPersonagens();

                    if (this.antesPersEhMenu)
                        this.mostrarHome(false);
                    else
                        this.mostrarBtnLeveis();
                    break;
                case 4:
                case 6:
                    this.pararJogo();
                    this.aux = 8;
                    this.tmrAux.Interval = this.tmrInimigos.Interval + 10;
                    this.tmrAux.Enabled = true;
                    break;
                case 7:
                    this.desmostrarCreditos();

                    for (int i = 0; i < this.qtdInim; i++)
                        this.imgInimigos[i].Visible = true;
                    for (int i = 0; i < this.qtdObst; i++)
                        this.imgObstaculos[i].Visible = true;
                    this.lbVidas.Visible = true;
                    this.lbQuizesErrados.Visible = true;
                    this.lbPoder.Visible = true;
                    this.prgPoder.Visible = true;
                    this.picMoedas.Visible = true;
                    this.lbMoedas.Visible = true;
                    this.picBtnVoltar.Visible = true;
                    this.lbXp.Visible = true;
                    this.lbIndicarXP.Visible = true;
                    this.lbNivel.Visible = true;
                    this.prgXp.Visible = true;
                    this.picQuizBox.Visible = true;
                    this.btnVoltar.Enabled = false;

                    this.btnVoltar.Location = new Point(18, 118);

                    this.picExplicacao.Visible = true;
                    this.lbExplicacao.Visible = true;
                    this.picPers.Visible = true;

                    this.btnFrente.Visible = true;

                    if (this.qlExplic > 0)
                    {
                        if (this.qlExplic != 0)
                            this.btnTras.Visible = true;
                    }
                    else
                        if (this.qlContext != 0)
                            this.btnTras.Visible = true;

                    this.lbCreditos.Visible = true;
                    this.etapa = 6;

                    break;
                case 8:
                    this.desmostrarRanking();
                    this.mostrarHome(false);
                    Application.DoEvents();
                    break;
                case 9:
                    this.colocarUsOnlineNoBD(false);
                    this.desmostrarUsuariosDispon();
                    this.mostrarBtnLeveis();
                    Application.DoEvents();
                    break;
                default:
                    MessageBox.Show("DEU RUIM: etapa" + Convert.ToString(this.etapa));
                    break;
            }

            Application.DoEvents();
        }

        protected void mostrarHome(bool tocarMusica)
        {
            this.etapa = 1;

            this.lbNomeUsuario.Text = this.nomeUsuario;
            this.lbNomeUsuario.Location = new Point((this.Width - this.lbNomeUsuario.Width) / 2, this.lbNomeUsuario.Location.Y);
            this.lbNomeUsuario.Visible = true;
            this.lbNomeUsuario.SendToBack();

            //xp
            this.colocarXPTela();
            this.lbIndicarXP.Location = new Point(this.lbNomeUsuario.Location.X + this.lbNomeUsuario.Width + 20, 156);
            this.lbNivel.Location = new Point(this.lbNomeUsuario.Location.X + this.lbNomeUsuario.Width - 20, 136);
            this.prgXp.Location = new Point(this.lbIndicarXP.Location.X - 150, 143);
            this.lbXp.Location = new Point(this.prgXp.Location.X + 50, 176);
            this.lbXp.Visible = true;
            this.lbIndicarXP.Visible = true;
            this.lbNivel.Visible = true;
            this.prgXp.Visible = true;

            this.btnMostrarLeveis.Visible = true;
            this.btnExplicacaoJogo.Visible = true;
            this.btnMostrarPersonagens.Visible = true;
            this.btnRanking.Visible = true;
            this.btnLogout.Visible = true;
            this.btnVoltar.Enabled = true;

            if(tocarMusica)
                this.tocarMusicMenu();

            this.btnMostrarLeveis.Focus();
        }

        protected void tocarMusicMenu()
        {
            try
            {
                this.music.Stop();
            }
            catch (Exception e) { }

            this.music = new System.Media.SoundPlayer();
            this.music.Stream = Properties.Resources.MENU_Magicka;
            this.music.PlayLooping();
        }

        protected void desmostrarHome()
        {
            this.etapa = 0;

            this.lbNomeUsuario.Visible = false;

            //xp
            this.lbXp.Visible = false;
            this.lbIndicarXP.Visible = false;
            this.lbNivel.Visible = false;
            this.prgXp.Visible = false;

            //this.BackgroundImage
            this.btnMostrarLeveis.Visible = false;
            this.btnExplicacaoJogo.Visible = false;
            this.btnMostrarPersonagens.Visible = false;
            this.btnRanking.Visible = false;
            this.btnLogout.Visible = false;
        }

        protected void btnMostrarLeveis_Click(object sender, EventArgs e)
        {
            this.desmostrarHome();
            this.mostrarBtnLeveis();
            Application.DoEvents();
        }

        protected void btnExplicacaoJogo_Click(object sender, EventArgs e)
        {
            this.desmostrarHome();
            this.levelAtual = 0;
            this.etapa = 6;
            this.iniciarLevel();
            this.comecarExplicacao();
            Application.DoEvents();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this.insertUsDispon(false);
            //sock_Servidor.Close();
            this.music.Stop();
            this.desmostrarHome();
            this.mostrarLogin();
            Application.DoEvents();
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            this.desmostrarHome();
            this.ranking(true);
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.insertUsDispon(false);
            //sock_Servidor.Close();
            splash.fechar();
            try
            {
                this.music.Stop();
            }catch(Exception err) { }
        }


        //ranking
        protected void mostrarRanking()
        {
            this.ranking(true);
        }

        protected void desmostrarRanking()
        {
            this.etapa = 0;

            this.lbTitulo.Visible = false;
            this.btnVoltar.Visible = false;

            for (int i = 0; i < this.numeracaoRanking.Length; i++)
            {
                this.numeracaoRanking[i].Visible = false;
                this.nomeRanking[i].Visible = false;
                this.lvRanking[i].Visible = false;
            }
        }


        //LOGIN e CADASTRO
        public void mostrarLogin()
        {
            this.lbSenha.Location = new Point(this.lbSenha.Location.X, 228);
            this.txtSenha.Location = new Point(this.txtUsuario.Location.X, 228);
            this.btnVerSenha.Location = new Point(740, this.txtSenha.Location.Y + 3);
            this.btnLogarCadastrar.Text = "Login";
            this.lbTitulo.Location = new Point(320, 5);
            this.lbTitulo.Text = "Login";
            this.lbMudarLoginCadastro.Text = "Não tem uma conta? Cadastre-se aqui.";

            this.lbTitulo.Visible = true;
            this.lbUsuario.Visible = true;
            this.txtUsuario.Visible = true;
            this.lbSenha.Visible = true;
            this.txtSenha.Visible = true;
            this.btnVerSenha.Visible = true;
            this.btnLogarCadastrar.Visible = true;
            this.lbMudarLoginCadastro.Visible = true;
            this.lbJogarAnonimamente.Visible = true;

            this.txtUsuario.Focus();
        }

        protected void mostrarCadastro()
        {
            this.lbSenha.Location = new Point(this.lbSenha.Location.X, 181);
            this.txtSenha.Location = new Point(this.txtUsuario.Location.X, 181);
            this.btnVerSenha.Location = new Point(740, this.txtSenha.Location.Y + 3);
            this.btnLogarCadastrar.Text = "Cadastrar";
            this.lbTitulo.Location = new Point(200, 5);
            this.lbTitulo.Text = "Cadastro";
            this.lbMudarLoginCadastro.Text = "Já tem uma conta? Faça login clicando aqui.";

            this.lbTitulo.Visible = true;
            this.lbUsuario.Visible = true;
            this.txtUsuario.Visible = true;
            this.lbSenha.Visible = true;
            this.txtSenha.Visible = true;
            this.btnVerSenha.Visible = true;
            this.lbSenha2.Visible = true;
            this.txtSenha2.Visible = true;
            this.btnVerSenha2.Visible = true;
            this.btnLogarCadastrar.Visible = true;
            this.lbMudarLoginCadastro.Visible = true;
            this.lbJogarAnonimamente.Visible = true;

            this.txtUsuario.Focus();
        }

        protected void desmostrarLogin()
        {
            this.lbJogarAnonimamente.Visible = false;
            this.lbTitulo.Visible = false;
            this.lbUsuario.Visible = false;
            this.txtUsuario.Visible = false;
            this.lbSenha.Visible = false;
            this.txtSenha.Visible = false;
            this.btnVerSenha.Visible = false;
            this.btnLogarCadastrar.Visible = false;
            this.lbMudarLoginCadastro.Visible = false;
        }

        protected void desmostrarCadastro()
        {
            this.lbJogarAnonimamente.Visible = false;
            this.lbTitulo.Visible = false;
            this.lbUsuario.Visible = false;
            this.txtUsuario.Visible = false;
            this.lbSenha.Visible = false;
            this.txtSenha.Visible = false;
            this.btnVerSenha.Visible = false;
            this.lbSenha2.Visible = false;
            this.txtSenha2.Visible = false;
            this.btnVerSenha2.Visible = false;
            this.btnLogarCadastrar.Visible = false;
            this.lbMudarLoginCadastro.Visible = false;
        }

        protected void lbMudarLoginCadastro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.lbSenha2.Visible)
            {
                this.desmostrarCadastro();
                this.mostrarLogin();
            }
            else
            {
                this.desmostrarLogin();
                this.mostrarCadastro();
            }

            Application.DoEvents();
        }

        protected void logar()
        {
            bool cadastrou = this.lbSenha2.Visible;

            if (cadastrou)
                this.desmostrarCadastro();
            else
                this.desmostrarLogin();

            if (!this.ehAnonimo)
            {
                this.nomeUsuario = this.txtUsuario.Text.Trim().ToUpper();

                //moedas e xp
                this.pegarMoedasXPNoBD();
            }else
            {
                this.moedas = 0;
                this.xp = 0;
            }

            this.insertUsDispon(true);

            if (cadastrou)
                this.btnExplicacaoJogo_Click(null, null);
            else
                this.mostrarHome(true);
        }

        protected void btnLogarCadastrar_Click(object sender, EventArgs e)
        {
            if (this.lbSenha2.Visible)
                this.cadastrarUsuario();
            else
                this.logarUsuario();
        }

        protected void btnVerSenha_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtSenha.PasswordChar = '\0';
        }

        protected void btnVerSenha2_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtSenha2.PasswordChar = '\0';
        }

        protected void btnVerSenha_MouseUp(object sender, MouseEventArgs e)
        {
            this.txtSenha.PasswordChar = '*';
        }

        protected void btnVerSenha2_MouseUp(object sender, MouseEventArgs e)
        {
            this.txtSenha2.PasswordChar = '*';
        }

        protected void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ehAnonimo = true;
            this.nomeUsuario = "Anonymous";
            this.logar();
        }


        //BANCO DE DADOS
        public void iniciarConexao()
        {
            this.con = new SqlConnection();
            this.connStr = this.connStr.Substring(this.connStr.IndexOf("Data Source"));
            this.con.ConnectionString = this.connStr;
            this.con.Open();
        }

        public Image getPersonagem(int iPers, int lado)
        {
            return this.personagens[iPers - 1, lado];
        }

        protected void pegarMoedasXPNoBD()
        //no comeco
        //quando for ganhar moeda
        //quando for ganhar xp
        //(nao vale a pena buscar soh moedas ou soh xp)
        {
            if (this.ehAnonimo)
                return;

            string cmd_s = "Select moedas, xp from usuario where nome=@nome";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            if (ds.Tables[0].Rows.Count != 1)
                MessageBox.Show("Quantidade de linhas de um registro != de 1. Rows: " + ds.Tables[0].Rows.Count);
            this.moedas = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
            this.xp = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[1]);
        }

        protected static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public void pegarPersonagensNoBD()
        //a primeira vez pega tudo
        //cada vez que entra nos personagens (para dificultar o adulteramento de informacao)
        {
            if (this.personagens == null)
            {
                SqlCommand cmdSelect = new SqlCommand(
                    "Select nome, preco, imagem0, imagem1, imagem2, imagem3 from personagem", this.con);

                SqlDataAdapter adapt = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();

                adapt.Fill(ds);

                int nPers = ds.Tables[0].Rows.Count;
                this.nomePersonagens = new String[nPers];
                this.precoPersonagens = new int[nPers];
                this.personagens = new Image[nPers, 4];
                //0:morto direita, 1:direita, 2:esquerda, 3:morto esquerda

                //SE O USUÁRIO JÁ COMPROU O PERSONAGEM, this.precoPersonagens[i]=0

                for (int i = 0; i < nPers; i++)
                {
                    this.nomePersonagens[i] = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    this.precoPersonagens[i] = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]);

                    byte[] vetorImagem;

                    for (int i2 = 0; i2 < 4; i2++)
                    {
                        vetorImagem = (byte[])ds.Tables[0].Rows[i].ItemArray[i2 + 2];
                        this.personagens[i, i2] = ByteToImage(vetorImagem);
                    }
                }

                GC.Collect();  // garbage collector (limpeza)
                cmdSelect.Dispose();
                cmdSelect = null;
            }
            else
            {
                SqlCommand cmdSelect = new SqlCommand(
                    "Select preco from personagem", this.con);

                SqlDataAdapter adapt = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();

                adapt.Fill(ds);

                int nPers = ds.Tables[0].Rows.Count;
                this.precoPersonagens = new int[nPers];
                //0:morto direita, 1:direita, 2:esquerda, 3:morto esquerda

                //SE O USUÁRIO JÁ COMPROU O PERSONAGEM, this.precoPersonagens[i]=0

                for (int i = 0; i < nPers; i++)
                {
                    this.precoPersonagens[i] = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                }

                GC.Collect();  // garbage collector (limpeza)
                cmdSelect.Dispose();
                cmdSelect = null;
            }

        }

        protected void ganharMoedasNoBD()
        //quando acerta o quiz final ou especial
        {
            this.pegarMoedasXPNoBD();

            if (this.tipoQuiz == 2)
                this.moedas += qtdMoedasQuizFinal;
            else
                this.moedas += qtdMoedasQuizBox;

            if (this.ehAnonimo)
                return;

            //colocar no banco essas moedas
            string cmd_s = "update usuario set moedas=@moedas where nome=@nome";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);
            cmd.Parameters.AddWithValue("@moedas", this.moedas);

            int iResultado = cmd.ExecuteNonQuery();

            if (iResultado <= 0)
                MessageBox.Show("BANCO NÃO CONSEGUIU FAZER O UPDATE DE MOEDAS");
        }

        protected void ganharXPNoBD(byte ql)
        //0: acertou quiz normal
        //1: ganhou level e nao acertou quiz
        //2: ganhou level e acertou quiz
        {
            this.pegarMoedasXPNoBD();

            if (ql == 1)
                this.xp += qntXpGanhaPorQuizNormal;
            else
            {
                if (ql == 2)
                    this.xp += qntXpGanhaPorQuiz;
                else
                    this.xp += qntXpGanhaPorLevel;
            }

            if (this.ehAnonimo)
                return;

            //colocar no XP essas moedas
            string cmd_s = "update usuario set xp=@xp where nome=@nome";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);
            cmd.Parameters.AddWithValue("@xp", this.xp);

            int iResultado = cmd.ExecuteNonQuery();

            if (iResultado <= 0)
                MessageBox.Show("BANCO NÃO CONSEGUIU FAZER O UPDATE DE XP");
        }

        protected void pegarLeveisNoBD()
        //quando vai exibir os botoes
        {
            string cmd_s;
            SqlCommand cmd;
            SqlDataAdapter adapt;
            DataSet ds;

            //pegar todos os leveis no BD
            if (this.leveis == null)
            {
                cmd_s = "Select nome from level";
                cmd = new SqlCommand(cmd_s, this.con);

                adapt = new SqlDataAdapter(cmd);
                ds = new DataSet();

                adapt.Fill(ds);

                if (ds.Tables[0].Rows.Count < 1)
                    MessageBox.Show("Nenhum level existente");


                //consulta pra saber quantos leveis e nomes
                ///vetor com nomes (codLv = i+1)
                this.qtsLv = (byte)(ds.Tables[0].Rows.Count - 1);

                this.leveis = new string[this.qtsLv];

                //o i=0 eh a explicacao do jogo em si
                for (int i = 1; i < this.qtsLv + 1; i++)
                    this.leveis[i - 1] = ds.Tables[0].Rows[i].ItemArray[0].ToString();
            }

            if (this.ehAnonimo)
            {
                this.estrelas = new byte[this.qtsLv];

                return;
            }
                

            //pegar no BD quais leveis o usuario completou e com quantas estrelas
            ///vetor com quantas estrelas (4:ouro) (lv = i+1)
            cmd_s = "Select estrelas from levelUs where nomeUs=@nome";
            cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);

            adapt = new SqlDataAdapter(cmd);
            ds = new DataSet();

            adapt.Fill(ds);

            this.estrelas = new byte[this.qtsLv];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                this.estrelas[i] = Convert.ToByte(ds.Tables[0].Rows[i].ItemArray[0]);
        }

        protected void pegarExplicacaoNoBD()
        //quando for mostrar a (explicacao e contextualizacao)
        {
            //level 0 eh a explicacao do Jogo

            string cmd_s;
            SqlCommand cmd;
            SqlDataAdapter adapt;
            DataSet ds;

            //CONTEXTUALIZACAO
            cmd_s = "Select texto from explicacao where codLv=@lv and explicacao=0 order by nPasso";
            cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@lv", this.levelAtual);

            adapt = new SqlDataAdapter(cmd);
            ds = new DataSet();

            adapt.Fill(ds);

            this.contextualizacao = new String[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                this.contextualizacao[i] = ds.Tables[0].Rows[i].ItemArray[0].ToString();

            //EXPLICACAO
            cmd_s = "Select texto from explicacao where codLv=@lv and explicacao=1 order by nPasso";
            cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@lv", this.levelAtual);

            adapt = new SqlDataAdapter(cmd);
            ds = new DataSet();

            adapt.Fill(ds);

            this.explicacao = new String[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                this.explicacao[i] = ds.Tables[0].Rows[i].ItemArray[0].ToString();


            //geral
            this.qlContext = 0;
            this.qlExplic = -1;
            this.qtdCaracAtual = 0;
        }

        protected string[] pegarOpcoesQuizNoBD()
        //quando for mostrar um quiz
        {
            Random rnd = new Random();
            int ql = rnd.Next(1, this.qtdQuizesPorLv[this.levelAtual] + 1);

            string cmd_s = "Select opcao1, opcao2, opcao3, opcao4, pergunta from quiz where level=@lv and codQuiz=@qlQuiz";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@lv", this.levelAtual);
            cmd.Parameters.AddWithValue("@qlQuiz", ql);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            string[] opcoes = new string[5];
            opcoes[0] = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            opcoes[1] = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            opcoes[2] = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            opcoes[3] = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            opcoes[4] = ds.Tables[0].Rows[0].ItemArray[4].ToString();

            return opcoes;
        }

        public void quantosQuizesPorLv()
        //na inicializacao do form
        {
            string cmd_s = "Select count(*) from quiz group by level order by level";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            this.qtdQuizesPorLv = new byte[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                this.qtdQuizesPorLv[i] = Convert.ToByte(ds.Tables[0].Rows[i].ItemArray[0]);
        }

        protected void colocarEstrelasNoBD(int estrelasAtual)
        //quando completar level
        {
            this.pegarLeveisNoBD();

            this.estrelas[this.levelAtual] = (byte)estrelasAtual;

            if (this.ehAnonimo)
                return;

            //se ainda nao 
            if (this.estrelas[this.levelAtual - 1] == 0)
            {
                //colocar estrelas no banco
                //this.estrelas[this.levelAtual];
                string cmd_s = "insert into levelUs values(@nome, @lv, @estrelas)";
                SqlCommand cmd = new SqlCommand(cmd_s, this.con);
                cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);
                cmd.Parameters.AddWithValue("@lv", this.levelAtual);
                cmd.Parameters.AddWithValue("@estrelas", estrelasAtual);

                int iResultado = cmd.ExecuteNonQuery();

                if (iResultado <= 0)
                    MessageBox.Show("BANCO NÃO CONSEGUIU FAZER O INSERT DE ESTRELAS");
            }
            else
                if (estrelasAtual > this.estrelas[this.levelAtual - 1])
            {
                //colocar estrelas no banco
                //this.estrelas[this.levelAtual];
                string cmd_s = "update levelUs set estrelas=@estrelas where nomeUs=@nome and codLv=@lv";
                SqlCommand cmd = new SqlCommand(cmd_s, this.con);
                cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);
                cmd.Parameters.AddWithValue("@lv", this.levelAtual);
                cmd.Parameters.AddWithValue("@estrelas", estrelasAtual);

                int iResultado = cmd.ExecuteNonQuery();

                if (iResultado <= 0)
                    MessageBox.Show("BANCO NÃO CONSEGUIU FAZER O UPDATE DE ESTRELAS");
            }
        }

        protected void inserirPersonagemComprado(int codPers)
        //quando comprar personagem
        {
            if (this.ehAnonimo)
            {
                this.precoPersonagens[codPers - 1] = 0;
                return;
            }

            //insert into personagemComprado ...
            //colocar estrelas no banco
            //this.estrelas[this.levelAtual];
            string cmd_s = "insert into personagemComprado values(@nome, @codPers)";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);
            cmd.Parameters.AddWithValue("@codPers", codPers);

            int iResultado = cmd.ExecuteNonQuery();

            if (iResultado <= 0)
                MessageBox.Show("BANCO NÃO CONSEGUIU FAZER O INSERT DE PERSONAGEM COMPRADO");
        }

        protected void cadastrarUsuario()
        {
            string usuario = this.txtUsuario.Text.Trim().ToUpper();

            if (usuario == "Anonymous")
            {
                MessageBox.Show("Usuário inválido!");
                return;
            }

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(this.txtSenha.Text) || string.IsNullOrEmpty(this.txtSenha2.Text))
            {
                MessageBox.Show("Preencha todos os campos!");
                return;
            }

            if (this.nomeUsuarioJahExiste(usuario))
            {
                this.nomeUsuarioSemNumeros(ref usuario);

                if (this.nomeUsuarioJahExiste(usuario))
                {
                    int i = 0;
                    while (true)
                    {
                        i++;
                        if (!this.nomeUsuarioJahExiste(usuario + i))
                            break;
                    }

                    MessageBox.Show("Esse usuário já existe!\nTente " + usuario + i);
                    this.txtUsuario.Text = usuario + i;
                }
                else
                {
                    MessageBox.Show("Esse usuário já existe!\nTente " + usuario);
                    this.txtUsuario.Text = usuario;
                }

                return;
            }

            if (this.txtSenha.Text != this.txtSenha2.Text)
            {
                MessageBox.Show("As senhas não se correspondem!");
                return;
            }

            string cmd_s = "insert into usuario values(@nome, @senha, @xp, @moedas)";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", usuario);
            string senha = Criptografia.Codificar(this.txtSenha.Text);//, Convert.ToString(this.txtSenha.Text.GetHashCode()));
            cmd.Parameters.AddWithValue("@senha", senha);
            cmd.Parameters.AddWithValue("@xp", 0);
            cmd.Parameters.AddWithValue("@moedas", 0);

            int iResultado = cmd.ExecuteNonQuery();

            this.txtUsuario.Text = usuario;

            if (iResultado <= 0)
                MessageBox.Show("BANCO NÃO CONSEGUIU FAZER O CADASTRO");
            else
                this.logar();
        }

        protected void logarUsuario()
        {
            string usuario = this.txtUsuario.Text.Trim().ToUpper();

            if (usuario == "Anonymous")
            {
                MessageBox.Show("Usuário inválido!");
                return;
            }


            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(this.txtSenha.Text))
            {
                MessageBox.Show("Preencha todos os campos!");
                return;
            }

            string cmd_s = "select senha from usuario where nome=@nome";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", usuario);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("Esse usuário não existe!");
                return;
            }

            string senhaNoBanco = Criptografia.Decodificar(ds.Tables[0].Rows[0].ItemArray[0].ToString());//, Convert.ToString(this.txtSenha.Text));

            if (senhaNoBanco == this.txtSenha.Text)
                this.logar();
            else
                MessageBox.Show("Senha errada! Digite novamente...");
        }

        protected void nomeUsuarioSemNumeros(ref string nome)
        {
            for (; ; )
            {
                if (int.TryParse(Convert.ToString(nome[nome.Length - 1]), out int x))
                    nome = nome.Substring(0, nome.Length - 1);
                else
                    break;
            }
        }

        protected bool nomeUsuarioJahExiste(string nome)
        {
            string cmd_s = "select nome from usuario where nome=@nome";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@nome", nome);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            return ds.Tables[0].Rows.Count > 0;
        }

        protected void ranking(bool porXp)
        {
            this.etapa = 8;

            string cmd_s;
            if (porXp)
                cmd_s = "select top 10 nome, xp, moedas from usuario where xp<>0 order by xp desc, moedas desc";
            else
                cmd_s = "select top 10 nome, xp, moedas from usuario where moedas <>0 order by moedas desc, xp desc";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            this.lbTitulo.Location = new Point(320, 5);
            this.lbTitulo.Text = "RANKING";
            this.lbTitulo.Visible = true;

            this.numeracaoRanking = new Label[ds.Tables[0].Rows.Count];
            this.nomeRanking = new Label[ds.Tables[0].Rows.Count];
            this.lvRanking = new Label[ds.Tables[0].Rows.Count];

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                //i+1
                this.numeracaoRanking[i] = new System.Windows.Forms.Label();
                this.numeracaoRanking[i].AutoSize = true;
                this.numeracaoRanking[i].BackColor = System.Drawing.Color.Transparent;
                this.numeracaoRanking[i].Font = new System.Drawing.Font("Century Gothic", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numeracaoRanking[i].ForeColor = System.Drawing.Color.Blue;
                if (i == 0)
                    this.numeracaoRanking[i].Location = new System.Drawing.Point(185, 79);
                else
                    this.numeracaoRanking[i].Location = new System.Drawing.Point(this.numeracaoRanking[i - 1].Location.X, this.numeracaoRanking[i - 1].Location.Y + this.numeracaoRanking[i - 1].Height);

                this.numeracaoRanking[i].Text = i + 1 + " - ";

                this.numeracaoRanking[i].Visible = true;

                this.numeracaoRanking[i].Invalidate();
                this.numeracaoRanking[i].Update();
                this.numeracaoRanking[i].Refresh();
                this.Controls.Add(this.numeracaoRanking[i]);

                //nome
                this.nomeRanking[i] = new System.Windows.Forms.Label();
                this.nomeRanking[i].AutoSize = true;
                this.nomeRanking[i].BackColor = System.Drawing.Color.Transparent;
                this.nomeRanking[i].Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.nomeRanking[i].ForeColor = System.Drawing.Color.DarkOrange;
                this.nomeRanking[i].Location = new System.Drawing.Point(this.numeracaoRanking[i].Location.X + this.numeracaoRanking[i].Width + 15, this.numeracaoRanking[i].Location.Y + 5);
               // loadFont();
               // this.AllocFont(font, this.nomeRanking[i], 14);

                this.nomeRanking[i].Text = ds.Tables[0].Rows[i].ItemArray[0].ToString();

                this.nomeRanking[i].Visible = true;

                this.nomeRanking[i].Invalidate();
                this.nomeRanking[i].Update();
                this.nomeRanking[i].Refresh();
                this.Controls.Add(this.nomeRanking[i]);

                //XP- ds.Tables[0].Rows[i].ItemArray[1] moedas ou xp
                this.lvRanking[i] = new System.Windows.Forms.Label();
                this.lvRanking[i].AutoSize = true;
                this.lvRanking[i].BackColor = System.Drawing.Color.Transparent;
                this.lvRanking[i].Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lvRanking[i].ForeColor = System.Drawing.SystemColors.HighlightText;
                this.lvRanking[i].ForeColor = System.Drawing.Color.Black;
              //  loadFont();
              //  this.AllocFont(font, this.lvRanking[i], 14);
                this.lvRanking[i].Location = new System.Drawing.Point(this.nomeRanking[i].Location.X + this.nomeRanking[i].Width + 10, this.numeracaoRanking[i].Location.Y + 12);

                int[] aux = this.xpNivel(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]));
                this.lvRanking[i].Text = "Level " + aux[0] + "   " + aux[1] + "/" + aux[2];

                this.lvRanking[i].Visible = true;

                this.lvRanking[i].Invalidate();
                this.lvRanking[i].Update();
                this.lvRanking[i].Refresh();
                this.Controls.Add(this.lvRanking[i]);
            }

            this.btnVoltar.Location = new Point(18, 18);
            this.btnVoltar.Visible = true;
        }

        protected void colocarUsOnlineNoBD(bool online)
        {
            string cmd_s = "update usDisponJogarContra set online = @on where porta = @p and ip = @ip";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            if (online)
                cmd.Parameters.AddWithValue("@on", 1);
            else
                cmd.Parameters.AddWithValue("@on", 0);
            cmd.Parameters.AddWithValue("@p", this.porta);
            cmd.Parameters.AddWithValue("@ip", ipAddress.ToString());

            int iResultado = cmd.ExecuteNonQuery();
            if (iResultado <= 0)
                MessageBox.Show("ERRO ao deixar usuario disponivel ou não disponivel!");
        }

        protected void insertUsDispon(bool inserir)
        {
            if (inserir)
            {
                string cmd_s = "insert into usDisponJogarContra values(@porta, @ip, @nome, @online)";
                SqlCommand cmd = new SqlCommand(cmd_s, this.con);
                cmd.Parameters.AddWithValue("@porta", this.porta);
                cmd.Parameters.AddWithValue("@ip", ipAddress.ToString());
                cmd.Parameters.AddWithValue("@nome", this.nomeUsuario);
                cmd.Parameters.AddWithValue("@online", 0);

                int iResultado = cmd.ExecuteNonQuery();
                if (iResultado <= 0)
                    MessageBox.Show("ERRO ao deixar usuario disponivel!");
            }
            else
            {
                try
                {
                    string cmd_s = "delete from usDisponJogarContra where porta = @porta and ip = @ip";
                    SqlCommand cmd = new SqlCommand(cmd_s, this.con);
                    cmd.Parameters.AddWithValue("@porta", this.porta);
                    cmd.Parameters.AddWithValue("@ip", ipAddress.ToString());

                    int iResultado = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                { } //se metodo vai deixar o usuario indisponivel mas ele jah esta
            }
        }

        protected void usuariosDisponiveis()
        {
            string cmd_s = "select u.nome, ud.porta, ud.ip, u.xp from"
                + " usuario as u,"
                + " usDisponJogarContra as ud"
                + " where"
                + " u.nome = ud.nome and"
                + " ud.online = 1 and"
                + " (ud.ip <> @ip or"
                + " ud.porta <> @porta)";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@ip", this.ipAddress.ToString());
            cmd.Parameters.AddWithValue("@porta", this.porta);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            this.nomesIpUsDispon = new string[ds.Tables[0].Rows.Count, 2];
            this.portasUsDispon = new int[ds.Tables[0].Rows.Count];
            this.lvsUsDispon = new int[ds.Tables[0].Rows.Count];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.nomesIpUsDispon[i, 0] = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                this.nomesIpUsDispon[i, 1] = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                this.portasUsDispon[i] = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]);

                int xp = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
                this.lvsUsDispon[i] = this.xpNivel(xp)[0];
            }
        }

        protected int xpUsuario(string us)
        {
            string cmd_s = "select xp from usuario where nome = @n";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@n", us);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
        }

        protected bool usDispon(string ip, int p)
        {
            string cmd_s = "select nome from usDisponJogarContra where ip = @ip and porta = @p";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@p", p);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            return ds.Tables[0].Rows.Count > 0;
        }

        protected bool ipJahDispon(string ip)
        {
            string cmd_s = "select nome from usDisponJogarContra where ip = @ip";
            SqlCommand cmd = new SqlCommand(cmd_s, this.con);
            cmd.Parameters.AddWithValue("@ip", ip);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapt.Fill(ds);

            return ds.Tables[0].Rows.Count > 0;
        }

        private void txtSenha2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.btnLogarCadastrar.PerformClick();
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !this.txtSenha2.Visible)
            {
                this.btnLogarCadastrar.PerformClick();
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            loadFont();
            AllocFont(font, this.lbNomeUsuario, 54);
            AllocFont(font, this.lbUsuario, 20);
            AllocFont(font, this.lbSenha, 20);
            AllocFont(font, this.lbSenha2, 20);
            AllocFont(font, this.lbVidas, 10);
            AllocFont(font, this.lbQuizesErrados, 10);
            AllocFont(font, this.lbTitulo, 44);
            AllocFont(font, this.lbPoder, 15);
            AllocFont(font, this.lbIndicarXP, 12);
            AllocFont(font, this.lbNivel, 15);
            AllocFont(font, this.lbXp, 8);
        }

        private void picLoading_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }


    //classe que deixa botoes redondos
    public class RoundedButton : Button
    {
        protected const int qntRedondo = 15;

        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();

            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath GraphPath = GetRoundPath(Rect, qntRedondo);

            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
            }
        }
    }

    public class RoundedPictureBox : PictureBox
    {
        protected const int qntRedondo = 15;

        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();

            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath GraphPath = GetRoundPath(Rect, qntRedondo);

            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
            }
        }
    }

}

//MANUAL:
//maneiras de perder:
// - perder X vezes
// - errar X quizes seguidos
//
//ganhar
// - chegar no final tendo matado todos os inimigos (jogar poder: apertar espaco e acertar o quiz)
//
//bonus
// - existem caixas bonus que se voce pega, responde um quiz e se acertar ganha XP (se errar nao perde nada)