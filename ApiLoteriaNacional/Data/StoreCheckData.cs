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
            using SqlCommand cmd = new SqlCommand("dbo.llenarFormulario", sql);
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
            using SqlCommand cmd = new SqlCommand("dbo.actualizarFormulario", sql);
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
                using SqlCommand cmd = new SqlCommand("dbo.consultarFormularioIngresado", sql);
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
            using SqlCommand cmd = new SqlCommand("dbo.obtieneZonasPorSupervisor", sql);
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
        public async Task<RespuestaDTO> ConsultarFormulariosporPOS(RegistroFormularioDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.consultarFormulariosporPOS", sql);
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
            using SqlCommand cmd = new SqlCommand("dbo.revisionFormularioSupervisor", sql);
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
        public async Task<RespuestaDTO> ConsultarFormularioRevisado(RegistroFormularioDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.consultarFormularioRevisado", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@codigoSupervidor", SqlDbType.VarChar, 15);
                cmd.Parameters["@codigoSupervidor"].Value = dato.codigoSupervisor;
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

        #region Seguimiento
        public async Task<RespuestaDTO> ObtieneResumenGerencialZonas()
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.obtieneResumenGerencialZonas", sql);
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
            using SqlCommand cmd = new SqlCommand("dbo.consultarFormulariosRevisadosporPOS", sql);
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
                using SqlCommand cmd = new SqlCommand("dbo.consultarFormularioConNovedades", sql);
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
        public async Task<RespuestaDTO> ObtieneRankingPDS()
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.obtieneRankingPDS", sql);
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
        public async Task<RespuestaDTO> ObtienePDSPorRangoCumplimiento(RankingCumplimientoPDSDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.obtienePDSPorRangoCumplimiento", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();

            try
            {
                cmd.Parameters.Add("@rango", SqlDbType.Int);
                cmd.Parameters["@rango"].Value = dato.grupo;
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
        public async Task<RespuestaDTO> ObtieneCalificacioCustionariosporPDS(CalificacionCuestionariosPDSDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.obtieneCalificacioCustionariosporPDS", sql);
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


        #endregion

        #region Supervisor
        public async Task<RespuestaDTO> ObtieneRankingPDSPorSupervisor(RegistroFormularioDTO dato)
        {
            int respuesta = 0;
            using SqlConnection sql = new SqlConnection(_cadenaConexion);
            using SqlCommand cmd = new SqlCommand("dbo.obtieneRankingPDSporSupervisor", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            await sql.OpenAsync();
            try
            {
                cmd.Parameters.Add("@codigoSupervisor", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoSupervisor"].Value = dato.codigoSupervisor;
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
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("dbo.obtieneRevisadosPorSupervisor", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@codigoSupervisor", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoSupervisor"].Value = dato.codigoSupervisor;
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
    }
}
