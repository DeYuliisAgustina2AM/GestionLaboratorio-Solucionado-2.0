using Controladora;
using Entidades;

namespace VISTA
{
    public partial class formLaboratorioAM : Form
    {
        private Laboratorio laboratorio; // variable de tipo Sede para almacenar la sede que se va a modificar
        private bool modificar = false;

        public formLaboratorioAM()
        {
            InitializeComponent();
            laboratorio = new Laboratorio();
            ActualizarCb();
        }

        public formLaboratorioAM(Laboratorio laboratorioModificar)
        {
            InitializeComponent();
            laboratorio = laboratorioModificar;
            modificar = true;
            ActualizarCb();
        }

        public void ActualizarCb()
        {
            foreach (Sede sede in ControladoraSede.Instancia.RecuperarSedes()) // se recorren las sedes para agregarlas al combobox de sedes para que el usuario pueda seleccionar una sede para el laboratorio que se va a modificar
            {
                cbSedes.Items.Add(sede.NombreSede.ToString());
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formLaboratorioAM_Load(object sender, EventArgs e)
        {
            if (modificar)
            {
                lblAgregaroModificar.Text = "Modificar Laboratorio";

                txtNombreLaboratorio.Text = laboratorio.NombreLaboratorio.ToString();
                cbSedes.SelectedItem = laboratorio.Sede.NombreSede.ToString(); // se selecciona la sede del laboratorio que se va a modificar en el combobox de sedes
                numCapacidad.Value = laboratorio.CapacidadMaxima;

            }
            else lblAgregaroModificar.Text = "Agregar Laboratorio";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                if (modificar)
                {
                    string NombreSede = cbSedes.Text; // se recupera el nombre de la sede seleccionada del combobox de sedes
                    if (ControladoraLaboratorio.Instancia.RecuperarLaboratorios().Any(l => l.Sede.NombreSede.ToLower() == NombreSede.ToLower() && l.NombreLaboratorio.ToLower() == txtNombreLaboratorio.Text.ToLower() && l.LaboratorioId != laboratorio.LaboratorioId))
                    {
                        MessageBox.Show("Ya existe un laboratorio con ese nombre en la sede seleccionada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    laboratorio.Sede = ControladoraSede.Instancia.RecuperarSedes().FirstOrDefault(s => s.NombreSede.ToLower() == NombreSede.ToLower()); // se recupera la sede seleccionada del combobox de sedes para asignarla al laboratorio que se va a modificar
                    laboratorio.CapacidadMaxima = (int)numCapacidad.Value;
                    laboratorio.NombreLaboratorio = txtNombreLaboratorio.Text;

                    var mensaje = ControladoraLaboratorio.Instancia.ModificarLaboratorio(laboratorio);
                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    string NombreSede = cbSedes.Text; // se recupera el nombre de la sede seleccionada del combobox de sedes
                    laboratorio.Sede = ControladoraSede.Instancia.RecuperarSedes().FirstOrDefault(s => s.NombreSede.ToLower() == NombreSede.ToLower()); // se recupera la sede seleccionada del combobox de sedes para asignarla al laboratorio que se va a modificar
                    laboratorio.CapacidadMaxima = (int)numCapacidad.Value;
                    laboratorio.NombreLaboratorio = txtNombreLaboratorio.Text;

                    var mensaje = ControladoraLaboratorio.Instancia.AgregarLaboratorio(laboratorio);
                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombreLaboratorio.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de laboratorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbSedes.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una sede para agregar el laboratorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (numCapacidad.Value == 0)
            {
                MessageBox.Show("Debe ingresar un número de capacidad máxima de computadoras para el laboratorio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

    }
}
