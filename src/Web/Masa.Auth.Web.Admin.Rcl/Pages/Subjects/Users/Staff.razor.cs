﻿namespace Masa.Auth.Web.Admin.Rcl.Pages.Subjects.Users;

public partial class Staff
{
    private string? _search;
    private bool? _enabled;
    private int _page = 1;
    private int _pageSize = 10;

    public string Search
    {
        get { return _search ?? ""; }
        set
        {
            _search = value;
            GetStaffAsync().ContinueWith(_ => InvokeAsync(StateHasChanged));
        }
    }

    public bool? Enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            GetStaffAsync().ContinueWith(_ => InvokeAsync(StateHasChanged));
        }
    }

    public int Page
    {
        get { return _page; }
        set
        {
            _page = value;
            GetStaffAsync().ContinueWith(_ => InvokeAsync(StateHasChanged));
        }
    }

    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            _pageSize = value;
            GetStaffAsync().ContinueWith(_ => InvokeAsync(StateHasChanged));
        }
    }

    public long Total { get; set; }

    public List<int> PageSizes = new() { 10, 25, 50, 100 };

    public List<StaffDto> Staffs { get; set; } = new();

    public StaffDto CurrentStaff { get; set; } = new();

    public List<DataTableHeader<StaffDto>> Headers { get; set; } = new();

    public bool AddStaffDialogVisible { get; set; }

    public bool UpdateStaffDialogVisible { get; set; }

    public bool AuthorizeDialogVisible { get; set; }

    public bool StaffSyncDialogVisible { get; set; }

    private StaffService StaffService => AuthCaller.StaffService;

    protected override async Task OnInitializedAsync()
    {
        Headers = new()
        {
            new() { Text = T(nameof(UserDto.Avatar)), Value = nameof(UserDto.Avatar), Sortable = false },
            new() { Text = T("Department"), Value = nameof(StaffDto.Department), Sortable = false },
            new() { Text = T(nameof(StaffDto.JobNumber)), Value = nameof(StaffDto.JobNumber), Sortable = false },
            new() { Text = T(nameof(StaffDto.Position)), Value = nameof(StaffDto.Position), Sortable = false },
            new() { Text = T("State"), Value = nameof(StaffDto.Enabled), Sortable = false },
            new() { Text = T("Action"), Value = "Action", Sortable = false },
        };

        await GetStaffAsync();
    }

    public async Task GetStaffAsync()
    {
        Loading = true;
        var request = new GetStaffsDto(Page, PageSize, Search, Enabled);
        var response = await StaffService.GetListAsync(request);
        Staffs = response.Items;
        Total = response.Total;
        Loading = false;
    }

    public void OpenAddStaffDialog()
    {
        AddStaffDialogVisible = true;
    }

    public void OpenUpdateStaffDialog(StaffDto staff)
    {
        CurrentStaff = staff;
        UpdateStaffDialogVisible = true;
    }

    public void OpenAuthorizeDialog(StaffDto staff)
    {
        CurrentStaff = staff;
        AuthorizeDialogVisible = true;
    }
}

