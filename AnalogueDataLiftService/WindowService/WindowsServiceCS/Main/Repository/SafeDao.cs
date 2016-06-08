using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WindowsServiceCS.Main.Repository
{
    class SafeDao
    {
        //Checking to make sure ALL values from the database are in the correct format
        public static int SafeGetInt(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? 0 : dataReader.GetInt32(columnIndex);
        }

        public static string SafeGetString(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? string.Empty : dataReader.GetString(columnIndex);
        }

        public static double SafeGetDouble(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? 0 : dataReader.GetDouble(columnIndex);
        }

        public static short SafeGetShort(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? (short)0 : dataReader.GetInt16(columnIndex);
        }

        public static DateTime SafeGetDateTime(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? DateTime.MinValue : dataReader.GetDateTime(columnIndex);
        }

        public static byte SafeGetByte(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? (byte)0 : dataReader.GetByte(columnIndex);
        }

        public static bool SafeGetBool(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? false : dataReader.GetBoolean(columnIndex);
        }

        public static decimal SafeGetDecimal(SqlDataReader dataReader, int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex) ? 0 : dataReader.GetDecimal(columnIndex);
        }
    }
}
