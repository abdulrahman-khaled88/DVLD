using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
using static DVLD_DataAccess.LicensesData;

namespace DVLD_Buisness
{
    public static class LicensesBuisness
    {
        public class clsLicenseInfo
        {
            public string Class { get; set; }
            public string Name { get; set; }
            public string LicenseID { get; set; }
            public string NationalNo { get; set; }
            public string Gendor { get; set; }
            public DateTime IssueDate { get; set; }
            public string IsActive { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string DriverID { get; set; }
            public DateTime ExpirationDate { get; set; }

            public string ImagePath { get; set; }

        }


        public static string AddNewLicense(string ApplicationID, string DriverID,
            string LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
           string notes, string paidFees, string IsActive,
           string IssueReason, string CreatedByUserID)
        {
            return LicensesData.AddNewLicense(ApplicationID, DriverID,
             LicenseClass, IssueDate, ExpirationDate,
            notes, paidFees, IsActive,
            IssueReason, CreatedByUserID);
        }

        public static clsLicenseInfo GetLicenseInfo(string LicenseID)
        {
            return _ConvertDataToBuisness(LicensesData.GetLicenseInfo(LicenseID));
        }

        public static clsLicenseInfo _ConvertDataToBuisness(LicensesData.clsLicenseInfo op1)
        {
            LicensesBuisness.clsLicenseInfo op2 = new LicensesBuisness.clsLicenseInfo();

            if (op1 != null)
            {
                op2.Class = op1.Class;
                op2.Name = op1.Name;
                op2.LicenseID = op1.LicenseID;
                op2.NationalNo = op1.NationalNo;
                op2.Gendor = op1.Gendor;
                op2.IssueDate = op1.IssueDate;
                op2.IsActive = op1.IsActive;
                op2.DateOfBirth = op1.DateOfBirth;
                op2.DriverID = op1.DriverID;
                op2.ExpirationDate = op1.ExpirationDate;
                op2.ImagePath = op1.ImagePath;
            }
            else
            {
                return null;
            }


            return op2;
        }

        public static bool IsLicenseDetan(string LicenseID)
        {
            return LicensesData.IsLicenseDetan(LicenseID);
        }

        public static bool IsHaveInternationalLicense(string LicenseID)
        {
            return LicensesData.IsHaveInternationalLicense(LicenseID);
        }


        public static string AddNewInternationalLicense(Local_Dl_Business.clsAP AP,
            string driverID,string issuedUsingLocalLicenseID,DateTime issueDate,
            DateTime expirationDate,int isActive,string createdByUserID)
        {
            return LicensesData.AddNewInternationalLicense(Local_Dl_Business._BusinessToDataConvert(AP)
                , driverID, issuedUsingLocalLicenseID,issueDate, expirationDate, isActive, createdByUserID);
        }


        public static string AddNewApplication(Local_Dl_Business.clsAP AP)
        {
            return LicensesData.AddNewApplication(Local_Dl_Business._BusinessToDataConvert( AP));
        }


        public static bool DeActiveLicence(string LicenseID)
        {
            return LicensesData.DeActiveLicence(LicenseID);
        }

        public static bool ActiveateLicence(string LicenseID)
        {
            return LicensesData.ActiveateLicence(LicenseID) ;
        }
        public static string DetanLicence
            (string licenseID, DateTime detainDate, string fineFees, string createdByUserID, int isReleased)
        {
            return LicensesData.DetanLicence( licenseID,  detainDate,  fineFees,  createdByUserID,  isReleased);
        }

        public static bool ReleaseLicence(int isReleased, DateTime releaseDate, string releasedByUserID, string licenseID)
        {
            return LicensesData.ReleaseLicence(isReleased, releaseDate, releasedByUserID, licenseID);
        }
    }
}
