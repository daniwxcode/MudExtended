# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial project structure
- Base configuration (.csproj, _Imports.razor)
- CSS base file

---

## [1.0.0] - 2025-XX-XX

### Added

#### Components
- `AuthorizedComponentBase` - Base class with permission checking
- `PaginatedServerTable<TEntity>` - Server-side paginated table with search and CRUD
- `EntityFormDialog<TEntity, TCreate, TUpdate>` - Generic form dialog
- `ConfirmationDialog` - Confirmation dialog with severity levels
- `MessageDialog` - Message box with Info/Success/Warning/Error types
- `AssetCard<TAsset>` - Asset card with CRUD actions
- `StatCard` - Statistical card with value formatting
- `StatCardGrid` - Grid of stat cards
- `StatusBadge<TStatus>` - Generic status badge with mappings
- `GenericSkeleton` - Configurable skeleton with 8 layouts
- `LoadingOverlay` - Global loading overlay
- `PageHeader` - Standardized page header
- `PaginationInfo` - Pagination information display
- `SignalRSubscriber` - SignalR event subscription

#### Services
- `IApiExecutor` / `ApiExecutor` - API execution with error handling
- `IPermissionService` / `PermissionService` - Permission checking service
- `LoaderService` - Global loader management

#### Models
- `PermissionSet` - Permission set model
- `PaginationState` - Pagination state
- `PaginationFilter` - Pagination filter for API calls
- `PagedResult<T>` - Paginated result wrapper
- `TableLocalization` - Table text localization
- `DialogConfiguration` - Dialog configuration
- `MudExtendedOptions` - Library configuration options
- `StatCardData` - Stat card data model

#### Mappings
- `IStatusMappingProvider<TStatus>` - Status mapping interface
- `StatusMappingRegistry` - Centralized mapping registry

#### Extensions
- `ServiceCollectionExtensions` - `AddMudExtended()` extension
- `DialogServiceExtensions` - Dialog service extensions (ConfirmAsync, ShowMessageAsync, etc.)

#### Enums
- `SkeletonLayout` - Skeleton layout types
- `ConfirmationType` - Confirmation types (Info, Warning, Danger)
- `MessageSeverity` - Message severity (Info, Success, Warning, Error)
- `StatValueType` - Stat value types (Number, Currency, Percentage, Decimal, Raw)
