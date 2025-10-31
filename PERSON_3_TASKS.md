# ?? Person 3: Dashboards, Reporting & Advanced Features

**Focus Area:** Analytics, visualizations, reporting, and advanced user features  
**Priority Level:** ? MEDIUM-HIGH - User Insights & Decision Support  
**Estimated Time:** 3-4 Weeks

---

## ?? Task List

### ? Task 1: Create Workload Dashboard Page (CRITICAL)
**Priority:** ??? Critical  
**Estimated Time:** 15-20 hours  
**Dependencies:** WorkloadCalculationService (Person 2), ApexCharts

#### Subtasks:

#### Part A: Create Main Dashboard Page
- [ ] Create new page: `WorkloadDashboardPage.razor`
  - Route: `@page "/workloaddashboard"`
  - Inject services: `ApplicationDbContext`, `WorkloadCalculationService`
  - Add to navigation menu with icon

- [ ] Design Dashboard Layout
  - Use MudGrid for responsive layout
  - Top section: Summary statistics cards (4 cards)
  - Middle section: Primary charts (2 large charts side-by-side)
  - Bottom section: Secondary charts and tables (2-3 smaller visualizations)
  - Add filter panel at top

#### Part B: Summary Statistics Cards
Create 4 KPI cards showing:

1. **Total Faculty Members**
   - Count by employment type
   - Icon: `@Icons.Material.Filled.Person`
   - Color: Primary
   - Breakdown tooltip:
     - Full-Time: X
     - Part-Time: Y
     - Adjunct: Z

2. **Total Workload Hours**
   - Sum of all assigned hours current term
   - Icon: `@Icons.Material.Filled.Timer`
   - Color: Info
   - Breakdown tooltip:
     - Teaching Hours: X
     - Non-Teaching Hours: Y

3. **Faculty with Overload**
   - Count where hours > max allowed
   - Icon: `@Icons.Material.Filled.Warning`
   - Color: Error
   - Click to filter dashboard to these faculty
   - Show percentage of total

4. **Faculty Under-Assigned**
   - Count where hours < min allowed
   - Icon: `@Icons.Material.Filled.Info`
   - Color: Warning
   - Click to filter dashboard
   - Show percentage of total

#### Part C: Primary Charts (ApexCharts)

**Chart 1: Workload Hours by Faculty (Bar Chart)**
- Horizontal bar chart
- X-axis: Hours
- Y-axis: Faculty names (top 20 by hours)
- Color coding:
  - Green: Within normal range
  - Yellow: Approaching max (90-99%)
  - Orange: At max (100-109%)
  - Red: Overload (>110%)
- Interactive: Click bar to see faculty details
- Options:
  - Sort by: Total Hours / Teaching Hours / Non-Teaching Hours
  - Filter by: Department / Program / Employment Type
  - Export to PNG/SVG

**Chart 2: Workload Distribution by Type (Pie/Donut Chart)**
- Show percentage breakdown:
  - Course - Lecture
  - Course - Lab
  - Coordinator Release
  - Research Release
  - Administrative
  - Other
- Legend with hour counts
- Interactive: Click slice to filter dashboard
- Center text: Total hours
- Color scheme: Use distinct, accessible colors

#### Part D: Secondary Visualizations

**Chart 3: Workload Trends by Term (Line Chart)**
- X-axis: Terms (last 5 terms)
- Y-axis: Total hours
- Multiple lines:
  - Total Workload (solid line)
  - Teaching Hours (dashed line)
  - Non-Teaching Hours (dotted line)
- Show average line (horizontal reference)
- Responsive: Works on mobile

**Chart 4: Department Comparison (Grouped Bar Chart)**
- X-axis: Departments
- Y-axis: Average hours per faculty
- Grouped bars:
  - Current term
  - Previous term
  - Institutional average
- Color coded by status (normal/overload)

**Table: Top Assignments**
- MudTable showing:
  - Faculty Name
  - Total Hours
  - Number of Assignments
  - Workload Status (chip with color)
  - Last Updated
- Sortable columns
- Pagination (10 per page)
- Search/filter
- Click row to navigate to faculty detail page

#### Part E: Filter Panel
- [ ] Add comprehensive filter controls:
  - **Term Selector** (dropdown, default: current term)
  - **Department Filter** (multi-select)
  - **Program Filter** (multi-select, cascades from department)
  - **Employment Type Filter** (multi-select checkboxes)
  - **Workload Status Filter** (checkboxes):
    - Normal
    - Underload
    - Approaching Max
    - Overload
  - **Hours Range Slider** (0-80 hours)
  - **Apply Filters** button
  - **Reset Filters** button

