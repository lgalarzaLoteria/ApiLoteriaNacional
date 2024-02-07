using RestSharp;
using System.DirectoryServices;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using NuGet.Protocol.Plugins;
using Newtonsoft.Json;
using LoteriaNacionalDominio;
using static LoteriaNacionalDominio.SeguridadDTO;


namespace ApiLoteriaNacional.Data
{
    public class SeguridadData
    {
        private readonly IConfigurationSection _tradicionales;
        private readonly string _cadenaConexion;

        public SeguridadData(IConfiguration configuration)
        {
            _tradicionales = configuration.GetSection("Tradicionales");
            _cadenaConexion = configuration.GetConnectionString("LNAPI");
        }
        public async Task<RespuestaDTO> LoginActiveDirectory(LoginDTO usuario)
        {
            string respuestaActiveDirectory = string.Empty;
            RespuestaDTO loginRespuesta = new RespuestaDTO(0, "Ejecutado con exito",string.Empty);
            
            try
            {
                #region Active Directory
                respuestaActiveDirectory = existeUsuarioCentral(usuario);

                if (respuestaActiveDirectory == string.Empty)
                {
                    respuestaActiveDirectory = existeUsuarioPSD(usuario);
                    if (respuestaActiveDirectory == string.Empty)
                    {
                        loginRespuesta.CodigoError = 1;
                        loginRespuesta.MensajeError = "Usuario o contrasena incorrecta";
                    }
                    else
                    {
                        loginRespuesta.Body = respuestaActiveDirectory;
                    }
                        
                }
                else
                {
                    loginRespuesta.Body = respuestaActiveDirectory;
                }

                #endregion

                DataTable tabla = new DataTable();
                DataColumn column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "NombreUsuario";
                tabla.Columns.Add(column);
                DataRow row = tabla.NewRow();
                row["NombreUsuario"] = respuestaActiveDirectory;
                tabla.Rows.Add(row);

                loginRespuesta.Body = JsonConvert.SerializeObject(tabla);

                return loginRespuesta;

            }
            catch (Exception ex)
            {
                return new RespuestaDTO(-1, ex.Message, "");
            }
        }

        public async Task<RespuestaDTO> obtieneMenuUsuario(string codigoUsuario)
        {
            try
            {
                using SqlConnection sql = new SqlConnection(_cadenaConexion);
                using SqlCommand cmd = new SqlCommand("usuarioAccede", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@codigoUsuario", SqlDbType.VarChar, 20);
                cmd.Parameters["@codigoUsuario"].Value = codigoUsuario;
                cmd.Parameters.Add("@codigoError", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@mensajeError", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                await sql.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                DataTable dtDatos = new DataTable();
                dtDatos.Load(reader);
                reader.Close();

                var response = new RespuestaDTO(
                    Convert.ToInt32(cmd.Parameters["@codigoError"].Value),
                    cmd.Parameters["@mensajeError"].Value.ToString(),
                    JsonConvert.SerializeObject(dtDatos)
                    )
                    ;

                return response; 
            }
            catch (Exception e)
            {
                return new RespuestaDTO(-1, e.Message, "");
            }
        }

        #region Métodos Privados
        private string existeUsuarioCentral(LoginDTO usuario)
        {
            string dominioUsuario = string.Empty;
            string nombreCompleto = string.Empty;
            try
            {
                SearchResult sresult = null;
                #region Active Directory Central
                string dominioCentral = "CENTRAL";
                dominioUsuario = dominioCentral + @"\" + usuario.UserName;
                DirectoryEntry deCentral = new DirectoryEntry("LDAP://central.jbgye.org.ec", dominioUsuario, usuario.Password);
                DirectorySearcher dsearcherCentral = new DirectorySearcher(deCentral);
                dsearcherCentral.Filter = string.Format("(|(&(objectCategory=user)(sAMAccountName={0})))", usuario.UserName);
                sresult = dsearcherCentral.FindOne();
                if (sresult != null)
                    nombreCompleto = sresult.GetDirectoryEntry().Properties["displayName"][0].ToString();

                #endregion

                return nombreCompleto;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message.ToString());
                return string.Empty;
            }
        }
        private string existeUsuarioPSD(LoginDTO usuario)
        {
            string dominioUsuario = string.Empty;
            string nombreCompleto = string.Empty;
            try
            {
                SearchResult sresult = null;
                #region Active Directory PSD
                string dominioPSD = "192.168.1.242";
                dominioUsuario = dominioPSD + @"\" + usuario.UserName;
                DirectoryEntry dePSD = new DirectoryEntry("LDAP://192.168.1.242", dominioUsuario, usuario.Password);
                DirectorySearcher dsearcherPSD = new DirectorySearcher(dePSD);
                dsearcherPSD.Filter = string.Format("(|(&(objectCategory=user)(sAMAccountName={0})))", usuario.UserName);
                sresult = dsearcherPSD.FindOne();
                if (sresult != null)
                    nombreCompleto = sresult.GetDirectoryEntry().Properties["displayName"][0].ToString();

                #endregion

                return nombreCompleto;

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message.ToString());
                return string.Empty;
            }
        }
        #endregion

        



    }
}
