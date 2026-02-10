# Contributing to MudExtended

Thank you for your interest in contributing to MudExtended! This document provides guidelines and instructions for contributing.

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Basic knowledge of Blazor and MudBlazor

### Setting Up the Development Environment

1. **Fork the repository**
   ```bash
   git clone https://github.com/YOUR-USERNAME/MudExtended.git
   cd MudExtended
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the documentation site**
   ```bash
   cd docs/MudExtended.Docs
   dotnet run
   ```

## 📁 Project Structure

```
MudExtended/
├── src/
│   └── MudExtended/           # Main library
│       ├── Components/        # Blazor components
│       ├── Services/          # Services and utilities
│       ├── Models/            # Data models
│       ├── Extensions/        # Extension methods
│       └── Enums/             # Enumerations
├── docs/
│   └── MudExtended.Docs/      # Documentation website
├── README.md
├── LICENSE
├── CHANGELOG.md
└── CONTRIBUTING.md
```

## 🔧 Development Guidelines

### Code Style

- Follow the `.editorconfig` rules in the repository
- Use file-scoped namespaces
- Prefer `var` when the type is apparent
- Use expression-bodied members when appropriate
- Add XML documentation comments to public APIs

### Naming Conventions

- **Components**: PascalCase (e.g., `PaginatedServerTable`)
- **Parameters**: PascalCase (e.g., `ShowIcon`)
- **Private fields**: `_camelCase` with underscore prefix
- **Methods**: PascalCase with verb (e.g., `LoadDataAsync`)

### Component Guidelines

1. **Parameters**: Use `[Parameter]` attribute with XML documentation
2. **Cascading Values**: Document expected cascading parameters
3. **Events**: Use `EventCallback<T>` for component events
4. **Rendering**: Implement `ShouldRender()` when appropriate for performance

### Example Component Template

```csharp
/// <summary>
/// Description of the component.
/// </summary>
public partial class MyComponent : ComponentBase
{
    /// <summary>
    /// Description of the parameter.
    /// </summary>
    [Parameter]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Event raised when action occurs.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnAction { get; set; }

    private bool _isLoading;

    private async Task HandleActionAsync()
    {
        _isLoading = true;
        try
        {
            await OnAction.InvokeAsync(Title);
        }
        finally
        {
            _isLoading = false;
        }
    }
}
```

## 📝 Pull Request Process

### Before Submitting

1. **Create an issue** first to discuss the change (for significant changes)
2. **Write tests** if applicable
3. **Update documentation** if you're changing public APIs
4. **Add to CHANGELOG.md** under `[Unreleased]`

### Submitting a Pull Request

1. Create a feature branch from `main`:
   ```bash
   git checkout -b feature/my-amazing-feature
   ```

2. Make your changes and commit:
   ```bash
   git add .
   git commit -m "feat: add amazing feature"
   ```

3. Push to your fork:
   ```bash
   git push origin feature/my-amazing-feature
   ```

4. Open a Pull Request against `main`

### Commit Message Format

We follow [Conventional Commits](https://www.conventionalcommits.org/):

- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `style:` - Code style changes (formatting, etc.)
- `refactor:` - Code refactoring
- `test:` - Adding or updating tests
- `chore:` - Maintenance tasks

Examples:
```
feat: add StatusBadge component
fix: resolve pagination reset issue
docs: update installation instructions
```

## 🧪 Testing

### Running Tests

```bash
dotnet test
```

### Writing Tests

- Place tests in `tests/MudExtended.Tests/`
- Use xUnit for test framework
- Use bUnit for component testing
- Name tests: `MethodName_Scenario_ExpectedResult`

## 📚 Documentation

When adding or modifying components:

1. **Add XML documentation** to all public members
2. **Create/update documentation page** in `docs/MudExtended.Docs/Pages/Components/`
3. **Include examples** with source code
4. **Update README.md** if adding new components

### Documentation Page Template

```razor
@page "/components/my-component"

<DocsSection Title="MyComponent" SubTitle="Brief description">
    Detailed description of the component and its use cases.
</DocsSection>

<MudText Typo="Typo.h5" GutterBottom="true">Basic Usage</MudText>

<ComponentExample Title="Example" Code="@_code">
    <MyComponent Title="Demo" />
</ComponentExample>

@code {
    private const string _code = @"<MyComponent Title=""Demo"" />";
}
```

## ❓ Questions?

- Open an [issue](https://github.com/daniwxcode/MudExtended/issues) for bugs or feature requests
- Start a [discussion](https://github.com/daniwxcode/MudExtended/discussions) for questions

## 📄 License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing to MudExtended! 🎉
