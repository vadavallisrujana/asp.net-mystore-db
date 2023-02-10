using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace mystore.Pages.clients
{
    public class EditModel : PageModel
    {
        public Clientinfo Clientinfo = new Clientinfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\mysqllocal;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM mytable WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Clientinfo.id = "" + reader.GetInt32(0);
                                Clientinfo.name = reader.GetString(1);
                                Clientinfo.phone = reader.GetString(2);
                                Clientinfo.email = reader.GetString(3);
                                Clientinfo.address = reader.GetString(4);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            Clientinfo.id = Request.Form["id"];
            Clientinfo.name = Request.Form["name"];
            Clientinfo.email = Request.Form["email"];
            Clientinfo.phone = Request.Form["phone"];
            Clientinfo.address = Request.Form["address"];

            if (Clientinfo.id.Length == 0 || Clientinfo.name.Length == 0 || Clientinfo.email.Length == 0 ||
               Clientinfo.phone.Length == 0 || Clientinfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\mysqllocal;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE mytable SET name=@name,email=@email,phone=@phone,address=@address WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", Clientinfo.id);
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

            Response.Redirect("/clients/Index");
        }

    }
}

