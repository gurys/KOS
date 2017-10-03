using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KosGSM
{
    public class GSM
    {
        string _conn;

        public GSM(string conn)
        {
            _conn = conn;
        }

        public void SendAll()
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select z.LiftId, t.Ttx, z.Category, z.[From], ui.Family, ui.IO, z.Text, z.Start from Zayavky z " +
                    "join Ttx t on z.TtxId=t.Id join UserInfo ui on z.UserId=ui.UserId where z.[Status]=0", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string s = dr["Category"].ToString() + " по лифту " + dr["LiftId"].ToString() +
                        " (" + dr["Ttx"].ToString() + "): " + dr["Text"].ToString() + " от " + dr["Family"].ToString() +
                        " " + dr["IO"].ToString() + " " + ((DateTime)dr["Start"]).ToString();
                    SendSMS(dr["LiftId"].ToString(), s);
                }
                dr.Close();
            }
        }

        public void SendSMS(string liftId, string text)
        {
            List<string> phones = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.N from Phones p " +
                    "join WorkerLifts wl on wl.UserId=p.UserId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where wl.LiftId=@liftId union " +
                    "select p.N from Phones p " +
                    "join Users u on u.UserId=p.UserId " +
                    "join UsersInRoles uir on uir.UserId=u.UserId " +
                    "join Roles r on r.RoleId=uir.RoleId " +
                    "where r.RoleName='Manager'", conn);
                cmd.Parameters.AddWithValue("liftId", liftId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    phones.Add(reader[0].ToString());
                reader.Close();
            }
            foreach (string p in phones)
                Call(p, text);
        }

        void Call(string phone, string text)
        {
        }
    }
}
