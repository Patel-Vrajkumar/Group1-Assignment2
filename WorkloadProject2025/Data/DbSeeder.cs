using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Seed Schools, Departments, Programs, and Courses if not present
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

            // Seed Workload Categories for schedule July1,2020 - June30,2024 if not present
            if (!context.WorkloadCategories.Any())
            {
                var start = new DateTime(2020,7,1);
                var end = new DateTime(2024,6,30);

                var categories = new List<WorkloadCategory>
                {
                    // Category1: degree/UT
                    new WorkloadCategory { MiniumHours =420, MaximumHours =462, StartDate = start, EndDate = end },
                    // Category2: diplomas/certificates
                    new WorkloadCategory { MiniumHours =462, MaximumHours =504, StartDate = start, EndDate = end },
                    // Category3: art & design
                    new WorkloadCategory { MiniumHours =504, MaximumHours =550, StartDate = start, EndDate = end },
                    // Category4: trades/clinical
                    new WorkloadCategory { MiniumHours =640, MaximumHours =720, StartDate = start, EndDate = end },
                };

                context.WorkloadCategories.AddRange(categories);
                context.SaveChanges();
            }
        }
    }
}
