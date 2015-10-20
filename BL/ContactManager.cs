using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DAL;

namespace BL
{
  public class ContactManager : IContactManager
  {
    private readonly IRepository contactRepository;
    public void AddContact(string name, string streetAndNumber, short zipCode, string city, Gender gender, DateTime birthDay, string phone, string mobile)
    {
      Address adres = new Address
      {
        StreetAndNumber = streetAndNumber,
        Zipcode = zipCode,
        City = city,
      };
      Contact contact = new Contact
      {
        Name = name,
        Gender = gender,
        Birthday = birthDay,
        Phone = phone,
        Mobile = mobile,
        Adress = adres
      };
      
    }

    public ContactManager()
    {
      contactRepository = new MemoryRepository();
    }

    public void ChangeContact(Contact contactToChange)
    {
      contactRepository.UpdateContact(contactToChange);
    }

    public IEnumerable<Category> GetAllCategories()
    {
      return contactRepository.ReadAllCatergories();
    }

    public IEnumerable<Contact> GetAllContacts()
    {
      return contactRepository.ReadAllContacts();
    }

    public IEnumerable<Contact> GetContactsForACategory(int categoryId)
    {
      return contactRepository.ReadAllContacts(categoryId);
    }

    public Contact ReadContact(int id)
    {
      return contactRepository.ReadContact(id);
    }

    public void RemoveContact(int id)
    {
      contactRepository.DeleteContact(id);
    }
  }
}
