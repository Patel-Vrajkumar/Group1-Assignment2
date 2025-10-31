# ?? Person 2: Workload Tracking, Calculations & Validation

**Focus Area:** Business logic, validation rules, and workload calculation engine  
**Priority Level:** ? HIGH - Core Business Logic  
**Estimated Time:** 3-4 Weeks

---

## ?? Task List

### ? Task 1: Implement Workload Total Calculations & Warnings (CRITICAL)
**Priority:** ??? Critical  
**Estimated Time:** 12-15 hours  
**Dependencies:** FacultyWorkLoad model, WorkloadCategory model

#### Subtasks:
- [ ] Create WorkloadCalculationService
  - New interface: `IWorkloadCalculationService.cs`
  - Implementation: `WorkloadCalculationService.cs`
  - Methods to implement:
    - `Task<decimal> GetTotalHoursForFacultyAsync(string facultyEmail, int? termId = null)`
    - `Task<decimal> GetTeachingHoursAsync(string facultyEmail, int termId)`
    - `Task<decimal> GetNonTeachingHoursAsync(string facultyEmail, int termId)`
    - `Task<WorkloadValidationResult> ValidateWorkloadAsync(string facultyEmail, int termId)`
    - `Task<List<FacultyWorkloadSummary>> GetWorkloadSummariesAsync(int termId)`

- [ ] Create validation models
  - `WorkloadValidationResult.cs`:
    ```csharp
    public class WorkloadValidationResult
    {
        public bool IsValid { get; set; }
        public decimal TotalHours { get; set; }
        public decimal MinAllowedHours { get; set; }
        public decimal MaxAllowedHours { get; set; }
        public WorkloadStatus Status { get; set; } // Enum: Normal, Warning, Overload, Underload
        public List<string> Messages { get; set; } = new();
    }
    
    public enum WorkloadStatus
    {
        Underload,      // Below minimum
        Normal,         // Within range
        ApproachingMax, // 90-99% of max
        Overload        // Exceeds maximum
    }
    ```

- [ ] Implement calculation logic
  - Query all FacultyWorkLoad records for given faculty/term
  - Sum HoursAssigned grouped by workload type
  - Calculate teaching vs non-teaching hours separately
  - Compare against WorkloadCategory min/max limits
  - Check faculty EmploymentType rules (from Person 1)

- [ ] Add color-coded warning system
  - **Red Alert** (Overload): Hours > MaxAllowedHours
    - Display flashing icon on FacultyPage
    - Show detailed message: "Exceeds maximum by X hours"
  - **Orange Warning** (Approaching): Hours >= 90% of MaxAllowedHours
    - Display warning icon
    - Message: "Approaching maximum workload"
  - **Yellow Warning** (Underload): Hours < MinAllowedHours
    - Display info icon
    - Message: "Below minimum workload by X hours"
  - **Green** (Normal): Within acceptable range

- [ ] Update FacultyPage to show warnings
  - Add visual indicators to faculty cards
  - Display total hours with color coding
  - Show breakdown: Teaching vs Non-Teaching
  - Add tooltip with detailed calculation

- [ ] Update FacultyWorkLoadPage
  - Add warning badges to workload cards
  - Show running total as assignments are added
  - Display real-time validation messages

#### Files to Create:
- `WorkloadProject2025\Services\IWorkloadCalculationService.cs`
- `WorkloadProject2025\Services\WorkloadCalculationService.cs`
- `WorkloadProject2025\Data\Models\WorkloadValidationResult.cs`
- `WorkloadProject2025\Data\Models\FacultyWorkloadSummary.cs`
- `WorkloadProject2025\Data\Models\WorkloadStatus.cs`

#### Files to Modify:
- `WorkloadProject2025\Components\Pages\FacultyPage.razor`
- `WorkloadProject2025\Components\Pages\FacultyWorkLoadPage.razor`
- `WorkloadProject2025\Program.cs` (register service)

#### Success Criteria:
- [ ] Service calculates total hours correctly
- [ ] Validation identifies overload/underload situations
- [ ] Color-coded warnings display on UI
- [ ] Real-time calculation as workload assigned
- [ ] Handles edge cases (no category, no term, etc.)
- [ ] Performance: calculations under 500ms for 100+ faculty

---

### ? Task 2: Add Non-Teaching Workload Tracking
**Priority:** ?? High  
**Estimated Time:** 8-10 hours  
**Dependencies:** Workload enum, FacultyWorkLoad model

