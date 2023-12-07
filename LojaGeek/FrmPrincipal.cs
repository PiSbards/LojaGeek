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

namespace LojaGeek
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            FrmSplash splash = new FrmSplash();
            splash.Show();
            Application.DoEvents();
            Thread.Sleep(3000);
            splash.Close();
        }

        private void pbxSair_Click(object sender, EventArgs e)
        {
            var a = MessageBox.Show("Deseja realmente sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = MessageBox.Show("Deseja realmente sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void pbxProduto_Click(object sender, EventArgs e)
        {
            FrmProduto produto = new FrmProduto();
            produto.Show();
        }

        private void pbxCliente_Click(object sender, EventArgs e)
        {
            FrmCliente cliente = new FrmCliente();
            cliente.Show();
        }

        private void pbxVenda_Click(object sender, EventArgs e)
        {
            FrmVenda venda = new FrmVenda();
            venda.Show();
        }

        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProduto produto = new FrmProduto();
            produto.Show();
        }

        private void cadastroClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente cliente = new FrmCliente();
            cliente.Show();
        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVenda venda = new FrmVenda();
            venda.Show();
        }

    }
}
