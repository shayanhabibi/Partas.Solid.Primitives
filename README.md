# Partas.Solid.Primitives

Monorepo containing version controlled projects for binding the primitives to be composed into higher order structures.

# Installation

Each primitive library can be installed through your preferred package manager the usual way

```
dotnet package add Partas.Solid.Primitives.Keyboard
```

But the best way to install (and the reason why they were structured like this) is to use Femto, so that the npm installation is handled for you

```
dotnet tool install femto --create-manifest-if-needed
dotnet femto install Partas.Solid.Primitives.Keyboard
```