- [ ] Filter persistence:
  - Save filter state in local storage
  - Restore on page reload
  - URL parameters for sharing filtered views

#### Part F: Data Loading & Performance
- [ ] Implement efficient data loading:
  ```csharp
  private async Task LoadDashboardDataAsync()
  {
      var termId = selectedTermId ?? await GetCurrentTermIdAsync();
      
      // Load in parallel
      var facultyTask = _context.Faculty.ToListAsync();
      var workloadsTask = _context.FacultyWorkLoads
          .Include(w => w.Faculty)
          .Include(w => w.Course)
          .Where(w => w.TermId == termId)
          .ToListAsync();
      var categoriesTask = _context.WorkloadCategories.ToListAsync();
      
      await Task.WhenAll(facultyTask, workloadsTask, categoriesTask);
      
      faculty = await facultyTask;
      workloads = await workloadsTask;
      categories = await categoriesTask;
      
      await CalculateMetrics();
  }
  ```

- [ ] Add loading states:
  - Show MudProgressCircular while loading
  - Skeleton screens for charts
  - Graceful error handling

- [ ] Caching strategy:
  - Cache chart data for 5 minutes
  - Refresh button to force reload
  - Auto-refresh every 10 minutes (optional)

#### Files to Create:
- `WorkloadProject2025\Components\Pages\WorkloadDashboardPage.razor`
- `WorkloadProject2025\Data\Models\WorkloadDashboardMetrics.cs`
- `WorkloadProject2025\Services\IDashboardService.cs`
- `WorkloadProject2025\Services\DashboardService.cs`

#### Files to Modify:
- `WorkloadProject2025\Components\Layout\NavMenu.razor`
- `WorkloadProject2025\Program.cs`

#### Success Criteria:
- [ ] All 4 summary cards display correctly
- [ ] All 4 charts render with real data
- [ ] Filters work and update all visualizations
- [ ] Click interactions work (drill-down)
- [ ] Page loads in <3 seconds
- [ ] Responsive on mobile/tablet/desktop
- [ ] Export functionality works
- [ ] No console errors
- [ ] Accessible (screen readers, keyboard navigation)

---

### ? Task 2: Implement Advanced Filtering & Search
**Priority:** ?? High  
**Estimated Time:** 10-12 hours  
**Dependencies:** FacultyWorkLoadPage, search services

#### Subtasks:

- [ ] Create FilterPanel Component
  - Reusable component: `Components\Shared\WorkloadFilterPanel.razor`
  - Can be used on multiple pages
  - Emits event when filters change
  - Supports saving/loading filter presets

- [ ] Implement Filter Options:

  **1. Term Filter**
  - Single select dropdown
  - Options: All Terms / Current Term / Specific Term
  - Default: Current Term

  **2. Workload Type Filter**
  - Multi-select checkboxes with "Select All"
  - Group by category:
    - Teaching (Lecture, Lab)
    - Release Time (Coordinator, Research, Chair)
    - Administrative (Admin Duties, Committee)
    - Other
  - Count displayed next to each option

  **3. Faculty Employment Type Filter**
  - Multi-select checkboxes
  - Options: Full-Time, Part-Time, Adjunct, etc.
  - Show count in each category
  - Color-coded badges

  **4. Program/Department Filter**
  - Cascading dropdowns:
    - First select Department (multi-select)
    - Then Programs filtered by selected departments
  - "Clear All" button
  - Search within dropdown (MudAutocomplete)

  **5. Hours Range Filter**
  - Dual-handle slider (MudSlider)
  - Min: 0, Max: 80 (or max from data)
  - Display current range: "15 - 40 hours"
  - Real-time update as slider moves

  **6. Date Assigned Filter**
  - Date range picker (MudDateRangePicker)
  - Presets:
    - This Week
    - This Month
    - Last 30 Days
    - This Term
    - Custom Range

  **7. Status Filter**
  - Checkboxes:
    - Normal (green)
    - Underload (yellow)
    - Approaching Max (orange)
    - Overload (red)
  - Icons with colors
  - Count per status

