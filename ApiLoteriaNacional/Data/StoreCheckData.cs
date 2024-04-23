using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using NuGet.Protocol.Plugins;
using Newtonsoft.Json;
using LoteriaNacionalDominio;
using Microsoft.VisualBasic;
using static LoteriaNacionalDominio.MantenimientoDTO;
using System.Reflection.Metadata.Ecma335;
using static LoteriaNacionalDominio.StoreCheckDTO;
using System.Collections.Generic;
using static LoteriaNacionalDominio.SeguridadDTO;
using Newtonsoft.Json.Converters;

namespace ApiLoteriaNacional.Data
{
    public class StoreCheckData
    {
        private readonly string _cadenaConexion;

        public StoreCheckData(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("LNAPI");
        }

        #region Llenar Formulario
        public async Task<RespuestaDTO> LlenarFormulario(RegistroDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spLlenarFormulario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                cmd.Parameters.Add("@json", SqlDbType.NVarChar,-1);
                cmd.Parameters["@json"].Value = JsonConvert.SerializeObject(dato);
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
                    //return new RespuestaDTO(ex.ErrorCode, ex.Message, "");
                    return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    ""
                    );
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
        public async Task<RespuestaDTO> ActualizarFormulario(RegistroDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spActualizarFormulario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                cmd.Parameters.Add("@json", SqlDbType.NVarChar, -1);
                cmd.Parameters["@json"].Value = JsonConvert.SerializeObject(dato);
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
        public async Task<RespuestaDTO> ConsultarFormularioIngresado(RegistroFormularioDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormularioIngresado", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@codigoFormulario", SqlDbType.BigInt);
                cmd.Parameters["@codigoFormulario"].Value = dato.codigoFormulario;
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
        public async Task<RespuestaDTO> ConsultarFormularioIngresadoSupervisor(RegistroFormularioDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormularioIngresadoSupervisor", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@codigoFormulario", SqlDbType.BigInt);
                cmd.Parameters["@codigoFormulario"].Value = dato.codigoFormulario;
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

        #region Revisar Formulario
        public async Task<RespuestaDTO> ObtieneZonasPorSupervisor(LoginDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneZonasPorSupervisor", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoUsuario"].Value = dato.UserName;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ConsultarFormulariosporPOS(ZonasPorSupervisorDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormulariosporPOS", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            
            try
            {
                cmd.Parameters.Add("@codigoPDS", SqlDbType.Int);
                cmd.Parameters["@codigoPDS"].Value = dato.codigoPDS;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ConsultarFormulariosporPOSJefeComercial(ZonasPorSupervisorDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormulariosporPOSJefeComercial", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();

            try
            {
                cmd.Parameters.Add("@codigoPDS", SqlDbType.Int);
                cmd.Parameters["@codigoPDS"].Value = dato.codigoPDS;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> RevisarFormularioSupervisor(RegistroDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spRevisionFormularioSupervisor", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                cmd.Parameters.Add("@json", SqlDbType.NVarChar, -1);
                cmd.Parameters["@json"].Value = JsonConvert.SerializeObject(dato);
                cmd.Parameters.Add("@evidenciaSupervisor", SqlDbType.NVarChar, -1);
                cmd.Parameters["@evidenciaSupervisor"].Value = dato.evidenciaRevision;
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
        public async Task<RespuestaDTO> ConsultarFormularioRevisado(RegistroFormularioDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormularioRevisado", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@codigoSupervidor", SqlDbType.VarChar, 15);
                cmd.Parameters["@codigoSupervidor"].Value = dato.codigoSupervisor;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRegistro;
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
        public async Task<RespuestaDTO> RevisarFormularioJefeComercial(RegistroDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spRevisionFormularioJefeComercial", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            SqlTransaction sqlTransaccion = sql.BeginTransaction();
            cmd.Transaction = sqlTransaccion;
            try
            {
                cmd.Parameters.Add("@json", SqlDbType.NVarChar, -1);
                cmd.Parameters["@json"].Value = JsonConvert.SerializeObject(dato);
                cmd.Parameters.Add("@evidenciaJefeVentas", SqlDbType.NVarChar, -1);
                cmd.Parameters["@evidenciaJefeVentas"].Value = dato.evidenciaRevision;
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

        #endregion

        #region Seguimiento
        public async Task<RespuestaDTO> ObtieneResumenGerencialZonas()
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneResumenGerencialZonas", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ConsultarFormulariosRevisadosporPOS(RegistroFormularioDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormulariosRevisadosporPOS", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();

            try
            {
                cmd.Parameters.Add("@codigoPDS", SqlDbType.Int);
                cmd.Parameters["@codigoPDS"].Value = dato.codigoPDS;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ConsultarFormularioConNovedades(RegistroFormularioDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spConsultarFormularioConNovedades", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@codigoFormulario", SqlDbType.VarChar, 15);
                cmd.Parameters["@codigoFormulario"].Value = dato.codigoFormulario;
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
        public async Task<RespuestaDTO> ObtieneRankingPDS(RegistroFormularioDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneRankingPDS", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ObtienePDSPorRangoCumplimiento(RankingCumplimientoPDSDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtienePDSPorRangoCumplimiento", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();

            try
            {
                cmd.Parameters.Add("@rango", SqlDbType.Int);
                cmd.Parameters["@rango"].Value = dato.grupo;
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoUsuario"].Value = dato.codigoUsuario;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.Char, 1);
                cmd.Parameters["@tipoConsulta"].Value = dato.tipoConsulta;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        

        #endregion

        #region Supervisor
        public async Task<RespuestaDTO> ObtieneRankingPDSPorSupervisor(RegistroFormularioDTO dato)
        {
            if (dato.fechaRegistro == "string")
                dato.fechaRegistro = null;
            if (dato.fechaRevisionSupervisor == "string")
                dato.fechaRevisionSupervisor = null;

            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneRankingPDSporSupervisor", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoSupervisor", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoSupervisor"].Value = dato.codigoSupervisor;
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ObtieneRevisadosPorSupervisor(RegistroFormularioDTO dato)
        {
            try
            {
                if (dato.fechaRegistro == "string")
                    dato.fechaRegistro = null;
                if (dato.fechaRevisionSupervisor == "string")
                    dato.fechaRevisionSupervisor = null;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spObtieneRevisadosPorSupervisor", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@codigoSupervisor", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoSupervisor"].Value = dato.codigoSupervisor;
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
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
        public async Task<RespuestaDTO> ObtieneCalificacioCustionariosporPDS(RegistroFormularioDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneCalificacioCustionariosporPDS", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();

            try
            {
                cmd.Parameters.Add("@codigoPDS", SqlDbType.Int);
                cmd.Parameters["@codigoPDS"].Value = dato.codigoPDS;
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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

        #endregion

        #region Jefe Comercial
        public async Task<RespuestaDTO> ObtieneZonasPorJefeComercial(LoginDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneZonasPorJefeComercial", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoUsuario"].Value = dato.UserName;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ObtieneRevisadosPorJefeComercial(RegistroFormularioDTO dato)
        {
            try
            {
                if (dato.fechaRegistro == "string")
                    dato.fechaRegistro = null;
                if (dato.fechaRevisionSupervisor == "string")
                    dato.fechaRevisionSupervisor = null;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.spObtieneRevisadosPorJefeComercial", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoUsuario"].Value = dato.codigoJefeVentas;
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
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
        public async Task<RespuestaDTO> ObtieneRankingPDSPorJefeComercial(RegistroFormularioDTO dato)
        {
            if (dato.fechaRegistro == "string")
                dato.fechaRegistro = null;
            if (dato.fechaRevisionSupervisor == "string")
                dato.fechaRevisionSupervisor = null;

            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneRankingPDSporJefeComercial", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoJefeComercial", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoJefeComercial"].Value = dato.codigoJefeVentas;
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ObtieneCalificacioCustionariosporPDSJefeComercial(RegistroFormularioDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneCalificacioCustionariosporPDSJefeComercial", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();

            try
            {
                cmd.Parameters.Add("@codigoPDS", SqlDbType.Int);
                cmd.Parameters["@codigoPDS"].Value = dato.codigoPDS;
                cmd.Parameters.Add("@fechaDesde", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaDesde"].Value = dato.fechaRegistro;
                cmd.Parameters.Add("@fechaHasta", SqlDbType.VarChar, 10);
                cmd.Parameters["@fechaHasta"].Value = dato.fechaRevisionSupervisor;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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

        #endregion

        #region Gerencia
        public async Task<RespuestaDTO> ObtieneZonasPorGerencia()
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtieneZonasPorGerencia", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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
        public async Task<RespuestaDTO> ObtieneDiasRetrasoRevision(string tipoConsulta)
        {
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spRankingTiempoRevision", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.Char, 1);
                cmd.Parameters["@tipoConsulta"].Value = tipoConsulta;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    JsonConvert.SerializeObject(dtDatos)
                    );

            }
            catch (SqlException ex)
            {
                try
                {
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


        #endregion

        #region Informes
        public async Task<DataSet> ObtieneInformeSupervisor(long codigoFormulario)
        {
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtenerInformeSupervisor", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoFormulario", SqlDbType.BigInt);
                cmd.Parameters["@codigoFormulario"].Value = codigoFormulario;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dsDatos = new DataSet("dsInformeSupervisor");
                await Task.Run(()=>adapter.Fill(dsDatos));
                return dsDatos;

            }
            catch (SqlException ex)
            {
                try
                {
                    return null;
                }
                catch (Exception ex2)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sql.Close();
            }
        }
        public async Task<DataSet> ObtieneEvidenciaInformeSupervisor(long codigoFormulario)
        {
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtenerEvidenciaInformeSupervisor", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoFormulario", SqlDbType.BigInt);
                cmd.Parameters["@codigoFormulario"].Value = codigoFormulario;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dsDatos = new DataSet("dsEvidenciaInformeSupervisor");
                await Task.Run(() => adapter.Fill(dsDatos));
                return dsDatos;

            }
            catch (SqlException ex)
            {
                try
                {
                    return null;
                }
                catch (Exception ex2)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sql.Close();
            }
        }
        public async Task<DataSet> ObtieneInformeJefeVentas(long codigoFormulario)
        {
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtenerInformeJefeVentas", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoFormulario", SqlDbType.BigInt);
                cmd.Parameters["@codigoFormulario"].Value = codigoFormulario;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dsDatos = new DataSet("dsInformeJefeVentas");
                await Task.Run(() => adapter.Fill(dsDatos));
                return dsDatos;

            }
            catch (SqlException ex)
            {
                try
                {
                    return null;
                }
                catch (Exception ex2)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sql.Close();
            }
        }
        public async Task<DataSet> ObtieneEvidenciaInformeJefeVentas(long codigoFormulario)
        {
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.spObtenerEvidenciaInformeJefeVentas", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoFormulario", SqlDbType.BigInt);
                cmd.Parameters["@codigoFormulario"].Value = codigoFormulario;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dsDatos = new DataSet("dsEvidenciaInformeJefeVentas");
                await Task.Run(() => adapter.Fill(dsDatos));
                return dsDatos;

            }
            catch (SqlException ex)
            {
                try
                {
                    return null;
                }
                catch (Exception ex2)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                sql.Close();
            }
        }
        #endregion
    }

}
