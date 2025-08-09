using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
using static DVLD_DataAccess.Local_DL_Data;

namespace DVLD_Buisness
{
    public static class Local_Dl_Business
    {



        public class clsAP
        {
            public string Local_DL_ID { get; set; }
            public string Local_DL_Class { get; set; }
            public string Local_DL_PassedTests { get; set; }
            public string ApplicationID { get; set; }
            public string ApplicantPersonID { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string ApplicationTypeID { get; set; }
            public string ApplicationStatus { get; set; }
            public DateTime LastStatusDate { get; set; }
            public string PaidFees { get; set; }
            public string CreatedByUserID { get; set; }
        }

        public class clsApInfo
        {
            public string LDL_ID { get; set; }
            public string ApplicationID { get; set; }
            public string ApplicationsPaidFees { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string ApplicationTypeTitle { get; set; }
            public string Applicant { get; set; }
            public string LicenseClasseName { get; set; }
            public string ApplicationStatusText { get; set; }

            public string ApplicationStatusNum { get; set; }
        }

        public static string SelectedTestAppointmentID = null;

        public static Local_DL_Data.clsAP _BusinessToDataConvert(Local_Dl_Business.clsAP AP)
        {
            Local_DL_Data.clsAP AP2 = new Local_DL_Data.clsAP();

            AP2.ApplicationID = AP.ApplicationID;
            AP2.ApplicantPersonID = AP.ApplicantPersonID;
            AP2.ApplicationDate = AP.ApplicationDate;
            AP2.ApplicationTypeID = AP.ApplicationTypeID;
            AP2.ApplicationStatus = AP.ApplicationStatus;
            AP2.LastStatusDate = AP.LastStatusDate;
            AP2.PaidFees = AP.PaidFees;
            AP2.CreatedByUserID = AP.CreatedByUserID;
            AP2.Local_DL_ID = AP.Local_DL_ID;
            AP2.Local_DL_Class = AP2.Local_DL_Class;
            AP2.Local_DL_PassedTests = AP2.Local_DL_PassedTests;

            return AP2;
        }

        private static Local_Dl_Business.clsApInfo _DataToBusinessConvert(Local_DL_Data.clsApInfo AP)
        {
            Local_Dl_Business.clsApInfo AP2 = new Local_Dl_Business.clsApInfo();

            AP2.LDL_ID = AP.LDL_ID;
            AP2.ApplicationID= AP.ApplicationID;
            AP2.ApplicationsPaidFees= AP.ApplicationsPaidFees;
            AP2.ApplicationDate= AP.ApplicationDate;
            AP2.ApplicationTypeTitle = AP.ApplicationTypeTitle;
            AP2.LicenseClasseName = AP.LicenseClasseName; 
            AP2.ApplicationStatusText = AP.ApplicationStatusText;
            AP2.Applicant = AP.Applicant;
            
            
            return AP2;
        }

        


        public static string HasOpenLicenseApplication(string PeronID, string ClassID, int Status)
        {
            return Local_DL_Data.HasOpenLicenseApplication(PeronID, ClassID, Status);
        }

        public static string AddNewLocalAP(clsAP AP, string licenseClassID)
        {
            return Local_DL_Data.AddNewLocalAP(_BusinessToDataConvert(AP), licenseClassID);
        }

        public static DataTable ListAllLocal_Dl()
        {
            return Local_DL_Data.ListAllLocal_Dl();
        }

        public static DataTable ListAllLocal_DlByFilter(string FilterBy, string Value)
        {
            return Local_DL_Data.ListAllLocal_DlByFilter(FilterBy, Value);
        }

        public static bool UpdateLDL_ApStatus(string LDL_ID, string NewStatus)
        {
            return Local_DL_Data.UpdateLDL_ApStatus(LDL_ID, NewStatus);
        }

        public static string GetApStatus(string ApID)
        {
            return Local_DL_Data.GetApStatus(ApID);
        }

        public static clsApInfo GetAPInfoByID(string AP_ID)
        {
            return _DataToBusinessConvert( Local_DL_Data.GetAPInfoByID( AP_ID ) );
        }

        public static string GetTestTypeFees(string TestID)
        {
            return Local_DL_Data.GetTestTypeFees(TestID);
        }

        public static bool CheckIfHaveAppointment(string LDL_ID, string TestID)
        {
            return Local_DL_Data.CheckIfHaveAppointment(LDL_ID, TestID);
        }

        public static DataTable GetAllTestAppointments(string LDL_ID, string TestID)
        {
           return Local_DL_Data.GetAllTestAppointments(LDL_ID , TestID);
        }

        public static bool AddNewTestAppointment(string LDL_ID, string TestID, string PaidFees,
             DateTime AppointmentDate, string CreatedByUserID, string RetakeTestApplicationID = null)
        {
            return Local_DL_Data.AddNewTestAppointment(LDL_ID, TestID, PaidFees,
              AppointmentDate, CreatedByUserID, RetakeTestApplicationID);
        }

        public static bool UpdateTestAppointment(string TestAppointmentID, DateTime NewDate)
        {
            return Local_DL_Data.UpdateTestAppointment(TestAppointmentID, NewDate);
        }

        public static DateTime GetTestAppointmentDate(string TestAppointmentID)
        {
            return Local_DL_Data.GetTestAppointmentDate(TestAppointmentID);
        }

        public static bool IsTestAppointmentLocked(string TestAppointmentID)
        {
          return  Local_DL_Data.IsTestAppointmentLocked(TestAppointmentID);
        }

        public static string TakeTest(string TestAppointmentID, string TestResult, string Notes, string CreatedByUserID)
        {
            return Local_DL_Data.TakeTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }

        public static bool MakeTestAppointmentLocked(string TestAppointmentID)
        {
            return Local_DL_Data.MakeTestAppointmentLocked(TestAppointmentID);
        }

        public static bool IsPassedTest(string LDL_ID, string TestTypeID)
        {
            return Local_DL_Data.IsPassedTest(LDL_ID, TestTypeID);
        }

        public static bool DeleteLDLAp(string LDL_ID)
        {
            return Local_DL_Data.DeleteLDLAp(LDL_ID);
        }

        public static DataTable GetAllGlobalLiceses(string PersonID)
        {
            return Local_DL_Data.GetAllGlobalLiceses(PersonID);
        }

        public static DataTable GetAllLocalLiceses(string PersonID)
        {
            return Local_DL_Data.GetAllLocalLiceses(PersonID) ;
        }

        public static string GetPersonIdByLDL(string LDL_ID)
        {
            return Local_DL_Data.GetPersonIdByLDL(LDL_ID) ;
        }

        public static bool IsDriver(string PersonID)
        {
            return Local_DL_Data.IsDriver(PersonID) ;
        }

        public static bool IsHaveTheLicense(string PersonID, string LicenseClassID)
        {
            return Local_DL_Data.IsHaveTheLicense(PersonID, LicenseClassID) ;
        }

        public static string GetLicenseClassID(string LDL_ID)
        {
            return Local_DL_Data.GetLicenseClassID(LDL_ID) ;
        }

        public static string GetApplicationID(string LDL_ID)
        {
            return Local_DL_Data.GetApplicationID(LDL_ID);
        }

        public static string GetApplicationFess(string AP_ID)
        {
            return Local_DL_Data.GetApplicationFess(AP_ID);
        }

    }
}
