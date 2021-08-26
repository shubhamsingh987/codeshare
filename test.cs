using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;


namespace sql
{
    class Program
    {
        static void Main(string[] args)
        {
            String SqlconString = "Data Source=DESKTOP-QQ8AQNL\\SQLEXPRESS;Initial Catalog=Sample;Trusted_Connection=True;MultipleActiveResultSets=True";
            SqlConnection Conn = new SqlConnection(SqlconString);
            SqlCommand GetTable = new SqlCommand("select * from tblEmployee", Conn);
            SqlDataReader reader = GetTable.ExecuteReader();
            int count = reader.FieldCount;
          

            Conn.Open();

            var returnValue = new DataTable();

            using (var adapter = new SqlDataAdapter(GetTable))
            {
                adapter.Fill(returnValue);
            }
            
            var sw = new StreamWriter("C:\\Users\\singh\\Documents\\test.csv", false);
            var table = returnValue;

            int icolcount = table.Columns.Count;

            if (true)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    sw.Write(table.Columns[i]);
                    if (i < icolcount - 1)
                        sw.Write(",");
                }

                sw.Write(sw.NewLine);
            }

            foreach (DataRow drow in table.Rows)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    if (!Convert.IsDBNull(drow[i]))
                        sw.Write(drow[i].ToString());
                    if (i < icolcount - 1)
                        sw.Write(",");
                }
                sw.Write(sw.NewLine);
            }

            sw.Close();
            reader.Close();
            Conn.Close();
        }
    }
}
