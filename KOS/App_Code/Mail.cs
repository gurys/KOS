using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace KOS.App_Code


{
    public class Mail
    {
        string _conn=string.Empty;

        public Mail(string conn)
        {
            _conn = conn;
        }

        class Data
        {
            public string Mail { get; set; }
            public string Fio { get; set; }
            public string Role { get; set; }
            public string UserId { get; set; }
        }

        public void SendMsg(string liftId, int ZayavkaId)
        {
            List<Data> phones = new List<Data>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.mail, ui.Family, ui.IO, r.RoleName, u.UserId from Mails p " +
                    "join WorkerLifts wl on wl.UserId=p.UserId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "join UsersInRoles uir on uir.UserId=u.UserId " +
                    "join UserInfo ui on ui.UserId=u.UserId " +
                    "join Roles r on r.RoleId=uir.RoleId " +
                    "where wl.LiftId=@liftId union " +
                    "select p.mail, ui.Family, ui.IO, r.RoleName, u.UserId from Mails p " +
                    "join Users u on u.UserId=p.UserId " +
                    "join UsersInRoles uir on uir.UserId=u.UserId " +
                    "join UserInfo ui on ui.UserId=u.UserId " +
                    "join Roles r on r.RoleId=uir.RoleId " +
                    "where r.RoleName='Manager'", conn);
                cmd.Parameters.AddWithValue("liftId", liftId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    phones.Add(new Data() 
                    { 
                        Mail = reader["mail"].ToString(),
                        Fio = reader["Family"].ToString() + " " + reader["IO"].ToString(),
                        Role = reader["RoleName"].ToString(),
                        UserId = reader["UserId"].ToString() 
                    });
                }
                reader.Close();
            }
            string text = "Новая заявка от ";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select z.LiftId, z.Start, ui.Family, ui.IO, t.Ttx from Zayavky z " +
                    "join UserInfo ui on z.UserId=ui.UserId " +
                    "join LiftsTtx lt on lt.LiftId=z.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id " +
                    "where t.TtxTitleId=1 and z.Id=@zId", conn);
                cmd.Parameters.AddWithValue("zId", ZayavkaId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                text += dt.Rows[0]["Family"].ToString() + " " + dt.Rows[0]["IO"].ToString() + ", лифт № " +
                    dt.Rows[0]["LiftId"].ToString() + " по адресу: " + dt.Rows[0]["Ttx"].ToString() + ", описание: " + dt.Rows[0]["Text"].ToString() + " " + 
                    ", отправлено " + ((DateTime)dt.Rows[0]["Start"]).ToString() + Environment.NewLine;
                DateTime d = DateTime.Now;
                foreach (Data p in phones)
                {
                    cmd = new SqlCommand("insert into Notification (ZayavkaId, UserId, [Date]) values (@zId, @user, @d)", conn);
                    cmd.Parameters.AddWithValue("zId", ZayavkaId);
                    cmd.Parameters.AddWithValue("user", p.UserId);
                    cmd.Parameters.AddWithValue("d", d);
                }
            }
            foreach (Data p in phones)
            {
                string s = text;
                if (p.Role == "Manager")
                {
                    s += "Разослано:";
                    for (int i = 0; i < phones.Count; i++)
                    {
                        if (i > 0)
                            s += ", ";
                        s += phones[i].Fio + " (" + phones[i].Mail + ")";
                    }
                }
                Send(p.Mail, s);
            }
        }

        void Send(string mail, string text)
        {
            MailMessage message = new MailMessage("office@emicatech.com", mail, "Новая заявка", text);
        //    message.Attachments.Add(new Attachment("C://Temp//zkp.pdf"));
            SmtpClient client = new SmtpClient("127.0.0.1");
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            //    client.Credentials = new System.Net.NetworkCredential("info@emicatech.com", "pass"); 
            client.Send(message);
        }
    }
}
