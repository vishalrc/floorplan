using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace JLT.MySql.DataAccess
{
    public class Parameters
    {
        public System.Collections.Hashtable collection = new System.Collections.Hashtable();

        public void Add(MySqlParameter param)
        {
            collection.Add(param.ParameterName, param);
        }

        public MySqlParameter Get(string paramName)
        {
            return (MySqlParameter)collection[paramName];
        }

        public void Add(string parameterName, object value, System.Data.ParameterDirection parameterDirection)
        {
            MySqlParameter param = new MySqlParameter(parameterName, value)
            {
                Direction = parameterDirection
            };
            collection.Add(parameterName, param);
        }

    }
}