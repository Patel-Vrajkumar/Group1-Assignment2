# 🧩 Assignment Tasks

## **Part We Do Together**

- [ ] Change the **appsettings** so everyone will use a **Docker container** instead of the instructor database.  
- [ ] Remove the **Primary Key ID** field from the **School form**.  
- [ ] Show **all results** on the **SchoolPage**.  
- [ ] Make a **callback** in the School form so we can **refresh the page**.  
- [ ] Link the **School** and **Department** together by updating the models:

   **Department.cs**
   ```csharp
   public class Department
   {
       public int Id { get; set; }
       public string Name { get; set; }
       // This should be enough to make the foreign key
       public int SchoolId { get; set; }
       public School School { get; set; }
   }
   ```

   **School.cs**
   ```csharp
   public class School
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public List<Department> Departments { get; set; } = new();
   }
   ```

- [ ] Make a **migration** and **update the database**.  
- [ ] Change the **Department form** to include a **dropdown list** of Schools.

---

## **Part the Students Will Do Themselves**

- [ ] Remove the **ID field** from the **ProgramOfStudyForm**, **CourseForm**, and **TermForm**.  
- [ ] Adjust each page (**DepartmentPage**, **TermPage**, **CoursePage**, and **ProgramOfStudyPage**) to show all available data — just like we did for **Schools**.  
- [ ] Link **Department** and **ProgramOfStudy** in the same way we linked **School** and **Department**.  
  - [ ] Update your models accordingly.  
  - [ ] Run `add-migration <name>` and `update-database`.  
- [ ] Under **ProgramOfStudy**, create a **dropdown** to select a **Department**.  
  - [ ] Ensure that data can be added correctly.  
- [ ] Repeat the same process to **link ProgramsOfStudy and Courses**.  
- [ ] Verify that **all data can be added**.  
  - [ ] Leave a comment below the question if something isn’t working.  
- [ ] Without **any collaboration** with classmates, create an **entity representing faculty workload**.  
  - [ ] This entity should be flexible enough to represent:
    - **Courses (lecture/lab)**
    - **Coordinator duties**
    - **Special projects**
  - [ ] Be creative — **unique solutions are expected**, and **identical ones will be flagged**.