- [ ] Add Filter Presets Feature
  - Allow saving current filter combination with name
  - Store in local storage or database
  - Dropdown to select saved presets
  - "Manage Presets" dialog:
    - List all saved presets
    - Edit/Delete/Share presets
  - Default presets:
    - "All Overloaded Faculty"
    - "Part-Time Current Term"
    - "Unassigned Faculty"

- [ ] Update FacultyWorkLoadPage
  - Add filter panel above workload cards
  - Apply filters to workload list
  - Show active filter chips (removable)
  - Display result count: "Showing X of Y assignments"
  - Clear all filters button

- [ ] Implement Search Functionality
  - Global search box (MudTextField with debounce)
  - Search across:
    - Faculty name
    - Faculty email
    - Course name
    - Workload description
  - Highlight matching terms in results
  - Search suggestions dropdown (autocomplete)

- [ ] Add Export Filtered Results
  - **Export to Excel** button
    - Install package: `ClosedXML` or `EPPlus`
    - Export currently filtered/searched results
    - Include all columns + calculations
    - Formatted with colors matching UI
  - **Export to CSV** button
    - Simple comma-separated format
    - Can open in Excel/Google Sheets
  - **Print View** button
    - Printer-friendly page layout
    - Hides filters/navigation
    - Includes date/time generated

- [ ] Performance Optimization
  - Use `IQueryable` for database filtering
  - Apply filters before loading data
  - Pagination for large result sets
  - Virtual scrolling for long lists

#### Files to Create:
- `WorkloadProject2025\Components\Shared\WorkloadFilterPanel.razor`
- `WorkloadProject2025\Data\Models\WorkloadFilterOptions.cs`
- `WorkloadProject2025\Data\Models\FilterPreset.cs`
- `WorkloadProject2025\Services\IExportService.cs`
- `WorkloadProject2025\Services\ExportService.cs`

#### Files to Modify:
- `WorkloadProject2025\Components\Pages\FacultyWorkLoadPage.razor`
- `WorkloadProject2025\Program.cs`
- `WorkloadProject2025\WorkloadProject2025.csproj` (add NuGet packages)

#### Success Criteria:
- [ ] All filter options work correctly
- [ ] Filters combine properly (AND logic)
- [ ] Search is fast (<500ms)
- [ ] Export includes filtered data only
- [ ] Filter presets save and load
- [ ] Clear filters resets everything
- [ ] Active filters displayed as chips
- [ ] Result count accurate
- [ ] Performance good with 1000+ records

---

### ? Task 3: Create Instructor-Course Assignment View
**Priority:** ?? High  
**Estimated Time:** 12-15 hours  
**Dependencies:** Course enrollment data, inline editing

#### Subtasks:

- [ ] Create Assignment Matrix Page
  - New page: `InstructorCourseAssignmentPage.razor`
  - Route: `@page "/instructorcourseassignment"`
  - Add to navigation menu

- [ ] Design Matrix Layout
  - Use MudTable or custom CSS Grid
  - **Rows:** Faculty members (sorted by name)
  - **Columns:** Courses (grouped by program)
  - **Cells:** Show workload assignments
  - Sticky headers (row and column)
  - Responsive: horizontal scroll on mobile

- [ ] Cell Content Display
  - Show if faculty teaches course:
    - **Hours Assigned** (large, bold)
    - **Workload Type** icon (Lecture/Lab badge)
    - **Color coding** based on workload status
  - Empty cells: Show "+" icon to add assignment
  - Hover tooltip:
    - Faculty name + Course name
    - Term information
    - Current total hours for faculty
    - Warning if would exceed max

- [ ] Implement Inline Editing
  - Click cell to edit:
    - Input: Hours (numeric)
    - Dropdown: Workload type
    - Checkbox: Co-teaching (if applicable)
  - **Save** button (checkmark icon)
  - **Cancel** button (X icon)
  - Validate before saving:
    - Hours must be positive
    - Check for overload
    - Show warning dialog if issues
  - Auto-save option (toggle)

- [ ] Add Course Enrollment Display
  - Extend Course model:
    ```csharp
    public class Course
    {
        // Existing properties...
        public int? EnrollmentCount { get; set; }
        public int? MaxEnrollment { get; set; }
        public string? ClassTimes { get; set; } // e.g., "MWF 9:00-10:00"
    }
    ```
  - Migration to add new properties
  - Show in column header:
    - Course name
    - Enrollment: 25/30 (current/max)
    - Class times
  - Color-code enrollment:
    - Green: < 75% capacity
    - Yellow: 75-90%
    - Orange: 90-100%
    - Red: At capacity or overenrolled

