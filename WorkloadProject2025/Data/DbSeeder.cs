using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Seed Schools/Departments/Programs/Courses if missing
            if (!context.Schools.Any())
            {
                // Create Schools based on Medicine Hat College structure
                var schoolOfArts = new School
                {
                    Name = "School of Arts and Literature",
                    Departments = new List<Department>()
                };

                var schoolOfTrades = new School
                {
                    Name = "School of Trades and Technology",
                    Departments = new List<Department>()
                };

                var schoolOfHealth = new School
                {
                    Name = "School of Health and Wellness",
                    Departments = new List<Department>()
                };

                var schoolOfBusiness = new School
                {
                    Name = "School of Business and Entrepreneurship",
                    Departments = new List<Department>()
                };

                // Add Departments and Programs for School of Arts and Literature
                var englishDept = new Department
                {
                    Name = "English Department",
                    School = schoolOfArts,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "English Literature",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Introduction to Literature", Hours =45 },
                                new Course { Name = "Creative Writing", Hours =45 },
                                new Course { Name = "English Composition", Hours =45 }
                            }
                        },
                        new ProgramOfStudy
                        {
                            Name = "Communications",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Technical Writing", Hours =45 },
                                new Course { Name = "Public Speaking", Hours =30 }
                            }
                        }
                    }
                };

                var finArtsDept = new Department
                {
                    Name = "Fine Arts Department",
                    School = schoolOfArts,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Visual Arts",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Drawing Fundamentals", Hours =60 },
                                new Course { Name = "Digital Arts", Hours =60 },
                                new Course { Name = "Art History", Hours =45 }
                            }
                        }
                    }
                };

                schoolOfArts.Departments.Add(englishDept);
                schoolOfArts.Departments.Add(finArtsDept);

                // Add Departments and Programs for School of Trades and Technology
                var electricalDept = new Department
                {
                    Name = "Electrical Department",
                    School = schoolOfTrades,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Electrician Apprenticeship",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Electrical Theory I", Hours =90 },
                                new Course { Name = "Electrical Theory II", Hours =90 },
                                new Course { Name = "Electrical Code", Hours =60 },
                                new Course { Name = "Motor Controls", Hours =75 }
                            }
                        }
                    }
                };

                var weldingDept = new Department
                {
                    Name = "Welding Department",
                    School = schoolOfTrades,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Welding Technology",
                            Courses = new List<Course>
                            {
                                new Course { Name = "SMAW Welding", Hours =120 },
                                new Course { Name = "GMAW Welding", Hours =120 },
                                new Course { Name = "Blueprint Reading", Hours =45 }
                            }
                        }
                    }
                };

                var itDept = new Department
                {
                    Name = "Information Technology",
                    School = schoolOfTrades,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Computer Systems Technology",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Programming Fundamentals", Hours =60 },
                                new Course { Name = "Database Management", Hours =60 },
                                new Course { Name = "Network Administration", Hours =75 },
                                new Course { Name = "Web Development", Hours =60 }
                            }
                        }
                    }
                };

                schoolOfTrades.Departments.Add(electricalDept);
                schoolOfTrades.Departments.Add(weldingDept);
                schoolOfTrades.Departments.Add(itDept);

                // Add Departments and Programs for School of Health and Wellness
                var nursingDept = new Department
                {
                    Name = "Nursing Department",
                    School = schoolOfHealth,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Practical Nursing",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Anatomy and Physiology", Hours =90 },
                                new Course { Name = "Pharmacology", Hours =60 },
                                new Course { Name = "Clinical Practice I", Hours =120 },
                                new Course { Name = "Clinical Practice II", Hours =120 },
                                new Course { Name = "Mental Health Nursing", Hours =60 }
                            }
                        }
                    }
                };

                var paramedicDept = new Department
                {
                    Name = "Paramedicine Department",
                    School = schoolOfHealth,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Emergency Medical Services",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Emergency Medical Responder", Hours =75 },
                                new Course { Name = "Primary Care Paramedic", Hours =150 },
                                new Course { Name = "Trauma Assessment", Hours =45 }
                            }
                        }
                    }
                };

                schoolOfHealth.Departments.Add(nursingDept);
                schoolOfHealth.Departments.Add(paramedicDept);

                // Add Departments and Programs for School of Business
                var accountingDept = new Department
                {
                    Name = "Accounting Department",
                    School = schoolOfBusiness,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Business Administration",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Financial Accounting", Hours =60 },
                                new Course { Name = "Managerial Accounting", Hours =60 },
                                new Course { Name = "Business Law", Hours =45 },
                                new Course { Name = "Marketing Principles", Hours =45 }
                            }
                        }
                    }
                };

                var entrepreneurDept = new Department
                {
                    Name = "Entrepreneurship Department",
                    School = schoolOfBusiness,
                    ProgramsOfStudy = new List<ProgramOfStudy>
                    {
                        new ProgramOfStudy
                        {
                            Name = "Small Business Management",
                            Courses = new List<Course>
                            {
                                new Course { Name = "Business Planning", Hours =45 },
                                new Course { Name = "Sales and Marketing", Hours =45 }
                            }
                        }
                    }
                };

                schoolOfBusiness.Departments.Add(accountingDept);
                schoolOfBusiness.Departments.Add(entrepreneurDept);

                // Add all schools to context
                context.Schools.AddRange(schoolOfArts, schoolOfTrades, schoolOfHealth, schoolOfBusiness);
                context.SaveChanges();
            }

            // Seed Terms
            if (!context.Terms.Any())
            {
                var terms = new List<Term>
                {
                    new Term { IntakeName = "Fall2024", StartDate = new DateTime(2024,9,1), EndDate = new DateTime(2024,12,20) },
                    new Term { IntakeName = "Winter2025", StartDate = new DateTime(2025,1,6), EndDate = new DateTime(2025,4,25) },
                    new Term { IntakeName = "Spring2025", StartDate = new DateTime(2025,5,5), EndDate = new DateTime(2025,8,15) }
                };
                context.Terms.AddRange(terms);
                context.SaveChanges();
            }

            // Seed Faculty
            if (!context.Faculty.Any())
            {
                var faculty = new List<Faculty>
                {
                    new Faculty { Email = "alice.johnson@example.edu", FirstName = "Alice", LastName = "Johnson", PhoneNumber = "555-1001", EmploymentCategory = EmploymentCategory.FullTime, IsActive = true },
                    new Faculty { Email = "bob.smith@example.edu", FirstName = "Bob", LastName = "Smith", PhoneNumber = "555-1002", EmploymentCategory = EmploymentCategory.PartTime, IsActive = true },
                    new Faculty { Email = "charlie.lee@example.edu", FirstName = "Charlie", LastName = "Lee", PhoneNumber = "555-1003", EmploymentCategory = EmploymentCategory.Adjunct, IsActive = true },
                    new Faculty { Email = "dana.khan@example.edu", FirstName = "Dana", LastName = "Khan", PhoneNumber = "555-1004", EmploymentCategory = EmploymentCategory.FullTime, IsActive = true }
                };
                context.Faculty.AddRange(faculty);
                context.SaveChanges();
            }

            // Seed Workload Categories
            if (!context.WorkloadCategories.Any())
            {
                // WORKLOAD SCHEDULE - JULY 1, 2020 - JUNE 30, 2024
                var scheduleStart = new DateTime(2020, 7, 1);
                var scheduleEnd = new DateTime(2024, 6, 30);
                var cats = new List<WorkloadCategory>
                {
                    new WorkloadCategory { Name = "CATEGORY 1: Courses within 4-year, government approved collaborative degree programs or courses applicable for university transfer within the Canadian university system (excluding those listed in category 3 and 4).", MiniumHours = 420, MaximumHours = 462, StartDate = scheduleStart, EndDate = scheduleEnd },
                    new WorkloadCategory { Name = "CATEGORY 2: Courses within 2-year government approved diploma programs and 1-year government approved certificate programs (excluding those listed in category 3 and 4)", MiniumHours = 462, MaximumHours = 504, StartDate = scheduleStart, EndDate = scheduleEnd },
                    new WorkloadCategory { Name = "CATEGORY 3: Courses within government approved art and design credentials.", MiniumHours = 504, MaximumHours = 550, StartDate = scheduleStart, EndDate = scheduleEnd },
                    new WorkloadCategory { Name = "CATEGORY 4: Courses within health care aide, power engineering, and skilled trades apprenticeship programs; and clinical courses in nursing and practical nurse.", MiniumHours = 640, MaximumHours = 720, StartDate = scheduleStart, EndDate = scheduleEnd }
                };
                context.WorkloadCategories.AddRange(cats);
                context.SaveChanges();
            }

            // Seed Faculty Workloads
            if (!context.FacultyWorkLoads.Any())
            {
                var fall = context.Terms.First(t => t.IntakeName == "Fall2024");
                var winter = context.Terms.First(t => t.IntakeName == "Winter2025");

                // Resolve some programs/courses
                var itProgram = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "Computer Systems Technology");
                var englishProg = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "English Literature");
                var businessProg = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "Business Administration");

                var progFund = context.Courses.FirstOrDefault(c => c.Name == "Programming Fundamentals");
                var dbMgmt = context.Courses.FirstOrDefault(c => c.Name == "Database Management");
                var comp = context.Courses.FirstOrDefault(c => c.Name == "English Composition");
                var marketing = context.Courses.FirstOrDefault(c => c.Name == "Marketing Principles");

                var alice = context.Faculty.First(f => f.Email == "alice.johnson@example.edu");
                var bob = context.Faculty.First(f => f.Email == "bob.smith@example.edu");
                var charlie = context.Faculty.First(f => f.Email == "charlie.lee@example.edu");
                var dana = context.Faculty.First(f => f.Email == "dana.khan@example.edu");

                var items = new List<FacultyWorkLoad>();

                // Alice: teaches Programming Fundamentals (Fall), full load
                if (itProgram != null && progFund != null)
                {
                    items.Add(new FacultyWorkLoad
                    {
                        FacultyEmail = alice.Email,
                        ProgramOfStudyId = itProgram.Id,
                        CourseId = progFund.Id,
                        TermId = fall.Id,
                        Workload = Workload.Course_Lecture,
                        HoursAssigned =30,
                        Description = "Intro programming course",
                        IsPrimaryInstructor = true,
                        HRProcessed = true,
                        HRProcessedDate = DateTime.UtcNow
                    });
                }

                // Co-teach Database Management (Fall): Alice50%, Bob50%
                if (itProgram != null && dbMgmt != null)
                {
                    items.Add(new FacultyWorkLoad
                    {
                        FacultyEmail = alice.Email,
                        ProgramOfStudyId = itProgram.Id,
                        CourseId = dbMgmt.Id,
                        TermId = fall.Id,
                        Workload = Workload.Course_Lecture,
                        HoursAssigned =20,
                        PercentShare =50,
                        IsPrimaryInstructor = true
                    });
                    items.Add(new FacultyWorkLoad
                    {
                        FacultyEmail = bob.Email,
                        ProgramOfStudyId = itProgram.Id,
                        CourseId = dbMgmt.Id,
                        TermId = fall.Id,
                        Workload = Workload.Course_Lecture,
                        HoursAssigned =20,
                        PercentShare =50,
                        IsPrimaryInstructor = false
                    });
                }

                // Charlie covers Marketing (Winter) for Bob
                if (businessProg != null && marketing != null)
                {
                    items.Add(new FacultyWorkLoad
                    {
                        FacultyEmail = charlie.Email,
                        ProgramOfStudyId = businessProg.Id,
                        CourseId = marketing.Id,
                        TermId = winter.Id,
                        Workload = Workload.Course_Lecture,
                        HoursAssigned =15,
                        IsCoverage = true,
                        CoveredForFacultyEmail = bob.Email,
                        HRProcessed = false,
                        HRNotes = "Pending HR approval"
                    });
                }

                // Dana teaches English Composition (Winter)
                if (englishProg != null && comp != null)
                {
                    items.Add(new FacultyWorkLoad
                    {
                        FacultyEmail = dana.Email,
                        ProgramOfStudyId = englishProg.Id,
                        CourseId = comp.Id,
                        TermId = winter.Id,
                        Workload = Workload.Course_Lecture,
                        HoursAssigned =25,
                        Description = "Writing fundamentals"
                    });
                }

                context.FacultyWorkLoads.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}
