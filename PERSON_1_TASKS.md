# ?? Person 1: Data Entry & User Experience Enhancements

**Focus Area:** Improving data entry speed, accuracy, and user interaction  
**Priority Level:** ? HIGH - Core Features  
**Estimated Time:** 2-3 Weeks

---

## ?? Task List

### ? Task 1: Add Dropdowns to FacultyWorkLoadForm (CRITICAL)
**Priority:** ??? Critical  
**Estimated Time:** 8-10 hours  
**Dependencies:** Faculty, Course, Term, WorkloadCategory services must exist

#### Subtasks:
- [ ] Replace manual Faculty Email text input with searchable dropdown
  - Load all faculty members from `Faculty` table
  - Display as: "FirstName LastName (email@example.com)"
  - Add search/filter functionality
- [ ] Add Course dropdown (optional, nullable)
  - Load courses from `Course` table
  - Filter by ProgramOfStudy if needed
  - Show course name and hours
- [ ] Add Term dropdown (required)
  - Load all terms from `Term` table
  - Display: "Term Name (Start - End dates)"
  - Mark current term as default
- [ ] Add WorkloadCategory dropdown (optional)
  - Load from `WorkloadCategory` table
  - Display min/max hours range
  - Auto-populate hours when selected
- [ ] Implement hours pre-fill logic
  - When WorkloadCategory selected, suggest hours based on category range
  - Show min/max validation warnings

#### Files to Modify:
- `WorkloadProject2025\Components\Forms\FacultyWorkLoadForm.razor`
- Consider creating reusable components:
  - `Components\Shared\FacultySelector.razor`
  - `Components\Shared\CourseSelector.razor`
  - `Components\Shared\TermSelector.razor`
  - `Components\Shared\WorkloadCategorySelector.razor`

#### Success Criteria:
- [ ] All dropdowns populate correctly
- [ ] Email text field replaced with faculty selector
- [ ] Hours auto-fill works when category selected
- [ ] Form validation prevents invalid selections
- [ ] No compilation errors
- [ ] Form still saves correctly to database

---

### ? Task 2: Implement "Copy Previous Year" Feature
**Priority:** ?? High  
**Estimated Time:** 12-15 hours  
**Dependencies:** FacultyWorkLoad service, Term service

#### Subtasks:
- [ ] Create new page: `CopyWorkloadPage.razor`
  - Add route: `@page "/copyworkload"`
  - Create UI with term selectors
- [ ] Add "Source Term" dropdown
  - Load all past terms (where EndDate < Today)
  - Display term names and date ranges
- [ ] Add "Target Term" dropdown
  - Load all future/current terms
  - Prevent selecting same as source
- [ ] Create copy logic in service layer
  - New method: `CopyWorkloadAsync(int sourceTermId, int targetTermId)`
  - Load all FacultyWorkLoad records from source term
  - Clone records and assign to target term
  - Reset DateAssigned to current date
- [ ] Add bulk-edit capability
  - Show preview table of records to be copied
  - Allow inline hour adjustments before saving
  - Add "Select All" / "Deselect All" checkboxes
  - Enable filtering by faculty, workload type, or program
- [ ] Implement save operation
  - Validate no duplicate assignments exist
  - Save all selected records in transaction
  - Show success/error notifications

#### Files to Create:
- `WorkloadProject2025\Components\Pages\CopyWorkloadPage.razor`
- `WorkloadProject2025\Services\IWorkloadCopyService.cs`
- `WorkloadProject2025\Services\WorkloadCopyService.cs`

#### Files to Modify:
- `WorkloadProject2025\Components\Layout\NavMenu.razor` (add navigation link)
- `WorkloadProject2025\Program.cs` (register new service)

#### Success Criteria:
- [ ] Page accessible from navigation menu
- [ ] Can select source and target terms
- [ ] Preview shows all records to be copied
- [ ] Can edit hours before saving
- [ ] Successfully copies records to target term
- [ ] Shows appropriate success/error messages
- [ ] No duplicate assignments created

---

### ? Task 3: Add Comments/Notes to Faculty Model
**Priority:** ? Medium  
**Estimated Time:** 4-6 hours  
**Dependencies:** Faculty model, EF Core migrations

#### Subtasks:
- [ ] Update Faculty model
  - Add `public string? Notes { get; set; }` property
  - Add XML documentation comments
- [ ] Create database migration
  - Run: `Add-Migration AddNotesToFaculty`
  - Review migration file
  - Run: `Update-Database`
- [ ] Update FacultyForm
  - Add MudTextField with `Lines="5"` for multiline input
  - Set as optional field
  - Add appropriate label and helper text
- [ ] Update FacultyPage display
  - Show notes in faculty cards (collapsible section)
  - Truncate long notes with "Read more" expansion
  - Add icon indicator when notes exist
