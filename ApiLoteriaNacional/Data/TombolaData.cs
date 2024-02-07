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
                string stringQuery = @"SELECT Tb1.*, Tb2.DescripcionPremio, Tb2.TipoPremio, CONVERT(varchar(30),Tb2.ValorPremio) AS ValorPremio, Tb3.DescripcionTombola, Tb3.EstadoTombola " +
                "FROM dbo.tbDisenoPremioWebTombola AS Tb1 " +
                "INNER JOIN dbo.tbPremioWebTombola AS Tb2 " +
                "ON Tb1.PremioWebTombolaId = Tb2.COD_tbPremioWebTombola " +
                "INNER JOIN dbo.tbJuegoWebTombola AS Tb3 " +
                "ON Tb1.JuegoWebTombolaId = Tb3.COD_tbJuegoWebTombola";
                using SqlCommand cmd = new SqlCommand(stringQuery, sql);
               
                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombolaUltimaJugada()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT TOP(1) Tb2.* FROM dbo.tbJuegoWebTombola AS Tb1 INNER JOIN dbo.tbDisenoPremioWebTombola AS Tb2  ON Tb1.COD_tbJuegoWebTombola = Tb2.JuegoWebTombolaId WHERE Tb1.EstadoTombola = 'Jugada' ORDER BY Tb1.COD_tbJuegoWebTombola DESC", sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombolaID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tbDisenoPremioWebTombola WHERE JuegoWebTombolaId = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerDisenoPremioWebTombolaIDpremio(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT TOP(1) * FROM dbo.tbDisenoPremioWebTombola WHERE PremioWebTombolaId = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> AgregarDisenoPremioWebTombola(TbDisenoPremioWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"INSERT dbo.tbDisenoPremioWebTombola (PremioWebTombolaId,JuegoWebTombolaId,OrdenPremio,ColorPremio,ColorRGBPremio,EstadoPremio)" +
                "VALUES (" + dato.PremioWebTombolaId + "," + dato.JuegoWebTombolaId + "," + dato.OrdenPremio + ",'" + dato.ColorPremio + "','" + dato.ColorRGBPremio + "','" + dato.EstadoPremio +"')";

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> ActualizarDisenoPremioWebTombola(TbDisenoPremioWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"UPDATE dbo.tbDisenoPremioWebTombola SET " +
                "PremioWebTombolaId = " + dato.PremioWebTombolaId + ",JuegoWebTombolaId = " + dato.JuegoWebTombolaId + ",OrdenPremio = " + dato.OrdenPremio + ",ColorPremio = '" + dato.ColorPremio + "',ColorRGBPremio = '" + dato.ColorRGBPremio + "',EstadoPremio = '" + dato.EstadoPremio +"'"+
                "WHERE COD_tbDisenoPremioWebTombola = " + dato.COD_tbDisenoPremioWebTombola;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> EliminarDisenoPremioWebTombola(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tbDisenoPremioWebTombola WHERE COD_tbDisenoPremioWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
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

        #endregion

        #region DisenoWebTombola

        public async Task<RespuestaDTO> ObtenerDisenoWebTombola()
        {
            try
            {

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                string stringQuery = @"SELECT Tb1.*, Tb2.DescripcionTombola " +
               "FROM dbo.TbDisenoWebTombola AS Tb1 " +
               "INNER JOIN dbo.tbJuegoWebTombola AS Tb2 " +
               "ON Tb1.JuegoWebTombolaId = Tb2.COD_tbJuegoWebTombola ";
                using SqlCommand cmd = new SqlCommand(stringQuery, sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerrDisenoWebTombolaID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.TbDisenoWebTombola WHERE COD_tbDisenoWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerDisenoWebTombolaUltimaJugada()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT TOP(1) Tb2.* FROM dbo.tbJuegoWebTombola AS Tb1 INNER JOIN dbo.tbDisenoWebTombola AS Tb2  ON Tb1.COD_tbJuegoWebTombola = Tb2.JuegoWebTombolaId WHERE Tb1.EstadoTombola = 'Jugada' ORDER BY Tb1.COD_tbJuegoWebTombola DESC", sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> AgregarDisenoWebTombola(TbDisenoWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"INSERT dbo.tbDisenoWebTombola (JuegoWebTombolaId,ImgCentralTombola,ColorFondoWebTombola,ColorRGBFondoWebTombola)" +
                "VALUES (" + dato.JuegoWebTombolaId + ",'" + dato.ImgCentralTombola + "','" + dato.ColorFondoWebTombola + "','" + dato.ColorRGBFondoWebTombola +"')";

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> ActualizarDisenoWebTombola(TbDisenoWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"UPDATE dbo.tbDisenoWebTombola SET " +
                "JuegoWebTombolaId = " + dato.JuegoWebTombolaId + ",ImgCentralTombola = '" + dato.ImgCentralTombola + "',ColorFondoWebTombola = '" + dato.ColorFondoWebTombola + "',ColorRGBFondoWebTombola = '" + dato.ColorRGBFondoWebTombola +"'"+
                "WHERE COD_tbDisenoWebTombola = " + dato.COD_tbDisenoWebTombola;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> EliminarDisenoWebTombola(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tbDisenoWebTombola WHERE COD_tbDisenoWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
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

        #endregion

        #region JuegoTombola

        public async Task<RespuestaDTO> ObtenerJuegoTombola()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tbJuegoWebTombola", sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerJuegoTombolaEnCurso()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT TOP(1) * FROM dbo.tbJuegoWebTombola WHERE EstadoTombola = 'En Curso'", sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerJuegoTombolaID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tbJuegoWebTombola WHERE COD_tbJuegoWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ActualizarEstadoTombolaPorID(int id,string estadoTombola)
        {
            try
            {
                string stringSqlQuery = @"UPDATE dbo.tbJuegoWebTombola SET " +
                "EstadoTombola = '" + estadoTombola +
                "' WHERE COD_tbJuegoWebTombola = " + id;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
                string stringSqlQuery = @"INSERT dbo.tbJuegoWebTombola (DescripcionTombola,EstadoTombola)" +
                "VALUES ('" + dato.DescripcionTombola + "','" + dato.EstadoTombola + "')";

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> ActualizarJuegoTombola(TbJuegoWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"UPDATE dbo.tbJuegoWebTombola SET " +
                "DescripcionTombola = '" + dato.DescripcionTombola + "',EstadoTombola = '" + dato.EstadoTombola +"'"+
                "WHERE COD_tbJuegoWebTombola = " + dato.COD_tbJuegoWebTombola;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> EliminarJuegoTombola(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tbJuegoWebTombola WHERE COD_tbJuegoWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
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

        #endregion

        #region PremioTombola
        public async Task<RespuestaDTO> ObtenerPremios()
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tbPremioWebTombola", sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> ObtenerPremiosID(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tbPremioWebTombola WHERE COD_tbPremioWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

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
        public async Task<RespuestaDTO> AgregarPremio(TbPremioWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"INSERT dbo.tbPremioWebTombola(TipoPremio,DescripcionPremio,ValorPremio)" +
                "VALUES ('" + dato.TipoPremio + "','" + dato.DescripcionPremio + "',"+ dato.ValorPremio + ")";

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> ActualizarPremio(TbPremioWebTombolaDTO dato)
        {
            try
            {
                string stringSqlQuery = @"UPDATE dbo.tbPremioWebTombola SET " +
                "TipoPremio = '" + dato.TipoPremio + "',DescripcionPremio = '" + dato.DescripcionPremio +"',ValorPremio = "+ dato.ValorPremio + 
                "WHERE COD_tbPremioWebTombola = " + dato.COD_tbPremioWebTombola;

                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand(stringSqlQuery, sql);

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
        public async Task<RespuestaDTO> EliminarPremio(int id)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tbPremioWebTombola WHERE COD_tbPremioWebTombola = "+id.ToString(), sql);

                await sql.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                reader.Close();
                object dtDatos = new
                {
                    Description = "La operación de eliminación fue exitosa, como se esperaba."
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
        #endregion
    }
}
