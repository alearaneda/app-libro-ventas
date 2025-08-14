using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FINegocio;
using FINegocio.FrmNegocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace AppLibroVentas
{
    public partial class Form1 : Form
    {
        FrmMenu menu;
        #region "Propiedades"
        private Guid ID { get; set; }
        private Guid IDCompra { get; set; }
        private Guid IDGasto { get; set; }
        private List<FrmVenta> listaVentasChart = new List<FrmVenta>();
        private List<FrmCompra> listaComprasChart = new List<FrmCompra>();
        private List<FrmGasto> listaGastosChart = new List<FrmGasto>();
        #endregion

        #region "Eventos"
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menu = new FrmMenu();

            #region "Ventas"
            lblRangioBoleta.Visible = false;
            nRangoBoletaDesde.Visible = false;
            nRangoBoletaHasta.Visible = false;
            cargarGrillaVentas();
            if (dtpFiltroDia.Value != null)
                CargarVentasPorDia(dtpFiltroDia.Value);
            if (dtpFiltroMes.Value != null)
                CargarVentasPorMes(dtpFiltroMes.Value);
            #endregion

            #region "Compras"
            lblRangoFactura.Visible = false;
            nRangoFacturaDesde.Visible = false;
            nRangoFacturaHasta.Visible = false;
            lblRutFactura.Visible = false;
            txtRutFactura.Visible = false;
            cargarGrillaCompras();
            if (dtpFiltrarDiaCompra.Value != null)
                CargarComprasPorDia(dtpFiltrarDiaCompra.Value);
            if (dtpFiltrarMesCompra.Value != null)
                CargarComprasPorMes(dtpFiltrarMesCompra.Value);
            #endregion

            #region "Gastos"
            cargarGrillaGastos();
            if (dtpFiltraDiaGasto.Value != null)
                CargarGastosPorDia(dtpFiltraDiaGasto.Value);
            if (dtpFiltraMesGasto.Value != null)
                CargarGastosPorMes(dtpFiltraMesGasto.Value);
            #endregion

            #region "Inversion"
            cargarGrillasInversion();
            #endregion
        }

        #region "Ventas"
        private void btnIngresaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                string monto = string.Empty;
                FIVentas ventas = new FIVentas();
                FrmVenta frmV = new FrmVenta();
                if (!string.IsNullOrEmpty(txtDescripcionVenta.Text) && !string.IsNullOrEmpty(txtMontoVenta.Value.ToString()))
                {
                    frmV.DescripcionVenta = txtDescripcionVenta.Text;
                    frmV.FechaInicioVenta = dtFechaInicioVenta.Value;
                    frmV.FechaTerminoVenta = dtFechaInicioVenta.Value;
                    monto = txtMontoVenta.Value.ToString().Replace(".", "").Replace("$", "");
                    frmV.MontoVenta = Convert.ToInt32(monto);
                    frmV.VentaConBoleta = chkConBoleta.Checked;
                    if (frmV.VentaConBoleta)
                    {
                        frmV.NumeroBoletaInicio = nRangoBoletaDesde.Value.ToString() != "" ? Convert.ToInt32(nRangoBoletaDesde.Value) : 0;
                        frmV.NumeroBoletaTermino = !String.IsNullOrEmpty(nRangoBoletaHasta.Value.ToString()) ? Convert.ToInt32(nRangoBoletaHasta.Value) : 0;
                    }
                    else
                    {
                        frmV.NumeroBoletaInicio = 0;
                        frmV.NumeroBoletaTermino = 0;
                    }

                    frmV.IdVenta = (Guid)ventas.RegistrarVenta(frmV);
                    if (frmV.IdVenta != Guid.Empty)
                    {
                        cargarGrillaVentas();
                        if (dtpFiltroDia.Value != null)
                            CargarVentasPorDia(dtpFiltroDia.Value);
                        if (dtpFiltroMes.Value != null)
                            CargarVentasPorMes(dtpFiltroMes.Value);
                        limpiarCamposVenta();
                        MessageBox.Show("Venta registrada correctamente!", "Registrar Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró registrar la venta.", "Registrar Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe completar todos los campos antes de registrar la venta.", "Registrar Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Registrar Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkConBoleta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConBoleta.Checked)
            {
                lblRangioBoleta.Visible = true;
                nRangoBoletaDesde.Visible = true;
                nRangoBoletaHasta.Visible = true;
            }
            else
            {
                lblRangioBoleta.Visible = false;
                nRangoBoletaDesde.Visible = false;
                nRangoBoletaHasta.Visible = false;
            }
        }

        private void gvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = Guid.Parse(gvVentas.Rows[e.RowIndex].Cells[0].Value.ToString());
                dtFechaInicioVenta.Value = Convert.ToDateTime(gvVentas.Rows[e.RowIndex].Cells[3].Value);
                txtDescripcionVenta.Text = gvVentas.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtMontoVenta.Value = Convert.ToInt32(gvVentas.Rows[e.RowIndex].Cells[2].Value);
                chkConBoleta.Checked = Convert.ToBoolean(gvVentas.Rows[e.RowIndex].Cells[4].Value);
                nRangoBoletaDesde.Value = Convert.ToInt32(gvVentas.Rows[e.RowIndex].Cells[5].Value);
                nRangoBoletaHasta.Value = Convert.ToInt32(gvVentas.Rows[e.RowIndex].Cells[6].Value);
                btnModificarVenta.Enabled = true;
                btnEliminarVenta.Enabled = true;
                btnLimpiarVenta.Enabled = true;
                btnIngresaVenta.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Editar Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = false;
                string monto = string.Empty;
                FIVentas ventas = new FIVentas();
                FrmVenta frmV = new FrmVenta();
                if (!string.IsNullOrEmpty(txtDescripcionVenta.Text) && !string.IsNullOrEmpty(txtMontoVenta.Value.ToString()))
                {
                    frmV.IdVenta = ID;
                    frmV.DescripcionVenta = txtDescripcionVenta.Text;
                    frmV.FechaInicioVenta = dtFechaInicioVenta.Value;
                    frmV.FechaTerminoVenta = dtFechaInicioVenta.Value;
                    monto = txtMontoVenta.Value.ToString().Replace(".", "").Replace("$", "");
                    frmV.MontoVenta = Convert.ToInt32(monto);
                    frmV.VentaConBoleta = chkConBoleta.Checked;
                    if (frmV.VentaConBoleta)
                    {
                        frmV.NumeroBoletaInicio = !String.IsNullOrEmpty(nRangoBoletaDesde.Value.ToString()) ? Convert.ToInt32(nRangoBoletaDesde.Value) : 0;
                        frmV.NumeroBoletaTermino = !String.IsNullOrEmpty(nRangoBoletaHasta.Value.ToString()) ? Convert.ToInt32(nRangoBoletaHasta.Value) : 0;
                    }
                    else
                    {
                        frmV.NumeroBoletaInicio = 0;
                        frmV.NumeroBoletaTermino = 0;
                    }

                    bandera = ventas.ModificarVenta(frmV);
                    if (bandera)
                    {
                        cargarGrillaVentas();
                        if (dtpFiltroDia.Value != null)
                            CargarVentasPorDia(dtpFiltroDia.Value);
                        if (dtpFiltroMes.Value != null)
                            CargarVentasPorMes(dtpFiltroMes.Value);
                        limpiarCamposVenta();
                        MessageBox.Show("Venta modificada correctamente!", "Modificar Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró modificar la venta.", "Modificar Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe completar todos los campos antes de modificar la venta.", "Modificar Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Modificar Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = false;
                FIVentas ventas = new FIVentas();
                if (ID != Guid.Empty)
                {
                    bandera = ventas.EliminarVenta(ID);
                    if (bandera)
                    {
                        cargarGrillaVentas();
                        if (dtpFiltroDia.Value != null)
                            CargarVentasPorDia(dtpFiltroDia.Value);
                        if (dtpFiltroMes.Value != null)
                            CargarVentasPorMes(dtpFiltroMes.Value);
                        limpiarCamposVenta();
                        MessageBox.Show("Venta eliminada correctamente!", "Eliminar Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró eliminar la venta.", "Eliminar Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe seleccionar el registro que quiere eliminar.", "Eliminar Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Eliminar Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCamposVenta();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Limpiar Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltroDia_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFiltroDia.Value != null)
                    CargarVentasPorDia(dtpFiltroDia.Value);
                else
                    MessageBox.Show("No se logró obtener montos de venta según día seleccionado", "Filtro día", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Filtro día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltroMes_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFiltroMes.Value != null)
                    CargarVentasPorMes(dtpFiltroMes.Value);
                else
                    MessageBox.Show("No se logró obtener montos de venta según mes seleccionado", "Filtro mes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Filtro mes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FIVentas ventas = new FIVentas();
                List<FrmVenta> lista = new List<FrmVenta>();
                if (chkBuscarMes.Checked)
                    lista = ventas.ListaVentasPorMes(dtpBuscar.Value);
                else
                    lista = ventas.ListaVentasPorDia(dtpBuscar.Value);

                if (lista != null && lista.Count > 0)
                    gvVentas.DataSource = lista;
                else
                    gvVentas.DataSource = null;

                chkBuscarMes.Checked = false;
                btnCancelarBusqueda.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Buscar Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                cargarGrillaVentas();
                btnCancelarBusqueda.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Cancelar Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Compras"
        private void btnIngresarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                string monto = string.Empty;
                FICompras compras = new FICompras();
                FrmCompra frmC = new FrmCompra();
                if (!string.IsNullOrEmpty(txtDescripcionCompra.Text) && !string.IsNullOrEmpty(nMontoCompra.Value.ToString()))
                {
                    frmC.DescripcionCompra = txtDescripcionCompra.Text;
                    frmC.FechaInicioCompra = dtpFechaCompra.Value;
                    frmC.FechaTerminoCompra = null;
                    monto = nMontoCompra.Value.ToString().Replace(".", "").Replace("$", "");
                    frmC.MontoCompra = Convert.ToInt32(monto);
                    frmC.CompraConFactura = chkConFactura.Checked;
                    if (frmC.CompraConFactura)
                    {
                        frmC.CompraFacturaInicio = nRangoFacturaDesde.Value.ToString() != "" ? Convert.ToInt32(nRangoFacturaDesde.Value) : 0;
                        frmC.CompraFacturaTermino = 0;
                        frmC.CompraRutFactura = txtRutFactura.Text;
                    }
                    else
                    {
                        frmC.CompraFacturaInicio = 0;
                        frmC.CompraFacturaTermino = 0;
                        frmC.CompraRutFactura = "";
                    }
                    frmC.EsInversion = false;

                    frmC.IdCompra = (Guid)compras.RegistrarCompra(frmC);
                    if (frmC.IdCompra != Guid.Empty)
                    {
                        cargarGrillaCompras();
                        if (dtpFiltrarDiaCompra.Value != null)
                            CargarComprasPorDia(dtpFiltrarDiaCompra.Value);
                        if (dtpFiltrarMesCompra.Value != null)
                            CargarComprasPorMes(dtpFiltrarMesCompra.Value);
                        limpiarCamposCompra();
                        MessageBox.Show("Compra registrada correctamente!", "Registrar Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró registrar la compra.", "Registrar Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe completar todos los campos antes de registrar la compra.", "Registrar Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Registrar Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = false;
                string monto = string.Empty;
                FICompras compras = new FICompras();
                FrmCompra frmC = new FrmCompra();
                if (!string.IsNullOrEmpty(txtDescripcionCompra.Text) && !string.IsNullOrEmpty(nMontoCompra.Value.ToString()))
                {
                    frmC.IdCompra = IDCompra;
                    frmC.DescripcionCompra = txtDescripcionCompra.Text;
                    frmC.FechaInicioCompra = dtpFechaCompra.Value;
                    if (chkRegistrarFactura.Checked)
                        frmC.FechaTerminoCompra = dtpFechaFactura.Value;
                    else
                        frmC.FechaTerminoCompra = null;
                    monto = nMontoCompra.Value.ToString().Replace(".", "").Replace("$", "");
                    frmC.MontoCompra = Convert.ToInt32(monto);
                    frmC.CompraConFactura = chkConFactura.Checked;
                    if (frmC.CompraConFactura)
                    {
                        frmC.CompraFacturaInicio = !String.IsNullOrEmpty(nRangoFacturaDesde.Value.ToString()) ? Convert.ToInt32(nRangoFacturaDesde.Value) : 0;
                        frmC.CompraFacturaTermino = !String.IsNullOrEmpty(nRangoFacturaHasta.Value.ToString()) ? Convert.ToInt32(nRangoFacturaHasta.Value) : 0;
                        frmC.CompraRutFactura = txtRutFactura.Text;
                    }
                    else
                    {
                        frmC.CompraFacturaInicio = 0;
                        frmC.CompraFacturaTermino = 0;
                        frmC.CompraRutFactura = "";
                    }

                    bandera = compras.ModificarCompra(frmC);
                    if (bandera)
                    {
                        cargarGrillaCompras();
                        if (dtpFiltrarDiaCompra.Value != null)
                            CargarComprasPorDia(dtpFiltrarDiaCompra.Value);
                        if (dtpFiltrarMesCompra.Value != null)
                            CargarComprasPorMes(dtpFiltrarMesCompra.Value);
                        limpiarCamposCompra();
                        MessageBox.Show("Compra modificada correctamente!", "Modificar Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró modificar la compra.", "Modificar Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe completar todos los campos antes de modificar la compra.", "Modificar Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Modificar Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = false;
                FICompras compras = new FICompras();
                if (IDCompra != Guid.Empty)
                {
                    bandera = compras.EliminarCompra(IDCompra);
                    if (bandera)
                    {
                        cargarGrillaCompras();
                        if (dtpFiltrarDiaCompra.Value != null)
                            CargarComprasPorDia(dtpFiltrarDiaCompra.Value);
                        if (dtpFiltrarMesCompra.Value != null)
                            CargarComprasPorMes(dtpFiltrarMesCompra.Value);
                        limpiarCamposCompra();
                        MessageBox.Show("Compra eliminada correctamente!", "Eliminar Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró eliminar la compra.", "Eliminar Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe seleccionar el registro que quiere eliminar.", "Eliminar Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Eliminar Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCamposCompra();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Limpiar Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                FICompras compras = new FICompras();
                List<FrmCompra> lista = new List<FrmCompra>();
                if (chkMesCompra.Checked)
                    lista = compras.ListaComprasPorMes(dtpFechaBuscarCompra.Value);
                else
                    lista = compras.ListaComprasPorDia(dtpFechaBuscarCompra.Value);

                if (lista != null && lista.Count > 0)
                    gvCompras.DataSource = lista;
                else
                    gvCompras.DataSource = null;

                chkMesCompra.Checked = false;
                btnCancelarCompra.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Buscar Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                cargarGrillaCompras();
                btnCancelarCompra.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Cancelar Busqueda Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrarDia_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFiltrarDiaCompra.Value != null)
                    CargarComprasPorDia(dtpFiltrarDiaCompra.Value);
                else
                    MessageBox.Show("No se logró obtener montos de compra según día seleccionado", "Filtro día", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Filtro día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrarMes_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFiltrarMesCompra.Value != null)
                    CargarComprasPorMes(dtpFiltrarMesCompra.Value);
                else
                    MessageBox.Show("No se logró obtener montos de compra según mes seleccionado", "Filtro mes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Filtro mes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkConFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConFactura.Checked)
            {
                lblRangoFactura.Visible = true;
                nRangoFacturaDesde.Visible = true;
                lblRutFactura.Visible = true;
                txtRutFactura.Visible = true;
                chkRegistrarFactura.Visible = true;   
            }
            else
            {
                lblRangoFactura.Visible = false;
                nRangoFacturaDesde.Visible = false;
                lblRutFactura.Visible = false;
                txtRutFactura.Visible = false;
                chkRegistrarFactura.Visible = false;
            }
        }

        private void gvCompras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IDCompra = Guid.Parse(gvCompras.Rows[e.RowIndex].Cells[0].Value.ToString());
                dtpFechaCompra.Value = Convert.ToDateTime(gvCompras.Rows[e.RowIndex].Cells[3].Value);
                dtpFechaFactura.Value = gvCompras.Rows[e.RowIndex].Cells[6].Value != null ? Convert.ToDateTime(gvCompras.Rows[e.RowIndex].Cells[6].Value) : DateTime.Now;
                txtDescripcionCompra.Text = gvCompras.Rows[e.RowIndex].Cells[1].Value.ToString();
                nMontoCompra.Value = Convert.ToInt32(gvCompras.Rows[e.RowIndex].Cells[2].Value);
                chkConFactura.Checked = Convert.ToBoolean(gvCompras.Rows[e.RowIndex].Cells[5].Value);
                nRangoFacturaDesde.Value = Convert.ToInt32(gvCompras.Rows[e.RowIndex].Cells[4].Value);
                nRangoFacturaHasta.Value = Convert.ToInt32(gvCompras.Rows[e.RowIndex].Cells[7].Value);
                txtRutFactura.Text = gvCompras.Rows[e.RowIndex].Cells[8].Value.ToString();
                if (nRangoFacturaHasta.Value > 0 && dtpFechaFactura.Value != null)
                    chkRegistrarFactura.Checked = true;
                else
                    chkRegistrarFactura.Checked = false;
                btnModificarCompra.Enabled = true;
                btnEliminarCompra.Enabled = true;
                btnLimpiarCompra.Enabled = true;
                btnIngresarCompra.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Editar Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Gastos"
        private void btnIngresaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                string monto = string.Empty;
                FIGastos gastos = new FIGastos();
                FrmGasto frmG = new FrmGasto();
                if (!string.IsNullOrEmpty(txtDescripcionGasto.Text) && !string.IsNullOrEmpty(nMontoGasto.Value.ToString()))
                {
                    frmG.DescripcionGasto = txtDescripcionGasto.Text;
                    frmG.FechaInicioGasto = dtpFechaGasto.Value;
                    frmG.FechaTerminoGasto = dtpFechaGasto.Value;
                    monto = nMontoGasto.Value.ToString().Replace(".", "").Replace("$", "");
                    frmG.MontoGasto = Convert.ToInt32(monto);
                    frmG.EsInversionGasto = false;

                    frmG.IdGasto = (Guid)gastos.RegistrarGasto(frmG);
                    if (frmG.IdGasto != Guid.Empty)
                    {
                        cargarGrillaGastos();
                        if (dtpFiltraDiaGasto.Value != null)
                            CargarGastosPorDia(dtpFiltraDiaGasto.Value);
                        if (dtpFiltraMesGasto.Value != null)
                            CargarGastosPorMes(dtpFiltraMesGasto.Value);
                        limpiarGastosVenta();
                        MessageBox.Show("Gasto registrado correctamente!", "Registrar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró registrar el gasto.", "Registrar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe completar todos los campos antes de registrar el gasto.", "Registrar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Registrar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = false;
                string monto = string.Empty;
                FIGastos gastos = new FIGastos();
                FrmGasto frmG = new FrmGasto();
                if (!string.IsNullOrEmpty(txtDescripcionGasto.Text) && !string.IsNullOrEmpty(nMontoGasto.Value.ToString()))
                {
                    frmG.IdGasto = IDGasto;
                    frmG.DescripcionGasto = txtDescripcionGasto.Text;
                    frmG.FechaInicioGasto = dtpFechaGasto.Value;
                    frmG.FechaTerminoGasto = dtpFechaGasto.Value;
                    monto = nMontoGasto.Value.ToString().Replace(".", "").Replace("$", "");
                    frmG.MontoGasto = Convert.ToInt32(monto);
                    frmG.EsInversionGasto = false;

                    bandera = gastos.ModificarGasto(frmG);
                    if (bandera)
                    {
                        cargarGrillaGastos();
                        if (dtpFiltraDiaGasto.Value != null)
                            CargarGastosPorDia(dtpFiltraDiaGasto.Value);
                        if (dtpFiltraMesGasto.Value != null)
                            CargarGastosPorMes(dtpFiltraMesGasto.Value);
                        limpiarGastosVenta();
                        MessageBox.Show("Gasto modificado correctamente!", "Modificar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró modificar el gasto.", "Modificar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe completar todos los campos antes de modificar el gasto.", "Modificar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Modificar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = false;
                FIGastos gastos = new FIGastos();
                if (IDGasto != Guid.Empty)
                {
                    bandera = gastos.EliminarGasto(IDGasto);
                    if (bandera)
                    {
                        cargarGrillaGastos();
                        if (dtpFiltraDiaGasto.Value != null)
                            CargarGastosPorDia(dtpFiltraDiaGasto.Value);
                        if (dtpFiltraMesGasto.Value != null)
                            CargarGastosPorMes(dtpFiltraMesGasto.Value);
                        limpiarGastosVenta();
                        MessageBox.Show("Gasto eliminado correctamente!", "Eliminar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se logró eliminar el gasto.", "Eliminar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Debe seleccionar el registro que quiere eliminar.", "Eliminar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Eliminar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarGastosVenta();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Limpiar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                FIGastos gastos = new FIGastos();
                List<FrmGasto> lista = new List<FrmGasto>();
                if (chkBuscaMesGasto.Checked)
                    lista = gastos.ListaGastosPorMes(dtpBuscaFechaGasto.Value);
                else
                    lista = gastos.ListaGastosPorDia(dtpBuscaFechaGasto.Value);

                if (lista != null && lista.Count > 0)
                    gvGastos.DataSource = lista;
                else
                    gvGastos.DataSource = null;

                chkBuscaMesGasto.Checked = false;
                btnCancelaBuscaGasto.Enabled = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Buscar Gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelaBuscaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                cargarGrillaGastos();
                btnCancelaBuscaGasto.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Cancelar Busqueda Gastos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrarDiaGasto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFiltraDiaGasto.Value != null)
                    CargarGastosPorDia(dtpFiltraDiaGasto.Value);
                else
                    MessageBox.Show("No se logró obtener montos de gasto según día seleccionado", "Filtro día", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Filtro día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltraMesGasto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFiltraMesGasto.Value != null)
                    CargarGastosPorMes(dtpFiltraMesGasto.Value);
                else
                    MessageBox.Show("No se logró obtener montos de gasto según mes seleccionado", "Filtro mes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Filtro mes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvGastos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IDGasto = Guid.Parse(gvGastos.Rows[e.RowIndex].Cells[0].Value.ToString());
                dtpFechaGasto.Value = Convert.ToDateTime(gvGastos.Rows[e.RowIndex].Cells[3].Value);
                txtDescripcionGasto.Text = gvGastos.Rows[e.RowIndex].Cells[1].Value.ToString();
                nMontoGasto.Value = Convert.ToInt32(gvGastos.Rows[e.RowIndex].Cells[2].Value);
                btnModificaGasto.Enabled = true;
                btnEliminaGasto.Enabled = true;
                btnLimpiaGasto.Enabled = true;
                btnIngresaGasto.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Editar Gastos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #region "Métodos"

        #region "Ventas"
        protected void limpiarCamposVenta()
        {
            txtDescripcionVenta.Text = "";
            dtFechaInicioVenta.Value = DateTime.Today;
            dtFechaTerminoVenta.Value = DateTime.Today;
            txtMontoVenta.Value = 0;
            chkConBoleta.Checked = false;
            nRangoBoletaDesde.Value = 0;
            nRangoBoletaHasta.Value = 0;
            ID = Guid.Empty;
            btnIngresaVenta.Enabled = true;
            btnModificarVenta.Enabled = false;
            btnEliminarVenta.Enabled = false;
            btnLimpiarVenta.Enabled = false;
        }
        protected void cargarGrillaVentas()
        {
            try
            {
                FIVentas ventas = new FIVentas();
                List<FrmVenta> lista = ventas.ListaTodasVentas();
                if (lista != null && lista.Count > 0)
                {
                    menu.TotalVentas = 0;
                    foreach (FrmVenta item in lista)
                    {
                        menu.TotalVentas += item.MontoVenta;
                    }
                    gvVentas.AutoGenerateColumns = false;
                    gvVentas.DataSource = lista;
                    listaVentasChart.AddRange(lista);
                }
                else
                {
                    gvVentas.DataSource = null;
                    menu.TotalVentas = 0;
                }

                CargarTotales();
                CargarChartVentas();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Obtener Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CargarChartVentas()
        {
            try
            {
                if (listaVentasChart != null && listaVentasChart.Count > 0)
                {
                    DateTime hoy = DateTime.Today;         
                    ChartVentas.Series.Clear();
                    ChartVentas.Titles.Add("VENTAS");
                    Series series = ChartVentas.Series.Add("Ventas");
                    series.ChartType = SeriesChartType.Column;
                    series.Color = Color.DarkSeaGreen;
                    series.IsVisibleInLegend = false;

                    listaVentasChart = listaVentasChart.Where(l => l.FechaInicioVenta >= hoy.AddMonths(-6)).OrderBy(f => f.FechaInicioVenta).ToList();
                    int mes = 0, mesAux = 0, montoTotalMes = 0;
                    string nombreMes = "";
                    foreach (FrmVenta item in listaVentasChart)
                    {
                        mes = item.FechaInicioVenta.Value.Month;
                        if (mes != mesAux && mesAux > 0)
                        {
                            series.Points.AddXY(nombreMes, montoTotalMes);
                            series.IsValueShownAsLabel = true;
                            montoTotalMes = 0;
                        }
                        nombreMes = item.FechaInicioVenta.Value.ToString("MMM-yyyy");
                        montoTotalMes += item.MontoVenta;
                        mesAux = mes;
                    }
                    series.Points.AddXY(nombreMes, montoTotalMes);
                    series.IsValueShownAsLabel = true;
                }
                else
                    MessageBox.Show("Advertencia: No se logró cargar el gráfico de ventas.", "Cargar Chart Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Cargar Chart Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CargarVentasPorDia(DateTime Dia)
        {
            try
            {
                FIVentas ventas = new FIVentas();
                FrmVenta venta = new FrmVenta();
                if (Dia != null)
                {
                    List<FrmVenta> lista = ventas.ListaVentasPorDia(Dia);
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (FrmVenta item in lista)
                        {
                            if (item.VentaConBoleta)
                                venta.TotalDiaConBoleta += item.MontoVenta;
                            else
                                venta.TotalDiaSinBoleta += item.MontoVenta;
                        }

                        venta.NumeroBoletaInicio = lista.Min(x => x.NumeroBoletaInicio);
                        venta.NumeroBoletaTermino = lista.Max(y => y.NumeroBoletaTermino);
                    }
                    else
                    {
                        venta.TotalDiaConBoleta = 0;
                        venta.TotalDiaSinBoleta = 0;
                    }

                    venta.TotalDia = venta.TotalDiaConBoleta + venta.TotalDiaSinBoleta;
                    txtTotalDiaCB.Text = venta.TotalDiaConBoleta.ToString("C0");
                    txtTotalDiaSB.Text = venta.TotalDiaSinBoleta.ToString("C0");
                    txtTotalDia.Text = venta.TotalDia.ToString("C0");
                    txtRangoBoletasVenta.Text = venta.NumeroBoletaInicio + " - " + venta.NumeroBoletaTermino;
                }
                else
                    MessageBox.Show("No se logró obtener ventas del día seleccionado", "Ventas por día", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Ventas por día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CargarVentasPorMes(DateTime mes)
        {
            try
            {
                FIVentas ventas = new FIVentas();
                FrmVenta venta = new FrmVenta();
                if (mes != null)
                {
                    List<FrmVenta> lista = ventas.ListaVentasPorMes(mes);
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (FrmVenta item in lista)
                        {
                            if (item.VentaConBoleta)
                                venta.TotalMesConBoleta += item.MontoVenta;
                            else
                                venta.TotalMesSinBoleta += item.MontoVenta;
                        }
                    }
                    else
                    {
                        venta.TotalMesConBoleta = 0;
                        venta.TotalMesSinBoleta = 0;
                    }

                    venta.TotalMes = venta.TotalMesConBoleta + venta.TotalMesSinBoleta;
                    txtTotalMesCB.Text = venta.TotalMesConBoleta.ToString("C0");
                    txtTotalMesSB.Text = venta.TotalMesSinBoleta.ToString("C0");
                    txtTotalMes.Text = venta.TotalMes.ToString("C0");
                }
                else
                    MessageBox.Show("No se logró obtener ventas del mes seleccionado", "Ventas por mes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Ventas por mes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Compras"
        protected void cargarGrillaCompras()
        {
            try
            {
                FICompras compras = new FICompras();
                List<FrmCompra> lista = compras.ListaTodasCompras();
                if (lista != null && lista.Count > 0)
                {
                    menu.TotalCompras = 0;
                    foreach (FrmCompra item in lista)
                    {
                        menu.TotalCompras += item.MontoCompra;
                    }
                    gvCompras.AutoGenerateColumns = false;
                    gvCompras.DataSource = lista;
                    listaComprasChart.AddRange(lista);
                }
                else
                {
                    menu.TotalCompras = 0;
                    gvCompras.DataSource = null;
                }

                CargarTotales();
                CargarChartGastos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Obtener Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void limpiarCamposCompra()
        {
            dtpFechaCompra.Value = DateTime.Today;
            txtDescripcionCompra.Text = "";
            nMontoCompra.Value = 0;
            chkConFactura.Checked = false;
            txtRutFactura.Text = "";
            nRangoFacturaDesde.Value = 0;
            nRangoFacturaHasta.Value = 0;
            IDCompra = Guid.Empty;
            btnIngresarCompra.Enabled = true;
            btnModificarCompra.Enabled = false;
            btnEliminarCompra.Enabled = false;
            btnLimpiarCompra.Enabled = false;
            chkRegistrarFactura.Checked = false;
            dtpFechaFactura.Value = DateTime.Today;
        }

        protected void CargarComprasPorDia(DateTime Dia)
        {
            try
            {
                FICompras compras = new FICompras();
                FrmCompra compra = new FrmCompra();
                if (Dia != null)
                {
                    List<FrmCompra> lista = compras.ListaComprasPorDia(Dia);
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (FrmCompra item in lista)
                        {
                            if (item.CompraConFactura)
                                compra.TotalDiaConFactura += item.MontoCompra;
                            else
                                compra.TotalDiaSinFactura += item.MontoCompra;
                        }
                        compra.CompraFacturaInicio = lista.Min(x => x.CompraFacturaInicio);
                        compra.CompraFacturaTermino = lista.Max(y => y.CompraFacturaTermino);
                    }
                    else
                    {
                        compra.TotalDiaConFactura = 0;
                        compra.TotalDiaSinFactura = 0;
                    }

                    compra.TotalCompraDia = compra.TotalDiaConFactura + compra.TotalDiaSinFactura;
                    txtTotalDiaCF.Text = compra.TotalDiaConFactura.ToString("C0");
                    txtTotalDiaSF.Text = compra.TotalDiaSinFactura.ToString("C0");
                    txtTotalDiaCompras.Text = compra.TotalCompraDia.ToString("C0");
                    txtRangoFacturas.Text = compra.CompraFacturaInicio + " - " + compra.CompraFacturaTermino;
                }
                else
                    MessageBox.Show("No se logró obtener compras del día seleccionado", "Compras por día", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Compras por día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CargarComprasPorMes(DateTime mes)
        {
            try
            {
                FICompras compras = new FICompras();
                FrmCompra compra = new FrmCompra();
                if (mes != null)
                {
                    List<FrmCompra> lista = compras.ListaComprasPorMes(mes);
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (FrmCompra item in lista)
                        {
                            if (item.CompraConFactura)
                                compra.TotalMesConFactura += item.MontoCompra;
                            else
                                compra.TotalMesSinFactura += item.MontoCompra;
                        }
                    }
                    else
                    {
                        compra.TotalMesConFactura = 0;
                        compra.TotalMesConFactura = 0;
                    }

                    compra.TotalCompraMes = compra.TotalMesConFactura + compra.TotalMesSinFactura;
                    txtTotalMesCF.Text = compra.TotalMesConFactura.ToString("C0");
                    txtTotalMesSF.Text = compra.TotalMesSinFactura.ToString("C0");
                    txtTotalMesCompras.Text = compra.TotalCompraMes.ToString("C0");
                }
                else
                    MessageBox.Show("No se logró obtener compras del mes seleccionado", "Compras por mes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Compras por mes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Gastos"
        protected void limpiarGastosVenta()
        {
            dtpFechaGasto.Value = DateTime.Today;
            txtDescripcionGasto.Text = "";
            nMontoGasto.Value = 0;
            IDGasto = Guid.Empty;
            btnIngresaGasto.Enabled = true;
            btnModificaGasto.Enabled = false;
            btnEliminaGasto.Enabled = false;
            btnLimpiaGasto.Enabled = false;
        }

        protected void cargarGrillaGastos()
        {
            try
            {
                FIGastos gastos = new FIGastos();
                List<FrmGasto> lista = gastos.ListaTodosGastos();
                if (lista != null && lista.Count > 0)
                {
                    menu.TotalGastos = 0;
                    foreach (FrmGasto item in lista)
                    {
                        menu.TotalGastos += item.MontoGasto;
                    }
                    gvGastos.AutoGenerateColumns = false;
                    gvGastos.DataSource = lista;
                    listaGastosChart.AddRange(lista);
                }
                else
                {
                    menu.TotalGastos = 0;
                    gvGastos.DataSource = null;
                }

                CargarTotales();
                CargarChartGastos();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Obtener Gastos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CargarGastosPorDia(DateTime Dia)
        {
            try
            {
                FIGastos gastos = new FIGastos();
                FrmGasto gasto = new FrmGasto();
                if (Dia != null)
                {
                    List<FrmGasto> lista = gastos.ListaGastosPorDia(Dia);
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (FrmGasto item in lista)
                        {
                            gasto.TotalGastoDia += item.MontoGasto;
                        }
                    }

                    txtFiltraDiaGasto.Text = gasto.TotalGastoDia.ToString("C0");
                }
                else
                    MessageBox.Show("No se logró obtener gastos del día seleccionado", "Gastos por día", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Gastos por día", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CargarGastosPorMes(DateTime mes)
        {
            try
            {
                FIGastos gastos = new FIGastos();
                FrmGasto gasto = new FrmGasto();
                if (mes != null)
                {
                    List<FrmGasto> lista = gastos.ListaGastosPorMes(mes);
                    if (lista != null && lista.Count > 0)
                    {
                        foreach (FrmGasto item in lista)
                        {
                            gasto.TotalGastoMes += item.MontoGasto;
                        }
                    }

                    txtFiltraMesGasto.Text = gasto.TotalGastoMes.ToString("C0");
                }
                else
                    MessageBox.Show("No se logró obtener gastos del mes seleccionado", "Gastos por mes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Gastos por mes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Inversion"
        protected void cargarGrillasInversion()
        {
            try
            {
                int InversionCompra = 0, InversionGasto = 0, InversionTotal = 0;
                FICompras compras = new FICompras();
                List<FrmCompra> lista = compras.ListaTodasComprasInversion();
                if (lista != null && lista.Count > 0)
                {
                    gvCompraInversion.AutoGenerateColumns = false;
                    gvCompraInversion.DataSource = lista;

                    foreach (FrmCompra item in lista)
                    {
                        InversionCompra += item.MontoCompra;
                    }
                }
                else
                    gvCompraInversion.DataSource = null;

                FIGastos gastos = new FIGastos();
                List<FrmGasto> listaGastos = gastos.ListaTodosGastosInversion();
                if (listaGastos != null && listaGastos.Count > 0)
                {
                    gvGastosInversion.AutoGenerateColumns = false;
                    gvGastosInversion.DataSource = listaGastos;

                    foreach (FrmGasto itemGasto in listaGastos)
                    {
                        InversionGasto += itemGasto.MontoGasto;
                    }
                }
                else
                    gvGastosInversion.DataSource = null;

                InversionTotal = InversionCompra + InversionGasto;
                lblInversionCompra.Text = InversionCompra.ToString("C0");
                lblInversionGasto.Text = InversionGasto.ToString("C0");
                lblInversionTotal.Text = InversionTotal.ToString("C0");
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Obtener Inversión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        protected void CargarTotales()
        {
            #region "Ventas"
            lblTotalVentas.Text = menu.TotalVentas.ToString("C0");
            txtIIngresos.Text = lblTotalVentas.Text;
            #endregion
            #region "Compras"
            lblTotalCompras.Text = menu.TotalCompras.ToString("C0");
            txtICompras.Text = lblTotalCompras.Text;
            #endregion
            #region "Gastos"
            lblTotalGastos.Text = menu.TotalGastos.ToString("C0");
            txtIGastos.Text = menu.TotalGastos.ToString("C0");
            #endregion
            menu.SumaGastos = menu.TotalCompras + menu.TotalGastos;
            menu.TotalGanancia = menu.TotalVentas - menu.SumaGastos;

            txtITotalGastos.Text = menu.SumaGastos.ToString("C0");
            txtITotalGanancia.Text = menu.TotalGanancia.ToString("C0");
            if (menu.TotalGanancia < 0)
            {
                txtITotalGanancia.BackColor = Color.Red;
                txtITotalGanancia.ForeColor = Color.White;
            }
            else
            {
                txtITotalGanancia.BackColor = Color.Transparent;
                txtITotalGanancia.ForeColor = Color.Black;
            }
        }

        protected void CargarChartGastos()
        {
            try
            {
                if (listaComprasChart != null && listaComprasChart.Count > 0 && listaGastosChart != null && listaGastosChart.Count > 0)
                {
                    DateTime hoy = DateTime.Today;
                    ChartGastos.Series.Clear();
                    ChartGastos.Titles.Add("GASTOS");
                    //COMPRAS
                    listaComprasChart = listaComprasChart.Where(l => l.FechaInicioCompra >= hoy.AddMonths(-6)).OrderBy(f => f.FechaInicioCompra).ToList();
                    Series serieCompras = ChartGastos.Series.Add("Compras");
                    serieCompras.ChartType = SeriesChartType.Line;
                    serieCompras.Color = Color.LightSalmon;
                    serieCompras.BorderWidth = 5;
                    serieCompras.IsVisibleInLegend = true;
                    int mes = 0, mesAux = 0, montoTotalMes = 0;
                    string nombreMes = "";
                    foreach (FrmCompra item in listaComprasChart)
                    {
                        mes = item.FechaInicioCompra.Value.Month;
                        if (mes != mesAux && mesAux > 0)
                        {
                            serieCompras.Points.AddXY(nombreMes, montoTotalMes.ToString("C0"));
                            serieCompras.IsValueShownAsLabel = true;
                            montoTotalMes = 0;
                        }
                        nombreMes = item.FechaInicioCompra.Value.ToString("MMM-yyyy");
                        montoTotalMes += item.MontoCompra;
                        mesAux = mes;
                    }
                    serieCompras.Points.AddXY(nombreMes, montoTotalMes.ToString("C0"));
                    serieCompras.IsValueShownAsLabel = true;
                    //GASTOS
                    listaGastosChart = listaGastosChart.Where(l => l.FechaInicioGasto >= hoy.AddMonths(-6)).OrderBy(f => f.FechaInicioGasto).ToList();
                    Series serieGastos = ChartGastos.Series.Add("Gastos");
                    serieGastos.ChartType = SeriesChartType.Line;
                    serieGastos.Color = Color.LightSteelBlue;
                    serieGastos.BorderWidth = 5;
                    serieGastos.IsVisibleInLegend = true;
                    mes = mesAux = montoTotalMes = 0;
                    foreach (FrmGasto item in listaGastosChart)
                    {
                        mes = item.FechaInicioGasto.Value.Month;
                        if (mes != mesAux && mesAux > 0)
                        {
                            serieGastos.Points.AddXY(nombreMes, montoTotalMes);
                            serieGastos.IsValueShownAsLabel = true;
                            montoTotalMes = 0;
                        }
                        nombreMes = item.FechaInicioGasto.Value.ToString("MMM-yyyy");
                        montoTotalMes += item.MontoGasto;
                        mesAux = mes;
                    }
                    serieGastos.Points.AddXY(nombreMes, montoTotalMes);
                    serieGastos.IsValueShownAsLabel = true;
                }
                else
                    MessageBox.Show("Advertencia: No se logró cargar el gráfico de gastos.", "Cargar Chart Gastos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message, "Cargar Chart Gastos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void chkRegistrarFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegistrarFactura.Checked)
            {
                lblFechaFactura.Visible = true;
                dtpFechaFactura.Visible = true;
                lblNumeroFactura.Visible = true;
                nRangoFacturaHasta.Visible = true;
            }
            else
            {
                lblFechaFactura.Visible = false;
                dtpFechaFactura.Visible = false;
                lblNumeroFactura.Visible = false;
                nRangoFacturaHasta.Visible = false;
            }
        }
    }
}
