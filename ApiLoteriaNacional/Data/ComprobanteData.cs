using LoteriaNacionalDominio;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static LoteriaNacionalDominio.ComprobanteExternoAdmDTO;
using System.Data;

namespace ApiLoteriaNacional.Data
{
    public class ComprobanteData
    {
        private readonly string _cadenaConexion;

        public ComprobanteData(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("ConexionPOS");
        }

        public async Task<RespuestaDTO> ObtenerComprobanteExternoAdm()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spComprobantesElectronicosEnvioExt", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@FechaProcesoDesde", SqlDbType.VarChar, 10).Direction = ParameterDirection.Input;
                cmd.Parameters["@FechaProcesoDesde"].Value = "";
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
                    );

            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }



        // 2024/06/06 - Control para verificar si se envió la trama al servicio externo
        public async Task<RespuestaDTO> ConfirnarEnvioComprobantesExternos(string IdEnvioTrama, bool TramaConfirmada)
        {
            using SqlConnection sql = new SqlConnection(_cadenaConexion);

            try
            {
                int respuesta = 0;

                using SqlCommand cmd = new SqlCommand("dbo.spComprobantesElectronicosConfirmarEnvioExt", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdEnvioTrama", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("@TramaConfirmada", SqlDbType.Bit).Direction = ParameterDirection.Input;
                cmd.Parameters["@IdEnvioTrama"].Value = IdEnvioTrama;
                cmd.Parameters["@TramaConfirmada"].Value = TramaConfirmada;
                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();

                respuesta = await cmd.ExecuteNonQueryAsync();
                return new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@co_msg"].Value),
                    Convert.ToString(cmd.Parameters["@ds_msg"].Value),
                    ""
                    );

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



    }
}
