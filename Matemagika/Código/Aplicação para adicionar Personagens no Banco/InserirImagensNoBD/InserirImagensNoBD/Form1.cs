using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//novo
using System.Data.SqlClient;
using System.IO;

namespace InserirImagensNoBD
{
    public partial class Form1 : Form
    {
        private String connStr = "";
        private SqlConnection myConnection;

        private long tamanhoArquivoImagem0 = 0;
        private byte[] vetorImagem0;
        private long tamanhoArquivoImagem1 = 0;
        private byte[] vetorImagem1;
        private long tamanhoArquivoImagem2 = 0;
        private byte[] vetorImagem2;
        private long tamanhoArquivoImagem3 = 0;
        private byte[] vetorImagem3;

        public Form1()
        {
            InitializeComponent();

            connStr = InserirImagensNoBD.Properties.Settings.Default.BDPRII17188ConnectionString;

            myConnection = new SqlConnection(connStr);
            myConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.dlgAbrir.ShowDialog(this);

                int tag = Convert.ToInt32(((Button)sender).Tag);
                string strArq = this.dlgAbrir.FileName;
                //  MessageBox.Show(strArq);

                if (string.IsNullOrEmpty(strArq))
                    return;// fechar se não selecionado arquivo

                FileInfo arqImagem;
                FileStream fs;
                int iByteRead;
                switch (tag)
                {
                    case 0:
                        this.pictureBox1.Image = Image.FromFile(strArq);
                        // transformar imagem em vetor de byte para colocar no BD
                        arqImagem = new FileInfo(strArq);
                        this.tamanhoArquivoImagem0 = arqImagem.Length;

                        // cria uma stream em memoria com a imagem
                        fs = new FileStream
                            (strArq, FileMode.Open, FileAccess.Read, FileShare.Read);

                        this.vetorImagem0 = new byte[Convert.ToInt32(tamanhoArquivoImagem0)];

                        iByteRead = fs.Read(vetorImagem0,
                                                                0,
                                                                Convert.ToInt32(tamanhoArquivoImagem0));
                        fs.Close();
                        break;
                    case 1:
                        this.pictureBox2.Image = Image.FromFile(strArq);
                        // transformar imagem em vetor de byte para colocar no BD
                        arqImagem = new FileInfo(strArq);
                        this.tamanhoArquivoImagem1 = arqImagem.Length;

                        // cria uma stream em memoria com a imagem
                        fs = new FileStream
                            (strArq, FileMode.Open, FileAccess.Read, FileShare.Read);

                        this.vetorImagem1 = new byte[Convert.ToInt32(tamanhoArquivoImagem1)];

                        iByteRead = fs.Read(vetorImagem1,
                                                                0,
                                                                Convert.ToInt32(tamanhoArquivoImagem1));
                        fs.Close();
                        break;
                    case 2:
                        this.pictureBox3.Image = Image.FromFile(strArq);
                        // transformar imagem em vetor de byte para colocar no BD
                        arqImagem = new FileInfo(strArq);
                        this.tamanhoArquivoImagem2 = arqImagem.Length;

                        // cria uma stream em memoria com a imagem
                        fs = new FileStream
                            (strArq, FileMode.Open, FileAccess.Read, FileShare.Read);

                        this.vetorImagem2 = new byte[Convert.ToInt32(tamanhoArquivoImagem2)];

                        iByteRead = fs.Read(vetorImagem2,
                                                                0,
                                                                Convert.ToInt32(tamanhoArquivoImagem2));
                        fs.Close();
                        break;
                    case 3:
                        this.pictureBox4.Image = Image.FromFile(strArq);
                        // transformar imagem em vetor de byte para colocar no BD
                        arqImagem = new FileInfo(strArq);
                        this.tamanhoArquivoImagem3 = arqImagem.Length;

                        // cria uma stream em memoria com a imagem
                        fs = new FileStream
                            (strArq, FileMode.Open, FileAccess.Read, FileShare.Read);

                        this.vetorImagem3 = new byte[Convert.ToInt32(tamanhoArquivoImagem3)];

                        iByteRead = fs.Read(vetorImagem3,
                                                                0,
                                                                Convert.ToInt32(tamanhoArquivoImagem3));
                        fs.Close();
                        break;
                }
                
                


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Digite o nome do personagem");
                return;
            }
            int iResultado = 0;
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                if (sqlCmd.Parameters.Count == 0)
                {
                    sqlCmd.CommandText = "INSERT INTO personagem" +
                        " VALUES(@nome,@img1, @img2, @img3, @img4, @preco)";

                    sqlCmd.Parameters.Add("@img1", System.Data.SqlDbType.Image);
                    sqlCmd.Parameters.Add("@img2", System.Data.SqlDbType.Image);
                    sqlCmd.Parameters.Add("@img3", System.Data.SqlDbType.Image);
                    sqlCmd.Parameters.Add("@img4", System.Data.SqlDbType.Image);
                }

                sqlCmd.Parameters.AddWithValue("@nome", txtNome.Text);
                sqlCmd.Parameters.AddWithValue("@preco", Convert.ToInt32(txtPreco.Text));
                sqlCmd.Parameters["@img1"].Value = vetorImagem0;
                sqlCmd.Parameters["@img2"].Value = vetorImagem1;
                sqlCmd.Parameters["@img3"].Value = vetorImagem2;
                sqlCmd.Parameters["@img4"].Value = vetorImagem3;

                iResultado = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                if (iResultado <= 0)
                {
                    MessageBox.Show("Falha ao inserir no BD");
                }
                else
                {
                    MessageBox.Show("Imagem inserida no BD");
                    this.txtNome.Text = "";
                    this.pictureBox1.Image = null;
                    this.pictureBox2.Image = null;
                    this.pictureBox3.Image = null;
                    this.pictureBox4.Image = null;
                }
            }
        }
    }
}