#### Subtasks:
- [ ] Extend Workload enum
  - Update file: `Data\Models\FacultyWorkLoad.cs`
  - Add new enum values:
    ```csharp
    public enum Workload
    {
        // Teaching (existing)
        Course_Lecture,
        Course_Lab,
        
        // Non-Teaching (new)
        Coordinator_Release,
        Research_Release,
        Chair_Release,
        Sick_Leave,
        Admin_Duties,
        Committee_Work,
        Special_Assignment,
        Professional_Development,
        Sabbatical,
        Other
    }
    ```

- [ ] Create database migration
  - Enum values stored as integers in DB
  - Run: `Add-Migration AddNonTeachingWorkloadTypes`
  - Run: `Update-Database`
  - Verify existing data not affected

- [ ] Update FacultyWorkLoadForm
  - Group workload types in dropdown:
    - **Teaching Workload**
      - Course - Lecture
      - Course - Lab
    - **Release Time**
      - Coordinator Release
      - Research Release
      - Chair Release
    - **Administrative**
      - Admin Duties
      - Committee Work
    - **Other**
      - Sick Leave
      - Special Assignment
      - Professional Development
      - Sabbatical
      - Other
  - Use MudSelect with grouped items
  - Show appropriate icon for each category

- [ ] Update calculation service
  - Modify `GetTeachingHoursAsync()`:
    - Only sum `Course_Lecture` and `Course_Lab`
  - Modify `GetNonTeachingHoursAsync()`:
    - Sum all other workload types
  - Create breakdown method:
    ```csharp
    Task<Dictionary<string, decimal>> GetWorkloadBreakdownAsync(string facultyEmail, int termId)
    ```
    - Returns hours grouped by category

- [ ] Update UI displays
  - FacultyPage: Show teaching vs non-teaching split
  - FacultyWorkLoadPage: Color-code by category
  - Add summary statistics:
    - Total Teaching Hours
    - Total Release Hours
    - Total Administrative Hours
    - Grand Total

#### Files to Modify:
- `WorkloadProject2025\Data\Models\FacultyWorkLoad.cs`
- `WorkloadProject2025\Components\Forms\FacultyWorkLoadForm.razor`
- `WorkloadProject2025\Services\WorkloadCalculationService.cs`
- `WorkloadProject2025\Components\Pages\FacultyPage.razor`
- `WorkloadProject2025\Components\Pages\FacultyWorkLoadPage.razor`

#### Files to Create:
- `WorkloadProject2025\Migrations\{timestamp}_AddNonTeachingWorkloadTypes.cs`

#### Success Criteria:
- [ ] All new workload types available in dropdown
- [ ] Grouped dropdown is user-friendly
- [ ] Teaching vs non-teaching calculated separately
- [ ] Breakdown displayed correctly
- [ ] Icons/colors distinguish categories
- [ ] Migration preserves existing data
- [ ] Reporting shows accurate breakdown

---

### ? Task 3: Implement Workload Category Rules Enforcement
**Priority:** ?? High  
**Estimated Time:** 10-12 hours  
**Dependencies:** WorkloadCategory model, service layer

#### Subtasks:
- [ ] Enhance WorkloadCategoriesService
  - Add validation methods:
    ```csharp
    Task<bool> ValidateDateRangesAsync(WorkloadCategory category);
    Task<WorkloadCategory?> GetCurrentCategoryAsync();
    Task<WorkloadCategory?> GetCategoryForDateAsync(DateTime date);
    Task<bool> HasOverlappingDatesAsync(WorkloadCategory category);
    ```

- [ ] Implement date range validation
  - Rule: No two categories can have overlapping date ranges
  - Rule: Only one category can have null EndDate (current category)
  - Rule: StartDate must be before EndDate
  - Rule: Cannot create category in the past (StartDate >= Today)
  - Rule: Cannot modify historical categories (EndDate < Today)

- [ ] Auto-calculate allowed hours by employment type
  - Create mapping table:
    ```csharp
    public class EmploymentTypeWorkloadRules
    {
        public EmploymentType Type { get; set; }
        public decimal StandardMinHours { get; set; }
        public decimal StandardMaxHours { get; set; }
        public decimal OvertimeThreshold { get; set; }
    }
    ```
  - Example rules:
    - FullTime: 30-40 hours/week, overtime after 40
    - PartTime: 10-25 hours/week, overtime after 25
    - Adjunct: 3-15 hours/week, overtime after 15
    - Visiting: 20-35 hours/week, overtime after 35

