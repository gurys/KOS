using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KOS.App_Code
{
    public class Base
    {
        string _conn;
       
        public static List<string> months = new List<string>() { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };

        public class UserInfo
        {
            public string Family { get; set; }
            public string IO { get; set; }
            public int HourBeg { get; set; }
            public int HourEnd { get; set; }
            public int Lunch { get; set; }
        }

        public class Zayavka
        {
            public string Url { get; set; }
            public string Title { get; set; }
        }

        public class PersonData : Object
        {
            public string Title { get; set; }
            public string Id { get; set; }
            public string UserName { get; set; }
            public override string ToString()
            {
                return Title;
            }
        }

        public class Holliday
        {
            public DateTime Date { get; set; }
            public int Id { get; set; }
        }

        public class UserLift
        {
            public string Address { get; set; }
            public string LiftId { get; set; }
            public bool Checked { get; set; }
        }

        public class LiftRep
        {
            public string Title { get; set; }
            public string LiftId { get; set; }
            public string Url { get; set; }
            public DateTime Date { get; set; }
            public bool Done { get; set; } 
        }
        public class MyObjectDataSource
        {

            public class DataItem
            {
                public string Name { get; set; }
                public double TimingUp { get; set; }
            }

            public DataItem[] GetData()
            {
                return new DataItem[] {
            new DataItem() {Name = "застревание", TimingUp = 30},
            new DataItem() {Name = "останов", TimingUp = 30},
            new DataItem() {Name = "внеплановые раб.", TimingUp = 20},
            new DataItem() {Name = "авария", TimingUp = 20}
        };
            }
        }
        public class Equipment
        {
                     public string Manufacturer { get; set; }
                     public string Model { get; set; }
                     public string Area { get; set; }
                     public string Node { get; set; }
                     public string Pozition { get; set; }
                     public string Name { get; set; }
                     public string NameId { get; set; }
        }

        public class Work
        {
            public string IdU { get; set; }
            public string LiftId { get; set; }
            public DateTime Date { get; set; }
            public string Worker { get; set; }
            public string Url { get; set; }
        }

        public class StoppedLift
        {
            public string LiftId { get; set; }
            public string Url { get; set; }
        }

        public class UM
        {
            public string IdUM { get; set; }
            public string IdU { get; set; }
            public string IdM { get; set; }
            public string Working { get; set; }
            public string Stopped { get; set; }
            public string Url { get; set; }
        }

        public class User : Object
        {
            public string Fio { get; set; }
            public string UserName { get; set; }
            public string UserId { get; set; }
            public override string ToString()
            {
                return Fio;
            }
        }

        public class CheckedLift
        {
            public string Title { get; set; }
            public bool Checked { get; set; }
        }

        public class EnviromentAddress : Object
        {
            public string Address { get; set; }
            public int Id { get; set; }
            public override string ToString()
            {
                return Address;
            }
        }

        public Base(string conn)
        {
            _conn = conn;
        }

        public List<string> GetEnviromentType()
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select [Type] from EnviromentType", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr[0].ToString());
                dr.Close();
            }
            return data;
        }

        public List<EnviromentAddress> GetEnviromentAddresses()
        {
            List<EnviromentAddress> data = new List<EnviromentAddress>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Address, Id from EnviromentAddresses order by Address", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(new EnviromentAddress()
                        {
                            Address = dr["Address"].ToString(),
                            Id = (int) dr["Id"]
                        });
                dr.Close();
            }
            return data;
        }

        public List<DateTime> GetDaysHolliday(string userId, DateTime d1, DateTime d2)
        {
            List<DateTime> data = new List<DateTime>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select h.[Date] from Holidays h where h.UserId=@id and h.[Date] between @d1 and @d2 order by h.[Date]", conn);
                cmd.Parameters.AddWithValue("id", userId);
                cmd.Parameters.AddWithValue("d1", d1);
                cmd.Parameters.AddWithValue("d2", d2);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add((DateTime)dr[0]);
                dr.Close();
            }
            return data;
        }

        public List<User> GetUsers()
        {
            List<User> data = new List<User>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family+' '+ui.IO as fio, u.UserName, u.UserId from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId order by ui.Family, ui.IO", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(new User()
                    {
                        Fio = dr["fio"].ToString(),
                        UserId = dr["UserId"].ToString(),
                        UserName = dr["UserName"].ToString()
                    });
                dr.Close();
            }
            return data;
        }

        public string GetWorker(string liftId)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO from WorkerLifts wl " +
                    "join UserInfo ui on wl.UserId=ui.UserId " +
                    "where wl.LiftId=@liftId", conn);
                cmd.Parameters.AddWithValue("liftId", liftId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count < 1) return string.Empty;
                return dt.Rows[0]["Family"].ToString() + " " + dt.Rows[0]["IO"].ToString();
            }
        }
        public List<string> GetWorkers(string liftId)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO from WorkerLifts wl " +
                    "join UserInfo ui on wl.UserId=ui.UserId " +
                    "where wl.LiftId=@liftId", conn);
                cmd.Parameters.AddWithValue("liftId", liftId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr["Family"].ToString() + " " + dr["IO"].ToString());
                dr.Close();
            }
            return data;
        }

        public List<UM> GetUM()
        {
            List<UM> data = new List<UM>();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, l.IdM from Lifts l " +
                    "where l.IdU+l.IdM!=N'12' and l.IdU+l.IdM!=N'17' and l.IdU+l.IdM!=N'31' and l.IdU+l.IdM!=N'42' " +
                    "group by l.IdU, l.IdM", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int n = GetTotalLifts(dr["IdU"].ToString(), dr["IdM"].ToString());
                    int s = GetStoppedLifts(dr["IdU"].ToString(), dr["IdM"].ToString()).Count;
                    data.Add(new UM()
                    {
                        IdU = dr["IdU"].ToString(),
                        IdM = dr["IdM"].ToString(),
                        IdUM = dr["IdU"].ToString() + "/" + dr["IdM"].ToString(),
                        Stopped = s.ToString(),
                        Working = (n - s).ToString(),
                        Url = "~/Stopped.aspx?U=" + HttpUtility.HtmlEncode(dr["IdU"].ToString()) + "&M=" +
                            HttpUtility.HtmlEncode(dr["IdM"].ToString())
                    });
                }
                dr.Close();
            }

            return data;
        }

        public List<StoppedLift> GetStoppedLifts(string idU, string idM)
        {
            List<StoppedLift> data = new List<StoppedLift>();
            
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select z.LiftId, z.Id from Zayavky z " +
                    "join Lifts l on z.LiftId=l.LiftId where l.IdU=@idU and l.IdM=@idM " + 
                    "and z.Finish is null and (z.Category=N'застревание' or z.Category=N'останов') order by z.LiftId", conn);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(new StoppedLift()
                    {
                        LiftId = dr["LiftId"].ToString(),
                        Url = "/ZayavkaClose.aspx?zId=" + dr["Id"].ToString()
                    });
                dr.Close();
            }

            return data;
        }

        public int GetTotalLifts(string idU, string idM)
        {
            int n = 0;

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(l.LiftId) from Lifts l " +
                    "where l.IdU=@idU and l.IdM=@idM", conn);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                n = (int)cmd.ExecuteScalar();
            }

            return n;
        }

        public int GetTotalDocUM(string name)  
        {
            int n = 0;

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(d.name) from DocUm d " +
                    "where d.name=@nm", conn);
                cmd.Parameters.AddWithValue("nm", name);
              
                n = (int)cmd.ExecuteScalar();
            }

            return n;
        }

        public List<StoppedLift> GetStoppedODSLifts(string userName)
        {
            List<StoppedLift> data = new List<StoppedLift>();
            
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select z.LiftId, z.Id from Zayavky z " +
                    "join ODSLifts ol on z.LiftId=ol.LiftId " +
                    "join Users u on ol.UserId=u.UserId " +
                    "where u.UserName=@userName and z.Finish is null and (z.Category=N'застревание' or z.Category=N'останов') order by z.LiftId", conn);
                cmd.Parameters.AddWithValue("userName", userName);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(new StoppedLift()
                    {
                        LiftId = dr["LiftId"].ToString(),
                        Url = "/ZayavkaCloseODS.aspx?zId=" + dr["Id"].ToString()
                    });
                dr.Close();
            }

            return data;
        }

        public List<string> GetODSLiftList(string userName)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ol.LiftId from ODSLifts ol " +
                    "join Users u on ol.UserId=u.UserId " +
                    "where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", userName);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr["LiftId"].ToString());
                dr.Close();
            }
            return data;
        }

        public int GetODSLifts(string userName)
        {
            int n = 0;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(ol.LiftId) from ODSLifts ol " +
                    "join Users u on ol.UserId=u.UserId " +
                    "where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", userName);
                n = (int)cmd.ExecuteScalar();
            }
            return n;
        }

        public List<Work> GetWorks(DateTime beg, DateTime end, string retUrl)
        {
            List<Work> data = new List<Work>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, p.LiftId, p.[Date], ui.Family, ui.IO, p.Id from [Plan] p " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join Users u on u.UserId=p.UserId " +
                    "join UserInfo ui on ui.UserId=p.UserId " +
                    "where p.[Date] between @d1 and @d2 " +
                    "order by ui.Family, ui.IO, p.[Date]", conn);
                cmd.Parameters.AddWithValue("d1", beg);
                cmd.Parameters.AddWithValue("d2", end);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(new Work()
                    {
                        IdU = dr["IdU"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Date = (DateTime)dr["Date"],
                        Worker = dr["Family"].ToString() + " " + dr["IO"].ToString(),
                        Url = "Reglament.aspx?PlanId=" + dr["Id"].ToString() +
                            (string.IsNullOrEmpty(retUrl) ? "" : "&ret=" + HttpUtility.HtmlEncode(retUrl))
                    });
                dr.Close();
            }
            return data;
        }

        public List<string> GetWZTypes()
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select [Type] from WZTypes", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr["Type"].ToString());
                dr.Close();
            }
            return data;
        }
        public List<string> GetWZAreas()
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Areas", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr[0].ToString());
                dr.Close();
            }
            return data;
        }
        public List<string> GetWZNodes(string area)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Nodes", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr[0].ToString());
                dr.Close();
            }
            return data;
        }
        public List<string> GetWZPozition(string area, string node)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Pozitions", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr[0].ToString());
                dr.Close();
            }
            return data;
        }

        public void GetIncidents(DateTime beg, DateTime end, ref List<LiftRep> data)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select z.LiftId, z.Start, z.Id, z.Category, z.Finish from Zayavky z " +
                    "where z.Start between @d1 and @d2 order by z.LiftId, z.Start", conn);
                cmd.Parameters.AddWithValue("d1", beg);
                cmd.Parameters.AddWithValue("d2", end);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new LiftRep()
                    {
                        Date = (DateTime)dr["Start"],
                        LiftId = dr["LiftId"].ToString(),
                        Title = dr["Category"].ToString().Remove(3),
                        Url = "ZayavkaView.aspx?zId=" + dr["Id"].ToString(),
                        Done = (dr["Finish"] is DBNull ? false : true)
                    });
                }
                dr.Close();
            }
        }

        public void GetPrim(DateTime beg, DateTime end, ref List<LiftRep> data)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId, rw.[Date], rw.Id, rw.Done from ReglamentWorks rw " +
                    "join [Plan] p on rw.PlanId=p.Id " +
                    "where rw.[Date] between @d1 and @d2 and Prim is not null order by p.LiftId, rw.[Date]", conn);
                cmd.Parameters.AddWithValue("d1", beg);
                cmd.Parameters.AddWithValue("d2", end);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new LiftRep()
                    {
                        Date = (DateTime)dr["Date"],
                        LiftId = dr["LiftId"].ToString(),
                        Title = "замечание",
                        Url = "Prim.aspx?rwId=" + dr["Id"].ToString(),
                        Done = bool.Parse(dr["Done"].ToString())
                    });
                }
                dr.Close();
            }
        }

        public void GetTP(DateTime beg, DateTime end, ref List<LiftRep> data, string retUrl)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId, p.[Date], p.Id, p.TpId, p.Done from [Plan] p " +
                    "where p.[Date] between @d1 and @d2 and p.TpId<>N'ОС' and p.TpId!=N'ВР' order by p.LiftId, p.[Date]", conn);
                cmd.Parameters.AddWithValue("d1", beg);
                cmd.Parameters.AddWithValue("d2", end);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new LiftRep()
                    {
                        Date = (DateTime)dr["Date"],
                        LiftId = dr["LiftId"].ToString(),
                        Title = dr["TpId"].ToString(),
                        Url = "Reglament.aspx?PlanId=" + dr["Id"].ToString() +
                            (string.IsNullOrEmpty(retUrl) ? "" : "&ret=" + HttpUtility.HtmlEncode(retUrl)),
                        Done = bool.Parse(dr["Done"].ToString())
                    });
                }
                dr.Close();
            }
        }

        public void GetOC(DateTime beg, DateTime end, ref List<LiftRep> data, string retUrl)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId, p.[Date], p.Id, p.TpId, p.Done from [Plan] p " +
                    "where p.[Date] between @d1 and @d2 and p.TpId=N'ОС' order by p.LiftId, p.[Date]", conn);
                cmd.Parameters.AddWithValue("d1", beg);
                cmd.Parameters.AddWithValue("d2", end);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new LiftRep()
                    {
                        Date = (DateTime)dr["Date"],
                        LiftId = dr["LiftId"].ToString(),
                        Title = dr["TpId"].ToString(),
                        Url = "Reglament.aspx?PlanId=" + dr["Id"].ToString() +
                            (string.IsNullOrEmpty(retUrl) ? "" : "&ret=" + HttpUtility.HtmlEncode(retUrl)),
                        Done = bool.Parse(dr["Done"].ToString())
                    });
                }
                dr.Close();
            }
        }
        public void GetBP(DateTime beg, DateTime end, ref List<LiftRep> data, string retUrl)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId, p.[Date], p.Id, p.TpId, p.Done from [Plan] p " +
                    "where p.[Date] between @d1 and @d2 and p.TpId=N'ВР' order by p.LiftId, p.[Date]", conn);
                cmd.Parameters.AddWithValue("d1", beg);
                cmd.Parameters.AddWithValue("d2", end);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new LiftRep()
                    {
                        Date = (DateTime)dr["Date"],
                        LiftId = dr["LiftId"].ToString(),
                        Title = dr["TpId"].ToString(),
                        Url = "Reglament.aspx?PlanId=" + dr["Id"].ToString() +
                            (string.IsNullOrEmpty(retUrl) ? "" : "&ret=" + HttpUtility.HtmlEncode(retUrl)),
                        Done = bool.Parse(dr["Done"].ToString())
                    });
                }
                dr.Close();
            }
        }

        public DataTable GetWorkTime()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.* from UserInfo ui " +
                    "join UsersInRoles uir on uir.UserId=ui.UserId " +
                    "join Roles r on r.RoleId= uir.RoleId " +
                    "where r.RoleName='worker' order by ui.Family, ui.[IO]", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }

        public DataTable GetWorks(string userId)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId, p.TpId, p.[Date], p.Done, t.Ttx from [Plan] p " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 " +
                    "where p.UserId=@userId and p.TpId<>N'ОС' order by p.LiftId, p.[Date]", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }

        public string GetTp(int planId)
        {
            string tp = string.Empty;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select TpId from [Plan] where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", planId);
                tp = (string)cmd.ExecuteScalar();
            }
            return tp;
        }

        public List<string> GetIdL(string userId, string idU, string idM)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdL from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where wl.UserId=@userId and l.IdU=@idU and l.IdM=@idM group by l.IdL", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdM(string userId, string idU)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdM from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where wl.UserId=@userId and l.IdU=@idU group by l.IdM", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("idU", idU);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdU()
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU from Lifts l " +
                    "group by l.IdU order by l.IdU", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdU(string userId)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId where wl.UserId=@userId group by l.IdU order by l.IdU", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }
        // лифты закрепленные за механиком по userName
        public List<string> GetLiftId(string userName)
        {
            List<string> data = new List<string>();
            data.Add("");
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.LiftId from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@userName group by l.LiftId", conn);
                cmd.Parameters.AddWithValue("userName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }

        public List<CheckedLift> GetCheckedLiftId(string userId, string idU, string idM)
        {
            List<CheckedLift> data = new List<CheckedLift>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.LiftId, u.UserId  from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where l.IdU=@idU and l.IdM=@idM group by l.LiftId, u.UserId", conn);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CheckedLift cl = new CheckedLift()
                    {
                        Title = reader["LiftId"].ToString(),
                        Checked = (reader["UserId"].ToString() == userId ? true : false)
                    };
                    int i = data.FindIndex(delegate(CheckedLift fcl)
                    {
                        return fcl.Title == cl.Title;
                    });
                    if (i < 0)
                        data.Add(cl);
                    else if (cl.Checked)
                        data[i].Checked = true;
                }
                reader.Close();
            }
            return data;
        }

        public List<string> GetLiftId(string userId, string idU, string idM)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.LiftId from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where wl.UserId=@userId and l.IdU=@idU and l.IdM=@idM group by l.LiftId", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetLiftId(string idU, string idM)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.LiftId from Lifts l " +
                    "where l.IdU=@idU and l.IdM=@idM group by l.LiftId", conn);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdM(string idU)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdM from Lifts l " +
                    "where l.IdU=@idU group by l.IdM", conn);
                cmd.Parameters.AddWithValue("idU", idU);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString());
                reader.Close();
            }
            return data;
        }
        // лифты закрепленные за механиком по userId
        public List<UserLift> GetUserLift(string userId)
        {
            List<UserLift> data = new List<UserLift>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select wl.LiftId, t.Ttx from WorkerLifts wl " +
                    "join LiftsTtx lt on lt.LiftId=wl.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where wl.UserId=@userId", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(new UserLift()
                    {
                        LiftId = reader["LiftId"].ToString(),
                        Checked = true,
                        Address = reader["Ttx"].ToString()
                    });
            }
            return data;
        }

        public List<UserLift> GetUserLift(string userName, string idU, string idM)
        {
            List<UserLift> data = new List<UserLift>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.LiftId, t.Ttx, u.UserName from Lifts l " +
                    "join LiftsTtx lt on lt.LiftId=l.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "left join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "left join Users u on u.UserId=wl.UserId and u.UserName=@userName " +
                    "where l.IdU=@idU and l.IdM=@idM group by l.LiftId, u.UserName, t.Ttx", conn);
                cmd.Parameters.AddWithValue("userName", userName);
                cmd.Parameters.AddWithValue("idU", idU);
                cmd.Parameters.AddWithValue("idM", idM);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int n = data.FindIndex(delegate(UserLift i)
                    {
                        return i.LiftId == reader[0].ToString();
                    });
                    if (n < 0)
                        data.Add(new UserLift()
                        {
                            LiftId = reader[0].ToString(),
                            Checked = (reader[2] is DBNull ? false : true),
                            Address = reader[1].ToString()
                        });
                    else if (data[n].Checked == false && !(reader[2] is DBNull))
                        data[n].Checked = true;
                }
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdUMbyName(string userName)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, l.IdM from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "join Users u on wl.UserId=u.UserId " +
                    "where u.UserName=@userName group by l.IdU, l.IdM", conn);
                cmd.Parameters.AddWithValue("userName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString() + "/" + reader[1].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdUM(string userId)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, l.IdM from Lifts l " +
                    "join WorkerLifts wl on wl.LiftId=l.LiftId " +
                    "where wl.UserId=@userId group by l.IdU, l.IdM", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString() + "/" + reader[1].ToString());
                reader.Close();
            }
            return data;
        }

        public List<string> GetIdUM()
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, l.IdM from Lifts l " +
                    "group by l.IdU, l.IdM", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader[0].ToString() + "/" + reader[1].ToString());
                reader.Close();
            }
            return data;
        }

        public class HoolidaysType
        {
            public string Title { get; set; }
            public int Id { get; set; }
            public override string ToString()
            {
                return Title;
            }
        }

        public List<HoolidaysType> GetWorkTypes()
        {
            List<HoolidaysType> data = new List<HoolidaysType>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Title, Id from HollidaysType", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (!(reader[0] is DBNull))
                        data.Add(new HoolidaysType()
                        {
                            Title = reader["Title"].ToString(),
                            Id = (int)reader["Id"]
                        });
                reader.Close();
            }
            return data;
        }

        public List<PersonData> GetWorkers()
        {
            List<PersonData> data = new List<PersonData>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.[IO], ui.UserId, u.UserName from UserInfo ui " +
                    "join UsersInRoles ur on ui.UserId=ur.UserId " +
                    "join Users u on ui.UserId=u.UserId " +
                    "join Roles r on ur.RoleId=r.RoleId " +
                    "where r.RoleName='Worker' " +
                    "group by ui.Family, ui.[IO], ui.UserId, u.UserName order by ui.Family, ui.[IO]", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (!(reader[0] is DBNull))
                        data.Add(new PersonData()
                        {
                            Title = reader[0].ToString() + " " + reader[1].ToString(),
                            Id = reader[2].ToString(),
                            UserName = reader[3].ToString()
                        });
                reader.Close();
            }
            return data;
        }

        public List<PersonData> GetWZPersons()
        {
            List<PersonData> data = new List<PersonData>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.[IO], ui.UserId from UserInfo ui " +
                    "join WorkerZayavky wz on ui.UserId=wz.UserId " +
                    "group by ui.Family, ui.[IO], ui.UserId", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (!(reader[0] is DBNull))
                        data.Add(new PersonData()
                        {
                            Title = reader[0].ToString() + " " + reader[1].ToString(),
                            Id = reader[2].ToString()
                        });
                reader.Close();
            }
            return data;
        }
        // Внеплановые ремонты
        public List<Zayavka> GetZayavkyVR(string userName)
        {
            List<Zayavka> list = new List<Zayavka>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select z.Id, z.LiftId, z.Text, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                    "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and z.Category=N'внеплановые ремонты' and z.[Finish] is null " +
                    "union select z2.Id, z2.LiftId, z2.Text, z2.Category, z2.Worker, z2.Start, z2.Status from Zayavky z2 " +
                    "join Users u2 on u2.UserId=z2.Worker where u2.UserName=@UserName and z2.Category=N'внеплановые ремонты' and z2.[Finish] is null order by Start",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string url = "~/";
                    if (reader[4] is DBNull)
                        url += "ZayavkaEdit";
                    else
                        url += "ZayavkaClose";
                    url += ".aspx?zId=" + reader[0].ToString();
                    Zayavka z = new Zayavka()
                    {
                        Title = "Лифт №_" + reader[1].ToString() + " - " + reader[5].ToString() + " - " + reader[2].ToString() +
                            (bool.Parse(reader[6].ToString()) ? "." : " "), //принято/не принято - не работает!
                        Url = url
                    };
                    list.Add(z);
                }
                reader.Close();
            }
            return list;
        }
        // заявки электронщику
        public List<Zayavka> GetZayavkyREO(string userName) 
        {
            List<Zayavka> list = new List<Zayavka>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select z.Id, z.LiftId, z.Text, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                    "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and z.Category=N'ПНР/РЭО' and z.[Finish] is null " +
                    "union select z2.Id, z2.LiftId, z2.Text, z2.Category, z2.Worker, z2.Start, z2.Status from Zayavky z2 " +
                    "join Users u2 on u2.UserId=z2.Worker where u2.UserName=@UserName and z2.Category=N'ПНР/РЭО' and z2.[Finish] is null order by Start",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string url = "~/";
                    if (reader[4] is DBNull)
                        url += "ZayavkaEdit";
                    else
                        url += "ZayavkaClose";
                    url += ".aspx?zId=" + reader[0].ToString();
                    Zayavka z = new Zayavka()
                    {
                        Title = reader[1].ToString() + " " + " | " + reader[5].ToString() + " | " + reader[2].ToString() +
                            (bool.Parse(reader[6].ToString()) ? "." : " "), //принято/не принято - не работает!
                        Url = url
                    };
                    list.Add(z);
                }
                reader.Close();
            }
            return list;
        }
        // Все заявки

        public List<Zayavka> GetZayavky(string userName)
        {
            List<Zayavka> list = new List<Zayavka>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select z.Id, z.LiftId, z.[Text], z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                    "join WorkerLifts wl on wl.LiftId=z.LiftId " +                
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and z.Category!=N'ПНР/РЭО' and z.[Finish] is null " +
                    "union select z2.Id, z2.LiftId, z2.Text, z2.Category, z2.Worker, z2.Start, z2.Status from Zayavky z2 " +
                    "join Users u2 on u2.UserId=z2.Worker where u2.UserName=@UserName and z2.Category!=N'ПНР/РЭО' and z2.[Finish] is null order by Start",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string url = "~/";
                    if (reader[4] is DBNull) 
                        url += "ZayavkaEdit";
                    else
                        url += "ZayavkaClose";
                    url += ".aspx?zId=" + reader[0].ToString();
                    Zayavka z = new Zayavka()
                    {
                        Title = reader[1].ToString() + " " + " | " + reader[5].ToString() + " | " + reader[2].ToString() +
                            (bool.Parse(reader[6].ToString()) ? "." : " "), //принято/не принято - не работает!
                        Url = url
                    };
                    list.Add(z);
                }
                reader.Close();
            }
            return list;
        }

        public List<Zayavka> GetManagerZayavky(string userName)
        {
            List<Zayavka> list = new List<Zayavka>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select z.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                    "where z.[Finish] is null order by Start",
                    conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string url = "~/ZayavkaEdit.aspx?zId=" + reader[0].ToString();
                    Zayavka z = new Zayavka()
                    {
                        Title = reader[1].ToString() + " №" + reader["Id"].ToString() +
                            (bool.Parse(reader["Status"].ToString()) ? " принято" : " не принято"),
                        Url = url
                    };
                    list.Add(z);
                }
                reader.Close();
            }
            return list;
        }

        public UserInfo GetUserInfo(string userName)
        {
            UserInfo ui = new UserInfo() { HourBeg = 8, HourEnd = 17, Lunch = 13 };


            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select i.Family, i.IO, i.HourBeg, i.HourEnd, i.Lunch from UserInfo i " +
                    "join Users u on u.UserId=i.UserId " +
                    "where u.UserName=@UserName",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if (data.Rows.Count > 0)
                {
                    ui.Family = data.Rows[0]["Family"].ToString();
                    ui.IO = data.Rows[0]["IO"].ToString();
                    ui.HourBeg = (int)data.Rows[0]["HourBeg"];
                    ui.HourEnd = (int)data.Rows[0]["HourEnd"];
                    ui.Lunch = (int)data.Rows[0]["Lunch"];
                }
            }

            return ui;
        }

        public DataTable GetWorkerZayavka(int id)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select wz.[Text], wz.[Date], wz.[Type], wz.Readed, wz.LiftId, wz.[Foto], wz.nameFoto, wz.Name, wz.NumID, ui.Family, ui.[IO], wz.Done, wz.DoneDate, ui2.Family as WhoFamily, ui2.[IO] as WhoIO from WorkerZayavky wz " +
                    "join UserInfo ui on ui.UserId=wz.UserId " +
                    "left join UserInfo ui2 on ui2.UserId=wz.WhoDone " +
                    "where wz.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        //просмотр документов
        public DataTable GetDoc(int id) 
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.Name, d.NumEvent, d.Image, d.NameFile, d.Status, d.Prim from Documents d " +
                 "where d.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }

        public DataTable GetDocUm(int id)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.name, d.numdoc, d.img, d.namefile, d.status, d.primm, d.usr, d.Idu, d.IdM from DocUM d " +
                 "where d.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }

        public DataTable GetPart(int id)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.NumEvent, p.Foto, p.namefoto, p.Name, p.NumID, p.Kol, p.Obz from PartsList p " +
                 "where p.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        //извлечение пин-кода
        public DataTable GetPin(string pin, string user) 
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.surname, p.name, p.midlename, p.education from People p " +
                        "where p.comments=@user and p.specialty=N'админ' and p.education=@pin", conn);
                cmd.Parameters.AddWithValue("user", user);
                cmd.Parameters.AddWithValue("pin", pin);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        //событие
        public DataTable GetEvent(int id)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
               SqlCommand cmd = new SqlCommand("select ev.[EventId], ev.[RegistrId], ev.[DataId], ev.[ZayavId], ev.[WZayavId], ev.[Sourse], ev.[Family], ev.[IO], ev.[TypeId], ev.[IdU], ev.[IdM], ev.[LiftId], " +
                "ev.Akt, ev.ZaprosMng, ev.ZaprosKP, ev.KP, ev.ZapBill, ev.Bill, ev.Payment, ev.Dostavka, ev.Prihod, ev.Peremeshenie, ev.AktVR, ev.Spisanie, ev.Cansel, " +
                "ev.[DateCansel], ev.[Foto], ev.[namefoto], ev.[Name], ev.[NumID], ev.[Kol], ev.[Who], ev.[ToApp], ev.[DateWho], ev.[DateToApp], ev.[Comment], ev.[Timing], ev.[Address], ev.[Prim], ev.[Obz] from Events ev " +
                "where ev.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        //дополнительные запчасти
        public DataTable GetPartsList(int id)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select pl.[NumEvent], pl.[Foto], pl.[namefoto], pl.[Name], pl.[NumID], pl.[Kol], pl.[Obz] from PartsList pl " +
                 "where pl.NumEvent=@id ", conn);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        public DataTable GetSklad(int wz)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select s.Id, s.DataPost, s.NumDoc, NumSklada, s.Zakreplen, s.Name, s.NumID, s.TheNum, s.Price, s.Source, s.DataSpisaniya, s.NumDocSpisan, s.TheNumSpisan, s.Prim, s.Ostatok, s.Obz, s.Prinyal from Sklady s " +
                                                "where s.Id=@num", conn);
                cmd.Parameters.AddWithValue("num", wz);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        public DataTable GetSklady()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select s.DataPost, s.NumDoc, NumSklada, s.Zakreplen, s.Name, s.NumID, s.TheNum, s.Price, s.Source, s.DataSpisaniya, s.NumDocSpisan, s.TheNumSpisan, s.Prim, s.Ostatok, s.Obz, s.Prinyal from Sklady s ", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }
        public DataTable GetWorkerZayavky()
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select wz.Id, wz.[Date], ui.Family, ui.[IO] from WorkerZayavky wz " +
                    "join UserInfo ui on ui.UserId=wz.UserId where wz.[Done]=0 ", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }
            return data;
        }

        public List<Holliday> GetHollidays(string userName, DateTime dateBeg, DateTime dateEnd)
        {
            List<Holliday> data = new List<Holliday>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select h.[Date], h.HollidaysTypeId from Holidays h " +
                    "join Users u on u.UserId=h.UserId " +
                    "where u.UserName=@UserName and (h.[Date] between @d1 and @d2) " +
                    "order by h.[Date]", conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(new Holliday() {
                        Date = (DateTime)dr[0],
                        Id = (int)dr[1]
                    });
                dr.Close();
            }
            return data;
        }

        public List<string> GetAddress(string IdU, string IdM)
        {
            List<string> data = new List<string>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select t.Ttx from Lifts l join LiftsTtx lt on lt.LiftId=l.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where l.IdU=@u and l.IdM=@m group by t.Ttx", conn);
                cmd.Parameters.AddWithValue("u", IdU);
                cmd.Parameters.AddWithValue("m", IdM);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    data.Add(dr[0].ToString());
                dr.Close();
            }
            return data;
        }

        public class ZPrim
        {
            public string Url { set; get; }
        }

        public List<ZPrim> GetNotDonePrim(string liftId, string retUrl)
        {
            List<ZPrim> data = new List<ZPrim>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select rw.Id from ReglamentWorks rw " +
                    "join [Plan] p on rw.PlanId=p.Id " +
                    "where p.LiftId=@liftId and rw.Done=0 and rw.Prim is not null", conn);
                cmd.Parameters.AddWithValue("liftId", liftId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new ZPrim()
                    {
                        Url = "~/Prim.aspx?rwId=" + dr["Id"].ToString()
                    });
                }
                dr.Close();

                cmd = new SqlCommand("select zp.Id from ZPrim zp " +
                    "where zp.LiftId=@liftId and zp.Done=0", conn);
                cmd.Parameters.AddWithValue("liftId", liftId);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new ZPrim()
                    {
                        Url = "~/ZPrimEdit.aspx?Id=" + dr["Id"].ToString() +
                            (string.IsNullOrEmpty(retUrl) ? "" : "&ReturnUrl=" + HttpUtility.HtmlEncode(retUrl))
                    });
                }
                dr.Close();
            }
            return data;
        }

        public class PrimView
        {
            public string Index { get; set; }
            public string LiftId { get; set; }
            public string Url { get; set; }
            public string Date { get; set; }
        }

        public List<PrimView> GetPrimView(string lift, string retUrl)
        {
            List<PrimView> data = new List<PrimView>();
            string[] id = lift.Split('/');
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select rw.Id, rw.[Index], p.LiftId, p.[Date]  from ReglamentWorks rw " +
                    "join [Plan] p on p.Id=rw.PlanId " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "where rw.Done=0 and rw.Prim is not null and l.IdU=@idU and l.IdM=@idM order by p.LiftId, p.[Date]", conn);
                cmd.Parameters.AddWithValue("idU", id[0]);
                cmd.Parameters.AddWithValue("idM", id[1]);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(new PrimView()
                    {
                        Index = reader["Index"].ToString(),
                        LiftId = reader["LiftId"].ToString(),
                        Date = ((DateTime)reader["Date"]).ToShortDateString(),
                        Url = "~/Prim.aspx?rwId=" + reader["Id"].ToString()
                    });
                reader.Close();

                cmd = new SqlCommand("select z.Id, z.Category, z.LiftId, z.[Date] from ZPrim z " +
                    "join Lifts l on l.LiftId=z.LiftId " +
                    "where z.Done=0 and l.IdU=@idU and l.IdM=@idM order by z.LiftId, z.[Date]", conn);
                cmd.Parameters.AddWithValue("idU", id[0]);
                cmd.Parameters.AddWithValue("idM", id[1]);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(new PrimView()
                    {
                        Index = reader["Category"].ToString(),
                        LiftId = reader["LiftId"].ToString(),
                        Date = ((DateTime)reader["Date"]).ToShortDateString(),
                        Url = "~/ZPrimEdit.aspx?Id=" + reader["Id"].ToString() +
                            (string.IsNullOrEmpty(retUrl) ? "" : "&ReturnUrl=" + retUrl)
                    });
                reader.Close();

                data.Sort(delegate(PrimView pv1, PrimView pv2)
                {
                    int i = string.Compare(pv1.LiftId, pv2.LiftId);
                    if (i == 0) i = string.Compare(pv1.Index, pv2.Index);
                    if (i == 0) return DateTime.Compare(DateTime.Parse(pv1.Date), DateTime.Parse(pv2.Date));
                    return i;
                });
            }
            return data;
        }

        public class LiftPrim
        {
            public string IdUM { get; set; }
            public string N { get; set; }
            public string Url { get; set; }
        }

        public List<LiftPrim> GetLiftPrim()
        {
            List<LiftPrim> data = new List<LiftPrim>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, l.IdM, COUNT(rw.Id) as N from ReglamentWorks rw " +
                    "join [Plan] p on p.Id=rw.PlanId " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "where rw.Done=0 and rw.Prim is not null " +
                    "group by l.IdU, l.IdM order by l.IdU, l.IdM", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(new LiftPrim()
                    {
                        IdUM = reader["IdU"].ToString() + "/" + reader["IdM"].ToString(),
                        N = reader["N"].ToString(),
                        Url = "~/LiftPrim.aspx?lift=" + HttpUtility.HtmlEncode(reader["IdU"].ToString() + "/" + reader["IdM"].ToString())
                    });
                reader.Close();
            }
            return data;
        }

        public List<LiftPrim> GetLiftZPrim()
        {
            List<LiftPrim> data = new List<LiftPrim>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select l.IdU, l.IdM, COUNT(z.Id) as N from ZPrim z " +
                    "join Lifts l on l.LiftId=z.LiftId " +
                    "where z.Done=0 " +
                    "group by l.IdU, l.IdM order by l.IdU, l.IdM", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(new LiftPrim()
                    {
                        IdUM = reader["IdU"].ToString() + "/" + reader["IdM"].ToString(),
                        N = reader["N"].ToString(),
                        Url = "~/LiftPrim.aspx?lift=" + HttpUtility.HtmlEncode(reader["IdU"].ToString() + "/" + reader["IdM"].ToString())
                    });
                reader.Close();
            }
            return data;
        }

        public DataTable GetNotDonePrim(string userName, DateTime dateBeg, DateTime dateEnd)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select z.Id, z.Category, z.LiftId, z.Responce from ZPrim z " +
                    "join Users u on u.UserId=z.[To] " +
                    "where u.UserName=@userName and z.Done=0 and z.LiftId in (select p2.LiftId from [Plan] p2 " +
                    "join Users u2 on u2.UserId=p2.UserId " +
                    "where u2.UserName=@userName and (p2.[Date] between @d1 and @d2)) " /* or not exists " +
                    "(select p2.LiftId from [Plan] p2 " +
                    "join Users u2 on u2.UserId=p2.UserId " +
                    "where u2.UserName=@userName and (p2.[Date] between @d1 and @d2)) "*/, conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }

            return data;
        }

        public DataTable GetUnplan(string userName, DateTime dateBeg, DateTime dateEnd)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd=new SqlCommand("select rw.Id, rw.PlanId, rw.[Index], p.LiftId, t.Ttx, rw.Prim from ReglamentWorks rw " +
                    "join [Plan] p on p.Id=rw.PlanId " +
                    "join Users u on u.UserId=p.UserId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where rw.Done=0 and rw.Prim is not null and rw.[Index]=1 and u.UserName=@userName or (rw.[Index]=2 and rw.Done=0 and rw.Prim is not null and (p.LiftId in " +
                    "(select p2.LiftId from [Plan] p2 " +
                    "join Users u2 on u2.UserId=p2.UserId " +
                    "where u2.UserName=@userName and (p2.[Date] between @d1 and @d2)) or not exists " +
                    "(select p2.LiftId from [Plan] p2 " +
                    "join Users u2 on u2.UserId=p2.UserId " +
                    "where u2.UserName=@userName and (p2.[Date] between @d1 and @d2)))) " +
                    "order by rw.[Index]", conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }

            return data;
        }

        public bool IsPlanned(string userName, string liftId, string tpId, DateTime dateBeg, DateTime dateEnd)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select p.[Date] from [Plan] p " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join WorkerLifts wl on wl.LiftId=p.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and p.[Date] between @d1 and @d2 " +
                    "and l.LiftId=@liftId and p.TpId=@tpId", conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                cmd.Parameters.AddWithValue("liftId", liftId);
                cmd.Parameters.AddWithValue("tpId", tpId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                da.Fill(data);
                if (data.Rows.Count > 0)
                    return true;
            }
            return false;
        }

        public bool IsHolliday(string userName, DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select h.[Date] from Holidays h " +
                    "join Users u on u.UserId=h.UserId " +
                    "where u.UserName=@UserName and h.[Date] between @d1 and @d2", conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                DateTime d1 = new DateTime(date.Year, date.Month, date.Day);
                cmd.Parameters.AddWithValue("d1", d1);
                cmd.Parameters.AddWithValue("d2", d1.AddDays(1).AddSeconds(-1));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                da.Fill(data);
                if (data.Rows.Count > 0)
                    return true;
            }
            return false;
        }

        public DataTable GetTpPlan(string userName, string IdUM, DateTime dateBeg, DateTime dateEnd)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select t.Ttx, tp.LiftId, l.IdL, tp.TpId, tp.[Date] from TpPlan tp " +
                    "join Lifts l on l.LiftId=tp.LiftId " +
                    "join LiftsTtx lt on lt.LiftId=tp.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "join WorkerLifts wl on wl.LiftId=tp.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and tp.[Date] between @d1 and @d2 and l.IdU=@U and l.IdM=@M",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                string[] s = IdUM.Split('/');
                if (s.Length > 1)
                {
                    cmd.Parameters.AddWithValue("U", s[0]);
                    cmd.Parameters.AddWithValue("M", s[1]);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);
                }
            }

            return data;
        }

        public DataTable GetPlan(string userName, DateTime dateBeg, DateTime dateEnd)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select p.TpId, p.[Date], p.DateEnd, p.LiftId, t.Ttx, p.Id as PlanId, p.Done, p.Prn from [Plan] p " +
                    "join Users u on u.UserId=p.UserId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where u.UserName=@UserName and (p.[Date] between @d1 and @d2) " +
                    "order by p.[Date], p.DateEnd",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }

            return data;
        }
        public DataTable GetPlanUM(string userName, string IdUM, DateTime dateBeg, DateTime dateEnd)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select p.TpId, p.[Date], p.DateEnd, p.LiftId, t.Ttx, p.Id as PlanId, p.Done, p.Prn from [Plan] p " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join Users u on u.UserId=p.UserId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where u.UserName=@UserName and (p.[Date] between @d1 and @d2) " + 
                    "and p.TpId<>N'ВР' and p.TpId<>N'ОС' and l.IdU=@U and l.IdM=@M " +
                    "order by p.LiftId",// p.[Date], p.DateEnd",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                string[] s = IdUM.Split('/');
                if (s.Length > 1)
                {
                    cmd.Parameters.AddWithValue("U", s[0]);
                    cmd.Parameters.AddWithValue("M", s[1]);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);
                }

                return data;
            }
        }
        public DataTable GetPlanUM13(string userName, string IdUM, DateTime dateBeg, DateTime dateEnd) 
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select p.TpId, p.[Date], p.DateEnd, p.LiftId, t.Ttx, p.Id as PlanId, p.Done, p.Prn from [Plan] p " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join Users u on u.UserId=p.UserId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where u.UserName=@UserName and (p.[Date] between @d1 and @d2) " +
                    "and p.TpId<>N'ВР' and p.TpId<>N'ОС' and l.IdU=@U and l.IdM=@M " +
                    "order by t.Ttx",// p.[Date], p.DateEnd",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("d1", dateBeg);
                cmd.Parameters.AddWithValue("d2", dateEnd);
                string[] s = IdUM.Split('/');
                if (s.Length > 1)
                {
                    cmd.Parameters.AddWithValue("U", s[0]);
                    cmd.Parameters.AddWithValue("M", s[1]);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);
                }

                return data;
            }
        }
        public List<int> GetPlans(string userName, int planId)
        {
            List<int> plans = new List<int>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select p.Id from [Plan] p " +
                    "where p.UserId=(select UserId from Users where UserName=@UserName) and " +
                    "p.[Date]=(select p2.[Date] from [Plan] p2 where p2.Id=@PlanId)", conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("PlanId", planId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    plans.Add(Int32.Parse(reader[0].ToString()));
                reader.Close();
            }
            return plans;
        }

        public DataTable GetPrim(string userName, int planId)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "select rw.Prim, rw.ReglamentId, rw.[Date], rw.PlanId, r.N, r.Title from ReglamentWorks rw " +
                    "join Reglament r on r.Id=rw.ReglamentId " +
                    "where rw.PlanId " +
                 /* "in (select p.Id from [Plan] p " +
                    "where p.UserId=(select UserId from Users where UserName=@UserName) and " +
                    "p.[Date]=(select p2.[Date] from [Plan] p2 where p2.Id=@PlanId) )" + */
                    "=@PlanId" +
	                " and rw.Prim is not null order by r.N",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                cmd.Parameters.AddWithValue("PlanId", planId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }

            return data;
        }

        public DataTable GetReglamentWorks(int planId)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select r.Title, r.N, w.Done, w.Prim, w.Id as WorksId, r.Id as ReglamentId from [Plan] p " +
                    "left join Reglament r on r.TpId=p.TpId and (r.LiftId is null or r.LiftId=p.LiftId) " +
                    "left join ReglamentWorks w on w.PlanId=p.Id and w.ReglamentId=r.Id " +
                    "where p.Id=@planId order by r.N ", conn);
                cmd.Parameters.AddWithValue("planId", planId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
            }

            return data;
        }

        public string GetLiftId(int planId)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId from [Plan] p where p.Id=@planId", conn);
                cmd.Parameters.AddWithValue("planId", planId);
                object o = cmd.ExecuteScalar();
                if (o == null) return string.Empty;
                else return o.ToString();
            }
        }

        public List<string> GetLiftsId(int planId)
        {
            List<string> data = new List<string>();

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.LiftId from [Plan] p " +
                    "where p.[Date]=(select [Date] from [Plan] p where p.Id=@planId) and " +
	                "p.UserId=(select UserId from [Plan] p where p.Id=@planId)", conn);
                cmd.Parameters.AddWithValue("planId", planId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    data.Add(reader["LiftId"].ToString());
                reader.Close();
            }

            return data;
        }

        public string GetAddress(int planId)
        {
            string a = string.Empty;

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select t.Ttx from Ttx t " +
                    "join LiftsTtx lt on lt.TtxId=t.Id " +
                    "join [Plan] p on p.LiftId=lt.LiftId " +
                    "where p.Id=@pId and t.TtxTitleId=1", conn);
                cmd.Parameters.AddWithValue("pId", planId);
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                if (data.Rows.Count > 0)
                    a = data.Rows[0]["Ttx"].ToString();
            }

            return a;
        }

        public string GetReglamentTitle(int reglamentId)
        {
            string a = string.Empty;

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Title from Reglament r " +
                    "where r.Id=@rId", conn);
                cmd.Parameters.AddWithValue("rId", reglamentId);
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                if (data.Rows.Count > 0)
                    a = data.Rows[0]["Title"].ToString();
            }

            return a;
        }

        public bool CheckAccount(string userName, List<string> mustRoles)
        {
            List<string> roles = new List<string>();
            
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT RoleName FROM Roles r LEFT JOIN UsersInRoles uir ON uir.RoleId = r.RoleId LEFT JOIN Users u ON uir.UserId=u.UserId WHERE (u.UserName = @UserName)",
                    conn);
                cmd.Parameters.AddWithValue("UserName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string r = reader["RoleName"].ToString();
                    roles.Add(r);
                }
                reader.Close();
            }

            foreach (string r in roles)
                if (mustRoles.Find(delegate(string role) { return (role == r); })==r) return true;
            return false;
        }
    }
}