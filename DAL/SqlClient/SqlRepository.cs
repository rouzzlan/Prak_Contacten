using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL.SqlClient
{
  public class SqlRepository : IRepository
  {
    public void CreateContact(Contact contactToInsert)
    {
      throw new NotImplementedException();
    }

    public void DeleteContact(int id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Category> ReadAllCatergories()
    {
      string query = "SELECT Id, Description FROM Categories";
      List<Category> categories = new List<Category>();
      using (SqlConnection conn = GetConnection())
      {
        //Toewijzen van zowel query alsook de connectie aan het commando object
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        //Commando uitvoeren op de DB en de resultatentabel uitlezen via onze
        //DataReader cursor
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          Category loadingCategory = new Category();
          loadingCategory.CategoryId = reader.GetInt32(0);
          loadingCategory.Description = reader.GetString(1);
          categories.Add(loadingCategory);
        }
        reader.Close();
        conn.Close();
      }
      return categories;
    }

    public IEnumerable<Contact> ReadAllContacts(int? categoryId = default(int?))
    {
      string query = "SELECT c.Id, Name, AddressId, a.StreetAndNumber, a.ZipCode, a.City, Gender, Blocked, BirthDay, Phone, Mobile FROM Contacts c INNER JOIN Addresses a ON a.Id = AddressId";
      List<Contact> contactsTemp = new List<Contact>();
      using (SqlConnection conn = GetConnection())
      {
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          Contact loadingContact = new Contact();
          loadingContact.ContactId = reader.GetInt32(0);
          loadingContact.Name = reader.GetString(1);
          Address adres = new Address();
          adres.AddressId = reader.GetInt32(2);
          adres.StreetAndNumber = reader.GetString(3);
          adres.Zipcode = (short)reader.GetInt16(4); //kan fout geven
          adres.City = reader.GetString(5);
          loadingContact.Adress = adres;
          byte genderInt = reader.GetByte(6);// blijkbaar met eem byte
          if (genderInt == 1)
            loadingContact.Gender = Gender.Male;
          else
            loadingContact.Gender = Gender.Female;
          loadingContact.Blocked = reader.GetBoolean(7);
          loadingContact.Birthday = reader.GetDateTime(8);
          loadingContact.Phone = reader.IsDBNull(9) ? null : reader.GetString(9);
          loadingContact.Mobile = reader.IsDBNull(10) ? null : reader.GetString(10);

          contactsTemp.Add(loadingContact);
        }
      }
      return contactsTemp;
    }

    public Contact ReadContact(int id)
    {
      throw new NotImplementedException();
    }

    public void UpdateContact(Contact contactToUpdate)
    {
      throw new NotImplementedException();
    }
    //string connString=ConfigurationManager.ConnectionStrings["<naamConnectionString>"].ConnectionString;
    private SqlConnection GetConnection()
    {
      var connStr = ConfigurationManager.ConnectionStrings["ContactsDB-ADO"].ConnectionString;
      //add reference niet vergeten
      return new SqlConnection(connStr);
    }
  }
}
