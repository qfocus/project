﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace runmu.Business
{
    public class CourseService : Service
    {
        private static string selectSql = @"SELECT c.ID, c.teacherID, c.name as '课程', c.price as '价格', t.name as '教师' from course as c join teacher as t on c.teacherId =  t.id";
        private static string insertSql = @"INSERT INTO `course`
                                           (`teacherID`,`name`,`price`,`createdTime`,`lastModifiedTime`) VALUES 
                                           (@teacherID, @name, @price, @date, @date);";
        private static string updateSql = @"UPDATE course set name = @name, price = @price, 
                                            lastModifiedTime = @date WHERE ID = @id;";

        public override bool Add(Model model)
        {
            string date = DateTime.Now.ToString();
            using (SQLiteConnection conn = new SQLiteConnection(Constants.DBCONN))
            {
                DateTime now = DateTime.Now;
                SQLiteCommand cmd = new SQLiteCommand(insertSql, conn);
                cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@teacherID", Value = model.TeacherId, DbType = DbType.Int32 });
                cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", Value = model.Name, DbType = DbType.String });
                cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@price", Value = model.Price, DbType = DbType.Double });
                cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@date", Value = date, DbType = DbType.String });

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return true;
        }

        public override bool Update(DataTable table)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constants.DBCONN))
            {
                conn.Open();
                DateTime now = DateTime.Now;
                foreach (DataRow row in table.Rows)
                {
                    if (row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    int id = Convert.ToInt32(row[0]);
                    string name = row[2].ToString();
                    double price = Convert.ToDouble(row[3]);
                    SQLiteCommand cmd = new SQLiteCommand(updateSql, conn);
                    cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@id", Value = id, DbType = DbType.Int32 });
                    cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@name", Value = name, DbType = DbType.String });
                    cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@price", Value = price, DbType = DbType.Double });
                    cmd.Parameters.Add(new SQLiteParameter() { ParameterName = "@date", Value = now.ToString(), DbType = DbType.String });

                    cmd.ExecuteNonQuery();

                }
                conn.Close();
            }
            return true;
        }

        protected override string SelectAllSql()
        {
            return selectSql;
        }
    }
}
