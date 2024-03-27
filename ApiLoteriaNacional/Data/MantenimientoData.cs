using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using NuGet.Protocol.Plugins;
using Newtonsoft.Json;
using LoteriaNacionalDominio;
using Microsoft.VisualBasic;
using static LoteriaNacionalDominio.MantenimientoDTO;
using System.Reflection.Metadata.Ecma335;

namespace ApiLoteriaNacional.Data
{
    public class MantenimientoData
    {
        private readonly string _cadenaConexion;

        public MantenimientoData(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("LNAPI");
        }

        #region Secciones
        public async Task<RespuestaDTO> mantenimientoSecciones(SeccionesDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spMantenimientoSecciones", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 15);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = "CO";
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        public async Task<RespuestaDTO> mantenimientoGrabarSecciones(SeccionesDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spMantenimientoSecciones", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                string opcion = string.Empty;

                if (dato.codigoSeccion == 0)
                    opcion = "IN";
                else
                    opcion = "AC";

                cmd.Parameters.Add("@codigoSeccion", SqlDbType.SmallInt);
                cmd.Parameters["@codigoSeccion"].Value = dato.codigoSeccion;
                cmd.Parameters.Add("@nombreSeccion", SqlDbType.VarChar, 255);
                cmd.Parameters["@nombreSeccion"].Value = dato.nombreSeccion;
                cmd.Parameters.Add("@porcentajeCalificacion", SqlDbType.SmallInt);
                cmd.Parameters["@porcentajeCalificacion"].Value = dato.porcentajeCalificacion;
                cmd.Parameters.Add("@estadoSeccion", SqlDbType.Bit);
                cmd.Parameters["@estadoSeccion"].Value = dato.estadoSeccion;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 20);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = opcion;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                respuesta = await cmd.ExecuteNonQueryAsync();
                sqlTransaccion.Commit();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    ""
                    );

            }
            catch (SqlException ex)
            {
                try
                {
                    sqlTransaccion.Rollback();
                    return new RespuestaDTO(ex.ErrorCode, ex.Message, "");
                }
                catch (Exception ex2)
                {
                    return new RespuestaDTO(ex.ErrorCode, ex2.Message, "");
                }
            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
            finally
            {
                sql.Close();
            }


        }
        public async Task<RespuestaDTO> obtenerSecciones()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerSecciones", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        public async Task<RespuestaDTO> obtenerSeccionesFormulario()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerSeccionesFormulario", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        #endregion

