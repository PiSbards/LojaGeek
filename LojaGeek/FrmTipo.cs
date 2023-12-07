using LojaGeek.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaGeek
{
    public partial class FrmTipo : Form
    {
        public FrmTipo()
        {
            InitializeComponent();
        }
        
        private void btnSair_Click(object sender, EventArgs e)
        {
            FrmProduto produto = new FrmProduto();
            produto.Show();
            this.Close();
        }

        private void FrmTipo_Load(object sender, EventArgs e)
        {
            TipoProduto tipoProduto = new TipoProduto();
            List<TipoProduto> tipoProdutos = tipoProduto.listatipoproduto();
            dgvTipoProduto.DataSource = tipoProdutos;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTipoProduto.Text == "")
                {
                    MessageBox.Show("Por favor, preencha o campo Tipo do Produto!", "Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ActiveControl = txtTipoProduto;
                    return;
                }
                else
                {
                    TipoProduto tipoProduto = new TipoProduto();
                    if (tipoProduto.RegistroRepetido(txtTipoProduto.Text) == true)
                    {
                        MessageBox.Show("Tipo do produto já existe em nossa base de dados!", "Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTipoProduto.Text = "";
                        this.ActiveControl = txtTipoProduto;
                    }
                    else
                    {
                        tipoProduto.Inserir(txtTipoProduto.Text);
                        MessageBox.Show("Tipo do produto inserido com sucesso!", "Inserção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTipoProduto.Text = "";
                        this.ActiveControl = txtTipoProduto;
                        List<TipoProduto> tipoProdutos = tipoProduto.listatipoproduto();
                        dgvTipoProduto.DataSource = tipoProdutos;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Inserção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdTipo.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor digite um ID para realizar a busca!", "Localizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ActiveControl = txtIdTipo;
                    return;
                }
                else
                {
                    int Id = Convert.ToInt32(txtIdTipo.Text.Trim());
                    TipoProduto tipoProduto = new TipoProduto();
                    tipoProduto.Localizar(Id);
                    txtTipoProduto.Text = tipoProduto.tipo;
                    btnEditar.Enabled = true;
                    btnExcluir.Enabled = true;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Localização", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(txtIdTipo.Text.Trim());
                TipoProduto tipoProduto = new TipoProduto();
                tipoProduto.Excluir(Id);
                MessageBox.Show("Tipo do produto excluído com sucesso!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdTipo.Text = "";
                txtTipoProduto.Text = "";
                this.ActiveControl = txtTipoProduto;
                List<TipoProduto> tipoProdutos = tipoProduto.listatipoproduto();
                dgvTipoProduto.DataSource = tipoProdutos;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTipoProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvTipoProduto.Rows[e.RowIndex];
                row.Selected = true;
                txtIdTipo.Text = row.Cells[0].Value.ToString();
                txtTipoProduto.Text = row.Cells[1].Value.ToString();
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(txtIdTipo.Text.Trim());
                TipoProduto tipoProduto = new TipoProduto();
                tipoProduto.Atualizar(Id, txtTipoProduto.Text);
                MessageBox.Show("Tipo do Produto atualizado com sucesso!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdTipo.Text = "";
                txtTipoProduto.Text = "";
                this.ActiveControl = txtTipoProduto;
                List<TipoProduto> tipoProdutos = tipoProduto.listatipoproduto();
                dgvTipoProduto.DataSource = tipoProdutos;

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
