# IPB2 Figure Store Management System

This repository now follows the same multi-project pattern as your referenced `EmployeeLeaveManagementSystem`, while using your own `FigureStoreDbContext` and entities as the data source.

## Project Structure

- `IPB2.FigureStoreManagementSystem.DB`  
  EF Core models and `FigureStoreDbContext` from your existing database.

- `IPB2.FigureStoreManagementSystem.Domain`  
  Shared DTOs and service contracts used between API and client apps.

- `IPB2.FigureStoreManagementSystem.WebApi`  
  Controller-based API with:
  - `api/categories` (read categories)
  - `api/figures` (CRUD for figures)

- `IPB2.FigureStoreManagementSystem.MVCwithHttpClient`  
  MVC client that calls `WebApi` using `HttpClient` and renders a figure catalog page.

- `IPB2.FigureStoreManagementSystem.MinimalApi`  
  Lightweight API entry point scaffold.

- `IPB2.FigureStoreManagementSystem.MVC`  
  Standard MVC scaffold entry point.

- `IPB2.FigureStoreManagementSystem.CA`  
  Console application with menu-based CRUD for figures/categories.

- `IPB2.FigureStoreManagementSystem.WinForm`  
  WinForms desktop scaffold.

## Solution

All projects are included in `IPB2.FigureStoreManagementSystem.DB.slnx`.

## Configuration

Set one of the following for database connection:

1. `ConnectionStrings:FigureStore` in appsettings (WebApi).
2. `FIGURE_STORE_CONNECTION_STRING` environment variable.

`FigureStoreDbContext` now supports environment-variable based configuration when DI options are not preconfigured.
