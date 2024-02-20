using LoteriaNacionalDominio;
using Newtonsoft.Json;
using static LoteriaNacionalDominio.TombolaDTO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.CodeAnalysis.Differencing;
using System.Collections.Generic;
using System;

namespace ApiLoteriaNacional.Data
{
    public class TombolaData
    {

        private readonly string _cadenaConexion;

        public TombolaData(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("LNAPI");
        }

        #region DisenoPremioWebTombola

        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombola()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerDisenoPremioWebTombola";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombolaUltimaJugada()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
              
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerDisenoPremioWebTombolaUltimaJugada";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombolaID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerDisenoPremioWebTombolaID";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;
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
        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombolaIDpremio(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
             
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerDisenoPremioWebTombolaIDpremio";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;
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
        public async Task<RespuestaDTO> AgregarDisenoPremioWebTombola(TbDisenoPremioWebTombolaDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spAgregarDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PremioWebTombolaId", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@PremioWebTombolaId"].Value = dato.PremioWebTombolaId;

                cmd.Parameters.Add("@JuegoWebTombolaId", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@JuegoWebTombolaId"].Value = dato.JuegoWebTombolaId;

                cmd.Parameters.Add("@OrdenPremio", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@OrdenPremio"].Value = dato.OrdenPremio;

                cmd.Parameters.Add("@ColorPremio", SqlDbType.VarChar, 50).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorPremio"].Value = dato.ColorPremio;

                cmd.Parameters.Add("@ColorRGBPremio", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorRGBPremio"].Value = dato.ColorRGBPremio;

                cmd.Parameters.Add("@EstadoPremio", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@EstadoPremio"].Value = dato.EstadoPremio;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de agregación fue exitosa, como se esperaba.",
                    Object = new
                    {
                        PremioWebTombolaId = dato.PremioWebTombolaId,
                        JuegoWebTombolaId = dato.JuegoWebTombolaId,
                        OrdenPremio = dato.OrdenPremio,
                        ColorPremio = dato.ColorPremio,
                        ColorRGBPremio = dato.EstadoPremio
                    }
                };

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
        public async Task<RespuestaDTO> ActualizarDisenoPremioWebTombola(TbDisenoPremioWebTombolaDTO dato)
        { 
            try
            {

                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spActualizarDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_tbDisenoPremioWebTombola", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@COD_tbDisenoPremioWebTombola"].Value = dato.COD_tbDisenoPremioWebTombola;

                cmd.Parameters.Add("@PremioWebTombolaId", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@PremioWebTombolaId"].Value = dato.PremioWebTombolaId;

                cmd.Parameters.Add("@JuegoWebTombolaId", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@JuegoWebTombolaId"].Value = dato.JuegoWebTombolaId;

                cmd.Parameters.Add("@OrdenPremio", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@OrdenPremio"].Value = dato.OrdenPremio;

                cmd.Parameters.Add("@ColorPremio", SqlDbType.VarChar, 50).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorPremio"].Value = dato.ColorPremio;

                cmd.Parameters.Add("@ColorRGBPremio", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorRGBPremio"].Value = dato.ColorRGBPremio;

                cmd.Parameters.Add("@EstadoPremio", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@EstadoPremio"].Value = dato.EstadoPremio;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de actualización fue exitosa, como se esperaba.",
                    Object = new
                    {
                        PremioWebTombolaId = dato.PremioWebTombolaId,
                        JuegoWebTombolaId = dato.JuegoWebTombolaId,
                        OrdenPremio = dato.OrdenPremio,
                        ColorPremio = dato.ColorPremio,
                        ColorRGBPremio = dato.EstadoPremio
                    }
                };

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
        public async Task<RespuestaDTO> EliminarDisenoPremioWebTombola(int id)
        { 
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
              
                using SqlCommand cmd = new SqlCommand("dbo.spEliminarDisenoPremioWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
                };

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

        #endregion

        #region DisenoWebTombola

        public async Task<RespuestaDTO> ObtenerDisenoWebTombola()
        {
            try
            {

                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerDisenoWebTombola";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> ObtenerrDisenoWebTombolaID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerrDisenoWebTombolaID";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;
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
        public async Task<RespuestaDTO> ObtenerDisenoWebTombolaUltimaJugada()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                //using SqlCommand cmd = new SqlCommand("SELECT TOP(1) Tb2.* FROM dbo.tbJuegoWebTombola AS Tb1 INNER JOIN dbo.tbDisenoWebTombola AS Tb2  ON Tb1.COD_tbJuegoWebTombola = Tb2.JuegoWebTombolaId WHERE Tb1.EstadoTombola = 'Jugada' ORDER BY Tb1.COD_tbJuegoWebTombola DESC", sql);
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerDisenoWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerDisenoWebTombolaUltimaJugada";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> AgregarDisenoWebTombola(TbDisenoWebTombolaDTO dato)
        {
            try
            {
                
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
               
                using SqlCommand cmd = new SqlCommand("dbo.spAgregarDisenoWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@JuegoWebTombolaId", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@JuegoWebTombolaId"].Value = dato.JuegoWebTombolaId;

                cmd.Parameters.Add("@ImgCentralTombola", SqlDbType.VarChar, 500).Direction = ParameterDirection.Input;
                cmd.Parameters["@ImgCentralTombola"].Value = dato.ImgCentralTombola;

                cmd.Parameters.Add("@ColorFondoWebTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorFondoWebTombola"].Value = dato.ColorFondoWebTombola;

                cmd.Parameters.Add("@ColorRGBFondoWebTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorRGBFondoWebTombola"].Value = dato.ColorRGBFondoWebTombola;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de agregación fue exitosa, como se esperaba.",
                    Object = new
                    {
                        JuegoWebTombolaId = dato.JuegoWebTombolaId,
                        ImgCentralTombola = dato.ImgCentralTombola,
                        ColorFondoWebTombola = dato.ColorFondoWebTombola,
                        ColorRGBFondoWebTombola = dato.ColorRGBFondoWebTombola
                    }
                };

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
        public async Task<RespuestaDTO> ActualizarDisenoWebTombola(TbDisenoWebTombolaDTO dato)
        {
            try
            {

                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spActualizarDisenoWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_tbDisenoWebTombola", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@COD_tbDisenoWebTombola"].Value = dato.COD_tbDisenoWebTombola;

                cmd.Parameters.Add("@JuegoWebTombolaId", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@JuegoWebTombolaId"].Value = dato.JuegoWebTombolaId;

                cmd.Parameters.Add("@ImgCentralTombola", SqlDbType.VarChar, 500).Direction = ParameterDirection.Input;
                cmd.Parameters["@ImgCentralTombola"].Value = dato.ImgCentralTombola;

                cmd.Parameters.Add("@ColorFondoWebTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorFondoWebTombola"].Value = dato.ColorFondoWebTombola;

                cmd.Parameters.Add("@ColorRGBFondoWebTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@ColorRGBFondoWebTombola"].Value = dato.ColorRGBFondoWebTombola;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de actualización fue exitosa, como se esperaba.",
                    Object = new
                    {
                        JuegoWebTombolaId = dato.JuegoWebTombolaId,
                        ImgCentralTombola = dato.ImgCentralTombola,
                        ColorFondoWebTombola = dato.ColorFondoWebTombola,
                        ColorRGBFondoWebTombola = dato.ColorRGBFondoWebTombola
                    }
                };

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
        public async Task<RespuestaDTO> EliminarDisenoWebTombola(int id)
        { 
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spEliminarDisenoWebTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
                };

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

        #endregion

        #region JuegoTombola

        public async Task<RespuestaDTO> ObtenerJuegoTombola()
        { 
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerJuegoTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerJuegoTombola";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> ObtenerJuegoTombolaEnCurso()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
               
                using SqlCommand cmd = new SqlCommand("dbo.spObtenerJuegoTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerJuegoTombolaEnCurso";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> ObtenerJuegoTombolaID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerJuegoTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerJuegoTombolaID";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;
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
        public async Task<RespuestaDTO> ActualizarEstadoTombolaPorID(int id,string estadoTombola)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spActualizarEstadoTombolaPorID", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@Id"].Value = id;

                cmd.Parameters.Add("@EstadoTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@EstadoTombola"].Value = estadoTombola;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de actualización fue exitosa, como se esperaba.",
                    Object = new
                    {
                        COD_tbJuegoWebTombola = id,
                        EstadoTombola = estadoTombola,

                    }
                };

                return new RespuestaDTO(
                    200,
                    "Ok",
                    JsonConvert.SerializeObject(dtDatos)
                );
            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }
        public async Task<RespuestaDTO> AgregarJuegoTombola(TbJuegoWebTombolaDTO dato)
        { 
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spAgregarJuegoTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@DescripcionTombola", SqlDbType.VarChar, 100).Direction = ParameterDirection.Input;
                cmd.Parameters["@DescripcionTombola"].Value = dato.DescripcionTombola;

                cmd.Parameters.Add("@EstadoTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@EstadoTombola"].Value = dato.EstadoTombola;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de agregación fue exitosa, como se esperaba.",
                    Object = new
                    {
                        DescripcionTombola = dato.DescripcionTombola,
                        EstadoTombola = dato.EstadoTombola
                    }
                };

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
        public async Task<RespuestaDTO> ActualizarJuegoTombola(TbJuegoWebTombolaDTO dato)
        {
            try
            {

                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spActualizarJuegoTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_tbJuegoWebTombola", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@COD_tbJuegoWebTombola"].Value = dato.COD_tbJuegoWebTombola;

                cmd.Parameters.Add("@DescripcionTombola", SqlDbType.VarChar, 100).Direction = ParameterDirection.Input;
                cmd.Parameters["@DescripcionTombola"].Value = dato.DescripcionTombola;

                cmd.Parameters.Add("@EstadoTombola", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@EstadoTombola"].Value = dato.EstadoTombola;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de actualización fue exitosa, como se esperaba.",
                    Object = new
                    {
                        DescripcionTombola = dato.DescripcionTombola,
                        EstadoTombola = dato.EstadoTombola
                    }
                };

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
        public async Task<RespuestaDTO> EliminarJuegoTombola(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spEliminarJuegoTombola", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
                };

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

        #endregion

        #region PremioTombola
        public async Task<RespuestaDTO> ObtenerPremios()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerPremios", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerPremios";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = -1;
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
        public async Task<RespuestaDTO> ObtenerPremiosID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spObtenerPremios", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@tipoConsulta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Input;
                cmd.Parameters["@tipoConsulta"].Value = "ObtenerPremiosID";
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;
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
        public async Task<RespuestaDTO> AgregarPremio(TbPremioWebTombolaDTO dato)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spAgregarPremio", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@TipoPremio", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@TipoPremio"].Value = dato.TipoPremio;

                cmd.Parameters.Add("@DescripcionPremio", SqlDbType.VarChar, 100).Direction = ParameterDirection.Input;
                cmd.Parameters["@DescripcionPremio"].Value = dato.DescripcionPremio;

                cmd.Parameters.Add("@ValorPremio", SqlDbType.Decimal).Direction = ParameterDirection.Input;
                cmd.Parameters["@ValorPremio"].Value = dato.ValorPremio;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de agregación fue exitosa, como se esperaba.",
                    Object = new
                    {
                        TipoPremio = dato.TipoPremio,
                        DescripcionPremio = dato.DescripcionPremio,
                        ValorPremio = dato.ValorPremio
                    }
                };

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
        public async Task<RespuestaDTO> ActualizarPremio(TbPremioWebTombolaDTO dato)
        { 
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spActualizarPremio", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_tbPremioWebTombola", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@COD_tbPremioWebTombola"].Value = dato.COD_tbPremioWebTombola;

                cmd.Parameters.Add("@TipoPremio", SqlDbType.VarChar, 20).Direction = ParameterDirection.Input;
                cmd.Parameters["@TipoPremio"].Value = dato.TipoPremio;

                cmd.Parameters.Add("@DescripcionPremio", SqlDbType.VarChar, 100).Direction = ParameterDirection.Input;
                cmd.Parameters["@DescripcionPremio"].Value = dato.DescripcionPremio;

                cmd.Parameters.Add("@ValorPremio", SqlDbType.Decimal).Direction = ParameterDirection.Input;
                cmd.Parameters["@ValorPremio"].Value = dato.ValorPremio;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();
                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de actualización fue exitosa, como se esperaba.",
                    Object = new
                    {
                        TipoPremio = dato.TipoPremio,
                        DescripcionPremio = dato.DescripcionPremio,
                        ValorPremio = dato.ValorPremio
                    }
                };

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
        public async Task<RespuestaDTO> EliminarPremio(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);

                using SqlCommand cmd = new SqlCommand("dbo.spEliminarPremio", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Input;
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@co_msg", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ds_msg", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
                };

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
        #endregion
    }
}
