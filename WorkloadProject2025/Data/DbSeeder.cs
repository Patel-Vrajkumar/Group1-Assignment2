using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Data
{
    public static class DbSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Check if data already exists
            if (context.Schools.Any())
            {
                return; // Data already seeded
            }

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
                            new Course { Name = "Introduction to Literature", Hours = 45 },
                            new Course { Name = "Creative Writing", Hours = 45 },
                            new Course { Name = "English Composition", Hours = 45 }
                        }
                    },
                    new ProgramOfStudy
                    {
                        Name = "Communications",
                        Courses = new List<Course>
                        {
                            new Course { Name = "Technical Writing", Hours = 45 },
                            new Course { Name = "Public Speaking", Hours = 30 }
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
                            new Course { Name = "Drawing Fundamentals", Hours = 60 },
                            new Course { Name = "Digital Arts", Hours = 60 },
                            new Course { Name = "Art History", Hours = 45 }
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
                            new Course { Name = "Electrical Theory I", Hours = 90 },
                            new Course { Name = "Electrical Theory II", Hours = 90 },
                            new Course { Name = "Electrical Code", Hours = 60 },
                            new Course { Name = "Motor Controls", Hours = 75 }
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
                            new Course { Name = "SMAW Welding", Hours = 120 },
                            new Course { Name = "GMAW Welding", Hours = 120 },
                            new Course { Name = "Blueprint Reading", Hours = 45 }
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
                            new Course { Name = "Programming Fundamentals", Hours = 60 },
                            new Course { Name = "Database Management", Hours = 60 },
                            new Course { Name = "Network Administration", Hours = 75 },
                            new Course { Name = "Web Development", Hours = 60 }
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
                            new Course { Name = "Anatomy and Physiology", Hours = 90 },
                            new Course { Name = "Pharmacology", Hours = 60 },
                            new Course { Name = "Clinical Practice I", Hours = 120 },
                            new Course { Name = "Clinical Practice II", Hours = 120 },
                            new Course { Name = "Mental Health Nursing", Hours = 60 }
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
                            new Course { Name = "Emergency Medical Responder", Hours = 75 },
                            new Course { Name = "Primary Care Paramedic", Hours = 150 },
                            new Course { Name = "Trauma Assessment", Hours = 45 }
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
                            new Course { Name = "Financial Accounting", Hours = 60 },
                            new Course { Name = "Managerial Accounting", Hours = 60 },
                            new Course { Name = "Business Law", Hours = 45 },
                            new Course { Name = "Marketing Principles", Hours = 45 }
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
                            new Course { Name = "Business Planning", Hours = 45 },
                            new Course { Name = "Sales and Marketing", Hours = 45 }
                        }
                    }
                }
            };

            schoolOfBusiness.Departments.Add(accountingDept);
            schoolOfBusiness.Departments.Add(entrepreneurDept);

            // Add all schools to context
            context.Schools.AddRange(schoolOfArts, schoolOfTrades, schoolOfHealth, schoolOfBusiness);
            
            // Save changes to get IDs
            context.SaveChanges();
            
            // Add Faculty members
            SeedFaculty(context);
            
            // Add Terms
            SeedTerms(context);
            
            // Add Course Schedules
            SeedCourseSchedules(context);
            
            // Add Faculty Workload
            SeedFacultyWorkload(context);
        }
        
        private static void SeedFaculty(ApplicationDbContext context)
        {
            var faculty = new List<Faculty>
            {
                new Faculty
                {
                    Email = "john.smith@mhc.ab.ca",
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "(403) 529-3811",
                    EmploymentCategory = EmploymentCategory.FullTime,
                    MaxTeachingHours = 15.0m,
                    MaxCourseLoad = 4,
                    HireDate = new DateTime(2015, 9, 1),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Information Technology")?.Id
                },
                new Faculty
                {
                    Email = "sarah.johnson@mhc.ab.ca",
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    PhoneNumber = "(403) 529-3812",
                    EmploymentCategory = EmploymentCategory.FullTime,
                    MaxTeachingHours = 15.0m,
                    MaxCourseLoad = 4,
                    HireDate = new DateTime(2016, 1, 15),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Information Technology")?.Id
                },
                new Faculty
                {
                    Email = "michael.brown@mhc.ab.ca",
                    FirstName = "Michael",
                    LastName = "Brown",
                    PhoneNumber = "(403) 529-3813",
                    EmploymentCategory = EmploymentCategory.PartTime,
                    MaxTeachingHours = 9.0m,
                    MaxCourseLoad = 2,
                    HireDate = new DateTime(2018, 9, 1),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Information Technology")?.Id
                },
                new Faculty
                {
                    Email = "emily.davis@mhc.ab.ca",
                    FirstName = "Emily",
                    LastName = "Davis",
                    PhoneNumber = "(403) 529-3814",
                    EmploymentCategory = EmploymentCategory.FullTime,
                    MaxTeachingHours = 15.0m,
                    MaxCourseLoad = 4,
                    HireDate = new DateTime(2017, 1, 10),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Nursing Department")?.Id
                },
                new Faculty
                {
                    Email = "david.wilson@mhc.ab.ca",
                    FirstName = "David",
                    LastName = "Wilson",
                    PhoneNumber = "(403) 529-3815",
                    EmploymentCategory = EmploymentCategory.FullTime,
                    MaxTeachingHours = 15.0m,
                    MaxCourseLoad = 4,
                    HireDate = new DateTime(2014, 9, 1),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Electrical Department")?.Id
                },
                new Faculty
                {
                    Email = "jennifer.martinez@mhc.ab.ca",
                    FirstName = "Jennifer",
                    LastName = "Martinez",
                    PhoneNumber = "(403) 529-3816",
                    EmploymentCategory = EmploymentCategory.Adjunct,
                    MaxTeachingHours = 6.0m,
                    MaxCourseLoad = 1,
                    HireDate = new DateTime(2020, 1, 15),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "English Department")?.Id
                },
                new Faculty
                {
                    Email = "robert.anderson@mhc.ab.ca",
                    FirstName = "Robert",
                    LastName = "Anderson",
                    PhoneNumber = "(403) 529-3817",
                    EmploymentCategory = EmploymentCategory.FullTime,
                    MaxTeachingHours = 15.0m,
                    MaxCourseLoad = 4,
                    HireDate = new DateTime(2013, 9, 1),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Accounting Department")?.Id
                },
                new Faculty
                {
                    Email = "lisa.taylor@mhc.ab.ca",
                    FirstName = "Lisa",
                    LastName = "Taylor",
                    PhoneNumber = "(403) 529-3818",
                    EmploymentCategory = EmploymentCategory.PartTime,
                    MaxTeachingHours = 9.0m,
                    MaxCourseLoad = 2,
                    HireDate = new DateTime(2019, 1, 10),
                    DepartmentId = context.Departments.FirstOrDefault(d => d.Name == "Welding Department")?.Id
                }
            };
            
            context.Faculty.AddRange(faculty);
            context.SaveChanges();
        }
        
        private static void SeedTerms(ApplicationDbContext context)
        {
            var terms = new List<Term>
            {
                new Term
                {
                    IntakeName = "Fall 2024",
                    StartDate = new DateTime(2024, 9, 1),
                    EndDate = new DateTime(2024, 12, 20)
                },
                new Term
                {
                    IntakeName = "Winter 2025",
                    StartDate = new DateTime(2025, 1, 6),
                    EndDate = new DateTime(2025, 4, 30)
                },
                new Term
                {
                    IntakeName = "Spring 2025",
                    StartDate = new DateTime(2025, 5, 1),
                    EndDate = new DateTime(2025, 8, 31)
                }
            };
            
            context.Terms.AddRange(terms);
            context.SaveChanges();
        }
        
        private static void SeedCourseSchedules(ApplicationDbContext context)
        {
            var fall2024 = context.Terms.FirstOrDefault(t => t.Name == "Fall 2024");
            if (fall2024 == null) return;
            
            // Get some IT courses
            var itProgram = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "Computer Systems Technology");
            if (itProgram == null) return;
            
            var programmingCourse = context.Courses.FirstOrDefault(c => c.Name == "Programming Fundamentals" && c.ProgramOfStudyId == itProgram.Id);
            var databaseCourse = context.Courses.FirstOrDefault(c => c.Name == "Database Management" && c.ProgramOfStudyId == itProgram.Id);
            var networkCourse = context.Courses.FirstOrDefault(c => c.Name == "Network Administration" && c.ProgramOfStudyId == itProgram.Id);
            var webdevCourse = context.Courses.FirstOrDefault(c => c.Name == "Web Development" && c.ProgramOfStudyId == itProgram.Id);
            
            var schedules = new List<CourseSchedule>();
            
            if (programmingCourse != null)
            {
                // Update course code
                programmingCourse.CourseCode = "ITEC 270";
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = programmingCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(11, 50, 0),
                    Room = "B231",
                    EnrolledStudents = 28,
                    MaxCapacity = 30,
                    InstructorEmail = "john.smith@mhc.ab.ca",
                    CourseType = CourseType.Lecture
                });
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = programmingCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Thursday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 50, 0),
                    Room = "B231",
                    EnrolledStudents = 28,
                    MaxCapacity = 30,
                    InstructorEmail = "john.smith@mhc.ab.ca",
                    CourseType = CourseType.Lab
                });
            }
            
            if (databaseCourse != null)
            {
                databaseCourse.CourseCode = "ITEC 230";
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = databaseCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(11, 50, 0),
                    Room = "B231",
                    EnrolledStudents = 25,
                    MaxCapacity = 30,
                    InstructorEmail = "sarah.johnson@mhc.ab.ca",
                    CourseType = CourseType.Lecture
                });
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = databaseCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Friday,
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(15, 30, 0),
                    Room = "B231",
                    EnrolledStudents = 25,
                    MaxCapacity = 30,
                    InstructorEmail = "sarah.johnson@mhc.ab.ca",
                    CourseType = CourseType.Lab
                });
            }
            
            if (networkCourse != null)
            {
                networkCourse.CourseCode = "NETW 290";
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = networkCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(14, 50, 0),
                    Room = "B228",
                    EnrolledStudents = 22,
                    MaxCapacity = 24,
                    InstructorEmail = "michael.brown@mhc.ab.ca",
                    CourseType = CourseType.Lecture
                });
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = networkCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Thursday,
                    StartTime = new TimeSpan(14, 50, 0),
                    EndTime = new TimeSpan(16, 40, 0),
                    Room = "B228",
                    EnrolledStudents = 22,
                    MaxCapacity = 24,
                    InstructorEmail = "michael.brown@mhc.ab.ca",
                    CourseType = CourseType.Lab
                });
            }
            
            if (webdevCourse != null)
            {
                webdevCourse.CourseCode = "PROG 225";
                
                schedules.Add(new CourseSchedule
                {
                    CourseId = webdevCourse.Id,
                    TermId = fall2024.Id,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeSpan(9, 25, 0),
                    EndTime = new TimeSpan(12, 5, 0),
                    Room = "B231",
                    EnrolledStudents = 30,
                    MaxCapacity = 30,
                    InstructorEmail = "john.smith@mhc.ab.ca",
                    CourseType = CourseType.Lab
                });
            }
            
            // Add nursing courses
            var nursingProgram = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "Practical Nursing");
            if (nursingProgram != null)
            {
                var anatomyCourse = context.Courses.FirstOrDefault(c => c.Name == "Anatomy and Physiology" && c.ProgramOfStudyId == nursingProgram.Id);
                if (anatomyCourse != null)
                {
                    anatomyCourse.CourseCode = "NURS 210";
                    
                    schedules.Add(new CourseSchedule
                    {
                        CourseId = anatomyCourse.Id,
                        TermId = fall2024.Id,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeSpan(13, 0, 0),
                        EndTime = new TimeSpan(15, 50, 0),
                        Room = "A105",
                        EnrolledStudents = 35,
                        MaxCapacity = 40,
                        InstructorEmail = "emily.davis@mhc.ab.ca",
                        CourseType = CourseType.Lecture
                    });
                }
            }
            
            context.CourseSchedules.AddRange(schedules);
            context.SaveChanges();
        }
        
        private static void SeedFacultyWorkload(ApplicationDbContext context)
        {
            var fall2024 = context.Terms.FirstOrDefault(t => t.Name == "Fall 2024");
            if (fall2024 == null) return;
            
            // Get IT courses
            var itProgram = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "Computer Systems Technology");
            if (itProgram == null) return;
            
            var programmingCourse = context.Courses.FirstOrDefault(c => c.Name == "Programming Fundamentals" && c.ProgramOfStudyId == itProgram.Id);
            var databaseCourse = context.Courses.FirstOrDefault(c => c.Name == "Database Management" && c.ProgramOfStudyId == itProgram.Id);
            var networkCourse = context.Courses.FirstOrDefault(c => c.Name == "Network Administration" && c.ProgramOfStudyId == itProgram.Id);
            var webdevCourse = context.Courses.FirstOrDefault(c => c.Name == "Web Development" && c.ProgramOfStudyId == itProgram.Id);
            
            var workloads = new List<FacultyWorkLoad>();
            
            // John Smith - Full load (slightly overloaded)
            if (programmingCourse != null)
            {
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "john.smith@mhc.ab.ca",
                    CourseId = programmingCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lecture,
                    HoursAssigned = 4.0m,
                    Description = "ITEC 270 - Programming Fundamentals Lecture",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-30)
                });
                
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "john.smith@mhc.ab.ca",
                    CourseId = programmingCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lab,
                    HoursAssigned = 3.0m,
                    Description = "ITEC 270 - Programming Fundamentals Lab",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-30)
                });
            }
            
            if (webdevCourse != null)
            {
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "john.smith@mhc.ab.ca",
                    CourseId = webdevCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lab,
                    HoursAssigned = 5.0m,
                    Description = "PROG 225 - Web Development Lab",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-28)
                });
            }
            
            // Add coordinator duties (pushes him to overload)
            workloads.Add(new FacultyWorkLoad
            {
                FacultyEmail = "john.smith@mhc.ab.ca",
                TermId = fall2024.Id,
                Workload = Workload.Coordinator_Release,
                HoursAssigned = 5.0m,
                Description = "IT Program Coordinator",
                Status = WorkloadStatus.Approved,
                DateAssigned = DateTime.Now.AddDays(-35)
            });
            
            // Sarah Johnson - Normal full load
            if (databaseCourse != null)
            {
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "sarah.johnson@mhc.ab.ca",
                    CourseId = databaseCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lecture,
                    HoursAssigned = 4.0m,
                    Description = "ITEC 230 - Database Management Lecture",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-30)
                });
                
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "sarah.johnson@mhc.ab.ca",
                    CourseId = databaseCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lab,
                    HoursAssigned = 3.0m,
                    Description = "ITEC 230 - Database Management Lab",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-30)
                });
            }
            
            workloads.Add(new FacultyWorkLoad
            {
                FacultyEmail = "sarah.johnson@mhc.ab.ca",
                TermId = fall2024.Id,
                Workload = Workload.Committee_Work,
                HoursAssigned = 3.0m,
                Description = "Curriculum Committee Member",
                Status = WorkloadStatus.Approved,
                DateAssigned = DateTime.Now.AddDays(-25)
            });
            
            // Michael Brown - Part-time, under normal load
            if (networkCourse != null)
            {
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "michael.brown@mhc.ab.ca",
                    CourseId = networkCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lecture,
                    HoursAssigned = 4.0m,
                    Description = "NETW 290 - Network Administration Lecture",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-30)
                });
                
                workloads.Add(new FacultyWorkLoad
                {
                    FacultyEmail = "michael.brown@mhc.ab.ca",
                    CourseId = networkCourse.Id,
                    TermId = fall2024.Id,
                    Workload = Workload.Course_Lab,
                    HoursAssigned = 3.0m,
                    Description = "NETW 290 - Network Administration Lab",
                    Status = WorkloadStatus.Approved,
                    DateAssigned = DateTime.Now.AddDays(-30)
                });
            }
            
            // Emily Davis - Full load
            var nursingProgram = context.ProgramsOfStudy.FirstOrDefault(p => p.Name == "Practical Nursing");
            if (nursingProgram != null)
            {
                var anatomyCourse = context.Courses.FirstOrDefault(c => c.Name == "Anatomy and Physiology" && c.ProgramOfStudyId == nursingProgram.Id);
                if (anatomyCourse != null)
                {
                    workloads.Add(new FacultyWorkLoad
                    {
                        FacultyEmail = "emily.davis@mhc.ab.ca",
                        CourseId = anatomyCourse.Id,
                        TermId = fall2024.Id,
                        Workload = Workload.Course_Lecture,
                        HoursAssigned = 6.0m,
                        Description = "NURS 210 - Anatomy and Physiology",
                        Status = WorkloadStatus.Approved,
                        DateAssigned = DateTime.Now.AddDays(-30)
                    });
                }
            }
            
            context.FacultyWorkLoads.AddRange(workloads);
            context.SaveChanges();
        }
    }
}
