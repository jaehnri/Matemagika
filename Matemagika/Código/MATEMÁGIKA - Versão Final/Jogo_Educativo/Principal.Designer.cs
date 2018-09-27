namespace Jogo_Educativo
{
    partial class Principal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrPular = new System.Windows.Forms.Timer(this.components);
            this.tmrInimigos = new System.Windows.Forms.Timer(this.components);
            this.lbVidas = new System.Windows.Forms.Label();
            this.prgPoder = new System.Windows.Forms.ProgressBar();
            this.lbPoder = new System.Windows.Forms.Label();
            this.tmrRevivendo = new System.Windows.Forms.Timer(this.components);
            this.tmrCarregarPoder = new System.Windows.Forms.Timer(this.components);
            this.pnlQuiz = new System.Windows.Forms.Panel();
            this.lbTempoQuiz = new System.Windows.Forms.Label();
            this.btnOpcao4 = new Jogo_Educativo.RoundedButton();
            this.btnOpcao3 = new Jogo_Educativo.RoundedButton();
            this.btnOpcao2 = new Jogo_Educativo.RoundedButton();
            this.btnOpcao1 = new Jogo_Educativo.RoundedButton();
            this.lbPergunta = new System.Windows.Forms.Label();
            this.prgXp = new System.Windows.Forms.ProgressBar();
            this.lbIndicarXP = new System.Windows.Forms.Label();
            this.lbNivel = new System.Windows.Forms.Label();
            this.tmrTempoQuiz = new System.Windows.Forms.Timer(this.components);
            this.lbQuizesErrados = new System.Windows.Forms.Label();
            this.lbXp = new System.Windows.Forms.Label();
            this.tmrJogarPoder = new System.Windows.Forms.Timer(this.components);
            this.lbGrande = new System.Windows.Forms.Label();
            this.tmrAux = new System.Windows.Forms.Timer(this.components);
            this.lbMoedas = new System.Windows.Forms.Label();
            this.lbExplicacao = new System.Windows.Forms.Label();
            this.tmrEscreverExplic = new System.Windows.Forms.Timer(this.components);
            this.lbTitulo = new System.Windows.Forms.Label();
            this.picQuizBox = new System.Windows.Forms.PictureBox();
            this.picPers = new System.Windows.Forms.PictureBox();
            this.picFinish = new System.Windows.Forms.PictureBox();
            this.picExplicacao = new System.Windows.Forms.PictureBox();
            this.picMoedas = new System.Windows.Forms.PictureBox();
            this.picFinal = new System.Windows.Forms.PictureBox();
            this.picPoder = new System.Windows.Forms.PictureBox();
            this.lbNomeUsuario = new System.Windows.Forms.Label();
            this.lbCreditos = new System.Windows.Forms.Label();
            this.lsbCreditos = new System.Windows.Forms.ListBox();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.lbSenha = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lbSenha2 = new System.Windows.Forms.Label();
            this.lbMudarLoginCadastro = new System.Windows.Forms.LinkLabel();
            this.txtSenha2 = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lbJogarAnonimamente = new System.Windows.Forms.LinkLabel();
            this.picBtnHome = new System.Windows.Forms.PictureBox();
            this.picBtnVoltar = new System.Windows.Forms.PictureBox();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.btnRefresh = new Jogo_Educativo.RoundedButton();
            this.btnRanking = new Jogo_Educativo.RoundedButton();
            this.btnJogarContraAmigos = new Jogo_Educativo.RoundedButton();
            this.btnVerSenha2 = new Jogo_Educativo.RoundedButton();
            this.btnVerSenha = new Jogo_Educativo.RoundedButton();
            this.btnLogarCadastrar = new Jogo_Educativo.RoundedButton();
            this.btnExplicacaoJogo = new Jogo_Educativo.RoundedButton();
            this.btnMostrarPersonagens = new Jogo_Educativo.RoundedButton();
            this.btnMostrarLeveis = new Jogo_Educativo.RoundedButton();
            this.btnLogout = new Jogo_Educativo.RoundedButton();
            this.btnVoltar = new Jogo_Educativo.RoundedButton();
            this.btnHome = new Jogo_Educativo.RoundedButton();
            this.btnFrente = new Jogo_Educativo.RoundedButton();
            this.btnTras = new Jogo_Educativo.RoundedButton();
            this.btnMostrarPersonagens2 = new Jogo_Educativo.RoundedButton();
            this.pnlQuiz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQuizBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExplicacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMoedas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnVoltar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrPular
            // 
            this.tmrPular.Interval = 50;
            this.tmrPular.Tick += new System.EventHandler(this.tmrPular_Tick);
            // 
            // tmrInimigos
            // 
            this.tmrInimigos.Tick += new System.EventHandler(this.tmrInimigos_Tick);
            // 
            // lbVidas
            // 
            this.lbVidas.AutoSize = true;
            this.lbVidas.BackColor = System.Drawing.Color.Transparent;
            this.lbVidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVidas.ForeColor = System.Drawing.Color.Lime;
            this.lbVidas.Location = new System.Drawing.Point(5, 9);
            this.lbVidas.Name = "lbVidas";
            this.lbVidas.Size = new System.Drawing.Size(120, 31);
            this.lbVidas.TabIndex = 5;
            this.lbVidas.Text = "Vidas: 3";
            this.lbVidas.Visible = false;
            // 
            // prgPoder
            // 
            this.prgPoder.Location = new System.Drawing.Point(120, 60);
            this.prgPoder.MarqueeAnimationSpeed = 1000;
            this.prgPoder.Maximum = 800;
            this.prgPoder.Name = "prgPoder";
            this.prgPoder.Size = new System.Drawing.Size(167, 31);
            this.prgPoder.TabIndex = 6;
            this.prgPoder.Visible = false;
            // 
            // lbPoder
            // 
            this.lbPoder.AutoSize = true;
            this.lbPoder.BackColor = System.Drawing.Color.Transparent;
            this.lbPoder.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbPoder.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbPoder.Location = new System.Drawing.Point(8, 58);
            this.lbPoder.Name = "lbPoder";
            this.lbPoder.Size = new System.Drawing.Size(110, 31);
            this.lbPoder.TabIndex = 7;
            this.lbPoder.Text = "PODER:";
            this.lbPoder.Visible = false;
            // 
            // tmrRevivendo
            // 
            this.tmrRevivendo.Tick += new System.EventHandler(this.tmrRevivendo_Tick);
            // 
            // tmrCarregarPoder
            // 
            this.tmrCarregarPoder.Tick += new System.EventHandler(this.tmrPoder_Tick);
            // 
            // pnlQuiz
            // 
            this.pnlQuiz.BackColor = System.Drawing.Color.ForestGreen;
            this.pnlQuiz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQuiz.Controls.Add(this.lbTempoQuiz);
            this.pnlQuiz.Controls.Add(this.btnOpcao4);
            this.pnlQuiz.Controls.Add(this.btnOpcao3);
            this.pnlQuiz.Controls.Add(this.btnOpcao2);
            this.pnlQuiz.Controls.Add(this.btnOpcao1);
            this.pnlQuiz.Controls.Add(this.lbPergunta);
            this.pnlQuiz.Location = new System.Drawing.Point(333, 19);
            this.pnlQuiz.Name = "pnlQuiz";
            this.pnlQuiz.Size = new System.Drawing.Size(300, 166);
            this.pnlQuiz.TabIndex = 9;
            this.pnlQuiz.Visible = false;
            // 
            // lbTempoQuiz
            // 
            this.lbTempoQuiz.AutoSize = true;
            this.lbTempoQuiz.Location = new System.Drawing.Point(205, 142);
            this.lbTempoQuiz.Name = "lbTempoQuiz";
            this.lbTempoQuiz.Size = new System.Drawing.Size(57, 13);
            this.lbTempoQuiz.TabIndex = 5;
            this.lbTempoQuiz.Text = "Tempo: 5s";
            // 
            // btnOpcao4
            // 
            this.btnOpcao4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpcao4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpcao4.Location = new System.Drawing.Point(41, 127);
            this.btnOpcao4.Name = "btnOpcao4";
            this.btnOpcao4.Size = new System.Drawing.Size(150, 23);
            this.btnOpcao4.TabIndex = 4;
            this.btnOpcao4.Tag = "4";
            this.btnOpcao4.Text = "Opção 4";
            this.btnOpcao4.UseVisualStyleBackColor = false;
            this.btnOpcao4.Click += new System.EventHandler(this.btnOpcao1_Click);
            // 
            // btnOpcao3
            // 
            this.btnOpcao3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpcao3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpcao3.Location = new System.Drawing.Point(41, 98);
            this.btnOpcao3.Name = "btnOpcao3";
            this.btnOpcao3.Size = new System.Drawing.Size(150, 23);
            this.btnOpcao3.TabIndex = 3;
            this.btnOpcao3.Tag = "3";
            this.btnOpcao3.Text = "Opção 3";
            this.btnOpcao3.UseVisualStyleBackColor = false;
            this.btnOpcao3.Click += new System.EventHandler(this.btnOpcao1_Click);
            // 
            // btnOpcao2
            // 
            this.btnOpcao2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpcao2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpcao2.Location = new System.Drawing.Point(41, 69);
            this.btnOpcao2.Name = "btnOpcao2";
            this.btnOpcao2.Size = new System.Drawing.Size(150, 23);
            this.btnOpcao2.TabIndex = 2;
            this.btnOpcao2.Tag = "2";
            this.btnOpcao2.Text = "Opção 2";
            this.btnOpcao2.UseVisualStyleBackColor = false;
            this.btnOpcao2.Click += new System.EventHandler(this.btnOpcao1_Click);
            // 
            // btnOpcao1
            // 
            this.btnOpcao1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpcao1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpcao1.Location = new System.Drawing.Point(41, 40);
            this.btnOpcao1.Name = "btnOpcao1";
            this.btnOpcao1.Size = new System.Drawing.Size(150, 23);
            this.btnOpcao1.TabIndex = 1;
            this.btnOpcao1.Tag = "1";
            this.btnOpcao1.Text = "Opção 1";
            this.btnOpcao1.UseVisualStyleBackColor = false;
            this.btnOpcao1.Click += new System.EventHandler(this.btnOpcao1_Click);
            // 
            // lbPergunta
            // 
            this.lbPergunta.AutoSize = true;
            this.lbPergunta.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPergunta.Location = new System.Drawing.Point(18, 12);
            this.lbPergunta.Name = "lbPergunta";
            this.lbPergunta.Size = new System.Drawing.Size(224, 25);
            this.lbPergunta.TabIndex = 0;
            this.lbPergunta.Text = "Qual é a opção certa?";
            // 
            // prgXp
            // 
            this.prgXp.Location = new System.Drawing.Point(585, 143);
            this.prgXp.MarqueeAnimationSpeed = 3000;
            this.prgXp.Maximum = 1000;
            this.prgXp.Name = "prgXp";
            this.prgXp.Size = new System.Drawing.Size(150, 30);
            this.prgXp.Step = 0;
            this.prgXp.TabIndex = 10;
            this.prgXp.Value = 670;
            this.prgXp.Visible = false;
            // 
            // lbIndicarXP
            // 
            this.lbIndicarXP.AutoSize = true;
            this.lbIndicarXP.BackColor = System.Drawing.Color.Transparent;
            this.lbIndicarXP.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndicarXP.Location = new System.Drawing.Point(749, 156);
            this.lbIndicarXP.Name = "lbIndicarXP";
            this.lbIndicarXP.Size = new System.Drawing.Size(36, 23);
            this.lbIndicarXP.TabIndex = 11;
            this.lbIndicarXP.Text = "XP";
            this.lbIndicarXP.Visible = false;
            // 
            // lbNivel
            // 
            this.lbNivel.AutoSize = true;
            this.lbNivel.BackColor = System.Drawing.Color.Transparent;
            this.lbNivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNivel.Location = new System.Drawing.Point(719, 89);
            this.lbNivel.Name = "lbNivel";
            this.lbNivel.Size = new System.Drawing.Size(55, 20);
            this.lbNivel.TabIndex = 12;
            this.lbNivel.Text = "Nível:1";
            this.lbNivel.Visible = false;
            // 
            // tmrTempoQuiz
            // 
            this.tmrTempoQuiz.Tick += new System.EventHandler(this.tmrTempoQuiz_Tick);
            // 
            // lbQuizesErrados
            // 
            this.lbQuizesErrados.AutoSize = true;
            this.lbQuizesErrados.BackColor = System.Drawing.Color.Transparent;
            this.lbQuizesErrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuizesErrados.ForeColor = System.Drawing.Color.Red;
            this.lbQuizesErrados.Location = new System.Drawing.Point(129, 9);
            this.lbQuizesErrados.Name = "lbQuizesErrados";
            this.lbQuizesErrados.Size = new System.Drawing.Size(248, 31);
            this.lbQuizesErrados.TabIndex = 13;
            this.lbQuizesErrados.Text = "Quizes Errados: 0";
            this.lbQuizesErrados.Visible = false;
            // 
            // lbXp
            // 
            this.lbXp.AutoSize = true;
            this.lbXp.BackColor = System.Drawing.Color.Transparent;
            this.lbXp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbXp.Location = new System.Drawing.Point(606, 176);
            this.lbXp.Name = "lbXp";
            this.lbXp.Size = new System.Drawing.Size(67, 16);
            this.lbXp.TabIndex = 14;
            this.lbXp.Text = "670 / 1000";
            this.lbXp.Visible = false;
            // 
            // tmrJogarPoder
            // 
            this.tmrJogarPoder.Tick += new System.EventHandler(this.tmrJogarPoder_Tick);
            // 
            // lbGrande
            // 
            this.lbGrande.AutoSize = true;
            this.lbGrande.BackColor = System.Drawing.Color.White;
            this.lbGrande.Font = new System.Drawing.Font("Courier New", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrande.Location = new System.Drawing.Point(-6, 228);
            this.lbGrande.Name = "lbGrande";
            this.lbGrande.Size = new System.Drawing.Size(564, 73);
            this.lbGrande.TabIndex = 16;
            this.lbGrande.Text = "VOCÊ GANHOU!!!";
            this.lbGrande.Visible = false;
            // 
            // tmrAux
            // 
            this.tmrAux.Tick += new System.EventHandler(this.tmrAux_Tick);
            // 
            // lbMoedas
            // 
            this.lbMoedas.AutoSize = true;
            this.lbMoedas.BackColor = System.Drawing.Color.Transparent;
            this.lbMoedas.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMoedas.Location = new System.Drawing.Point(694, 98);
            this.lbMoedas.Name = "lbMoedas";
            this.lbMoedas.Size = new System.Drawing.Size(110, 31);
            this.lbMoedas.TabIndex = 19;
            this.lbMoedas.Text = "127500";
            this.lbMoedas.Visible = false;
            // 
            // lbExplicacao
            // 
            this.lbExplicacao.AutoSize = true;
            this.lbExplicacao.BackColor = System.Drawing.SystemColors.HighlightText;
            this.lbExplicacao.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExplicacao.Location = new System.Drawing.Point(215, 105);
            this.lbExplicacao.Name = "lbExplicacao";
            this.lbExplicacao.Size = new System.Drawing.Size(530, 162);
            this.lbExplicacao.TabIndex = 22;
            this.lbExplicacao.Text = "1234567890123456789012345678901234567\r\n123456789012345678901234567890\r\n1234567890" +
    "12345678901234567890\r\n123456789012345678901234567890\r\n12345678901234567890123456" +
    "7890\r\n123456789012345678901234567890";
            this.lbExplicacao.Visible = false;
            // 
            // tmrEscreverExplic
            // 
            this.tmrEscreverExplic.Interval = 35;
            this.tmrEscreverExplic.Tick += new System.EventHandler(this.tmrEscreverExplic_Tick);
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lbTitulo.Font = new System.Drawing.Font("Courier New", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.ForeColor = System.Drawing.Color.LawnGreen;
            this.lbTitulo.Location = new System.Drawing.Point(320, 5);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(260, 73);
            this.lbTitulo.TabIndex = 26;
            this.lbTitulo.Text = "LEVEIS";
            this.lbTitulo.Visible = false;
            // 
            // picQuizBox
            // 
            this.picQuizBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picQuizBox.Image = global::Jogo_Educativo.Properties.Resources.box;
            this.picQuizBox.Location = new System.Drawing.Point(600, 228);
            this.picQuizBox.Name = "picQuizBox";
            this.picQuizBox.Size = new System.Drawing.Size(64, 64);
            this.picQuizBox.TabIndex = 17;
            this.picQuizBox.TabStop = false;
            this.picQuizBox.Visible = false;
            // 
            // picPers
            // 
            this.picPers.BackColor = System.Drawing.Color.Transparent;
            this.picPers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPers.Location = new System.Drawing.Point(185, 371);
            this.picPers.Name = "picPers";
            this.picPers.Size = new System.Drawing.Size(69, 100);
            this.picPers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPers.TabIndex = 1;
            this.picPers.TabStop = false;
            this.picPers.Visible = false;
            // 
            // picFinish
            // 
            this.picFinish.BackColor = System.Drawing.Color.Transparent;
            this.picFinish.Image = global::Jogo_Educativo.Properties.Resources.finish_line;
            this.picFinish.Location = new System.Drawing.Point(604, 344);
            this.picFinish.Name = "picFinish";
            this.picFinish.Size = new System.Drawing.Size(129, 127);
            this.picFinish.TabIndex = 25;
            this.picFinish.TabStop = false;
            this.picFinish.Visible = false;
            // 
            // picExplicacao
            // 
            this.picExplicacao.BackColor = System.Drawing.Color.Transparent;
            this.picExplicacao.Image = global::Jogo_Educativo.Properties.Resources.balaoD;
            this.picExplicacao.Location = new System.Drawing.Point(189, 76);
            this.picExplicacao.Name = "picExplicacao";
            this.picExplicacao.Size = new System.Drawing.Size(554, 322);
            this.picExplicacao.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExplicacao.TabIndex = 21;
            this.picExplicacao.TabStop = false;
            this.picExplicacao.Visible = false;
            // 
            // picMoedas
            // 
            this.picMoedas.BackColor = System.Drawing.Color.Transparent;
            this.picMoedas.Image = global::Jogo_Educativo.Properties.Resources.moedas;
            this.picMoedas.Location = new System.Drawing.Point(831, 46);
            this.picMoedas.Name = "picMoedas";
            this.picMoedas.Size = new System.Drawing.Size(45, 45);
            this.picMoedas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMoedas.TabIndex = 20;
            this.picMoedas.TabStop = false;
            this.picMoedas.Visible = false;
            // 
            // picFinal
            // 
            this.picFinal.BackColor = System.Drawing.Color.Transparent;
            this.picFinal.Image = global::Jogo_Educativo.Properties.Resources.final;
            this.picFinal.Location = new System.Drawing.Point(749, 319);
            this.picFinal.Name = "picFinal";
            this.picFinal.Size = new System.Drawing.Size(119, 152);
            this.picFinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFinal.TabIndex = 18;
            this.picFinal.TabStop = false;
            this.picFinal.Visible = false;
            // 
            // picPoder
            // 
            this.picPoder.BackColor = System.Drawing.Color.Transparent;
            this.picPoder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPoder.Image = global::Jogo_Educativo.Properties.Resources.abacus;
            this.picPoder.Location = new System.Drawing.Point(389, 404);
            this.picPoder.Name = "picPoder";
            this.picPoder.Size = new System.Drawing.Size(40, 44);
            this.picPoder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoder.TabIndex = 15;
            this.picPoder.TabStop = false;
            this.picPoder.Visible = false;
            // 
            // lbNomeUsuario
            // 
            this.lbNomeUsuario.AutoSize = true;
            this.lbNomeUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbNomeUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 54F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNomeUsuario.ForeColor = System.Drawing.Color.White;
            this.lbNomeUsuario.Location = new System.Drawing.Point(167, 61);
            this.lbNomeUsuario.Name = "lbNomeUsuario";
            this.lbNomeUsuario.Size = new System.Drawing.Size(680, 82);
            this.lbNomeUsuario.TabIndex = 33;
            this.lbNomeUsuario.Text = "LBNOMEUSUARIO";
            this.lbNomeUsuario.Visible = false;
            // 
            // lbCreditos
            // 
            this.lbCreditos.AutoSize = true;
            this.lbCreditos.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbCreditos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbCreditos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCreditos.Location = new System.Drawing.Point(7, 487);
            this.lbCreditos.Name = "lbCreditos";
            this.lbCreditos.Size = new System.Drawing.Size(184, 20);
            this.lbCreditos.TabIndex = 35;
            this.lbCreditos.Text = "Créditos das Imagens";
            this.lbCreditos.Visible = false;
            this.lbCreditos.Click += new System.EventHandler(this.lbCreditos_Click);
            // 
            // lsbCreditos
            // 
            this.lsbCreditos.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbCreditos.FormattingEnabled = true;
            this.lsbCreditos.ItemHeight = 30;
            this.lsbCreditos.Items.AddRange(new object[] {
            "Play - FreePik",
            "Help - Eleonor Wang ",
            "Home - Miscellaneous Elements",
            "Voltar - Dave Gandy",
            "Moedas - Color Startups and New Business",
            "Finish Line - Vignesh Oviyan",
            "Finish Flag - Vectors Market",
            "Box - Those Icons",
            "Refresh - Pixel Buddha",
            "",
            "www.flaticon.com"});
            this.lsbCreditos.Location = new System.Drawing.Point(190, 94);
            this.lsbCreditos.Name = "lsbCreditos";
            this.lsbCreditos.Size = new System.Drawing.Size(519, 334);
            this.lsbCreditos.TabIndex = 36;
            this.lsbCreditos.Visible = false;
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.Location = new System.Drawing.Point(162, 123);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(165, 39);
            this.lbUsuario.TabIndex = 38;
            this.lbUsuario.Text = "Usuario: ";
            // 
            // lbSenha
            // 
            this.lbSenha.AutoSize = true;
            this.lbSenha.BackColor = System.Drawing.Color.Transparent;
            this.lbSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSenha.Location = new System.Drawing.Point(198, 182);
            this.lbSenha.Name = "lbSenha";
            this.lbSenha.Size = new System.Drawing.Size(142, 39);
            this.lbSenha.TabIndex = 39;
            this.lbSenha.Text = "Senha: ";
            this.lbSenha.Visible = false;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(367, 118);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(366, 44);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.Visible = false;
            this.txtUsuario.TextChanged += new System.EventHandler(this.txtUsuario_TextChanged);
            // 
            // lbSenha2
            // 
            this.lbSenha2.AutoSize = true;
            this.lbSenha2.BackColor = System.Drawing.Color.Transparent;
            this.lbSenha2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSenha2.Location = new System.Drawing.Point(72, 250);
            this.lbSenha2.Name = "lbSenha2";
            this.lbSenha2.Size = new System.Drawing.Size(230, 39);
            this.lbSenha2.TabIndex = 41;
            this.lbSenha2.Text = "Conf. Senha:";
            this.lbSenha2.Visible = false;
            // 
            // lbMudarLoginCadastro
            // 
            this.lbMudarLoginCadastro.AutoSize = true;
            this.lbMudarLoginCadastro.BackColor = System.Drawing.Color.Transparent;
            this.lbMudarLoginCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMudarLoginCadastro.Location = new System.Drawing.Point(486, 428);
            this.lbMudarLoginCadastro.Name = "lbMudarLoginCadastro";
            this.lbMudarLoginCadastro.Size = new System.Drawing.Size(286, 20);
            this.lbMudarLoginCadastro.TabIndex = 43;
            this.lbMudarLoginCadastro.TabStop = true;
            this.lbMudarLoginCadastro.Text = "Não tem uma conta? Cadastre-se aqui.";
            this.lbMudarLoginCadastro.Visible = false;
            this.lbMudarLoginCadastro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbMudarLoginCadastro_LinkClicked);
            // 
            // txtSenha2
            // 
            this.txtSenha2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha2.Location = new System.Drawing.Point(367, 237);
            this.txtSenha2.MaxLength = 64;
            this.txtSenha2.Name = "txtSenha2";
            this.txtSenha2.PasswordChar = '*';
            this.txtSenha2.Size = new System.Drawing.Size(366, 44);
            this.txtSenha2.TabIndex = 2;
            this.txtSenha2.Visible = false;
            this.txtSenha2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenha2_KeyDown);
            // 
            // txtSenha
            // 
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(367, 179);
            this.txtSenha.MaxLength = 64;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(366, 44);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.Visible = false;
            this.txtSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenha_KeyDown);
            // 
            // lbJogarAnonimamente
            // 
            this.lbJogarAnonimamente.AutoSize = true;
            this.lbJogarAnonimamente.BackColor = System.Drawing.Color.Transparent;
            this.lbJogarAnonimamente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJogarAnonimamente.Location = new System.Drawing.Point(131, 428);
            this.lbJogarAnonimamente.Name = "lbJogarAnonimamente";
            this.lbJogarAnonimamente.Size = new System.Drawing.Size(259, 20);
            this.lbJogarAnonimamente.TabIndex = 104;
            this.lbJogarAnonimamente.TabStop = true;
            this.lbJogarAnonimamente.Text = "Jogue anonimamente clicando aqui";
            this.lbJogarAnonimamente.Visible = false;
            this.lbJogarAnonimamente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // picBtnHome
            // 
            this.picBtnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnHome.Image = global::Jogo_Educativo.Properties.Resources.home;
            this.picBtnHome.Location = new System.Drawing.Point(77, 121);
            this.picBtnHome.Name = "picBtnHome";
            this.picBtnHome.Size = new System.Drawing.Size(39, 39);
            this.picBtnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnHome.TabIndex = 105;
            this.picBtnHome.TabStop = false;
            this.picBtnHome.Visible = false;
            this.picBtnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // picBtnVoltar
            // 
            this.picBtnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnVoltar.Image = global::Jogo_Educativo.Properties.Resources.voltar;
            this.picBtnVoltar.Location = new System.Drawing.Point(21, 121);
            this.picBtnVoltar.Name = "picBtnVoltar";
            this.picBtnVoltar.Size = new System.Drawing.Size(39, 39);
            this.picBtnVoltar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnVoltar.TabIndex = 106;
            this.picBtnVoltar.TabStop = false;
            this.picBtnVoltar.Visible = false;
            this.picBtnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLoading.Image = global::Jogo_Educativo.Properties.Resources.loading;
            this.picLoading.Location = new System.Drawing.Point(357, 270);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(120, 115);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 110;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            this.picLoading.Click += new System.EventHandler(this.picLoading_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::Jogo_Educativo.Properties.Resources.refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(780, 83);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 45);
            this.btnRefresh.TabIndex = 109;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRanking
            // 
            this.btnRanking.BackgroundImage = global::Jogo_Educativo.Properties.Resources.ranking;
            this.btnRanking.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRanking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRanking.Location = new System.Drawing.Point(758, 456);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(45, 45);
            this.btnRanking.TabIndex = 108;
            this.btnRanking.UseVisualStyleBackColor = true;
            this.btnRanking.Visible = false;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnJogarContraAmigos
            // 
            this.btnJogarContraAmigos.BackgroundImage = global::Jogo_Educativo.Properties.Resources.online_game;
            this.btnJogarContraAmigos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJogarContraAmigos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJogarContraAmigos.Location = new System.Drawing.Point(746, 456);
            this.btnJogarContraAmigos.Name = "btnJogarContraAmigos";
            this.btnJogarContraAmigos.Size = new System.Drawing.Size(50, 50);
            this.btnJogarContraAmigos.TabIndex = 107;
            this.btnJogarContraAmigos.UseVisualStyleBackColor = true;
            this.btnJogarContraAmigos.Visible = false;
            this.btnJogarContraAmigos.Click += new System.EventHandler(this.btnJogarContraAmigos_Click);
            // 
            // btnVerSenha2
            // 
            this.btnVerSenha2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerSenha2.Location = new System.Drawing.Point(740, 242);
            this.btnVerSenha2.Name = "btnVerSenha2";
            this.btnVerSenha2.Size = new System.Drawing.Size(36, 36);
            this.btnVerSenha2.TabIndex = 103;
            this.btnVerSenha2.Text = "Ver Senha";
            this.btnVerSenha2.UseVisualStyleBackColor = true;
            this.btnVerSenha2.Visible = false;
            this.btnVerSenha2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnVerSenha2_MouseDown);
            this.btnVerSenha2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnVerSenha2_MouseUp);
            // 
            // btnVerSenha
            // 
            this.btnVerSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerSenha.Location = new System.Drawing.Point(740, 182);
            this.btnVerSenha.Name = "btnVerSenha";
            this.btnVerSenha.Size = new System.Drawing.Size(36, 36);
            this.btnVerSenha.TabIndex = 102;
            this.btnVerSenha.Text = "Ver Senha";
            this.btnVerSenha.UseVisualStyleBackColor = true;
            this.btnVerSenha.Visible = false;
            this.btnVerSenha.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnVerSenha_MouseDown);
            this.btnVerSenha.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnVerSenha_MouseUp);
            // 
            // btnLogarCadastrar
            // 
            this.btnLogarCadastrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogarCadastrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogarCadastrar.Location = new System.Drawing.Point(389, 319);
            this.btnLogarCadastrar.Name = "btnLogarCadastrar";
            this.btnLogarCadastrar.Size = new System.Drawing.Size(126, 54);
            this.btnLogarCadastrar.TabIndex = 3;
            this.btnLogarCadastrar.Text = "Login";
            this.btnLogarCadastrar.UseVisualStyleBackColor = true;
            this.btnLogarCadastrar.Visible = false;
            this.btnLogarCadastrar.Click += new System.EventHandler(this.btnLogarCadastrar_Click);
            // 
            // btnExplicacaoJogo
            // 
            this.btnExplicacaoJogo.BackgroundImage = global::Jogo_Educativo.Properties.Resources.question;
            this.btnExplicacaoJogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExplicacaoJogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExplicacaoJogo.Location = new System.Drawing.Point(178, 273);
            this.btnExplicacaoJogo.Name = "btnExplicacaoJogo";
            this.btnExplicacaoJogo.Size = new System.Drawing.Size(85, 85);
            this.btnExplicacaoJogo.TabIndex = 31;
            this.btnExplicacaoJogo.UseVisualStyleBackColor = true;
            this.btnExplicacaoJogo.Visible = false;
            this.btnExplicacaoJogo.Click += new System.EventHandler(this.btnExplicacaoJogo_Click);
            // 
            // btnMostrarPersonagens
            // 
            this.btnMostrarPersonagens.BackgroundImage = global::Jogo_Educativo.Properties.Resources.user;
            this.btnMostrarPersonagens.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMostrarPersonagens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMostrarPersonagens.Location = new System.Drawing.Point(590, 273);
            this.btnMostrarPersonagens.Name = "btnMostrarPersonagens";
            this.btnMostrarPersonagens.Size = new System.Drawing.Size(85, 85);
            this.btnMostrarPersonagens.TabIndex = 30;
            this.btnMostrarPersonagens.UseVisualStyleBackColor = true;
            this.btnMostrarPersonagens.Visible = false;
            this.btnMostrarPersonagens.Click += new System.EventHandler(this.btnMostrarPersonagens_Click);
            // 
            // btnMostrarLeveis
            // 
            this.btnMostrarLeveis.BackgroundImage = global::Jogo_Educativo.Properties.Resources.play_button;
            this.btnMostrarLeveis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMostrarLeveis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMostrarLeveis.Location = new System.Drawing.Point(331, 228);
            this.btnMostrarLeveis.Name = "btnMostrarLeveis";
            this.btnMostrarLeveis.Size = new System.Drawing.Size(185, 185);
            this.btnMostrarLeveis.TabIndex = 29;
            this.btnMostrarLeveis.UseVisualStyleBackColor = true;
            this.btnMostrarLeveis.Visible = false;
            this.btnMostrarLeveis.Click += new System.EventHandler(this.btnMostrarLeveis_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackgroundImage = global::Jogo_Educativo.Properties.Resources.login1;
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Location = new System.Drawing.Point(818, 456);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(45, 45);
            this.btnLogout.TabIndex = 32;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Visible = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackgroundImage = global::Jogo_Educativo.Properties.Resources.voltar;
            this.btnVoltar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.Location = new System.Drawing.Point(18, 18);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(45, 45);
            this.btnVoltar.TabIndex = 101;
            this.btnVoltar.TabStop = false;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Visible = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.SystemColors.Control;
            this.btnHome.BackgroundImage = global::Jogo_Educativo.Properties.Resources.home;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.Location = new System.Drawing.Point(74, 118);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(45, 45);
            this.btnHome.TabIndex = 27;
            this.btnHome.TabStop = false;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Visible = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnFrente
            // 
            this.btnFrente.Location = new System.Drawing.Point(627, 270);
            this.btnFrente.Name = "btnFrente";
            this.btnFrente.Size = new System.Drawing.Size(82, 22);
            this.btnFrente.TabIndex = 23;
            this.btnFrente.Text = "Próximo";
            this.btnFrente.UseVisualStyleBackColor = true;
            this.btnFrente.Visible = false;
            this.btnFrente.Click += new System.EventHandler(this.btnFrente_Click);
            // 
            // btnTras
            // 
            this.btnTras.Location = new System.Drawing.Point(222, 270);
            this.btnTras.Name = "btnTras";
            this.btnTras.Size = new System.Drawing.Size(80, 22);
            this.btnTras.TabIndex = 24;
            this.btnTras.Text = "Voltar";
            this.btnTras.UseVisualStyleBackColor = true;
            this.btnTras.Visible = false;
            this.btnTras.Click += new System.EventHandler(this.btnTras_Click);
            // 
            // btnMostrarPersonagens2
            // 
            this.btnMostrarPersonagens2.BackgroundImage = global::Jogo_Educativo.Properties.Resources.user;
            this.btnMostrarPersonagens2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMostrarPersonagens2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMostrarPersonagens2.Location = new System.Drawing.Point(816, 456);
            this.btnMostrarPersonagens2.Name = "btnMostrarPersonagens2";
            this.btnMostrarPersonagens2.Size = new System.Drawing.Size(50, 50);
            this.btnMostrarPersonagens2.TabIndex = 100;
            this.btnMostrarPersonagens2.UseVisualStyleBackColor = true;
            this.btnMostrarPersonagens2.Visible = false;
            this.btnMostrarPersonagens2.Click += new System.EventHandler(this.btnMostrarPersonagens_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Jogo_Educativo.Properties.Resources.Wallpaper1;
            this.ClientSize = new System.Drawing.Size(880, 512);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.btnJogarContraAmigos);
            this.Controls.Add(this.picBtnVoltar);
            this.Controls.Add(this.picBtnHome);
            this.Controls.Add(this.lbJogarAnonimamente);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lbUsuario);
            this.Controls.Add(this.btnVerSenha2);
            this.Controls.Add(this.btnVerSenha);
            this.Controls.Add(this.lbNomeUsuario);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.pnlQuiz);
            this.Controls.Add(this.lbNivel);
            this.Controls.Add(this.lbMudarLoginCadastro);
            this.Controls.Add(this.txtSenha2);
            this.Controls.Add(this.lbSenha2);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lbSenha);
            this.Controls.Add(this.btnLogarCadastrar);
            this.Controls.Add(this.lbCreditos);
            this.Controls.Add(this.lbXp);
            this.Controls.Add(this.lbIndicarXP);
            this.Controls.Add(this.prgXp);
            this.Controls.Add(this.btnExplicacaoJogo);
            this.Controls.Add(this.btnMostrarPersonagens);
            this.Controls.Add(this.btnMostrarLeveis);
            this.Controls.Add(this.lbGrande);
            this.Controls.Add(this.picQuizBox);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.btnFrente);
            this.Controls.Add(this.lbExplicacao);
            this.Controls.Add(this.picPers);
            this.Controls.Add(this.picFinish);
            this.Controls.Add(this.btnTras);
            this.Controls.Add(this.picExplicacao);
            this.Controls.Add(this.picMoedas);
            this.Controls.Add(this.lbMoedas);
            this.Controls.Add(this.picFinal);
            this.Controls.Add(this.picPoder);
            this.Controls.Add(this.lbPoder);
            this.Controls.Add(this.prgPoder);
            this.Controls.Add(this.lbVidas);
            this.Controls.Add(this.btnMostrarPersonagens2);
            this.Controls.Add(this.lsbCreditos);
            this.Controls.Add(this.lbQuizesErrados);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jogo Interativo de Matemática";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principal_FormClosed);
            this.Load += new System.EventHandler(this.Principal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Principal_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Principal_KeyUp);
            this.pnlQuiz.ResumeLayout(false);
            this.pnlQuiz.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQuizBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExplicacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMoedas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnVoltar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picPers;
        private System.Windows.Forms.Timer tmrPular;
        private System.Windows.Forms.Timer tmrInimigos;
        private System.Windows.Forms.Label lbVidas;
        private System.Windows.Forms.ProgressBar prgPoder;
        private System.Windows.Forms.Label lbPoder;
        private System.Windows.Forms.Timer tmrRevivendo;
        private System.Windows.Forms.Timer tmrCarregarPoder;
        private System.Windows.Forms.Panel pnlQuiz;
        private RoundedButton btnOpcao4;
        private RoundedButton btnOpcao3;
        private RoundedButton btnOpcao2;
        private RoundedButton btnOpcao1;
        private System.Windows.Forms.Label lbPergunta;
        private System.Windows.Forms.ProgressBar prgXp;
        private System.Windows.Forms.Label lbIndicarXP;
        private System.Windows.Forms.Label lbNivel;
        private System.Windows.Forms.Label lbTempoQuiz;
        private System.Windows.Forms.Timer tmrTempoQuiz;
        private System.Windows.Forms.Label lbQuizesErrados;
        private System.Windows.Forms.Label lbXp;
        private System.Windows.Forms.Timer tmrJogarPoder;
        private System.Windows.Forms.PictureBox picPoder;
        private System.Windows.Forms.Label lbGrande;
        private System.Windows.Forms.Timer tmrAux;
        private System.Windows.Forms.PictureBox picFinal;
        private System.Windows.Forms.Label lbMoedas;
        private System.Windows.Forms.PictureBox picMoedas;
        private System.Windows.Forms.PictureBox picExplicacao;
        private System.Windows.Forms.Label lbExplicacao;
        private RoundedButton btnFrente;
        private RoundedButton btnTras;
        private System.Windows.Forms.PictureBox picFinish;
        private System.Windows.Forms.Timer tmrEscreverExplic;

        private System.Windows.Forms.PictureBox[] imgInimigos;
        private System.Windows.Forms.PictureBox[] imgObstaculos;
        private System.Windows.Forms.PictureBox[] picEstrelasGanhou;

        //mostrar btn leveis
        private RoundedPictureBox[] btnLeveis;
        private System.Windows.Forms.Label[] lbLeveis;
        private System.Windows.Forms.PictureBox[,] picEstrelas;
        private System.Windows.Forms.Label lbTitulo;

        //mostrar personagens
        private System.Windows.Forms.PictureBox[] imgPersonagens;
        private System.Windows.Forms.Label[] lbNomePersonagens;
        private System.Windows.Forms.Label[] lbPrecoPersonagens;

        //ranking
        private System.Windows.Forms.Label[] numeracaoRanking;
        private System.Windows.Forms.Label[] nomeRanking;
        private System.Windows.Forms.Label[] lvRanking;

        //jogar com amigos
        private RoundedPictureBox[] btnJogarContraAmigoEspecifico;
        private System.Windows.Forms.Label[] lbNomeAmigos;
        private System.Windows.Forms.Label[] lbNivelAmigos;

        private RoundedButton btnHome;
        private RoundedButton btnMostrarLeveis;
        private RoundedButton btnMostrarPersonagens;
        private RoundedButton btnExplicacaoJogo;
        private RoundedButton btnLogout;
        private System.Windows.Forms.PictureBox picQuizBox;
        private RoundedButton btnVoltar;
        private System.Windows.Forms.Label lbNomeUsuario;
        private RoundedButton btnMostrarPersonagens2;
        private System.Windows.Forms.Label lbCreditos;
        private System.Windows.Forms.ListBox lsbCreditos;
        private RoundedButton btnLogarCadastrar;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Label lbSenha;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lbSenha2;
        private System.Windows.Forms.LinkLabel lbMudarLoginCadastro;
        private System.Windows.Forms.TextBox txtSenha2;
        private System.Windows.Forms.TextBox txtSenha;
        private RoundedButton btnVerSenha;
        private RoundedButton btnVerSenha2;
        private System.Windows.Forms.LinkLabel lbJogarAnonimamente;
        private System.Windows.Forms.PictureBox picBtnHome;
        private System.Windows.Forms.PictureBox picBtnVoltar;
        private RoundedButton btnJogarContraAmigos;
        private RoundedButton btnRanking;
        private RoundedButton btnRefresh;
        private System.Windows.Forms.PictureBox picLoading;
    }
}

