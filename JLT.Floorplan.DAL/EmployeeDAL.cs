using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JLT.Floorplan.Entity;
using JLT.MySql.DataAccess;
using MySql.Data.MySqlClient;
using System.Data;
using JLT.Common.Utility;
using System.Globalization;

namespace JLT.Floorplan.DAL
{
    public class EmployeeDAL : IDisposable
    {

        public void Dispose() { }

        public bool SaveEmployee(Employee objEntity)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployee(Employee objEntity)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_associateid", objEntity.associateid, ParameterDirection.Input);
                parameters.Add("p_associateno", objEntity.associateno, ParameterDirection.Input);
                parameters.Add("p_departmentid", objEntity.departmentid, ParameterDirection.Input);
                parameters.Add("p_departmentname", objEntity.departmentname, ParameterDirection.Input);
                parameters.Add("p_name", objEntity.name, ParameterDirection.Input);
                parameters.Add("p_isactive", objEntity.isactive, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_associate, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<Employee>(dt);
            }
            catch (MySqlException odbcEx)
            {
                throw odbcEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { db.CloseConnection(conn); }


        }
    }
}
