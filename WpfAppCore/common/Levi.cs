using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCore.model;

namespace WpfAppCore.common
{
    class Levi
    {
        public static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
        public static int LevenshteinDistance(string firstWord, string secondWord)
        {
            Console.WriteLine("-----start levi ------------");
            Console.WriteLine(firstWord);
            Console.WriteLine(secondWord);
            Console.WriteLine("-----end levi ------------");
            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                            matrixD[i, j - 1] + insertionCost,         // вставка
                                            matrixD[i - 1, j - 1] + substitutionCost); // замена
                }
            }

            return matrixD[n - 1, m - 1];
        }
        public static string getCityFromObject(object obj)
        {

            if (obj == null)
            {
                return "";
            }
            if (obj is IRealty)
            {
                return ((IRealty)obj).city?.Trim().ToLower();
            }
            
            return "";
        }
        public static string getStreetFromObject(object obj)
        {

            if (obj == null)
            {
                return "";
            }
            if (obj is IRealty)
            {
                return ((IRealty)obj).street?.Trim().ToLower();
            }
            
            return "";
        }
        public static string getHouseNumberFromObject(object obj)
        {

            if (obj == null)
            {
                return "";
            }
            if (obj is IRealty)
            {
                return ((IRealty)obj).houseNumber?.Trim().ToLower();
            }
            return "";
        }
        public static string getApartmentNumberFromObject(object obj)
        {

            if (obj == null)
            {
                return "";
            }
            if (obj is IRealty)
            {
                return ((IRealty)obj).apartmentNumber?.Trim().ToLower();
            }
            return "";
        }
        public static string getAdressFromObject(object obj)
        {

            if (obj == null)
            {
                return "";
            }
            return getCityFromObject(obj) + getStreetFromObject(obj) + getHouseNumberFromObject(obj) + getApartmentNumberFromObject(obj);
        }

        public static string[] GetLeviData(string city, string street, string houseNumber, string apartmentNumber, IRealty item)
        {
            string[] res = new string[2] { "", "" };
            string sourse = "";
            string search = "";
            if (item == null)
            {
                return res;
            }

            if (city != null && city.Trim().Length != 0)
            {
                search += city;
                sourse += item.city ?? "";
            }
            if (street != null && street.Trim().Length != 0)
            {
                search += street;
                sourse += item.street ?? "";
            }
            if (houseNumber != null && houseNumber.Trim().Length != 0)
            {
                search += houseNumber;
                sourse += item.houseNumber ?? "";
            }
            if (apartmentNumber != null && apartmentNumber.Trim().Length != 0)
            {
                search += apartmentNumber;
                sourse += item.apartmentNumber ?? "";
            }

            res[0] = search;
            res[1] = sourse;
            return res;
        }
    }
}