- [ ] Test functionality
  - Add faculty with notes
  - Edit existing faculty notes
  - Verify notes display correctly

#### Files to Modify:
- `WorkloadProject2025\Data\Models\Faculty.cs`
- `WorkloadProject2025\Components\Forms\FacultyForm.razor`
- `WorkloadProject2025\Components\Pages\FacultyPage.razor`

#### Files to Create:
- `WorkloadProject2025\Migrations\{timestamp}_AddNotesToFaculty.cs` (auto-generated)

#### Success Criteria:
- [ ] Notes field exists in Faculty table
- [ ] Can add/edit notes in form
- [ ] Notes display on faculty cards
- [ ] Long notes are properly formatted
- [ ] Migration runs without errors
- [ ] No data loss for existing faculty records

---

### ? Task 4: Add Faculty Category/Role Field (EmploymentType)
**Priority:** ?? High  
**Estimated Time:** 6-8 hours  
**Dependencies:** Faculty model, enum creation

#### Subtasks:
- [ ] Create EmploymentType enum
  - New file: `Data\Models\EmploymentType.cs`
  - Values: `FullTime`, `PartTime`, `Adjunct`, `Visiting`, `Emeritus`, `Sessional`
- [ ] Update Faculty model
  - Add `public EmploymentType EmploymentCategory { get; set; }` property
  - Set default value: `EmploymentType.FullTime`
- [ ] Create database migration
  - Run: `Add-Migration AddEmploymentTypeToFaculty`
  - Run: `Update-Database`
- [ ] Update FacultyForm
  - Add MudSelect dropdown for EmploymentType
  - Display user-friendly names (convert underscores to spaces)
  - Make required field
- [ ] Update FacultyPage display
  - Add color-coded badge showing employment type
  - Use different colors:
    - FullTime: Primary (Blue)
    - PartTime: Info (Light Blue)
    - Adjunct: Warning (Orange)
    - Visiting: Secondary (Gray)
    - Emeritus: Success (Green)
    - Sessional: Default (Dark)
- [ ] Add filtering by employment type
  - Add filter dropdown above faculty list
  - Filter faculty cards by selected type
  - Show count per category

#### Files to Create:
- `WorkloadProject2025\Data\Models\EmploymentType.cs`
- `WorkloadProject2025\Migrations\{timestamp}_AddEmploymentTypeToFaculty.cs` (auto-generated)

#### Files to Modify:
- `WorkloadProject2025\Data\Models\Faculty.cs`
- `WorkloadProject2025\Components\Forms\FacultyForm.razor`
- `WorkloadProject2025\Components\Pages\FacultyPage.razor`

#### Success Criteria:
- [ ] EmploymentType enum created with all values
- [ ] Faculty table has EmploymentCategory column
- [ ] Dropdown shows in form with readable names
- [ ] Badges display correctly with colors
- [ ] Can filter faculty by employment type
- [ ] Migration completes successfully
- [ ] Existing faculty records have default value

---

## ?? Testing Checklist

Before considering tasks complete, verify:

- [ ] All forms validate correctly
- [ ] Dropdowns populate with real data
- [ ] Database migrations run without errors
- [ ] No compilation warnings or errors
- [ ] UI is responsive on mobile/tablet/desktop
- [ ] Search and filter functions work correctly
- [ ] Success/error messages display appropriately
- [ ] Data saves and loads correctly
- [ ] No JavaScript console errors
- [ ] Accessibility: keyboard navigation works
- [ ] MudBlazor components render properly

---

## ?? Resources & References

### MudBlazor Documentation:
- [MudSelect](https://mudblazor.com/components/select)
- [MudTextField](https://mudblazor.com/components/textfield)
- [MudAutocomplete](https://mudblazor.com/components/autocomplete)
- [MudChip](https://mudblazor.com/components/chip)

### EF Core Documentation:
- [Migrations Overview](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [Adding Properties](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties)

### Blazor Documentation:
- [Component Parameters](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/)
- [Event Handling](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/event-handling)

---

## ?? Integration Points

### With Person 2:
- Coordinate on WorkloadCategory validation rules
- Share EmploymentType enum for workload calculations
- Test dropdown selections with calculation service

### With Person 3:
- Ensure copy feature works with dashboard filters
- Verify employment type filtering matches dashboard data
- Test notes display in reports/exports

---

## ?? Notes & Considerations

- **Performance:** Use async loading for dropdowns with large datasets
- **UX:** Add loading spinners while data loads
- **Validation:** Prevent duplicate workload assignments
- **Search:** Implement debouncing for search fields (300ms delay)
- **Accessibility:** Ensure all form controls have labels
- **Mobile:** Test dropdown usability on touch devices
- **Error Handling:** Add try-catch blocks with user-friendly messages
- **Caching:** Consider caching frequently accessed dropdown data
