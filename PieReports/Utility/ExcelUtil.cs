using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text; // For Encoding.RegisterProvider

namespace PieReports.Utility
{
    public static class ExcelUtil
    {
        public static DataTable ReadExcelFile(string filePath)
        {
            // Required for .NET Core to register code page providers
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Treat the first row as a header
                        }
                    });

                    if (result.Tables.Count > 0)
                    {
                        foreach (DataColumn column in result.Tables[0].Columns)
                        {
                            column.ColumnName = column.ColumnName.Replace(" ", ""); // Remove spaces
                        }
                        return result.Tables[0]; // Return the first sheet as a DataTable
                    }
                }
            }
            return null;
        }
        public static DataTable ReadExcelFile(IFormFile file)
        {
            // Required for .NET Core to register code page providers
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Treat the first row as a header
                            }
                        });

                        if (result.Tables.Count > 0)
                        {
                            foreach (DataColumn column in result.Tables[0].Columns)
                            {
                                column.ColumnName = column.ColumnName.Replace(" ", ""); // Remove spaces
                            }
                            return result.Tables[0]; // Return the first sheet as a DataTable
                        }
                    }
                }
            }
            return null;
        }
        public static List<T> ToList<T>(DataTable table) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                T obj = new T();
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    if (table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}