using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDbfReader;
using Update_Crew_Info.Model;
using Domino;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.WebServices.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Update_Crew_Info
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo input;
            bool flaglocal = false;

            //Mỗi lần update ERMS model luu ý sửa chô KTKL


            //Get IP
            //GetFPTBk();
            //Backup_doc();
            //for(int i = 0; i < 2; i++)
            //{
            //    UpdateATB();
            //}


            //update_RouteComm();
            //update_THHDLD();
            //Update_Sys_Acc();
            //Update_Giobay();
            //Update_SMS_Acc();
            //UpdateSafetyCert();
            //Update_KTKL();
            //Update_Nhanthan();
            //Update_Thannhan();
            //Update_Dang();
            //Update_Group();
            //UpdateClass();
            //Contract2RedAnt();


            //
            //SendMailRemoveAccount();
            //SendMailUnlockAccount();
            //SendMailLockAccount();

            //SendUpdateMailGroup();

            //return;






            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            string filelog = @"c:\temp\Update_App_log New.txt";
            StreamWriter mainlog = new StreamWriter(filelog, true);
            mainlog.WriteLine("----------------------------------------------------------------------------------------");
            mainlog.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " App Start!");
            try
            {


                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                foreach (var entry in addr)
                {
                    if (entry.ToString().Contains("10.105.15.45"))
                    {
                        Console.WriteLine(entry.ToString());
                        flaglocal = true;
                        break;
                    }

                }
#if (DEBUG)
                Console.WriteLine("Debug mode");
                do
                {
                    // For Interval in Hours 
                    // This Scheduler will start at 9:44 and call after every 1 Hour
                    // IntervalInSeconds(start_hour, start_minute, hours)


                    Scheduler.IntervalInHours(7, 00, 3,true,
                    () =>
                        Update_Sys_Acc()
                    );

                    Scheduler.IntervalInHours(7, 15, 3, true,
                    () =>
                        Update_Giobay()
                    );

                    Scheduler.IntervalInHours(7, 30, 3, true,
                    () =>
                        Update_SMS_Acc()
                    );

                    Scheduler.IntervalInHours(14, 30, 6, true,
                    () =>
                      UpdateSafetyCert()
                    );

                   
                    Scheduler.IntervalInHours(7, 55, 3, true,
                   () =>
                       //Update_Learning()
                       UpdateClass()
                   );

                    Scheduler.IntervalInHours(10, 0, 6, true,
                    () =>
                     //HDLD2RedAnt
                     Contract2RedAnt()
                    );

                    Scheduler.IntervalInHours(8, 00, 3, true,
                    () =>
                        Update_KTKL()
                    );

                    Scheduler.IntervalInHours(8, 15, 3, true,
                    () =>
                        Update_Nhanthan()
                    );

                    Scheduler.IntervalInHours(8, 30, 3, true,
                    () =>
                        Update_Thannhan()
                    );

                    Scheduler.IntervalInHours(8, 45, 3, true,
                    () =>
                        Update_Dang()
                    );


                    //Chạy trên máy Cá nhân
                    Scheduler.IntervalInHours(22, 00, 24,false,
                    () =>
                        Backup_doc()
                    );
                    //Update Group
                    Scheduler.IntervalInHours(1, 00, 1, true,
                    () =>
                        Update_Group()
                    );

                    //update_THHDLD()
                    Scheduler.IntervalInHours(09, 00, 24, true,
                    () => update_THHDLD());


                    Scheduler.IntervalInHours(21, 00, 24, false,
                    () => update_RouteComm());

                    //SyncFPT
                    Scheduler.IntervalInHours(05, 00, 24, false,
                    () => SyncFPT());

                    //Update Group mail
                    Scheduler.IntervalInHours(15, 30, 24, false,
                    () =>
                      UpdateMailGroup()
                    );

                    //Send Delete Mail
                    Scheduler.IntervalInHours(16, 45, 24, false,
                    () => SendMailRemoveAccount());

                    //Send Mail Unlock
                    Scheduler.IntervalInHours(16, 40, 24, false,
                    () => SendMailUnlockAccount());

                    //Send Mail Lock
                    Scheduler.IntervalInHours(16, 50, 24, false,
                    () => SendMailLockAccount());

                    //Set Active Bộ phận
                    Scheduler.IntervalInHours(0, 30, 24,true,
                    () => SetActiveDep());

                    input = Console.ReadKey();
                } while (input.Key != ConsoleKey.Escape);
#else
            Console.WriteLine("Relese mode");
            do
            {
                // For Interval in Hours 
                // This Scheduler will start at 9:44 and call after every 1 Hour
                // IntervalInSeconds(start_hour, start_minute, hours)

                //Chạy trên máy 251
                if (!flaglocal)
                {
                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_Sys_Acc()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_Giobay()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_SMS_Acc()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_Learning()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_KTKL()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_Nhanthan()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_Thannhan()
                    );

                    Scheduler.IntervalInHours(19, 00, 3,
                    () =>
                        Update_Dang()
                    );

                }
                else
                {
                    //Chạy trên máy Cá nhân
                    Scheduler.IntervalInHours(22, 00, 24,
                    () =>
                        Backup_doc()
                    );

                    //Scheduler.IntervalInHours(23, 00, 12,
                    //() =>
                    //    Update_Group()
                    //);
                }
                //// For Interval in Seconds 
                //// This Scheduler will start at 17:22 and call after every 3 Days
                //// IntervalInSeconds(start_hour, start_minute, days)
                //Scheduler.IntervalInDays(17, 22, 3,
                //() => {
                //    Console.WriteLine("//here write the code that you want to schedule");
                //});
                //Console.ReadLine();
                input = Console.ReadKey();
            } while (input.Key != ConsoleKey.Escape);
