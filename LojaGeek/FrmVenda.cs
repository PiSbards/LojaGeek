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
    public partial class FrmVenda : Form
    {
        public FrmVenda()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\TDS\\2° Periodo\\PA - Prof.Emerson\\LojaGeek\\LojaGeek.mdf\";Integrated Security=True");
        public void CarregaCbxCliente()
        {
            string cli = "SELECT Id, nome FROM Cliente";
            SqlCommand cmd = new SqlCommand(cli, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cli, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "cliente");
            cbxCliente.ValueMember = "Id";
            cbxCliente.DisplayMember = "nome";
            cbxCliente.DataSource = ds.Tables["cliente"];
            con.Close();
        }
        public void CarregacbxProduto()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string pro = "SELECT Id, nome FROM Produto";
            SqlCommand cmd = new SqlCommand(pro, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(pro, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "produto");
            cbxProduto.ValueMember = "Id";
            cbxProduto.DisplayMember = "nome";
            cbxProduto.DataSource = ds.Tables["produto"];
            con.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVenda_Load(object sender, EventArgs e)
        {
            cbxCliente.Enabled = false;
            cbxProduto.Enabled = false;
            txtIdProduto.Enabled = false;
            txtQuantidade.Enabled = false;
            txtValor.Enabled = false;
            txtTotal.Enabled = false;            
            btnFinalizarVenda.Enabled = false;
            btnNovoItem.Enabled = false;
            btnEditarItem.Enabled = false;
            btnExcluirItem.Enabled = false;
        }

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            cbxCliente.Enabled = true;
            cbxProduto.Enabled = true;
            CarregaCbxCliente();
            CarregacbxProduto();
            txtIdProduto.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValor.Enabled = true;
            txtTotal.Enabled = true;            
            btnFinalizarVenda.Enabled = true;
            btnNovoItem.Enabled = true;
            btnEditarItem.Enabled = true;
            btnExcluirItem.Enabled = true;
            dgvVenda.Columns.Add("ID", "ID");
            dgvVenda.Columns.Add("Produto", "Produto");
            dgvVenda.Columns.Add("Quantidade", "Quantidade");
            dgvVenda.Columns.Add("Valor", "Valor");
            dgvVenda.Columns.Add("Total", "Total");
        }

        private void cbxProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM Produto WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", cbxProduto.SelectedValue);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtValor.Text = dr["preco"].ToString();
                txtIdProduto.Text = dr["Id"].ToString();
                lblEstoque.Text = dr["quantidade"].ToString();
                txtQuantidade.Focus();
                dr.Close();
            }
            con.Close();
        }

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            if (cbxCliente.Text == "" || cbxProduto.Text == "" || txtValor.Text == "" || txtQuantidade.Text == "" || txtIdProduto.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var repetido = false;
                foreach (DataGridViewRow dr in dgvVenda.Rows)
                {
                    if (txtIdProduto.Text == Convert.ToString(dr.Cells[0].Value))
                    {
                        repetido = true;
                    }
                }
                if (repetido == false)
                {
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dgvVenda);
                    item.Cells[0].Value = txtIdProduto.Text;
                    item.Cells[1].Value = cbxProduto.Text;
                    item.Cells[2].Value = txtQuantidade.Text;
                    item.Cells[3].Value = txtValor.Text;
                    item.Cells[4].Value = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
                    dgvVenda.Rows.Add(item);
                    txtIdProduto.Text = "";
                    txtValor.Text = "";
                    txtQuantidade.Text = "";
                    cbxProduto.Text = "";
                    decimal soma = 0;
                    foreach (DataGridViewRow dr in dgvVenda.Rows)
                        soma += Convert.ToDecimal(dr.Cells[4].Value);
                    txtTotal.Text = Convert.ToString(soma);
                }
                else
                {
                    MessageBox.Show("Produto já Cadastrado!!", "Produto Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Inserção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvVenda.Rows[e.RowIndex];
            cbxProduto.Text = row.Cells[1].Value.ToString();
            txtIdProduto.Text = row.Cells[0].Value.ToString();
            txtQuantidade.Text = row.Cells[2].Value.ToString();
            txtValor.Text = row.Cells[3].Value.ToString();
        }

        private void btnEditarItem_Click(object sender, EventArgs e)
        {
            if (cbxCliente.Text == "" || cbxProduto.Text == "" || txtValor.Text == "" || txtQuantidade.Text == "" || txtIdProduto.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvVenda.CurrentRow.Index;
                dgvVenda.Rows[linha].Cells[0].Value = txtIdProduto.Text;
                dgvVenda.Rows[linha].Cells[1].Value = cbxProduto.Text;
                dgvVenda.Rows[linha].Cells[2].Value = txtQuantidade.Text;
                dgvVenda.Rows[linha].Cells[3].Value = txtValor.Text;
                dgvVenda.Rows[linha].Cells[4].Value = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
                txtIdProduto.Text = "";
                txtValor.Text = "";
                txtQuantidade.Text = "";
                cbxProduto.Text = "";
                decimal soma = 0;
                foreach (DataGridViewRow dr in dgvVenda.Rows)
                    soma += Convert.ToDecimal(dr.Cells[4].Value);
                txtTotal.Text = Convert.ToString(soma);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (cbxCliente.Text == "" || cbxProduto.Text == "" || txtValor.Text == "" || txtQuantidade.Text == "" || txtIdProduto.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvVenda.CurrentRow.Index;
                dgvVenda.Rows.RemoveAt(linha);
                dgvVenda.Refresh();
                txtIdProduto.Text = "";
                txtValor.Text = "";
                txtQuantidade.Text = "";
                cbxProduto.Text = "";
                decimal soma = 0;
                foreach (DataGridViewRow dr in dgvVenda.Rows)
                    soma += Convert.ToDecimal(dr.Cells[4].Value);
                txtTotal.Text = Convert.ToString(soma);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("Quantidade_Produto", con);
            cmd.Parameters.AddWithValue("@Id", txtIdProduto.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            int valor1 = 0;
            bool conversaoSucedida = int.TryParse(txtQuantidade.Text, out valor1);
            if (dr.Read())
            {
                int valor2 = Convert.ToInt32(dr["quantidade"].ToString());
                if (valor1 > valor2)
                {
                    MessageBox.Show("Não há quantidade suficiente do produto!!", "Estoque insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantidade.Text = "";
                    txtQuantidade.Focus();
                }
                con.Close();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand lvenda = new SqlCommand("LocalizarVendido", con);
                lvenda.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text.Trim());
                lvenda.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter ven = new SqlDataAdapter(lvenda);
                DataTable dtven = new DataTable();
                ven.Fill(dtven);
                int linhasven = dtven.Rows.Count;
                if (linhasven > 0)
                {
                    cbxCliente.Enabled = true;
                    cbxCliente.Text = "";
                    cbxCliente.Text = dtven.Rows[0]["nomecliente"].ToString();
                    txtTotal.Text = dtven.Rows[0]["total"].ToString();
                    cbxProduto.Enabled = true;
                    txtQuantidade.Enabled = true;
                    txtValor.Enabled = true;                    
                    btnFinalizarVenda.Enabled = true;
                    btnNovoItem.Enabled = true;
                    btnEditarItem.Enabled = true;
                    btnExcluirItem.Enabled = true;
                    dgvVenda.Columns.Add("ID", "ID");
                    dgvVenda.Columns.Add("Produto", "Produto");
                    dgvVenda.Columns.Add("Quantidade", "Quantidade");
                    dgvVenda.Columns.Add("Valor", "Valor");
                    dgvVenda.Columns.Add("Total", "Total");
                    for (int i = 0; i < linhasven; i++)
                    {
                        DataGridViewRow itemven = new DataGridViewRow();
                        itemven.CreateCells(dgvVenda);
                        itemven.Cells[0].Value = dtven.Rows[i]["id_produto"].ToString();
                        itemven.Cells[1].Value = dtven.Rows[i]["nomeproduto"].ToString();
                        itemven.Cells[2].Value = dtven.Rows[i]["quantidade"].ToString();
                        itemven.Cells[3].Value = dtven.Rows[i]["valor_unitario"].ToString();
                        itemven.Cells[4].Value = dtven.Rows[i]["valor_total"].ToString();
                        dgvVenda.Rows.Add(itemven);
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nenhum pedido ou venda localizado.Por favor verifique o ID correto","Erro",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            foreach (DataGridViewRow dr in dgvVenda.Rows)
            {
                SqlCommand cmd = new SqlCommand("InserirVenda", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_cliente", SqlDbType.NChar).Value = cbxCliente.SelectedValue;
                cmd.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = Convert.ToDecimal(txtTotal.Text);
                cmd.Parameters.AddWithValue("@data_venda", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@situacao", SqlDbType.NChar).Value = "Pago";
                cmd.ExecuteNonQuery();
                string idvenda = "SELECT IDENT_CURRENT('Venda') AS id_venda";                
                SqlCommand cmd2 = new SqlCommand(idvenda, con);
                Int32 idvenda2 = Convert.ToInt32(cmd2.ExecuteScalar());
                SqlCommand itens = new SqlCommand("InserirItensVendidos", con);
                itens.CommandType = CommandType.StoredProcedure;
                itens.Parameters.AddWithValue("@id_venda", SqlDbType.Int).Value = idvenda2;
                itens.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[0].Value);
                itens.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[2].Value);
                itens.Parameters.AddWithValue("@valor_unitario", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[3].Value);
                itens.Parameters.AddWithValue("@valor_total", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[4].Value);
                itens.ExecuteNonQuery();
            }
            con.Close();
            MessageBox.Show("Venda finalizada com sucesso com sucesso!!", "Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvVenda.Columns.Clear();
            dgvVenda.Rows.Clear();
            txtId.Text = "";
            txtTotal.Text = "";
            txtValor.Text = "";
            txtQuantidade.Text = "";
            lblEstoque.Text = "";
            txtIdProduto.Text = "";
            cbxCliente.Text = "";
            cbxProduto.Text = "";
            cbxCliente.Enabled = false;
            cbxProduto.Enabled = false;
            txtIdProduto.Enabled = false;
            txtQuantidade.Enabled = false;
            txtValor.Enabled = false;
            txtTotal.Enabled = false;
            btnFinalizarVenda.Enabled = false;
            btnNovoItem.Enabled = false;
            btnEditarItem.Enabled = false;
            btnExcluirItem.Enabled = false;

        }
    }
}
