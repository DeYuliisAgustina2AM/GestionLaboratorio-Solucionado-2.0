using Controladora;
using Entidades;

namespace VISTA
{
    public partial class formTecnicoDGV : Form
    {
        public formTecnicoDGV()
        {
            InitializeComponent();
            ActualizarGrilla();
            dgvTecnico.AllowUserToResizeColumns = false;
        }

        private void ActualizarGrilla()
        {
            dgvTecnico.DataSource = null;
            dgvTecnico.DataSource = ControladoraTecnico.Instancia.RecuperarTecnicos();
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
            Form formTecnicoAM = new formTecnicoAM();
            formTecnicoAM.ShowDialog();
            ActualizarGrilla();
        }

        private void formTecnicoDGV_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTecnico.Rows.Count > 0)
            {
                var tecnicoSeleccionado = (Tecnico)dgvTecnico.CurrentRow.DataBoundItem;
                var mensaje = ControladoraTecnico.Instancia.EliminarTecnico(tecnicoSeleccionado);
                MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarGrilla();
            }
            else
            {
                MessageBox.Show("Seleccione un tecnico para eliminarlo");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvTecnico.Rows.Count > 0)
            {
                var tecnicoSeleccionado = (Tecnico)dgvTecnico.CurrentRow.DataBoundItem;
                formTecnicoAM formTecnicoAM = new formTecnicoAM(tecnicoSeleccionado);
                formTecnicoAM.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un tecnico para modificarlo");
            }
            ActualizarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarTecnico.Text != "")
            {
                var listaTecnicos = ControladoraTecnico.Instancia.RecuperarTecnicos();
                var tecnicoEncontrado = listaTecnicos.FirstOrDefault(t => t.NombreyApellido.ToLower().Contains(txtBuscarTecnico.Text.ToLower()) || t.Dni.ToString().Contains(txtBuscarTecnico.Text) || t.Legajo.ToString().Contains(txtBuscarTecnico.Text));
                if (tecnicoEncontrado != null)
                {
                    dgvTecnico.DataSource = null;
                    dgvTecnico.DataSource = new List<Tecnico> { tecnicoEncontrado };
                }
                else
                {
                    MessageBox.Show("No se ha encontrado el tecnico");
                    ActualizarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un nombre y apellido, dni o legajo para buscar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
