using Controladora;
using Entidades;

namespace VISTA
{
    public partial class formComputadoraDGV : Form
    {
        public formComputadoraDGV()
        {
            InitializeComponent();
            ActualizarGrilla();
            dgvComputadora.AllowUserToResizeColumns = false;
        }

        private void ActualizarGrilla()
        {
            dgvComputadora.DataSource = null;
            dgvComputadora.DataSource = ControladoraComputadora.Instancia.RecuperarComputadoras();
            dgvComputadora.Columns["Laboratorio"].Visible = false;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form formComputadoraAM = new formComputadoraAM();
            formComputadoraAM.ShowDialog();
            ActualizarGrilla();
        }

        private void formComputadoraDGV_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvComputadora.Rows.Count > 0)
            {
                var computadoraSeleccionada = (Computadora)dgvComputadora.CurrentRow.DataBoundItem;
                var mensaje = ControladoraComputadora.Instancia.EliminarComputadora(computadoraSeleccionada);
                MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarGrilla();
            }
            else
            {
                MessageBox.Show("Seleccione una computadora para eliminarla");
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvComputadora.Rows.Count > 0)
            {
                var computadoraSeleccionada = (Computadora)dgvComputadora.CurrentRow.DataBoundItem;
                formComputadoraAM formComputadoraAM = new formComputadoraAM(computadoraSeleccionada);
                formComputadoraAM.ShowDialog();
                ActualizarGrilla();
            }
            else
            {
                MessageBox.Show("Seleccione una computadora para modificarla");
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (txtBuscarComputadora.Text != "")
            {
                var listaComputadora = ControladoraComputadora.Instancia.RecuperarComputadoras();
                var computadoraEncontrada = listaComputadora.FirstOrDefault(c => c.CodigoComputadora.ToLower().Contains(txtBuscarComputadora.Text.ToLower()));
                if (computadoraEncontrada != null)
                {
                    dgvComputadora.DataSource = null;
                    dgvComputadora.DataSource = new List<Computadora> { computadoraEncontrada };
                }
                else
                {
                    MessageBox.Show("No se ha encontrado la computadora");
                    ActualizarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un código de computadora para buscar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
