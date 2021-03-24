using ProjectPartB.Entities;
using System;
using System.Collections.Generic;

namespace ProjectPartB.Services
{
    class TypeService : TypeCourse
    {
        private readonly string query = "SELECT * FROM C_Types";
        public List<TypeCourse> SetList() => GetAll<TypeCourse>(query);
    }
}