- [ ] Update WorkloadCategoriesService.AddAsync()
  - Validate date ranges before insert
  - If EndDate is null:
    - Find current category (existing null EndDate)
    - Set its EndDate to day before new StartDate
  - Throw descriptive exceptions for violations
  - Wrap in transaction for atomicity

- [ ] Add validation in FacultyWorkLoadForm
  - Before saving workload assignment:
    - Get appropriate WorkloadCategory for term dates
    - Get faculty EmploymentType
    - Calculate their allowed hours based on rules
    - Show warning if assignment exceeds limits
    - Require manager approval for overload (future enhancement)

- [ ] Create WorkloadRulesService
  - Central place for business rules
  - Methods:
    ```csharp
    Task<(decimal Min, decimal Max)> GetAllowedHoursAsync(string facultyEmail, int termId);
    Task<bool> RequiresApprovalAsync(string facultyEmail, decimal proposedHours, int termId);
    Task<List<string>> GetViolationsAsync(string facultyEmail, int termId);
    ```

#### Files to Create:
- `WorkloadProject2025\Data\Models\EmploymentTypeWorkloadRules.cs`
- `WorkloadProject2025\Services\IWorkloadRulesService.cs`
- `WorkloadProject2025\Services\WorkloadRulesService.cs`

#### Files to Modify:
- `WorkloadProject2025\Services\WorkloadCategoriesService.cs`
- `WorkloadProject2025\Components\Forms\FacultyWorkLoadForm.razor`
- `WorkloadProject2025\Components\Forms\WorkloadCategoriesForm.razor`
- `WorkloadProject2025\Program.cs` (register new service)

#### Success Criteria:
- [ ] Cannot create overlapping workload categories
- [ ] Only one current category exists (null EndDate)
- [ ] Historical categories cannot be modified
- [ ] Hours calculated based on employment type
- [ ] Validation messages are clear and actionable
- [ ] Form prevents invalid submissions
- [ ] Database integrity maintained with transactions
- [ ] Business rules centralized and testable

---

### ? Task 4: Add Bulk Edit/Mass Update Feature
**Priority:** ? Medium-High  
**Estimated Time:** 15-18 hours  
**Dependencies:** FacultyWorkLoad service, UI components

#### Subtasks:
- [ ] Create BulkEditWorkloadPage
  - New page: `Components\Pages\BulkEditWorkloadPage.razor`
  - Route: `@page "/bulkeditworkload"`
  - Multi-step wizard interface:
    1. **Step 1:** Select workload assignments
    2. **Step 2:** Choose what to edit
    3. **Step 3:** Preview changes
    4. **Step 4:** Confirm and save

- [ ] Step 1: Selection Interface
  - Display all workload assignments in MudDataGrid
  - Enable row selection (checkboxes)
  - Add "Select All" / "Deselect All" buttons
  - Implement filtering:
    - By Faculty (multi-select dropdown)
    - By Term (single select)
    - By Workload Type (multi-select)
    - By Program/Department (cascading dropdowns)
    - By Hours Range (slider: min-max)
    - By Date Assigned (date range picker)
  - Show selected count: "X of Y assignments selected"

- [ ] Step 2: Edit Options
  - Radio button choices:
    - **Update Hours:** Adjust hours for all selected
      - Input: New hours value
      - Option: Add/subtract from existing (e.g., +5 hours)
    - **Change Workload Type:** Bulk change type
      - Dropdown: Select new workload type
    - **Reassign Term:** Move to different term
      - Dropdown: Select target term
    - **Change Category:** Update workload category
      - Dropdown: Select category
    - **Update Date:** Change DateAssigned
      - Date picker
  - Allow multiple changes simultaneously
  - Validation: Ensure changes are valid

- [ ] Step 3: Preview Changes
  - Show side-by-side comparison:
    - Column: Faculty Name
    - Column: Original Hours ? New Hours
    - Column: Original Type ? New Type
    - Column: Original Term ? New Term
  - Calculate impact:
    - Total hours changed
    - Number of assignments affected
    - Warnings for overload situations
  - Allow deselecting individual changes
  - "Back" button to modify selections

