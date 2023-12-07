using LojaGeek.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LojaGeek
{
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCEP_Click(object sender, EventArgs e)
        {
            BuscaCep cep = new BuscaCep();
            cep.Busca(mkCEP.Text);
            txtEndereco.Text = cep.endereco.ToString();
            txtBairro.Text = cep.bairro.ToString();
            txtCidade.Text = cep.cidade.ToString();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            List<Cliente> clientes = cliente.listaCliente();
            dgvCliente.DataSource = clientes;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            this.ActiveControl = txtNome;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || mkCPF.Text == "" || mkCelular.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cliente cliente = new Cliente();
                if (cliente.RegistroRepetido(txtNome.Text, mkCPF.Text) == true)
                {
                    MessageBox.Show("Cliente já existe em nossa base de dados!, utilizar cadastro existente", "Cliente Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Text = "";
                    mkCPF.Text = "";
                    txtEndereco.Text = "";
                    txtComplemento.Text = "";
                    txtBairro.Text = "";
                    txtCidade.Text = "";
                    mkCEP.Text = "";
                    mkCelular.Text = "";                    
                    return;
                }
                else
                {
                    cliente.Inserir(txtNome.Text, mkCPF.Text, txtEndereco.Text, txtBairro.Text, txtCidade.Text, mkCelular.Text, mkCEP.Text,txtComplemento.Text);
                    MessageBox.Show("Cliente inserido com sucesso!", "Inserção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Cliente> clientes =    cliente.listaCliente();
                    dgvCliente.DataSource = clientes;
                    txtNome.Text = "";
                    mkCPF.Text = "";
                    txtEndereco.Text = "";
                    txtComplemento.Text = "";
                    txtBairro.Text = "";
                    txtCidade.Text = "";
                    mkCEP.Text = "";
                    mkCelular.Text = "";                    
                    this.txtNome.Focus();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Inserção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Atualizar(Id, txtNome.Text, mkCPF.Text, txtEndereco.Text, txtBairro.Text, txtCidade.Text, mkCelular.Text, mkCEP.Text, txtComplemento.Text);
                MessageBox.Show("Cliente atualizado com sucesso!", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> clientes = cliente.listaCliente();
                dgvCliente.DataSource = clientes;
                txtID.Text = "";
                txtNome.Text = "";
                mkCPF.Text = "";
                txtEndereco.Text = "";
                txtComplemento.Text = "";
                txtBairro.Text = "";
                txtCidade.Text = "";
                mkCEP.Text = "";
                mkCelular.Text = "";  
                this.ActiveControl = txtNome;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Excluir(Id);
                MessageBox.Show("Cliente excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> clientes = cliente.listaCliente();
                dgvCliente.DataSource = clientes;
                txtID.Text = "";
                txtNome.Text = "";
                mkCPF.Text = "";
                txtEndereco.Text = "";
                txtComplemento.Text = "";
                txtBairro.Text = "";
                txtCidade.Text = "";
                mkCEP.Text = "";
                mkCelular.Text = "";                
                this.ActiveControl = txtNome;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Por favor, digite um ID para localizar!", "Sem ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                int Id = Convert.ToInt32(txtID.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Localizar(Id);
                txtNome.Text = cliente.nome;
                mkCPF.Text = cliente.cpf;
                txtEndereco.Text = cliente.endereco;
                txtComplemento.Text = cliente.complemento;
                txtBairro.Text = cliente.bairro;
                txtCidade.Text = cliente.cidade;
                mkCEP.Text = cliente.cep;
                mkCelular.Text = cliente.celular;                
                if (txtNome.Text != null)
                {
                    btnEditar.Enabled = true;
                    btnExcluir.Enabled = true;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Localização", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvCliente.Rows[e.RowIndex];
                this.dgvCliente.Rows[e.RowIndex].Selected = true;
                txtID.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                mkCPF.Text = row.Cells[2].Value.ToString();
                txtEndereco.Text = row.Cells[3].Value.ToString();
                txtBairro.Text = row.Cells[3].Value.ToString();
                txtCidade.Text = row.Cells[3].Value.ToString();
                mkCelular.Text = row.Cells[3].Value.ToString();
                mkCEP.Text = row.Cells[3].Value.ToString();                
                txtComplemento.Text = row.Cells[3].Value.ToString();
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
