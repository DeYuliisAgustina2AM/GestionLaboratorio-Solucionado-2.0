using Controladora;
using Entidades;
using System.Text.RegularExpressions;

namespace VISTA
{
    public partial class formTecnicoAM : Form
    {
        private Tecnico tecnico; // variable de tipo Sede para almacenar la sede que se va a modificar
        private bool modificar = false;

        public formTecnicoAM()
        {
            InitializeComponent();
            tecnico = new Tecnico();
        }

        public formTecnicoAM(Tecnico tecnicoModificar)
        {
            InitializeComponent();
            tecnico = tecnicoModificar;
            modificar = true;
        }

        private void formTecnicoAM_Load(object sender, EventArgs e)
        {
            if (modificar)
            {
                lblAgregaroModificar.Text = "Modificar Tecnico";

                txtNombreyApellido.Text = tecnico.NombreyApellido;
                txtDni.Text = tecnico.Dni.ToString();
                txtLegajo.Text = tecnico.Legajo.ToString();
            }
            else lblAgregaroModificar.Text = "Agregar Tecnico";
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
            this.Hide();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                if (modificar)
                {
                    if (ControladoraTecnico.Instancia.RecuperarTecnicos().Any(t => t.NombreyApellido.ToLower() == txtNombreyApellido.Text.ToLower()))
                    {
                        // Mostrar un mensaje de error indicando que ya existe otro tecnico con ese código
                        MessageBox.Show("Ya existe otro tecnico con el mismo Nombre y Apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (ControladoraTecnico.Instancia.RecuperarTecnicos().Any(t => t.Dni == Convert.ToInt32(txtDni.Text)))
                    {
                        // Mostrar un mensaje de error indicando que ya existe otro tecnico con ese código
                        MessageBox.Show("Ya existe otro tecnico con el mismo DNI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (ControladoraTecnico.Instancia.RecuperarTecnicos().Any(t => t.Legajo == Convert.ToInt32(txtLegajo.Text)))
                    {
                        // Mostrar un mensaje de error indicando que ya existe otro tecnico con ese código
                        MessageBox.Show("Ya existe otro tecnico con el mismo legajo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (ValidaDNI(txtDni.Text) == false)
                    {
                        MessageBox.Show("El DNI ingresado no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    tecnico.NombreyApellido = txtNombreyApellido.Text;
                    tecnico.Dni = Convert.ToInt32(txtDni.Text);
                    tecnico.Legajo = Convert.ToInt32(txtLegajo.Text);

                    var mensaje = ControladoraTecnico.Instancia.ModificarTecnico(tecnico);
                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    if (ControladoraTecnico.Instancia.RecuperarTecnicos().Any(t => t.NombreyApellido.ToLower() == txtNombreyApellido.Text.ToLower()))
                    {
                        // Mostrar un mensaje de error indicando que ya existe otro tecnico con ese código
                        MessageBox.Show("Ya existe otro tecnico con el mismo Nombre y Apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (ControladoraTecnico.Instancia.RecuperarTecnicos().Any(t => t.Dni == Convert.ToInt32(txtDni.Text)))
                    {
                        // Mostrar un mensaje de error indicando que ya existe otro tecnico con ese código
                        MessageBox.Show("Ya existe otro tecnico con el mismo DNI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (ControladoraTecnico.Instancia.RecuperarTecnicos().Any(t => t.Legajo == Convert.ToInt32(txtLegajo.Text)))
                    {
                        // Mostrar un mensaje de error indicando que ya existe otro tecnico con ese código
                        MessageBox.Show("Ya existe otro tecnico con el mismo legajo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (ValidaDNI(txtDni.Text) == false)
                    {
                        MessageBox.Show("El DNI ingresado no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    tecnico.NombreyApellido = txtNombreyApellido.Text;
                    tecnico.Dni = Convert.ToInt32(txtDni.Text);
                    tecnico.Legajo = Convert.ToInt32(txtLegajo.Text);


                    var mensaje = ControladoraTecnico.Instancia.AgregarTecnico(tecnico);
                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }

        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombreyApellido.Text))
            {
                MessageBox.Show("Ingrese el nombre y apellido del tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombreyApellido.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Ingrese el DNI del tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDni.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLegajo.Text))
            {
                MessageBox.Show("Ingrese el legajo del tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLegajo.Focus();
                return false;
            }
            return true;
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPressSoloNumeros(e).Handled)
            {
                MessageBox.Show("Solo se permiten números", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLegajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPressSoloNumeros(e).Handled)
            {
                MessageBox.Show("Solo se permiten números", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtNombreyApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPressSoloLetras(e, txtNombreyApellido.Text).Handled)
            {
                MessageBox.Show("Solo se permiten letras", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        static public bool ValidaDNI(string dni)
        {

            if (Regex.Match(dni, @"^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$").Success == true)
            {
                //dni correcto
                return true;
            }
            else
            {
                //dni incorrecto
                return false;
            }
        }

        static public KeyPressEventArgs KeyPressSoloNumeros(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            return e;
        }

        static public KeyPressEventArgs KeyPressSoloLetras(KeyPressEventArgs e, string TEXTO)
        {

            if (Char.IsLetter(e.KeyChar))//solo letras
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))//solo backspace, space, delete, etc.
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))// solo .:;,_- (simbolos de punt
            {
                e.Handled = false;
            }
            else if (Char.IsSymbol(e.KeyChar))//demas caracteres
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))//espacio
            {
                e.Handled = false;
            }
            else//que el resto no lo escriba
            {
                e.Handled = true;
            }
            return e;
        }
    }
}
