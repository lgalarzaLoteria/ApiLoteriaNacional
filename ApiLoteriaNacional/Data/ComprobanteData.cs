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
                cmd.Parameters["@FechaProcesoDesde"].Value = "2016/09/06";
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

    }
}
