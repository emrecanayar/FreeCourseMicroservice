using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }
            public static string NotFoundById(int categoryId)
            {

                return $"{categoryId} makale koduna ait bir makale bulunamadı.";
            }
        }
        public static class Course
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kurs bulunamadı.";
                return "Böyle bir kurs bulunamadı.";
            }
            public static string NotFoundById(int courseId)
            {

                return $"{courseId} kurs koduna ait bir kurs bulunamadı.";
            }
        }
    }
}
