using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace pollutionSoap
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PollutionService : IPollutionService
    {
        private String connStr =
"Server=tcp:teamawesome.database.windows.net,1433;Initial Catalog=Co2;Persist Security Info=False;User ID=riso;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
;

        private static Measurements ReadSensorData(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            int measurement = reader.GetInt32(1);
            DateTime dateCreated = reader.GetDateTime(2);

            Measurements measurements = new Measurements
            {
                Id = id,
                Measurement = measurement,
                DateCreated = dateCreated

            };
            return measurements;
        }

        public IList<Measurements> GetAllData()
        {
            const string selectAllSensorData = "SELECT* FROM dbo.Measurements order by Id";

            using (SqlConnection databaseConnection = new SqlConnection(connStr))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllSensorData, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Measurements> sensorDataList = new List<Measurements>();
                        while (reader.Read())
                        {
                            Measurements measurement = ReadSensorData(reader);
                            sensorDataList.Add(measurement);
                        }
                        return sensorDataList;
                    }
                }
            }
        }
    }
}