- [ ] Step 4: Confirm and Save
  - Create BulkUpdateService
    - Method: `BulkUpdateWorkloadsAsync(List<int> workloadIds, BulkUpdateRequest request)`
    - Wrap in database transaction
    - Validate each update before committing
    - Log all changes for audit trail
  - Show progress indicator during save
  - Handle partial failures gracefully
  - Display detailed success/error report:
    - X assignments updated successfully
    - Y assignments failed (with reasons)
    - Download error log as CSV

- [ ] Add Audit Trail
  - Create WorkloadChangeLog table:
    ```csharp
    public class WorkloadChangeLog
    {
        public int Id { get; set; }
        public int FacultyWorkLoadId { get; set; }
        public string ChangedBy { get; set; } // User email
        public DateTime ChangedAt { get; set; }
        public string ChangeType { get; set; } // "BulkUpdate"
        public string OldValue { get; set; } // JSON
        public string NewValue { get; set; } // JSON
    }
    ```
  - Log every bulk change
  - Create migration

- [ ] Security and Permissions
  - Check if user has permission to bulk edit
  - Restrict to admin/manager roles
  - Add confirmation dialog for large changes (>50 records)

#### Files to Create:
- `WorkloadProject2025\Components\Pages\BulkEditWorkloadPage.razor`
- `WorkloadProject2025\Services\IBulkUpdateService.cs`
- `WorkloadProject2025\Services\BulkUpdateService.cs`
- `WorkloadProject2025\Data\Models\BulkUpdateRequest.cs`
- `WorkloadProject2025\Data\Models\BulkUpdateResult.cs`
- `WorkloadProject2025\Data\Models\WorkloadChangeLog.cs`
- `WorkloadProject2025\Migrations\{timestamp}_AddWorkloadChangeLog.cs`

#### Files to Modify:
- `WorkloadProject2025\Components\Layout\NavMenu.razor`
- `WorkloadProject2025\Program.cs`

#### Success Criteria:
- [ ] Can select multiple workload assignments
- [ ] Filtering works correctly
- [ ] All edit options functional
- [ ] Preview shows accurate changes
- [ ] Changes save in transaction (all or nothing)
- [ ] Audit log records all changes
- [ ] Error handling is robust
- [ ] Performance acceptable for 500+ records
- [ ] UI is intuitive and responsive
- [ ] Confirmation required for large changes

---

## ?? Testing Checklist

### Unit Tests:
- [ ] WorkloadCalculationService calculates correctly
- [ ] Validation rules work for edge cases
- [ ] Date range overlap detection works
- [ ] Employment type rules are accurate
- [ ] Bulk update handles partial failures

### Integration Tests:
- [ ] Calculations match database reality
- [ ] Transactions rollback on error
- [ ] Audit logs are created correctly
- [ ] Warnings trigger at correct thresholds

### Performance Tests:
- [ ] Calculation service handles 1000+ faculty
- [ ] Bulk update processes 500+ records in <5 seconds
- [ ] UI remains responsive during calculations
- [ ] Database queries are optimized (use indexes)

### User Acceptance:
- [ ] Business rules match institutional policy
- [ ] Warning messages are clear
- [ ] Bulk edit workflow is intuitive
- [ ] No data loss during bulk operations

---

## ?? Resources & References

### EF Core:
- [Transactions](https://learn.microsoft.com/en-us/ef/core/saving/transactions)
- [Query Performance](https://learn.microsoft.com/en-us/ef/core/performance/)

### Design Patterns:
- [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [Specification Pattern](https://deviq.com/design-patterns/specification-pattern)

### Blazor:
- [State Management](https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management)
- [Form Validation](https://learn.microsoft.com/en-us/aspnet/core/blazor/forms-and-input-components)

---

## ?? Integration Points

### With Person 1:
- Use EmploymentType enum for workload rules
- Validate against WorkloadCategory in dropdowns
- Show calculation results in faculty cards

### With Person 3:
- Provide calculation service for dashboard metrics
- Support filtering in bulk edit from dashboard
- Export calculation results for reports

---

## ?? Notes & Considerations

- **Performance:** Index FacultyEmail and TermId columns
- **Caching:** Cache workload rules (rarely change)
- **Validation:** Server-side validation is critical
- **Logging:** Log all calculation errors for debugging
- **Business Rules:** Document all rules in code comments
- **Testing:** Create seed data for various scenarios
- **Error Messages:** User-friendly but informative
- **Audit:** Keep change history for compliance
