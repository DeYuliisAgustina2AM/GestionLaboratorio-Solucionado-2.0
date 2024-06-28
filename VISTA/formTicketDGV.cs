using Controladora;
using Entidades;

namespace VISTA
{
    public partial class formTicketDGV : Form
    {
        public formTicketDGV()
        {
            InitializeComponent();
            ActualizarGrilla();
            dgvTicket.AllowUserToResizeColumns = false;

        }

        private void ActualizarGrilla()
        {
            dgvTicket.DataSource = null;
            dgvTicket.DataSource = ControladoraTicket.Instancia.RecuperarTicket();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void formHistorialDGV_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form formTicketAM = new formTicketAM();
            formTicketAM.ShowDialog();
            ActualizarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTicket.Rows.Count > 0)
            {
                var ticketSelecionado = (Ticket)dgvTicket.CurrentRow.DataBoundItem;
                var respuesta = ControladoraTicket.Instancia.EliminarTicket(ticketSelecionado);
                MessageBox.Show(respuesta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarGrilla();
            }
            else
            {
                MessageBox.Show("Seleccione un ticket para eliminarlo");
            }
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvTicket.Rows.Count > 0)
            {
                var ticketSeleccionado = (Ticket)dgvTicket.CurrentRow.DataBoundItem;
                formTicketAM formTicketAM = new formTicketAM(ticketSeleccionado);
                formTicketAM.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un ticket para modificarlo");
            }
            ActualizarGrilla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarTicket.Text != "")
            {
                var listaTicket = ControladoraTicket.Instancia.RecuperarTicket();
                var ticketEncontrado = listaTicket.FirstOrDefault(t => t.Computadora.CodigoComputadora.ToLower().Contains(txtBuscarTicket.Text.ToLower()) || t.TicketId.ToString().Contains(txtBuscarTicket.Text.ToLower()) || t.estado.ToString().ToLower().Contains(txtBuscarTicket.Text.ToLower()) || t.FechaCreacion.ToString().ToLower().Contains(txtBuscarTicket.Text.ToLower()) || t.tipo.ToString().ToLower().Contains(txtBuscarTicket.Text.ToLower()) || t.Tecnico.NombreyApellido.ToLower().Contains(txtBuscarTicket.Text.ToLower()));
                if (ticketEncontrado != null)
                {
                    dgvTicket.DataSource = null;
                    dgvTicket.DataSource = new List<Ticket> { ticketEncontrado };
                }
                else
                {
                    MessageBox.Show("No se ha encontrado el ticket");
                    ActualizarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un código de computadora,fecha de creacion, tipo de ticket, id de ticket, nombre o apellido de técnico para buscar un ticket", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
