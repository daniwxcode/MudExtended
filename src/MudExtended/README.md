# MudExtended

[![NuGet](https://img.shields.io/nuget/v/MudExtended.svg)](https://www.nuget.org/packages/MudExtended/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MudExtended.svg)](https://www.nuget.org/packages/MudExtended/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub](https://img.shields.io/github/stars/daniwxcode/MudExtended?style=social)](https://github.com/daniwxcode/MudExtended)

**MudExtended** is a powerful extension library for [MudBlazor](https://mudblazor.com/) that provides reusable components for common enterprise patterns like CRUD tables, dialogs, status badges, loading states, and SignalR integration.

## ? Features

- ??? **Server-side paginated tables** with search, sorting, and CRUD actions
- ?? **Generic form dialogs** for create/edit operations
- ? **Confirmation dialogs** with severity levels
- ?? **Message dialogs** (Info, Success, Warning, Error)
- ??? **Status badges** with customizable mappings
- ? **Loading skeletons** with multiple layouts
- ?? **Authorization-aware components** with permission sets
- ?? **SignalR integration** for real-time updates
- ?? **Stat cards** for dashboards

## ?? Installation

```bash
dotnet add package MudExtended
```

## ?? Configuration

### 1. Program.cs

```csharp
using MudExtended.Extensions;

// Add MudBlazor (required)
builder.Services.AddMudServices();

// Add MudExtended
builder.Services.AddMudExtended(options =>
{
    options.Culture = CultureInfo.GetCultureInfo("en-US");
    options.UseGlobalLoader = true;
    options.CurrencySymbol = "$";
    options.DateFormat = "MM/dd/yyyy";
    options.DefaultTableLocalization = new TableLocalization
    {
        AddButton = "Add",
        SearchPlaceholder = "Search...",
        NoRecordsMessage = "No records found"
    };
});
```

### 2. index.html / App.razor

```html
<!-- In <head> -->
<link href="_content/MudBlazor/MudBlazor.min.css" rel="stylesheet" />
<link href="_content/MudExtended/css/mudextended.css" rel="stylesheet" />

<!-- Before </body> -->
<script src="_content/MudBlazor/MudBlazor.min.js"></script>
```

### 3. _Imports.razor

```razor
@using MudExtended.Components.Tables
@using MudExtended.Components.Dialogs
@using MudExtended.Components.Cards
@using MudExtended.Components.Status
@using MudExtended.Components.Loading
@using MudExtended.Components.Layout
@using MudExtended.Extensions
```

## ?? Components

### Tables

| Component | Description |
|-----------|-------------|
| `PaginatedServerTable<TEntity>` | Server-side paginated table with search, sorting, and CRUD |

### Dialogs

| Component | Description |
|-----------|-------------|
| `ConfirmationDialog` | Confirmation dialog (Info/Warning/Danger) |
| `MessageDialog` | Message box (Info/Success/Warning/Error) |
| `EntityFormDialog<TEntity, TCreate, TUpdate>` | Generic CRUD form dialog |

### Cards

| Component | Description |
|-----------|-------------|
| `AssetCard<TAsset>` | Asset card with CRUD actions |
| `StatCard` | Statistical card with formatting |
| `StatCardGrid` | Grid of stat cards |

### Status

| Component | Description |
|-----------|-------------|
| `StatusBadge<TStatus>` | Colored status badge with icon |

### Loading

| Component | Description |
|-----------|-------------|
| `GenericSkeleton` | Configurable skeleton (Card, Table, Form, etc.) |
| `LoadingOverlay` | Global loading overlay |

### Layout

| Component | Description |
|-----------|-------------|
| `PageHeader` | Standardized page header |
| `PaginationInfo` | Pagination information display |

### Realtime

| Component | Description |
|-----------|-------------|
| `SignalRSubscriber` | Simplified SignalR subscription |

## ?? Usage Examples

### PaginatedServerTable

```razor
<PaginatedServerTable TEntity="ProductDto"
                      ResourceName="Products"
                      SearchFunc="SearchProductsAsync"
                      CreateDialogType="typeof(ProductFormDialog)"
                      EditDialogType="typeof(ProductFormDialog)"
                      DeleteFunc="DeleteProductAsync"
                      IdFunc="p => p.Id"
                      Title="Products">
    <Columns>
        <MudTh>Name</MudTh>
        <MudTh>Price</MudTh>
        <MudTh>Category</MudTh>
    </Columns>
    <RowTemplate>
        <MudTd>@context.Name</MudTd>
        <MudTd>@context.Price.ToString("C")</MudTd>
        <MudTd>@context.Category</MudTd>
    </RowTemplate>
</PaginatedServerTable>
```

### Dialogs

```csharp
// Confirmation
if (await DialogService.ConfirmDeleteAsync("this product"))
{
    await DeleteProductAsync(product.Id);
}

// Warning confirmation
if (await DialogService.ConfirmWarningAsync("This action cannot be undone."))
{
    await PerformAction();
}

// Message boxes
await DialogService.ShowInfoAsync("Operation completed successfully.");
await DialogService.ShowSuccessAsync("Product saved!");
await DialogService.ShowWarningAsync("Low stock warning.");
await DialogService.ShowErrorAsync("Failed to save product.");

// Error with details
await DialogService.ShowErrorWithDetailsAsync(
    "Connection failed",
    "Timeout after 30 seconds. Check your network connection.");

// Yes/No question
if (await DialogService.ShowYesNoAsync("Save changes before closing?"))
{
    await SaveAsync();
}
```

### GenericSkeleton

```razor
@* Card grid *@
<GenericSkeleton Layout="SkeletonLayout.CardGrid" Count="6" Columns="3" />

@* Table *@
<GenericSkeleton Layout="SkeletonLayout.Table" TableColumns="5" TableRows="10" ShowSearch />

@* Dashboard *@
<GenericSkeleton Layout="SkeletonLayout.Dashboard" Count="4" />

@* Custom template *@
<GenericSkeleton Layout="SkeletonLayout.Custom" Count="3">
    <ItemTemplate>
        <MudCard Class="ma-2">
            <MudSkeleton Width="100%" Height="100px" />
        </MudCard>
    </ItemTemplate>
</GenericSkeleton>
```

### StatusBadge

```razor
@* Register a status mapping provider *@
@code {
    // In Program.cs:
    // builder.Services.AddStatusMappingProvider<OrderStatus, OrderStatusMappingProvider>();
}

@* Use in component *@
<StatusBadge TStatus="OrderStatus" Status="@order.Status" />

@* Customize *@
<StatusBadge TStatus="OrderStatus" 
             Status="@order.Status" 
             ShowIcon="false" 
             Size="Size.Medium"
             Variant="Variant.Outlined" />
```

### StatCards

```razor
<StatCardGrid Cards="@dashboardCards" />

@code {
    private List<StatCardData> dashboardCards = new()
    {
        new() { Label = "Revenue", Value = 150000, ValueType = StatValueType.Currency, Color = Color.Success },
        new() { Label = "Orders", Value = 1234, Color = Color.Info, Icon = Icons.Material.Filled.ShoppingCart },
        new() { Label = "Conversion", Value = 87.5, ValueType = StatValueType.Percentage, Color = Color.Warning }
    };
}
```

## ?? Services

### IApiExecutor

```csharp
[Inject] private IApiExecutor ApiExecutor { get; set; } = default!;

// Simple execution with error handling
var result = await ApiExecutor.ExecuteAsync(
    () => ApiClient.GetProductsAsync(),
    new() { SuccessMessage = "Products loaded" });

// With confirmation
var deleted = await ApiExecutor.ExecuteWithConfirmAsync(
    () => ApiClient.DeleteProductAsync(id),
    "Confirm",
    "Delete this product?",
    ConfirmationType.Danger);
```

### LoaderService

```csharp
[Inject] private LoaderService Loader { get; set; } = default!;

// Manual
Loader.Show();
try { /* ... */ }
finally { Loader.Hide(); }

// Automatic
var result = await Loader.WithLoadingAsync(async ct => 
    await ApiClient.GetDataAsync(ct));
```

## ?? Customization

### Status Mapping Provider

```csharp
public class OrderStatusMappingProvider : IStatusMappingProvider<OrderStatus>
{
    public Color GetColor(OrderStatus status) => status switch
    {
        OrderStatus.Pending => Color.Warning,
        OrderStatus.Processing => Color.Info,
        OrderStatus.Shipped => Color.Primary,
        OrderStatus.Delivered => Color.Success,
        OrderStatus.Cancelled => Color.Error,
        _ => Color.Default
    };

    public string GetIcon(OrderStatus status) => status switch
    {
        OrderStatus.Pending => Icons.Material.Filled.HourglassEmpty,
        OrderStatus.Processing => Icons.Material.Filled.Sync,
        OrderStatus.Shipped => Icons.Material.Filled.LocalShipping,
        OrderStatus.Delivered => Icons.Material.Filled.CheckCircle,
        OrderStatus.Cancelled => Icons.Material.Filled.Cancel,
        _ => Icons.Material.Filled.Help
    };

    public string GetLabel(OrderStatus status) => status switch
    {
        OrderStatus.Pending => "Pending",
        OrderStatus.Processing => "Processing",
        OrderStatus.Shipped => "Shipped",
        OrderStatus.Delivered => "Delivered",
        OrderStatus.Cancelled => "Cancelled",
        _ => status.ToString()
    };
}

// Register in Program.cs
builder.Services.AddStatusMappingProvider<OrderStatus, OrderStatusMappingProvider>();
```

### Table Localization

```csharp
builder.Services.AddMudExtended(options =>
{
    options.DefaultTableLocalization = new TableLocalization
    {
        AddButton = "New",
        RefreshButton = "Refresh",
        SearchPlaceholder = "Search...",
        ActionsColumn = "Actions",
        EditAction = "Edit",
        DeleteAction = "Delete",
        NoRecordsMessage = "No records found",
        ConfirmDeleteTitle = "Confirm Deletion",
        ConfirmDeleteMessage = "Are you sure you want to delete this item?"
    };
});
```

## ?? Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ?? Acknowledgments

- [MudBlazor](https://mudblazor.com/) - The amazing Blazor component library this extends
- All contributors who help improve this library