- [ ] Filtering and Grouping
  - Filter by:
    - Term (dropdown)
    - Department (multi-select)
    - Program (multi-select)
    - Faculty employment type
    - Course with/without assignments
  - Group courses by:
    - Program (default)
    - Department
    - Course level (100-level, 200-level, etc.)
  - Collapsible groups

- [ ] Summary Statistics
  - Row totals (per faculty):
    - Total assignments
    - Total hours
    - Status indicator (color)
  - Column totals (per course):
    - Number of instructors
    - Total hours allocated
    - Coverage status (adequate/understaffed/overstaffed)
  - Grand total: Total hours all assignments

- [ ] Bulk Assignment Features
  - Select multiple cells (shift+click)
  - Apply same workload type to selected
  - Copy assignment pattern to another term
  - Assign co-teachers quickly

- [ ] Conflict Detection
  - Highlight scheduling conflicts:
    - Faculty assigned to overlapping class times
    - Faculty overload (hours exceed max)
    - Course with no instructor assigned
  - Toggle to show only conflicts
  - Export conflict report

#### Files to Create:
- `WorkloadProject2025\Components\Pages\InstructorCourseAssignmentPage.razor`
- `WorkloadProject2025\Components\Shared\AssignmentMatrixCell.razor`
- `WorkloadProject2025\Data\Models\AssignmentMatrixData.cs`
- `WorkloadProject2025\Migrations\{timestamp}_AddEnrollmentToCourse.cs`

#### Files to Modify:
- `WorkloadProject2025\Data\Models\Course.cs`
- `WorkloadProject2025\Components\Layout\NavMenu.razor`

#### Success Criteria:
- [ ] Matrix displays correctly with all data
- [ ] Inline editing works smoothly
- [ ] Enrollment data displays and updates
- [ ] Filters work correctly
- [ ] Conflict detection identifies issues
- [ ] Performance acceptable (500+ courses)
- [ ] Responsive on tablet (main use case)
- [ ] Changes save to database correctly
- [ ] Validation prevents invalid assignments

---

### ? Task 4: Add Historical Workload Records Access
**Priority:** ? Medium  
**Estimated Time:** 12-15 hours  
**Dependencies:** Reporting service, PDF generation

#### Subtasks:

- [ ] Create Historical View Page
  - New page: `WorkloadHistoryPage.razor`
  - Route: `@page "/workloadhistory"`
  - Add to navigation menu

- [ ] Display Options
  - Tab 1: **Faculty History**
    - Select faculty member
    - Show workload for last 5 years
    - Timeline view (visual)
  - Tab 2: **Term Comparison**
    - Select multiple terms to compare
    - Side-by-side comparison
  - Tab 3: **Trend Analysis**
    - Department/Program trends over time
    - Charts and graphs

- [ ] Faculty History View
  - Faculty selector (dropdown with search)
  - Year selector (checkboxes for last 5 years)
  - Display as cards or table:
    - Term name and dates
    - Total hours worked
    - Breakdown by workload type
    - Courses taught
    - Status (normal/overload)
  - Timeline visualization:
    - Horizontal timeline
    - Each term as point on timeline
    - Size represents hours
    - Color represents status
    - Click point to see details

- [ ] Term Comparison Tool
  - Select 2-4 terms to compare
  - Display comparison table:
    - Rows: Faculty members
    - Columns: Selected terms
    - Cells: Hours and status
  - Highlight changes:
    - Increase (green up arrow)
    - Decrease (red down arrow)
    - No change (gray dash)
  - Summary statistics:
    - Average change
    - Biggest increase/decrease
    - Most consistent faculty
  - Export comparison as Excel

- [ ] Trend Analysis
  - Charts showing:
    - **Line Chart:** Average workload per term (last 10 terms)
    - **Stacked Area Chart:** Workload by type over time
    - **Heatmap:** Faculty workload intensity by term
  - Filters:
    - By department
    - By program
    - By employment type
  - Detect trends:
    - Increasing/decreasing workload
    - Seasonality patterns
    - Anomalies (outliers)

