using ProjectPartB.Entities;
using System;
using System.Collections.Generic;

namespace ProjectPartB.Services
{
    class SubjectService : Subject
    {
        private readonly string query = "SELECT * FROM T_Subjects";
        public List<Subject> SetList() => GetAll<Subject>(query);

    }
}
