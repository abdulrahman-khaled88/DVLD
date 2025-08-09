using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.Local_DL_Data;

namespace DVLD_DataAccess
{
    public static class Global_Data
    {

        


        public static string GetApFeesByID(string ID)
        {

            string Query = "select ApplicationFees from ApplicationTypes where ApplicationTypeID = @ID";
            object Result = new object();
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                DataAccessSettinegs.Connection.Open();

                Result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }




            return Convert.ToString(Convert.ToInt32(Result));








        }




        //if (e.RowIndex >= 0)
        //{

        //    dataGridView1.ClearSelection();
        //    dataGridView1.Rows[e.RowIndex].Selected = true;


        //    UserBuisness._SelectedUserIdGrid = dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value.ToString();



        //}













        //if (MessageBox.Show($"Are you sure you want to cancle this application?"
        //        , "Confirm cancle", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
        //{
        //    if (LocalDl_Business.UpdateApStatus(_SelectedApIdGrid, _GetStatusNumber("Cancelled")))
        //    {
        //        MessageBox.Show("application cancled Successfuly.", "Successful", MessageBoxButtons.OK
        //            , MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("application Was not cancled."
        //            , "Error", MessageBoxButtons.OK
        //            , MessageBoxIcon.Error);
        //    }
        //}















    }
}