        #region Preguntas
        public async Task<RespuestaDTO> mantenimientoPreguntas(PreguntasDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spMantenimientoPreguntas", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@codigoSeccion", SqlDbType.SmallInt);
                cmd.Parameters["@codigoSeccion"].Value = dato.codigoSeccion;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 15);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = "CO";
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        public async Task<RespuestaDTO> mantenimientoGrabarPreguntas(PreguntasDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spMantenimientoPreguntas", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                string opcion = string.Empty;

                if (dato.codigoPregunta == 0)
                    opcion = "IN";
                else
                    opcion = "AC";

                cmd.Parameters.Add("@codigoPregunta", SqlDbType.SmallInt);
                cmd.Parameters["@codigoPregunta"].Value = dato.codigoPregunta;
                cmd.Parameters.Add("@codigoSeccion", SqlDbType.SmallInt);
                cmd.Parameters["@codigoSeccion"].Value = dato.codigoSeccion;
                cmd.Parameters.Add("@descripcionPregunta", SqlDbType.VarChar, 255);
                cmd.Parameters["@descripcionPregunta"].Value = dato.descripcionPregunta;
                cmd.Parameters.Add("@conceptoPregunta", SqlDbType.VarChar, 255);
                cmd.Parameters["@conceptoPregunta"].Value = dato.conceptoPregunta;
                cmd.Parameters.Add("@vigenciaDesde", SqlDbType.DateTime);
                cmd.Parameters["@vigenciaDesde"].Value = dato.vigenciaDesde;
                cmd.Parameters.Add("@vigenciaHasta", SqlDbType.DateTime);
                cmd.Parameters["@vigenciaHasta"].Value = dato.vigenciaHasta;
                cmd.Parameters.Add("@estadoPregunta", SqlDbType.Bit);
                cmd.Parameters["@estadoPregunta"].Value = dato.estadoPregunta;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 20);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = opcion;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                respuesta = await cmd.ExecuteNonQueryAsync();
                sqlTransaccion.Commit();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    ""
                    );

            }
            catch (SqlException ex)
            {
                try
                {
                    sqlTransaccion.Rollback();
                    return new RespuestaDTO(ex.ErrorCode, ex.Message, "");
                }
                catch (Exception ex2)
                {
                    return new RespuestaDTO(ex.ErrorCode, ex2.Message, "");
                }
            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
            finally
            {
                sql.Close();
            }
        }
        public async Task<RespuestaDTO> obtenerPreguntas()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerPreguntas", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@codigoSeccion", SqlDbType.SmallInt);
                //cmd.Parameters["@codigoSeccion"].Value = dato.codigoSeccion;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }


        #endregion

        #region Novedades
        public async Task<RespuestaDTO> mantenimientoNovedades(NovedadesDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spMantenimientoNovedades", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 15);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = "CO";
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        public async Task<RespuestaDTO> mantenimientoGrabarNovedades(NovedadesDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spMantenimientoNovedades", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                string opcion = string.Empty;

                if (dato.codigoNovedad == 0)
                    opcion = "IN";
                else
                    opcion = "AC";

                cmd.Parameters.Add("@codigoNovedad", SqlDbType.SmallInt);
                cmd.Parameters["@codigoNovedad"].Value = dato.codigoNovedad;
                cmd.Parameters.Add("@descripcionNovedad", SqlDbType.VarChar, 255);
                cmd.Parameters["@descripcionNovedad"].Value = dato.descripcionNovedad;
                cmd.Parameters.Add("@estadoNovedad", SqlDbType.Bit);
                cmd.Parameters["@estadoNovedad"].Value = dato.estadoNovedad;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 20);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = opcion;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                respuesta = await cmd.ExecuteNonQueryAsync();
                sqlTransaccion.Commit();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    ""
                    );

            }
            catch (SqlException ex)
            {
                try
                {
                    sqlTransaccion.Rollback();
                    return new RespuestaDTO(ex.ErrorCode, ex.Message, "");
                }
                catch (Exception ex2)
                {
                    return new RespuestaDTO(ex.ErrorCode, ex2.Message, "");
                }
            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
            finally
            {
                sql.Close();
            }


        }
        public async Task<RespuestaDTO> obtenerNovedades()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerNovedades", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        #endregion

        #region Aplicaciones
        public async Task<RespuestaDTO> mantenimientoAplicaciones(AplicacionDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.mantenimientoAplicaciones", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 15);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = "CO";
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        public async Task<RespuestaDTO> mantenimientoGrabarAplicaciones(AplicacionDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.mantenimientoAplicaciones", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                string opcion = string.Empty;

                if (dato.codigoAplicacion == 0)
                    opcion = "IN";
                else
                    opcion = "AC";

                cmd.Parameters.Add("@codigoAplicacion", SqlDbType.SmallInt);
                cmd.Parameters["@codigoAplicacion"].Value = dato.codigoAplicacion;
                cmd.Parameters.Add("@nombreAplicacion", SqlDbType.VarChar, 255);
                cmd.Parameters["@nombreAplicacion"].Value = dato.nombreAplicacion;
                cmd.Parameters.Add("@estadoAplicacion", SqlDbType.Bit);
                cmd.Parameters["@estadoAplicacion"].Value = dato.estadoAplicacion;
                cmd.Parameters.Add("@usuarioTransaccion", SqlDbType.VarChar, 20);
                cmd.Parameters["@usuarioTransaccion"].Value = dato.usuarioTransaccion;
                cmd.Parameters.Add("@equipoTransaccion", SqlDbType.VarChar, 250);
                cmd.Parameters["@equipoTransaccion"].Value = dato.equipoTransaccion;
                cmd.Parameters.Add("@opcion", SqlDbType.Char, 2);
                cmd.Parameters["@opcion"].Value = opcion;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                respuesta = await cmd.ExecuteNonQueryAsync();
                sqlTransaccion.Commit();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    ""
                    );

            }
            catch (SqlException ex)
            {
                try
                {
                    sqlTransaccion.Rollback();
                    return new RespuestaDTO(ex.ErrorCode, ex.Message, "");
                }
                catch (Exception ex2)
                {
                    return new RespuestaDTO(ex.ErrorCode, ex2.Message, "");
                }
            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
            finally
            {
                sql.Close();
            }


        }
        public async Task<RespuestaDTO> obtenerAplicaciones()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.obtenerAplicaciones", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    cmd.Parameters["@ds_msg"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        #endregion

        #region Bases
        
        #endregion

        #region Procesos por Aplicacion

        #endregion

    }
}
