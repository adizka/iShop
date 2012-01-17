using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Modules.Countries
{
    public class Countries
    {
        public IQueryable<Country> GetAllCountries()
        {
            return new ShopDataContext().Countries;
        }

        public Country GetCountryByID(int ID)
        {
            return new ShopDataContext().Countries.FirstOrDefault(c => c.ID == ID);
        }

        public void AddCountry(string Name)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                db.Countries.InsertOnSubmit(new Country()
                {
                    Name = Name
                });
                db.SubmitChanges();
            }
        }
        
        public bool DeleteCountry(int ID)
        {
            using (ShopDataContext db = new ShopDataContext())
            {
                var country = db.Countries.FirstOrDefault(c => c.ID == ID);
                if (country == null)
                    return false;
                db.Countries.DeleteOnSubmit(country);
                db.SubmitChanges();
            }
            return true;
        }
    }
}