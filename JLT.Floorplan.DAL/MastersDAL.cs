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
    public class MastersDAL : IDisposable
    {
        public List<Country> GetCountry(Country obj)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_id", obj.id, ParameterDirection.Input);
                parameters.Add("p_name", obj.name, ParameterDirection.Input);
                parameters.Add("p_isactive", obj.isactive, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_country, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<Country>(dt);
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

        public List<seat> GetSeats(seat objseat)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_seatid", objseat.seatid, ParameterDirection.Input);
                parameters.Add("p_floorid", objseat.floorid, ParameterDirection.Input);
                parameters.Add("p_seatlabel", objseat.seatlabel, ParameterDirection.Input);
                parameters.Add("p_booked", objseat.isbooked, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_seat, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<seat>(dt);
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

        public List<buildingfloor> Getfloors(buildingfloor objbuildingfloor)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_floorid", objbuildingfloor.floorid, ParameterDirection.Input);
                parameters.Add("p_floorname", objbuildingfloor.floorname, ParameterDirection.Input);
                parameters.Add("p_buildingname", objbuildingfloor.buildingname, ParameterDirection.Input);
                parameters.Add("p_isactive", objbuildingfloor.isactive, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_floor, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<buildingfloor>(dt);
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

        public int AllocateSeat(seatallocation objseatallocation)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_allocationid", objseatallocation.allocationid, ParameterDirection.Input);
                parameters.Add("p_employeeno", objseatallocation.employeeno, ParameterDirection.Input);
                parameters.Add("p_seatid", objseatallocation.seatno, ParameterDirection.Input);
                parameters.Add("p_startdate", objseatallocation.startdate, ParameterDirection.Input);
                parameters.Add("p_enddate", objseatallocation.enddate, ParameterDirection.Input);

                int result;
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        result = db.ExecuteNonQuery(conn, tran, CommandType.StoredProcedure, Constants.StoredProcedures.uasp_seatallocation, parameters);
                        tran.Commit();
                    }
                    catch (MySqlException ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
                return result;
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

        public dashboard GetDashboard(string floorid)
        {
            MySqlDatabaseFactory db = new MySqlDatabaseFactory();
            Parameters parameters = new Parameters();
            MySqlDataReader reader = null;
            MySqlConnection conn = db.GetDatabaseConnection();
            try
            {
                DataTable dt = new DataTable();
                parameters.Add("p_floorid", floorid, ParameterDirection.Input);
                using (reader = db.GetReader(conn, CommandType.StoredProcedure, Constants.StoredProcedures.ussp_dashboard, parameters))
                {
                    dt = db.Load(reader, true);
                }
                return CommonUtility.ToList<dashboard>(dt).FirstOrDefault();
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

        public void Dispose() { }
    }
}
