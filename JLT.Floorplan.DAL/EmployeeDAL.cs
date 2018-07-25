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
            throw new NotImplementedException();
        }
    }
}