#endif

            }
            catch (Exception ex)
            {

                mainlog.WriteLine("Error:"+ex.Message+"\r\n"+ex.InnerException);
            }
            finally
            {
                mainlog.WriteLine("End:" + DateTime.Now.ToString());
                mainlog.Close();
            }
        }

        private static void UpdateMailGroup()
        {
            if(DateTime.Now.Date.DayOfWeek== DayOfWeek.Friday)
                SendUpdateMailGroup();
        }

        private static void SetActiveDep()
        {
            string filelog = @"c:\temp\Active Dep_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Active Dep Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            DateTime curDate = DateTime.Now.Date;
            int numAdd = 0;
            
            ERMSEntities db = null;
            try
            {
                db = new ERMSEntities();
                var listDep = db.HR_Dep_Log.Where(x => x.From <= curDate && (x.To==null?new DateTime(2050,12,31):x.To) >= curDate && x.isActive == false).ToList();
                if (listDep.Count > 0)
                {
                    foreach(var item in listDep)
                    {
                        var oDep = db.HR_Dep_Log.Where(x => x.isActive == true && x.CrewID == item.CrewID).FirstOrDefault();
                        if (oDep != null)
                            oDep.isActive = false;
                        item.isActive = true;
                        numAdd++;
                        writetext.WriteLine("Active Dep CrewID: " + item.CrewID+"\tFrom:"+item.From.Value.ToString("dd/MM/yyyy") +"\tTo:"+(item.To==null?"null":item.To.Value.ToString("dd/MM/yyyy")));
                    }
                }
            }
            catch(Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Active Dep Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Active Dep Error: " + ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Active Dep:" + numAdd.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Active Dep Complete!");
                }
                db.Dispose();                
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm")+ " Active Dep Complete!");
            }
        }

        private static void UpdateSafetyCert()
        {
            DTV_CabinetEntities Trn = null;
            ERMSEntities db = null;
            string filelog = @"c:\temp\Safety_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            DateTime begindate = DateTime.Now.Date.AddYears(-3), issuedDate;
            bool isError = false, flagupdate;
            short FilterTrainingID1 = 10;
            byte Filter1DataType = 1;
            string EmployerIDs = "1";
            string otext = "",msnv="";
            try
            {
                Trn = new DTV_CabinetEntities();

                //Trn.Database.Connection.ConnectionString.ti

                db = new ERMSEntities();


                var lstEmp = db.Sys_Account.Where(x => x.IsCrew == true && x.end_date == null 
                                                  && x.Employer != "HVTV" && x.Employer != "NVMD" && x.Employer != null && x.Employer != "").ToList();
                foreach (var item in lstEmp)
                {
                    msnv = item.CrewID;
                    var nhanthan = db.HR_NhanThan.Where(x => x.CrewID == item.CrewID).FirstOrDefault();
                    if (nhanthan != null)
                    {
                        EmployerIDs = (item.Employer == "NN" ? "6" : item.Employer == "ALS" ? "2" : "1");
                        //Filter1DataType = 2;
                        //var iss = Trn.sp_TrainingStudentStatistic_L(FilterTrainingID1, 0, 0, Filter1DataType, 0, 0, 0, 0, 0, "", item.CrewID, "", EmployerIDs, "", "", "", "", "", null, "", "", "", "", "", "", null, 0).FirstOrDefault();
                        var exp = Trn.sp_TrainingStudentStatistic_L(FilterTrainingID1   //filterTrainingID1
                                                                    , 0                 //filterTrainingID2
                                                                    , 0                 //filterTrainingID3
                                                                    , Filter1DataType   //filter1Data
                                                                    , 0                 //filter2Data
                                                                    , 0                 //filter3Data
                                                                    , 0                 //filter1FinalResult
                                                                    , 0                 //filter2FinalResult
                                                                    , 0                 //filter3FinalResult
                                                                    , ""                //classIDs
                                                                    , item.CrewID       //crewID
                                                                    , ""                //crewNameNU
                                                                    , null              //toDate
                                                                    , EmployerIDs       //employerIDs
                                                                    , ""                //employerIDsEx
                                                                    , ""                //employerTypes
                                                                    , ""                //employerTypesEx
                                                                    , ""                //employerStatusCodes
                                                                    , ""                //employerStatusCodesEx
                                                                    , null              //employerStatusCodesIsEmpty
                                                                    , ""                //bases
                                                                    , ""                //basesEx
                                                                    , ""                //crewTitles
                                                                    , ""                //crewTitlesEx
                                                                    , ""                //flyCapability
                                                                    , ""                //flyCapabilityEx
                                                                    , null              //teacher
                                                                    , 0                 //gender
                                                                    ).FirstOrDefault();
                        //Xác định ngày thi là ngày cấp chứng chỉ ATB


                        //if (iss == null && exp == null)
                        //    continue;

                        //if (!iss.c_DataDate1.HasValue && !exp.c_DataDate1.HasValue)
                        //    continue;
                        //otext = "";
                        flagupdate = false;
                        //if (iss != null && iss.c_DataDate1.HasValue)
                        //{
                        //   
                        //}
                        if (exp != null && exp.c_DataDate1.HasValue)
                        {
                            var student = Trn.t_TrainingClassStudent.Where(x => x.c_IsDeleted == false && x.c_CrewID == item.CrewID && x.c_TrainingTypeID == 10 && x.c_ExpiredDate == exp.c_DataDate1).FirstOrDefault();
                            if (student != null)
                            {
                                var safetyclass = Trn.t_TrainingClass.Where(x => x.c_IsDeleted == false && x.c_TrainingTypeID == 10 && x.c_PID == student.c_ClassID).FirstOrDefault();
                                if (safetyclass != null)
                                {
                                    issuedDate = safetyclass.c_TestDate ?? safetyclass.c_FrDate;
                                    if (nhanthan.Safety_Issued != issuedDate)
                                    {
                                        flagupdate = true;
                                        nhanthan.Safety_Issued = issuedDate;
                                        otext = "Update TV " + item.CrewID + "\t ISS:" + issuedDate.ToString("dd/MM/yyyy");
                                    }
                                }
                            }

                            if (nhanthan.Safety_Expired != exp.c_DataDate1)
                            {
                                flagupdate = true;
                                nhanthan.Safety_Expired = exp.c_DataDate1;
                                if (otext == "")
                                    otext = "Update TV " + item.CrewID + "\t ISS:" + "\t EXP:" + exp.c_DataDate1.Value.ToString("dd/MM/yyyy");
                                else
                                    otext = otext + "\t EXP:" + exp.c_DataDate1.Value.ToString("dd/MM/yyyy");
                            }
                        }
                        if (flagupdate)
                        {
                            writetext.WriteLine(otext);
                            db.SaveChanges();
                            Console.WriteLine(otext);
                        }
                    }
                    else
                    {
                        writetext.WriteLine("TV " + item.CrewID + "\t ISS:" + "\t EXP:" + "\tKhông thấy trên Nhanthan");
                    }

                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Error: " + msnv+" " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Safety Error at " + msnv +" "+ ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();                    
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Safety Complete OK!");
                }
                if(Trn!=null)
                    Trn.Dispose();
                if(db!=null)
                    db.Dispose();            
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Safety Cert Complete! " + (isError==true?"Error!":"OK"));
            }
        }

        private static void UpdateClass()
        {
            string otext = "";
            DTV_CabinetEntities Trn = null;
            ERMSEntities db = null;
            string filelog = @"c:\temp\Class_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            DateTime begindate = DateTime.Now.Date.AddYears(-3);
            bool isError = false, flagupdate;
            try
            {
                Trn = new DTV_CabinetEntities();
                db = new ERMSEntities();
                var hisLearning = (from x in Trn.t_TrainingClassStudent
                                   join y in Trn.t_TrainingClass on x.c_ClassID equals y.c_PID
                                   where x.c_IsDeleted == false && y.c_IsDeleted == false && x.c_CrewID.Length == 4 && y.c_ClassNature != 5 && y.c_FrDate>= begindate
                                   select new
                                   {
                                       CrewID = x.c_CrewID,
                                       ClassID = x.c_ClassID,
                                       ClassName = y.c_Name,
                                       FromDate = y.c_FrDate,
                                       ToDate = y.c_ToDate,
                                       Location=y.c_Location,
                                       Result = x.c_FinalResult == 1 ? "Đạt" : x.c_FinalResult == 2 ? "Trượt" : x.c_FinalResult == 3 ? "Vắng" : ""
                                   }).ToList();
                
                foreach(var item in hisLearning)
                {
                    var history = db.TRN_HisLearning.Where(x => x.CrewID == item.CrewID && x.ClassID == item.ClassID).FirstOrDefault();
                    //Mới phát sinh ==> Bổ sung
                    if (history == null)
                    {
                        TRN_HisLearning his = new TRN_HisLearning();
                        his.CrewID = item.CrewID;
                        his.ClassID = item.ClassID;
                        his.ClassName = item.ClassName;
                        his.FromDate = item.FromDate;
                        his.ToDate = item.ToDate;
                        his.Location = item.Location;
                        his.Result = item.Result;
                        his.Created = DateTime.Now;
                        db.TRN_HisLearning.Add(his);
                        writetext.WriteLine("Add New CrewID=" + item.CrewID + " ClassName=" + item.ClassName + " From =" + item.FromDate.ToString("dd/MM/yyyy") + " To =" + item.ToDate.ToString("dd/MM/yyyy"));
                        Console.WriteLine("Add New " + item.CrewID+" Classs "+item.ClassName);
                    }
                    //Đã có ==> cập nhật
                    else
                    {
                       if(history.ClassName!=item.ClassName || history.FromDate!=item.FromDate || history.ToDate!=item.ToDate || history.Location!=item.Location || history.Result != item.Result)
                        {
                            history.ClassName = item.ClassName;
                            history.FromDate = item.FromDate;
                            history.ToDate = item.ToDate;
                            history.Location = item.Location;
                            history.Result = item.Result;
                            history.Modified = DateTime.Now;
                            writetext.WriteLine("Update CrewID=" + item.CrewID + " ClassName=" + item.ClassName+" From ="+ item.FromDate.ToString("dd/MM/yyyy")+" To ="+ item.ToDate.ToString("dd/MM/yyyy"));
                            Console.WriteLine("Update " + item.CrewID + " Classs " + item.ClassName);
                        }
                    }
                }
            }
            catch(SqlException sqlEx)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Class Error: " + sqlEx.Errors + "\n\r" + sqlEx.Message);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm")+" Update Class Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Class Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Class Error: " + ex.Message+"\n\r"+ex.InnerException);
            }
            
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();                    
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Classs Complete!");
                }
                if (Trn!=null)
                    Trn.Dispose();
                if(db!=null)
                    db.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Classs Complete!");
            }
        }

        //private static void UpdateATB()
        //{

        //    short FilterTrainingID1 = 10;
        //    byte Filter1DataType = 2;
        //    string EmployerIDs = "1";
        //    string otext = "";
        //    DTV_CabinetEntities Trn=null;
        //    string filelog = @"c:\temp\ATB_log_New.txt";
        //    StreamWriter writetext = new StreamWriter(filelog, true);
        //    writetext.WriteLine("----------------------------------------------------------------------------------------");
        //    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
        //    bool isError = false,flagupdate;
        //    try
        //    {
        //        Trn = new DTV_CabinetEntities();
        //        using (ERMSEntities db = new ERMSEntities())
        //        {

        //            var lstEmp = db.Sys_Account.Where(x => x.IsCrew == true && x.end_date == null && x.Employer != "HVTV" && x.Employer != "NVMD" && x.Employer != null && x.Employer != "").ToList();
        //            foreach (var item in lstEmp)
        //            {

        //                var nhanthan = db.HR_NhanThan.Where(x => x.CrewID == item.CrewID).FirstOrDefault();
        //                if (nhanthan != null)
        //                {
        //                    EmployerIDs = (item.Employer == "NN" ? "6" : item.Employer == "ALS" ? "2" : "1");
        //                    Filter1DataType = 2;
        //                    var iss = Trn.sp_TrainingStudentStatistic_L(FilterTrainingID1, 0, 0, Filter1DataType, 0, 0,0, 0, 0, "", item.CrewID, "", EmployerIDs, "", "", "", "", "", null, "", "", "", "", "", "", null, 0).FirstOrDefault();
        //                    Filter1DataType = 1;
        //                    var exp = Trn.sp_TrainingStudentStatistic_L(FilterTrainingID1, 0, 0, Filter1DataType, 0, 0, 0, 0, 0, "", item.CrewID, "", EmployerIDs, "", "", "", "", "", null, "", "", "", "", "", "", null, 0).FirstOrDefault();
        //                    if (iss == null && exp == null )
        //                        continue;
                            
        //                    if (!iss.c_DataDate1.HasValue && !exp.c_DataDate1.HasValue)
        //                        continue;
        //                    otext = "";
        //                    flagupdate = false;
        //                    if (iss != null && iss.c_DataDate1.HasValue)
        //                    {
        //                        if(nhanthan.Safety_Issued!= iss.c_DataDate1)
        //                        {
        //                            flagupdate = true;
        //                            nhanthan.Safety_Issued = iss.c_DataDate1;
        //                            otext = "Update TV " + item.CrewID + "\t ISS:" + iss.c_DataDate1.Value.ToString("dd/MM/yyyy");
        //                        }
        //                    }
        //                    if (exp != null && exp.c_DataDate1.HasValue)
        //                    {
        //                        if(nhanthan.Safety_Expired!= exp.c_DataDate1)
        //                        {
        //                            flagupdate = true;
        //                            nhanthan.Safety_Expired = exp.c_DataDate1;
        //                            if(otext=="")
        //                                otext= "TV " + item.CrewID + "\t ISS:" + "\t EXP:" + exp.c_DataDate1.Value.ToString("dd/MM/yyyy");
        //                            else 
        //                                otext =otext+ "\t EXP:" + exp.c_DataDate1.Value.ToString("dd/MM/yyyy");
        //                        }
        //                    }
        //                    if (flagupdate)
        //                    {
        //                        writetext.WriteLine(otext);
        //                        db.SaveChanges();
        //                    }
        //                }
        //                else
        //                {
        //                    writetext.WriteLine("TV " + item.CrewID + "\t ISS:" + "\t EXP:" + "\tKhông thấy trên Nhanthan");
        //                }

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        isError = true;
        //        writetext.WriteLine("Error: " + ex.Message + "\t" + ex.InnerException);
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (!isError)
        //        {
        //            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        //            writetext.WriteLine("Update ATB Complete!");
        //        }

        //        Trn.Dispose();
        //        writetext.Close();
        //        Console.WriteLine("Complete!");
        //    }
        //    if (writetext != null)
        //    {
        //        writetext.Close();
        //    }
        //}

        //private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        //{
        //    writetext.WriteLine("Error:" + ex.Message + "\r\n" + ex.InnerException);
        //}

        private static void Update_Group()
        {
            DateTime curDate = DateTime.Now.Date;
            DateTime fromDate = curDate.AddMonths(-5);
            RedantEntities hr = new RedantEntities();
            NotesSession session;
            NotesDatabase dbase;
            NotesView view;
            NotesDocument doc;
            string newGroupName = "", liendoi = "1*2*3*4*5*6", otext="";
            string filelog = @"c:\temp\Group_log New.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            try
            {
                session = new NotesSession();
                session.Initialize("btliem");
                dbase = session.GetDatabase("domino.dev/DTV", "Nhansu\\qlns.nsf");
                view = dbase.GetView("Nhan su\\Theo ma so");

                using (ERMSEntities db = new ERMSEntities())
                {
                    #region Set Active
                    //var pendingChanegGroup= db.HR_Dep_Log.Where(x => x.From >= fromDate && x.From <= curDate && x.isActive!=null && x.Submitted!=null).OrderBy(x => x.CrewID).ToList();
                    //foreach(var it in pendingChanegGroup)
                    //{
                    //    if (it.isDraft == true)
                    //        it.isDraft = false;

                    //    it.isActive = true;
                    //    it.Modified = DateTime.Now;
                    //    it.Modifier = "Auto";

                    //    var oldGr = db.HR_Dep_Log.Where(x => x.CrewID == it.CrewID && x.isActive == true).FirstOrDefault();
                    //    if(oldGr!=null)
                    //    {
                    //        oldGr.To = it.From.Value.AddDays(-1);
                    //        oldGr.isActive = false;
                    //        oldGr.Modifier = "Auto";
                    //        oldGr.Modified = DateTime.Now;
                    //    }
                    //    db.SaveChanges();
                    //}
                    
                    #endregion
                    var listChangeGroup = db.HR_Dep_Log.Where(x => x.From >= fromDate && x.From <= curDate && x.isActive==true).OrderBy(x=>x.CrewID).ToList();
                    foreach (var item in listChangeGroup)
                    {
                        //if (item.CrewID == "6302")
                        //   otext = "";

                        var NewGroup = db.HR_Dep_Cat.Where(x => x.ID == item.DepID).FirstOrDefault();
                        #region Cập nhật Kiến đỏ
                        //Cập nhật trên Kiến đỏ
                        //Trên Kiến đỏ Bộ phận làm việc bắt đầu với LĐ
                        var hs = hr.HoSoGocs.Where(x => x.mans == item.Manv).FirstOrDefault();
                        if (hs != null)
                        {
                            var currentGroup = hr.danhmucs.Where(x => x.id == hs.bophanlamviec).FirstOrDefault();
                            if (currentGroup != null)
                            {
                                if (NewGroup.DepName == "HV")
                                    newGroupName = "TRN";

                                if (liendoi.Contains(NewGroup.DepName.Substring(0, 1)))
                                    newGroupName = "LĐ" + NewGroup.DepName.Trim();
                                else
                                    newGroupName = NewGroup.DepName.Trim();

                                if (currentGroup.TenDanhMuc.Trim()!= newGroupName) //Cập nhật.
                                {
                                    var newid = hr.danhmucs.Where(x => x.TenDanhMuc == newGroupName).FirstOrDefault();
                                    if (newid == null) //Chưa có trong danh mục Kiến đỏ thì bổ sung vào
                                    {
                                        danhmuc dm = new danhmuc();
                                        dm.TenDanhMuc = newGroupName;
                                        dm.LoaiDanhMuc = "phongban";
                                        dm.TinhTrang = true;
                                        dm.MaDanhMuc = GetRandomString(20);
                                        hr.danhmucs.Add(dm);
                                        hr.SaveChanges();
                                    }
                                    newid = hr.danhmucs.Where(x => x.TenDanhMuc == newGroupName).FirstOrDefault();
                                    hs.bophanlamviec = newid.id;
                                    hr.SaveChanges();
                                    writetext.WriteLine("RedAnt \t" + item.Manv+"\t New:" + newGroupName + "\t Old:"+ currentGroup.TenDanhMuc.Trim());
                                    //result += "RedAnt: " + item.CrewID + "\t" + currentGroup.TenDanhMuc.Trim() + "\t" + newGroupName+"\r\n";
                                }
                            }
                            else
                            {
                                writetext.WriteLine("RedAnt \t" + item.Manv +"\t New:"+"\t Old:"+ hs.bophanlamviec.ToString()+"\t Không tìm thấy trong danh mục Kiến đỏ của ");
                            }
                        }
                        else
                        {
                            writetext.WriteLine("RedAnt \t"+item.Manv+"\t New:"+"\t Old:"+"\t Không tìm thấy trên Kiến đỏ");
                        }
                        #endregion

                        #region Cập nhật Lotus Note
                        //Cập nhật trên Lotus Note
                        //Trên Lotus Note Bộ phận bắt đầu bằng LD
                        if (NewGroup.DepName == "HV")
                            newGroupName = "TRN";

                        if (liendoi.Contains(NewGroup.DepName.Substring(0, 1)))
                            newGroupName = "LD" + NewGroup.DepName.Trim();
                        else
                            newGroupName = NewGroup.DepName.Trim();

                        doc = view.GetDocumentByKey(item.Manv);
                        if (doc != null)
                        {
                            string oBophan = "";
                            oBophan = doc.GetItemValue("Bophan")[0];
                            if (oBophan == null)
                                oBophan = "";

                            if(oBophan.Trim()!= newGroupName)
                            {
                                doc.ReplaceItemValue("Bophan", newGroupName);
                                doc.Save(true, true);
                                writetext.WriteLine("Lotus \t" + item.CrewID + "\t New:" + newGroupName + "\t Old:" + oBophan);                                
                                //result += "Lotus: " + item.CrewID + "\t" + oBophan + "\t" + newGroupName + "\r\n";
                            }
                        }
                        else
                            writetext.WriteLine("Lotus \t"+item.CrewID+"\t New:"+"\t Old:"+"\tKhông tìm thấy trên Lotus");

                        #endregion

                        #region Cập nhật Sys_account

                        newGroupName = NewGroup.DepName;
                        newGroupName = newGroupName.Trim().Replace("TMP", "").Replace(".TMP", "");

                        if (newGroupName.EndsWith("PN"))
                            newGroupName = newGroupName.Replace("PN", "");
                        if (newGroupName.EndsWith("PB"))
                            newGroupName = newGroupName.Replace("PB","");

                        if (newGroupName.EndsWith("S"))
                            newGroupName = newGroupName.Substring(0,newGroupName.Length-1);
                        if (newGroupName.EndsWith("H") && !newGroupName.EndsWith("TH"))
                            newGroupName = newGroupName.Substring(0, newGroupName.Length - 1);


                        if (newGroupName=="HV")
                            newGroupName ="TRN";
                        if(newGroupName=="KT")
                            newGroupName = "KTOA";
                        if (newGroupName == "DT")
                            newGroupName = "DTAO";

                        var acc = db.Sys_Account.Where(x => x.CrewID == item.CrewID).FirstOrDefault();
                        if (acc != null)
                        {
                            if(acc.Group!= newGroupName)
                            {
                                otext = acc.Group;
                                acc.Group = newGroupName.Trim();
                                db.SaveChanges();                                
                                writetext.WriteLine("SysAcc \t" + item.CrewID + "\t New:" + newGroupName + "\t Old:" + otext);
                                //result += "SysAcc: " + item.CrewID + "\t" + otext + "\t" + newGroupName + "\r\n";
                            }
                        }
                        else
                        {
                            writetext.WriteLine("SysAcc \t" + item.CrewID + "\t New:" + "\t Old:" + "\tKhông thấy trên Sys Account");
                        }
                        #endregion
                        
                    }
                }

            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Update Group  Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") +" Update Group Error"+ ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") +" Update Group Complete!");
                }
                if(hr!=null)
                    hr.Dispose();
                view = null;
                dbase = null;
                session = null;
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Group Complete!");
                //File.WriteAllText(@"c:\temp\testgroup.txt", result);
            }
        }

        private static void Update_Thannhan()
        {
            string filelog = @"c:\temp\Thanhnhan_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            ERMSEntities db = null;
            RedantEntities hr = null;
            int numAdd = 0,numUpdate=0;
            try
            {
                db = new ERMSEntities();
                hr = new RedantEntities();
                var lstRelation = hr.USP_Get_Relationship_Info("").ToList();
                foreach (var item in lstRelation)
                {
                    
                    var rela = db.HR_ThanNhan.Where(x => x.CrewID == item.CrewiD && x.OrgID == item.OrgID).FirstOrDefault();
                    if (rela != null)
                    {
                        rela.Relationship = item.Relationship;
                        rela.FullName = item.FulleName;

                        rela.DoB = item.DoB;
                        rela.Note = item.Note;
                        numUpdate++;
                    }
                    else
                    {
                        HR_ThanNhan rel = new HR_ThanNhan();
                        rel.CrewID = item.CrewiD;
                        rel.OrgID = item.OrgID;
                        rel.Relationship = item.Relationship;
                        rel.FullName = item.FulleName;
                        rel.DoB = item.DoB;
                        rel.Note = item.Note;
                        db.HR_ThanNhan.Add(rel);
                        numAdd++;
                        
                    }
                    Console.WriteLine("Cập nhật Thân nhân {0}", item.CrewiD);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Update Than Nhan Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Thanh Nhan Error: "+ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Thanh nhan Add:" + numAdd.ToString() + " Update: " + numUpdate.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Update Thanh Nhan Complete!");
                }
                if(db!=null)
                    db.Dispose();
                if(hr!=null)
                    hr.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Thanh nhan Complete!");
            }
        }

        private static void Update_Nhanthan()
        {
            string filelog = @"c:\temp\Nhanthan_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            ERMSEntities db = null;
            RedantEntities hr = null;
            int numAdd = 0,numUpdate=0;
            try
            {
                db = new ERMSEntities();
                hr = new RedantEntities();
                var lstAdress = hr.USP_Get_Address("").ToList();
                foreach (var item in lstAdress)
                {
                    var addr = db.HR_NhanThan.Where(x => x.CrewID == item.CrewID).FirstOrDefault();
                    var chucvu = hr.USP_Get_Chucvu(item.CrewID).FirstOrDefault();
                    if (addr == null)
                    {
                        HR_NhanThan nhanthan = new HR_NhanThan();
                        nhanthan.CrewID = item.CrewID;
                        nhanthan.TemporaryAddress = item.TemporaryAddress;
                        nhanthan.PermanentAddress = item.PermanentAddress;
                        nhanthan.HomeTown = item.HomeTown;
                        nhanthan.Course = item.dotuyen == null ? "" : item.dotuyen;
                        nhanthan.SignFirstName = item.TenStt == null ? "" : item.TenStt;
                        nhanthan.SignLastName = item.ns_ho == null ? "" : item.ns_ho;
                        nhanthan.Gender = item.gioitinh == 2536 ? true : false;
                        nhanthan.Title = chucvu == null ? "" : chucvu;
                        nhanthan.PlaceOfBirth = item.Noisinh == null ? "" : item.Noisinh;
                        db.HR_NhanThan.Add(nhanthan);
                        numAdd++;
                    }
                    else
                    {
                        addr.TemporaryAddress = item.TemporaryAddress;
                        addr.PermanentAddress = item.PermanentAddress;
                        addr.HomeTown = item.HomeTown;
                        addr.Course = item.dotuyen == null ? "" : item.dotuyen;
                        addr.SignFirstName = item.TenStt == null ? "" : item.TenStt;
                        addr.SignLastName = item.ns_ho == null ? "" : item.ns_ho;
                        addr.Gender = item.gioitinh == 2536 ? true : false;
                        addr.Title = chucvu == null ? "" : chucvu;
                        addr.PlaceOfBirth = item.Noisinh == null ? "" : item.Noisinh;
                        numUpdate++;
                    }

                    Console.WriteLine("Cập nhật Nhân thân {0}", item.CrewID);

                };
                foreach(var item in db.TMP_GSHH.Where(x => x.Info != null))
                {
                    var it = db.HR_NhanThan.Where(x => x.CrewID == item.CrewID).FirstOrDefault();
                    if (it != null)
                        it.KN_Khac = item.Info;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Nhan thân Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Nhan than Error: "+ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Nhanthan Add: " + numAdd.ToString() + " Update: " + numUpdate.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Nhan than Complete!");
                }
                if(db!=null)
                    db.Dispose();
                if(hr!=null)
                    hr.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Nhan than Complete!");
            }
        }

        private static void Update_KTKL()
        {
            
            string filelog = @"c:\temp\KTKL_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);

            bool isError = false;
            ERMSEntities erms = null;
            RedantEntities hr = null;
            int numAdd = 0;
            try
            {
                erms = new ERMSEntities();
                hr = new RedantEntities();
                //Chỉ lấy KTKKl 6 tháng gần nhất xét để giảm thời gian
                DateTime ngayxet = DateTime.Now.AddMonths(-6);
                var dsktkl = hr.PView_ktkl
                                .Where(x => x.ktkl_hinhthuc != null && x.ktkl_ngayqd != null && x.kyluat != null && x.ktkl_ngayqd >= ngayxet)
                                .ToList();

                foreach (var ktkl in dsktkl)
                {
                    var item = erms.PView_ktkl.Where(x => x.mans == ktkl.mans && x.ktkl_hinhthuc == ktkl.ktkl_hinhthuc && x.ktkl_ngayqd == ktkl.ktkl_ngayqd && x.kyluat == ktkl.kyluat).FirstOrDefault();
                    if (item == null)
                    {
                        erms.PView_ktkl.Add(ktkl);
                        numAdd++;
                        Console.WriteLine("Add KTKL " + ktkl.mans);
                    }                  
                        
                }
        }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update KTKL Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm")  +" Update KTKL Error: "+ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    erms.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Add KTKL total:" + numAdd.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update KTKL Complete!");
                }
                if(erms!=null)
                    erms.Dispose();
                if(hr!=null)
                    hr.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update KTKL Complete!");
            }


        }

        private static void Update_Learning()
        {
            //string f_source = @"\\10.97.9.108\FoxApp\HLDT\Data\process.dbf";
            //string filelog = @"c:\temp\Learning_log.txt";            
            //StreamWriter writetext = new StreamWriter(filelog,true);

            //bool isError = false;
            //ERMSEntities erms = null;
            //FileStream fslearing = null;
            //Table learning = null;
            //int rowadd = 0, rowupdate = 0;
            ////try
            ////{
                
            //    if (File.Exists(f_source))
            //    {
            //        writetext.WriteLine("----------------------------------------------------------------------------------------");
            //        writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm")+" Start!");
            //        erms = new ERMSEntities();

            //        //đánh dấu xóa các trường hợp testdate.Value >= DateTime.Now.Date.AddMonths(-24)) để bên source nguồn có xóa rồi lấy qua kg bị xót
            //        DateTime threshold = DateTime.Now.Date.AddMonths(-12*30);
            //        foreach (var item in erms.TRN_Learning.Where(x=>x.testdate.Value>=threshold))
            //        {
            //            item.isDeleted = true;
            //        }
            //        erms.SaveChanges();

            //        string f_des = @"C:\temp\process.dbf";
            //        string status = "", editstat = "";
            //        File.Copy(f_source, f_des, true);
            //        fslearing = new FileStream(f_des, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //        learning = Table.Open(fslearing);
            //        var learn = learning.OpenReader(Encoding.ASCII);
            //        while (learn.Read())
            //        {
            //            string Manv, Code;
            //            DateTime? testdate, expireddate;

            //            Manv = learn.GetString("PAXCODE");
            //            Code = learn.GetString("OBJCODE");
            //            if (Code == null)
            //                Code = "";
            //            testdate = learn.GetDateTime("TESTDATE");
            //            expireddate = learn.GetDateTime("EXPIREDATE");
                        
            //            status= learn.GetString("STATUS");
            //            if (status == null)
            //                status = "";
            //            editstat =learn.GetString("EDITSTAT");
            //            if (editstat == null)
            //                editstat = "";

            //            if (testdate == null)
            //                continue;

            //            if (testdate.Value < DateTime.Now.Date.AddMonths(-12*30))
            //                continue;

            //            var it = erms.TRN_Learning.Where(x => x.PaxCode == Manv && x.objcode == Code && x.testdate.Value == testdate.Value && x.expiredate.Value == expireddate.Value && x.status==status && x.editstat==editstat).FirstOrDefault();

            //            if (it == null) //Chưa có -> thim vào.
            //            {
            //                TRN_Learning row = new TRN_Learning();

            //                row.TrnCode = learn.GetString("TRNCODE");
            //                row.PaxCode = learn.GetString("PAXCODE");
            //                row.objcode = learn.GetString("OBJCODE");
            //                row.Descript = learn.GetString("DESCRIPT");
            //                row.cdatefrom = learn.GetDateTime("CDATEFROM") == null ? "" : learn.GetDateTime("CDATEFROM").Value.ToString();
            //                row.cdateto = learn.GetDateTime("CDATETO") == null ? "" : learn.GetDateTime("CDATETO").Value.ToString();
            //                row.status = learn.GetString("STATUS");
            //                row.oa = learn.GetBoolean("OA") == null ? false : learn.GetBoolean("OA").Value;
            //                row.eo32 = learn.GetBoolean("EO32") == null ? false : learn.GetBoolean("EO32").Value;
            //                row.eo76 = learn.GetBoolean("EO76") == null ? false : learn.GetBoolean("EO76").Value;
            //                row.eotu = learn.GetBoolean("EOTU") == null ? false : learn.GetBoolean("EOTU").Value;
            //                row.eoat = learn.GetBoolean("EOAT") == null ? false : learn.GetBoolean("EOAT").Value;
            //                row.eoyk = learn.GetBoolean("EOYK") == null ? false : learn.GetBoolean("EOYK").Value;
            //                row.eofo = learn.GetBoolean("EOFO") == null ? false : learn.GetBoolean("EOFO").Value;
            //                row.di = learn.GetBoolean("DI") == null ? false : learn.GetBoolean("DI").Value;
            //                row.ff = learn.GetBoolean("FF") == null ? false : learn.GetBoolean("FF").Value;
            //                row.js = learn.GetBoolean("JS") == null ? false : learn.GetBoolean("JS").Value;
            //                row.pi = learn.GetBoolean("PI") == null ? false : learn.GetBoolean("PI").Value;
            //                row.cc = learn.GetBoolean("CC") == null ? false : learn.GetBoolean("CC").Value;
            //                row.wt1 = learn.GetDecimal("WT1") == null ? 0 : (int)learn.GetDecimal("WT1");
            //                row.ot1 = learn.GetDecimal("OT1") == null ? 0 : (int)learn.GetDecimal("OT1");
            //                row.wt2 = learn.GetDecimal("WT2") == null ? 0 : (int)learn.GetDecimal("WT2");
            //                row.ot2 = learn.GetDecimal("OT2") == null ? 0 : (int)learn.GetDecimal("OT2");
            //                row.fa1 = learn.GetDecimal("FA1") == null ? 0 : (int)learn.GetDecimal("FA1");
            //                row.fa2 = learn.GetBoolean("FA2") == null ? false : learn.GetBoolean("FA2").Value;
            //                row.res1 = learn.GetDecimal("RES1") == null ? 0 : (int)learn.GetDecimal("RES1");
            //                row.res2 = learn.GetDecimal("RES2") == null ? 0 : (int)learn.GetDecimal("RES2");
            //                row.res3 = learn.GetDecimal("RES3") == null ? 0 : (int)learn.GetDecimal("RES3");
            //                row.res4 = learn.GetDecimal("RES4") == null ? 0 : (int)learn.GetDecimal("RES4");
            //                row.res5 = learn.GetDecimal("RES5") == null ? 0 : (int)learn.GetDecimal("RES5");
            //                row.res6 = learn.GetDecimal("RES6") == null ? 0 : (int)learn.GetDecimal("RES6");
            //                row.res7 = learn.GetDecimal("RES7") == null ? 0 : (int)learn.GetDecimal("RES7");
            //                row.res8 = learn.GetDecimal("RES8") == null ? 0 : (int)learn.GetDecimal("RES8");
            //                row.testdate = learn.GetDateTime("TESTDATE");
            //                row.expiredate = learn.GetDateTime("EXPIREDATE");
            //                row.editstat = learn.GetString("EDITSTAT");

            //                row.editdt = learn.GetDateTime("EDITDT");
            //                row.tmprecno = learn.GetDecimal("TMPRECNO") == null ? 0 : (int?)learn.GetDecimal("TMPRECNO");
            //                row.l1 = learn.GetBoolean("L1") == null ? false : learn.GetBoolean("L1").Value;
            //                row.l2 = learn.GetBoolean("L2") == null ? false : learn.GetBoolean("L2").Value;
            //                row.l3 = learn.GetBoolean("L3") == null ? false : learn.GetBoolean("L3").Value;
            //                row.l4 = learn.GetBoolean("L4") == null ? false : learn.GetBoolean("L4").Value;
            //                row.note1 = learn.GetString("NOTE1");
            //                row.note2 = learn.GetString("NOTE2");
            //                row.note3 = learn.GetString("NOTE3");
            //                row.Added = DateTime.Now;

            //                erms.TRN_Learning.Add(row);
            //                Console.WriteLine("Add Learning " + learn.GetString("PAXCODE"));
            //                rowadd++;
            //            }
            //            else //Có rồi -> cập nhật.
            //            {
            //                it.TrnCode = learn.GetString("TRNCODE");
            //                it.Descript = learn.GetString("DESCRIPT");
            //                it.cdatefrom = learn.GetDateTime("CDATEFROM") == null ? "" : learn.GetDateTime("CDATEFROM").Value.ToString();
            //                it.cdateto = learn.GetDateTime("CDATETO") == null ? "" : learn.GetDateTime("CDATETO").Value.ToString();
            //                it.status = learn.GetString("STATUS");
            //                it.oa = learn.GetBoolean("OA") == null ? false : learn.GetBoolean("OA").Value;
            //                it.eo32 = learn.GetBoolean("EO32") == null ? false : learn.GetBoolean("EO32").Value;
            //                it.eo76 = learn.GetBoolean("EO76") == null ? false : learn.GetBoolean("EO76").Value;
            //                it.eotu = learn.GetBoolean("EOTU") == null ? false : learn.GetBoolean("EOTU").Value;
            //                it.eoat = learn.GetBoolean("EOAT") == null ? false : learn.GetBoolean("EOAT").Value;
            //                it.eoyk = learn.GetBoolean("EOYK") == null ? false : learn.GetBoolean("EOYK").Value;
            //                it.eofo = learn.GetBoolean("EOFO") == null ? false : learn.GetBoolean("EOFO").Value;
            //                it.di = learn.GetBoolean("DI") == null ? false : learn.GetBoolean("DI").Value;
            //                it.ff = learn.GetBoolean("FF") == null ? false : learn.GetBoolean("FF").Value;
            //                it.js = learn.GetBoolean("JS") == null ? false : learn.GetBoolean("JS").Value;
            //                it.pi = learn.GetBoolean("PI") == null ? false : learn.GetBoolean("PI").Value;
            //                it.cc = learn.GetBoolean("CC") == null ? false : learn.GetBoolean("CC").Value;
            //                it.wt1 = learn.GetDecimal("WT1") == null ? 0 : (int)learn.GetDecimal("WT1");
            //                it.ot1 = learn.GetDecimal("OT1") == null ? 0 : (int)learn.GetDecimal("OT1");
            //                it.wt2 = learn.GetDecimal("WT2") == null ? 0 : (int)learn.GetDecimal("WT2");
            //                it.ot2 = learn.GetDecimal("OT2") == null ? 0 : (int)learn.GetDecimal("OT2");
            //                it.fa1 = learn.GetDecimal("FA1") == null ? 0 : (int)learn.GetDecimal("FA1");
            //                it.fa2 = learn.GetBoolean("FA2") == null ? false : learn.GetBoolean("FA2").Value;
            //                it.res1 = learn.GetDecimal("RES1") == null ? 0 : (int)learn.GetDecimal("RES1");
            //                it.res2 = learn.GetDecimal("RES2") == null ? 0 : (int)learn.GetDecimal("RES2");
            //                it.res3 = learn.GetDecimal("RES3") == null ? 0 : (int)learn.GetDecimal("RES3");
            //                it.res4 = learn.GetDecimal("RES4") == null ? 0 : (int)learn.GetDecimal("RES4");
            //                it.res5 = learn.GetDecimal("RES5") == null ? 0 : (int)learn.GetDecimal("RES5");
            //                it.res6 = learn.GetDecimal("RES6") == null ? 0 : (int)learn.GetDecimal("RES6");
            //                it.res7 = learn.GetDecimal("RES7") == null ? 0 : (int)learn.GetDecimal("RES7");
            //                it.res8 = learn.GetDecimal("RES8") == null ? 0 : (int)learn.GetDecimal("RES8");
            //                it.editstat = learn.GetString("EDITSTAT");

            //                it.editdt = learn.GetDateTime("EDITDT");
            //                it.tmprecno = learn.GetDecimal("TMPRECNO") == null ? 0 : (int?)learn.GetDecimal("TMPRECNO");
            //                it.l1 = learn.GetBoolean("L1") == null ? false : learn.GetBoolean("L1").Value;
            //                it.l2 = learn.GetBoolean("L2") == null ? false : learn.GetBoolean("L2").Value;
            //                it.l3 = learn.GetBoolean("L3") == null ? false : learn.GetBoolean("L3").Value;
            //                it.l4 = learn.GetBoolean("L4") == null ? false : learn.GetBoolean("L4").Value;
            //                it.note1 = learn.GetString("NOTE1");
            //                it.note2 = learn.GetString("NOTE2");
            //                it.note3 = learn.GetString("NOTE3");
            //                it.Updated = DateTime.Now;
            //                it.isDeleted = false;
            //                Console.WriteLine("Update Learning " + learn.GetString("PAXCODE"));
            //                rowupdate++;
            //            }

            //        }
            //    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    isError = true;
            ////    writetext.WriteLine("Error: " + ex.Message);
            ////    Console.WriteLine(ex.Message);
            ////}
            ////finally
            ////{
            ////    if (!isError)
            ////    {
            ////        erms.SaveChanges();
            ////        writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " TRN_Learning Add:" + rowadd.ToString() + ", Update:" + rowupdate.ToString());
            ////        writetext.WriteLine("Complete!");
            ////    }
            //    writetext.Close();                
            //    erms.Dispose();                
            //    fslearing.Dispose();
            //    Console.WriteLine("Complete!");
            ////}                
           
        }

        private static void Update_Subject()
        {
            //string f_source = @"\\10.97.9.108\FoxApp\HLDT\Data\process.dbf";
            //string filelog = @"c:\temp\Subject_log.txt";
            //StreamWriter writetext = new StreamWriter(filelog, true);

            //bool isError = false;
            //ERMSEntities erms = null;
            //FileStream fslearing = null;
            //Table learning = null;
            //int rowadd = 0, rowupdate = 0;
            //try
            //{

            //    if (File.Exists(f_source))
            //    {
            //        writetext.WriteLine("----------------------------------------------------------------------------------------");
            //        writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            //        erms = new ERMSEntities();
            //        string f_des = @"C:\temp\process.dbf";

            //        File.Copy(f_source, f_des, true);
            //        fslearing = new FileStream(f_des, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //        learning = Table.Open(fslearing);
            //        var learn = learning.OpenReader(Encoding.ASCII);
            //        while (learn.Read())
            //        {
            //            string Manv, Code;
            //            DateTime? testdate, expireddate;

            //            Manv = learn.GetString("PAXCODE");
            //            Code = learn.GetString("OBJCODE");
            //            testdate = learn.GetDateTime("TESTDATE");
            //            expireddate = learn.GetDateTime("EXPIREDATE");

            //            if (testdate == null)
            //                continue;

            //            if (testdate.Value < DateTime.Now.Date.AddMonths(-12))
            //                continue;

            //            var it = erms.TRN_Learning.Where(x => x.PaxCode == Manv && x.objcode == Code && x.testdate.Value == testdate.Value && x.expiredate.Value == expireddate.Value).FirstOrDefault();

            //            if (it == null) //Chưa có -> thim vào.
            //            {
            //                TRN_Learning row = new TRN_Learning();

            //                row.TrnCode = learn.GetString("TRNCODE");
            //                row.PaxCode = learn.GetString("PAXCODE");
            //                row.objcode = learn.GetString("OBJCODE");
            //                row.Descript = learn.GetString("DESCRIPT");
            //                row.cdatefrom = learn.GetDateTime("CDATEFROM") == null ? "" : learn.GetDateTime("CDATEFROM").Value.ToString();
            //                row.cdateto = learn.GetDateTime("CDATETO") == null ? "" : learn.GetDateTime("CDATETO").Value.ToString();
            //                row.status = learn.GetString("STATUS");
            //                row.oa = learn.GetBoolean("OA") == null ? false : learn.GetBoolean("OA").Value;
            //                row.eo32 = learn.GetBoolean("EO32") == null ? false : learn.GetBoolean("EO32").Value;
            //                row.eo76 = learn.GetBoolean("EO76") == null ? false : learn.GetBoolean("EO76").Value;
            //                row.eotu = learn.GetBoolean("EOTU") == null ? false : learn.GetBoolean("EOTU").Value;
            //                row.eoat = learn.GetBoolean("EOAT") == null ? false : learn.GetBoolean("EOAT").Value;
            //                row.eoyk = learn.GetBoolean("EOYK") == null ? false : learn.GetBoolean("EOYK").Value;
            //                row.eofo = learn.GetBoolean("EOFO") == null ? false : learn.GetBoolean("EOFO").Value;
            //                row.di = learn.GetBoolean("DI") == null ? false : learn.GetBoolean("DI").Value;
            //                row.ff = learn.GetBoolean("FF") == null ? false : learn.GetBoolean("FF").Value;
            //                row.js = learn.GetBoolean("JS") == null ? false : learn.GetBoolean("JS").Value;
            //                row.pi = learn.GetBoolean("PI") == null ? false : learn.GetBoolean("PI").Value;
            //                row.cc = learn.GetBoolean("CC") == null ? false : learn.GetBoolean("CC").Value;
            //                row.wt1 = learn.GetDecimal("WT1") == null ? 0 : (int)learn.GetDecimal("WT1");
            //                row.ot1 = learn.GetDecimal("OT1") == null ? 0 : (int)learn.GetDecimal("OT1");
            //                row.wt2 = learn.GetDecimal("WT2") == null ? 0 : (int)learn.GetDecimal("WT2");
            //                row.ot2 = learn.GetDecimal("OT2") == null ? 0 : (int)learn.GetDecimal("OT2");
            //                row.fa1 = learn.GetDecimal("FA1") == null ? 0 : (int)learn.GetDecimal("FA1");
            //                row.fa2 = learn.GetBoolean("FA2") == null ? false : learn.GetBoolean("FA2").Value;
            //                row.res1 = learn.GetDecimal("RES1") == null ? 0 : (int)learn.GetDecimal("RES1");
            //                row.res2 = learn.GetDecimal("RES2") == null ? 0 : (int)learn.GetDecimal("RES2");
            //                row.res3 = learn.GetDecimal("RES3") == null ? 0 : (int)learn.GetDecimal("RES3");
            //                row.res4 = learn.GetDecimal("RES4") == null ? 0 : (int)learn.GetDecimal("RES4");
            //                row.res5 = learn.GetDecimal("RES5") == null ? 0 : (int)learn.GetDecimal("RES5");
            //                row.res6 = learn.GetDecimal("RES6") == null ? 0 : (int)learn.GetDecimal("RES6");
            //                row.res7 = learn.GetDecimal("RES7") == null ? 0 : (int)learn.GetDecimal("RES7");
            //                row.res8 = learn.GetDecimal("RES8") == null ? 0 : (int)learn.GetDecimal("RES8");
            //                row.testdate = learn.GetDateTime("TESTDATE");
            //                row.expiredate = learn.GetDateTime("EXPIREDATE");
            //                row.editstat = learn.GetString("EDITSTAT");

            //                row.editdt = learn.GetDateTime("EDITDT");
            //                row.tmprecno = learn.GetDecimal("TMPRECNO") == null ? 0 : (int?)learn.GetDecimal("TMPRECNO");
            //                row.l1 = learn.GetBoolean("L1") == null ? false : learn.GetBoolean("L1").Value;
            //                row.l2 = learn.GetBoolean("L2") == null ? false : learn.GetBoolean("L2").Value;
            //                row.l3 = learn.GetBoolean("L3") == null ? false : learn.GetBoolean("L3").Value;
            //                row.l4 = learn.GetBoolean("L4") == null ? false : learn.GetBoolean("L4").Value;
            //                row.note1 = learn.GetString("NOTE1");
            //                row.note2 = learn.GetString("NOTE2");
            //                row.note3 = learn.GetString("NOTE3");
            //                row.Added = DateTime.Now;

            //                erms.TRN_Learning.Add(row);
            //                Console.WriteLine("Add Learning " + learn.GetString("PAXCODE"));
            //                rowadd++;
            //            }
            //            else //Có rồi -> cập nhật.
            //            {
            //                it.TrnCode = learn.GetString("TRNCODE");
            //                it.Descript = learn.GetString("DESCRIPT");
            //                it.cdatefrom = learn.GetDateTime("CDATEFROM") == null ? "" : learn.GetDateTime("CDATEFROM").Value.ToString();
            //                it.cdateto = learn.GetDateTime("CDATETO") == null ? "" : learn.GetDateTime("CDATETO").Value.ToString();
            //                it.status = learn.GetString("STATUS");
            //                it.oa = learn.GetBoolean("OA") == null ? false : learn.GetBoolean("OA").Value;
            //                it.eo32 = learn.GetBoolean("EO32") == null ? false : learn.GetBoolean("EO32").Value;
            //                it.eo76 = learn.GetBoolean("EO76") == null ? false : learn.GetBoolean("EO76").Value;
            //                it.eotu = learn.GetBoolean("EOTU") == null ? false : learn.GetBoolean("EOTU").Value;
            //                it.eoat = learn.GetBoolean("EOAT") == null ? false : learn.GetBoolean("EOAT").Value;
            //                it.eoyk = learn.GetBoolean("EOYK") == null ? false : learn.GetBoolean("EOYK").Value;
            //                it.eofo = learn.GetBoolean("EOFO") == null ? false : learn.GetBoolean("EOFO").Value;
            //                it.di = learn.GetBoolean("DI") == null ? false : learn.GetBoolean("DI").Value;
            //                it.ff = learn.GetBoolean("FF") == null ? false : learn.GetBoolean("FF").Value;
            //                it.js = learn.GetBoolean("JS") == null ? false : learn.GetBoolean("JS").Value;
            //                it.pi = learn.GetBoolean("PI") == null ? false : learn.GetBoolean("PI").Value;
            //                it.cc = learn.GetBoolean("CC") == null ? false : learn.GetBoolean("CC").Value;
            //                it.wt1 = learn.GetDecimal("WT1") == null ? 0 : (int)learn.GetDecimal("WT1");
            //                it.ot1 = learn.GetDecimal("OT1") == null ? 0 : (int)learn.GetDecimal("OT1");
            //                it.wt2 = learn.GetDecimal("WT2") == null ? 0 : (int)learn.GetDecimal("WT2");
            //                it.ot2 = learn.GetDecimal("OT2") == null ? 0 : (int)learn.GetDecimal("OT2");
            //                it.fa1 = learn.GetDecimal("FA1") == null ? 0 : (int)learn.GetDecimal("FA1");
            //                it.fa2 = learn.GetBoolean("FA2") == null ? false : learn.GetBoolean("FA2").Value;
            //                it.res1 = learn.GetDecimal("RES1") == null ? 0 : (int)learn.GetDecimal("RES1");
            //                it.res2 = learn.GetDecimal("RES2") == null ? 0 : (int)learn.GetDecimal("RES2");
            //                it.res3 = learn.GetDecimal("RES3") == null ? 0 : (int)learn.GetDecimal("RES3");
            //                it.res4 = learn.GetDecimal("RES4") == null ? 0 : (int)learn.GetDecimal("RES4");
            //                it.res5 = learn.GetDecimal("RES5") == null ? 0 : (int)learn.GetDecimal("RES5");
            //                it.res6 = learn.GetDecimal("RES6") == null ? 0 : (int)learn.GetDecimal("RES6");
            //                it.res7 = learn.GetDecimal("RES7") == null ? 0 : (int)learn.GetDecimal("RES7");
            //                it.res8 = learn.GetDecimal("RES8") == null ? 0 : (int)learn.GetDecimal("RES8");
            //                it.editstat = learn.GetString("EDITSTAT");

            //                it.editdt = learn.GetDateTime("EDITDT");
            //                it.tmprecno = learn.GetDecimal("TMPRECNO") == null ? 0 : (int?)learn.GetDecimal("TMPRECNO");
            //                it.l1 = learn.GetBoolean("L1") == null ? false : learn.GetBoolean("L1").Value;
            //                it.l2 = learn.GetBoolean("L2") == null ? false : learn.GetBoolean("L2").Value;
            //                it.l3 = learn.GetBoolean("L3") == null ? false : learn.GetBoolean("L3").Value;
            //                it.l4 = learn.GetBoolean("L4") == null ? false : learn.GetBoolean("L4").Value;
            //                it.note1 = learn.GetString("NOTE1");
            //                it.note2 = learn.GetString("NOTE2");
            //                it.note3 = learn.GetString("NOTE3");
            //                it.Updated = DateTime.Now;
            //                Console.WriteLine("Update Learning " + learn.GetString("PAXCODE"));
            //                rowupdate++;
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    isError = true;
            //    writetext.WriteLine("Error: " + ex.Message);
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    if (!isError)
            //    {
            //        erms.SaveChanges();
            //        writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " TRN_Learning Add:" + rowadd.ToString() + ", Update:" + rowupdate.ToString());
            //        writetext.WriteLine("Complete!");
            //    }
            //    writetext.Close();
            //    erms.Dispose();
            //    fslearing.Dispose();
            //    Console.WriteLine("Complete!");
            //}

        }

        private static void Update_SMS_Acc()
        {
            string filelog = @"c:\temp\SMS_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm")+" Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            ERMSEntities db=null;
            RedantEntities redant = null;
            SataHRMEntities sms=null;
            bool isError = false;
            int numadd = 0;
            string basehan = "4*5*6";
            try
            {
                db = new ERMSEntities();
                sms = new SataHRMEntities();
                redant = new RedantEntities();
                foreach (var mail in db.HR_Mail.Where(x => x.Status != "XX" && x.manv!=""))
                {
                    var smsac = sms.smsAddressBooks.Where(x => x.ContactCode == mail.manv).FirstOrDefault();
                    var hs = redant.HoSoGocs.Where(x => x.mans == mail.manv).FirstOrDefault();
                    if (smsac == null)
                    {
                        smsAddressBook newacc = new smsAddressBook();
                        var group = redant.danhmucs.Where(x => x.id == hs.bophanlamviec).FirstOrDefault();
                        newacc.ContactCode = mail.manv;
                        newacc.FullName = hs.tenkodau.ToUpper();
                        newacc.FirstName = hs.Tenkd.ToUpper();
                        newacc.Gender = hs.gioitinh == 2536 ? "M" : "F";// false(item.man == false) ? "F" : "M";
                        newacc.MobilePhone = mail.phone;
                        newacc.MainBase = group.TenDanhMuc.Length <= 4 ? group.TenDanhMuc : group.TenDanhMuc.Contains("CXR") || group.TenDanhMuc.Contains("DAD") ? group.TenDanhMuc.Substring(4, 3) : basehan.Contains(group.TenDanhMuc.Substring(2, 1)) ? "HAN" : "SGN";//item.main_base == null ? "SGN" : item.main_base;
                        newacc.Group = group.TenDanhMuc == "Không rõ" ? "TRN" : group.TenDanhMuc;
                        newacc.Email = mail.mail;
                        sms.smsAddressBooks.Add(newacc);
                        numadd++;
                        Console.WriteLine("SMS Add " + mail.manv);
                    }
                }
            }
            catch (Exception ex)
            {
                //isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update SMS ACC Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update SMS Acc Error: "+ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {

                if (!isError)
                {
                    sms.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " SMS Add:" + numadd.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Date SMS ACC Complete!");
                }
                writetext.Close();
                if (redant != null)
                    redant.Dispose();
                if (db != null)
                    db.Dispose();
                if (sms != null)
                    sms.Dispose();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update SMS ACC Complete!");
            }

        }

        private static void Update_Giobay()
        {

            string filelog = @"c:\temp\Giobay_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            double giobay = 0;
            string thang = "";
            CabinetEntities sal = null;
            ERMSEntities db = null;
            try
            {
                DateTime ctungay = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
                
                DateTime ctoingay = ctungay.AddMonths(1).AddDays(-1);
                string period = "SLR_" + ctungay.ToString("yyyyMMdd") + ctoingay.ToString("yyyyMMdd");
                thang = ctungay.ToString("dd/MM/yyyy").Substring(3, 7);
                sal = new CabinetEntities();
                db = new ERMSEntities();
                var SalFlytime = sal.t_SalaryLCBDetail
                                    .Where(x => x.c_PeriodTag == period && x.c_JobSalary != "POS" && x.c_JobSalary != "DEH" &&
                                                (x.c_IsDeleted == false || x.c_IsDeleted == null))
                                    .GroupBy(x => x.c_CrewID)
                                    .Select(x => new
                                    {
                                        CrewID = x.Key,
                                        Total = x.Sum(y => y.c_FlyingMinsA),
                                        B787 = x.Sum(y => y.c_AircarftType == "787" ? y.c_FlyingMinsA : 0),
                                        A350 = x.Sum(y => y.c_AircarftType == "350" ? y.c_FlyingMinsA : 0),
                                        A321 = x.Sum(y => y.c_AircarftType == "321" ? y.c_FlyingMinsA : 0),
                                        GBTVT2 = x.Sum(y => y.c_JobSalary == "I" || y.c_JobSalary == "P" ? y.c_FlyingMinsA : 0),
                                        GBTVT1 = x.Sum(y => y.c_JobSalary == "P1" || y.c_JobSalary == "X" ? y.c_FlyingMinsA : 0),
                                        GBTVC = x.Sum(y => y.c_JobSalary == "B" ? y.c_FlyingMinsA : 0),
                                    })
                                    .ToList();
                //Reset số liệu của tháng hiện tại về zero để tránh lỗi dữ liệu
                foreach (var it in db.HR_GBLuyke.Where(x => x.month == thang))
                {
                    it.total = 0;
                    it.f_321 = 0;
                    it.f_350 = 0;
                    it.f_787 = 0;
                    it.gbtvt2 = 0;
                    it.gbtvt1 = 0;
                    it.gbtvc = 0;
                }
                //Cập nhật giờ bay
                foreach (var it in SalFlytime)
                {
                    
                    var gb = db.HR_GBLuyke.Where(x => x.crewid == it.CrewID && x.month == thang).FirstOrDefault();
                    if (gb == null) //Chưa có thì tjeem vào
                    {
                        HR_GBLuyke gnnew = new HR_GBLuyke();
                        gnnew.month = thang;
                        gnnew.crewid = it.CrewID;
                        gnnew.total = (double?)it.Total;
                        gnnew.f_321 = (double?)it.A321;
                        gnnew.f_350 = (double?)it.A350;
                        gnnew.f_787 = (double?)it.B787;
                        gnnew.gbtvt2 = (double?)it.GBTVT2;
                        gnnew.gbtvt1 = (double?)it.GBTVT1;
                        gnnew.gbtvc = (double?)it.GBTVC;
                        db.HR_GBLuyke.Add(gnnew);
                        Console.WriteLine("Add GB " + ctungay.ToString("dd/MM/yyyy").Substring(3) + "-" + it.CrewID);
                    }
                    else //Có rồi thì cập nhật
                    {
                        gb.total = (double?)it.Total;
                        gb.f_321 = (double?)it.A321;
                        gb.f_350 = (double?)it.A350;
                        gb.f_787 = (double?)it.B787;
                        gb.gbtvt2 = (double?)it.GBTVT2;
                        gb.gbtvt1 = (double?)it.GBTVT1;
                        gb.gbtvc = (double?)it.GBTVC;
                        Console.WriteLine("Update GB "+ctungay.ToString("dd/MM/yyyy").Substring(3)+"-" + it.CrewID);
                    }
                    giobay += (double)it.Total;
                }

                if (DateTime.Now.Day < 15) //Trước ngày 10 hàng tháng cập nhật của tháng trước
                {
                    ctungay = new DateTime(DateTime.Now.Date.AddMonths(-1).Year, DateTime.Now.Date.AddMonths(-1).Month, 1);



                    ctoingay = ctungay.AddMonths(1).AddDays(-1);
                    period = "SLR_" + ctungay.ToString("yyyyMMdd") + ctoingay.ToString("yyyyMMdd");
                    thang = ctungay.ToString("dd/MM/yyyy").Substring(3, 7);

                    SalFlytime = sal.t_SalaryLCBDetail
                                        .Where(x => x.c_PeriodTag == period && x.c_JobSalary != "POS" && x.c_JobSalary != "DEH" &&
                                                    (x.c_IsDeleted == false || x.c_IsDeleted == null))
                                        .GroupBy(x => x.c_CrewID)
                                        .Select(x => new
                                        {
                                            CrewID = x.Key,
                                            Total = x.Sum(y => y.c_FlyingMinsA),
                                            B787 = x.Sum(y => y.c_AircarftType == "787" ? y.c_FlyingMinsA : 0),
                                            A350 = x.Sum(y => y.c_AircarftType == "350" ? y.c_FlyingMinsA : 0),
                                            A321 = x.Sum(y => y.c_AircarftType == "321" ? y.c_FlyingMinsA : 0),
                                            GBTVT2 = x.Sum(y => y.c_JobSalary == "I" || y.c_JobSalary == "P" ? y.c_FlyingMinsA : 0),
                                            GBTVT1 = x.Sum(y => y.c_JobSalary == "P1" || y.c_JobSalary == "X" ? y.c_FlyingMinsA : 0),
                                            GBTVC = x.Sum(y => y.c_JobSalary == "B" ? y.c_FlyingMinsA : 0),
                                        })
                                        .ToList();
                    //Reset số liệu của tháng hiện tại về zero để tránh lỗi dữ liệu
                    foreach (var it in db.HR_GBLuyke.Where(x => x.month == thang))
                    {
                        it.total = 0;
                        it.f_321 = 0;
                        it.f_350 = 0;
                        it.f_787 = 0;
                        it.gbtvt2 = 0;
                        it.gbtvt1 = 0;
                        it.gbtvc = 0;
                    }
                    //Cập nhật giờ bay
                    foreach (var it in SalFlytime)
                    {
                        var gb = db.HR_GBLuyke.Where(x => x.crewid == it.CrewID && x.month == thang).FirstOrDefault();
                        if (gb == null) //Chưa có thì tjeem vào
                        {
                            HR_GBLuyke gnnew = new HR_GBLuyke();
                            gnnew.month = thang;
                            gnnew.crewid = it.CrewID;
                            gnnew.total = (double?)it.Total;
                            gnnew.f_321 = (double?)it.A321;
                            gnnew.f_350 = (double?)it.A350;
                            gnnew.f_787 = (double?)it.B787;
                            gnnew.gbtvt2 = (double?)it.GBTVT2;
                            gnnew.gbtvt1 = (double?)it.GBTVT1;
                            gnnew.gbtvc = (double?)it.GBTVC;
                            db.HR_GBLuyke.Add(gnnew);
                            Console.WriteLine("Add GB " + ctungay.ToString("dd/MM/yyyy").Substring(3) + "-" + it.CrewID);
                        }
                        else //Có rồi thì cập nhật
                        {
                            gb.total = (double?)it.Total;
                            gb.f_321 = (double?)it.A321;
                            gb.f_350 = (double?)it.A350;
                            gb.f_787 = (double?)it.B787;
                            gb.gbtvt2 = (double?)it.GBTVT2;
                            gb.gbtvt1 = (double?)it.GBTVT1;
                            gb.gbtvc = (double?)it.GBTVC;
                            Console.WriteLine("Update GB " + ctungay.ToString("dd/MM/yyyy").Substring(3) + "-" + it.CrewID);
                        }
                        giobay += (double)it.Total;
                    }
                }

            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update GB Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Date GB Error:" +ex.Message + "\n\r" + ex.InnerException);

            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update GB " + thang + " Total=" + giobay.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update GB Complate!");
                }
                writetext.Close();
                if(db!=null)
                    db.Dispose();
                if(sal!=null)
                    sal.Dispose();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update GB Complete!");
            }

        }

        private static void Update_Sys_Acc()
        {
            string filelog = @"c:\temp\SysAcc_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            int numAdd = 0, numUpdate = 0,kk;
            Dictionary<string, int> bangcap = new Dictionary<string, int>();
            bangcap.Add("không rõ", 0);
            bangcap.Add("ptth", 1);
            bangcap.Add("công nhân kỹ thuật", 1);
            bangcap.Add("dưới đại học", 1);
            bangcap.Add("nghề", 1);
            bangcap.Add("trung cấp", 2);
            bangcap.Add("cao đẳng", 3);
            bangcap.Add("đại học", 4);
            bangcap.Add("thạc sĩ", 5);
            bangcap.Add("thạc sỹ", 5);
            bangcap.Add("tiến sĩ", 6);
            bangcap.Add("tiến sỹ", 6);
            bangcap.Add("", -1);
            RedantEntities hr = null;
            ERMSEntities   db = null;

            try
            {
                db = new ERMSEntities();
                hr = new RedantEntities();                

                string hocvan = "", ghichu = "", chuyenmon = "", loainhansu = "",trinhdo="",bophan="";
                foreach(var mail in db.HR_Mail.Where(x => x.Status != "XX" ))
                {
                    var acc = db.Sys_Account.Where(x => x.CrewID == mail.manv).FirstOrDefault();
                    var hs = hr.HoSoGocs.Where(x => x.mans == mail.manv).FirstOrDefault();
                    if (mail.manv == "6641")
                        kk = 0;
                    if (hs == null || hs.nghiviec == true) //Không có trong nhân sự hoặc đã nghỉ việc bỏ qua không cập nhật
                        continue;

                    

                    var chucdanhtv = hr.USP_Get_Chucdanh_Momment(acc.CrewID, DateTime.Now.Date).FirstOrDefault();
                    var tienganh = hr.USP_Get_Ngoaingu_Moinhat(acc.CrewID, "EN").FirstOrDefault();
                    var danhsachcm = hr.nhomchuyenmons.Where(x => x.HoSoGoc.id == hs.id).ToList();
                    var loains = hr.danhmucs.Where(x => x.id == hs.loains).FirstOrDefault();

                    chuyenmon="";
                    foreach(var cm in danhsachcm)
                    {
                        var dm = hr.danhmucs.Where(x => x.id == cm.chuyenmon).FirstOrDefault();
                        if (bangcap[chuyenmon.ToLower()] < bangcap[dm.TenDanhMuc.Trim().ToLower()])
                            chuyenmon = dm.TenDanhMuc.Trim();
                    }


                    if (hs.hocvantd > 0)
                    {
                        var dm = hr.danhmucs.Where(x => x.id == hs.hocvantd).FirstOrDefault();
                        if (dm != null)
                            hocvan = dm.TenDanhMuc;
                        else
                            hocvan = "";
                    }
                    else
                        hocvan = "";
                    
                    trinhdo = bangcap[hocvan.ToLower()] < bangcap[chuyenmon.ToLower()] ? chuyenmon : hocvan;                    
                    loainhansu = loains == null ? "" : loains.TenDanhMuc == "Tiếp Viên" ? "VNA" : loains.TenDanhMuc == "Mặt đất" ? "NVMD" : loains.TenDanhMuc == "Người nước ngoài" ? "NN" : loains.TenDanhMuc == "Tiếp viên thời vụ" ? "ALS" : loains.TenDanhMuc == "Tiếp viên K6" ? "TVK6" : loains.TenDanhMuc == "Học viên" ? "HVTV" : loains.TenDanhMuc == "Cộng tác viên (Ban ATCL-AN)" ? "SQD" : loains.TenDanhMuc == "Cộng tác viên (TTHL)" ? "FTC" : loains.TenDanhMuc == "Cộng tác viên" ? "TCT" : loains.TenDanhMuc == "Cộng tác viên (VASC)" ? "VAS" : loains.TenDanhMuc == "Cộng tác viên (Ban DVHK)" ?"DVHK": loains.TenDanhMuc;
                    ghichu = loains == null ? "" : loains.TenDanhMuc == "Tiếp Viên" ? "1" : loains.TenDanhMuc == "Mặt đất" ? "2" : loains.TenDanhMuc == "Người nước ngoài" ? "4" : loains.TenDanhMuc == "Tiếp viên thời vụ" ? "7" : loains.TenDanhMuc == "Tiếp viên K6" ? "8" : loains.TenDanhMuc == "Học viên" ? "5" : loains.TenDanhMuc == "Cộng tác viên (Ban ATCL-AN)" ? "3b" : loains.TenDanhMuc == "Cộng tác viên (TTHL)" ? "3a" : loains.TenDanhMuc == "Cộng tác viên" ? "3" : loains.TenDanhMuc == "Cộng tác viên (VASC)" ? "3c" : loains.TenDanhMuc == "Cộng tác viên (Ban DVHK)" ? "3e" : loains.TenDanhMuc;

                    


                    if (acc == null)
                    {
                        acc = new Sys_Account();
                        acc.CrewID = mail.manv;
                        acc.Code_tv = mail.manv;
                        acc.Account = mail.mail.Substring(0, mail.mail.IndexOf("@"));
                        acc.Email = mail.mail;
                        acc.Phone = mail.phone;
                        acc.pag_no = mail.phone;
                        acc.IsCrew = true;
                        acc.IsDeleted = false;
                        acc.Created = DateTime.Now;

                        if (hs != null)
                        {
                            acc.FirstNameVn = hs.Tenkd.ToUpper();
                            acc.LastNameVn = hs.tenkodau.ToUpper().Substring(0, hs.tenkodau.ToUpper().LastIndexOf(hs.Tenkd.ToUpper()));
                            acc.name_tv = hs.tenkodau.ToUpper();
                            acc.name = hs.Tenkd.ToUpper();
                            acc.man = hs.gioitinh == 2536 ? true : false;
                            acc.dob = hs.ngaysinh.Date;
                            acc.start_date = hs.bienche_tct != null ? hs.bienche_tct : hs.bienche_dtv;
                        }
                        //Cập nhật chức danh
                        if (chucdanhtv != null)
                        {
                            acc.type_tv = chucdanhtv.chucdanhtv == "TVTb2" ? "P" : chucdanhtv.chucdanhtv == "TVTb1" ? "X" : chucdanhtv.chucdanhtv == "TVC" ? "B" : "Y";
                        }
                        //Cập nhật tiếng Anh
                        if (tienganh != null)
                        {
                            acc.EngName = tienganh.Bangcap;
                            acc.EngScore = tienganh.tong;
                            acc.EngValid = tienganh.ngaycap;                            
                        }
                        //Cập nhật trình độ học vấn, loại nhân sự và ghi chú
                        acc.Education = trinhdo;
                        acc.Employer = loainhansu;
                        acc.Note = ghichu;
                        

                        numAdd++;
                        db.Sys_Account.Add(acc);
                        Console.WriteLine("Add " + mail.manv +" to sys_account");
                    }
                    else //Có trong sys_acc thì cập nhật các thông tin Bộ phận, chức danh, Ngoại ngữ, ngày vào ngành. Từ 17/06/2021 cập nhật thêm tên phòng trường hợp đổi tên.
                    {
                        //Cập nhật chức danh
                        if (chucdanhtv != null)
                        {
                            acc.type_tv = chucdanhtv.chucdanhtv == "TVTb2" ? "P" : chucdanhtv.chucdanhtv == "TVTb1" ? "X" : chucdanhtv.chucdanhtv == "TVC" ? "B" : "Y";
                        }
                        //Cập nhật tiếng Anh
                        if (tienganh != null)
                        {
                            acc.EngName = tienganh.Bangcap;
                            acc.EngScore = tienganh.tong;
                            acc.EngValid = tienganh.ngaycap;
                        }
                        //Cập nhật trình độ học vấn, loại nhân sự và ghi chú
                        acc.Education = trinhdo;
                        acc.Employer = loainhansu;
                        acc.Note = ghichu;
                        //Từ 17/06/2021
                        if (hs != null)
                        {
                            acc.FirstNameVn = hs.Tenkd.ToUpper();
                            acc.LastNameVn = hs.tenkodau.ToUpper().Substring(0, hs.tenkodau.ToUpper().LastIndexOf(hs.Tenkd.ToUpper()));
                            acc.name_tv = hs.tenkodau.ToUpper();
                            acc.name = hs.Tenkd.ToUpper();
                            acc.man = hs.gioitinh == 2536 ? true : false;
                            acc.dob = hs.ngaysinh.Date;
                            acc.start_date = hs.bienche_tct != null ? hs.bienche_tct : hs.bienche_dtv;
                        }

                        numUpdate++;
                        Console.WriteLine("Update " + mail.manv + " trong sys_account");
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Sys Account Error: " + ex.Message + "\n\r" + ex.InnerException);

                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Sys Account Error: "+ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Sys_Acc Add: " + numAdd.ToString() + " Update: " + numUpdate.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Sys Account Complete!");
                }
                writetext.Close();
                if(hr!=null)
                    hr.Dispose();
                if(db!=null)
                    db.Dispose();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Sys Account Complete!");
            }
        }

        private static void Update_Dang()
        {
            string filelog = @"c:\temp\Dang_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            ERMSEntities db = null;
            RedantEntities hr = null;
            int numAdd = 0,numUpdate=0;
            try
            {
                db = new ERMSEntities();
                hr = new RedantEntities();
                var lstdv = hr.HoSoGocs.Where(x => x.dangvien == true && (x.nghiviec == null || x.nghiviec == false)).ToList();
                foreach (var it in lstdv)
                {
                    var chibo = hr.danhmucs.Where(x => x.id == it.dang_chibo).FirstOrDefault();
                    var cvdang = hr.danhmucs.Where(x => x.id == it.dang_chucvu).FirstOrDefault();
                    var dv = db.HR_Party.Where(x => x.CrewID == it.mans).FirstOrDefault();
                    if (dv == null)
                    {
                        HR_Party dangvien = new HR_Party();
                        dangvien.CrewID = it.mans;
                        dangvien.Ngayvao = it.dang_ngaykn;
                        dangvien.Ngaychinhthuc = it.dang_ngaychuyen;
                        dangvien.Chibo = chibo == null ? "" : chibo.TenDanhMuc;
                        dangvien.Chucvu = cvdang == null ? "" : cvdang.TenDanhMuc;
                        db.HR_Party.Add(dangvien);
                        numAdd++;
                    }
                    else
                    {
                        dv.Ngaychinhthuc = it.dang_ngaychuyen;
                        dv.Chibo = chibo == null ? "" : chibo.TenDanhMuc;
                        dv.Chucvu = cvdang == null ? "" : cvdang.TenDanhMuc;
                        numUpdate++;
                    }
                    Console.WriteLine("Cập nhật Đảng {0}", it.mans);
                }

            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Dang Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Dang Error: "+ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Cap nhat Dang Add:" + numAdd.ToString() + " Update: " + numUpdate.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Dang Complete!");
                }
                if(db!=null)
                    db.Dispose();
                if(hr!=null)
                    hr.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Dang Complete!");
            }
        }

        private static void Update_Group_Old()
        {
            string filelog = @"c:\temp\Group_log.txt";
            string ldtv = "1;2;3;4;5;6";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            bool isError = false;
            ERMSEntities db = null;
            NotesSession session = new NotesSession();
            NotesDatabase dbase;
            NotesView view;
            NotesDocument doc;
            int numUpdate = 0;

            try
            {
                db = new ERMSEntities();
                session.Initialize("btliem");
                dbase = session.GetDatabase("domino.dev/DTV", "Nhansu\\qlns.nsf");
                view = dbase.GetView("Nhan su\\Theo ma so");
                string bophan = "";
                foreach(var item in db.Sys_Account.Where(x=>x.IsCrew==true && x.end_date == null))
                {


                    doc = view.GetDocumentByKey(item.CrewID);
                    bophan = "";
                    if (doc != null)
                    {
                        bophan = doc.GetItemValue("Bophan")[0];
                        bophan = bophan.Trim().Replace("TMP","").Replace(".TMP","");

                        bophan = bophan == "LDAO" ? "LDAO" : bophan == "" ? "TRN" : bophan == "VASCO" ? "VAS" : bophan.Substring(0, 2) != "LD" ? bophan :
                            (bophan.Substring(2).Contains("CXR") || bophan.Substring(2).Contains("DAD")) && !bophan.Substring(2).Contains(".") ? bophan.Substring(2, 1) + "." + bophan.Substring(3) : bophan.Substring(2);

                        if (bophan.Substring(1, 1) != "." && ldtv.Contains(bophan.Substring(0, 1)))
                            bophan = bophan.Substring(0, 1);
                        if (bophan.Substring(bophan.Length-1,1) == ".")
                            bophan = bophan.Substring(0,bophan.Length-1);

                        item.Group = bophan;
                        numUpdate++;
                    }

                    Console.WriteLine("Cập nhật Group {0}", item.CrewID);
                }

            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine("Error: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Cap nhat Group:" + numUpdate.ToString());
                    writetext.WriteLine("Complete!");
                }
                db.Dispose();
                
                writetext.Close();
                Console.WriteLine("Complete!");
            }
        }

        private static void Backup_doc()
        {
            if(DateTime.Now.Date.Day % 2 == 1) //Ngày lẻ
            {
                System.Diagnostics.Process.Start(@"C:\Users\admin\Desktop\Backupdtv - Chan.cmd");
            }
            else //Ngày chẵn
            {
                System.Diagnostics.Process.Start(@"C:\Users\admin\Desktop\Backupdtv - Le.cmd");
            }
        }

        private static void GetFPTBk()
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("administrator", "!@34567890Dtv");
            //client.DownloadFile("ftp://118.68.169.88/backup/model_backup_2021_06_04_000005_2214854.bak", @"\\10.105.2.240\BackupDB\IISData\\model_backup_2021_06_04_000005_2214854.bak");
            client.DownloadFile("ftp://118.68.169.88/backup/model_backup_2021_06_04_000005_2214854.bak", @"\\10.105.2.240\BackupDB\IISData\\model_backup_2021_06_04_000005_2214854.bak");
            //client.  
        }

        private static void update_THHDLD()
        {
            string filelog = @"c:\temp\THHDLD_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            ERMSEntities db = null;
            RedantEntities hr = null;
            string result = "";
            int numUpdate = 0, numAdd = 0;
            try
            {
                db = new ERMSEntities();
                hr = new RedantEntities();
                var lstTamhoan = db.CR_NoneFlight.Where(x =>(x.DecisionNo != null && x.DecisionNo != string.Empty) || (x.BreakDecision != "" && x.BreakDecision != null)).ToList();
                foreach (var item in lstTamhoan)
                {
                    if (item.CrewID == "3632")
                    {
                        int zzz = 1;
                    }
                    var nhanvien = hr.HoSoGocs.Where(x => x.mans.Trim() == item.CrewID).FirstOrDefault();
                    if (nhanvien == null)
                        continue;

                    string soqd = convertToUnSign(item.DecisionNo).Trim().ToLower();
                    string soqdcd = convertToUnSign(item.BreakDecision).Trim().ToLower();

                    var hdld = hr.qlttlaodongs.Where(x => x.id_ns == nhanvien.id).OrderByDescending(x => x.id).FirstOrDefault();
                    if (hdld == null)
                        continue;
                    //Các lần tạm hoãn
                    var its = hr.tamhoanhds.Where(x => x.id_ns == hdld.id && x.hoanhd_ngayky == item.SignedDate).ToList();

                    if (soqd != "")
                    {
                        //Xem cái đang xét đã có thêm vào chưa
                        var it = its.Where(x => x.hoanhd_soqd.Trim().ToLower() == soqd || convertToUnSign(x.hoanhd_soqd.Trim().ToLower()) == soqd).FirstOrDefault();
                        if (it == null) //Chưa có thì thêm vào
                        {
                            tamhoanhd tamhoan = new tamhoanhd();
                            tamhoan.id_ns = hdld.id;
                            tamhoan.hoanhd_soqd = soqd.ToUpper();
                            tamhoan.hoanhd_ngayky = item.SignedDate.Value;
                            tamhoan.hoanhd_tungay = item.From.Value;
                            tamhoan.hoanhd_denngay = item.To.Value;
                            //Phần ghi chú, nếu có ghi chú thfi xét, không thì cho lý do là 2589
                            if (item.Note != null && item.Note != string.Empty)
                            {
                                string note = item.Note.Replace("\r\n", "").Trim();
                                if (note.Length > 50)
                                {
                                    note = item.CR_NoneFlight_Category.Code;
                                }
                                var lydo = hr.danhmucs.Where(x => x.TenDanhMuc.Trim().ToLower() == note.ToLower() && x.LoaiDanhMuc == "lydohoanhd").FirstOrDefault();
                                if (lydo == null)
                                {
                                    danhmuc dm = new danhmuc();
                                    dm.LoaiDanhMuc = "lydohoanhd";
                                    dm.TenDanhMuc = note;
                                    dm.TinhTrang = true;
                                    dm.MaDanhMuc = "lydohoanhd";
                                    hr.danhmucs.Add(dm);
                                    hr.SaveChanges();
                                    lydo = hr.danhmucs.Where(x => x.TenDanhMuc.Trim().ToLower() == note.ToLower() && x.LoaiDanhMuc == "lydohoanhd").FirstOrDefault();
                                }
                                tamhoan.hoanhd_lydo = lydo.id;
                            }
                            else
                                tamhoan.hoanhd_lydo = 2589;
                            //Thêm vào tbQuyetdinh
                            var qdinh = hr.tbQuyetDinhs.Where(x => x.hd_loai_quyet_dinh == 3142 &&
                                                             (x.hd_so_quyet_dinh.Trim().ToLower() == soqd || x.hd_so_quyet_dinh.Trim().ToLower() == item.DecisionNo.Trim().ToLower()) &&
                                                             x.hd_ngay_ky_quyet_dinh == item.SignedDate).FirstOrDefault();
                            if (qdinh == null)
                            {
                                tbQuyetDinh qd = new tbQuyetDinh();
                                qd.hd_loai_quyet_dinh = 3142;
                                qd.hd_so_quyet_dinh = soqd.ToUpper();
                                qd.hd_ngay_ky_quyet_dinh = item.SignedDate.Value;
                                qd.hd_ngay_hieu_luc = item.From.Value;
                                qd.hd_ngay_het_hieu_luc = item.To.Value;
                                qd.hd_cap_ky_quyet_dinh = 368;
                                qd.hd_nguoi_ky_quyet_dinh = "";
                                qd.hd_tinh_trang = 1;
                                qd.hd_tom_tat_phu_luc_trong_quyet_dinh = "TH HDLD " + nhanvien.Tenkd;
                                qd.hd_ly_do_chinh = 370;
                                hr.tbQuyetDinhs.Add(qd);
                                hr.SaveChanges();
                                qdinh = hr.tbQuyetDinhs.Where(x => x.hd_loai_quyet_dinh == 3142 &&
                                                             (x.hd_so_quyet_dinh.Trim().ToLower() == soqd || x.hd_so_quyet_dinh.Trim().ToLower() == item.DecisionNo.Trim().ToLower()) &&
                                                             x.hd_ngay_ky_quyet_dinh == item.SignedDate).FirstOrDefault();
                            }

                            hr.tamhoanhds.Add(tamhoan);
                            hr.SaveChanges();
                            result = string.Format("Update Manv={0},Số QĐ={1},Ngày ký ={2},từ {3} - đến {4}", item.CrewID, item.DecisionNo, item.SignedDate.Value.ToString("dd/MM/yyyy"), item.From.Value.ToString("dd/MM/yyyy"), item.To.Value.ToString("dd/MM/yyyy"));
                            Console.WriteLine(result);
                            //writetext.WriteLine(result);
                            numAdd++;
                        }
                    }//soqd

                    if (soqdcd != "" && nhanvien.loains!=3542)
                    {
                        var dsqd = hr.tamhoanhds.Where(x => x.id_ns == hdld.id && x.hoanhd_ngayky == item.SignedDate).ToList();//có thể có nhiều Tạm hoãn HĐLĐ có cùng ngày ký.
                        var qdth = dsqd.Where(x => x.hoanhd_soqd.Trim().ToLower() == soqd || convertToUnSign(x.hoanhd_soqd.Trim().ToLower()) == soqd).FirstOrDefault();
                        if (qdth != null)//có tạm hoãn thì cập nhật thông tin chấm dứt tạm hoãn, không thì thôi
                        {
                            var qdinh = hr.tbQuyetDinhs.Where(x =>
                                                    (x.hd_so_quyet_dinh.Trim().ToLower() == soqdcd || x.hd_so_quyet_dinh.Trim().ToLower() == item.BreakDecision.Trim().ToLower()) &&
                                                    x.hd_ngay_ky_quyet_dinh == item.SignedDate).FirstOrDefault();
                            if (qdinh == null) //chưa có QĐ thì thêm vào
                            {
                                tbQuyetDinh qd = new tbQuyetDinh();
                                qd.hd_loai_quyet_dinh = 3144;
                                qd.hd_so_quyet_dinh = soqdcd.ToUpper();
                                qd.hd_ngay_ky_quyet_dinh = item.BreakSigned.Value;

                                qd.hd_ngay_hieu_luc = item.BreakDate.Value;
                                qd.hd_cap_ky_quyet_dinh = 368;
                                qd.hd_nguoi_ky_quyet_dinh = "";
                                qd.hd_tinh_trang = 1;
                                qd.hd_tom_tat_phu_luc_trong_quyet_dinh = "CD TH HDLD " + nhanvien.Tenkd;
                                qd.hd_ly_do_chinh = 370;
                                hr.tbQuyetDinhs.Add(qd);
                                hr.SaveChanges();
                                qdinh = hr.tbQuyetDinhs.Where(x => x.hd_loai_quyet_dinh == 3142 &&
                                                                (x.hd_so_quyet_dinh.Trim().ToLower() == soqd || x.hd_so_quyet_dinh.Trim().ToLower() == item.DecisionNo.Trim().ToLower()) &&
                                                                x.hd_ngay_ky_quyet_dinh == item.SignedDate).FirstOrDefault();
                            }
                            qdth.hoanhd_soqdcdhoan = soqdcd.ToUpper();
                            qdth.hoanhd_ngaykycdhoan = item.BreakSigned.Value;
                            qdth.hoanhd_ngaycdhoanhl = item.BreakDate.Value;
                            hr.SaveChanges();
                            result = string.Format("Update CDTH Manv={0},Số QĐ={1},Ngày ký ={2},từ {3} - đến {4}", item.CrewID, item.BreakDecision, item.BreakSigned.Value.ToString("dd/MM/yyyy"), item.From.Value.ToString("dd/MM/yyyy"), item.BreakDate.Value.ToString("dd/MM/yyyy"));
                            Console.WriteLine(result);
                            //writetext.WriteLine(result);
                            numUpdate++;
                        }
                    }

            }
        }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update THHDLD Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Update THHDLD Error: "+ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "THHD:" + numAdd.ToString() + "; CDTH: " + numUpdate.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update THHDLD Complete!");
                }
                if(db!=null)
                    db.Dispose();
                if(hr!=null)
                    hr.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update THHD Complete!");
            }
        }

        private static void update_RouteComm()
        {
            string filelog = @"c:\temp\CommRoute_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false;
            ERMSEntities db = null;
            DateTime startdate = DateTime.Now.Date.AddDays(-50);
            string listbase = "SGN*HAN*DAD*CXR";
            int numAdd = 0;
            try
            {
                db = new ERMSEntities();
                var RouteIntl = db.CR_FlightInfo.Where(x => x.Departed >= startdate && (x.IsDeleted == null || x.IsDeleted == false) && (listbase.Contains(x.Routing.Substring(0, 3)))).ToList();
                foreach(var item in RouteIntl)
                {
                    var country = db.CR_TimeZone.Where(x => x.Code == item.Routing.Substring(4, 3)).FirstOrDefault();
                    if (country != null)
                    {
                        if (country.Country.Trim().ToLower() == "vietnam")
                            continue;
                        else
                        {
                            var commRoute = db.CR_Flight_CoviComRoute.Where(x => x.Origin + "-" + x.Destination == item.Routing).FirstOrDefault();
                            if (commRoute == null)
                            {
                                CR_Flight_CoviComRoute comm = new CR_Flight_CoviComRoute();
                                comm.Origin = item.Routing.Substring(0, 3);
                                comm.Destination = item.Routing.Substring(4, 3);
                                comm.Note = country.Country;
                                comm.Created = DateTime.Now;
                                db.CR_Flight_CoviComRoute.Add(comm);
                                numAdd++;
                            }
                        }
                        
                    }
                    else
                    {
                        writetext.WriteLine("TimeZone không có sân bay "+ item.Routing.Substring(4, 3));
                    }
                    
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update RouteCOMM Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Route COMM Error: "+ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Add " + numAdd.ToString() + " routing");
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Route COMM Complete!");
                }
                writetext.Close();
                if(db!=null)
                    db.Dispose();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update RouteComm Complete!");
            }
        }
        private static void Contract2RedAnt()
        {
            //Lấy cái HĐLĐ có Created hoặc Modified trong vòng 7 ngày so với ngày hiện tại để xem xét chuyển sang ReadAnt
            string filelog = @"c:\temp\Contract_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Contract Start!");
            writetext.Close();
            writetext = new StreamWriter(filelog, true);
            bool isError = false,isModify=false;
            ERMSEntities db = null;
            RedantEntities hr = null;
            string result = "",sohd="";
            int numUpdate = 0, numAdd = 0, numDelete=0,i=1;
            DateTime date = DateTime.Now.Date.AddDays(-30);
            DateTime tmpTo;
            DateTime BeginDate = new DateTime(2022, 7, 19);
            ////todo: Soát cập nhật HĐ active (x.ValidTo!=null || (x.ContractTypeID!=3 && x.ContractTypeID!=5)) &&
            try
            {
                db = new ERMSEntities();
                hr = new RedantEntities();
                var listHDLD = db.HR_Contract.Where(x =>  (x.Created >= date && x.Created > BeginDate) || (x.Modified >= date && x.Modified>BeginDate)).ToList();

                foreach (var item in listHDLD) //x.CrewID!="5694"
                {
                    
                    isModify = false;
                    var hoso = hr.HoSoGocs.Where(x => x.mans == item.CrewID).FirstOrDefault();
                    if (item.ValidTo == null)
                    {
                        if ((item.ContractTypeID == 3 || item.ContractTypeID == 5) && item.TerminationDate==null) //HĐ cập nhật NS mới nên kg lấy qua Kiến Đỏ.
                        {
                            continue;
                        }
                        else
                        {
                            if (item.TerminationDate != null) //Nghỉ việc    
                                tmpTo = item.TerminationDate.Value.Date;
                            else //Còn làm thì lấy ngày sinh + 65 năm. Coi là làm đến 65 tuổi nghỉ, không phân biệt nam nữ
                                tmpTo = hoso.ngaysinh.AddYears(65);
                        }
                    }
                    else
                    {
                        tmpTo = item.ValidTo.Value.Date;
                    }
                    var cat = db.HR_Contract_Category.Where(x => x.ID == item.ContractTypeID).FirstOrDefault();
                   
                    var hdldRA = hr.qlttlaodongs.Where(x => x.id_ns == hoso.id && x.hd_ngayhieuluc==item.ValidFrom).FirstOrDefault();

                    //Trên Kiến đỏ số HĐ chỉ cho 20 ký tự nên cần xử lý trước
                    sohd = item.ContractNo == null ? "" : item.ContractNo;
                    if(sohd.Length>20)
                    {
                        sohd = sohd.Replace("ALSIMEXCO", "ALS").Replace(" ", "");
                        sohd = sohd.Length > 20 ? sohd.Substring(0, 20) : sohd;
                    }
                    else
                    {
                        sohd = item.ContractNo == null ? "" : item.ContractNo;
                    }

                    

                    if(hdldRA==null) //Chưa có--> thêm mới
                    {
                        qlttlaodong newHD = new qlttlaodong();
                        newHD.id_ns = hoso.id;
                        newHD.hd_loai = cat.OID.Value;
                        newHD.hd_ngayhieuluc = item.ValidFrom.Value;
                        newHD.hd_ngayhet = tmpTo;
                        newHD.hd_ngaykyhd = item.SignedDate.Value;
                        newHD.hd_sohd = sohd;
                        hr.qlttlaodongs.Add(newHD);
                        writetext.WriteLine("Add Contract \t CrewID=" + item.CrewID + "\t Loại=" + cat.ContractName + "\t Từ =" + item.ValidFrom.Value.Date.ToString("dd/MM/yyyy") + "\t Đến =" + tmpTo.ToString("dd/MM/yyyy") + "\t Ký=" + item.SignedDate.Value.ToString("dd/MM/yyyy") + "\t Số=" + item.ContractNo);
                        numAdd++;
                        isModify = true;
                    }
                    else //Có rồi cập nhật thông tin
                    {
                        if (item.TerminationDate == null)
                        {
                            if(hdldRA.hd_loai != cat.OID.Value || hdldRA.hd_ngayhet != item.ValidTo.Value || hdldRA.hd_ngaykyhd != item.SignedDate.Value || hdldRA.hd_sohd !=sohd)
                            {
                                writetext.WriteLine("Update Contract \t CrewID=" + item.CrewID + "\t Loại O="+hdldRA.hd_loai.ToString()+" N=" + cat.ContractName 
                                                    + "\t Từ O ="+hdldRA.hd_ngayhieuluc.ToString("dd/MM/yyyy")+" N=" + item.ValidFrom.Value.ToString("dd/MM/yyyy") 
                                                    + "\t Đến O="+ (hdldRA.hd_ngaychamdut==null?"":hdldRA.hd_ngaychamdut.Value.ToString("dd/MM/yyyy"))+" N=" + item.ValidTo.Value.ToString("dd/MM/yyyy") 
                                                    + "\t Ký O="+ (hdldRA.hd_ngaykyhd==null?"":hdldRA.hd_ngaykyhd.ToString("dd/MM/yyyy"))+" N="+(item.SignedDate.HasValue ? item.SignedDate.Value.ToString("dd/MM/yyyy") : "") 
                                                    + "\t Số O="+(hdldRA.hd_sohd==null?"":hdldRA.hd_sohd)+" N=" + (item.ContractNo == null ? "" : item.ContractNo));
                                hdldRA.hd_loai = cat.OID.Value;
                                hdldRA.hd_ngayhet = tmpTo;
                                hdldRA.hd_ngaykyhd = item.SignedDate.Value;
                                hdldRA.hd_sohd = sohd;                                
                               
                            }
                            hoso.nghiviec = false;
                            hdldRA.hd_ngaychamdut = null;
                            hdldRA.hd_ngayhlchamduthd = null;
                            hdldRA.hd_lydochamduthd = null;
                            numUpdate++;
                            isModify = true;
                        }
                        else //Nghỉ việc
                        {
                            if (hoso.nghiviec != true)
                            {
                                var stop = db.HR_Contract_Termination.Where(x => x.ID == item.TerminationID).FirstOrDefault();
                                if (stop.OID == null)
                                {
                                    string newnhom = stop.ID.ToString();
                                    danhmuc dm = new danhmuc();
                                    dm.LoaiDanhMuc = "loaihopdong";
                                    dm.MaDanhMuc = "L";
                                    dm.TenDanhMuc = stop.Reason;
                                    dm.TinhTrang = true;
                                    dm.nhom = stop.ID.ToString();
                                    hr.danhmucs.Add(dm);
                                    hr.SaveChanges();
                                    var newdm = hr.danhmucs.Where(x => x.nhom == newnhom).FirstOrDefault();
                                    stop.OID = newdm.id;
                                    db.SaveChanges();
                                }
                                hoso.nghiviec = true;
                                hdldRA.hd_ngaychamdut = item.TerminationDate.Value;
                                hdldRA.hd_ngayhlchamduthd = item.TerminationDate.Value;
                                hdldRA.hd_lydochamduthd = stop.OID.Value;
                                numDelete++;
                                writetext.WriteLine("Termination Contract \t CrewID=" + item.CrewID + "\t Loại=" + cat.ContractName + "\t Từ =" + item.ValidFrom.Value.ToString("dd/MM/yyyy") + "\t Đến =" + tmpTo.ToString("dd/MM/yyyy") + "\t Ký=" + (item.SignedDate.HasValue ? item.SignedDate.Value.ToString("dd/MM/yyyy") : "") + "\t Số=" + (item.ContractNo == null ? "" : item.ContractNo) + "\t Termination Date=" + (item.TerminationDate.HasValue ? item.TerminationDate.Value.ToString("dd/MM/yyyy") : "") + "\t Lý do=" + stop.Reason);
                                isModify = true;
                            }
                            else //Các trường hợp nghỉ rồi quay lại
                            {
                                //todo: soát chỗ này
                            }
                        }
                    }
                    //if (isModify)
                    //    hr.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Contract2RedAnt Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "Update Contract2RedAnt Error: " + ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (!isError)
                {

                    hr.SaveChanges();
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\t Add: " + numAdd.ToString() + "; Update: " + numUpdate.ToString() + "; Delete: " + numDelete.ToString());
                    writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Contract2RedAnt Complete!");
                }
                if(db!=null)
                    db.Dispose();
                if(hr!=null)
                    hr.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Contract2RedAnt Complete!");
            }
        }
        private static void Contract2Lotus()
        {
            //Lấy cái HĐLĐ có Created hoặc Modified trong vòng 7 ngày so với ngày hiện tại để xem xét chuyển sang ReadAnt
            string filelog = @"c:\temp\Contract2Lotus_log.txt";
            if (FileInUse(filelog))
                return;
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Update Contract 2 Lotus Start!");
            bool isError = false;
            ERMSEntities db = null;
            RedantEntities hr = null;
            string result = "";
            int numUpdate = 0, numAdd = 0, numDelete = 0;
            DateTime date = DateTime.Now.Date.AddDays(-7);
            DateTime BeginDate = new DateTime(2022, 7, 19);
            NotesSession session;
            NotesDatabase dbase;
            NotesView view;
            NotesDocument doc;
            try
            {
                session= new NotesSession();
                session.Initialize("btliem");
                dbase = session.GetDatabase("domino.dev/DTV", "Nhansu\\qlns.nsf");
                view = dbase.GetView("Nhan su\\Theo ma so");

                db = new ERMSEntities();
                hr = new RedantEntities();
                var listHDLD = db.HR_Contract.Where(x => (x.Created >= date && x.Created > BeginDate) || (x.Modified >= date && x.Modified > BeginDate)).ToList();

                foreach (var item in listHDLD)
                {
                    doc = view.GetDocumentByKey(item.CrewID);
                    if (doc != null)
                    {
                        doc.ReplaceItemValue("","");
                        doc.Save(true,true);
                    }
                    else
                    {
                        writetext.WriteLine("CrewID="+item.CrewID+" cannot Found!");
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public static string convertToUnSign(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static void SyncFPT()
        {
            string filelog = @"c:\temp\SyncFPT.txt";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string pathDst = @"\\10.105.2.240\BackupDB\IISData\";
            string tmp = "";
            string download = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string delete = DateTime.Now.Date.ToString("yyyy_MM_dd");
            List<string> filedelete = new List<string>();
            List<string> filedownload = new List<string>();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.1.10.87:22/backup");
            try
            {
                request.Credentials = new NetworkCredential("vna_admin", "Admin12345");
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, true))
                        {
                            while (!reader.EndOfStream)
                            {
                                tmp = reader.ReadLine();
                                if (tmp.Contains(download))
                                {
                                    filedownload.Add(tmp);

                                }
                                if (tmp.Contains(delete))
                                {
                                    filedelete.Add(tmp);
                                }
                            }
                        }
                    }
                }
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " List completed!");
                //Download file
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential("vna_admin", "Admin12345");
                foreach (var item in filedownload)
                {
                    string fileSrc = "ftp://10.1.10.87:22/backup/" + item;
                    ftpClient.DownloadFile(fileSrc, pathDst + "\\" + item);
                }
                ftpClient.Dispose();
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Download completed!");
                //Remove
                foreach (var item in filedelete)
                {
                    string serverUri = "ftp://10.1.10.87:22/backup/" + item;
                    request = (FtpWebRequest)WebRequest.Create(serverUri);
                    request.Credentials = new NetworkCredential("vna_admin", "Admin12345");
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    response.Dispose();
                }
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Remove completed!");
        }
            catch (Exception ex)
            {
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Sync FPT Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Sync FPT Error: "+ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Sync FPT Complete!");
            }
        }

        private static void SendUpdateMailGroup()
        {
            string bodytext = "", pass = "";
            
            int soacc = 0;
            string filelog = @"c:\temp\SendUpdateMailLog.txt";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            ERMSEntities db = null;
            try
            {
                var hfile = File.ReadAllLines(@"D:\OneDrive\Private Document\Cac account\liembt.TXT");
                pass = hfile[0].Trim();
                db = new ERMSEntities();
                var mailchangegroup = db.USP_HR_UpdateMailGroup().ToList();
                if (mailchangegroup.Count > 0)
                {
                    string filename = @"F:\Cac van de ve Mail TCT\Group mail Jun2018\Cạp nhat group mail DTV - " + DateTime.Now.Date.ToString("ddMMMyyyy") + ".xlsx";
                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
                    service.Credentials = new WebCredentials("liembt@vietnamairlines.com", pass);
                    service.Url = new Uri("https://mail.vietnamairlines.com/ews/exchange.asmx");
                    service.TraceFlags = TraceFlags.All;
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlBook = xlApp.Workbooks.Add();
                    xlBook.Worksheets.Add();
                    Excel.Worksheet xlAdd=null, xlRemove=null;
                    int iAdd = 1, iRemove = 1,num_o_Add=0,num_o_Remove=0;
                    var Add = mailchangegroup.Where(x => x.Note == "Add").Count();
                    if (Add > 0)
                    {
                        xlAdd = xlBook.Worksheets[1];
                        xlAdd.Name = "Add";
                        xlAdd.Cells[iAdd, 1].Value = "Account bổ sung";
                        xlAdd.Cells[iAdd, 2].Value = "Tên group";
                        iAdd++;
                    }
                    var Remove = mailchangegroup.Where(x => x.Note == "Remove").Count();
                    if (Remove > 0)
                    {
                        xlRemove = xlBook.Worksheets[2];
                        xlRemove.Name = "Remove";
                        xlRemove.Cells[iRemove, 1].Value = "Account loại bỏ";
                        xlRemove.Cells[iRemove, 2].Value = "Tên group";
                        iRemove++;
                    }

                    var listgroup = mailchangegroup.Select(x => x.GroupName).Distinct().ToList();
                    foreach (var groupname in listgroup)
                    {
                        var changeitemingroup = mailchangegroup.Where(x => x.GroupName == groupname).ToList();
                        //Lấy danh sách tài khoản trong GroupName hiện tại trên hệ thống.
                        //Nếu là Add kiểm tra trên danh sách có chưa? Nếu chưa thì đưa vào file đề nghị Add. Cập nhật Database
                        //Nếu là Remove kiểm tra trên danh sách có không? Nếu có thì đưa vào file đề nghị Remove. Cập nhật Database
                        var listmails = service.ExpandGroup(groupname);
                        foreach (var item in changeitemingroup)
                        {
                            if (item.Note == "Add")
                            {
                                HR_MailinGroup it = new HR_MailinGroup();
                                it.GroupName = groupname;
                                it.MailAccount = item.Account;
                                it.isDeleted = false;
                                it.Created = DateTime.Now;
                                it.Creator = "liembt";
                                it.Note = "";
                                var ExistItem = listmails.Where(x => x.Address.Trim().ToLower() == item.Mail.Trim().ToLower()).FirstOrDefault();
                                if (ExistItem == null)
                                {
                                    xlAdd.Cells[iAdd, 1].Value = item.Mail;
                                    xlAdd.Cells[iAdd, 2].Value = groupname;
                                    iAdd++;
                                    num_o_Add++;
                                }
                                else
                                    it.Note = "Account " + item.Account + " đã có trong group " + groupname + ". Nên không đề nghị Add";

                                db.HR_MailinGroup.Add(it);
                            }
                            else //Remove
                            {
                                var it = db.HR_MailinGroup.Where(x => x.GroupName == groupname && x.MailAccount == item.Account).FirstOrDefault();
                                if (it != null)
                                {
                                    var ExistItem = listmails.Where(x => x.Address.Trim().ToLower() == item.Mail.Trim().ToLower()).FirstOrDefault();
                                    if (ExistItem != null)
                                    {
                                        xlRemove.Cells[iRemove, 1].Value = item.Mail;
                                        xlRemove.Cells[iRemove, 2].Value = groupname;
                                        iRemove++;
                                        num_o_Remove++;
                                    }
                                    else
                                        it.Note = "Account " + item.Account + " không có trong group " + groupname + ". Nên không đề nghị Remove";

                                    it.isDeleted = true;
                                    it.Modified = DateTime.Now;
                                    it.Modifier = "liembt";
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                    if (num_o_Add == 0)
                        xlAdd.Delete();
                    if (num_o_Remove == 0)
                        xlRemove.Delete();

                    xlBook.SaveAs(filename);
                    xlBook.Close();
                    xlAdd = null;
                    xlRemove = null;
                    xlBook = null;
                    xlAdd = null;

                    //Tạo mail để gửi
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "V/v Cập nhật group mail ĐTV (" + DateTime.Now.Date.ToString("ddMMMyyyy") + ")";
                    message.ToRecipients.Add("helpdesk.it@vietnamairlines.com");
                    //message.CcRecipients.Add("vinhvu@vietnamairlines.com");
                    message.CcRecipients.Add("khanhhn@vietnamairlines.com");
                    message.BccRecipients.Add("liembt75@gmail.com");
                    message.Attachments.AddFileAttachment(filename);
                    bodytext = "<h5>Gửi AITS,</h5><br>AITS vui lòng cử nhân sự cập nhật group mail của Đoàn tiếp viên theo thông tin trong file đính kèm.";
                    bodytext += "<br><br><b>Trân trọng cảm ơn.</b><br>";
                    bodytext += "<br>Bùi Thanh Liêm";
                    bodytext += "<br>IT - Phòng Tổ chức Hành chính";
                    bodytext += "<br>Đoàn tiếp viên";
                    bodytext += "<br>108 Hồng Hà – P.2 – Q.Tân Bình – Tp.Hồ Chí Minh";
                    bodytext += "<br>Fax: (84-8) 3.8446.333";
                    bodytext += "<br>Mobile: (84) 913.146.309";

                    message.Body = new MessageBody(BodyType.HTML, bodytext);

                    message.SendAndSaveCopy();
                    //message.Save();
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " đề nghị Add " + num_o_Add.ToString()+" và Remove "+num_o_Remove.ToString() + " tài khoản.");
                }
            }
            catch(Exception ex)
            {
               
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Update Mail Group Error: " + ex.Message + "\n\r" + ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Update Mail Group  Error: " + ex.Message + "\n\r" + ex.InnerException);
            }
            finally
            {
                if (db != null)
                    db.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Update Mail Group Complete!");
            }
        }
        public static void SendMailRemoveAccount()
        {
            string bodytext = "",pass="",account="";
            bool isError = false;
            int soacc = 0;
            string filelog = @"c:\temp\SendMailRemoveAccount.txt";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            ERMSEntities db = null;
            try
            {
                var hfile = File.ReadAllLines(@"D:\OneDrive\Private Document\Cac account\liembt.TXT");
                pass=hfile[0].Trim();
                
                db = new ERMSEntities();
                DateTime curDate = DateTime.Now.Date;
                var removemails = db.HR_MailOpsLog.Where(x =>x.OpsID==4 && x.FromDate== curDate).ToList();
                if (removemails.Count > 0)
                {

                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
                    service.Credentials = new WebCredentials("liembt@vietnamairlines.com", pass);
                    service.Url = new Uri("https://mail.vietnamairlines.com/ews/exchange.asmx");
                    service.TraceFlags = TraceFlags.All;

                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "V/v Xóa tài khoản do người lao động nghỉ việc (" + DateTime.Now.Date.ToString("ddMMMyyyy") + ")";
                    message.ToRecipients.Add("helpdesk.it@vietnamairlines.com");                    
                    message.CcRecipients.Add("vinhvu@vietnamairlines.com");
                    message.CcRecipients.Add("butnt@vietnamairlines.com");
                    message.BccRecipients.Add("liembt75@gmail.com");

                    bodytext = "<h4>Gửi Ban CNTT,</h4><br>Ban CNTT vui lòng cho xóa tài khoản của người dùng bên dưới trên các hệ thống của TCT do người lao động đã nghỉ việc.";
                    bodytext += "<p/><table style='width: 100 %' border='1' style='border-collapse:collapse'><tr>";
                    bodytext += "<th>STT</th><th>Manv</th><th>Họ và Tên</th><th>Email</th></tr>";
                    //bodytext += "<tr>";
                    foreach(var it in removemails)
                    {
                        var email = db.HR_Mail.Where(x => x.ID == it.MailID).FirstOrDefault();
                        soacc++;
                        bodytext += "<tr><td>" + soacc.ToString() + "</td><td>" + email.manv + "</td><td>" + email.hodem.Trim()+" "+ email.ten.Trim() + "</td><td>" + email.mail + "</td></tr>";

                        email.Modified = DateTime.Now;
                        email.Status = "XX";
                        email.State = "Deleted";
                        email.ngaygiam = DateTime.Now.Date.ToString("dd/MM/yyyy");
                        //Xóa các group mà mail thuộc vào.
                        account = email.mail.Substring(0, email.mail.IndexOf("@"));
                        var mailgroup = db.HR_MailinGroup.Where(x => x.MailAccount.ToLower() == account.ToLower()).ToList();
                        foreach(var mig in mailgroup)
                        {
                            mig.isDeleted = true;
                            mig.Modified = DateTime.Now;
                            mig.Modifier = "liembt";
                            mig.Note = "Đề nghị xóa tài khoản nên hủy các group thuộc vào tương ứng";
                        }
                    }
                    

                    bodytext += "</table>";
                    bodytext += "<p/><b>Trân trọng cảm ơn.</b>";
                    bodytext += "<br>Bùi Thanh Liêm";
                    bodytext += "<br>IT - Phòng Tổ chức Hành chính";
                    bodytext += "<br>Đoàn tiếp viên";
                    bodytext += "<br>108 Hồng Hà – P.2 – Q.Tân Bình – Tp.Hồ Chí Minh";
                    bodytext += "<br>Fax: (84-8) 3.8446.333";
                    bodytext += "<br>Mobile: (84) 913.146.309 | www.crew.vn";

                    message.Body = new MessageBody(BodyType.HTML, bodytext);

                    message.SendAndSaveCopy();
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " đề nghị xóa "+soacc.ToString()+" tài khoản.");
                }
                else
                {
                    writetext.WriteLine("Ngày "+DateTime.Now.Date.ToString("dd/MM/yyyy")+" không có tài khoản nào cần xóa");
                }
            }
            catch(Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Remove Account Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Remove Account  Error: "+ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                }
                if(db!=null)
                    db.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Remove Account Complete!");
            }
        }
        public static void SendMailLockAccount()
        {
            string bodytext = "", pass = "";
            bool isError = false;
            int soacc = 0;
            string filelog = @"c:\temp\SendMailLockAccount.txt";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            ERMSEntities db = null;
            try
            {
                var hfile = File.ReadAllLines(@"D:\OneDrive\Private Document\Cac account\liembt.TXT");
                pass = hfile[0].Trim();

                db = new ERMSEntities();
                DateTime curDate = DateTime.Now.Date;
                var lockemails = db.HR_MailOpsLog.Where(x => x.OpsID==1 && x.FromDate== curDate).ToList();
                if (lockemails.Count > 0)
                {

                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
                    service.Credentials = new WebCredentials("liembt@vietnamairlines.com", pass);
                    service.Url = new Uri("https://mail.vietnamairlines.com/ews/exchange.asmx");
                    service.TraceFlags = TraceFlags.All;

                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "V/v Disable tài khoản người lao động (" + DateTime.Now.Date.ToString("ddMMMyyyy") + ")";
                    //message.ToRecipients.Add("liembt75@gmail.com");

                    message.ToRecipients.Add("helpdesk.aits@vietnamairlines.com");
                    message.CcRecipients.Add("helpdesk.it@vietnamairlines.com");
                    message.CcRecipients.Add("vinhvu@vietnamairlines.com");
                    message.BccRecipients.Add("liembt75@gmail.com");

                    bodytext = "<h4>Gửi AITS,</h4><br>AITS vui lòng Disable tài khoản của người dùng dưới đây.";
                    bodytext += "<p/><table style='width: 100 %' border='1' style='border-collapse:collapse'><tr>";
                    bodytext += "<th>STT</th><th>Manv</th><th>Họ và Tên</th><th>Email</th></tr>";
                    //bodytext += "<tr>";
                    foreach (var it in lockemails)
                    {
                        soacc++;
                        var email = db.HR_Mail.Where(x => x.ID == it.MailID).FirstOrDefault();
                        bodytext += "<tr><td>" + soacc.ToString() + "</td><td>" + email.manv + "</td><td>" + email.hodem.Trim() + " " + email.ten.Trim() + "</td><td>" + email.mail + "</td></tr>";

                        email.Modified = DateTime.Now;
                        email.State="Locked";                        
                    }


                    bodytext += "</table>";
                    bodytext += "<strong><p/><br>Trân trọng cảm ơn.";
                    bodytext += "<br>Bùi Thanh Liêm";
                    bodytext += "<br>IT - Phòng Tổ chức Hành chính";
                    bodytext += "<br>Đoàn tiếp viên";
                    bodytext += "<br>108 Hồng Hà – P.2 – Q.Tân Bình – Tp.Hồ Chí Minh";
                    bodytext += "<br>Fax: (84-8) 3.8446.333";
                    bodytext += "<br>Mobile: (84) 913.146.309 | www.crew.vn</strong>";

                    message.Body = new MessageBody(BodyType.HTML, bodytext);

                    message.SendAndSaveCopy();
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " đề nghị Disable " + soacc.ToString() + " tài khoản.");
                }
                else
                {
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " không có tài khoản nào cần Disable");
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Lock Account Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Lock Account Error: "+ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                }
                if (db != null)
                    db.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Lock Account Complete!");
            }
        }
        public static void SendMailUnlockAccount()
        {
            string bodytext = "", pass = "";
            bool isError = false;
            int soacc = 0;
            string filelog = @"c:\temp\SendMailUnlockAccount.txt";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            ERMSEntities db = null;
            try
            {
                var hfile = File.ReadAllLines(@"D:\OneDrive\Private Document\Cac account\liembt.TXT");
                pass = hfile[0].Trim();

                db = new ERMSEntities();
                DateTime curDate = DateTime.Now.Date;
                var Unlockemails = db.HR_MailOpsLog.Where(x => x.OpsID == 2 && x.FromDate == curDate).ToList();
                if (Unlockemails.Count > 0)
                {

                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
                    service.Credentials = new WebCredentials("liembt@vietnamairlines.com", pass);
                    service.Url = new Uri("https://mail.vietnamairlines.com/ews/exchange.asmx");
                    service.TraceFlags = TraceFlags.All;

                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "V/v Enable tài khoản người lao động (" + DateTime.Now.Date.ToString("ddMMMyyyy") + ")";


                    //message.ToRecipients.Add("liembt75@gmail.com");
                    message.ToRecipients.Add("helpdesk.aits@vietnamairlines.com");
                    message.CcRecipients.Add("helpdesk.it@vietnamairlines.com");
                    message.CcRecipients.Add("vinhvu@vietnamairlines.com");
                    message.BccRecipients.Add("liembt75@gmail.com");

                    bodytext = "<h4>Gửi AITS,</h4><p>AITS vui lòng Enable tài khoản của người dùng dưới đây.</p>";
                    bodytext += "<p/><table style='width: 100 %' border='1' style='border-collapse:collapse'><tr>";
                    bodytext += "<th>STT</th><th>Manv</th><th>Họ và Tên</th><th>Email</th></tr>";
                    //bodytext += "<tr>";
                    foreach (var it in Unlockemails)
                    {
                        soacc++;
                        var email = db.HR_Mail.Where(x => x.ID == it.MailID).FirstOrDefault();
                        bodytext += "<tr><td>" + soacc.ToString() + "</td><td>" + email.manv + "</td><td>" + email.hodem.Trim() + " " + email.ten.Trim() + "</td><td>" + email.mail + "</td></tr>";

                        email.Modified = DateTime.Now;
                        email.State = "";
                    }


                    bodytext += "</table>";
                    bodytext += "<strong><p/><br>Trân trọng cảm ơn.";
                    bodytext += "<br>Bùi Thanh Liêm";
                    bodytext += "<br>IT - Phòng Tổ chức Hành chính";
                    bodytext += "<br>Đoàn tiếp viên";
                    bodytext += "<br>108 Hồng Hà – P.2 – Q.Tân Bình – Tp.Hồ Chí Minh";
                    bodytext += "<br>Fax: (84-8) 3.8446.333";
                    bodytext += "<br>Mobile: (84) 913.146.309 | www.crew.vn</strong>";

                    message.Body = new MessageBody(BodyType.HTML, bodytext);

                    message.SendAndSaveCopy();
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " đề nghị Enable " + soacc.ToString() + " tài khoản.");
                }
                else
                {
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " không có tài khoản nào cần Enable");
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Unlock Account Error: " + ex.Message+"\n\r"+ex.InnerException);
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Unlock Account Error: "+ex.Message+"\n\r"+ex.InnerException);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                }
                if (db != null)
                    db.Dispose();
                writetext.Close();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Send Unlock Account Complete!");
            }
        }

        public static void SendMailResetPassword()
        {
            string bodytext = "", pass = "";
            bool isError = false;
            int soacc = 0;
            string filelog = @"c:\temp\Ressetpassword.txt";
            StreamWriter writetext = new StreamWriter(filelog, true);
            writetext.WriteLine("----------------------------------------------------------------------------------------");
            writetext.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " Start!");
            ERMSEntities db = null;
            try
            {
                var hfile = File.ReadAllLines(@"F:\Cac account\it.crew.TXT");
                pass = hfile[0].Trim();

                db = new ERMSEntities();
                DateTime curDate = DateTime.Now.Date;
                var resetpass = db.HR_MailOpsLog.Where(x => x.OpsID == 8 && x.FromDate == curDate).ToList();
                if (resetpass.Count > 0)
                {

                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
                    service.Credentials = new WebCredentials("it.crew@vietnamairlines.com", pass);
                    service.Url = new Uri("https://mail.vietnamairlines.com/ews/exchange.asmx");
                    service.TraceFlags = TraceFlags.All;

                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "V/v Reset password mail (" + DateTime.Now.Date.ToString("ddMMMyyyy") + ")";


                    //message.ToRecipients.Add("liembt75@gmail.com");
                    message.ToRecipients.Add("helpdesk.aits@vietnamairlines.com");                    
                    message.CcRecipients.Add("ninhhth@vietnamairlines.com");
                    message.BccRecipients.Add("liembt75@gmail.com");

                    bodytext = "<h4>Gửi AITS,</h4><p>AITS vui lòng reset password cho các tài khoản mail sau:.</p>";
                    bodytext += "<p/><table style='width: 100 %' border='1' style='border-collapse:collapse'><tr>";
                    bodytext += "<th>STT</th><th>Email</th></tr>";
                    //bodytext += "<tr>";
                    foreach (var it in resetpass)
                    {
                        soacc++;
                        var email = db.HR_Mail.Where(x => x.ID == it.MailID).FirstOrDefault();
                        bodytext += "<tr><td>" + soacc.ToString() + "</td><td>" + email.mail + "</td></tr>";
                        
                    }


                    bodytext += "</table>";
                    bodytext += "<strong><p/><br>Trân trọng cảm ơn.";
                    bodytext += "<br>Bùi Thanh Liêm";
                    bodytext += "<br>IT - Phòng Kế hoạch hành chính";
                    bodytext += "<br>Đoàn tiếp viên";
                    bodytext += "<br>108 Hồng Hà – P.2 – Q.Tân Bình – Tp.Hồ Chí Minh";
                    bodytext += "<br>Fax: (84-8) 3.8446.333";
                    bodytext += "<br>Mobile: (84) 913.146.309 | www.crew.vn</strong>";

                    message.Body = new MessageBody(BodyType.HTML, bodytext);

                    message.SendAndSaveCopy();
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " đề nghị Reset password " + soacc.ToString() + " tài khoản.");
                }
                else
                {
                    writetext.WriteLine("Ngày " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " không có tài khoản nào cần Reset password");
                }
            }
            catch (Exception ex)
            {
                isError = true;
                writetext.WriteLine("Error: " + ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (!isError)
                {
                    db.SaveChanges();
                }
                if (db != null)
                    db.Dispose();
                writetext.Close();
                Console.WriteLine("Complete!");
            }
        }
        internal static string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }

        internal static bool FileInUse(string filename)
        {

            try
            {
                if (!System.IO.File.Exists(filename))
                    return false;

                using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fs.Close();
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

    }
}
