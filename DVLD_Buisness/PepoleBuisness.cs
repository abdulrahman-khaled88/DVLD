using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public static class PepoleBuisness
    {
        public class ClsPerson
        {
            public int ID { get; set; } = 0;
            public string FirstName { get; set; } = string.Empty;
            public string SecondName { get; set; } = string.Empty;
            public string ThirdName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string NationalNo { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public int NationalityCountryID { get; set; } = 0;
            public string CountryName { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; } = new DateTime(2006, 1, 1);
            public string ImagePath { get; set; } = string.Empty;
            public int Gendor { get; set; } = 0;
            public string GendorText { get; set; } = string.Empty;


        }

        private static PepoleData.ClsPerson _BusinessToDataClassConverter(PepoleBuisness.ClsPerson Person)
        {
            PepoleData.ClsPerson Person2 = new PepoleData.ClsPerson();

            Person2.ID = Person.ID;
            Person2.FirstName = Person.FirstName;
            Person2.SecondName = Person.SecondName;
            Person2.ThirdName = Person.ThirdName;
            Person2.LastName = Person.LastName;
            Person2.NationalNo = Person.NationalNo;
            Person2.Email = Person.Email;
            Person2.Phone = Person.Phone;
            Person2.NationalityCountryID = PepoleData.GetCountryIdByName(Person.CountryName);
            Person2.Address = Person.Address;
            Person2.DateOfBirth = Person.DateOfBirth;
            Person2.ImagePath = Person.ImagePath;
            Person2.Gendor = PepoleBuisness.ConvertGendorTextToNumber(Person.GendorText) ;

            return Person2;
        }

        private static PepoleBuisness.ClsPerson _DataToBusinessClassConverter(PepoleData.ClsPerson Person)
        {
            PepoleBuisness.ClsPerson Person2 = new PepoleBuisness.ClsPerson();

            Person2.ID = Person.ID;
            Person2.FirstName = Person.FirstName;
            Person2.SecondName = Person.SecondName;
            Person2.ThirdName = Person.ThirdName;
            Person2.LastName = Person.LastName;
            Person2.NationalNo = Person.NationalNo;
            Person2.Email = Person.Email;
            Person2.Phone = Person.Phone;
            Person2.CountryName = PepoleData.GetCountryNameByID(Person.NationalityCountryID);
            Person2.Address = Person.Address;
            Person2.DateOfBirth = Person.DateOfBirth;
            Person2.ImagePath = Person.ImagePath;
            Person2.Gendor = Person.Gendor;

            return Person2;
        }

        private static string _QueryFilterGenerator(string FilterBy)
        {

            string Query = $@"
        SELECT 
            dbo.People.PersonID, 
            dbo.People.NationalNo, 
            dbo.People.FirstName, 
            dbo.People.SecondName, 
            dbo.People.ThirdName, 
            dbo.People.LastName, 
            dbo.People.DateOfBirth, 
            dbo.People.Gendor, 
            dbo.People.Address, 
            dbo.People.Phone, 
            dbo.People.Email, 
            dbo.Countries.CountryName AS Country, 
            dbo.People.ImagePath
        FROM 
            dbo.People 
        INNER JOIN
            dbo.Countries ON dbo.People.NationalityCountryID = dbo.Countries.CountryID
        WHERE 
            {FilterBy} = @{FilterBy}";







            return Query;
        }

        public static DataTable GetAllPersonsInfo()
        {

            return _ConvertGendorNumberToText(PepoleData.ListAllPersonsInfo());

        }

        public static DataTable GetAllPersonsInfoByFilter(string FilterBy, string Value)
        {

            return _ConvertGendorNumberToText(PepoleData.ListAllPersonsInfoByFilter
                (_QueryFilterGenerator(FilterBy), FilterBy, Value));

        }

        public static string AddNewPerson(ClsPerson NewPerson)
        {
            return PepoleData.AddNewPerson(_BusinessToDataClassConverter(NewPerson));
        }

        public static bool DeletePerson(int PersonID)
        {
            return PepoleData.DeletePerson(PersonID);
        }

        public static bool UpdatePerson(int PersonID, ClsPerson NewPerson)
        {
            return PepoleData.UpdatePerson(PersonID, _BusinessToDataClassConverter(NewPerson));
        }

        public static bool IsNatonalNoExists(string NationalNo)
        {
            return PepoleData.IsNatonalNoExists(NationalNo);
        }

        public static int GetCountryIdByName(string CountryName)
        {
            return PepoleData.GetCountryIdByName(CountryName);
        }

        private static DataTable _ConvertGendorNumberToText(DataTable dt)
        {
            if (dt.Rows.Count != 0)
            {
                dt.Columns.Add("Gender", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    row["Gender"] = Convert.ToInt16(row["Gendor"]) == 0 ? "Male" : "Female";
                }

                dt.Columns["Gender"].SetOrdinal(6);
                dt.Columns.Remove("Gendor");
            }


            return dt;
        }

        public static int ConvertGendorTextToNumber(string Gendor)
        {
            if (Gendor == "Male" || Gendor == "male")
            {
                return 0;
            }
            else if (Gendor == "Female" || Gendor == "female")
            {
                return 1;
            }

            return 0;
        }

        public static ClsPerson FindPersonByID(string PersonID)
        {
            DataTable dt = PepoleBuisness.GetAllPersonsInfoByFilter("PersonID", PersonID);


            if (dt.Rows.Count == 0)
            {
                return null;
            }



            DataRow row = dt.Rows[0];
            ClsPerson Person = new ClsPerson();

            Person.ID = Convert.ToInt32(row["PersonID"]);
            Person.NationalNo = Convert.ToString(row["NationalNo"]);
            Person.FirstName = Convert.ToString(row["FirstName"]);
            Person.SecondName = Convert.ToString(row["SecondName"]);
            Person.ThirdName = Convert.ToString(row["ThirdName"]);
            Person.LastName = Convert.ToString(row["LastName"]);
            Person.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]);
            Person.GendorText = Convert.ToString(row["Gender"]);
            Person.Address = Convert.ToString(row["Address"]);
            Person.Phone = Convert.ToString(row["Phone"]);
            Person.Email = Convert.ToString(row["Email"]);
            Person.CountryName = Convert.ToString(row["Country"]);
            Person.ImagePath = Convert.ToString(row["ImagePath"]);

            return Person;

        }

        public static ClsPerson FindPersonByNationalNo(string PersonNationalNo)
        {
            DataTable dt = PepoleBuisness.GetAllPersonsInfoByFilter("NationalNo", PersonNationalNo);


            if (dt.Rows.Count == 0)
            {
                return null; 
            }




            DataRow row = dt.Rows[0];
            ClsPerson Person = new ClsPerson();

            Person.ID = Convert.ToInt32(row["PersonID"]);
            Person.NationalNo = Convert.ToString(row["NationalNo"]);
            Person.FirstName = Convert.ToString(row["FirstName"]);
            Person.SecondName = Convert.ToString(row["SecondName"]);
            Person.ThirdName = Convert.ToString(row["ThirdName"]);
            Person.LastName = Convert.ToString(row["LastName"]);
            Person.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]);
            Person.GendorText = Convert.ToString(row["Gender"]);
            Person.Address = Convert.ToString(row["Address"]);
            Person.Phone = Convert.ToString(row["Phone"]);
            Person.Email = Convert.ToString(row["Email"]);
            Person.CountryName = Convert.ToString(row["Country"]);
            Person.ImagePath = Convert.ToString(row["ImagePath"]);

            return Person;

        }

        public static bool IsPersoFound(PepoleBuisness.ClsPerson person)
        {
            return person != null;
        }
    }
}
