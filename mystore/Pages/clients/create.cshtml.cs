using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace mystore.Pages.clients
{
    public class CreateModel : PageModel
    {
        public Clientinfo Clientinfo = new Clientinfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void Onpost()
        {
            Clientinfo.name = Request.Form["name"];
            Clientinfo.email = Request.Form["email"];
            Clientinfo.phone = Request.Form["phone"];
            Clientinfo.address = Request.Form["address"];

            if(Clientinfo.name.Length == 0 || Clientinfo.email.Length == 0 ||
               Clientinfo.phone.Length == 0 || Clientinfo.address.Length == 0) 
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save the new client into database
            try
            {
                String connectionString = "Data Source=.\\mysqllocal;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO mytable" +
                        "(name, email, phone, address) VALUES " +
                        "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", Clientinfo.name);
                        command.Parameters.AddWithValue("@email", Clientinfo.email);
                        command.Parameters.AddWithValue("@phone", Clientinfo.phone);
                        command.Parameters.AddWithValue("@address", Clientinfo.address);
                        command.ExecuteNonQuery();  

                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            Clientinfo.name = ""; Clientinfo.email = ""; Clientinfo.phone = ""; Clientinfo.address = "";
            successMessage = "New client Added correctly";
            Response.Redirect("/clients/Index");
        }
    }
}