- [ ] Implement PDF Report Generation
  - Install package: `QuestPDF` or `iTextSharp`
  - Create report templates:
    - **Faculty Workload History Report**
      - Cover page with faculty details
      - Summary of 5-year workload
      - Term-by-term breakdown with charts
      - Recommendations section
    - **Department Annual Report**
      - All faculty in department
      - Aggregate statistics
      - Charts and visualizations
      - Trend analysis
  - Report customization:
    - Select date range
    - Choose sections to include
    - Add custom notes/comments
  - Generate and download PDF
  - Email report (optional)

- [ ] Create Archive Management
  - Archive old workload data (>5 years)
  - Keep in separate table: `ArchivedFacultyWorkLoads`
  - Archive service:
    ```csharp
    Task ArchiveOldDataAsync(DateTime cutoffDate);
    Task RetrieveArchivedDataAsync(int year);
    Task RestoreArchivedDataAsync(int year);
    ```
  - Admin page for archive management
  - Data retention policy settings

- [ ] Performance Optimization
  - Index historical data tables
  - Use materialized views for aggregates
  - Cache frequently accessed reports
  - Pagination for large datasets
  - Background jobs for report generation

#### Files to Create:
- `WorkloadProject2025\Components\Pages\WorkloadHistoryPage.razor`
- `WorkloadProject2025\Services\IReportingService.cs`
- `WorkloadProject2025\Services\ReportingService.cs`
- `WorkloadProject2025\Services\IPdfGenerationService.cs`
- `WorkloadProject2025\Services\PdfGenerationService.cs`
- `WorkloadProject2025\Data\Models\ArchivedFacultyWorkLoad.cs`
- `WorkloadProject2025\Data\Models\WorkloadHistoryData.cs`
- `WorkloadProject2025\Migrations\{timestamp}_CreateArchivedWorkloadsTable.cs`

#### Files to Modify:
- `WorkloadProject2025\Components\Layout\NavMenu.razor`
- `WorkloadProject2025\Program.cs`
- `WorkloadProject2025\WorkloadProject2025.csproj` (add NuGet packages)

#### Success Criteria:
- [ ] Can view 5+ years of history
- [ ] Comparison tool works correctly
- [ ] Trend charts display properly
- [ ] PDF reports generate successfully
- [ ] PDF reports are well-formatted
- [ ] Archive/restore works without data loss
- [ ] Performance acceptable for historical queries
- [ ] Reports downloadable and printable
- [ ] Email functionality works (if implemented)

---

## ?? Testing Checklist

### Dashboard Testing:
- [ ] All charts render correctly
- [ ] Filters apply to all visualizations
- [ ] Data accuracy verified against database
- [ ] Performance acceptable on slow connections
- [ ] Mobile responsive

### Export Testing:
- [ ] Excel exports open without errors
- [ ] CSV format is correct
- [ ] PDF formatting is professional
- [ ] All data included in exports

### Historical Data:
- [ ] 5-year history accessible
- [ ] Comparisons calculate correctly
- [ ] Trends are accurate
- [ ] Archive/restore preserves data integrity

---

## ?? Resources & References

### ApexCharts:
- [Blazor ApexCharts Documentation](https://apexcharts.github.io/Blazor-ApexCharts/)
- [Chart Types](https://apexcharts.com/docs/chart-types/)

### PDF Generation:
- [QuestPDF](https://www.questpdf.com/)
- [iTextSharp/iText7](https://itextpdf.com/en)

### Excel Export:
- [ClosedXML](https://github.com/ClosedXML/ClosedXML)
- [EPPlus](https://epplussoftware.com/)

### Data Visualization:
- [Chart.js](https://www.chartjs.org/) (alternative)
- [Plotly](https://plotly.com/graphing-libraries/) (advanced)

---

## ?? Integration Points

### With Person 1:
- Use employment type for filtering
- Display faculty notes in reports
- Show dropdown selections in exports

### With Person 2:
- Use WorkloadCalculationService for metrics
- Display validation warnings in dashboard
- Include calculation results in reports

---

## ?? Notes & Considerations

- **Accessibility:** Ensure charts have text alternatives
- **Color Blindness:** Use patterns + colors in charts
- **Performance:** Lazy load charts (render when visible)
- **Caching:** Cache dashboard data for 5 minutes
- **Print Styles:** CSS for print-friendly layouts
- **Localization:** Consider date/number formats
- **Error Handling:** Graceful degradation if chart fails
- **Browser Support:** Test in Chrome, Firefox, Edge
- **Mobile:** Charts responsive on small screens
- **Loading States:** Show skeletons while loading
