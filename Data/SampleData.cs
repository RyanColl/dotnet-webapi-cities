

using System.Collections.Generic;
using System.Linq;
using webapi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace webapi.Data
{

    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                // Look for any Provinces.
                if (context.Provinces.Any())
                {
                    return;   // DB has already been seeded
                }

                var provinces = GetProvinces().ToArray();
                context.Provinces.AddRange(provinces);
                context.SaveChanges();

                var cities = GetCities(context).ToArray();
                context.Cities.AddRange(cities);
                context.SaveChanges();
            }
        }

        public static List<Province> GetProvinces()
        {
            List<Province> Provinces = new List<Province>() {
            new Province() {
                ProvinceCode="BC",
                ProvinceName="British Columbia"
            },
            new Province() {
                ProvinceCode="AB",
                ProvinceName="Alberta"
            },
            new Province() {
                ProvinceCode="ON",
                ProvinceName="Ontario"
            }
        };

            return Provinces;
        }

        public static List<City> GetCities(ApplicationDbContext context)
        {
            List<City> Cities = new List<City>() {
            new City {
                CityName = "Vancouver",
                Population = 2356700,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode
            },
            new City {
                CityName = "Surrey",
                Population = 778000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode
            },
            new City {
                CityName = "New West",
                Population = 578000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode
            },
            new City {
                CityName = "Medicine Hat",
                Population = 90000,
                ProvinceCode = context.Provinces.Find("AB").ProvinceCode
            },
            new City {
                CityName = "Edmonton",
                Population = 230000,
                ProvinceCode = context.Provinces.Find("AB").ProvinceCode
            },
            new City {
                CityName = "Calgary",
                Population = 420000,
                ProvinceCode = context.Provinces.Find("AB").ProvinceCode
            },
            new City {
                CityName = "Mississaugua",
                Population = 2356700,
                ProvinceCode = context.Provinces.Find("ON").ProvinceCode
            },
            new City {
                CityName = "Toronto",
                Population = 2356700,
                ProvinceCode = context.Provinces.Find("ON").ProvinceCode
            },
            new City {
                CityName = "Ottawa",
                Population = 2356700,
                ProvinceCode = context.Provinces.Find("ON").ProvinceCode
            },
        };

            return Cities;
        }
    }
}


