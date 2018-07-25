using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.JLT.Entity;
using com.JLT.DataAccess;
using MySql.Data.MySqlClient;
using System.Data;
using com.JLT.Common.Utility;
using System.Globalization;

namespace com.JLT.DAL
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
