using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;



namespace AccesoBase
{
    public class CSE_01_CONEXION_2
    {

        /// ///////////////////////////////////////////////////////////////////////////////////////////////     Se declara to para que pueda ser a traves de sql server la conexion a computadora interna
        /// ///////////////////////////////////////////////////////////////////////////////////////////////

        public string cadena_sql_interno;                       ///// nos ayudara a escribir las sentencias en SQL

        public SqlDataReader leer_interno;                      ///// para leer el resultado
        public SqlConnectionStringBuilder constructor_interno;  ///// para pder construir la conexion a la base
        public SqlCommand cmd_interno;                          ///// para ingresar procedimientos almacenados
        public SqlConnection cnn_interno;                       ///// abrimos y cerramos la conexion

        //public static string preconex = ConnectionStrings["SMAv2.0.Properties.Settings.DB_INTMIOD_ATENCOConnectionString"].ConnectionString;
        //SqlConnection conexion = new SqlConnection(preconex);

        /// ///////////////////////////////////////////////////////////////////////////////////////////////     Se declara todo para que pueda conectarse a la base de internet al MySql
        /// ///////////////////////////////////////////////////////////////////////////////////////////////

        public string cadena_MySql_interno;                             ///// nos ayudara a escribir las sentencias en SQL

        public MySqlDataReader leer_internoMySql;                       ///// para leer el resultado
        public MySqlConnectionStringBuilder constructorMySql_interno;   ///// para pder construir la conexion a la base
        public MySqlCommand cmdMySql_interno;                           ///// para ingresar procedimientos almacenados
        public MySqlConnection cnnMySql_interno;                        ///// abrimos y cerramos la conexion

        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////

        public void conectar_baseMySql_interno()
        {
            constructorMySql_interno = new MySqlConnectionStringBuilder();
            constructorMySql_interno.Server = "74.48.106";
            constructorMySql_interno.UserID = "sapasepagos";
            constructorMySql_interno.Password = "s#OGY2%O";
            constructorMySql_interno.Database = "bdSapase";

            cnnMySql_interno = new MySqlConnection(constructorMySql_interno.ToString());
            cmdMySql_interno = cnnMySql_interno.CreateCommand();
        }

        public void openMySql_interno() { cmdMySql_interno.CommandText = cadena_MySql_interno; }
        public void openMySql_c_interno() { cnnMySql_interno.Open(); }
        public void cerrarMySql_interno() { cnnMySql_interno.Close(); cnnMySql_interno.Dispose(); }
        public void cadena_MySql_cmd_interno() { cmdMySql_interno.CommandText = cadena_MySql_interno; }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////

        public void cambiarConexion(string cadenaConex)
        {
            string cadenaNueva = cadenaConex;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["SMAv2.0.Properties.Settings.DB_INTMIOD_ATENCOConnectionString"].ConnectionString = cadenaNueva;
            config.Save(ConfigurationSaveMode.Modified, true);
        }

        public void conectar_base_interno() 
        {
            constructor_interno = new SqlConnectionStringBuilder();

            //desarrollo
            constructor_interno.DataSource = "25.52.13.234";
            constructor_interno.UserID = "sa";
            constructor_interno.Password = "Songui2025";
            constructor_interno.InitialCatalog = "DB_INTMIOD_ATENCO_DESARROLLO_SEPTIEMBRE";

            /////charly
            //constructor_interno.DataSource = "KALUSHA10\\SONGUI2005";
            //constructor_interno.UserID = "sa";
            //constructor_interno.Password = "Songui2019";
            //constructor_interno.InitialCatalog = "DB_INTMIOD_ATENCO";


            //////produccion
            //constructor_interno.DataSource = "SVR-SAN-MATEO\\PROYECTO_SONGUI";
            //constructor_interno.UserID = "sa";
            //constructor_interno.Password = "Songui2019";
            //constructor_interno.InitialCatalog = "DB_INTMIOD_ATENCO";

            //constructor_interno.DataSource = "25.52.13.234";
            //constructor_interno.UserID = "sa";
            //constructor_interno.Password = "Songui2025";
            //constructor_interno.InitialCatalog = "DB_INTMIOD_ATENCO_DESARROLLO";

            //constructor_interno.DataSource = "192.168.0.22\\PROYECTO_SONGUI";
            //constructor_interno.UserID = "sa";
            //constructor_interno.Password = "Songui2019";
            //constructor_interno.InitialCatalog = "DB_INTMIOD_ATENCO";

            //constructor_interno.DataSource = "25.51.96.16\\SAPATEST";
            //constructor_interno.UserID = "sa";
            //constructor_interno.Password = "Songuisapa21";
            //constructor_interno.InitialCatalog = "DESARROLLOSMA25";

            cnn_interno = new SqlConnection(constructor_interno.ToString());
            cmd_interno = cnn_interno.CreateCommand();
        }
        public void open_interno() { cmd_interno.CommandText = cadena_sql_interno; }
        public void open_c_interno() { cnn_interno.Open(); }
        public void cerrar_interno() { cnn_interno.Close(); cnn_interno.Dispose(); }
        public void cadena_sql_cmd_interno() { cmd_interno.CommandText = cadena_sql_interno; }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////     coneccion a sapase VPN
        /// ///////////////////////////////////////////////////////////////////////////////////////////////

        public string cadena_sql_sapase;                       ///// nos ayudara a escribir las sentencias en SQL
        public SqlDataReader leer_sapase;                      ///// para leer el resultado
        public SqlConnectionStringBuilder constructor_sapase;  ///// para pder construir la conexion a la base
        public SqlCommand cmd_sapase;                          ///// para ingresar procedimientos almacenados
        public SqlConnection cnn_sapase;                       ///// abrimos y cerramos la conexion

        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////


    }
}
